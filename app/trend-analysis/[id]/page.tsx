"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

interface SurveyRow {
  id: string;
  measuredDepth: number;
  inclination: number;
  azimuth: number;
  trueVerticalDepth: number | null;
  doglegSeverity?: number;
}

interface TrendResult {
  averageInclination: number;
  averageAzimuth: number;
  averageTVD: number;
  totalMeasuredDepth: number;
  totalTVDChange: number;
  inclinationRange: { min: number; max: number };
  azimuthRange: { min: number; max: number };
  surveyCount: number;
  doglegSeverityTrend: { measuredDepth: number; dls: number }[];
}

export default function TrendAnalysisPage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [surveys, setSurveys] = useState<SurveyRow[]>([]);
  const [trend, setTrend] = useState<TrendResult | null>(null);
  const [fromMD, setFromMD] = useState("");
  const [toMD, setToMD] = useState("");
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const [hRes, sRes] = await Promise.all([
          fetch("/api/holes/" + params.id),
          fetch("/api/holes/" + params.id + "/surveys"),
        ]);
        const hData = await hRes.json();
        const sData = await sRes.json();
        setHole(hData.hole);
        const sortedSurveys = (sData.surveys || []).sort(
          (a: any, b: any) => Number(a.measuredDepth) - Number(b.measuredDepth)
        );
        setSurveys(sortedSurveys);
      } catch {
        setError("Failed to load data");
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, [params.id]);

  const handleRunAnalysis = async () => {
    setIsCalculating(true);
    setError("");
    setTrend(null);
    try {
      let filtered = surveys;
      if (fromMD) {
        filtered = filtered.filter(
          (s) => Number(s.measuredDepth) >= parseFloat(fromMD)
        );
      }
      if (toMD) {
        filtered = filtered.filter(
          (s) => Number(s.measuredDepth) <= parseFloat(toMD)
        );
      }
      if (filtered.length < 2) {
        setError("Need at least 2 surveys in range for trend analysis");
        setIsCalculating(false);
        return;
      }

      // First calculate trajectory to get TVD and DLS values
      let calculatedSurveys = filtered.map((s) => ({
        measuredDepth: Number(s.measuredDepth),
        inclination: Number(s.inclination),
        azimuth: Number(s.azimuth),
        trueVerticalDepth: Number(s.trueVerticalDepth || 0),
        doglegSeverity: 0,
      }));

      // Try to get calculated trajectory with DLS
      if (hole?.collar) {
        try {
          const calcRes = await fetch("/api/survey/calculate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              holeId: hole.id,
              collar: {
                northing: Number(hole.collar.northing),
                easting: Number(hole.collar.easting),
                elevation: Number(hole.collar.elevation),
              },
              stations: filtered.map((s) => ({
                measuredDepth: Number(s.measuredDepth),
                inclination: Number(s.inclination),
                azimuth: Number(s.azimuth),
              })),
            }),
          });
          if (calcRes.ok) {
            const calcData = await calcRes.json();
            calculatedSurveys = calcData.trajectory || calculatedSurveys;
          }
        } catch {
          // Use raw data if calculation fails
        }
      }

      const res = await fetch("/api/trend-analysis", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ holeId: params.id, surveys: calculatedSurveys }),
      });
      if (!res.ok) throw new Error("Analysis failed");
      const data = await res.json();
      setTrend(data.trend);
    } catch {
      setError("Trend analysis failed");
    } finally {
      setIsCalculating(false);
    }
  };

  if (isLoading)
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading...</p>
      </div>
    );

  if (!hole)
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg shadow-sm border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">
            Hole not found
          </h1>
          <Link href="/holes" className="text-blue-600 hover:underline">
            Back to holes
          </Link>
        </div>
      </div>
    );

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-5xl mx-auto">
          <div className="mb-8">
            <Link
              href={"/holes/" + params.id}
              className="text-sm text-blue-600 hover:underline mb-2 inline-block"
            >
              ← Back to hole
            </Link>
            <h1 className="text-3xl font-bold text-gray-900">
              Trend Analysis
            </h1>
            <p className="text-gray-600">
              {hole.name} — {hole.holeId}
            </p>
          </div>

          {error && (
            <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
              {error}
            </div>
          )}

          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8 mb-8">
            {/* Controls */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">
                Analysis Range
              </h2>
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    From MD (m)
                  </label>
                  <input
                    type="number"
                    step="any"
                    placeholder="Start MD"
                    value={fromMD}
                    onChange={(e) => setFromMD(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:outline-none text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    To MD (m)
                  </label>
                  <input
                    type="number"
                    step="any"
                    placeholder="End MD"
                    value={toMD}
                    onChange={(e) => setToMD(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:outline-none text-sm"
                  />
                </div>
                <p className="text-xs text-gray-500">
                  Leave blank to include all surveys ({surveys.length} total)
                </p>
                <button
                  onClick={handleRunAnalysis}
                  disabled={isCalculating || surveys.length < 2}
                  className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  {isCalculating ? "Analyzing..." : "Run Trend Analysis"}
                </button>
              </div>
            </div>

            {/* Results */}
            <div className="lg:col-span-2 bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">
                Results
              </h2>
              {!trend && (
                <p className="text-gray-500 text-sm">
                  Select a range and run the analysis.
                </p>
              )}
              {trend && (
                <div className="space-y-6">
                  <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                    <div className="bg-blue-50 p-3 rounded-lg">
                      <p className="text-xs text-blue-600 font-medium uppercase">
                        Surveys
                      </p>
                      <p className="text-xl font-bold text-gray-900">
                        {trend.surveyCount}
                      </p>
                    </div>
                    <div className="bg-blue-50 p-3 rounded-lg">
                      <p className="text-xs text-blue-600 font-medium uppercase">
                        Total MD
                      </p>
                      <p className="text-xl font-bold text-gray-900">
                        {trend.totalMeasuredDepth.toFixed(2)} m
                      </p>
                    </div>
                    <div className="bg-blue-50 p-3 rounded-lg">
                      <p className="text-xs text-blue-600 font-medium uppercase">
                        TVD Change
                      </p>
                      <p className="text-xl font-bold text-gray-900">
                        {trend.totalTVDChange.toFixed(2)} m
                      </p>
                    </div>
                    <div className="bg-blue-50 p-3 rounded-lg">
                      <p className="text-xs text-blue-600 font-medium uppercase">
                        Avg TVD
                      </p>
                      <p className="text-xl font-bold text-gray-900">
                        {trend.averageTVD.toFixed(2)} m
                      </p>
                    </div>
                  </div>

                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="p-4 rounded-lg border border-gray-200">
                      <p className="text-sm font-medium text-gray-700 mb-2">
                        Inclination
                      </p>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-500">Average</span>
                        <span className="font-medium">
                          {trend.averageInclination.toFixed(2)}°
                        </span>
                      </div>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-500">Range</span>
                        <span className="font-medium">
                          {trend.inclinationRange.min.toFixed(2)}° —{" "}
                          {trend.inclinationRange.max.toFixed(2)}°
                        </span>
                      </div>
                    </div>
                    <div className="p-4 rounded-lg border border-gray-200">
                      <p className="text-sm font-medium text-gray-700 mb-2">
                        Azimuth
                      </p>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-500">Average</span>
                        <span className="font-medium">
                          {trend.averageAzimuth.toFixed(2)}°
                        </span>
                      </div>
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-500">Range</span>
                        <span className="font-medium">
                          {trend.azimuthRange.min.toFixed(2)}° —{" "}
                          {trend.azimuthRange.max.toFixed(2)}°
                        </span>
                      </div>
                    </div>
                  </div>

                  {/* DLS Trend Table */}
                  {trend.doglegSeverityTrend.length > 0 && (
                    <div>
                      <p className="text-sm font-medium text-gray-700 mb-2">
                        Dogleg Severity Trend
                      </p>
                      <div className="overflow-x-auto">
                        <table className="w-full text-left border-collapse text-sm">
                          <thead>
                            <tr className="border-b border-gray-200">
                              <th className="pb-2 text-xs text-gray-400 uppercase">
                                MD (m)
                              </th>
                              <th className="pb-2 text-xs text-gray-400 uppercase">
                                DLS (°/30m)
                              </th>
                            </tr>
                          </thead>
                          <tbody>
                            {trend.doglegSeverityTrend.map((d, i) => (
                              <tr
                                key={i}
                                className="border-b border-gray-100"
                              >
                                <td className="py-1.5 text-gray-900">
                                  {d.measuredDepth.toFixed(2)}
                                </td>
                                <td className="py-1.5 text-gray-900">
                                  {d.dls.toFixed(2)}
                                </td>
                              </tr>
                            ))}
                          </tbody>
                        </table>
                      </div>
                    </div>
                  )}
                </div>
              )}
            </div>
          </div>

          {/* Survey Table */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <h2 className="text-lg font-bold text-gray-900 mb-4">
              Survey Data ({surveys.length} records)
            </h2>
            {surveys.length === 0 ? (
              <p className="text-gray-500 text-sm">No survey data available.</p>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full text-left border-collapse text-sm">
                  <thead>
                    <tr className="border-b border-gray-200">
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        MD (m)
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Inc (°)
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        Az (°)
                      </th>
                      <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                        TVD (m)
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {surveys.map((s) => (
                      <tr key={s.id} className="border-b border-gray-100">
                        <td className="py-2 text-gray-900">
                          {String(s.measuredDepth)}
                        </td>
                        <td className="py-2 text-gray-900">
                          {String(s.inclination)}°
                        </td>
                        <td className="py-2 text-gray-900">
                          {String(s.azimuth)}°
                        </td>
                        <td className="py-2 text-gray-600">
                          {s.trueVerticalDepth
                            ? String(s.trueVerticalDepth) + " m"
                            : "—"}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
