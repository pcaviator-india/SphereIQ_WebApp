import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const holeUpdateSchema = z.object({
  name: z.string().min(1).optional(),
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

  const hole = await prisma.hole.findFirst({
    where: { id: params.id, userId: session.user.id },
    include: { collar: true, surveys: true, target: true, fileUploads: true },
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
      where: { id: params.id, userId: session.user.id },
      data,
    });

    return NextResponse.json({ hole });
  } catch {
    return NextResponse.json({ error: "Invalid request" }, { status: 400 });
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

  await prisma.hole.delete({
    where: { id: params.id, userId: session.user.id },
  });

  return NextResponse.json({ success: true });
}
