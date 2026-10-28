/**
 * EZN File Format Handler
 * Implements the portable .ezn hole-file format from SphereIQ / Ezy-Nav:
 * - Magic header: "EZN1\n"
 * - JSON payload representing HoleFileDto
 */

import { isNrbfBinary, parseNrbfEznFile } from "./nrbf-parser";

export interface EznSurveyRow {
  MD: number;
  Dip: number; // In directional drilling, dip/inclination
  Azimuth: number;
}

export interface EznCollar {
  Easting: number;
  Northing: number;
  Elevation?: number;
}

export interface EznTarget {
  Easting: number;
  Northing: number;
  TVD: number;
}

export interface EznHoleFileDto {
  HoleID: string;
  RigID?: string;
  Location?: string | null;
  Area?: string | null;
  DrillingContractor?: string | null;
  SurveyingContractor?: string | null;
  Client?: string | null;
  CreatedDate?: string | Date;
  SideTrackMD?: number;
  Collar?: EznCollar | null;
  Target?: EznTarget | null;
  Surveys?: EznSurveyRow[];
}

export const EZN_MAGIC = "EZN1";

/**
 * Parses an .ezn file content (string, Buffer, Uint8Array, or ArrayBuffer) into an EznHoleFileDto object.
 * Supports both:
 * 1. Legacy Ezy-Nav binary (.NET BinaryFormatter / NRBF) files
 * 2. Modern SphereIQ EZN1 JSON files (with or without magic header)
 */
export function parseEznFile(content: string | Buffer | Uint8Array | ArrayBuffer): EznHoleFileDto {
  // 1. Check for legacy NRBF binary format
  if (typeof content !== "string") {
    if (isNrbfBinary(content)) {
      return parseNrbfEznFile(content);
    }
  } else {
    // If passed as binary string, check first bytes for NRBF header (\x00\x01\x00\x00\x00\xff\xff\xff\xff)
    if (content.length >= 9 && content.charCodeAt(0) === 0x00 && content.charCodeAt(1) === 0x01) {
      const bytes = new Uint8Array(content.length);
      for (let i = 0; i < content.length; i++) {
        bytes[i] = content.charCodeAt(i) & 0xff;
      }
      if (isNrbfBinary(bytes)) {
        return parseNrbfEznFile(bytes);
      }
    }
  }

  // 2. Modern JSON / EZN1 format
  let text = typeof content === "string" ? content : new TextDecoder("utf-8").decode(content instanceof Uint8Array ? content : new Uint8Array(content));

  // Remove potential UTF-8 BOM
  if (text.charCodeAt(0) === 0xfeff) {
    text = text.slice(1);
  }

  // Check and strip magic header "EZN1"
  if (text.startsWith(EZN_MAGIC)) {
    text = text.slice(EZN_MAGIC.length);
    // Strip leading newline / whitespace
    text = text.replace(/^\r?\n/, "").trim();
  } else {
    text = text.trim();
  }

  let raw: any;
  try {
    raw = JSON.parse(text);
  } catch (err: any) {
    throw new Error("Invalid .ezn file: payload is not valid JSON (" + err.message + ")");
  }

  // Normalize fields case-insensitively
  const getProp = (obj: any, ...keys: string[]) => {
    if (!obj || typeof obj !== "object") return undefined;
    for (const k of keys) {
      if (obj[k] !== undefined) return obj[k];
      const found = Object.keys(obj).find((x) => x.toLowerCase() === k.toLowerCase());
      if (found && obj[found] !== undefined) return obj[found];
    }
    return undefined;
  };

  const holeId = getProp(raw, "HoleID", "holeId", "id", "name");
  if (!holeId) {
    throw new Error("Invalid .ezn file: Missing required HoleID property");
  }

  const collarRaw = getProp(raw, "Collar", "collar");
  let collar: EznCollar | null = null;
  if (collarRaw) {
    collar = {
      Easting: Number(getProp(collarRaw, "Easting", "easting") ?? 0),
      Northing: Number(getProp(collarRaw, "Northing", "northing") ?? 0),
      Elevation: Number(getProp(collarRaw, "Elevation", "elevation") ?? 0),
    };
  }

  const targetRaw = getProp(raw, "Target", "target");
  let target: EznTarget | null = null;
  if (targetRaw) {
    target = {
      Easting: Number(getProp(targetRaw, "Easting", "easting") ?? 0),
      Northing: Number(getProp(targetRaw, "Northing", "northing") ?? 0),
      TVD: Number(getProp(targetRaw, "TVD", "tvd") ?? 0),
    };
  }

  const surveysRaw = getProp(raw, "Surveys", "surveys", "surveyList") || [];
  const surveys: EznSurveyRow[] = [];
  if (Array.isArray(surveysRaw)) {
    for (const s of surveysRaw) {
      const md = Number(getProp(s, "MD", "md", "measuredDepth") ?? 0);
      const dip = Number(getProp(s, "Dip", "dip", "inclination", "inc") ?? 0);
      const az = Number(getProp(s, "Azimuth", "azimuth", "az") ?? 0);
      if (!isNaN(md) && !isNaN(dip) && !isNaN(az)) {
        surveys.push({ MD: md, Dip: dip, Azimuth: az });
      }
    }
  }

  // Sort surveys ascending by MD
  surveys.sort((a, b) => a.MD - b.MD);

  return {
    HoleID: String(holeId),
    RigID: String(getProp(raw, "RigID", "rigId") || "RIG-01"),
    Location: getProp(raw, "Location", "location") || null,
    Area: getProp(raw, "Area", "area") || null,
    DrillingContractor: getProp(raw, "DrillingContractor", "drillingContractor") || null,
    SurveyingContractor: getProp(raw, "SurveyingContractor", "surveyingContractor") || null,
    Client: getProp(raw, "Client", "client") || null,
    CreatedDate: getProp(raw, "CreatedDate", "createdDate") || new Date().toISOString(),
    SideTrackMD: Number(getProp(raw, "SideTrackMD", "sideTrackMD") ?? -1),
    Collar: collar,
    Target: target,
    Surveys: surveys,
  };
}

