import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function ReportsPage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const reports = await prisma.report.findMany({
    where: { userId: session.user.id },
    include: { hole: true },
    orderBy: { createdAt: "desc" },
  });

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-900">Reports</h1>
              <p className="text-gray-600">
                View and manage your borehole reports
              </p>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {reports.length === 0 ? (
              <div className="col-span-3 flex flex-col items-center justify-center py-16 text-gray-400">
                <p>No reports yet.</p>
              </div>
            ) : (
              reports.map((report) => (
                <div
                  key={report.id}
                  className="bg-white rounded-xl shadow-sm border border-gray-200 p-6 hover:shadow-md transition"
                >
                  <div className="flex items-start justify-between mb-4">
                    <div>
                      <span
                        className={`inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-xs font-medium ${
                          report.type === "build"
                            ? "bg-blue-100 text-blue-700"
                            : report.type === "trend"
                            ? "bg-green-100 text-green-700"
                            : report.type === "extrapolate"
                            ? "bg-purple-100 text-purple-700"
                            : "bg-gray-100 text-gray-700"
                        }`}
                      >
                        {report.type}
                      </span>
                    </div>
                    <Link
                      href={`/reports/${report.id}`}
                      className="text-gray-400 hover:text-gray-600"
                    >
                      ↗
                    </Link>
                  </div>

                  <h2 className="text-xl font-semibold text-gray-900 mb-1">
                    {report.title}
                  </h2>
                  <p className="text-sm text-gray-500 mb-4">
                    Hole: {report.hole?.name} ({report.hole?.holeId})
                  </p>

                  <div className="space-y-2 text-sm text-gray-600 mb-6">
                    <p>
                      <span className="font-medium text-gray-700">Type:</span>{" "}
                      {report.type}
                    </p>
                    <p>
                      <span className="font-medium text-gray-700">PDF:</span>{" "}
                      {report.pdfUrl ? "Available" : "Not generated"}
                    </p>
                  </div>

                  <div className="flex gap-2">
                    <Link
                      href={`/reports/${report.id}`}
                      className="flex-1 px-4 py-2 bg-blue-600 text-white text-sm rounded-lg font-medium hover:bg-blue-700 transition text-center"
                    >
                      View
                    </Link>
                    {report.pdfUrl && (
                      <a
                        href={report.pdfUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                        className="flex-1 px-4 py-2 border border-gray-300 text-gray-700 text-sm rounded-lg font-medium hover:bg-gray-50 transition text-center"
                      >
                        Download PDF
                      </a>
                    )}
                  </div>
                </div>
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
