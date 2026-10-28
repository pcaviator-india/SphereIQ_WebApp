"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";

export default function EditHolePage({ params }: { params: { id: string } }) {
  const router = useRouter();
  const [holeId, setHoleId] = useState("");
  const [rigId, setRigId] = useState("");
  const [name, setName] = useState("");
  const [location, setLocation] = useState("");
  const [area, setArea] = useState("");
  const [drillingContractor, setDrillingContractor] = useState("");
  const [surveyingContractor, setSurveyingContractor] = useState("");
  const [client, setClient] = useState("");
  const [status, setStatus] = useState("planned");
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    const load = async () => {
      try {
        const res = await fetch("/api/holes/" + params.id);
        const data = await res.json();
        const h = data.hole;
        if (h) {
          setHoleId(h.holeId || "");
          setRigId(h.rigId || "");
          setName(h.name || "");
          setLocation(h.location || "");
          setArea(h.area || "");
          setDrillingContractor(h.drillingContractor || "");
          setSurveyingContractor(h.surveyingContractor || "");
          setClient(h.client || "");
          setStatus(h.status || "planned");
        }
      } catch {
        setError("Failed to load hole");
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, [params.id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setIsSaving(true);
    try {
      const res = await fetch("/api/holes/" + params.id, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId,
          rigId,
          name,
          location: location || null,
          area: area || null,
          drillingContractor: drillingContractor || null,
          surveyingContractor: surveyingContractor || null,
          client: client || null,
          status,
        }),
      });
      if (!res.ok) {
        const err = await res.json();
        setError(err.error || "Failed to update hole");
        return;
      }
      router.push("/holes/" + params.id);
    } catch {
      setError("Something went wrong");
    } finally {
      setIsSaving(false);
    }
  };

  if (isLoading)
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading...</p>
      </div>
    );

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <Link
            href={"/holes/" + params.id}
            className="text-sm text-blue-600 hover:underline mb-4 inline-block"
          >
            ← Back to hole
          </Link>
          <h1 className="text-3xl font-bold text-gray-900 mb-2">
            Edit Hole Details
          </h1>

          <form
            onSubmit={handleSubmit}
            className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 space-y-6"
          >
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Hole ID
                </label>
                <input type="text" required value={holeId} onChange={(e) => setHoleId(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Rig ID
                </label>
                <input type="text" required value={rigId} onChange={(e) => setRigId(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div className="md:col-span-2">
                <label className="block text-sm font-medium text-gray-700 mb-1">Name</label>
                <input type="text" required value={name} onChange={(e) => setName(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div className="md:col-span-2">
                <label className="block text-sm font-medium text-gray-700 mb-1">Location</label>
                <input type="text" value={location} onChange={(e) => setLocation(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Area</label>
                <input type="text" value={area} onChange={(e) => setArea(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Drilling Contractor</label>
                <input type="text" value={drillingContractor} onChange={(e) => setDrillingContractor(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Surveying Contractor</label>
                <input type="text" value={surveyingContractor} onChange={(e) => setSurveyingContractor(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Client</label>
                <input type="text" value={client} onChange={(e) => setClient(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" />
              </div>
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
              <select value={status} onChange={(e) => setStatus(e.target.value)} className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white">
                <option value="planned">Planned</option>
                <option value="drilling">Drilling</option>
                <option value="completed">Completed</option>
                <option value="archived">Archived</option>
              </select>
            </div>
            {error && (
              <div className="text-sm text-red-600 bg-red-50 px-3 py-2 rounded-md border border-red-100">{error}</div>
            )}
            <div className="flex gap-3">
              <button type="submit" disabled={isSaving} className="flex-1 px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition">
                {isSaving ? "Saving..." : "Save Changes"}
              </button>
              <Link href={"/holes/" + params.id} className="px-5 py-3 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition text-center">
                Cancel
              </Link>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
