"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

export default function BuildToTargetPage() {
  // ── All hooks declared FIRST ──────────────────────────────────────────────
  const [holes, setHoles] = useState<any[]>([]);
  const [selectedHoleId, setSelectedHoleId] = useState("");
  const [hole, setHole] = useState<any>(null);
  const [surveys, setSurveys] = useState<any[]>([]);
  const [collar, setCollar] = useState<any>(null);
  const [isLoadingHoles, setIsLoadingHoles] = useState(true);
  const [isLoadingHole, setIsLoadingHole] = useState(false);
  const [targetTVD, setTargetTVD] = useState("");
  const [targetNorthing, setTargetNorthing] = useState("");
  const [targetEasting, setTargetEasting] = useState("");
  const [vsAzimuth, setVsAzimuth] = useState("0");
  const [result, setResult] = useState<any>(null);
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState("");
  // ─────────────────────────────────────────────────────────────────────────

  // Fetch the list of holes on mount
  useEffect(() => {
    fetch("/api/holes")
      .then((res) => res.json())
      .then((data) => {
        setHoles(data.holes ?? []);
      })
      .catch(() => {})
      .finally(() => setIsLoadingHoles(false));
  }, []);

  // Fetch collar + surveys whenever a hole is selected
  useEffect(() => {
    if (!selectedHoleId) {
      setHole(null);
      setSurveys([]);
      setCollar(null);
      setResult(null);
      return;
    }
    setIsLoadingHole(true);
    setResult(null);
    setError("");

    Promise.all([
      fetch(`/api/holes/${selectedHoleId}`).then((r) => r.json()),
      fetch(`/api/holes/${selectedHoleId}/surveys`).then((r) => r.json()),
      fetch(`/api/holes/${selectedHoleId}/collar`).then((r) => r.json()),
    ])
      .then(([holeData, surveysData, collarData]) => {
        setHole(holeData.hole);
        setSurveys(surveysData.surveys ?? holeData.hole?.surveys ?? []);
        setCollar(collarData.collar ?? holeData.hole?.collar ?? null);
      })
      .catch(() => {})
      .finally(() => setIsLoadingHole(false));
  }, [selectedHoleId]);

  const handleCalculate = async () => {
    if (!targetTVD) return;
    setIsCalculating(true);
    setError("");
    setResult(null);

    try {
      const stations = surveys.map((s: any) => ({
        measuredDepth: Number(s.measuredDepth),
        inclination: Number(s.inclination),
        azimuth: Number(s.azimuth),
      }));

      const collarPayload = collar
        ? {
            northing: Number(collar.northing),
            easting: Number(collar.easting),
            elevation: Number(collar.elevation),
          }
        : { northing: 0, easting: 0, elevation: 0 };

      const res = await fetch("/api/build-to-target", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: selectedHoleId,
          collar: collarPayload,
          stations,
          target: {
            tvd: Number(targetTVD),
            northing: Number(targetNorthing),
            easting: Number(targetEasting),
          },
          vsAzimuth: Number(vsAzimuth),
        }),
      });

      if (!res.ok) throw new Error("Build calculation failed");
      const data = await res.json();
      setResult(data);
    } catch {
      setError("Build-to-target calculation failed. Please check your inputs.");
    } finally {
      setIsCalculating(false);
    }
  };

  const lastSurvey = surveys.length > 0 ? surveys[surveys.length - 1] : null;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          {/* Header */}
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link
                href="/holes"
                className="text-sm text-blue-600 hover:underline mb-2 inline-block"
              >
                ← Back to holes
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Build to Target</h1>
              <p className="text-gray-600">
                Plan a build section to reach a target position
              </p>
            </div>
          </div>

          {/* Hole Selector */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-lg font-bold text-gray-900 mb-3">Select Hole</h2>
            {isLoadingHoles ? (
              <p className="text-sm text-gray-500">Loading holes...</p>
            ) : (
              <select
                value={selectedHoleId}
                onChange={(e) => setSelectedHoleId(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
              >
                <option value="">— Select a hole —</option>
                {holes.map((h: any) => (
                  <option key={h.id} value={h.id}>
                    {h.name} ({h.holeId})
                  </option>
                ))}
              </select>
            )}

            {isLoadingHole && (
              <p className="text-sm text-gray-500 mt-3">Loading hole data...</p>
            )}

            {hole && !isLoadingHole && (
              <div className="mt-4 grid grid-cols-2 sm:grid-cols-4 gap-3">
                <InfoCard label="Status" value={hole.status ?? "—"} />
                <InfoCard label="Surveys" value={String(surveys.length)} />
                {lastSurvey && (
                  <>
                    <InfoCard
                      label="Last MD"
                      value={`${Number(lastSurvey.measuredDepth).toFixed(2)} m`}
                    />
                    <InfoCard
                      label="Last TVD"
                      value={
                        lastSurvey.trueVerticalDepth != null
                          ? `${Number(lastSurvey.trueVerticalDepth).toFixed(2)} m`
                          : "—"
                      }
                    />
                  </>
                )}
              </div>
            )}
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            {/* Target Form */}
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
                    placeholder="e.g. 2000"
                    value={targetTVD}
                    onChange={(e) => setTargetTVD(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Northing (m)
                  </label>
                  <input
                    type="number"
                    placeholder="e.g. 500"
                    value={targetNorthing}
                    onChange={(e) => setTargetNorthing(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Easting (m)
                  </label>
                  <input
                    type="number"
                    placeholder="e.g. 300"
                    value={targetEasting}
                    onChange={(e) => setTargetEasting(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    VS Azimuth (°)
                  </label>
                  <input
                    type="number"
                    value={vsAzimuth}
                    onChange={(e) => setVsAzimuth(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>

                {error && (
                  <div className="p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
                    {error}
                  </div>
                )}

                <button
                  onClick={handleCalculate}
                  disabled={isCalculating || !targetTVD || !selectedHoleId}
                  className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  {isCalculating ? "Calculating..." : "Calculate Build Plan"}
                </button>
              </div>
            </div>

            {/* Results */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Results</h2>

              {!result && !error && (
                <p className="text-gray-500 text-sm">
                  Select a hole, enter target settings, and click Calculate.
                </p>
              )}

              {result && (
                <div className="space-y-4">
                  {/* Above Target Badge */}
                  <div className="flex items-center justify-between p-3 rounded-lg bg-gray-50 border border-gray-100">
                    <span className="text-sm font-medium text-gray-700">Above Target?</span>
                    <span
                      className={`px-2.5 py-0.5 rounded-full text-xs font-semibold ${
                        result.aboveTarget
                          ? "bg-green-100 text-green-700"
                          : "bg-red-100 text-red-700"
                      }`}
                    >
                      {result.aboveTarget ? "Yes" : "No"}
                    </span>
                  </div>

                  {/* Build Plan details */}
                  {result.aboveTarget && result.buildPlan && (
                    <div className="space-y-2">
                      <h3 className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
                        Build Plan
                      </h3>
                      <ResultRow
                        label="Build Rate"
                        value={`${Number(result.buildPlan.buildRate).toFixed(2)} °/30m`}
                      />
                      <ResultRow
                        label="Build End MD"
                        value={`${Number(result.buildPlan.buildEndMD).toFixed(2)} m`}
                      />
                      <ResultRow
                        label="Hold Start MD"
                        value={`${Number(result.buildPlan.holdStartMD ?? result.buildPlan.buildEndMD).toFixed(2)} m`}
                      />
                      <ResultRow
                        label="Total MD"
                        value={`${Number(result.buildPlan.totalMD).toFixed(2)} m`}
                      />
                    </div>
                  )}

                  {/* Last survey position */}
                  {lastSurvey && (
                    <div className="space-y-2 pt-2 border-t border-gray-100">
                      <h3 className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
                        Last Survey Position
                      </h3>
                      <ResultRow
                        label="TVD"
                        value={
                          lastSurvey.trueVerticalDepth != null
                            ? `${Number(lastSurvey.trueVerticalDepth).toFixed(2)} m`
                            : "—"
                        }
                      />
                      <ResultRow
                        label="Northing"
                        value={
                          lastSurvey.northing != null
                            ? `${Number(lastSurvey.northing).toFixed(2)} m`
                            : "—"
                        }
                      />
                      <ResultRow
                        label="Easting"
                        value={
                          lastSurvey.easting != null
                            ? `${Number(lastSurvey.easting).toFixed(2)} m`
                            : "—"
                        }
                      />
                    </div>
                  )}

                  {/* Warning when not above target */}
                  {!result.aboveTarget && (
                    <div className="p-3 rounded-md bg-amber-50 border border-amber-100 text-sm text-amber-700">
                      The hole is at or below the target TVD. Build-to-target cannot be
                      performed from this position.
                    </div>
                  )}
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

function InfoCard({ label, value }: { label: string; value: string }) {
  return (
    <div className="bg-gray-50 rounded-lg p-3 border border-gray-100">
      <p className="text-xs font-semibold text-gray-400 uppercase mb-0.5">{label}</p>
      <p className="text-sm font-bold text-gray-900">{value}</p>
    </div>
  );
}

function ResultRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex items-center justify-between py-2 border-b border-gray-100 last:border-0">
      <span className="text-sm text-gray-600">{label}</span>
      <span className="text-sm font-semibold text-gray-900">{value}</span>
    </div>
  );
}
