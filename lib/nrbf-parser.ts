/**
 * NRBF (Microsoft .NET Remoting Binary Format) Parser
 *
 * Universal parser (browser, edge, and Node.js) for legacy
 * Ezy-Nav .ezn files that use .NET 2.0 BinaryFormatter serialization.
 * Reference: [MS-NRBF] https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-nrbf/
 *
 * Extracts:
 *   - HoleID, RigID, Location, Area, DrillingContractor, SurveyingContractor, Client
 *   - CreatedDate (DateTime Int64 ticks)
 *   - SideTrackMD (Decimal)
 *   - Collar: { Easting, Northing }
 *   - Target: { Easting, Northing, TVD }
 *   - Surveys[]: { MD, Dip, Azimuth }
 */

import type { EznHoleFileDto } from "./ezn-format";

const textDecoder = new TextDecoder("utf-8");
const textEncoder = new TextEncoder();

function toUint8Array(data: Uint8Array | ArrayBuffer | Buffer): Uint8Array {
  if (data instanceof Uint8Array) {
    return data;
  }
  return new Uint8Array(data);
}

/**
 * Searches for a byte pattern inside a Uint8Array.
 */
function findBytes(bytes: Uint8Array, pattern: number[] | string, startFrom = 0): number {
  const p = typeof pattern === "string" ? new TextEncoder().encode(pattern) : new Uint8Array(pattern);
  const end = bytes.length - p.length;
  for (let i = startFrom; i <= end; i++) {
    let match = true;
    for (let j = 0; j < p.length; j++) {
      if (bytes[i + j] !== p[j]) {
        match = false;
        break;
      }
    }
    if (match) return i;
  }
  return -1;
}

/**
 * Reads a 7-bit length-prefixed UTF-8 string from a Uint8Array.
 */
function readLenPrefixedStr(bytes: Uint8Array, pos: number): { str: string; nextPos: number } {
  let len = 0;
  let shift = 0;
  let cur = pos;
  while (cur < bytes.length) {
    const b = bytes[cur++];
    len |= (b & 0x7f) << shift;
    if ((b & 0x80) === 0) break;
    shift += 7;
  }
  if (len === 0) return { str: "", nextPos: cur };
  const strBytes = bytes.subarray(cur, cur + len);
  const str = textDecoder.decode(strBytes);
  return { str, nextPos: cur + len };
}

/**
 * Checks if the buffer starts with the NRBF binary header.
 * NRBF SerializationHeaderRecord starts with 0x00 0x01 0x00 0x00 0x00 0xFF 0xFF 0xFF 0xFF
 */
export function isNrbfBinary(data: Uint8Array | ArrayBuffer | Buffer): boolean {
  const bytes = toUint8Array(data);
  if (bytes.length < 9) return false;
  return (
    bytes[0] === 0x00 &&
    bytes[1] === 0x01 &&
    bytes[2] === 0x00 &&
    bytes[3] === 0x00 &&
    bytes[4] === 0x00 &&
    bytes[5] === 0xff &&
    bytes[6] === 0xff &&
    bytes[7] === 0xff &&
    bytes[8] === 0xff
  );
}

/**
 * Parses a legacy Ezy-Nav .ezn binary (.NET BinaryFormatter / NRBF) file.
 * Compatible with browsers, Edge, and Node.js.
 */
