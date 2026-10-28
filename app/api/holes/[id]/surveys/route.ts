import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const surveySchema = z.object({
  measuredDepth: z.number().positive(),
  inclination: z.number().min(-90).max(90),
  azimuth: z.number().min(0).max(360),
});

export async function GET(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const surveys = await prisma.survey.findMany({
    where: { holeId: params.id },
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

  try {
    const body = await req.json();
    const data = surveySchema.parse(body);

    const survey = await prisma.survey.create({
      data: {
        holeId: params.id,
        measuredDepth: data.measuredDepth,
        inclination: data.inclination,
        azimuth: data.azimuth,
        trueVerticalDepth: null,
        northing: null,
        easting: null,
        verticalSection: null,
      },
    });

    return NextResponse.json({ survey }, { status: 201 });
  } catch {
    return NextResponse.json({ error: "Invalid survey data" }, { status: 400 });
  }
}
