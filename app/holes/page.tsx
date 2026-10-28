import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function HolesPage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  // Boreholes are shared with everyone across the team
  const holes = await prisma.hole.findMany({
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" }, take: 1 },
      fileUploads: true,
      user: {
        select: {
          id: true,
          name: true,
          email: true,
        },
      },
    },
    orderBy: { updatedAt: "desc" },
  });

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">Boreholes</h1>
            <p className="text-gray-600 mt-1">
              Shared team borehole repository — view, plan, and analyze all active boreholes.
            </p>
          </div>
          <div className="flex gap-2">
            <Link
              href="/import?tab=ezn"
              className="px-4 py-2.5 border border-gray-300 text-gray-700 bg-white rounded-lg font-medium hover:bg-gray-50 transition flex items-center gap-2 shadow-xs text-sm"
            >
              <span>📁</span>
              <span>Import .ezn</span>
            </Link>
            <Link
              href="/holes/create"
              className="px-5 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 transition flex items-center gap-2 shadow-sm text-sm"
            >
              <span className="font-semibold">+ New Borehole</span>
            </Link>
          </div>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {holes.length === 0 ? (
            <div className="col-span-3 flex flex-col items-center justify-center py-16 text-gray-400 bg-white rounded-xl border border-gray-200">
              <span className="text-4xl mb-2">🎯</span>
              <p className="text-base text-gray-600 font-medium">No boreholes added yet.</p>
              <Link
                href="/holes/create"
                className="mt-4 px-4 py-2 bg-blue-600 text-white text-xs font-semibold rounded-lg hover:bg-blue-700"
              >
                Create your first borehole
              </Link>
            </div>
          ) : (
            holes.map((hole) => {
              const creatorName =
                hole.user?.name || hole.user?.email?.split("@")[0] || "Unknown User";
              const isCurrentUser = hole.user?.id === session.user.id;

              return (
                <div
                  key={hole.id}
                  className="bg-white rounded-xl shadow-xs border border-gray-200 p-6 hover:shadow-md transition flex flex-col justify-between"
                >
                  <div>
                    <div className="flex items-start justify-between mb-3">
                      <div className="flex items-center gap-2">
                        <span
                          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-semibold uppercase tracking-wider ${
                            hole.status === "planned"
                              ? "bg-amber-100 text-amber-700"
                              : hole.status === "drilling"
                              ? "bg-blue-100 text-blue-700"
                              : hole.status === "completed"
                              ? "bg-green-100 text-green-700"
                              : "bg-gray-100 text-gray-700"
                          }`}
                        >
                          {hole.status}
                        </span>
                        <span className="text-xs text-gray-400 font-mono">Rig: {hole.rigId}</span>
                      </div>

                      <Link
                        href={`/holes/${hole.id}`}
                        className="text-gray-400 hover:text-blue-600 text-sm font-bold"
                        title="Open Hole Details"
                      >
                        ↗
                      </Link>
                    </div>

                    <h2 className="text-xl font-bold text-gray-900 mb-1">
                      {hole.name}
                    </h2>
                    <p className="text-xs text-gray-500 font-mono mb-3">ID: {hole.holeId}</p>

                    <div className="space-y-1.5 text-xs text-gray-600 mb-4 bg-gray-50/70 p-3 rounded-lg border border-gray-100">
                      <p className="flex justify-between">
                        <span className="text-gray-500">Location:</span>
                        <span className="font-medium text-gray-800">{hole.location || "—"}</span>
                      </p>
                      <p className="flex justify-between">
                        <span className="text-gray-500">Area:</span>
                        <span className="font-medium text-gray-800">{hole.area || "—"}</span>
                      </p>
                      <p className="flex justify-between">
                        <span className="text-gray-500">Collar:</span>
                        <span className="font-medium text-gray-800">
                          {hole.collar
                            ? `N: ${Number(hole.collar.northing).toFixed(1)}, E: ${Number(hole.collar.easting).toFixed(1)}`
                            : "Not Set"}
                        </span>
                      </p>
                    </div>

                    {/* Creator Display matching user requirement */}
                    <div className="pt-2 border-t border-gray-100 flex items-center justify-between text-xs text-gray-500 mb-4">
                      <span className="text-gray-400">Created by:</span>
                      <span className="font-medium text-gray-800 flex items-center gap-1">
                        <span className="w-5 h-5 rounded-full bg-blue-100 text-blue-700 font-bold text-[10px] flex items-center justify-center">
                          {creatorName.charAt(0).toUpperCase()}
                        </span>
                        <span>{creatorName}</span>
                        {isCurrentUser && (
                          <span className="text-[10px] text-blue-600 bg-blue-50 px-1.5 py-0.2 rounded font-semibold">
                            You
                          </span>
                        )}
                      </span>
                    </div>
                  </div>

                  <div className="flex gap-2 pt-2 border-t border-gray-100">
                    <Link
                      href={`/holes/${hole.id}`}
                      className="flex-1 text-center py-2 bg-slate-900 text-white rounded-lg text-xs font-semibold hover:bg-slate-800 transition"
                    >
                      Hole Management
                    </Link>
                    <Link
                      href={`/holes/${hole.id}/navigation`}
                      className="flex-1 text-center py-2 bg-emerald-600 text-white rounded-lg text-xs font-semibold hover:bg-emerald-700 transition"
                    >
                      🎯 Navigation
                    </Link>
                  </div>
                </div>
              );
            })
          )}
        </div>
      </div>
    </div>
  );
}
