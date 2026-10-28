import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export async function GET() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  const reports = await prisma.report.findMany({
    where: { userId: session.user.id },
    include: { hole: true },
    orderBy: { createdAt: "desc" },
  });

  return NextResponse.json({ reports });
}

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const body = await req.json();
    const { holeId, title, type, pdfUrl } = body;

    if (!holeId || !title || !type) {
      return NextResponse.json(
        { error: "holeId, title, and type are required" },
        { status: 400 }
      );
    }

    const report = await prisma.report.create({
      data: {
        holeId,
        title,
        type,
        pdfUrl: pdfUrl || null,
        userId: session.user.id,
      },
      include: { hole: true },
    });

    return NextResponse.json({ report }, { status: 201 });
  } catch {
    return NextResponse.json({ error: "Failed to create report" }, { status: 500 });
  }
}
