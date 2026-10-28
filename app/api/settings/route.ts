import { NextRequest, NextResponse } from "next/server";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { z } from "zod";

const settingsSchema = z.object({
  theme: z.enum(["light", "dark"]).optional(),
  units: z.enum(["metric", "imperial"]).optional(),
});

export async function GET() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    const [user, settings] = await Promise.all([
      prisma.user.findUnique({ where: { id: session.user.id } }),
      prisma.userSettings.findUnique({ where: { userId: session.user.id } }),
    ]);

    const effectiveSettings =
      settings ||
      (await prisma.userSettings.create({
        data: { userId: session.user.id, theme: "light", units: "metric" },
      }));

    return NextResponse.json({ user, settings: effectiveSettings });
  } catch {
    return NextResponse.json({ error: "Failed to load settings" }, { status: 500 });
  }
}

export async function PATCH(req: NextRequest) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }
  try {
    const body = await req.json();
    const data = settingsSchema.parse(body);
    const settings = await prisma.userSettings.upsert({
      where: { userId: session.user.id },
      update: data,
      create: {
        userId: session.user.id,
        theme: data.theme ?? "light",
        units: data.units ?? "metric",
      },
    });
    return NextResponse.json({ settings });
  } catch {
    return NextResponse.json({ error: "Invalid settings data" }, { status: 400 });
  }
}

export async function DELETE() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  }

  try {
    await prisma.user.delete({ where: { id: session.user.id } });
    return NextResponse.json({ message: "Account deleted" });
  } catch {
    return NextResponse.json({ error: "Failed to delete account" }, { status: 500 });
  }
}
