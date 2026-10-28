"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import Borehole3DChart from "@/app/components/Borehole3DChart";

interface Survey {
  id: string;
  measuredDepth: number;
  inclination: number;
  azimuth: number;
  trueVerticalDepth: number | null;
  northing: number | null;
  easting: number | null;
}

interface CollarData {
  id: string;
  northing: number;
  easting: number;
  elevation: number;
  totalDepth: number | null;
}

interface TargetData {
  id: string;
  tvd: number;
  northing: number;
  easting: number;
}

interface HoleData {
  id: string;
  holeId: string;
  rigId: string;
  name: string;
  status: string;
  location: string | null;
  area: string | null;
  drillingContractor: string | null;
  surveyingContractor: string | null;
  client: string | null;
  user?: { id: string; name: string | null; email: string } | null;
  collar: CollarData | null;
  target: TargetData | null;
  surveys: Survey[];
  createdAt: string;
  updatedAt: string;
}

export default function HoleManagementPage({ params }: { params: { id: string } }) {
  const router = useRouter();
  const [hole, setHole] = useState<HoleData | null>(null);
  const [collar, setCollar] = useState<CollarData | null>(null);
  const [target, setTarget] = useState<TargetData | null>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);

  const [activeTab, setActiveTab] = useState<"details" | "chartOptions">("details");
  const [mobileTab, setMobileTab] = useState<"3d" | "details">("3d");
  const [showEditCollarModal, setShowEditCollarModal] = useState(false);
  const [showEditTargetModal, setShowEditTargetModal] = useState(false);

  // Quick edit states
  const [collarN, setCollarN] = useState("");
  const [collarE, setCollarE] = useState("");
  const [collarElev, setCollarElev] = useState("0");
  const [targetTVD, setTargetTVD] = useState("");
  const [targetN, setTargetN] = useState("");
  const [targetE, setTargetE] = useState("");

  // Chart layer options
  const [showPlan, setShowPlan] = useState(true);
  const [showSurveys, setShowSurveys] = useState(true);
  const [showSurface, setShowSurface] = useState(true);

  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

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
      setSurveys(sData.surveys || hData.hole?.surveys || []);
      setCollar(cData.collar || null);
      setTarget(tData.target || null);

      if (cData.collar) {
        setCollarN(String(cData.collar.northing));
        setCollarE(String(cData.collar.easting));
        setCollarElev(String(cData.collar.elevation ?? 0));
      }
      if (tData.target) {
        setTargetTVD(String(tData.target.tvd));
        setTargetN(String(tData.target.northing));
        setTargetE(String(tData.target.easting));
      }
    } catch {
      setError("Failed to load hole management data");
    } finally {
      setIsLoading(false);
    }
  };

  const saveCollar = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setError("");
    setSuccess("");
    try {
      const res = await fetch(`/api/holes/${params.id}/collar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          northing: parseFloat(collarN),
          easting: parseFloat(collarE),
          elevation: parseFloat(collarElev || "0"),
        }),
      });
      if (!res.ok) throw new Error("Save collar failed");
      setSuccess("Collar coordinates updated successfully");
      setShowEditCollarModal(false);
      await fetchData();
    } catch (err: any) {
      setError(err.message || "Failed to save collar");
    } finally {
      setIsSaving(false);
    }
  };

  const saveTarget = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setError("");
    setSuccess("");
    try {
      const res = await fetch(`/api/holes/${params.id}/target`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          tvd: parseFloat(targetTVD),
          northing: parseFloat(targetN),
          easting: parseFloat(targetE),
        }),
      });
      if (!res.ok) throw new Error("Save target failed");
      setSuccess("Target coordinates updated successfully");
      setShowEditTargetModal(false);
      await fetchData();
    } catch (err: any) {
      setError(err.message || "Failed to save target");
    } finally {
      setIsSaving(false);
    }
  };

  const deleteHole = async () => {
    if (!confirm("Are you sure you want to delete this hole and all its survey data? This cannot be undone.")) {
      return;
    }
    try {
      const res = await fetch(`/api/holes/${params.id}`, { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      router.push("/holes");
    } catch {
      setError("Failed to delete hole");
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="text-center">
          <div className="w-10 h-10 border-4 border-blue-600 border-t-transparent rounded-full animate-spin mx-auto mb-3"></div>
          <p className="text-gray-600 font-medium">Loading Hole Management...</p>
        </div>
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

  const createdDateStr = hole.createdAt
    ? new Date(hole.createdAt).toLocaleDateString("en-GB")
    : "—";

  return (
    <div className="min-h-screen bg-slate-100 flex flex-col font-sans select-none">
      {/* Top Header Bar matching Screenshot 1 */}
      <header className="bg-white border-b border-gray-200 px-6 py-2.5 flex items-center justify-between shadow-xs">
        <div className="flex items-center gap-3">
          <span className="text-2xl" role="img" aria-label="Hole Management">📕</span>
          <h1 className="text-xl font-bold text-gray-900 tracking-tight">
            Hole Management: <span className="text-blue-600 font-semibold">{hole.name || hole.holeId}</span>
          </h1>
          <span
            className={`ml-2 px-2.5 py-0.5 rounded-full text-xs font-semibold uppercase tracking-wider ${
              hole.status === "completed"
                ? "bg-green-100 text-green-700"
                : hole.status === "drilling"
                ? "bg-blue-100 text-blue-700"
                : "bg-amber-100 text-amber-700"
            }`}
          >
            {hole.status}
          </span>
        </div>

        <div className="flex items-center gap-3">
          <button
            onClick={deleteHole}
            className="text-xs text-red-600 hover:text-red-800 font-medium px-2 py-1 rounded hover:bg-red-50 transition"
          >
            Delete Hole
          </button>
        </div>
      </header>

      {/* Status messages */}
      {error && (
        <div className="bg-red-50 border-b border-red-200 px-6 py-2 text-xs text-red-700 font-medium">
          {error}
        </div>
      )}
      {success && (
        <div className="bg-green-50 border-b border-green-200 px-6 py-2 text-xs text-green-700 font-medium">
          {success}
        </div>
      )}

      {/* Main Body */}
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
          onClick={() => setMobileTab("details")}
          className={`flex-1 py-2.5 text-xs font-bold transition flex items-center justify-center gap-1.5 ${
            mobileTab === "details"
              ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/40"
              : "text-gray-500 hover:text-gray-700"
          }`}
        >
          <span>📋</span>
          <span>Hole Details</span>
        </button>
      </div>

      <div className="flex-1 flex flex-col lg:flex-row overflow-hidden">
        {/* Left Toolbar (matching Screenshot 1 glossy button strip, scrollable horizontally on mobile) */}
        <div className="w-full lg:w-48 bg-slate-900 p-2.5 lg:p-3 flex flex-row lg:flex-col gap-2 shrink-0 border-b lg:border-b-0 lg:border-r border-slate-800 overflow-x-auto">
          <Link
            href="/"
            className="flex items-center gap-2.5 px-3 py-2.5 rounded-lg bg-blue-700 hover:bg-blue-800 text-white text-xs font-bold shadow-sm transition"
          >
            <span className="text-base">🏠</span>
            <div className="leading-tight">
              <div>Back</div>
              <div className="text-[10px] text-blue-200 font-normal">Welcome Screen</div>
            </div>
          </Link>

          <Link
            href={`/holes/${params.id}/edit`}
            className="flex items-center gap-2.5 px-3 py-2.5 rounded-lg bg-blue-900/90 hover:bg-blue-800 text-white text-xs font-semibold shadow-sm transition border border-blue-700/50"
          >
            <span className="text-base">📝</span>
            <span>Edit Details</span>
          </Link>

          <Link
            href={`/holes/${params.id}/survey-entry`}
            className="flex items-center gap-2.5 px-3 py-2.5 rounded-lg bg-blue-900/90 hover:bg-blue-800 text-white text-xs font-semibold shadow-sm transition border border-blue-700/50"
          >
            <span className="text-base">📋</span>
            <span>Edit Survey Data</span>
          </Link>

          <Link
            href={`/import?holeId=${params.id}`}
            className="flex items-center gap-2.5 px-3 py-2.5 rounded-lg bg-blue-900/90 hover:bg-blue-800 text-white text-xs font-semibold shadow-sm transition border border-blue-700/50"
          >
            <span className="text-base">📥</span>
            <span>Import Survey Data</span>
          </Link>

          <div className="hidden lg:block my-2 border-t border-slate-800" />

          {/* Prominent Green Hole Navigation Button matching Screenshot 1 */}
          <Link
            href={`/holes/${params.id}/navigation`}
            className="flex items-center gap-2.5 px-3 py-3 rounded-lg bg-gradient-to-r from-emerald-600 to-green-600 hover:from-emerald-500 hover:to-green-500 text-white text-xs font-bold shadow-md hover:shadow-lg transition transform hover:-translate-y-0.5 border border-emerald-400"
          >
            <div className="w-6 h-6 rounded-full bg-emerald-950/60 flex items-center justify-center shrink-0 border border-emerald-300">
              <span className="text-xs">🎯</span>
            </div>
            <div className="leading-tight">
              <div className="text-[13px] font-bold">Hole Navigation</div>
              <div className="text-[10px] text-emerald-100 font-normal">Navi work functions</div>
            </div>
          </Link>

          <div className="hidden lg:block my-2 border-t border-slate-800" />

          {/* Export Options */}
          <div className="hidden lg:block text-[10px] uppercase font-bold text-slate-400 px-1 pt-1 tracking-wider">
            Export Data
          </div>

          <a
            href={`/api/holes/${params.id}/export/ezn`}
            download
            className="flex items-center gap-2 px-3 py-2 rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-200 text-xs font-medium transition border border-slate-700"
            title="Download original SphereIQ / Ezy-Nav borehole file format (.ezn)"
          >
            <span>💾</span>
            <span>Export .ezn File</span>
          </a>

          <a
            href={`/api/holes/${params.id}/export/csv`}
            download
            className="flex items-center gap-2 px-3 py-2 rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-200 text-xs font-medium transition border border-slate-700"
            title="Download survey stations as CSV"
          >
            <span>📊</span>
            <span>Export Surveys (CSV)</span>
          </a>
        </div>

        {/* Center Details Panel matching Screenshot 1 */}
        <div className={`w-full lg:w-80 bg-white border-r border-gray-200 flex flex-col shrink-0 overflow-y-auto ${mobileTab === "details" ? "block" : "hidden lg:flex"}`}>
          {/* Tabs */}
          <div className="flex border-b border-gray-200 bg-gray-50 text-xs font-semibold">
            <button
              onClick={() => setActiveTab("details")}
              className={`flex-1 py-2.5 text-center border-b-2 transition ${
                activeTab === "details"
                  ? "border-blue-600 text-blue-700 bg-white"
                  : "border-transparent text-gray-500 hover:text-gray-800"
              }`}
            >
              Hole Details
            </button>
            <button
              onClick={() => setActiveTab("chartOptions")}
              className={`flex-1 py-2.5 text-center border-b-2 transition ${
                activeTab === "chartOptions"
                  ? "border-blue-600 text-blue-700 bg-white"
                  : "border-transparent text-gray-500 hover:text-gray-800"
              }`}
            >
              Chart Options
            </button>
          </div>

          {activeTab === "details" ? (
            <div className="p-4 space-y-4 text-xs">
              {/* Identification Box */}
              <div className="border border-gray-200 rounded p-3 bg-gray-50/50">
                <h3 className="font-bold text-gray-900 border-b border-gray-200 pb-1 mb-2 text-[13px]">
                  Identification
                </h3>
                <div className="space-y-1.5 text-gray-700">
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Hole ID:</span>
                    <span className="font-semibold text-gray-900">{hole.holeId}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">RigID:</span>
                    <span className="font-semibold text-gray-900">{hole.rigId}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Location:</span>
                    <span>{hole.location || "test"}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Area:</span>
                    <span>{hole.area || "test"}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Client:</span>
                    <span>{hole.client || "test"}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Surveying Contractor:</span>
                    <span>{hole.surveyingContractor || "test"}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="font-medium text-gray-500">Drilling Contractor:</span>
                    <span>{hole.drillingContractor || "test"}</span>
                  </div>
                  <div className="flex justify-between pt-1 border-t border-gray-100">
                    <span className="font-medium text-gray-500">Created Date:</span>
                    <span>{createdDateStr}</span>
                  </div>
                  <div className="flex justify-between pt-1">
                    <span className="font-medium text-gray-500">Created By:</span>
                    <span className="font-semibold text-blue-700">
                      {hole.user?.name || hole.user?.email || "Unknown"}
                    </span>
                  </div>
                </div>
              </div>

              {/* Collar (m) Box */}
              <div className="border border-gray-200 rounded p-3 bg-gray-50/50">
                <div className="flex items-center justify-between border-b border-gray-200 pb-1 mb-2">
                  <h3 className="font-bold text-gray-900 text-[13px]">Collar (m)</h3>
                  <button
                    onClick={() => setShowEditCollarModal(true)}
                    className="text-[11px] text-blue-600 hover:text-blue-800 font-semibold"
                  >
                    Edit
                  </button>
                </div>
                {collar ? (
                  <div className="space-y-1 text-gray-700">
                    <div className="flex justify-between">
                      <span className="font-medium text-gray-500">Easting:</span>
                      <span className="font-mono font-semibold">{collar.easting}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="font-medium text-gray-500">Northing:</span>
                      <span className="font-mono font-semibold">{collar.northing}</span>
                    </div>
                  </div>
                ) : (
                  <div className="text-gray-400 italic py-1 flex items-center justify-between">
                    <span>Not Set</span>
                    <button
                      onClick={() => setShowEditCollarModal(true)}
                      className="px-2 py-0.5 bg-blue-50 text-blue-600 rounded text-[11px] font-semibold"
                    >
                      + Set Collar
                    </button>
                  </div>
                )}
              </div>

              {/* Target (m) Box */}
              <div className="border border-gray-200 rounded p-3 bg-gray-50/50">
                <div className="flex items-center justify-between border-b border-gray-200 pb-1 mb-2">
                  <h3 className="font-bold text-gray-900 text-[13px]">Target (m)</h3>
                  <button
                    onClick={() => setShowEditTargetModal(true)}
                    className="text-[11px] text-blue-600 hover:text-blue-800 font-semibold"
                  >
                    Edit
                  </button>
                </div>
                {target ? (
                  <div className="space-y-1 text-gray-700">
                    <div className="flex justify-between">
                      <span className="font-medium text-gray-500">Easting:</span>
                      <span className="font-mono font-semibold">{target.easting}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="font-medium text-gray-500">Northing:</span>
                      <span className="font-mono font-semibold">{target.northing}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="font-medium text-gray-500">TVD:</span>
                      <span className="font-mono font-semibold">{target.tvd}</span>
                    </div>
                  </div>
                ) : (
                  <div className="text-gray-400 italic py-1 flex items-center justify-between">
                    <span>Not Set</span>
                    <button
                      onClick={() => setShowEditTargetModal(true)}
                      className="px-2 py-0.5 bg-blue-50 text-blue-600 rounded text-[11px] font-semibold"
                    >
                      + Set Target
                    </button>
                  </div>
                )}
              </div>

              {/* Survey Summary */}
              <div className="border border-gray-200 rounded p-3 bg-blue-50/40">
                <div className="flex items-center justify-between mb-1">
                  <span className="font-bold text-gray-900 text-[12px]">Surveys Loaded</span>
                  <span className="font-bold text-blue-600 bg-blue-100 px-2 py-0.5 rounded-full text-[11px]">
                    {surveys.length} stations
                  </span>
                </div>
                {surveys.length > 0 && (
                  <p className="text-[11px] text-gray-600">
                    End MD: <span className="font-bold">{surveys[surveys.length - 1].measuredDepth}m</span> · TVD:{" "}
                    <span className="font-bold">
                      {surveys[surveys.length - 1].trueVerticalDepth !== null
                        ? Number(surveys[surveys.length - 1].trueVerticalDepth).toFixed(2)
                        : "—"}
                      m
                    </span>
                  </p>
                )}
              </div>
            </div>
          ) : (
            /* Chart Options Tab */
            <div className="p-4 space-y-3 text-xs">
              <h3 className="font-bold text-gray-900 border-b border-gray-200 pb-1 mb-2 text-[13px]">
                3D Display Options
              </h3>

              <label className="flex items-center gap-2 cursor-pointer text-gray-700">
                <input
                  type="checkbox"
                  checked={showPlan}
                  onChange={(e) => setShowPlan(e.target.checked)}
                  className="rounded text-blue-600"
                />
                <span>Hole Plan Line (Black)</span>
              </label>

              <label className="flex items-center gap-2 cursor-pointer text-gray-700">
                <input
                  type="checkbox"
                  checked={showSurveys}
                  onChange={(e) => setShowSurveys(e.target.checked)}
                  className="rounded text-green-600"
                />
                <span>Survey Data Points (Green)</span>
              </label>

              <label className="flex items-center gap-2 cursor-pointer text-gray-700">
                <input
                  type="checkbox"
                  checked={showSurface}
                  onChange={(e) => setShowSurface(e.target.checked)}
                  className="rounded text-blue-400"
                />
                <span>Surface Ground Plane (TVD 0)</span>
              </label>

              <div className="pt-3 border-t border-gray-200 text-gray-500 text-[11px]">
                <p className="font-medium text-gray-700 mb-1">Interactive Navigation:</p>
                <ul className="list-disc pl-4 space-y-1">
                  <li>Left click + drag to rotate</li>
                  <li>Right click + drag to pan</li>
                  <li>Scroll wheel to zoom in / out</li>
                  <li>Hover spheres to inspect coordinates</li>
                </ul>
              </div>
            </div>
          )}
        </div>

        {/* Right 3D Chart View matching Screenshot 1 */}
        <div className={`flex-1 bg-white relative flex flex-col min-h-[420px] lg:min-h-[550px] overflow-hidden ${mobileTab === "3d" ? "block" : "hidden lg:flex"}`}>
          {/* Top Right Legend (Desktop matching) */}
          <div className="absolute top-3 right-4 z-10 bg-white/90 backdrop-blur-xs border border-gray-200 rounded p-2 text-xs space-y-1 shadow-sm">
            <div className="flex items-center gap-2">
              <span className="w-3 h-3 rounded-full bg-fuchsia-600 inline-block border border-black/20" />
              <span className="text-gray-800 font-medium">Collar</span>
            </div>
            <div className="flex items-center gap-2">
              <span className="w-3 h-3 rounded-full bg-orange-600 inline-block border border-black/20" />
              <span className="text-gray-800 font-medium">Target</span>
            </div>
            <div className="flex items-center gap-2">
              <span className="w-5 h-0.5 bg-black inline-block" />
              <span className="text-gray-800 font-medium">Hole Plan</span>
            </div>
            <div className="flex items-center gap-2">
              <span className="w-2.5 h-2.5 rounded-full bg-green-500 inline-block" />
              <span className="text-gray-800 font-medium">Survey Data</span>
            </div>
          </div>

          {/* WebGL 3D Canvas */}
          <div className="flex-1 w-full h-full min-h-[500px]">
            <Borehole3DChart
              collar={collar ? { northing: collar.northing, easting: collar.easting, elevation: collar.elevation } : null}
              target={target ? { tvd: target.tvd, northing: target.northing, easting: target.easting } : null}
              surveys={surveys}
              className="w-full h-full min-h-[550px]"
            />
          </div>

          {/* Bottom Banner matching Screenshot 1 verbatim */}
          <div className="bg-white/95 border-t border-gray-200 px-4 py-1.5 flex items-center justify-between text-xs z-10">
            {/* Sphere IQ Logo watermark */}
            <div className="flex items-center gap-2">
              <div className="w-6 h-6 rounded-full bg-slate-900 border border-blue-400 flex items-center justify-center text-[10px] text-blue-300 font-bold">
                SQ
              </div>
              <div className="leading-tight">
                <span className="font-bold text-slate-800 text-xs">Sphere IQ</span>
                <span className="block text-[9px] text-gray-500 uppercase tracking-wider">3D Intelligence Platform</span>
              </div>
            </div>

            {/* Note verbatim from Screenshot 1 */}
            <div className="text-red-600 font-semibold text-center text-xs tracking-tight">
              Note: Scaling is not consistant across axis - The hole path may appear exaggerated!
            </div>

            <div className="text-[11px] text-gray-400 font-mono">
              3D WebGL Engine
            </div>
          </div>
        </div>
      </div>

      {/* Edit Collar Modal */}
      {showEditCollarModal && (
        <div className="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
          <div className="bg-white rounded-xl shadow-xl max-w-sm w-full p-6">
            <h3 className="text-lg font-bold text-gray-900 mb-4">Edit Collar Coordinates</h3>
            <form onSubmit={saveCollar} className="space-y-4">
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Northing (m)
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={collarN}
                  onChange={(e) => setCollarN(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Easting (m)
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={collarE}
                  onChange={(e) => setCollarE(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Elevation (m)
                </label>
                <input
                  type="number"
                  step="any"
                  value={collarElev}
                  onChange={(e) => setCollarElev(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div className="flex gap-2 justify-end pt-2">
                <button
                  type="button"
                  onClick={() => setShowEditCollarModal(false)}
                  className="px-4 py-2 border border-gray-300 text-gray-700 rounded text-xs font-semibold hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  disabled={isSaving}
                  className="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded text-xs font-semibold disabled:opacity-50"
                >
                  {isSaving ? "Saving..." : "Save Collar"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Edit Target Modal */}
      {showEditTargetModal && (
        <div className="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
          <div className="bg-white rounded-xl shadow-xl max-w-sm w-full p-6">
            <h3 className="text-lg font-bold text-gray-900 mb-4">Edit Target Coordinates</h3>
            <form onSubmit={saveTarget} className="space-y-4">
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Northing (m)
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={targetN}
                  onChange={(e) => setTargetN(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Easting (m)
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={targetE}
                  onChange={(e) => setTargetE(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  TVD (m)
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={targetTVD}
                  onChange={(e) => setTargetTVD(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded text-sm font-mono focus:ring-2 focus:ring-blue-500"
                />
              </div>
              <div className="flex gap-2 justify-end pt-2">
                <button
                  type="button"
                  onClick={() => setShowEditTargetModal(false)}
                  className="px-4 py-2 border border-gray-300 text-gray-700 rounded text-xs font-semibold hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  disabled={isSaving}
                  className="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded text-xs font-semibold disabled:opacity-50"
                >
                  {isSaving ? "Saving..." : "Save Target"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
