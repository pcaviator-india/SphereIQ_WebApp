import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const formData = await req.formData();
    const file = formData.get("file") as File;
    const holeId = formData.get("holeId") as string;

    if (!file) {
      return NextResponse.json({ error: "No file provided" }, { status: 400 });
    }

    const text = await file.text();
    const lines = text.split("\n").filter((l) => l.trim());

    if (lines.length < 2) {
      return NextResponse.json({ error: "CSV file appears empty" }, { status: 400 });
    }

    // Simple CSV parser - expects: MD,Inclination,Azimuth
    const header = lines[0].toLowerCase();
    const hasMD = header.includes("md") || header.includes("depth");
    const hasInc = header.includes("inc") || header.includes("incl");
    const hasAz = header.includes("az") || header.includes("azimuth");

    const surveys = [];
    for (let i = 1; i < lines.length; i++) {
      const cols = lines[i].split(",");
      if (cols.length < 3) continue;

      const md = parseFloat(cols[0]);
      const inc = parseFloat(cols[1]);
      const az = parseFloat(cols[2]);

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
      return NextResponse.json({ error: "No valid survey data found in CSV" }, { status: 400 });
    }

    await prisma.survey.createMany({
      data: surveys,
    });

    return NextResponse.json({
      imported: surveys.length,
      message: `Successfully imported ${surveys.length} survey records`,
    });
  } catch {
    return NextResponse.json({ error: "CSV import failed" }, { status: 500 });
  }
}
