import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function HoleDetailPage({
  params,
}: {
  params: { id: string };
}) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const hole = await prisma.hole.findFirst({
    where: { id: params.id, userId: session.user.id },
    include: {
      collar: true,
      surveys: { orderBy: { measuredDepth: "asc" } },
      target: true,
      fileUploads: true,
    },
  });

  if (!hole) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg shadow-sm border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">Hole not found</h1>
          <Link
            href="/holes"
            className="text-blue-600 hover:underline font-medium"
          >
            Back to holes
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-6xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-900">{hole.name}</h1>
              <p className="text-gray-600">Hole ID: {hole.holeId}</p>
            </div>
            <div className="flex gap-2">
              <Link
                href="/holes"
                className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition"
              >
                Back
              </Link>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h3 className="text-xs font-semibold text-gray-400 uppercase tracking-wider mb-3">
                Status
              </h3>
              <span
                className={`inline-flex items-center gap-1 px-3 py-1 rounded-full text-sm font-medium ${
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
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h3 className="text-xs font-semibold text-gray-400 uppercase tracking-wider mb-3">
                Surveys
              </h3>
              <p className="text-2xl font-bold text-gray-900">{hole.surveys.length}</p>
              <p className="text-xs text-gray-500 mt-1">Total records</p>
            </div>
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h3 className="text-xs font-semibold text-gray-400 uppercase tracking-wider mb-3">
                Files
              </h3>
              <p className="text-2xl font-bold text-gray-900">{hole.fileUploads.length}</p>
              <p className="text-xs text-gray-500 mt-1">Uploaded documents</p>
            </div>
          </div>

          {/* Collar Section */}
          {hole.collar && (
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Collar Location</h2>
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div>
                  <p className="text-xs font-semibold text-gray-400 uppercase">Northing</p>
                  <p className="text-lg text-gray-900">{hole.collar.northing} m</p>
                </div>
                <div>
                  <p className="text-xs font-semibold text-gray-400 uppercase">Easting</p>
                  <p className="text-lg text-gray-900">{hole.collar.easting} m</p>
                </div>
                <div>
                  <p className="text-xs font-semibold text-gray-400 uppercase">Elevation</p>
                  <p className="text-lg text-gray-900">{hole.collar.elevation} m</p>
                </div>
              </div>
            </div>
          )}

          {/* Surveys Table */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Survey Data</h2>
            {hole.surveys.length === 0 ? (
              <p className="text-gray-500">No survey data yet.</p>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full text-left border-collapse">
                  <thead>
                    <tr className="border-b border-gray-200">
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Measured Depth
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Inclination
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Azimuth
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        TVD
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Northing
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Easting
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {hole.surveys.map((s) => (
                      <tr key={s.id} className="border-b border-gray-100 hover:bg-gray-50 transition">
                        <td className="py-3 text-gray-900">{s.measuredDepth} m</td>
                        <td className="py-3 text-gray-900">{s.inclination}°</td>
                        <td className="py-3 text-gray-900">{s.azimuth}°</td>
                        <td className="py-3 text-gray-600">
                          {s.trueVerticalDepth ? s.trueVerticalDepth + " m" : "—"}
                        </td>
                        <td className="py-3 text-gray-600">
                          {s.northing ? s.northing + " m" : "—"}
                        </td>
                        <td className="py-3 text-gray-600">
                          {s.easting ? s.easting + " m" : "—"}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>

          {/* File Uploads */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Files</h2>
            <div className="flex gap-4">
              <form
                action="/api/upload"
                method="POST"
                encType="multipart/form-data"
                className="flex-1"
              >
                <input type="hidden" name="holeId" value={hole.id} />
                <div className="flex gap-2">
                  <input
                    type="file"
                    name="file"
                    className="flex-1 border border-gray-300 rounded-md p-3 focus:outline-none"
                  />
                  <button
                    type="submit"
                    className="px-4 py-2 bg-blue-600 text-white rounded-md font-medium hover:bg-blue-700 transition"
                  >
                    Upload
                  </button>
                </div>
              </form>
            </div>
            {hole.fileUploads.length > 0 && (
              <div className="mt-4 space-y-2">
                {hole.fileUploads.map((f) => (
                  <div key={f.id} className="flex items-center justify-between py-2 border-b border-gray-100 text-sm">
                    <span className="text-gray-700">{f.fileName}</span>
                    <span className="text-gray-500">{f.size} bytes</span>
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