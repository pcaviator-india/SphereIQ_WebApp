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

function writeInt32LE(buf: Uint8Array, pos: number, value: number): number {
  const view = new DataView(buf.buffer, buf.byteOffset, buf.byteLength);
  view.setUint32(pos, value, true);
  return pos + 4;
}

function writeLenPrefixedStr(buf: Uint8Array, pos: number, s: string | null): number {
  const encoder = new TextEncoder();
  const bytes = encoder.encode(s ?? "");
  if (bytes.length === 0) {
    buf[pos++] = 0x00;
    return pos;
  }
  let len = bytes.length;
  while (true) {
    let b = len & 0x7f;
    len >>= 7;
    if (len !== 0) { buf[pos++] = b | 0x80; } else { buf[pos++] = b; break; }
  }
  buf.set(bytes, pos);
  return pos + bytes.length;
}

export function serializeNrbfEznFile(data: EznHoleFileDto): Uint8Array {
  const buf: number[] = [];
  const writeByte = (b: number) => buf.push(b);
  const writeInt32LE = (v: number) => {
    buf.push(v & 0xff, (v >> 8) & 0xff, (v >> 16) & 0xff, (v >> 24) & 0xff);
  };
  const writeLenPrefixedStr = (s: string) => {
    const bytes = new TextEncoder().encode(s);
    let len = bytes.length;
    while (true) {
      let b = len & 0x7f;
      len >>= 7;
      if (len === 0) { writeByte(b); break; }
      writeByte(b | 0x80);
    }
    for (let i = 0; i < bytes.length; i++) writeByte(bytes[i]);
  };

  writeByte(0x00); writeByte(0x01); // ROOT
  writeInt32LE(0); writeInt32LE(1); writeInt32LE(0);

  writeByte(0x0C); writeInt32LE(2);
  writeLenPrefixedStr("EzyPlanStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

  writeByte(0x0C); writeInt32LE(3);
  writeLenPrefixedStr("EzyPlanData, Version=2.0.0.5, Culture=neutral, PublicKeyToken=null");

  writeByte(0x05); writeInt32LE(1);
  const drillName = "EzyPlanStorage.DrillHoleFile";
  writeLenPrefixedStr(drillName);

  const drillMembers = [
    "m_surveyList", "m_target", "m_collar", "m_reportList",
    "m_createdDate", "m_sideTrackMD",
    "m_holeId", "m_rigId", "m_location", "m_area",
    "m_drillingContractor", "m_surveyingContractor", "m_client"
  ];
  writeInt32LE(drillMembers.length);
  for (const m of drillMembers) writeLenPrefixedStr(m);

  const drillTypes = [0x05, 0x05, 0x05, 0x05, 0x0C, 0x0C, 0x06, 0x06, 0x06, 0x06, 0x06, 0x06, 0x06];
  for (const t of drillTypes) writeByte(t);

  writeByte(0x09); writeInt32LE(4);
  writeByte(0x09); writeInt32LE(2);
  writeByte(0x09); writeInt32LE(3);
  writeByte(0x0A);
  const created = data.CreatedDate ? new Date(data.CreatedDate) : new Date();
  const ticks = BigInt(Math.floor(created.getTime() * 10000)) + BigInt("621355968000000000");
  writeInt32LE(Number(ticks & BigInt("0xFFFFFFFF")));
  writeInt32LE(Number((ticks >> BigInt(32)) & BigInt("0xFFFFFFFF")));
  writeByte(0x06); writeLenPrefixedStr(String(data.SideTrackMD ?? -1));
  const holeStrs = [data.HoleID, data.RigID ?? "RIG-01", data.Location ?? "", data.Area ?? "",
    data.DrillingContractor ?? "", data.SurveyingContractor ?? "", data.Client ?? ""];
  for (const v of holeStrs) { writeByte(0x06); writeLenPrefixedStr(v); }

  writeByte(0x05); writeInt32LE(2);
  writeLenPrefixedStr("EzyPlanData.Target");
  const targetMembers = ["m_easting", "m_northing", "m_TVD"];
  writeInt32LE(targetMembers.length);
  for (const m of targetMembers) writeLenPrefixedStr(m);
  writeInt32LE(3);
  for (let i = 0; i < 3; i++) writeByte(0x0C);
  const targetVals = [String(data.Target?.Easting ?? 0), String(data.Target?.Northing ?? 0), String(data.Target?.TVD ?? 0)];
  for (const v of targetVals) { writeByte(0x06); writeLenPrefixedStr(v); }

  writeByte(0x05); writeInt32LE(3);
  writeLenPrefixedStr("EzyPlanData.Collar");
  const collarMembers = ["m_easting", "m_northing"];
  writeInt32LE(collarMembers.length);
  for (const m of collarMembers) writeLenPrefixedStr(m);
  writeInt32LE(2);
  for (let i = 0; i < 2; i++) writeByte(0x0C);
  const collarVals = [String(data.Collar?.Easting ?? 0), String(data.Collar?.Northing ?? 0)];
  for (const v of collarVals) { writeByte(0x06); writeLenPrefixedStr(v); }

  const surveys = data.Surveys || [];
  writeByte(0x05); writeInt32LE(4);
  writeLenPrefixedStr("EzyPlanData.SurveyList");
  writeInt32LE(1);
  writeLenPrefixedStr("m_list");
  writeInt32LE(1);
  writeByte(0x05);
  writeLenPrefixedStr("EzyPlanData.SurveyList+Data");
  writeInt32LE(surveys.length);
  for (let si = 0; si < surveys.length; si++) {
    const surveyObjId = 10 + si;
    writeByte(0x09); writeInt32LE(surveyObjId);
  }

  for (let si = 0; si < surveys.length; si++) {
    const s = surveys[si];
    const surveyObjId = 10 + si;
    writeByte(0x05); writeInt32LE(surveyObjId);
    writeLenPrefixedStr("EzyPlanData.Survey");
    const surveyMembers = ["m_measuredDepth", "m_dip", "m_azimuth"];
    writeInt32LE(surveyMembers.length);
    for (const m of surveyMembers) writeLenPrefixedStr(m);
    writeInt32LE(3);
    for (let i = 0; i < 3; i++) writeByte(0x0C);
    const surveyVals = [String(s.MD ?? 0), String(s.Dip ?? 0), String(s.Azimuth ?? 0)];
    for (const v of surveyVals) { writeByte(0x06); writeLenPrefixedStr(v); }
  }

  writeByte(0x0B);
  return new Uint8Array(buf);
}


