"use client";

import { useState } from "react";
import Link from "next/link";

export default function TrendAnalysisPage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [trend, setTrend] = useState<any>(null);
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
              <h1 className="text-3xl font-bold text-gray-900">Trend Analysis</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Survey Data</h2>
            <div className="overflow-x-auto">
              <table className="w-full text-left border-collapse">
                <thead>
                  <tr className="border-b border-gray-200">
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">MD</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Inclination</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Azimuth</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">TVD</th>
                  </tr>
                </thead>
                <tbody>
                  {hole.surveys.length === 0 ? (
                    <tr>
                      <td colSpan={4} className="py-4 text-center text-gray-500">
                        No survey data available
                      </td>
                    </tr>
                  ) : (
                    hole.surveys.map((s: any) => (
                      <tr key={s.id} className="border-b border-gray-100">
                        <td className="py-3 text-gray-900">{s.measuredDepth.toString()} m</td>
                        <td className="py-3 text-gray-900">{s.inclination.toString()}°</td>
                        <td className="py-3 text-gray-900">{s.azimuth.toString()}°</td>
                        <td className="py-3 text-gray-600">
                          {s.trueVerticalDepth?.toString() || "—"} m
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Trend Analysis</h2>
            <button
              className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition mb-6"
            >
              Run Trend Analysis
            </button>

            <div className="bg-gray-50 p-4 rounded-lg border border-gray-200">
              <p className="text-gray-500 text-sm">
                Trend statistics will appear here after analysis.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
"use client";

import { useState } from "react";
import Link from "next/link";

export default function TrendAnalysisPage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [trend, setTrend] = useState<any>(null);
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
              <h1 className="text-3xl font-bold text-gray-900">Trend Analysis</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-8">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Survey Data</h2>
            <div className="overflow-x-auto">
              <table className="w-full text-left border-collapse">
                <thead>
                  <tr className="border-b border-gray-200">
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">MD</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Inclination</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">Azimuth</th>
                    <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">TVD</th>
                  </tr>
                </thead>
                <tbody>
                  {hole.surveys.length === 0 ? (
                    <tr>
                      <td colSpan={4} className="py-4 text-center text-gray-500">
                        No survey data available
                      </td>
                    </tr>
                  ) : (
                    hole.surveys.map((s: any) => (
                      <tr key={s.id} className="border-b border-gray-100">
                        <td className="py-3 text-gray-900">{s.measuredDepth.toString()} m</td>
                        <td className="py-3 text-gray-900">{s.inclination.toString()}°</td>
                        <td className="py-3 text-gray-900">{s.azimuth.toString()}°</td>
                        <td className="py-3 text-gray-600">
                          {s.trueVerticalDepth?.toString() || "—"} m
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </div>
          </div>