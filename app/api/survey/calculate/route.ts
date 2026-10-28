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
    const { holeId, collar, stations, vsAzimuth = 0 } = body;

    if (!holeId || !collar || !stations || !Array.isArray(stations)) {
      return NextResponse.json(
        { error: "holeId, collar, and stations array are required" },
        { status: 400 }
      );
    }

    const trajectory = calculateTrajectory(
      { northing: collar.northing, easting: collar.easting, elevation: collar.elevation },
      stations,
      vsAzimuth
    );

    // Save/update collar
    await prisma.collar.upsert({
      where: { holeId },
      update: {
        northing: collar.northing,
        easting: collar.easting,
        elevation: collar.elevation,
      },
      create: {
        holeId,
        northing: collar.northing,
        easting: collar.easting,
        elevation: collar.elevation,
      },
    });

    // Replace existing surveys with new calculated ones
    await prisma.survey.deleteMany({ where: { holeId } });
    await prisma.survey.createMany({
      data: trajectory.map((s, i) => ({
        holeId,
        measuredDepth: s.measuredDepth,
        inclination: s.inclination,
        azimuth: s.azimuth,
        trueVerticalDepth: s.trueVerticalDepth,
        northing: s.northing,
        easting: s.easting,
        verticalSection: s.verticalSection,
      })),
    });

    return NextResponse.json({ trajectory });
  } catch {
    return NextResponse.json({ error: "Calculation failed" }, { status: 500 });
  }
}
