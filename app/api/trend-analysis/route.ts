import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const { holeId, surveys } = body;

    if (!holeId || !surveys || !Array.isArray(surveys) || surveys.length < 2) {
      return NextResponse.json(
        { error: "holeId and at least 2 surveys are required" },
        { status: 400 }
      );
    }

    // Calculate trend statistics
    const mds = surveys.map((s: any) => s.measuredDepth);
    const incs = surveys.map((s: any) => s.inclination);
    const azs = surveys.map((s: any) => s.azimuth);
    const tvds = surveys.map((s: any) => s.trueVerticalDepth);

    const avg = (arr: number[]) => arr.reduce((a, b) => a + b, 0) / arr.length;

    const trend = {
      holeId,
      averageInclination: avg(incs),
      averageAzimuth: avg(azs),
      averageTVD: avg(tvds),
      totalMeasuredDepth: mds[mds.length - 1] - mds[0],
      totalTVDChange: tvds[tvds.length - 1] - tvds[0],
      inclinationRange: {
        min: Math.min(...incs),
        max: Math.max(...incs),
      },
      azimuthRange: {
        min: Math.min(...azs),
        max: Math.max(...azs),
      },
      surveyCount: surveys.length,
      doglegSeverityTrend: surveys.slice(1).map((s: any, i) => ({
        measuredDepth: s.measuredDepth,
        dls: s.doglegSeverity || 0,
      })),
    };

    return NextResponse.json({ trend });
  } catch {
    return NextResponse.json({ error: "Trend analysis failed" }, { status: 500 });
  }
}
