import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { getServerAuthSession } from "@/lib/auth";
import SidebarNav from "./components/SidebarNav";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "SphereIQ — Borehole Planning",
  description: "Cross-platform directional drilling planning web application",
};

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const session = await getServerAuthSession();

  return (
    <html lang="en" suppressHydrationWarning>
      <body className={inter.className} suppressHydrationWarning>
        <div className="flex min-h-screen">
          {session && (
            <SidebarNav
              userEmail={session.user?.email ?? ""}
              userName={session.user?.name ?? ""}
            />
          )}
          <main
            className={`flex-1 ${
              session ? "ml-56" : ""
            } bg-gray-50 min-h-screen`}
          >
            {children}
          </main>
        </div>
      </body>
    </html>
  );
}
