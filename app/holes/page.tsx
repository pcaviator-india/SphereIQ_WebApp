import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function HolesPage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null; // middleware should redirect, but safety fallback
  }

  const holes = await prisma.hole.findMany({
    where: { userId: session.user.id },
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" }, take: 1 },
      fileUploads: true,
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
              Manage your drilling hole plans and surveys
            </p>
          </div>
          <Link
            href="/holes/create"
            className="px-5 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 transition flex items-center gap-2"
          >
            <span className="text-sm">+ New Hole</span>
          </Link>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {holes.length === 0 ? (
            <div className="col-span-3 flex flex-col items-center justify-center py-16 text-gray-400">
              <p>No holes yet.</p>
              <Link
                href="/holes/create"
                className="mt-4 text-blue-600 hover:underline font-medium"
              >
                Create your first borehole
              </Link>
            </div>
          ) : (
            holes.map((hole) => (
              <div
                key={hole.id}
                className="bg-white rounded-xl shadow-sm border border-gray-200 p-6 hover:shadow-md transition"
              >
                <div className="flex items-start justify-between mb-4">
                  <div>
                    <span
                      className={`inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-xs font-medium ${
                        hole.status === "planned"
                          ? "bg-yellow-100 text-yellow-700"
                          : hole.status === "drilling"
                          ? "bg-blue-100 text-blue-700"
                          : hole.status === "completed"
                          ? "bg-green-100 text-green-700"
                          : "bg-gray-100 text-gray-700"
                      }`}
                    >
                      {hole.status}
                    </span>
                  </div>
                  <Link
                    href={`/holes/${hole.id}`}
                    className="text-gray-400 hover:text-gray-600"
                  >
                    ↗
                  </Link>
                </div>

                <h2 className="text-xl font-semibold text-gray-900 mb-1">
                  {hole.name}
                </h2>
                <p className="text-sm text-gray-500 mb-4">Hole ID: {hole.holeId}</p>

                <div className="space-y-2 text-sm text-gray-600 mb-6">
                  <p>
                    <span className="font-medium text-gray-700">Surveys:</span>{" "}
                    {hole.surveys.length}
                  </p>
                  {hole.collar && (
                    <p>
                      <span className="font-medium text-gray-700">Collar:</span>{" "}
                      ({hole.collar.northing}, {hole.collar.easting})
                    </p>
                  )}
                  <p>
                    <span className="font-medium text-gray-700">Files:</span>{" "}
                    {hole.fileUploads.length}
                  </p>
                </div>

                <div className="flex gap-2">
                  <Link
                    href={`/holes/${hole.id}`}
                    className="flex-1 px-4 py-2 bg-blue-600 text-white text-sm rounded-lg font-medium hover:bg-blue-700 transition text-center"
                  >
                    View Details
                  </Link>
                </div>
              </div>
            ))
          )}
        </div>
      </div>
    </div>
  );
}
