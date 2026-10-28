import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    let holeId = "";
    let csvText = "";
    let mdIndex = -1;
    let incIndex = -1;
    let azIndex = -1;

    const contentType = req.headers.get("content-type") || "";

    if (contentType.includes("application/json")) {
      const body = await req.json();
      holeId = body.holeId;
      csvText = body.csvContent;
      const columnMap = body.columnMap; // { measuredDepth: number|string, inclination: number|string, azimuth: number|string }
      if (columnMap) {
        if (typeof columnMap.measuredDepth === "number") mdIndex = columnMap.measuredDepth;
        if (typeof columnMap.inclination === "number") incIndex = columnMap.inclination;
        if (typeof columnMap.azimuth === "number") azIndex = columnMap.azimuth;
      }
    } else if (contentType.includes("multipart/form-data")) {
      const formData = await req.formData();
      const file = formData.get("file") as File;
      holeId = formData.get("holeId") as string;
      const mdCol = formData.get("mdCol") as string;
      const incCol = formData.get("incCol") as string;
      const azCol = formData.get("azCol") as string;

      if (!file) {
        return NextResponse.json({ error: "No file provided" }, { status: 400 });
      }

      csvText = await file.text();
      if (mdCol !== null && mdCol !== undefined) mdIndex = parseInt(mdCol);
      if (incCol !== null && incCol !== undefined) incIndex = parseInt(incCol);
      if (azCol !== null && azCol !== undefined) azIndex = parseInt(azCol);
    }

    if (!holeId) {
      return NextResponse.json({ error: "Borehole selection is required" }, { status: 400 });
    }

    // Verify hole belongs to user
    const hole = await prisma.hole.findFirst({
      where: { id: holeId, userId: session.user.id },
    });

    if (!hole) {
      return NextResponse.json({ error: "Hole not found or access denied" }, { status: 404 });
    }

    const lines = csvText.split(/\r?\n/).filter((l) => l.trim().length > 0);
    if (lines.length < 2) {
      return NextResponse.json({ error: "CSV file must contain a header and at least one data row" }, { status: 400 });
    }

    const headerCols = lines[0].split(",").map((c) => c.trim().toLowerCase());

    // Auto-detect columns if not explicitly indexed
    if (mdIndex < 0) {
      mdIndex = headerCols.findIndex(
        (c) => c.includes("md") || c.includes("depth") || c.includes("measured")
      );
    }
    if (incIndex < 0) {
      incIndex = headerCols.findIndex(
        (c) => c.includes("inc") || c.includes("inclination") || c.includes("dip")
      );
    }
    if (azIndex < 0) {
      azIndex = headerCols.findIndex(
        (c) => c.includes("az") || c.includes("azimuth") || c.includes("bearing") || c.includes("dir")
      );
    }

    // Fallbacks if still not found
    if (mdIndex < 0) mdIndex = 0;
    if (incIndex < 0) incIndex = 1;
    if (azIndex < 0) azIndex = 2;

    const surveys = [];
    for (let i = 1; i < lines.length; i++) {
      const cols = lines[i].split(",").map((c) => c.trim());
      if (cols.length <= Math.max(mdIndex, incIndex, azIndex)) continue;

      const md = parseFloat(cols[mdIndex]);
      const inc = parseFloat(cols[incIndex]);
      const az = parseFloat(cols[azIndex]);

      if (!isNaN(md) && !isNaN(inc) && !isNaN(az)) {
        surveys.push({
          holeId,
          measuredDepth: md,
          inclination: inc,
          azimuth: az,
          trueVerticalDepth: null,
          northing: null,
          easting: null,
          verticalSection: null,
        });
      }
    }

    if (surveys.length === 0) {
      return NextResponse.json(
        { error: "No valid survey records found matching the column configuration" },
        { status: 400 }
      );
    }

    await prisma.survey.createMany({
      data: surveys,
    });

    return NextResponse.json({
      imported: surveys.length,
      message: `Successfully imported ${surveys.length} survey records`,
    });
  } catch (err: any) {
    return NextResponse.json({ error: err.message || "CSV import failed" }, { status: 500 });
  }
}