/**
 * Serializes borehole data into an official SphereIQ .ezn binary/text buffer.
 * Starts with "EZN1\n" followed by JSON.
 */
export function serializeEznFile(data: {
  holeId: string;
  rigId?: string | null;
  name?: string | null;
  location?: string | null;
  area?: string | null;
  drillingContractor?: string | null;
  surveyingContractor?: string | null;
  client?: string | null;
  createdAt?: Date | string;
  sideTrackMd?: number | null;
  collar?: { northing: number | string; easting: number | string; elevation?: number | string | null } | null;
  target?: { tvd: number | string; northing: number | string; easting: number | string } | null;
  surveys?: Array<{
    measuredDepth: number | string;
    inclination: number | string;
    azimuth: number | string;
  }>;
}): string {
  const dto: EznHoleFileDto = {
    HoleID: data.holeId,
    RigID: data.rigId || "RIG-01",
    Location: data.location || null,
    Area: data.area || null,
    DrillingContractor: data.drillingContractor || null,
    SurveyingContractor: data.surveyingContractor || null,
    Client: data.client || null,
    CreatedDate: data.createdAt ? new Date(data.createdAt).toISOString() : new Date().toISOString(),
    SideTrackMD: data.sideTrackMd !== null && data.sideTrackMd !== undefined ? Number(data.sideTrackMd) : -1,
    Collar: data.collar
      ? {
        Easting: Number(data.collar.easting),
        Northing: Number(data.collar.northing),
        Elevation: Number(data.collar.elevation ?? 0),
      }
      : null,
    Target: data.target
      ? {
        Easting: Number(data.target.easting),
        Northing: Number(data.target.northing),
        TVD: Number(data.target.tvd),
      }
      : null,
    Surveys: (data.surveys || []).map((s) => ({
      MD: Number(s.measuredDepth),
      Dip: Number(s.inclination),
      Azimuth: Number(s.azimuth),
    })),
  };

  return `${EZN_MAGIC}\n${JSON.stringify(dto, null, 2)}`;
}

/**
 * Serializes borehole surveys into CSV format.
 */
export function serializeSurveyCsv(data: {
  holeId: string;
  rigId?: string | null;
  name?: string | null;
  client?: string | null;
  location?: string | null;
  collar?: { northing: number | string; easting: number | string; elevation?: number | string | null } | null;
  target?: { tvd: number | string; northing: number | string; easting: number | string } | null;
  surveys: Array<{
    measuredDepth: number | string;
    inclination: number | string;
    azimuth: number | string;
    trueVerticalDepth?: number | string | null;
    northing?: number | string | null;
    easting?: number | string | null;
    verticalSection?: number | string | null;
    doglegSeverity?: number | string | null;
  }>;
}): string {
  const lines: string[] = [];
  lines.push(`# SphereIQ Borehole Survey Export`);
  lines.push(`# Hole ID: ${data.holeId}, Rig ID: ${data.rigId || "RIG-01"}, Name: ${data.name || data.holeId}`);
  if (data.client) lines.push(`# Client: ${data.client}`);
  if (data.location) lines.push(`# Location: ${data.location}`);

  if (data.collar) {
    lines.push(
      `# Collar: Northing=${Number(data.collar.northing).toFixed(2)}, Easting=${Number(data.collar.easting).toFixed(2)}, Elevation=${Number(data.collar.elevation ?? 0).toFixed(2)}`
    );
  }
  if (data.target) {
    lines.push(
      `# Target: TVD=${Number(data.target.tvd).toFixed(2)}, Northing=${Number(data.target.northing).toFixed(2)}, Easting=${Number(data.target.easting).toFixed(2)}`
    );
  }

  lines.push(
    `MeasuredDepth,Inclination,Azimuth,TrueVerticalDepth,Northing,Easting,VerticalSection,DoglegSeverity`
  );

  for (const s of data.surveys) {
    const md = Number(s.measuredDepth).toFixed(2);
    const inc = Number(s.inclination).toFixed(2);
    const az = Number(s.azimuth).toFixed(2);
    const tvd = s.trueVerticalDepth !== null && s.trueVerticalDepth !== undefined ? Number(s.trueVerticalDepth).toFixed(2) : "";
    const n = s.northing !== null && s.northing !== undefined ? Number(s.northing).toFixed(2) : "";
    const e = s.easting !== null && s.easting !== undefined ? Number(s.easting).toFixed(2) : "";
    const vs = s.verticalSection !== null && s.verticalSection !== undefined ? Number(s.verticalSection).toFixed(2) : "";
    const dls = s.doglegSeverity !== null && s.doglegSeverity !== undefined ? Number(s.doglegSeverity).toFixed(2) : "";

    lines.push(`${md},${inc},${az},${tvd},${n},${e},${vs},${dls}`);
  }

  return lines.join("\r\n");
}

