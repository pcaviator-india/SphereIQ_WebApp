import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const holeCreateSchema = z.object({
  holeId: z.string().min(1, "Hole ID is required"),
  rigId: z.string().optional().default("RIG-01"),
  name: z.string().optional().nullable(),
  location: z.string().optional().nullable(),
  area: z.string().optional().nullable(),
  drillingContractor: z.string().optional().nullable(),
  surveyingContractor: z.string().optional().nullable(),
  client: z.string().optional().nullable(),
  status: z.enum(["planned", "drilling", "completed", "archived"]).default("planned"),
});

export async function GET() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  // Boreholes are shared with everyone across the system
  const holes = await prisma.hole.findMany({
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" }, take: 5 },
      target: true,
      user: {
        select: {
          id: true,
          name: true,
          email: true,
        },
      },
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

    const holeName =
      data.name && data.name.trim().length > 0
        ? data.name.trim()
        : data.holeId.trim();

    const rigId =
      data.rigId && data.rigId.trim().length > 0
        ? data.rigId.trim()
        : "RIG-01";

    const hole = await prisma.hole.create({
      data: {
        holeId: data.holeId.trim(),
        rigId: rigId,
        name: holeName,
        location: data.location?.trim() || null,
        area: data.area?.trim() || null,
        drillingContractor: data.drillingContractor?.trim() || null,
        surveyingContractor: data.surveyingContractor?.trim() || null,
        client: data.client?.trim() || null,
        status: data.status,
        userId: session.user.id,
      },
      include: {
        user: {
          select: {
            id: true,
            name: true,
            email: true,
          },
        },
      },
    });

    return NextResponse.json({ hole }, { status: 201 });
  } catch (e: any) {
    console.error("Hole creation error:", e);
    const errorMessage =
      e?.errors?.[0]?.message || e?.message || "Invalid request parameters";
    return NextResponse.json({ error: errorMessage }, { status: 400 });
  }
}
