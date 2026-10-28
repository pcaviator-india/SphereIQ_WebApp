"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { signOut } from "next-auth/react";

const navLinks = [
  { href: "/", label: "Dashboard", icon: "⊞" },
  { href: "/holes", label: "Boreholes", icon: "⛏" },
  { href: "/import", label: "Import CSV", icon: "📄" },
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

  const isActive = (href: string) => {
    if (href === "/") return pathname === "/";
    return pathname.startsWith(href);
  };

  return (
    <nav className="fixed top-0 left-0 h-screen w-56 bg-slate-900 flex flex-col z-50">
      {/* Logo */}
      <div className="px-5 py-5 border-b border-slate-700">
        <h1 className="text-white text-lg font-bold tracking-tight">
          SphereIQ
        </h1>
        <p className="text-slate-400 text-xs mt-0.5">Borehole Planning</p>
      </div>

      {/* Links */}
      <div className="flex-1 py-4 overflow-y-auto">
        {navLinks.map((link) => (
          <Link
            key={link.href}
            href={link.href}
            className={`flex items-center gap-3 px-5 py-2.5 text-sm font-medium transition ${
              isActive(link.href)
                ? "bg-slate-700 text-white"
                : "text-slate-300 hover:bg-slate-800 hover:text-white"
            }`}
          >
            <span className="text-base">{link.icon}</span>
            {link.label}
          </Link>
        ))}
      </div>

      {/* User / Sign Out */}
      <div className="border-t border-slate-700 px-5 py-4">
        <div className="mb-3">
          <p className="text-white text-sm font-medium truncate">
            {userName || "User"}
          </p>
          <p className="text-slate-400 text-xs truncate">{userEmail}</p>
        </div>
        <button
          onClick={() => signOut({ callbackUrl: "/auth/signin" })}
          className="w-full px-3 py-2 text-sm text-slate-300 hover:text-white hover:bg-slate-800 rounded-lg transition text-left"
        >
          Sign Out
        </button>
      </div>
    </nav>
  );
}
