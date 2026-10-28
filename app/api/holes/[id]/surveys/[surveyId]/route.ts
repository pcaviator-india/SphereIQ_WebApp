import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const surveySchema = z.object({
  measuredDepth: z.number().positive(),
  inclination: z.number().min(-90).max(90),
  azimuth: z.number().min(0).max(360),
});

export async function PUT(
  req: NextRequest,
  { params }: { params: { id: string; surveyId: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const data = surveySchema.parse(body);

    const survey = await prisma.survey.findUnique({
      where: { id: params.surveyId },
      include: { hole: { select: { userId: true } } },
    });

    if (!survey || survey.hole.userId !== session.user.id) {
      return NextResponse.json({ error: "Not found" }, { status: 404 });
    }

    const updated = await prisma.survey.update({
      where: { id: params.surveyId },
      data,
    });

    return NextResponse.json({ survey: updated });
  } catch {
    return NextResponse.json({ error: "Invalid survey data" }, { status: 400 });
  }
}

export async function DELETE(
  _req: NextRequest,
  { params }: { params: { id: string; surveyId: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const survey = await prisma.survey.findUnique({
    where: { id: params.surveyId },
    include: { hole: { select: { userId: true } } },
  });

  if (!survey || survey.hole.userId !== session.user.id) {
    return NextResponse.json({ error: "Not found" }, { status: 404 });
  }

  await prisma.survey.delete({ where: { id: params.surveyId } });

  return NextResponse.json({ success: true });
}
