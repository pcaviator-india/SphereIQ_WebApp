"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";

export default function CreateHolePage() {
  const router = useRouter();
  const [holeId, setHoleId] = useState("");
  const [rigId, setRigId] = useState("RIG-01");
  const [name, setName] = useState("");
  const [location, setLocation] = useState("");
  const [area, setArea] = useState("");
  const [drillingContractor, setDrillingContractor] = useState("");
  const [surveyingContractor, setSurveyingContractor] = useState("");
  const [client, setClient] = useState("");
  const [status, setStatus] = useState("planned");
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setIsLoading(true);

    try {
      const trimmedHoleId = holeId.trim();
      if (!trimmedHoleId) {
        setError("Hole ID is required");
        setIsLoading(false);
        return;
      }

      const res = await fetch("/api/holes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: trimmedHoleId,
          rigId: rigId.trim() || "RIG-01",
          name: name.trim() || trimmedHoleId,
          location: location.trim() || null,
          area: area.trim() || null,
          drillingContractor: drillingContractor.trim() || null,
          surveyingContractor: surveyingContractor.trim() || null,
          client: client.trim() || null,
          status,
        }),
      });

      if (!res.ok) {
        const err = await res.json();
        setError(err.error || "Failed to create hole");
        return;
      }

      const data = await res.json();
      router.push(`/holes/${data.hole.id}`);
    } catch {
      setError("Something went wrong while connecting to the server");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-4 sm:px-6 py-6 sm:py-8">
        <div className="max-w-4xl mx-auto">
          <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-4 inline-block">
            ← Back to boreholes
          </Link>

          <h1 className="text-2xl sm:text-3xl font-bold text-gray-900 mb-2">Create New Borehole</h1>
          <p className="text-gray-600 mb-6 sm:mb-8 text-sm sm:text-base">
            Enter the hole details below. Boreholes are shared across all team members, and your user account will be shown as the creator.
          </p>

          {error && (
            <div className="mb-6 p-4 rounded-lg bg-red-50 border border-red-200 text-sm text-red-700 font-medium">
              ⚠️ {error}
            </div>
          )}

          <form onSubmit={handleSubmit} className="bg-white p-5 sm:p-8 rounded-xl shadow-sm border border-gray-200 space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Hole ID <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., H-001"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 font-medium"
                  value={holeId}
                  onChange={(e) => setHoleId(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The primary identification code of the borehole.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Rig ID <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., RIG-01"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={rigId}
                  onChange={(e) => setRigId(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">Drill rig identification (default RIG-01).</p>
              </div>

              <div className="md:col-span-2">
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Hole Name <span className="text-gray-400 font-normal">(Optional — defaults to Hole ID)</span>
                </label>
                <input
                  type="text"
                  placeholder="e.g., Target Shaft 01"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">Display label used across charts and reports.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Location</label>
                <input
                  type="text"
                  placeholder="e.g., Mine Site A"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={location}
                  onChange={(e) => setLocation(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The mine site or deposit.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Area</label>
                <input
                  type="text"
                  placeholder="e.g., North Pit"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={area}
                  onChange={(e) => setArea(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">Area / sector within the site.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Client</label>
                <input
                  type="text"
                  placeholder="e.g., Exploration Co."
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={client}
                  onChange={(e) => setClient(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">Client organization.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
                <select
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
                  value={status}
                  onChange={(e) => setStatus(e.target.value)}
                >
                  <option value="planned">Planned</option>
                  <option value="drilling">Drilling</option>
                  <option value="completed">Completed</option>
                  <option value="archived">Archived</option>
                </select>
                <p className="text-xs text-gray-500 mt-1">Current operational status.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Drilling Contractor</label>
                <input
                  type="text"
                  placeholder="e.g., Global Drilling Services"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={drillingContractor}
                  onChange={(e) => setDrillingContractor(e.target.value)}
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Surveying Contractor</label>
                <input
                  type="text"
                  placeholder="e.g., Precision Gyro Co."
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={surveyingContractor}
                  onChange={(e) => setSurveyingContractor(e.target.value)}
                />
              </div>
            </div>

            <div className="flex justify-end gap-3 pt-6 border-t border-gray-200">
              <Link
                href="/holes"
                className="px-5 py-2.5 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition text-sm"
              >
                Cancel
              </Link>
              <button
                type="submit"
                disabled={isLoading}
                className="px-6 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition text-sm flex items-center gap-2"
              >
                {isLoading ? (
                  <>
                    <span className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></span>
                    <span>Creating Borehole...</span>
                  </>
                ) : (
                  <span>Create Borehole</span>
                )}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
