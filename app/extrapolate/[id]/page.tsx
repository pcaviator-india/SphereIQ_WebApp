"use client";

import { useState } from "react";
import Link from "next/link";

export default function ExtrapolatePage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [targetMD, setTargetMD] = useState("");
  const [vsAzimuth, setVsAzimuth] = useState(0);
  const [result, setResult] = useState<any>(null);
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);

  const fetchHole = async () => {
    const res = await fetch(`/api/holes/${params.id}`);
    const data = await res.json();
    setHole(data.hole);
    setIsLoading(false);
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading...</p>
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

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
                ← Back to holes
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Extrapolation</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Extrapolation Settings</h2>
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Target Measured Depth (m)
                  </label>
                  <input
                    type="number"
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
                    value={vsAzimuth}
                    onChange={(e) => setVsAzimuth(Number(e.target.value))}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <p className="text-xs text-gray-500 mb-2">
                    Last Survey MD:{" "}
                    {hole.surveys.length > 0
                      ? hole.surveys[hole.surveys.length - 1].measuredDepth
                      : "None"}
                  </p>
                  <p className="text-xs text-gray-500">
                    Last Survey TVD:{" "}
                    {hole.surveys.length > 0
                      ? hole.surveys[hole.surveys.length - 1].trueVerticalDepth
                      : "None"}
                  </p>
                </div>
                <button
                  className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  Extrapolate
                </button>
              </div>
            </div>

            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-xl font-bold text-gray-900 mb-4">
                Extrapolated Position
              </h2>
              <div className="space-y-4">
                <p className="text-gray-500 text-sm">
                  Enter target MD and extrapolate position.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}