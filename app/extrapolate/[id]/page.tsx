"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

export default function ExtrapolatePage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [surveys, setSurveys] = useState<any[]>([]);
  const [collar, setCollar] = useState<any>(null);
  const [targetMD, setTargetMD] = useState("");
  const [vsAzimuth, setVsAzimuth] = useState(0);
  const [result, setResult] = useState<any>(null);
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const [hRes, sRes, cRes] = await Promise.all([
          fetch("/api/holes/" + params.id),
          fetch("/api/holes/" + params.id + "/surveys"),
          fetch("/api/holes/" + params.id + "/collar"),
        ]);
        const hData = await hRes.json();
        const sData = await sRes.json();
        const cData = await cRes.json();
        setHole(hData.hole);
        setSurveys(
          (sData.surveys || []).sort(
            (a: any, b: any) =>
              Number(a.measuredDepth) - Number(b.measuredDepth)
          )
        );
        setCollar(cData.collar || null);
      } catch {
        setError("Failed to load data");
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, [params.id]);

  const handleExtrapolate = async () => {
    setIsCalculating(true);
    setError("");
    setResult(null);
    try {
      if (surveys.length === 0) {
        setError("No surveys to extrapolate from");
        setIsCalculating(false);
        return;
      }
      const c = collar || { northing: 0, easting: 0, elevation: 0 };
      const res = await fetch("/api/extrapolate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: params.id,
          collar: {
            northing: Number(c.northing),
            easting: Number(c.easting),
            elevation: Number(c.elevation),
          },
          stations: surveys.map((s: any) => ({
            measuredDepth: Number(s.measuredDepth),
            inclination: Number(s.inclination),
            azimuth: Number(s.azimuth),
          })),
          targetMD: parseFloat(targetMD),
          vsAzimuth,
        }),
      });
      if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.error || "Extrapolation failed");
      }
      const data = await res.json();
      setResult(data.extrapolation);
    } catch (e: any) {
      setError(e.message || "Extrapolation failed");
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

  const lastSurvey = surveys.length > 0 ? surveys[surveys.length - 1] : null;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="mb-8">
            <Link
              href={"/holes/" + params.id}
              className="text-sm text-blue-600 hover:underline mb-2 inline-block"
            >
              ← Back to hole
            </Link>
            <h1 className="text-3xl font-bold text-gray-900">Extrapolation</h1>
            <p className="text-gray-600">
              {hole.name} — {hole.holeId}
            </p>
          </div>

          {error && (
            <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
              {error}
            </div>
          )}

          {/* Last Survey Info */}
          {lastSurvey && (
            <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-200 mb-6">
              <h3 className="text-sm font-semibold text-gray-700 mb-2">
                Last Survey Station
              </h3>
              <div className="grid grid-cols-4 gap-4 text-sm">
                <div>
                  <p className="text-xs text-gray-400">MD</p>
                  <p className="font-medium text-gray-900">
                    {String(lastSurvey.measuredDepth)} m
                  </p>
                </div>
                <div>
                  <p className="text-xs text-gray-400">TVD</p>
                  <p className="font-medium text-gray-900">
                    {lastSurvey.trueVerticalDepth
                      ? String(lastSurvey.trueVerticalDepth) + " m"
                      : "—"}
                  </p>
                </div>
                <div>
                  <p className="text-xs text-gray-400">Inclination</p>
                  <p className="font-medium text-gray-900">
                    {String(lastSurvey.inclination)}°
                  </p>
                </div>
                <div>
                  <p className="text-xs text-gray-400">Azimuth</p>
                  <p className="font-medium text-gray-900">
                    {String(lastSurvey.azimuth)}°
                  </p>
                </div>
              </div>
            </div>
          )}

          {surveys.length === 0 && (
            <div className="bg-amber-50 p-4 rounded-xl border border-amber-200 mb-6 text-sm text-amber-700">
              No survey data available. Add surveys before extrapolating.
            </div>
          )}

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            {/* Settings */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">
                Extrapolation Settings
              </h2>
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Measured Depth (m)
                  </label>
                  <input
                    type="number"
                    step="any"
                    required
                    value={targetMD}
                    onChange={(e) => setTargetMD(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Vertical Section Azimuth (°)
                  </label>
                  <input
                    type="number"
                    step="any"
                    value={vsAzimuth}
                    onChange={(e) => setVsAzimuth(Number(e.target.value))}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <button
                  onClick={handleExtrapolate}
                  disabled={
                    isCalculating || !targetMD || surveys.length === 0
                  }
                  className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  {isCalculating ? "Calculating..." : "Extrapolate"}
                </button>
              </div>
            </div>

            {/* Results */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">
                Extrapolated Position
              </h2>
              {!result && (
                <p className="text-gray-500 text-sm">
                  Enter target MD and extrapolate position.
                </p>
              )}
              {result && (
                <div className="space-y-3">
                  <div className="flex justify-between text-sm p-2 bg-blue-50 rounded-lg">
                    <span className="text-gray-600">Target MD</span>
                    <span className="font-medium text-gray-900">
                      {result.measuredDepth.toFixed(2)} m
                    </span>
                  </div>
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Extrapolated TVD</span>
                    <span className="font-medium text-gray-900">
                      {result.trueVerticalDepth.toFixed(2)} m
                    </span>
                  </div>
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Northing</span>
                    <span className="font-medium text-gray-900">
                      {result.northing.toFixed(2)} m
                    </span>
                  </div>
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Easting</span>
                    <span className="font-medium text-gray-900">
                      {result.easting.toFixed(2)} m
                    </span>
                  </div>
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Vertical Section</span>
                    <span className="font-medium text-gray-900">
                      {result.verticalSection.toFixed(2)} m
                    </span>
                  </div>
                  {lastSurvey && (
                    <div className="flex justify-between text-sm pt-2 border-t border-gray-200">
                      <span className="text-gray-600">
                        ΔMD from last survey
                      </span>
                      <span className="font-medium text-gray-900">
                        {(
                          result.measuredDepth -
                          Number(lastSurvey.measuredDepth)
                        ).toFixed(2)}{" "}
                        m
                      </span>
                    </div>
                  )}
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Assumed Inc / Az</span>
                    <span className="font-medium text-gray-900">
                      {result.inclination.toFixed(2)}° /{" "}
                      {result.azimuth.toFixed(2)}°
                    </span>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
