import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const holeCreateSchema = z.object({
  holeId: z.string().min(1),
  rigId: z.string().min(1),
  name: z.string().min(1),
  location: z.string().optional(),
  area: z.string().optional(),
  drillingContractor: z.string().optional(),
  surveyingContractor: z.string().optional(),
  client: z.string().optional(),
  status: z.enum(["planned", "drilling", "completed", "archived"]).default("planned"),
});

export async function GET() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const holes = await prisma.hole.findMany({
    where: { userId: session.user.id },
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" }, take: 5 },
      target: true,
    },
    orderBy: { updatedAt: "desc" },
  });

  return NextResponse.json({ holes });
}

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const data = holeCreateSchema.parse(body);

    const hole = await prisma.hole.create({
      data: {
        holeId: data.holeId,
        rigId: data.rigId,
        name: data.name,
        location: data.location,
        area: data.area,
        drillingContractor: data.drillingContractor,
        surveyingContractor: data.surveyingContractor,
        client: data.client,
        status: data.status,
        userId: session.user.id,
      },
    });

    return NextResponse.json({ hole }, { status: 201 });
  } catch (e) {
    return NextResponse.json({ error: "Invalid request" }, { status: 400 });
  }
}
