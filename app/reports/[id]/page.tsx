import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function ReportDetailPage({
  params,
}: {
  params: { id: string };
}) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const report = await prisma.report.findFirst({
    where: { id: params.id, userId: session.user.id },
    include: { hole: true },
  });

  if (!report) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg shadow-sm border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">Report not found</h1>
          <Link href="/reports" className="text-blue-600 hover:underline">
            Back to reports
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-3xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link href="/reports" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
                ← Back to reports
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">{report.title}</h1>
              <p className="text-gray-600">
                Hole: {report.hole?.name} ({report.hole?.holeId})
              </p>
            </div>
            <div className="flex gap-2">
              {report.pdfUrl && (
                <a
                  href={report.pdfUrl}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="px-5 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 transition flex items-center gap-2"
                >
                  <span>Download PDF</span>
                </a>
              )}
              <Link
                href="/reports"
                className="px-5 py-2.5 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition"
              >
                Back
              </Link>
            </div>
          </div>

          <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
              <div className="bg-gray-50 p-4 rounded-lg border border-gray-200">
                <h3 className="text-xs font-semibold text-gray-400 uppercase mb-2">
                  Report Type
                </h3>
                <p className="text-gray-900 font-medium">{report.type}</p>
              </div>
              <div className="bg-gray-50 p-4 rounded-lg border border-gray-200">
                <h3 className="text-xs font-semibold text-gray-400 uppercase mb-2">
                  Status
                </h3>
                <p className="text-gray-900 font-medium">
                  {report.pdfUrl ? "Generated" : "Pending"}
                </p>
              </div>
              <div className="bg-gray-50 p-4 rounded-lg border border-gray-200">
                <h3 className="text-xs font-semibold text-gray-400 uppercase mb-2">
                  Created
                </h3>
                <p className="text-gray-900 font-medium">
                  {new Date(report.createdAt).toLocaleDateString()}
                </p>
              </div>
            </div>

            <div className="bg-white p-6 rounded-lg border border-gray-200">
              <h3 className="text-lg font-bold text-gray-900 mb-4">Report Preview</h3>
              <div className="aspect-video bg-gray-100 rounded-lg border border-gray-200 flex items-center justify-center">
                {report.pdfUrl ? (
                  <iframe
                    src={report.pdfUrl}
                    className="w-full h-full rounded-lg"
                    title="Report preview"
                  />
                ) : (
                  <div className="text-center py-12 text-gray-400">
                    <p>No PDF available for this report.</p>
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
