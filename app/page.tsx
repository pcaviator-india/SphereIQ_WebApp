import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function HomePage() {
  const session = await getServerAuthSession();

  if (!session?.user?.id) {
    return (
      <main className="min-h-screen flex flex-col items-center justify-center p-6 bg-gradient-to-b from-gray-50 to-gray-100">
        <div className="text-center max-w-2xl bg-white p-10 rounded-2xl shadow-sm border border-gray-200">
          <div className="w-16 h-16 bg-blue-600 rounded-2xl flex items-center justify-center mx-auto mb-6 shadow-md text-white font-bold text-2xl">
            SQ
          </div>
          <h1 className="text-4xl font-extrabold text-gray-900 tracking-tight mb-3">
            SphereIQ Web
          </h1>
          <p className="text-lg text-gray-600 mb-8 leading-relaxed">
            Professional borehole survey calculation, 3D trajectory visualization, and directional drilling planning platform.
          </p>
          <div className="flex flex-col sm:flex-row gap-4 justify-center">
            <Link
              href="/auth/signin"
              className="px-6 py-3 bg-blue-600 text-white rounded-xl font-medium hover:bg-blue-700 shadow-sm transition"
            >
              Sign In
            </Link>
            <Link
              href="/auth/register"
              className="px-6 py-3 border border-gray-300 text-gray-700 rounded-xl font-medium hover:bg-gray-50 transition"
            >
              Create Account
            </Link>
          </div>
        </div>
      </main>
    );
  }

  // User is authenticated - show rich dashboard
  const [holes, totalSurveys, totalReports] = await Promise.all([
    prisma.hole.findMany({
      where: { userId: session.user.id },
      include: {
        collar: true,
        target: true,
        surveys: { orderBy: { measuredDepth: "desc" }, take: 1 },
      },
      orderBy: { updatedAt: "desc" },
      take: 6,
    }),
    prisma.survey.count({
      where: { hole: { userId: session.user.id } },
    }),
    prisma.report.count({
      where: { userId: session.user.id },
    }),
  ]);

  const allHoles = await prisma.hole.findMany({
    where: { userId: session.user.id },
    select: { status: true },
  });

  const plannedCount = allHoles.filter((h) => h.status === "planned").length;
  const drillingCount = allHoles.filter((h) => h.status === "drilling").length;
  const completedCount = allHoles.filter((h) => h.status === "completed").length;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-6xl mx-auto">
          {/* Welcome Banner */}
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-900">
                Welcome back, {session.user.name || "Surveyor"}
              </h1>
              <p className="text-gray-600 mt-1">
                SphereIQ Directional Drilling & Borehole Planning Dashboard
              </p>
            </div>
            <div className="flex gap-3">
              <Link
                href="/import"
                className="px-4 py-2.5 border border-gray-300 text-gray-700 rounded-lg text-sm font-medium hover:bg-gray-50 transition"
              >
                Import CSV
              </Link>
              <Link
                href="/holes/create"
                className="px-4 py-2.5 bg-blue-600 text-white rounded-lg text-sm font-medium hover:bg-blue-700 shadow-sm transition"
              >
                + New Borehole
              </Link>
            </div>
          </div>

          {/* Metric Cards */}
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-8">
            <div className="bg-white p-5 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-gray-400 uppercase">Total Boreholes</p>
              <p className="text-3xl font-bold text-gray-900 mt-1">{allHoles.length}</p>
              <p className="text-xs text-gray-500 mt-1">Active database records</p>
            </div>
            <div className="bg-white p-5 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-blue-600 uppercase">Drilling In Progress</p>
              <p className="text-3xl font-bold text-blue-600 mt-1">{drillingCount}</p>
              <p className="text-xs text-gray-500 mt-1">{plannedCount} planned</p>
            </div>
            <div className="bg-white p-5 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-green-600 uppercase">Completed</p>
              <p className="text-3xl font-bold text-green-600 mt-1">{completedCount}</p>
              <p className="text-xs text-gray-500 mt-1">Ready for reporting</p>
            </div>
            <div className="bg-white p-5 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-purple-600 uppercase">Total Survey Stations</p>
              <p className="text-3xl font-bold text-purple-600 mt-1">{totalSurveys}</p>
              <p className="text-xs text-gray-500 mt-1">{totalReports} generated reports</p>
            </div>
          </div>

          {/* Quick Tools */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-lg font-bold text-gray-900 mb-4">Calculation & Analysis Tools</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
              <Link
                href="/build-to-target"
                className="p-4 rounded-xl border border-gray-200 hover:border-blue-500 hover:shadow-sm transition group"
              >
                <div className="text-2xl mb-2">🎯</div>
                <h3 className="font-semibold text-gray-900 group-hover:text-blue-600">Build To Target</h3>
                <p className="text-xs text-gray-500 mt-1">
                  Calculate build rate, kick-off depths, and hold lines to reach target depths.
                </p>
              </Link>

              <Link
                href="/import"
                className="p-4 rounded-xl border border-gray-200 hover:border-blue-500 hover:shadow-sm transition group"
              >
                <div className="text-2xl mb-2">📥</div>
                <h3 className="font-semibold text-gray-900 group-hover:text-blue-600">CSV Survey Import</h3>
                <p className="text-xs text-gray-500 mt-1">
                  Batch import multi-station trajectory surveys with automatic column mapping.
                </p>
              </Link>

              <Link
                href="/reports"
                className="p-4 rounded-xl border border-gray-200 hover:border-blue-500 hover:shadow-sm transition group"
              >
                <div className="text-2xl mb-2">📑</div>
                <h3 className="font-semibold text-gray-900 group-hover:text-blue-600">Reports Engine</h3>
                <p className="text-xs text-gray-500 mt-1">
                  Access status logs, trajectory analysis, and printable PDF documents.
                </p>
              </Link>

              <Link
                href="/settings"
                className="p-4 rounded-xl border border-gray-200 hover:border-blue-500 hover:shadow-sm transition group"
              >
                <div className="text-2xl mb-2">⚙️</div>
                <h3 className="font-semibold text-gray-900 group-hover:text-blue-600">Settings & Units</h3>
                <p className="text-xs text-gray-500 mt-1">
                  Toggle metric/imperial coordinate systems and visual color schemes.
                </p>
              </Link>
            </div>
          </div>

          {/* Recent Boreholes */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <div className="flex items-center justify-between mb-4">
              <h2 className="text-lg font-bold text-gray-900">Recent Boreholes</h2>
              <Link href="/holes" className="text-sm font-medium text-blue-600 hover:underline">
                View all ({allHoles.length}) →
              </Link>
            </div>

            {holes.length === 0 ? (
              <div className="text-center py-12 text-gray-400">
                <p className="text-base">No boreholes added yet.</p>
                <Link
                  href="/holes/create"
                  className="mt-3 inline-block text-sm text-blue-600 font-medium hover:underline"
                >
                  Create your first borehole
                </Link>
              </div>
            ) : (
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {holes.map((h) => (
                  <div
                    key={h.id}
                    className="p-4 rounded-xl border border-gray-200 hover:border-gray-300 transition flex flex-col justify-between"
                  >
                    <div>
                      <div className="flex items-center justify-between mb-2">
                        <span
                          className={`px-2 py-0.5 rounded-full text-xs font-medium ${
                            h.status === "planned"
                              ? "bg-yellow-100 text-yellow-700"
                              : h.status === "drilling"
                              ? "bg-blue-100 text-blue-700"
                              : h.status === "completed"
                              ? "bg-green-100 text-green-700"
                              : "bg-gray-100 text-gray-700"
                          }`}
                        >
                          {h.status}
                        </span>
                        <span className="text-xs text-gray-400">Rig: {h.rigId}</span>
                      </div>
                      <h3 className="font-semibold text-gray-900">{h.name}</h3>
                      <p className="text-xs text-gray-500 mb-3">ID: {h.holeId}</p>

                      <div className="text-xs text-gray-600 space-y-1 mb-4">
                        <p>
                          <span className="text-gray-400">Location:</span> {h.location || "—"}
                        </p>
                        <p>
                          <span className="text-gray-400">Latest Survey:</span>{" "}
                          {h.surveys[0] ? `${h.surveys[0].measuredDepth} m` : "None"}
                        </p>
                      </div>
                    </div>

                    <div className="flex gap-2 pt-2 border-t border-gray-100">
                      <Link
                        href={`/holes/${h.id}`}
                        className="flex-1 text-center px-3 py-1.5 bg-gray-100 text-gray-700 rounded-lg text-xs font-medium hover:bg-gray-200 transition"
                      >
                        Details
                      </Link>
                      <Link
                        href={`/holes/${h.id}/charts`}
                        className="flex-1 text-center px-3 py-1.5 bg-blue-50 text-blue-600 rounded-lg text-xs font-medium hover:bg-blue-100 transition"
                      >
                        Charts
                      </Link>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