export function parseNrbfEznFile(data: Uint8Array | ArrayBuffer | Buffer): EznHoleFileDto {
  const bytes = toUint8Array(data);
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);

  // 1. DrillHoleFile class definition
  const dhIdx = findBytes(bytes, "EzyPlanStorage.DrillHoleFile");
  if (dhIdx === -1) {
    throw new Error("Invalid .ezn file: EzyPlanStorage.DrillHoleFile not found in binary stream");
  }

  const clientPos = findBytes(bytes, "m_client", dhIdx);
  if (clientPos === -1) {
    throw new Error("Invalid .ezn file: corrupt DrillHoleFile structure");
  }

  // Scan forward from clientPos to find member values start:
  // Look for 0x09 0x04 0x00 0x00 0x00 (ref to surveyList objId 4)
  let p = findBytes(bytes, [0x09, 0x04, 0x00, 0x00, 0x00], clientPos);
  if (p === -1) {
    p = clientPos + "m_client".length;
    while (p < bytes.length && bytes[p] !== 0x09) p++;
  }

  // Skip member 0, 1, 2 refs (3 * 5 bytes = 15 bytes)
  p += 15;

  // Member 3 (m_reportList): null (0x0a) or ref (0x09 + 4)
  if (bytes[p] === 0x0a) {
    p += 1;
  } else if (bytes[p] === 0x09) {
    p += 5;
  }

  // Member 4 (m_createdDate): 8 bytes DateTime ticks
  const lo = BigInt(view.getUint32(p, true));
  const hi = BigInt(view.getUint32(p + 4, true));
  const ticks = (lo | (hi << BigInt(32))) & BigInt("0x3fffffffffffffff");
  p += 8;
  const createdDate = new Date(Number((ticks - BigInt("621355968000000000")) / BigInt(10000))).toISOString();

  // Member 5 (m_sideTrackMD): decimal string
  const st = readLenPrefixedStr(bytes, p);
  p = st.nextPos;
  const sideTrackMD = parseFloat(st.str);

  // Members 6..12: strings (HoleID, RigID, Location, Area, DrillingContractor, SurveyingContractor, Client)
  const stringMembers: (string | null)[] = [];
  for (let m = 6; m <= 12; m++) {
    if (p >= bytes.length) break;
    const tag = bytes[p++];
    if (tag === 0x06) {
      p += 4; // skip objId
      const s = readLenPrefixedStr(bytes, p);
      p = s.nextPos;
      stringMembers.push(s.str);
    } else if (tag === 0x0a) {
      stringMembers.push(null);
    } else {
      stringMembers.push(null);
    }
  }

  const holeId = stringMembers[0] || "UNKNOWN";
  const rigId = stringMembers[1] || "RIG-01";
  const location = stringMembers[2] || null;
  const area = stringMembers[3] || null;
  const drillingContractor = stringMembers[4] || null;
  const surveyingContractor = stringMembers[5] || null;
  const client = stringMembers[6] || null;

  // 2. Target: EzyPlanData.Target
  let target = null;
  const tDef = findBytes(bytes, "EzyPlanData.Target\x03\x00\x00\x00");
  if (tDef !== -1) {
    const tvdPos = findBytes(bytes, "m_TVD", tDef);
    if (tvdPos !== -1) {
      // After m_TVD: len=0x05 + 'm_TVD'(5) + 3 nulls(0x00 0x00 0x00) + bTypes(0x05 0x05 0x05) + libId(4 bytes)
      let tp = tvdPos + "m_TVD".length + 3 + 3 + 4;
      const e = readLenPrefixedStr(bytes, tp);
      tp = e.nextPos;
      const n = readLenPrefixedStr(bytes, tp);
      tp = n.nextPos;
      const tvd = readLenPrefixedStr(bytes, tp);
      target = {
        Easting: parseFloat(e.str) || 0,
        Northing: parseFloat(n.str) || 0,
        TVD: parseFloat(tvd.str) || 0,
      };
    }
  }

  // 3. Collar: EzyPlanData.Collar
  let collar = null;
  const cDef = findBytes(bytes, "EzyPlanData.Collar\x02\x00\x00\x00");
  if (cDef !== -1) {
    const northPos = findBytes(bytes, "m_northing", cDef);
    if (northPos !== -1) {
      let cp = northPos + "m_northing".length + 2 + 2 + 4;
      const e = readLenPrefixedStr(bytes, cp);
      cp = e.nextPos;
      const n = readLenPrefixedStr(bytes, cp);
      collar = {
        Easting: parseFloat(e.str) || 0,
        Northing: parseFloat(n.str) || 0,
      };
    }
  }

  // 4. Surveys: EzyPlanData.Survey
  const surveys: { MD: number; Dip: number; Azimuth: number }[] = [];
  const sDef = findBytes(bytes, "EzyPlanData.Survey\x04\x00\x00\x00");
  if (sDef !== -1) {
    const pcePos = findBytes(bytes, "PropertyChangedEventHandler", sDef);
    if (pcePos !== -1) {
      // Find 4-byte libId (0x03 0x00 0x00 0x00) before first survey values
      let sp = pcePos + "PropertyChangedEventHandler".length;
      while (sp < bytes.length - 20) {
        if (bytes[sp] === 0x03 && bytes[sp + 1] === 0x00 && bytes[sp + 2] === 0x00 && bytes[sp + 3] === 0x00) {
          sp += 4;
          break;
        }
        sp++;
      }

      // First survey values:
      const md0 = readLenPrefixedStr(bytes, sp);
      sp = md0.nextPos;
      const dip0 = readLenPrefixedStr(bytes, sp);
      sp = dip0.nextPos;
      const az0 = readLenPrefixedStr(bytes, sp);
      sp = az0.nextPos;
      sp++; // skip null PropertyChanged (0x0a)

      const m0 = parseFloat(md0.str);
      const d0 = parseFloat(dip0.str);
      const a0 = parseFloat(az0.str);
      if (!isNaN(m0) && !isNaN(d0) && !isNaN(a0)) {
        surveys.push({ MD: m0, Dip: d0, Azimuth: a0 });
      }

      // Subsequent surveys (ClassWithId: 0x01 + 4-byte objId + 4-byte metaId + 3 strings + 0x0a)
      while (sp < bytes.length - 1) {
        const tag = bytes[sp++];
        if (tag === 0x0b) break; // MessageEnd
        if (tag === 0x01) {
          // ClassWithId
          sp += 8; // skip objId(4) + metaId(4)
          const md = readLenPrefixedStr(bytes, sp);
          sp = md.nextPos;
          const dip = readLenPrefixedStr(bytes, sp);
          sp = dip.nextPos;
          const az = readLenPrefixedStr(bytes, sp);
          sp = az.nextPos;
          sp++; // skip 0x0a

          const m = parseFloat(md.str);
          const d = parseFloat(dip.str);
          const a = parseFloat(az.str);
          if (!isNaN(m) && !isNaN(d) && !isNaN(a)) {
            surveys.push({ MD: m, Dip: d, Azimuth: a });
          }
        } else {
          break;
        }
      }
    }
  }

  surveys.sort((a, b) => a.MD - b.MD);

  return {
    HoleID: holeId,
    RigID: rigId,
    Location: location,
    Area: area,
    DrillingContractor: drillingContractor,
    SurveyingContractor: surveyingContractor,
    Client: client,
    CreatedDate: createdDate,
    SideTrackMD: isNaN(sideTrackMD) ? -1 : sideTrackMD,
    Collar: collar,
    Target: target,
    Surveys: surveys,
  };
}

