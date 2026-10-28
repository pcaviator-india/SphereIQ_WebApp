import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";
import { calculateTrajectory, Collar, SurveyStation } from "@/lib/survey-math";

const surveySchema = z.object({
  measuredDepth: z.coerce.number().min(0, "Measured depth must be greater than or equal to 0"),
  inclination: z.coerce.number().min(-180).max(360, "Inclination must be between -180 and 360"),
  azimuth: z.coerce.number().min(0).max(360, "Azimuth must be between 0 and 360"),
});

async function recalculateHole(holeId: string) {
  try {
    const hole = await prisma.hole.findUnique({
      where: { id: holeId },
      include: { collar: true, surveys: { orderBy: { measuredDepth: "asc" } } },
    });

    if (!hole || !hole.collar || hole.surveys.length === 0) return;

    const collar: Collar = {
      northing: Number(hole.collar.northing),
      easting: Number(hole.collar.easting),
      elevation: Number(hole.collar.elevation),
    };

    const stations: SurveyStation[] = hole.surveys.map((s) => ({
      measuredDepth: Number(s.measuredDepth),
      inclination: Number(s.inclination),
      azimuth: Number(s.azimuth),
    }));

    const trajectory = calculateTrajectory(collar, stations);

    await prisma.$transaction(
      trajectory.map((calc, idx) =>
        prisma.survey.update({
          where: { id: hole.surveys[idx].id },
          data: {
            trueVerticalDepth: calc.trueVerticalDepth,
            northing: calc.northing,
            easting: calc.easting,
            verticalSection: calc.verticalSection,
          },
        })
      )
    );
  } catch (err) {
    console.error("Auto recalculate trajectory error:", err);
  }
}

export async function PUT(
  req: NextRequest,
  { params }: { params: { id: string; surveyId: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const resolvedParams = await Promise.resolve(params);
  let surveyId = resolvedParams?.surveyId;
  let holeId = resolvedParams?.id;

  if (!surveyId || !holeId) {
    const segments = req.nextUrl.pathname.split("/").filter(Boolean);
    const sIdx = segments.indexOf("surveys");
    if (sIdx !== -1 && segments[sIdx + 1]) surveyId = segments[sIdx + 1];
    const hIdx = segments.indexOf("holes");
    if (hIdx !== -1 && segments[hIdx + 1]) holeId = segments[hIdx + 1];
  }

  if (!surveyId) {
    return NextResponse.json({ error: "Missing survey ID" }, { status: 400 });
  }

  try {
    const body = await req.json();
    const data = surveySchema.parse(body);

    const survey = await prisma.survey.findUnique({
      where: { id: surveyId },
      include: { hole: { select: { id: true, userId: true } } },
    });

    if (!survey || survey.hole.userId !== session.user.id) {
      return NextResponse.json({ error: "Survey not found" }, { status: 404 });
    }

    const updated = await prisma.survey.update({
      where: { id: surveyId },
      data: {
        measuredDepth: data.measuredDepth,
        inclination: data.inclination,
        azimuth: data.azimuth,
      },
    });

    // Auto-recalculate trajectory for the borehole
    await recalculateHole(survey.hole.id);

    return NextResponse.json({ survey: updated });
  } catch (err: any) {
    console.error("PUT survey error:", err);
    if (err instanceof z.ZodError) {
      const msg = err.errors.map((e) => e.message).join(", ");
      return NextResponse.json({ error: msg, details: err.errors }, { status: 400 });
    }
    return NextResponse.json({ error: err?.message || "Invalid survey data" }, { status: 400 });
  }
}

export async function DELETE(
  req: NextRequest,
  { params }: { params: { id: string; surveyId: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const resolvedParams = await Promise.resolve(params);
  let surveyId = resolvedParams?.surveyId;

  if (!surveyId) {
    const segments = req.nextUrl.pathname.split("/").filter(Boolean);
    const sIdx = segments.indexOf("surveys");
    if (sIdx !== -1 && segments[sIdx + 1]) surveyId = segments[sIdx + 1];
  }

  if (!surveyId) {
    return NextResponse.json({ error: "Missing survey ID" }, { status: 400 });
  }

  try {
    const survey = await prisma.survey.findUnique({
      where: { id: surveyId },
      include: { hole: { select: { id: true, userId: true } } },
    });

    if (!survey || survey.hole.userId !== session.user.id) {
      return NextResponse.json({ error: "Survey not found" }, { status: 404 });
    }

    const targetHoleId = survey.hole.id;
    await prisma.survey.delete({ where: { id: surveyId } });

    // Recalculate remaining surveys
    await recalculateHole(targetHoleId);

    return NextResponse.json({ success: true });
  } catch (err: any) {
    console.error("DELETE survey error:", err);
    return NextResponse.json({ error: err?.message || "Delete failed" }, { status: 500 });
  }
}
