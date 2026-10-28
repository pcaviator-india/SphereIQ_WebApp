import { useState } from "react";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function SurveyCalculatePage({
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
    include: { collar: true, surveys: { orderBy: { measuredDepth: "asc" } } },
  });

  if (!hole) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg shadow-sm border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">Hole not found</h1>
          <Link href="/holes" className="text-blue-600 hover:underline">
            Back to holes
          </Link>
        </div>
      </div>
    );
  }

  const [vsAzimuth, setVsAzimuth] = useState(0);
  const [trajectory, setTrajectory] = useState<any[]>([]);
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState("");

  const handleCalculate = async () => {
    setIsCalculating(true);
    setError("");

    try {
      const stations = hole.surveys.map((s: any) => ({
        measuredDepth: s.measuredDepth,
        inclination: s.inclination,
        azimuth: s.azimuth,
      }));

      const res = await fetch("/api/survey/calculate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: hole.id,
          collar: hole.collar
            ? {
                northing: Number(hole.collar.northing),
                easting: Number(hole.collar.easting),
                elevation: Number(hole.collar.elevation),
              }
            : { northing: 0, easting: 0, elevation: 0 },
          stations,
          vsAzimuth,
        }),
      });

      if (!res.ok) throw new Error("Calculation failed");
      const data = await res.json();
      setTrajectory(data.trajectory);
    } catch {
      setError("Survey calculation failed");
    } finally {
      setIsCalculating(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
                ← Back to holes
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Survey Calculation</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Survey Stations</h2>
            <div className="overflow-x-auto">
              <table className="w-full text-left border-collapse">
                <thead>
                  <tr className="border-b border-gray-200">
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">MD</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Inclination</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Azimuth</th>
                  </tr>
                </thead>
                <tbody>
                  {hole.surveys.length === 0 ? (
                    <tr>
                      <td colSpan={3} className="py-4 text-center text-gray-500">
                        No survey data. Add surveys to this hole first.
                      </td>
                    </tr>
                  ) : (
                    hole.surveys.map((s: any) => (
                      <tr key={s.id} className="border-b border-gray-100">
                        <td className="py-3 text-gray-900">{s.measuredDepth} m</td>
                        <td className="py-3 text-gray-900">{s.inclination}°</td>
                        <td className="py-3 text-gray-900">{s.azimuth}°</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Calculation Settings</h2>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Vertical Section Azimuth (°)
                </label>
                <input
                  type="number"
                  value={vsAzimuth}
                  onChange={(e) => setVsAzimuth(Number(e.target.value))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                />
              </div>
            </div>
            <button
              onClick={handleCalculate}
              disabled={isCalculating || hole.surveys.length === 0}
              className="mt-4 w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
            >
              {isCalculating ? "Calculating..." : "Calculate Trajectory"}
            </button>
          </div>

          {error && (
            <div className="mb-6 p-4 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
              {error}
            </div>
          )}

          {trajectory.length > 0 && (
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Calculated Trajectory</h2>
              <div className="overflow-x-auto">
                <table className="w-full text-left border-collapse">
                  <thead>
                    <tr className="border-b border-gray-200">
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">MD</th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">TVD</th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Northing</th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Easting</th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Vert. Section</th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">DLS (°/30m)</th>
                    </tr>
                  </thead>
                  <tbody>
                    {trajectory.map((s) => (
                      <tr key={s.measuredDepth} className="border-b border-gray-100 hover:bg-gray-50 transition">
                        <td className="py-3 text-gray-900">{s.measuredDepth.toFixed(2)} m</td>
                        <td className="py-3 text-gray-900">{s.trueVerticalDepth.toFixed(2)} m</td>
                        <td className="py-3 text-gray-900">{s.northing.toFixed(2)} m</td>
                        <td className="py-3 text-gray-900">{s.easting.toFixed(2)} m</td>
                        <td className="py-3 text-gray-600">{s.verticalSection.toFixed(2)} m</td>
                        <td className="py-3 text-gray-600">{s.doglegSeverity.toFixed(2)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}