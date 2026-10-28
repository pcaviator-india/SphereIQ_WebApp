import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { calculateTrajectory } from "@/lib/survey-math";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const { holeId, collar, stations, targetMD, vsAzimuth = 0 } = body;

    if (!holeId || !collar || !stations || !targetMD) {
      return NextResponse.json(
        { error: "holeId, collar, stations, and targetMD are required" },
        { status: 400 }
      );
    }

    const trajectory = calculateTrajectory(
      { northing: collar.northing, easting: collar.easting, elevation: collar.elevation },
      stations,
      vsAzimuth
    );

    if (trajectory.length === 0) {
      return NextResponse.json({ error: "No trajectory data" }, { status: 400 });
    }

    const lastStation = trajectory[trajectory.length - 1];

    // Simple linear extrapolation from last survey station
    const deltaMD = targetMD - lastStation.measuredDepth;
    if (deltaMD <= 0) {
      return NextResponse.json(
        { error: "Target MD must be greater than last survey MD" },
        { status: 400 }
      );
    }

    // Use last station's inclination and azimuth for extrapolation
    const incRad = (lastStation.inclination * Math.PI) / 180;
    const azRad = (lastStation.azimuth * Math.PI) / 180;

    const extrapolatedTVD = lastStation.trueVerticalDepth + deltaMD * Math.cos(incRad);
    const extrapolatedNorthing = lastStation.northing + deltaMD * Math.sin(incRad) * Math.cos(azRad);
    const extrapolatedEasting = lastStation.easting + deltaMD * Math.sin(incRad) * Math.sin(azRad);

    const vs =
      (extrapolatedNorthing - collar.northing) * Math.cos((vsAzimuth * Math.PI) / 180) +
      (extrapolatedEasting - collar.easting) * Math.sin((vsAzimuth * Math.PI) / 180);

    const extrapolation = {
      measuredDepth: targetMD,
      inclination: lastStation.inclination,
      azimuth: lastStation.azimuth,
      trueVerticalDepth: extrapolatedTVD,
      northing: extrapolatedNorthing,
      easting: extrapolatedEasting,
      verticalSection: vs,
      doglegSeverity: 0,
      isExtrapolated: true,
    };

    return NextResponse.json({ extrapolation, trajectory });
  } catch {
    return NextResponse.json({ error: "Extrapolation calculation failed" }, { status: 500 });
  }
}
