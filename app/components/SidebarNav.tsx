"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { signOut } from "next-auth/react";

const navLinks = [
  { href: "/", label: "Dashboard", icon: "⊞" },
  { href: "/holes", label: "Boreholes", icon: "⛏" },
  { href: "/import", label: "Import .ezn / CSV", icon: "📥" },
  { href: "/reports", label: "Reports", icon: "📊" },
  { href: "/settings", label: "Settings", icon: "⚙" },
];

export default function SidebarNav({
  userEmail,
  userName,
}: {
  userEmail: string;
  userName: string;
}) {
  const pathname = usePathname();
  const [mobileOpen, setMobileOpen] = useState(false);

  // Close mobile drawer on route change
  useEffect(() => {
    setMobileOpen(false);
  }, [pathname]);

  const isActive = (href: string) => {
    if (href === "/") return pathname === "/";
    return pathname.startsWith(href);
  };

  return (
    <>
      {/* ================= Mobile Sticky Topbar (Screen < md) ================= */}
      <header className="md:hidden fixed top-0 left-0 right-0 h-14 bg-slate-900 border-b border-slate-800 flex items-center justify-between px-4 z-40 shadow-md">
        <Link href="/" className="flex items-center gap-2.5">
          <div className="w-8 h-8 rounded-lg bg-blue-600 flex items-center justify-center text-white text-base font-bold shadow-xs">
            🌐
          </div>
          <div>
            <h1 className="text-white text-base font-bold tracking-tight leading-tight">SphereIQ</h1>
            <p className="text-slate-400 text-[10px] leading-none">Borehole Planning</p>
          </div>
        </Link>

        <button
          onClick={() => setMobileOpen(!mobileOpen)}
          className="p-2 rounded-lg text-slate-300 hover:text-white hover:bg-slate-800 transition"
          aria-label="Toggle navigation menu"
        >
          {mobileOpen ? (
            <svg className="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
            </svg>
          ) : (
            <svg className="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
            </svg>
          )}
        </button>
      </header>

      {/* ================= Mobile Slide-Out Drawer Overlay ================= */}
      {mobileOpen && (
        <div
          onClick={() => setMobileOpen(false)}
          className="md:hidden fixed inset-0 bg-black/60 backdrop-blur-xs z-40 transition-opacity"
        />
      )}

      {/* ================= Mobile Drawer Menu ================= */}
      <div
        className={`md:hidden fixed top-14 left-0 bottom-0 w-64 bg-slate-900 border-r border-slate-800 flex flex-col z-40 transform transition-transform duration-200 ease-in-out ${
          mobileOpen ? "translate-x-0" : "-translate-x-full"
        }`}
      >
        <div className="flex-1 py-4 overflow-y-auto">
          {navLinks.map((link) => (
            <Link
              key={link.href}
              href={link.href}
              className={`flex items-center gap-3 px-5 py-3 text-sm font-medium transition ${
                isActive(link.href)
                  ? "bg-slate-800 text-blue-400 border-l-4 border-blue-500"
                  : "text-slate-300 hover:bg-slate-800 hover:text-white"
              }`}
            >
              <span className="text-lg">{link.icon}</span>
              {link.label}
            </Link>
          ))}
        </div>

        {/* User profile & logout */}
        <div className="border-t border-slate-800 p-4 bg-slate-950/40">
          <div className="mb-3">
            <p className="text-white text-sm font-semibold truncate">{userName || "User"}</p>
            <p className="text-slate-400 text-xs truncate">{userEmail}</p>
          </div>
          <button
            onClick={() => signOut({ callbackUrl: "/auth/signin" })}
            className="w-full py-2.5 px-3 rounded-lg bg-red-600/10 hover:bg-red-600/20 text-red-400 text-xs font-semibold text-center transition border border-red-500/20"
          >
            Sign Out
          </button>
        </div>
      </div>

      {/* ================= Desktop Permanent Sidebar (Screen >= md) ================= */}
      <nav className="hidden md:flex fixed top-0 left-0 h-screen w-56 bg-slate-900 flex-col z-40 border-r border-slate-800">
        {/* Logo */}
        <div className="px-5 py-5 border-b border-slate-800 flex items-center gap-2.5">
          <div className="w-8 h-8 rounded-lg bg-blue-600 flex items-center justify-center text-white text-base font-bold shadow-xs">
            🌐
          </div>
          <div>
            <h1 className="text-white text-lg font-bold tracking-tight leading-tight">SphereIQ</h1>
            <p className="text-slate-400 text-[10px]">Borehole Planning</p>
          </div>
        </div>

        {/* Links */}
        <div className="flex-1 py-4 overflow-y-auto">
          {navLinks.map((link) => (
            <Link
              key={link.href}
              href={link.href}
              className={`flex items-center gap-3 px-5 py-2.5 text-sm font-medium transition ${
                isActive(link.href)
                  ? "bg-slate-800 text-blue-400 border-l-4 border-blue-500"
                  : "text-slate-300 hover:bg-slate-800 hover:text-white"
              }`}
            >
              <span className="text-base">{link.icon}</span>
              {link.label}
            </Link>
          ))}
        </div>

        {/* User / Sign Out */}
        <div className="border-t border-slate-800 px-5 py-4 bg-slate-950/40">
          <div className="mb-3">
            <p className="text-white text-sm font-medium truncate">{userName || "User"}</p>
            <p className="text-slate-400 text-xs truncate">{userEmail}</p>
          </div>
          <button
            onClick={() => signOut({ callbackUrl: "/auth/signin" })}
            className="w-full px-3 py-2 text-xs font-medium text-slate-300 hover:text-white hover:bg-slate-800 rounded-lg transition text-left border border-slate-700/60"
          >
            Sign Out
          </button>
        </div>
      </nav>
    </>
  );
}
