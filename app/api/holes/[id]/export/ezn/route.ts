import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { serializeEznFile } from "@/lib/ezn-format";

export async function GET(
  req: NextRequest,
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

  const { searchParams } = new URL(req.url);
  const format = (searchParams.get("format") ?? "json") as "nrbf" | "json";

  const eznResult = serializeEznFile({
    holeId: hole.holeId,
    rigId: hole.rigId,
    name: hole.name,
    location: hole.location,
    area: hole.area,
    drillingContractor: hole.drillingContractor,
    surveyingContractor: hole.surveyingContractor,
    client: hole.client,
    createdAt: hole.createdAt,
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
    })),
    format,
  });

  const safeFilename = `${hole.holeId.replace(/[^a-zA-Z0-9_-]/g, "_")}.ezn`;

  const isBinary = format === "nrbf";
  const body = isBinary && eznResult instanceof Uint8Array ? Buffer.from(eznResult) : eznResult;

  return new NextResponse(body as unknown as BodyInit, {
    headers: {
      "Content-Type": isBinary ? "application/octet-stream" : "application/json",
      "Content-Disposition": `attachment; filename="${safeFilename}"`,
    },
  });
}

