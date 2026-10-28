import { useState } from "react";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import Link from "next/link";

export default async function BuildToTargetPage({
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
    include: { collar: true, surveys: { orderBy: { measuredDepth: "asc" } }, target: true },
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

  const [target, setTarget] = useState({
    tvd: "",
    northing: "",
    easting: "",
  });
  const [vsAzimuth, setVsAzimuth] = useState(0);
  const [result, setResult] = useState<any>(null);
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

      const res = await fetch("/api/build-to-target", {
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
          target: {
            tvd: Number(target.tvd),
            northing: Number(target.northing),
            easting: Number(target.easting),
          },
          vsAzimuth,
        }),
      });

      if (!res.ok) throw new Error("Build calculation failed");
      const data = await res.json();
      setResult(data);
    } catch {
      setError("Build-to-target calculation failed");
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
              <h1 className="text-3xl font-bold text-gray-900">Build to Target</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Target Settings</h2>
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target TVD (m)
                  </label>
                  <input
                    type="number"
                    required
                    value={target.tvd}
                    onChange={(e) => setTarget({ ...target, tvd: e.target.value })}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Northing (m)
                  </label>
                  <input
                    type="number"
                    value={target.northing}
                    onChange={(e) => setTarget({ ...target, northing: e.target.value })}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Easting (m)
                  </label>
                  <input
                    type="number"
                    value={target.easting}
                    onChange={(e) => setTarget({ ...target, easting: e.target.value })}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
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
                <button
                  onClick={handleCalculate}
                  disabled={isCalculating || !target.tvd}
                  className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  {isCalculating ? "Calculating..." : "Calculate Build Plan"}
                </button>
              </div>
            </div>

            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Results</h2>
              {error && (
                <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
                  {error}
                </div>
              )}
              {result && (
                <div className="space-y-4">
                  <div className="flex items-center justify-between p-3 rounded-lg bg-blue-50 border border-blue-100">
                    <span className="text-sm font-medium text-gray-700">
                      Above Target
                    </span>
                    <span
                      className={`text-sm font-medium ${
                        result.aboveTarget ? "text-green-600" : "text-red-600"
                      }`}
                    >
                      {result.aboveTarget ? "Yes" : "No"}
                    </span>
                  </div>
                  {result.aboveTarget && result.buildPlan && (
                    <div className="space-y-3">
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-600">Build Rate</span>
                        <span className="text-gray-900 font-medium">
                          {result.buildPlan.buildRate.toFixed(2)} °/30m
                        </span>
                      </div>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-600">Build End MD</span>
                        <span className="text-gray-900 font-medium">
                          {result.buildPlan.buildEndMD.toFixed(2)} m
                        </span>
                      </div>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-600">Total MD</span>
                        <span className="text-gray-900 font-medium">
                          {result.buildPlan.totalMD.toFixed(2)} m
                        </span>
                      </div>
                    </div>
                  )}
                  {!result.aboveTarget && (
                    <div className="p-3 rounded-md bg-amber-50 border border-amber-100 text-sm text-amber-700">
                      Hole is at or below target TVD. Build to target cannot be performed.
                    </div>
                  )}
                </div>
              )}
              {!result && (
                <p className="text-gray-500 text-sm">Enter target settings and calculate.</p>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}