export function serializeNrbfEznFile(data: EznHoleFileDto): Uint8Array {
  const buf: number[] = [];
  const writeByte = (b: number) => buf.push(b & 0xff);
  const writeInt32LE = (v: number) => {
    buf.push(v & 0xff, (v >> 8) & 0xff, (v >> 16) & 0xff, (v >> 24) & 0xff);
  };
  const writeInt64LE = (n: number | bigint) => {
    const big = typeof n === "bigint" ? n : BigInt(Math.round(Number(n)));
    const lo = Number(big & BigInt("0xFFFFFFFF"));
    const hi = Number((big >> BigInt(32)) & BigInt("0xFFFFFFFF"));
    writeInt32LE(lo);
    writeInt32LE(hi);
  };
  // 7-bit variable-length integer prefix + UTF-8 bytes
  const writeLenStr = (s: string) => {
    const bytes = new TextEncoder().encode(s);
    let rem = bytes.length;
    while (true) {
      const b = rem & 0x7f;
      rem >>= 7;
      if (rem === 0) { buf.push(b); break; }
      buf.push(b | 0x80);
    }
    for (let i = 0; i < bytes.length; i++) buf.push(bytes[i]);
  };
  // A Decimal in NRBF = 7-bit-prefixed ASCII string
  const writeDecimalStr = (v: number | string) => writeLenStr(String(v));

  // 1. SerializationHeaderRecord (0x00): rootId=1, headerId=-1, majorVer=1, minorVer=0
  writeByte(0x00);
  writeInt32LE(1);           // rootId
  writeInt32LE(0xFFFFFFFF); // headerId (-1)
  writeInt32LE(1);           // majorVersion
  writeInt32LE(0);           // minorVersion

  // 2. BinaryLibrary records (0x0C)
  writeByte(0x0C); writeInt32LE(2);
  writeLenStr("EzyPlanStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
  writeByte(0x0C); writeInt32LE(3);
  writeLenStr("EzyPlanData, Version=2.0.0.5, Culture=neutral, PublicKeyToken=null");

  // 3. DrillHoleFile ClassWithMembersAndTypes (0x05, objId=1, libId=2)
  writeByte(0x05); writeInt32LE(1);
  writeLenStr("EzyPlanStorage.DrillHoleFile");
  const drillMembers = [
    "m_surveyList", "m_target", "m_collar", "m_reportList",
    "m_createdDate", "m_sideTrackMD",
    "m_holeId", "m_rigId", "m_location", "m_area",
    "m_drillingContractor", "m_surveyingContractor", "m_client",
  ];
  writeInt32LE(drillMembers.length);
  for (const m of drillMembers) writeLenStr(m);
  // BinaryTypeEnums: 04=Class x4, 00=Primitive x2, 01=String x7
  const drillBinaryTypes = [0x04, 0x04, 0x04, 0x04, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01];
  for (const t of drillBinaryTypes) writeByte(t);
  // AdditionalInfos for Class (04): className + libId
  writeLenStr("EzyPlanData.SurveyList"); writeInt32LE(3);
  writeLenStr("EzyPlanData.Target");     writeInt32LE(3);
  writeLenStr("EzyPlanData.Collar");     writeInt32LE(3);
  writeLenStr("EzyPlanData.ReportList"); writeInt32LE(3);
  // AdditionalInfos for Primitive (00): PrimitiveTypeEnum
  writeByte(0x0D); // DateTime (13)
  writeByte(0x05); // Decimal  (5)
  // No extra for String (01)
  // LibraryId for DrillHoleFile class
  writeInt32LE(2);

  // 4. Values for DrillHoleFile
  writeByte(0x09); writeInt32LE(4);   // m_surveyList -> objId=4
  writeByte(0x09); writeInt32LE(5);   // m_target     -> objId=5
  writeByte(0x09); writeInt32LE(6);   // m_collar     -> objId=6
  writeByte(0x0A);                    // m_reportList  = Null
  // m_createdDate: DateTime ticks (Int64LE)
  const created = data.CreatedDate ? new Date(data.CreatedDate as string) : new Date();
  const ticks = BigInt(Math.floor(created.getTime() * 10000)) + BigInt("621355968000000000");
  writeInt64LE(ticks);
  // m_sideTrackMD: Decimal (7-bit len + ASCII)
  writeDecimalStr(data.SideTrackMD ?? -1);
  // m_holeId … m_client: BinaryObjectString records (0x06) with objIds 7-13
  const strVals = [
    data.HoleID                 ?? "",
    data.RigID                  ?? "RIG-01",
    data.Location               ?? "",
    data.Area                   ?? "",
    data.DrillingContractor     ?? "",
    data.SurveyingContractor    ?? "",
    data.Client                 ?? "",
  ];
  for (let i = 0; i < strVals.length; i++) {
    writeByte(0x06); writeInt32LE(7 + i); writeLenStr(strVals[i]);
  }

  // 5. SurveyList (0x05, objId=4, libId=3)
  writeByte(0x05); writeInt32LE(4);
  writeLenStr("EzyPlanData.SurveyList");
  writeInt32LE(1);
  writeLenStr("m_list");
  writeByte(0x04); // Class
  writeLenStr("EzyPlanData.SurveyList+Data"); writeInt32LE(3);
  writeInt32LE(3); // LibraryId for SurveyList
  writeByte(0x09); writeInt32LE(14); // -> SurveyList+Data objId=14

  // 6. Target (0x05, objId=5, libId=3) — Decimal members
  writeByte(0x05); writeInt32LE(5);
  writeLenStr("EzyPlanData.Target");
  writeInt32LE(3);
  writeLenStr("m_easting"); writeLenStr("m_northing"); writeLenStr("m_TVD");
  writeByte(0x00); writeByte(0x00); writeByte(0x00); // Primitive x3
  writeByte(0x05); writeByte(0x05); writeByte(0x05); // Decimal x3
  writeInt32LE(3);
  writeDecimalStr(data.Target?.Easting  ?? 0);
  writeDecimalStr(data.Target?.Northing ?? 0);
  writeDecimalStr(data.Target?.TVD      ?? 0);

  // 7. Collar (0x05, objId=6, libId=3) — Decimal members
  writeByte(0x05); writeInt32LE(6);
  writeLenStr("EzyPlanData.Collar");
  writeInt32LE(2);
  writeLenStr("m_easting"); writeLenStr("m_northing");
  writeByte(0x00); writeByte(0x00); // Primitive x2
  writeByte(0x05); writeByte(0x05); // Decimal x2
  writeInt32LE(3);
  writeDecimalStr(data.Collar?.Easting  ?? 0);
  writeDecimalStr(data.Collar?.Northing ?? 0);

  // 8. SurveyList+Data (objId=14, libId=3)
  writeByte(0x05);
  writeInt32LE(14);
  writeLenStr("EzyPlanData.SurveyList+Data");
  writeInt32LE(1);
  writeLenStr("items");
  writeByte(0x04);
  writeLenStr("EzyPlanData.SortableBindingList`1[[EzyPlanData.Survey, EzyPlanData, Version=2.0.0.5, Culture=neutral, PublicKeyToken=null]]");
  writeInt32LE(3);
  writeInt32LE(3);
  writeByte(0x09);
  writeInt32LE(15); // -> SortableBindingList objId 15

  // 9. SortableBindingList (objId=15) - Standard 696-byte .NET BinaryFormatter block
  // Base64 decoded represents the exact metadata graph for SortableBindingList<Survey> -> BindingList -> Collection -> List<Survey>
  const sortableBlockB64 =
    "BQ8AAAB7RXp5UGxhbkRhdGEuU29ydGFibGVCaW5kaW5nTGlzdGAxW1tFenlQbGFuRGF0YS5TdXJ2ZXksIEV6eVBsYW5EYXRhLCBWZXJzaW9uPTIuMC4wLjUsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dCAAAABdCaW5kaW5nTGlzdGAxK2FkZE5ld1BvcyRCaW5kaW5nTGlzdGAxK3JhaXNlTGlzdENoYW5nZWRFdmVudHMkQmluZGluZ0xpc3RgMStyYWlzZUl0ZW1DaGFuZ2VkRXZlbnRzFkJpbmRpbmdMaXN0YDErYWxsb3dOZXcXQmluZGluZ0xpc3RgMSthbGxvd0VkaXQZQmluZGluZ0xpc3RgMSthbGxvd1JlbW92ZR1CaW5kaW5nTGlzdGAxK3VzZXJTZXRBbGxvd05ldxJDb2xsZWN0aW9uYDEraXRlbXMAAAAAAAAAAwgBAQEBAQF7U3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFenlQbGFuRGF0YS5TdXJ2ZXksIEV6eVBsYW5EYXRhLCBWZXJzaW9uPTIuMC4wLjUsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAP////8BAAEBAQAJEAAAAAQQAAAAe1N5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbRXp5UGxhbkRhdGEuU3VydmV5LCBFenlQbGFuRGF0YSwgVmVyc2lvbj0yLjAuMC41LCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAFEV6eVBsYW5EYXRhLlN1cnZleVtdAwAAAAgI";
  if (typeof Buffer !== "undefined") {
    const sBuf = Buffer.from(sortableBlockB64, "base64");
    for (let i = 0; i < sBuf.length; i++) writeByte(sBuf[i]);
  } else {
    const binStr = atob(sortableBlockB64);
    for (let i = 0; i < binStr.length; i++) writeByte(binStr.charCodeAt(i));
  }

  // 10. List<Survey> values & ArrayInfo record (0x07)
  const surveys = data.Surveys || [];
  const nSurveys = surveys.length;
  let capacity = 4;
  while (capacity < nSurveys) {
    capacity *= 2;
  }
  if (capacity < 16 && nSurveys > 0) capacity = 16;
  if (nSurveys === 0) capacity = 0;

  writeByte(0x09);
  writeInt32LE(17); // _items ref -> objId 17
  writeInt32LE(nSurveys); // _size
  writeInt32LE(nSurveys); // _version

  // ArrayInfo record (0x07) for Survey[]
  writeByte(0x07);
  writeInt32LE(17); // objId 17
  writeByte(0x00); // 00 01 00 00 00 = rank (1) in BinaryFormatter 5-byte encoding
  writeByte(0x01);
  writeByte(0x00);
  writeByte(0x00);
  writeByte(0x00);
  writeInt32LE(capacity); // length = capacity
  writeByte(0x04); // Class
  writeLenStr("EzyPlanData.Survey");
  writeInt32LE(3); // libId = 3

  // Array elements: MemberReferences for each survey
  const SURVEY_BASE_ID = 18;
  for (let si = 0; si < nSurveys; si++) {
    writeByte(0x09);
    writeInt32LE(SURVEY_BASE_ID + si);
  }

  // Fill remaining slots with ObjectNullMultiple (0x0D) or ObjectNull (0x0A)
  const remainingNulls = capacity - nSurveys;
  if (remainingNulls === 1) {
    writeByte(0x0a);
  } else if (remainingNulls > 1) {
    writeByte(0x0d);
    writeByte(remainingNulls);
  }

  // System BinaryLibrary
  const sysLibId = SURVEY_BASE_ID + nSurveys;
  writeByte(0x0c);
  writeInt32LE(sysLibId);
  writeLenStr("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

  // 11. Survey objects
  if (nSurveys > 0) {
    for (let si = 0; si < nSurveys; si++) {
      const s = surveys[si];
      const surveyObjId = SURVEY_BASE_ID + si;
      if (si === 0) {
        // First survey: full ClassWithMembersAndTypes
        writeByte(0x05);
        writeInt32LE(surveyObjId);
        writeLenStr("EzyPlanData.Survey");
        writeInt32LE(4);
        writeLenStr("m_measuredDepth");
        writeLenStr("m_dip");
        writeLenStr("m_azimuth");
        writeLenStr("PropertyChanged");
        writeByte(0x00);
        writeByte(0x00);
        writeByte(0x00);
        writeByte(0x04);
        writeByte(0x05);
        writeByte(0x05);
        writeByte(0x05);
        writeLenStr("System.ComponentModel.PropertyChangedEventHandler");
        writeInt32LE(sysLibId);
        writeInt32LE(3); // Survey class belongs to EzyPlanData (libId=3)
      } else {
        // Subsequent surveys: ClassWithId (0x01) referencing first survey metadata
        writeByte(0x01);
        writeInt32LE(surveyObjId);
        writeInt32LE(SURVEY_BASE_ID);
      }
      writeDecimalStr(s.MD ?? 0);
      writeDecimalStr(s.Dip ?? 0);
      writeDecimalStr(s.Azimuth ?? 0);
      writeByte(0x0a); // PropertyChanged null
    }
  }

  // 12. MessageEnd
  writeByte(0x0B);
  return new Uint8Array(buf);
}