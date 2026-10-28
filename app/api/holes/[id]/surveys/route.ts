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

export async function GET(
  req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const resolvedParams = await Promise.resolve(params);
  let holeId = resolvedParams?.id;
  if (!holeId) {
    const segments = req.nextUrl.pathname.split("/").filter(Boolean);
    const hIdx = segments.indexOf("holes");
    if (hIdx !== -1 && segments[hIdx + 1]) holeId = segments[hIdx + 1];
  }

  const surveys = await prisma.survey.findMany({
    where: { holeId },
    orderBy: { measuredDepth: "asc" },
  });

  return NextResponse.json({ surveys });
}

export async function POST(
  req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const resolvedParams = await Promise.resolve(params);
  let holeId = resolvedParams?.id;
  if (!holeId) {
    const segments = req.nextUrl.pathname.split("/").filter(Boolean);
    const hIdx = segments.indexOf("holes");
    if (hIdx !== -1 && segments[hIdx + 1]) holeId = segments[hIdx + 1];
  }

  if (!holeId) {
    return NextResponse.json({ error: "Missing hole ID" }, { status: 400 });
  }

  try {
    const body = await req.json();
    const data = surveySchema.parse(body);

    const hole = await prisma.hole.findFirst({
      where: { id: holeId, userId: session.user.id },
    });

    if (!hole) {
      return NextResponse.json({ error: "Hole not found" }, { status: 404 });
    }

    const survey = await prisma.survey.create({
      data: {
        holeId,
        measuredDepth: data.measuredDepth,
        inclination: data.inclination,
        azimuth: data.azimuth,
        trueVerticalDepth: null,
        northing: null,
        easting: null,
        verticalSection: null,
      },
    });

    // Auto-recalculate trajectory
    await recalculateHole(holeId);

    return NextResponse.json({ survey }, { status: 201 });
  } catch (err: any) {
    console.error("POST survey error:", err);
    if (err instanceof z.ZodError) {
      const msg = err.errors.map((e) => e.message).join(", ");
      return NextResponse.json({ error: msg, details: err.errors }, { status: 400 });
    }
    return NextResponse.json({ error: err?.message || "Invalid survey data" }, { status: 400 });
  }
}
