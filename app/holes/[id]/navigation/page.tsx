"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import Borehole3DChart from "@/app/components/Borehole3DChart";
import {
  calculateDistanceToTarget,
  calculateStraightPathToTargetTVD,
  calculateMinDoglegToTarget,
  DistanceResult,
  StraightPathResult,
  MinDoglegResult,
} from "@/lib/survey-math";

interface Survey {
  id: string;
  measuredDepth: number;
  inclination: number;
  azimuth: number;
  trueVerticalDepth: number | null;
  northing: number | null;
  easting: number | null;
}

interface Collar {
  id: string;
  northing: number;
  easting: number;
  elevation: number;
}

interface Target {
  id: string;
  tvd: number;
  northing: number;
  easting: number;
}

export default function HoleNavigationPage({ params }: { params: { id: string } }) {
  const [hole, setHole] = useState<any>(null);
  const [collar, setCollar] = useState<Collar | null>(null);
  const [target, setTarget] = useState<Target | null>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);

  const [activeTab, setActiveTab] = useState<"status" | "options">("status");
  const [mobileTab, setMobileTab] = useState<"3d" | "status">("3d");
  const [showAdvanced, setShowAdvanced] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchData();
  }, [params.id]);

  const fetchData = async () => {
    try {
      const [hRes, cRes, tRes, sRes] = await Promise.all([
        fetch(`/api/holes/${params.id}`),
        fetch(`/api/holes/${params.id}/collar`),
        fetch(`/api/holes/${params.id}/target`),
        fetch(`/api/holes/${params.id}/surveys`),
      ]);

      const hData = await hRes.json();
      const cData = await cRes.json();
      const tData = await tRes.json();
      const sData = await sRes.json();

      setHole(hData.hole);
      setCollar(cData.collar || null);
      setTarget(tData.target || null);
      setSurveys(sData.surveys || []);
    } catch {
      setError("Failed to load borehole navigation data");
    } finally {
      setIsLoading(false);
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading Hole Navigation...</p>
      </div>
    );
  }

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

  // Last survey station
  const lastSurvey =
    surveys.length > 0 ? surveys[surveys.length - 1] : null;

  // Compute navigation metrics
  let endOfHoleDistance: DistanceResult | null = null;
  let straightPath: StraightPathResult | null = null;
  let minDogleg: MinDoglegResult | null = null;

  if (lastSurvey && target && lastSurvey.northing !== null && lastSurvey.easting !== null && lastSurvey.trueVerticalDepth !== null) {
    const ls = {
      measuredDepth: Number(lastSurvey.measuredDepth),
      inclination: Number(lastSurvey.inclination),
      azimuth: Number(lastSurvey.azimuth),
      trueVerticalDepth: Number(lastSurvey.trueVerticalDepth),
      northing: Number(lastSurvey.northing),
      easting: Number(lastSurvey.easting),
    };
    const tg = {
      tvd: Number(target.tvd),
      northing: Number(target.northing),
      easting: Number(target.easting),
    };

    endOfHoleDistance = calculateDistanceToTarget(ls, tg);
    straightPath = calculateStraightPathToTargetTVD(ls, tg);
    minDogleg = calculateMinDoglegToTarget(ls, tg);
  }

  return (
    <div className="min-h-screen bg-gray-100 flex flex-col">
      {/* Top Header Bar matching Screenshot 2 */}
      <header className="bg-white border-b border-gray-200 px-6 py-3 flex items-center justify-between">
        <div className="flex items-center gap-4">
          <div className="w-8 h-8 rounded-lg bg-blue-600 flex items-center justify-center text-white font-bold text-sm">
            SQ
          </div>
          <div>
            <h1 className="text-xl font-bold text-gray-900 flex items-center gap-2">
              Hole Navigation:
              <span className="text-blue-600 font-semibold">{hole.name}</span>
            </h1>
            <p className="text-xs text-gray-500">
              ID: {hole.holeId} · Rig: {hole.rigId}
              {hole.user && (
                <span className="ml-2 text-slate-600 font-medium">
                  · Creator: {hole.user.name || hole.user.email}
                </span>
              )}
            </p>
          </div>
        </div>

        {/* Status Indicators matching Screenshot 2 */}
        <div className="flex items-center gap-6">
          <div className="flex items-center gap-2 text-xs font-semibold">
            <span className="text-gray-600">Severe Dogleg:</span>
            <span
              className={`w-10 h-5 rounded flex items-center justify-center font-bold text-white text-[11px] ${
                minDogleg?.severity === "severe" ? "bg-amber-500 shadow-sm animate-pulse" : "bg-amber-400/80"
              }`}
            >
              &gt; 3°
            </span>
          </div>

          <div className="flex items-center gap-2 text-xs font-semibold">
            <span className="text-gray-600">Extreme Dogleg:</span>
            <span
              className={`w-10 h-5 rounded flex items-center justify-center font-bold text-white text-[11px] ${
                minDogleg?.severity === "extreme" ? "bg-red-600 shadow-sm animate-pulse" : "bg-red-500/80"
              }`}
            >
              &gt; 7°
            </span>
          </div>

          <div className="w-8 h-8 rounded-full border-2 border-green-500 flex items-center justify-center bg-green-50 text-green-700 font-bold text-xs shadow-sm">
            🎯
          </div>
        </div>
      </header>

      {/* Main Screen Layout */}
      {/* Mobile View Segmented Switch (visible on < lg screens) */}
      <div className="lg:hidden flex border-b border-gray-200 bg-white sticky top-0 z-20 shadow-xs">
        <button
          onClick={() => setMobileTab("3d")}
          className={`flex-1 py-2.5 text-xs font-bold transition flex items-center justify-center gap-1.5 ${
            mobileTab === "3d"
              ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/40"
              : "text-gray-500 hover:text-gray-700"
          }`}
        >
          <span>🧭</span>
          <span>3D Trajectory</span>
        </button>
        <button
          onClick={() => setMobileTab("status")}
          className={`flex-1 py-2.5 text-xs font-bold transition flex items-center justify-center gap-1.5 ${
            mobileTab === "status"
              ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/40"
              : "text-gray-500 hover:text-gray-700"
          }`}
        >
          <span>📊</span>
          <span>Navigation Status</span>
        </button>
      </div>

      <div className="flex-1 flex flex-col lg:flex-row overflow-hidden">
        {/* Left Action Toolbar Buttons (Matching Screenshot 2 Sidebar, scrollable on mobile) */}
        <div className="w-full lg:w-48 bg-slate-900 p-2.5 lg:p-3 flex flex-row lg:flex-col gap-2 shrink-0 border-b lg:border-b-0 lg:border-r border-slate-800 overflow-x-auto">
          <Link
            href={`/holes/${params.id}`}
            className="flex items-center gap-2 px-3 py-2.5 rounded-lg bg-blue-700 text-white text-xs font-medium hover:bg-blue-800 transition"
          >
            <span>⬅</span>
            <span>Hole Management</span>
          </Link>

          <div className="hidden lg:block my-1 border-t border-slate-700/60" />

          <Link
            href={`/build-to-target?holeId=${params.id}`}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
          >
            <span>🔨</span>
            <span>Build To Target</span>
          </Link>

          <Link
            href={`/trend-analysis/${params.id}`}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
          >
            <span>📐</span>
            <span>Trend Analysis</span>
          </Link>

          <Link
            href={`/extrapolate/${params.id}`}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
          >
            <span>🧮</span>
            <span>Extrapolate</span>
          </Link>

          <Link
            href={`/holes/${params.id}/sidetrack`}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
          >
            <span>🌱</span>
            <span>Sidetrack Hole</span>
          </Link>

          <button
            onClick={() => setShowAdvanced(true)}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition text-left"
          >
            <span>⚡</span>
            <span>Advanced</span>
          </button>

          <Link
            href={`/reports`}
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-200 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
          >
            <span>📑</span>
            <span>Report</span>
          </Link>

          <div className="hidden lg:block my-1 border-t border-slate-800" />

          <a
            href={`/api/holes/${params.id}/export/ezn`}
            download
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-300 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
            title="Export official SphereIQ .ezn file"
          >
            <span>💾</span>
            <span>Export .ezn</span>
          </a>

          <a
            href={`/api/holes/${params.id}/export/csv`}
            download
            className="flex items-center gap-2 px-3 py-2 rounded-lg text-slate-300 hover:bg-slate-800 hover:text-white text-xs font-medium transition"
            title="Export CSV surveys"
          >
            <span>📊</span>
            <span>Export CSV</span>
          </a>
        </div>

        {/* Status Panel (Matching Screenshot 2 Left Tabs) */}
        <div className={`w-full lg:w-80 bg-white border-r border-gray-200 flex flex-col shrink-0 overflow-y-auto ${mobileTab === "status" ? "block" : "hidden lg:flex"}`}>
          {/* Tabs */}
          <div className="flex border-b border-gray-200 bg-gray-50">
            <button
              onClick={() => setActiveTab("status")}
              className={`flex-1 py-2.5 text-xs font-semibold uppercase tracking-wider transition ${
                activeTab === "status"
                  ? "bg-white text-blue-600 border-b-2 border-blue-600"
                  : "text-gray-500 hover:text-gray-700"
              }`}
            >
              Hole Status
            </button>
            <button
              onClick={() => setActiveTab("options")}
              className={`flex-1 py-2.5 text-xs font-semibold uppercase tracking-wider transition ${
                activeTab === "options"
                  ? "bg-white text-blue-600 border-b-2 border-blue-600"
                  : "text-gray-500 hover:text-gray-700"
              }`}
            >
              Chart Options
            </button>
          </div>

          <div className="p-4 space-y-4 text-xs">
            {activeTab === "status" ? (
              <>
                {/* Drill hole depth */}
                <div className="border border-gray-200 rounded-lg p-3 bg-gray-50/50">
                  <p className="font-bold text-gray-800 uppercase mb-2">Drill hole depth (m):</p>
                  <div className="grid grid-cols-2 gap-2 text-gray-700">
                    <p>
                      <span className="text-gray-500">MD:</span>{" "}
                      <span className="font-semibold">{lastSurvey ? Number(lastSurvey.measuredDepth).toFixed(2) : "0.00"}</span>
                    </p>
                    <p>
                      <span className="text-gray-500">TVD:</span>{" "}
                      <span className="font-semibold">
                        {lastSurvey?.trueVerticalDepth ? Number(lastSurvey.trueVerticalDepth).toFixed(2) : "0.00"}
                      </span>
                    </p>
                  </div>
                </div>

                {/* End of hole distance from target */}
                <div className="border border-gray-200 rounded-lg p-3 bg-gray-50/50">
                  <p className="font-bold text-gray-800 uppercase mb-2">End of hole distance from target (m):</p>
                  {endOfHoleDistance ? (
                    <div className="grid grid-cols-2 gap-y-1.5 text-gray-700">
                      <p>
                        <span className="text-gray-500">North:</span>{" "}
                        <span className="font-semibold">{endOfHoleDistance.north.toFixed(2)}</span>
                      </p>
                      <p>
                        <span className="text-gray-500">East:</span>{" "}
                        <span className="font-semibold">{endOfHoleDistance.east.toFixed(2)}</span>
                      </p>
                      <p>
                        <span className="text-gray-500">HD:</span>{" "}
                        <span className="font-semibold text-blue-600">
                          {endOfHoleDistance.horizontalDistance.toFixed(2)}
                        </span>
                      </p>
                      <p>
                        <span className="text-gray-500">TVD:</span>{" "}
                        <span className="font-semibold">{endOfHoleDistance.tvd.toFixed(2)}</span>
                      </p>
                    </div>
                  ) : (
                    <p className="text-gray-400">Target or surveys not defined</p>
                  )}
                </div>

                {/* Straight path to target TVD distance */}
                <div className="border border-gray-200 rounded-lg p-3 bg-gray-50/50">
                  <p className="font-bold text-gray-800 uppercase mb-2">
                    Straight path to target TVD distance from target (m):
                  </p>
                  {straightPath ? (
                    <div className="grid grid-cols-3 gap-y-1 text-gray-700">
                      <p>
                        <span className="text-gray-500">North:</span>{" "}
                        <span className="font-semibold">{straightPath.northDistance.toFixed(2)}</span>
                      </p>
                      <p>
                        <span className="text-gray-500">East:</span>{" "}
                        <span className="font-semibold">{straightPath.eastDistance.toFixed(2)}</span>
                      </p>
                      <p>
                        <span className="text-gray-500">HD:</span>{" "}
                        <span className="font-semibold text-red-600">
                          {straightPath.horizontalDistance.toFixed(2)}
                        </span>
                      </p>
                    </div>
                  ) : (
                    <p className="text-gray-400">Path already below target TVD or not available</p>
                  )}
                </div>

                {/* Minimum dogleg to target */}
                <div className="border border-gray-200 rounded-lg p-3 bg-gray-50/50">
                  <p className="font-bold text-gray-800 uppercase mb-1">
                    Minimum dogleg to target (degrees/30m):
                  </p>
                  {minDogleg ? (
                    <div className="flex items-center justify-between mt-2">
                      <span className="text-xl font-extrabold text-gray-900">
                        {minDogleg.stateString}
                      </span>
                      <span
                        className={`px-2.5 py-1 rounded-full text-[11px] font-bold text-white uppercase ${
                          minDogleg.severity === "extreme"
                            ? "bg-red-600"
                            : minDogleg.severity === "severe"
                            ? "bg-amber-500"
                            : "bg-green-600"
                        }`}
                      >
                        {minDogleg.severity}
                      </span>
                    </div>
                  ) : (
                    <p className="text-gray-400 mt-1">Not calculated</p>
                  )}
                </div>

                {/* Target & Collar reference */}
                <div className="border border-gray-200 rounded-lg p-3 space-y-2 bg-gray-50/50">
                  <div>
                    <p className="font-bold text-gray-800 uppercase mb-1">Target (m):</p>
                    {target ? (
                      <div className="text-gray-700 space-y-0.5">
                        <p>
                          <span className="text-gray-500">Easting:</span> {Number(target.easting).toFixed(3)}
                        </p>
                        <p>
                          <span className="text-gray-500">Northing:</span> {Number(target.northing).toFixed(3)}
                        </p>
                        <p>
                          <span className="text-gray-500">TVD:</span> {Number(target.tvd).toFixed(2)}
                        </p>
                      </div>
                    ) : (
                      <p className="text-gray-400">Target not set</p>
                    )}
                  </div>

                  <div className="pt-2 border-t border-gray-200">
                    <p className="font-bold text-gray-800 uppercase mb-1">Collar (m):</p>
                    {collar ? (
                      <div className="text-gray-700 space-y-0.5">
                        <p>
                          <span className="text-gray-500">Easting:</span> {Number(collar.easting).toFixed(3)}
                        </p>
                        <p>
                          <span className="text-gray-500">Northing:</span> {Number(collar.northing).toFixed(3)}
                        </p>
                      </div>
                    ) : (
                      <p className="text-gray-400">Collar not set</p>
                    )}
                  </div>
                </div>
              </>
            ) : (
              <div className="space-y-3">
                <p className="font-bold text-gray-800 uppercase mb-2">3D Display Layers:</p>
                <p className="text-gray-500 text-xs">
                  Interact with the 3D model using mouse or touch gestures.
                </p>
                <div className="p-3 bg-gray-50 rounded-lg border border-gray-200 space-y-2 text-xs">
                  <p>✔ Collar Marker (Magenta Sphere)</p>
                  <p>✔ Target Marker (Orange Sphere)</p>
                  <p>✔ Planned Wellpath (Black Solid Line)</p>
                  <p>✔ Survey Data Stations (Green Spheres)</p>
                  <p>✔ Straight Path Projection (Red Line)</p>
                  <p>✔ Ground Plane at TVD = 0 (Translucent Blue)</p>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* 3D WebGL Chart Viewport (Matching Screenshot 2) */}
        <div className={`flex-1 p-2 sm:p-4 bg-gray-100 flex flex-col min-h-[420px] lg:min-h-[500px] ${mobileTab === "3d" ? "block" : "hidden lg:flex"}`}>
          <Borehole3DChart
            collar={collar}
            target={target}
            surveys={surveys}
            straightPath={straightPath}
            showStraightPath={true}
            className="flex-1 w-full h-full min-h-[550px]"
          />
        </div>
      </div>

      {/* Advanced Data Modal (Port of Popup_DrillHolePlanning_Advanced.cs) */}
      {showAdvanced && (
        <div className="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
          <div className="bg-white rounded-xl shadow-xl max-w-2xl w-full p-6 border border-gray-200 max-h-[90vh] overflow-y-auto">
            <div className="flex items-center justify-between pb-3 border-b border-gray-200 mb-4">
              <h2 className="text-lg font-bold text-gray-900 flex items-center gap-2">
                <span>⚡</span> Advanced Navigation Coordinates
              </h2>
              <button
                onClick={() => setShowAdvanced(false)}
                className="text-gray-400 hover:text-gray-600 font-bold text-lg"
              >
                ✕
              </button>
            </div>

            <div className="space-y-4 text-xs">
              {/* Straight Path Group */}
              <div className="border border-gray-200 rounded-lg p-4 bg-gray-50">
                <h3 className="font-bold text-gray-800 uppercase mb-3">Straight Path to Target TVD Position</h3>
                {straightPath ? (
                  <div className="grid grid-cols-3 gap-3">
                    <div>
                      <span className="text-gray-500 block">Target MD:</span>
                      <span className="font-semibold text-gray-900">{straightPath.md.toFixed(2)} m</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">TVD:</span>
                      <span className="font-semibold text-gray-900">{straightPath.tvd.toFixed(2)} m</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Northing:</span>
                      <span className="font-semibold text-gray-900">{straightPath.northing.toFixed(3)}</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Easting:</span>
                      <span className="font-semibold text-gray-900">{straightPath.easting.toFixed(3)}</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Δ North from Target:</span>
                      <span className="font-semibold text-gray-900">{straightPath.northDistance.toFixed(2)} m</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Δ East from Target:</span>
                      <span className="font-semibold text-gray-900">{straightPath.eastDistance.toFixed(2)} m</span>
                    </div>
                  </div>
                ) : (
                  <p className="text-gray-500">Straight path projection not available</p>
                )}
              </div>

              {/* End of Hole Group */}
              <div className="border border-gray-200 rounded-lg p-4 bg-gray-50">
                <h3 className="font-bold text-gray-800 uppercase mb-3">Current End of Hole Position</h3>
                {lastSurvey ? (
                  <div className="grid grid-cols-3 gap-3">
                    <div>
                      <span className="text-gray-500 block">MD:</span>
                      <span className="font-semibold text-gray-900">{Number(lastSurvey.measuredDepth).toFixed(2)} m</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Dip / Inc:</span>
                      <span className="font-semibold text-gray-900">{Number(lastSurvey.inclination).toFixed(2)}°</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Azimuth:</span>
                      <span className="font-semibold text-gray-900">{Number(lastSurvey.azimuth).toFixed(2)}°</span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">TVD:</span>
                      <span className="font-semibold text-gray-900">
                        {lastSurvey.trueVerticalDepth !== null ? Number(lastSurvey.trueVerticalDepth).toFixed(2) : "—"} m
                      </span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Northing:</span>
                      <span className="font-semibold text-gray-900">
                        {lastSurvey.northing !== null ? Number(lastSurvey.northing).toFixed(3) : "—"}
                      </span>
                    </div>
                    <div>
                      <span className="text-gray-500 block">Easting:</span>
                      <span className="font-semibold text-gray-900">
                        {lastSurvey.easting !== null ? Number(lastSurvey.easting).toFixed(3) : "—"}
                      </span>
                    </div>
                  </div>
                ) : (
                  <p className="text-gray-500">No survey records available</p>
                )}
              </div>
            </div>

            <div className="mt-6 flex justify-end">
              <button
                onClick={() => setShowAdvanced(false)}
                className="px-5 py-2 bg-blue-600 text-white rounded-lg text-xs font-medium hover:bg-blue-700 transition"
              >
                Close
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

