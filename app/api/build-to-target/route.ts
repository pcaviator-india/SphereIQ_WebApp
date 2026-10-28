import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { calculateTrajectory, isAboveTarget, calculateBuildPlan } from "@/lib/survey-math";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const { holeId, collar, stations, target, vsAzimuth = 0 } = body;

    if (!holeId || !collar || !stations || !target) {
      return NextResponse.json(
        { error: "holeId, collar, stations, and target are required" },
        { status: 400 }
      );
    }

    const trajectory = calculateTrajectory(
      { northing: collar.northing, easting: collar.easting, elevation: collar.elevation },
      stations,
      vsAzimuth
    );

    const lastSurvey = trajectory[trajectory.length - 1];
    const aboveTarget = isAboveTarget(trajectory, target.tvd);

    let buildPlan = null;
    if (aboveTarget) {
      buildPlan = calculateBuildPlan(
        lastSurvey.trueVerticalDepth,
        lastSurvey.inclination,
        target.tvd,
        target.northing,
        target.easting,
        lastSurvey.northing,
        lastSurvey.easting
      );
    }

    // Save target
    await prisma.target.upsert({
      where: { holeId },
      update: { tvd: target.tvd, northing: target.northing, easting: target.easting },
      create: { holeId, tvd: target.tvd, northing: target.northing, easting: target.easting },
    });

    return NextResponse.json({
      trajectory,
      aboveTarget,
      lastSurvey,
      buildPlan,
    });
  } catch {
    return NextResponse.json({ error: "Build calculation failed" }, { status: 500 });
  }
}
