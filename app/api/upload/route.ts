import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export async function POST(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const formData = await req.formData();
    const file = formData.get("file") as File;
    const holeId = formData.get("holeId") as string;

    if (!file) {
      return NextResponse.json({ error: "No file provided" }, { status: 400 });
    }

    // For Vercel deployment without Blob configured, store file metadata
    // In production, upload to Vercel Blob or S3 and store the URL
    const arrayBuffer = await file.arrayBuffer();
    const buffer = Buffer.from(arrayBuffer);
    const base64 = buffer.toString("base64");
    const dataUrl = `data:${(file as File).type};base64,${base64}`;
    const mimeType = (file as File).type || "application/octet-stream";
    const size = (file as File).size;

    const fileUpload = await prisma.fileUpload.create({
      data: {
        fileName: file.name,
        fileUrl: dataUrl,
        mimeType: mimeType,
        size,
        holeId: holeId || null,
        userId: session.user.id,
      },
    });

    return NextResponse.json({ fileUpload }, { status: 201 });
  } catch {
    return NextResponse.json({ error: "Upload failed" }, { status: 500 });
  }
}
