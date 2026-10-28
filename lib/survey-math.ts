/**
 * Survey Math Library — ported from EzyNavMath.dll minimum curvature logic
 */

export interface SurveyStation {
  measuredDepth: number;
  inclination: number;
  azimuth: number;
}

export interface CalculatedSurvey extends SurveyStation {
  trueVerticalDepth: number;
  northing: number;
  easting: number;
  verticalSection: number;
  doglegSeverity: number;
}

export interface Collar {
  northing: number;
  easting: number;
  elevation: number;
}

const DEG_TO_RAD = Math.PI / 180;
const RAD_TO_DEG = 180 / Math.PI;

function degToRad(deg: number): number {
  return deg * DEG_TO_RAD;
}

function doglegAngle(s1: SurveyStation, s2: SurveyStation): number {
  const inc1 = degToRad(s1.inclination);
  const inc2 = degToRad(s2.inclination);
  const az1 = degToRad(s1.azimuth);
  const az2 = degToRad(s2.azimuth);

  const cosDogleg =
    Math.cos(inc1) * Math.cos(inc2) +
    Math.sin(inc1) * Math.sin(inc2) * Math.cos(az2 - az1);

  const clamped = Math.max(-1, Math.min(1, cosDogleg));
  return Math.acos(clamped);
}

function ratioFactor(dogleg: number): number {
  if (Math.abs(dogleg) < 1e-10) return 1.0;
  return (2 * Math.tan(dogleg / 2)) / dogleg;
}

export function calculateTrajectory(
  collar: Collar,
  stations: SurveyStation[],
  vsAzimuth: number = 0
): CalculatedSurvey[] {
  const results: CalculatedSurvey[] = [];

  let prevMD = 0;
  let prevInc = 0;
  let prevAz = 0;
  let tvd = 0;
  let northing = collar.northing;
  let easting = collar.easting;

  for (let i = 0; i < stations.length; i++) {
    const station = stations[i];
    const md = station.measuredDepth;
    const inc = station.inclination;
    const az = station.azimuth;

    const deltaMD = md - prevMD;

    if (i === 0) {
      const avgInc = degToRad((prevInc + inc) / 2);
      const avgAz = degToRad((prevAz + az) / 2);
      const dl = doglegAngle(
        { measuredDepth: 0, inclination: 0, azimuth: 0 },
        station
      );
      const rf = ratioFactor(dl);

      tvd += deltaMD * Math.cos(avgInc);
      northing += deltaMD * Math.sin(avgInc) * Math.cos(avgAz) * rf;
      easting += deltaMD * Math.sin(avgInc) * Math.sin(avgAz) * rf;

      const vs =
        (northing - collar.northing) * Math.cos(degToRad(vsAzimuth)) +
        (easting - collar.easting) * Math.sin(degToRad(vsAzimuth));

      results.push({
        ...station,
        trueVerticalDepth: tvd,
        northing,
        easting,
        verticalSection: vs,
        doglegSeverity: (dl * RAD_TO_DEG * 30) / Math.max(deltaMD, 0.1),
      });
    } else {
      const avgInc = degToRad((prevInc + inc) / 2);
      const avgAz = degToRad((prevAz + az) / 2);
      const dl = doglegAngle(
        { measuredDepth: prevMD, inclination: prevInc, azimuth: prevAz },
        station
      );
      const rf = ratioFactor(dl);

      tvd += deltaMD * Math.cos(avgInc);
      northing += deltaMD * Math.sin(avgInc) * Math.cos(avgAz) * rf;
      easting += deltaMD * Math.sin(avgInc) * Math.sin(avgAz) * rf;

      const vs =
        (northing - collar.northing) * Math.cos(degToRad(vsAzimuth)) +
        (easting - collar.easting) * Math.sin(degToRad(vsAzimuth));

      results.push({
        ...station,
        trueVerticalDepth: tvd,
        northing,
        easting,
        verticalSection: vs,
        doglegSeverity: (dl * RAD_TO_DEG * 30) / Math.max(deltaMD, 0.1),
      });
    }

    prevMD = md;
    prevInc = inc;
    prevAz = az;
  }

  return results;
}

export function interpolateTVD(
  trajectory: CalculatedSurvey[],
  targetMD: number
): number {
  if (trajectory.length === 0) return 0;
  if (targetMD <= trajectory[0].measuredDepth)
    return trajectory[0].trueVerticalDepth;
  if (targetMD >= trajectory[trajectory.length - 1].measuredDepth) {
    return trajectory[trajectory.length - 1].trueVerticalDepth;
  }

  for (let i = 1; i < trajectory.length; i++) {
    const prev = trajectory[i - 1];
    const curr = trajectory[i];
    if (targetMD >= prev.measuredDepth && targetMD <= curr.measuredDepth) {
      const t =
        (targetMD - prev.measuredDepth) /
        (curr.measuredDepth - prev.measuredDepth);
      return prev.trueVerticalDepth + t * (curr.trueVerticalDepth - prev.trueVerticalDepth);
    }
  }

  return trajectory[trajectory.length - 1].trueVerticalDepth;
}

export function isAboveTarget(
  trajectory: CalculatedSurvey[],
  targetTVD: number
): boolean {
  if (trajectory.length === 0) return true;
  const lastTVD = trajectory[trajectory.length - 1].trueVerticalDepth;
  return lastTVD < targetTVD;
}

export interface BuildPlan {
  buildRate: number;
  buildStartMD: number;
  buildEndMD: number;
  holdStartMD: number;
  totalMD: number;
}

