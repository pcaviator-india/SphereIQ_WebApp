import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const holeUpdateSchema = z.object({
  name: z.string().min(1).optional(),
  holeId: z.string().min(1).optional(),
  rigId: z.string().min(1).optional(),
  location: z.string().optional().nullable(),
  area: z.string().optional().nullable(),
  drillingContractor: z.string().optional().nullable(),
  surveyingContractor: z.string().optional().nullable(),
  client: z.string().optional().nullable(),
  status: z.enum(["planned", "drilling", "completed", "archived"]).optional(),
});

export async function GET(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  // Boreholes are shared with everyone across the team
  const hole = await prisma.hole.findUnique({
    where: { id: params.id },
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" } },
      target: true,
      fileUploads: true,
      user: {
        select: {
          id: true,
          name: true,
          email: true,
        },
      },
    },
  });

  if (!hole) {
    return NextResponse.json({ error: "Hole not found" }, { status: 404 });
  }

  return NextResponse.json({ hole });
}

export async function PUT(
  req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const data = holeUpdateSchema.parse(body);

    const hole = await prisma.hole.update({
      where: { id: params.id },
      data,
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

    return NextResponse.json({ hole });
  } catch (e: any) {
    return NextResponse.json(
      { error: e?.message || "Invalid request" },
      { status: 400 }
    );
  }
}

export async function DELETE(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    await prisma.hole.delete({
      where: { id: params.id },
    });

    return NextResponse.json({ success: true });
  } catch {
    return NextResponse.json({ error: "Delete failed" }, { status: 500 });
  }
}
