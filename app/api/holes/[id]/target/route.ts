import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const targetSchema = z.object({
  tvd: z.number(),
  northing: z.number(),
  easting: z.number(),
});

export async function GET(
  _req: NextRequest,
  { params }: { params: { id: string } }
) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const target = await prisma.target.findFirst({
    where: { holeId: params.id },
  });

  return NextResponse.json({ target });
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
    const data = targetSchema.parse(body);

    const target = await prisma.target.upsert({
      where: { holeId: params.id },
      update: {
        tvd: data.tvd,
        northing: data.northing,
        easting: data.easting,
      },
      create: {
        holeId: params.id,
        tvd: data.tvd,
        northing: data.northing,
        easting: data.easting,
      },
    });

    return NextResponse.json({ target }, { status: 201 });
  } catch {
    return NextResponse.json({ error: "Invalid target data" }, { status: 400 });
  }
}