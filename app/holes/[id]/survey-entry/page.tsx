"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

interface Survey {
  id: string;
  measuredDepth: number;
  inclination: number;
  azimuth: number;
  trueVerticalDepth: number | null;
}

export default function SurveyEntryPage({ params }: { params: { id: string } }) {
  const [hole, setHole] = useState<any>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);
  const [md, setMd] = useState("");
  const [inc, setInc] = useState("");
  const [az, setAz] = useState("");
  const [editId, setEditId] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => { fetchData(); }, []);

  const fetchData = async () => {
    try {
      const [h, s] = await Promise.all([
        fetch("/api/holes/" + params.id),
        fetch("/api/holes/" + params.id + "/surveys"),
      ]);
      setHole((await h.json()).hole);
      setSurveys((await s.json()).surveys || []);
    } catch { setError("Failed to load"); }
    finally { setIsLoading(false); }
  };

  const save = async (e: React.FormEvent) => {
    e.preventDefault(); setError("");
    try {
      const url = editId ? "/api/holes/" + params.id + "/surveys/" + editId : "/api/holes/" + params.id + "/surveys";
      const res = await fetch(url, {
        method: editId ? "PUT" : "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ measuredDepth: parseFloat(md), inclination: parseFloat(inc), azimuth: parseFloat(az) }),
      });
      if (!res.ok) throw new Error("Save failed");
      setSuccess(editId ? "Updated" : "Added");
      setMd(""); setInc(""); setAz(""); setEditId(null);
      await fetchData();
    } catch { setError("Save failed"); }
  };

  const del = async (id: string) => {
    if (!confirm("Delete?")) return;
    try {
      const res = await fetch("/api/holes/" + params.id + "/surveys/" + id, { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      setSuccess("Deleted");
      await fetchData();
    } catch { setError("Delete failed"); }
  };

  const edit = (s: Survey) => {
    setEditId(s.id); setMd(s.measuredDepth.toString()); setInc(s.inclination.toString()); setAz(s.azimuth.toString());
  };

  const cancel = () => { setEditId(null); setMd(""); setInc(""); setAz(""); };

  if (isLoading) return <div className="min-h-screen flex items-center justify-center bg-gray-50"><p className="text-gray-500">Loading...</p></div>;
  if (!hole) return <div className="min-h-screen flex items-center justify-center bg-gray-50"><div className="bg-white p-8 rounded-lg border border-gray-200 text-center"><h1 className="text-2xl font-bold text-gray-900 mb-2">Hole not found</h1><Link href="/holes" className="text-blue-600 hover:underline">Back to holes</Link></div></div>;

  const isEdit = !!editId;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="flex flex-col md:flex-row justify-between items-center mb-6 gap-4">
            <div>
              <Link href={"/holes/" + params.id} className="text-sm text-blue-600 hover:underline">← Back</Link>
              <h1 className="text-3xl font-bold text-gray-900 mb-1">Edit Survey Data</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          {error && <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-100 text-sm text-red-600">{error}</div>}
          {success && <div className="mb-4 p-3 rounded-md bg-green-50 border border-green-100 text-sm text-green-600">{success}</div>}

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-xl font-bold text-gray-900 mb-4">{isEdit ? "Edit Survey" : "Add Survey"}</h2>
            <form onSubmit={save} className="grid grid-cols-1 md:grid-cols-5 gap-4 items-end">
              <div><label className="block text-sm font-medium text-gray-700 mb-1">MD (m)</label><input type="number" step="0.01" required value={md} onChange={e => setMd(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500" /></div>
              <div><label className="block text-sm font-medium text-gray-700 mb-1">Inclination (°)</label><input type="number" step="0.01" required value={inc} onChange={e => setInc(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500" /></div>
              <div><label className="block text-sm font-medium text-gray-700 mb-1">Azimuth (°)</label><input type="number" step="0.01" required value={az} onChange={e => setAz(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500" /></div>
              <div><button type="submit" className={`w-full px-5 py-3 rounded-lg font-medium transition ${isEdit ? "bg-green-600 text-white hover:bg-green-700" : "bg-blue-600 text-white hover:bg-blue-700"}`}>{isEdit ? "Update" : "Add"}</button></div>
              <div>{editId && <button type="button" onClick={cancel} className="w-full px-5 py-3 rounded-lg text-gray-700 bg-gray-100 hover:bg-gray-200 transition font-medium">Cancel</button>}</div>
            </form>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Survey Records ({surveys.length})</h2>
            {surveys.length === 0 ? (
              <div className="py-8 text-center text-gray-500">No survey data yet.</div>
            ) : (
              <table className="w-full text-left border-collapse">
                <thead><tr className="border-b border-gray-200">
                  <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">MD (m)</th>
                  <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Inc (°)</th>
                  <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Az (°)</th>
                  <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">TVD (m)</th>
                  <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Actions</th>
                </tr></thead>
                <tbody>
                  {surveys.map(s => (
                    <tr key={s.id} className="border-b border-gray-100">
                      <td className="py-3 text-gray-900">{s.measuredDepth.toString()} m</td>
                      <td className="py-3 text-gray-900">{s.inclination.toString()}°</td>
                      <td className="py-3 text-gray-900">{s.azimuth.toString()}°</td>
                      <td className="py-3 text-gray-600">{s.trueVerticalDepth ? s.trueVerticalDepth.toString() + " m" : "—"}</td>
                      <td className="py-3"><div className="flex gap-2"><button onClick={() => edit(s)} className="px-3 py-1 text-xs bg-blue-600 text-white rounded hover:bg-blue-700">Edit</button><button onClick={() => del(s.id)} className="px-3 py-1 text-xs bg-red-600 text-white rounded hover:bg-red-700">Delete</button></div></td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>

          <div className="mt-6 flex gap-2">
            <Link href={"/holes/" + params.id} className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50">Back to Hole</Link>
            <Link href={"/holes/" + params.id + "/charts"} className="px-4 py-2 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700">View Charts</Link>
          </div>
        </div>
      </div>
    </div>
  );
}