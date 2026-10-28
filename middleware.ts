import { withAuth } from "next-auth/middleware";
import { NextResponse } from "next/server";

export default withAuth({
  callbacks: {
    authorized: ({ token, req }) => {
      const { pathname } = req.nextUrl;

      // Allow public auth and API routes
      if (pathname.startsWith("/auth") || pathname.startsWith("/api")) {
        return true;
      }

      // Allow homepage
      if (pathname === "/") {
        return true;
      }

      // Protect all other routes
      return !!token;
    },
  },
});

export const config = {
  matcher: ["/((?!_next/static|_next/image|favicon.ico|.*\\..*).*)"],
};
