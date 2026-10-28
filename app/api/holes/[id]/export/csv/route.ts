import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { serializeSurveyCsv } from "@/lib/ezn-format";

export async function GET(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const hole = await prisma.hole.findUnique({
    where: { id: params.id },
    include: {
      collar: true,
      target: true,
      surveys: { orderBy: { measuredDepth: "asc" } },
    },
  });

  if (!hole) {
    return NextResponse.json({ error: "Hole not found" }, { status: 404 });
  }

  const csvString = serializeSurveyCsv({
    holeId: hole.holeId,
    rigId: hole.rigId,
    name: hole.name,
    client: hole.client,
    location: hole.location,
    collar: hole.collar
      ? {
          northing: Number(hole.collar.northing),
          easting: Number(hole.collar.easting),
          elevation: Number(hole.collar.elevation),
        }
      : null,
    target: hole.target
      ? {
          tvd: Number(hole.target.tvd),
          northing: Number(hole.target.northing),
          easting: Number(hole.target.easting),
        }
      : null,
    surveys: hole.surveys.map((s) => ({
      measuredDepth: Number(s.measuredDepth),
      inclination: Number(s.inclination),
      azimuth: Number(s.azimuth),
      trueVerticalDepth: s.trueVerticalDepth !== null ? Number(s.trueVerticalDepth) : null,
      northing: s.northing !== null ? Number(s.northing) : null,
      easting: s.easting !== null ? Number(s.easting) : null,
      verticalSection: s.verticalSection !== null ? Number(s.verticalSection) : null,
    })),
  });

  const safeFilename = `${hole.holeId.replace(/[^a-zA-Z0-9_-]/g, "_")}_surveys.csv`;

  return new NextResponse(csvString, {
    headers: {
      "Content-Type": "text/csv; charset=utf-8",
      "Content-Disposition": `attachment; filename="${safeFilename}"`,
    },
  });
}

