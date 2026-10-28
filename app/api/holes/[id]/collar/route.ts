import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const collarSchema = z.object({
  northing: z.number(),
  easting: z.number(),
  elevation: z.number(),
  totalDepth: z.number().optional(),
});

export async function GET(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const collar = await prisma.collar.findFirst({
    where: { holeId: params.id },
  });

  return NextResponse.json({ collar });
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
    const data = collarSchema.parse(body);

    const collar = await prisma.collar.upsert({
      where: { holeId: params.id },
      update: {
        northing: data.northing,
        easting: data.easting,
        elevation: data.elevation,
        totalDepth: data.totalDepth,
      },
      create: {
        holeId: params.id,
        northing: data.northing,
        easting: data.easting,
        elevation: data.elevation,
        totalDepth: data.totalDepth,
      },
    });

    return NextResponse.json({ collar }, { status: 201 });
  } catch {
    return NextResponse.json({ error: "Invalid collar data" }, { status: 400 });
  }
}