import type { Metadata, Viewport } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { getServerAuthSession } from "@/lib/auth";
import SidebarNav from "./components/SidebarNav";
import PwaRegister from "./components/PwaRegister";

const inter = Inter({ subsets: ["latin"] });

export const viewport: Viewport = {
  themeColor: "#0f172a",
  width: "device-width",
  initialScale: 1,
  maximumScale: 1,
  userScalable: false,
  viewportFit: "cover",
};

export const metadata: Metadata = {
  title: "SphereIQ — Borehole Planning & Directional Drilling",
  description: "Cross-platform directional drilling planning web application with 3D WebGL trajectory visualization",
  manifest: "/manifest.webmanifest",
  appleWebApp: {
    capable: true,
    statusBarStyle: "black-translucent",
    title: "SphereIQ",
  },
  icons: {
    icon: [
      { url: "/icons/icon-192.png", sizes: "192x192", type: "image/png" },
      { url: "/icons/icon-512.png", sizes: "512x512", type: "image/png" },
    ],
    apple: [{ url: "/icons/apple-touch-icon.png", sizes: "180x180", type: "image/png" }],
  },
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
        <PwaRegister />
        <div className="flex flex-col md:flex-row min-h-screen">
          {session && (
            <SidebarNav
              userEmail={session.user?.email ?? ""}
              userName={session.user?.name ?? ""}
            />
          )}
          <main
            className={`flex-1 ${
              session ? "md:ml-56 pt-14 md:pt-0" : ""
            } bg-gray-50 min-h-screen`}
          >
            {children}
          </main>
        </div>
      </body>
    </html>
  );
}
