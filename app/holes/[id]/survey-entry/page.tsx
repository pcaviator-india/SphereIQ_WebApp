"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

interface Survey {
  id: string;
  measuredDepth: number | string;
  inclination: number | string;
  azimuth: number | string;
  trueVerticalDepth: number | string | null;
  northing: number | string | null;
  easting: number | string | null;
  verticalSection: number | string | null;
}

export default function SurveyEntryPage({ params }: { params: { id: string } }) {
  const [hole, setHole] = useState<any>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);
  const [md, setMd] = useState("");
  const [inc, setInc] = useState("");
  const [az, setAz] = useState("");
  const [editId, setEditId] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    fetchData();
  }, [params.id]);

  const fetchData = async () => {
    try {
      const [hRes, sRes] = await Promise.all([
        fetch(`/api/holes/${params.id}`),
        fetch(`/api/holes/${params.id}/surveys`),
      ]);
      const hData = await hRes.json();
      const sData = await sRes.json();
      setHole(hData.hole);
      setSurveys(sData.surveys || []);
    } catch {
      setError("Failed to load borehole survey data");
    } finally {
      setIsLoading(false);
    }
  };

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");

    const cleanMD = typeof md === "string" ? md.replace(",", ".").trim() : String(md);
    const cleanInc = typeof inc === "string" ? inc.replace(",", ".").trim() : String(inc);
    const cleanAz = typeof az === "string" ? az.replace(",", ".").trim() : String(az);

    const parsedMD = parseFloat(cleanMD);
    const parsedInc = parseFloat(cleanInc);
    const parsedAz = parseFloat(cleanAz);

    if (isNaN(parsedMD) || isNaN(parsedInc) || isNaN(parsedAz)) {
      setError("Please enter valid numeric values for MD, Inclination, and Azimuth");
      return;
    }

    if (parsedMD < 0) {
      setError("Measured depth cannot be negative");
      return;
    }

    setIsSaving(true);

    try {
      const url = editId
        ? `/api/holes/${params.id}/surveys/${editId}`
        : `/api/holes/${params.id}/surveys`;

      const res = await fetch(url, {
        method: editId ? "PUT" : "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          measuredDepth: parsedMD,
          inclination: parsedInc,
          azimuth: parsedAz,
        }),
      });

      const data = await res.json().catch(() => ({}));

      if (!res.ok) {
        throw new Error(data.error || "Save failed");
      }

      setSuccess(editId ? "Survey station updated successfully" : "Survey station added successfully");
      setMd("");
      setInc("");
      setAz("");
      setEditId(null);
      await fetchData();
    } catch (err: any) {
      setError(err.message || "Failed to save survey station");
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (!confirm("Are you sure you want to delete this survey station?")) return;
    setError("");
    setSuccess("");

    try {
      const res = await fetch(`/api/holes/${params.id}/surveys/${id}`, {
        method: "DELETE",
      });
      const data = await res.json().catch(() => ({}));
      if (!res.ok) throw new Error(data.error || "Delete failed");

      setSuccess("Survey station deleted");
      if (editId === id) {
        setEditId(null);
        setMd("");
        setInc("");
        setAz("");
      }
      await fetchData();
    } catch (err: any) {
      setError(err.message || "Failed to delete survey");
    }
  };

  const handleEdit = (s: Survey) => {
    setEditId(s.id);
    setMd(String(s.measuredDepth));
    setInc(String(s.inclination));
    setAz(String(s.azimuth));
    setError("");
    setSuccess("");
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  const handleCancel = () => {
    setEditId(null);
    setMd("");
    setInc("");
    setAz("");
    setError("");
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading survey records...</p>
      </div>
    );
  }

  if (!hole) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">Hole not found</h1>
          <Link href="/holes" className="text-blue-600 hover:underline">
            Back to holes
          </Link>
        </div>
      </div>
    );
  }

  const isEdit = !!editId;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-5xl mx-auto">
          <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-6 gap-4">
            <div>
              <Link href={`/holes/${params.id}`} className="text-sm text-blue-600 hover:underline mb-1 inline-block">
                ← Back to {hole.name}
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Manage Survey Stations</h1>
              <p className="text-gray-600 text-sm">
                Borehole ID: {hole.holeId} · Rig: {hole.rigId}
              </p>
            </div>
            <div className="flex gap-2">
              <Link
                href={`/import`}
                className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg text-sm font-medium hover:bg-gray-50 transition"
              >
                Import CSV
              </Link>
              <Link
                href={`/holes/${params.id}/charts`}
                className="px-4 py-2 bg-purple-600 text-white rounded-lg text-sm font-medium hover:bg-purple-700 transition"
              >
                View Charts
              </Link>
            </div>
          </div>

          {error && (
            <div className="mb-4 p-4 rounded-xl bg-red-50 border border-red-200 text-sm text-red-600">
              {error}
            </div>
          )}

          {success && (
            <div className="mb-4 p-4 rounded-xl bg-green-50 border border-green-200 text-sm text-green-700">
              {success}
            </div>
          )}

          {/* Form */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-lg font-bold text-gray-900 mb-4">
              {isEdit ? "Edit Survey Station" : "Add Survey Station"}
            </h2>
            <form onSubmit={handleSave} className="grid grid-cols-1 md:grid-cols-4 gap-4 items-end">
              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Measured Depth (m) *
                </label>
                <input
                  type="number"
                  step="any"
                  min="0"
                  required
                  placeholder="e.g. 100.5"
                  value={md}
                  onChange={(e) => setMd(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Inclination / Dip (°) *
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  placeholder="e.g. -72.6"
                  value={inc}
                  onChange={(e) => setInc(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                  Azimuth / Bearing (°) *
                </label>
                <input
                  type="number"
                  step="any"
                  min="0"
                  max="360"
                  required
                  placeholder="e.g. 277.0"
                  value={az}
                  onChange={(e) => setAz(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
                />
              </div>

              <div className="flex gap-2">
                <button
                  type="submit"
                  disabled={isSaving}
                  className={`flex-1 px-4 py-2.5 rounded-lg text-sm font-medium transition text-white ${
                    isEdit
                      ? "bg-green-600 hover:bg-green-700"
                      : "bg-blue-600 hover:bg-blue-700"
                  } disabled:opacity-50`}
                >
                  {isSaving ? "Saving..." : isEdit ? "Update Station" : "+ Add Station"}
                </button>
                {isEdit && (
                  <button
                    type="button"
                    onClick={handleCancel}
                    className="px-4 py-2.5 rounded-lg text-sm text-gray-700 bg-gray-100 hover:bg-gray-200 transition font-medium"
                  >
                    Cancel
                  </button>
                )}
              </div>
            </form>
          </div>

          {/* Survey Stations Table */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <div className="flex items-center justify-between mb-4">
              <h2 className="text-lg font-bold text-gray-900">
                Survey Stations ({surveys.length})
              </h2>
              {surveys.length > 0 && (
                <span className="text-xs text-gray-400">
                  Calculated automatically using minimum curvature
                </span>
              )}
            </div>

            {surveys.length === 0 ? (
              <div className="py-12 text-center text-gray-400">
                <p>No survey stations recorded yet.</p>
                <p className="text-xs text-gray-400 mt-1">
                  Use the form above to add your first survey, or import a CSV file.
                </p>
              </div>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full text-left border-collapse text-sm">
                  <thead>
                    <tr className="border-b border-gray-200 bg-gray-50">
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">MD (m)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">Inc (°)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">Az (°)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">TVD (m)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">North (m)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">East (m)</th>
                      <th className="py-2.5 px-3 text-xs font-semibold text-gray-500 uppercase">Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {surveys.map((s) => {
                      const isCurrent = editId === s.id;
                      return (
                        <tr
                          key={s.id}
                          className={`border-b border-gray-100 transition ${
                            isCurrent ? "bg-blue-50" : "hover:bg-gray-50"
                          }`}
                        >
                          <td className="py-2.5 px-3 font-medium text-gray-900">
                            {Number(s.measuredDepth).toFixed(2)}
                          </td>
                          <td className="py-2.5 px-3 text-gray-700">
                            {Number(s.inclination).toFixed(2)}°
                          </td>
                          <td className="py-2.5 px-3 text-gray-700">
                            {Number(s.azimuth).toFixed(2)}°
                          </td>
                          <td className="py-2.5 px-3 text-gray-600">
                            {s.trueVerticalDepth !== null
                              ? `${Number(s.trueVerticalDepth).toFixed(2)}`
                              : "—"}
                          </td>
                          <td className="py-2.5 px-3 text-gray-600">
                            {s.northing !== null
                              ? `${Number(s.northing).toFixed(2)}`
                              : "—"}
                          </td>
                          <td className="py-2.5 px-3 text-gray-600">
                            {s.easting !== null
                              ? `${Number(s.easting).toFixed(2)}`
                              : "—"}
                          </td>
                          <td className="py-2.5 px-3">
                            <div className="flex gap-2">
                              <button
                                onClick={() => handleEdit(s)}
                                className="px-2.5 py-1 text-xs font-medium bg-blue-50 text-blue-600 border border-blue-200 rounded hover:bg-blue-100 transition"
                              >
                                Edit
                              </button>
                              <button
                                onClick={() => handleDelete(s.id)}
                                className="px-2.5 py-1 text-xs font-medium bg-red-50 text-red-600 border border-red-200 rounded hover:bg-red-100 transition"
                              >
                                Delete
                              </button>
                            </div>
                          </td>
                        </tr>
                      );
                    })}
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