export function calculateBuildPlan(
  currentTVD: number,
  currentInc: number,
  targetTVD: number,
  targetNorthing: number,
  targetEasting: number,
  currentNorthing: number,
  currentEasting: number,
  maxBuildRate: number = 5
): BuildPlan | null {
  const tvdDiff = targetTVD - currentTVD;
  if (tvdDiff <= 0) return null;

  const distHorizontal = Math.sqrt(
    Math.pow(targetNorthing - currentNorthing, 2) +
    Math.pow(targetEasting - currentEasting, 2)
  );

  const buildMD = (tvdDiff / Math.cos(degToRad(currentInc))) * 1.1;
  const buildRate = Math.min(maxBuildRate, ((90 - currentInc) * 30) / buildMD);

  return {
    buildRate,
    buildStartMD: 0,
    buildEndMD: buildMD,
    holdStartMD: buildMD,
    totalMD: buildMD + distHorizontal / Math.sin(degToRad(90)),
  };
}



// ── Distance & Navigation Calculations ported from SphereIQ Core / NavMath ──

export interface DistanceResult {
  north: number;
  east: number;
  horizontalDistance: number;
  tvd: number;
}

export function calculateDistanceToTarget(
  lastSurvey: { northing: number; easting: number; trueVerticalDepth: number },
  target: { northing: number; easting: number; tvd: number }
): DistanceResult {
  const north = target.northing - lastSurvey.northing;
  const east = target.easting - lastSurvey.easting;
  const hd = Math.sqrt(north * north + east * east);
  const tvd = target.tvd - lastSurvey.trueVerticalDepth;

  return {
    north: Math.abs(north),
    east: Math.abs(east),
    horizontalDistance: hd,
    tvd: tvd,
  };
}

export interface StraightPathResult {
  md: number;
  tvd: number;
  northing: number;
  easting: number;
  northDistance: number;
  eastDistance: number;
  horizontalDistance: number;
}

export function calculateStraightPathToTargetTVD(
  lastSurvey: {
    measuredDepth: number;
    inclination: number;
    azimuth: number;
    trueVerticalDepth: number;
    northing: number;
    easting: number;
  },
  target: { tvd: number; northing: number; easting: number }
): StraightPathResult | null {
  const deltaTVD = target.tvd - lastSurvey.trueVerticalDepth;
  if (deltaTVD <= 0) return null;

  // In EzyPlan, dip can be given as negative or positive.
  // We use inclination from vertical (or dip magnitude).
  const dipRad = degToRad(Math.abs(lastSurvey.inclination));
  const azRad = degToRad(lastSurvey.azimuth);

  // Vertical factor
  const cosDip = Math.cos(dipRad);
  if (Math.abs(cosDip) < 1e-4) return null;

  const deltaMD = deltaTVD / cosDip;
  const endMD = lastSurvey.measuredDepth + deltaMD;
  const endTVD = target.tvd;

  const deltaNorth = deltaMD * Math.sin(dipRad) * Math.cos(azRad);
  const deltaEast = deltaMD * Math.sin(dipRad) * Math.sin(azRad);

  const endNorthing = lastSurvey.northing + deltaNorth;
  const endEasting = lastSurvey.easting + deltaEast;

  const diffNorth = Math.abs(target.northing - endNorthing);
  const diffEast = Math.abs(target.easting - endEasting);
  const hd = Math.sqrt(diffNorth * diffNorth + diffEast * diffEast);

  return {
    md: endMD,
    tvd: endTVD,
    northing: endNorthing,
    easting: endEasting,
    northDistance: diffNorth,
    eastDistance: diffEast,
    horizontalDistance: hd,
  };
}

export interface MinDoglegResult {
  dogleg: number;
  severity: "normal" | "severe" | "extreme";
  stateString: string;
}

export function calculateMinDoglegToTarget(
  lastSurvey: {
    northing: number;
    easting: number;
    trueVerticalDepth: number;
    inclination: number;
    azimuth: number;
  },
  target: { northing: number; easting: number; tvd: number },
  severeThreshold: number = 3.0,
  extremeThreshold: number = 7.0
): MinDoglegResult | null {
  const tvdDiff = target.tvd - lastSurvey.trueVerticalDepth;
  if (tvdDiff <= 0) return null;

  const dx = target.easting - lastSurvey.easting;
  const dy = target.northing - lastSurvey.northing;
  const dz = tvdDiff;
  const totalDist = Math.sqrt(dx * dx + dy * dy + dz * dz);
  if (totalDist < 1e-3) return { dogleg: 0, severity: "normal", stateString: "0.00" };

  // Target direction unit vector
  const tx = dx / totalDist;
  const ty = dy / totalDist;
  const tz = dz / totalDist;

  // Current station tangential unit vector
  const incRad = degToRad(Math.abs(lastSurvey.inclination));
  const azRad = degToRad(lastSurvey.azimuth);
  const sx = Math.sin(incRad) * Math.sin(azRad);
  const sy = Math.sin(incRad) * Math.cos(azRad);
  const sz = Math.cos(incRad);

  // Angle alpha between current direction and target vector
  const dot = Math.max(-1, Math.min(1, sx * tx + sy * ty + sz * tz));
  const alpha = Math.acos(dot); // radians

  // Minimum dogleg required over circular arc to target
  // DLS (°/30m) = 2 * sin(alpha) * (180/pi) * 30 / totalDist
  const doglegVal = Math.abs((2 * Math.sin(alpha) * RAD_TO_DEG * 30) / totalDist);
  const roundedDL = Math.round(doglegVal * 100) / 100;

  let severity: "normal" | "severe" | "extreme" = "normal";
  if (roundedDL >= extremeThreshold) {
    severity = "extreme";
  } else if (roundedDL >= severeThreshold) {
    severity = "severe";
  }

  return {
    dogleg: roundedDL,
    severity,
    stateString: roundedDL.toFixed(2),
  };
}
