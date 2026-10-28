"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";

export default function CreateHolePage() {
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
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setIsLoading(true);

    try {
      const res = await fetch("/api/holes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId,
          rigId,
          name,
          location,
          area,
          drillingContractor,
          surveyingContractor,
          client,
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
      setError("Something went wrong");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-4 inline-block">
            ← Back to holes
          </Link>

          <h1 className="text-3xl font-bold text-gray-900 mb-2">Create New Hole</h1>
          <p className="text-gray-600 mb-8">
            Enter the required hole details below and press "Create Hole" to continue.{" "}
            <span className="font-medium">HoleID and RigID are the only required fields; the rest are optional.</span>
          </p>

          <form onSubmit={handleSubmit} className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Hole ID <span className="text-red-500">(Required)</span>
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., H-001"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={holeId}
                  onChange={(e) => setHoleId(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The identification code of the drill hole.</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Rig ID <span className="text-red-500">(Required)</span>
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., RIG-01"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={rigId}
                  onChange={(e) => setRigId(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The identification code of the drill rig.</p>
              </div>
              <div className="md:col-span-2">
                <label className="block text-sm font-medium text-gray-700 mb-1">Location</label>
                <input
                  type="text"
                  placeholder="e.g., Mine Site A"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={location}
                  onChange={(e) => setLocation(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The mine site that the hole is being drilled on.</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Area</label>
                <input
                  type="text"
                  placeholder="e.g., North Area"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={area}
                  onChange={(e) => setArea(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The area of the mine site the hole is being drilled in.</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Drilling Contractor</label>
                <input
                  type="text"
                  placeholder="e.g., Drilling Co Ltd"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={drillingContractor}
                  onChange={(e) => setDrillingContractor(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The name of the company drilling the hole.</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Surveying Contractor</label>
                <input
                  type="text"
                  placeholder="e.g., Survey Co Ltd"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={surveyingContractor}
                  onChange={(e) => setSurveyingContractor(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The name of the company providing survey's of the hole.</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Client</label>
                <input
                  type="text"
                  placeholder="e.g., Client Company"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={client}
                  onChange={(e) => setClient(e.target.value)}
                />
                <p className="text-xs text-gray-500 mt-1">The name of the company the hole is being drilled for.</p>
              </div>
            </div>




            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
              <select
                value={status}
                onChange={(e) => setStatus(e.target.value as any)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
              >
                <option value="planned">Planned</option>
                <option value="drilling">Drilling</option>
                <option value="completed">Completed</option>
                <option value="archived">Archived</option>
              </select>
            </div>

            {error && (
              <div className="text-sm text-red-600 bg-red-50 px-3 py-2 rounded-md border border-red-100">
                {error}
              </div>
            )}

            <button
              type="submit"
              disabled={isLoading}
              className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
            >
              {isLoading ? "Creating..." : "Create Hole"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
