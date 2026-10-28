import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { parseEznFile, EznHoleFileDto } from "@/lib/ezn-format";
import { recalculateHoleTrajectory } from "@/lib/recalculate";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    let fileContent = "";
    const contentType = req.headers.get("content-type") || "";

    if (contentType.includes("multipart/form-data")) {
      const formData = await req.formData();
      const file = formData.get("file") as File | null;
      if (!file) {
        return NextResponse.json({ error: "No file provided" }, { status: 400 });
      }
      fileContent = await file.text();
    } else {
      const body = await req.json();
      fileContent = body.content || body.fileContent || JSON.stringify(body);
    }

    if (!fileContent || fileContent.trim().length === 0) {
      return NextResponse.json({ error: "File content is empty" }, { status: 400 });
    }

    // Parse .ezn file
    const dto: EznHoleFileDto = parseEznFile(fileContent);

    // Create the hole in DB
    const hole = await prisma.hole.create({
      data: {
        holeId: dto.HoleID.trim(),
        rigId: dto.RigID?.trim() || "RIG-01",
        name: dto.HoleID.trim(),
        location: dto.Location?.trim() || null,
        area: dto.Area?.trim() || null,
        drillingContractor: dto.DrillingContractor?.trim() || null,
        surveyingContractor: dto.SurveyingContractor?.trim() || null,
        client: dto.Client?.trim() || null,
        status: "drilling",
        userId: session.user.id,
      },
    });

    // Create Collar if present
    if (dto.Collar) {
      await prisma.collar.create({
        data: {
          holeId: hole.id,
          easting: dto.Collar.Easting,
          northing: dto.Collar.Northing,
          elevation: dto.Collar.Elevation ?? 0,
        },
      });
    }

    // Create Target if present
    if (dto.Target) {
      await prisma.target.create({
        data: {
          holeId: hole.id,
          easting: dto.Target.Easting,
          northing: dto.Target.Northing,
          tvd: dto.Target.TVD,
        },
      });
    }

    // Insert surveys if present
    let surveysCount = 0;
    if (dto.Surveys && dto.Surveys.length > 0) {
      await prisma.survey.createMany({
        data: dto.Surveys.map((s) => ({
          holeId: hole.id,
          measuredDepth: s.MD,
          inclination: s.Dip,
          azimuth: s.Azimuth,
        })),
      });
      surveysCount = dto.Surveys.length;

      // Auto-recalculate minimum curvature trajectory
      await recalculateHoleTrajectory(hole.id);
    }

    return NextResponse.json({
      success: true,
      hole: {
        id: hole.id,
        holeId: hole.holeId,
        name: hole.name,
      },
      surveysCount,
    });
  } catch (err: any) {
    console.error("EZN import error:", err);
    return NextResponse.json(
      { error: err.message || "Failed to parse or import .ezn hole file" },
      { status: 400 }
    );
  }
}
