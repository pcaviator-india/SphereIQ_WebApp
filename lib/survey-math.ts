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

