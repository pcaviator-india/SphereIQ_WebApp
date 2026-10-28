"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

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
  collar: CollarData | null;
  target: TargetData | null;
  surveys: Survey[];
  createdAt: string;
  updatedAt: string;
}

export default function HoleDetailPage({ params }: { params: { id: string } }) {
  const router = useRouter();
  const [hole, setHole] = useState<HoleData | null>(null);
  const [collar, setCollar] = useState<CollarData | null>(null);
  const [target, setTarget] = useState<TargetData | null>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);
  const [collarN, setCollarN] = useState("");
  const [collarE, setCollarE] = useState("");
  const [collarElev, setCollarElev] = useState("");
  const [targetTVD, setTargetTVD] = useState("");
  const [targetN, setTargetN] = useState("");
  const [targetE, setTargetE] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const [hRes, cRes, tRes] = await Promise.all([
        fetch("/api/holes/" + params.id),
        fetch("/api/holes/" + params.id + "/collar"),
        fetch("/api/holes/" + params.id + "/target"),
      ]);
      const hData = await hRes.json();
      const cData = await cRes.json();
      const tData = await tRes.json();
      setHole(hData.hole);
      setSurveys(hData.hole?.surveys ?? []);
      setCollar(cData.collar ?? null);
      setTarget(tData.target ?? null);
      if (cData.collar) {
        setCollarN(String(cData.collar.northing));
        setCollarE(String(cData.collar.easting));
        setCollarElev(String(cData.collar.elevation));
      }
      if (tData.target) {
        setTargetTVD(String(tData.target.tvd));
        setTargetN(String(tData.target.northing));
        setTargetE(String(tData.target.easting));
      }
    } catch {
      setError("Failed to load data");
    } finally {
      setIsLoading(false);
    }
  };

  const saveCollar = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    try {
      const res = await fetch("/api/holes/" + params.id + "/collar", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          northing: parseFloat(collarN),
          easting: parseFloat(collarE),
          elevation: parseFloat(collarElev),
        }),
      });
      if (!res.ok) throw new Error("Save failed");
      setSuccess("Collar saved");
      await fetchData();
    } catch {
      setError("Failed to save collar");
    }
  };

  const saveTarget = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    try {
      const res = await fetch("/api/holes/" + params.id + "/target", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          tvd: parseFloat(targetTVD),
          northing: parseFloat(targetN),
          easting: parseFloat(targetE),
        }),
      });
      if (!res.ok) throw new Error("Save failed");
      setSuccess("Target saved");
      await fetchData();
    } catch {
      setError("Failed to save target");
    }
  };

  const deleteHole = async () => {
    if (!confirm("Delete this hole and all its data? This cannot be undone."))
      return;
    try {
      const res = await fetch("/api/holes/" + params.id, { method: "DELETE" });
      if (!res.ok) throw new Error();
      router.push("/holes");
    } catch {
      setError("Delete failed");
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

  const statusColor =
    hole.status === "planned"
      ? "bg-yellow-100 text-yellow-700"
      : hole.status === "drilling"
      ? "bg-blue-100 text-blue-700"
      : hole.status === "completed"
      ? "bg-green-100 text-green-700"
      : "bg-gray-100 text-gray-700";

  const lastSurvey =
    surveys.length > 0 ? surveys[surveys.length - 1] : null;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-6xl mx-auto">
          {/* Header */}
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link
                href="/holes"
                className="text-sm text-blue-600 hover:underline mb-2 inline-block"
              >
                ← Back to holes
              </Link>
              <div className="flex items-center gap-3">
                <h1 className="text-3xl font-bold text-gray-900">
                  {hole.name}
                </h1>
                <span
                  className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${statusColor}`}
                >
                  {hole.status}
                </span>
              </div>
              <p className="text-gray-600 mt-1">
                Hole ID: {hole.holeId} · Rig: {hole.rigId}
              </p>
            </div>
            <div className="flex gap-2">
              <Link
                href={"/holes/" + params.id + "/edit"}
                className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition text-sm"
              >
                Edit Details
              </Link>
              <button
                onClick={deleteHole}
                className="px-4 py-2 border border-red-300 text-red-600 rounded-lg font-medium hover:bg-red-50 transition text-sm"
              >
                Delete Hole
              </button>
            </div>
          </div>

          {error && (
            <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">
              {error}
            </div>
          )}
          {success && (
            <div className="mb-4 p-3 rounded-md bg-green-50 border border-green-100 text-sm text-green-600">
              {success}
            </div>
          )}

          {/* Info Cards */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
            <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-gray-400 uppercase mb-1">
                Location
              </p>
              <p className="text-gray-900 font-medium">
                {hole.location || "—"}
              </p>
              {hole.area && (
                <p className="text-sm text-gray-500">Area: {hole.area}</p>
              )}
            </div>
            <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-gray-400 uppercase mb-1">
                Drilling Contractor
              </p>
              <p className="text-gray-900 font-medium">
                {hole.drillingContractor || "—"}
              </p>
            </div>
            <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-200">
              <p className="text-xs font-semibold text-gray-400 uppercase mb-1">
                Surveying Contractor
              </p>
              <p className="text-gray-900 font-medium">
                {hole.surveyingContractor || "—"}
              </p>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
            {/* Collar */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">Collar</h2>
              {collar && (
                <div className="grid grid-cols-3 gap-4 mb-4">
                  <div>
                    <p className="text-xs text-gray-400 uppercase">Northing</p>
                    <p className="text-gray-900 font-medium">
                      {String(collar.northing)}
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-gray-400 uppercase">Easting</p>
                    <p className="text-gray-900 font-medium">
                      {String(collar.easting)}
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-gray-400 uppercase">Elevation</p>
                    <p className="text-gray-900 font-medium">
                      {String(collar.elevation)}
                    </p>
                  </div>
                </div>
              )}
              <form onSubmit={saveCollar} className="space-y-3">
                <div className="grid grid-cols-3 gap-3">
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      Northing
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={collarN}
                      onChange={(e) => setCollarN(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      Easting
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={collarE}
                      onChange={(e) => setCollarE(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      Elevation
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={collarElev}
                      onChange={(e) => setCollarElev(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                </div>
                <button
                  type="submit"
                  className="w-full px-4 py-2 bg-blue-600 text-white text-sm rounded-lg font-medium hover:bg-blue-700 transition"
                >
                  {collar ? "Update Collar" : "Save Collar"}
                </button>
              </form>
            </div>

            {/* Target */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">Target</h2>
              {target && (
                <div className="grid grid-cols-3 gap-4 mb-4">
                  <div>
                    <p className="text-xs text-gray-400 uppercase">TVD</p>
                    <p className="text-gray-900 font-medium">
                      {String(target.tvd)} m
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-gray-400 uppercase">Northing</p>
                    <p className="text-gray-900 font-medium">
                      {String(target.northing)}
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-gray-400 uppercase">Easting</p>
                    <p className="text-gray-900 font-medium">
                      {String(target.easting)}
                    </p>
                  </div>
                </div>
              )}
              <form onSubmit={saveTarget} className="space-y-3">
                <div className="grid grid-cols-3 gap-3">
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      TVD (m)
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={targetTVD}
                      onChange={(e) => setTargetTVD(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      Northing
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={targetN}
                      onChange={(e) => setTargetN(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                  <div>
                    <label className="block text-xs font-medium text-gray-500 mb-1">
                      Easting
                    </label>
                    <input
                      type="number"
                      step="any"
                      required
                      value={targetE}
                      onChange={(e) => setTargetE(e.target.value)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                  </div>
                </div>
                <button
                  type="submit"
                  className="w-full px-4 py-2 bg-blue-600 text-white text-sm rounded-lg font-medium hover:bg-blue-700 transition"
                >
                  {target ? "Update Target" : "Save Target"}
                </button>
              </form>
            </div>
          </div>

          {/* Surveys Summary */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <div className="flex items-center justify-between mb-4">
              <h2 className="text-lg font-bold text-gray-900">Surveys</h2>
              <Link
                href={"/holes/" + params.id + "/survey-entry"}
                className="px-4 py-2 bg-blue-600 text-white text-sm rounded-lg font-medium hover:bg-blue-700 transition"
              >
                Manage Surveys
              </Link>
            </div>
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              <div>
                <p className="text-xs text-gray-400 uppercase">Total Surveys</p>
                <p className="text-2xl font-bold text-gray-900">
                  {surveys.length}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-400 uppercase">Last MD</p>
                <p className="text-2xl font-bold text-gray-900">
                  {lastSurvey
                    ? String(lastSurvey.measuredDepth) + " m"
                    : "—"}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-400 uppercase">Last TVD</p>
                <p className="text-2xl font-bold text-gray-900">
                  {lastSurvey?.trueVerticalDepth
                    ? String(lastSurvey.trueVerticalDepth) + " m"
                    : "—"}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-400 uppercase">Last Inc / Az</p>
                <p className="text-2xl font-bold text-gray-900">
                  {lastSurvey
                    ? String(lastSurvey.inclination) +
                      "° / " +
                      String(lastSurvey.azimuth) +
                      "°"
                    : "—"}
                </p>
              </div>
            </div>
          </div>

          {/* Quick Actions */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <h2 className="text-lg font-bold text-gray-900 mb-4">
              Quick Actions
            </h2>
            <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-3">
              <Link
                href={"/holes/" + params.id + "/survey-entry"}
                className="px-4 py-3 bg-blue-50 text-blue-700 rounded-lg font-medium hover:bg-blue-100 transition text-center text-sm border border-blue-200"
              >
                Survey Entry
              </Link>
              <Link
                href={"/holes/" + params.id + "/charts"}
                className="px-4 py-3 bg-purple-50 text-purple-700 rounded-lg font-medium hover:bg-purple-100 transition text-center text-sm border border-purple-200"
              >
                Charts
              </Link>
              <Link
                href={"/trend-analysis/" + params.id}
                className="px-4 py-3 bg-green-50 text-green-700 rounded-lg font-medium hover:bg-green-100 transition text-center text-sm border border-green-200"
              >
                Trend Analysis
              </Link>
              <Link
                href={"/extrapolate/" + params.id}
                className="px-4 py-3 bg-amber-50 text-amber-700 rounded-lg font-medium hover:bg-amber-100 transition text-center text-sm border border-amber-200"
              >
                Extrapolate
              </Link>
              <Link
                href={"/build-to-target?holeId=" + params.id}
                className="px-4 py-3 bg-red-50 text-red-700 rounded-lg font-medium hover:bg-red-100 transition text-center text-sm border border-red-200"
              >
                Build Target
              </Link>
              <Link
                href={"/holes/" + params.id + "/sidetrack"}
                className="px-4 py-3 bg-indigo-50 text-indigo-700 rounded-lg font-medium hover:bg-indigo-100 transition text-center text-sm border border-indigo-200"
              >
                SideTrack
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
