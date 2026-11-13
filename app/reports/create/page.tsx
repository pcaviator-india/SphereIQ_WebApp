"use client";

import { useState } from "react";
import Link from "next/link";
import { prisma } from "@/lib/prisma";
import { getServerAuthSession } from "@/lib/auth";

interface ReportComponentFlags {
  surveyData: boolean;
  buildSurveys: boolean;
  extrapolatedSurveys: boolean;
  analysedSurveys: boolean;
  charts: boolean;
}

export default async function CreateReportPage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const [step, setStep] = useState<"userFields" | "components" | "submitting">("userFields");
  const [holeId, setHoleId] = useState("");
  const [title, setTitle] = useState("");
  const [type, setType] = useState("status");
  const [author, setAuthor] = useState(session.user.name || "");
  const [notes, setNotes] = useState("");
  const [components, setComponents] = useState<ReportComponentFlags>({
    surveyData: true,
    buildSurveys: false,
    extrapolatedSurveys: false,
    analysedSurveys: false,
    charts: true,
  });
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError("");

    try {
      const res = await fetch("/api/reports", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId,
          title,
          type,
          author,
          notes,
        }),
      });

      if (!res.ok) throw new Error("Failed to create report");

      const data = await res.json();
      window.location.href = `/reports/${data.report.id}`;
    } catch {
      setError("Failed to create report. Please try again.");
      setLoading(false);
    }
  };

  const typeOptions = [
    { value: "build", label: "Build to Target" },
    { value: "trend", label: "Trend Analysis" },
    { value: "extrapolate", label: "Extrapolation" },
    { value: "status", label: "Hole Status" },
  ];

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-2xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link href="/reports" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
                ← Back to reports
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Create Report</h1>
              <p className="text-gray-600">
                Select hole, enter details, and choose components to include.
              </p>
            </div>
          </div>

          {step === "userFields" && (
            <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200">
              <div className="flex items-center gap-2 mb-6">
                <span className="text-xs font-semibold text-blue-600 bg-blue-50 px-2 py-1 rounded">
                  Step 1 of 2
                </span>
                <span className="text-xs text-gray-400">Report Details</span>
              </div>

              <form onSubmit={handleSubmit}>
                <div className="space-y-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Borehole ID
                    </label>
                    <input
                      type="text"
                      value={holeId}
                      onChange={(e) => setHoleId(e.target.value)}
                      placeholder="e.g. H-001"
                      className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm"
                      required
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Report Title
                    </label>
                    <input
                      type="text"
                      value={title}
                      onChange={(e) => setTitle(e.target.value)}
                      placeholder="e.g. Weekly Progress Report"
                      className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm"
                      required
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Report Type
                    </label>
                    <select
                      value={type}
                      onChange={(e) => setType(e.target.value as any)}
                      className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm bg-white"
                    >
                      {typeOptions.map((t) => (
                        <option key={t.value} value={t.value}>
                          {t.label}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Author Name
                    </label>
                    <input
                      type="text"
                      value={author}
                      onChange={(e) => setAuthor(e.target.value)}
                      placeholder="Your name"
                      className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm"
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Notes
                    </label>
                    <textarea
                      value={notes}
                      onChange={(e) => setNotes(e.target.value)}
                      placeholder="Additional notes for this report..."
                      rows={3}
                      className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-sm"
                    />
                  </div>

                  {error && (
                    <div className="p-3 rounded-lg bg-red-50 border border-red-100 text-red-600 text-sm">
                      {error}
                    </div>
                  )}

                  <div className="flex justify-end gap-3 pt-4">
                    <button
                      type="button"
                      onClick={() => setStep("components")}
                      disabled={loading}
                      className="px-5 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                    >
                      {loading ? "Creating..." : "Next →"}
                    </button>
                  </div>
                </div>
              </form>
            </div>
          )}

          {step === "components" && (
            <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200">
              <div className="flex items-center gap-2 mb-6">
                <span className="text-xs font-semibold text-blue-600 bg-blue-50 px-2 py-1 rounded">
                  Step 2 of 2
                </span>
                <span className="text-xs text-gray-400">Report Components</span>
              </div>

              <div className="space-y-4">
                <div className="border border-gray-200 rounded-lg p-4 bg-gray-50">
                  <h3 className="text-sm font-semibold text-gray-900 mb-3">Components to Include</h3>
                  <div className="space-y-2">
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input
                        type="checkbox"
                        checked={components.surveyData}
                        onChange={(e) =>
                          setComponents({ ...components, surveyData: e.target.checked })
                        }
                        className="rounded text-blue-600 focus:ring-blue-500"
                        defaultChecked
                      />
                      <span className="text-gray-700 font-medium">Survey Data</span>
                    </label>
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input
                        type="checkbox"
                        checked={components.buildSurveys}
                        onChange={(e) =>
                          setComponents({ ...components, buildSurveys: e.target.checked })
                        }
                        className="rounded text-blue-600 focus:ring-blue-500"
                      />
                      <span className="text-gray-700 font-medium">Build Surveys</span>
                    </label>
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input
                        type="checkbox"
                        checked={components.extrapolatedSurveys}
                        onChange={(e) =>
                          setComponents({
                            ...components,
                            extrapolatedSurveys: e.target.checked,
                          })
                        }
                        className="rounded text-blue-600 focus:ring-blue-500"
                      />
                      <span className="text-gray-700 font-medium">Extrapolated Surveys</span>
                    </label>
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input
                        type="checkbox"
                        checked={components.analysedSurveys}
                        onChange={(e) =>
                          setComponents({ ...components, analysedSurveys: e.target.checked })
                        }
                        className="rounded text-blue-600 focus:ring-blue-500"
                      />
                      <span className="text-gray-700 font-medium">Analysed Surveys</span>
                    </label>
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input
                        type="checkbox"
                        checked={components.charts}
                        onChange={(e) =>
                          setComponents({ ...components, charts: e.target.checked })
                        }
                        className="rounded text-blue-600 focus:ring-blue-500"
                        defaultChecked
                      />
                      <span className="text-gray-700 font-medium">Charts</span>
                    </label>
                  </div>
                </div>

                <div className="flex justify-between items-center pt-4 border-t border-gray-100">
                  <button
                    onClick={() => setStep("userFields")}
                    className="px-5 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition"
                  >
                    ← Back
                  </button>
                  <button
                    onClick={handleSubmit}
                    disabled={loading}
                    className="px-5 py-2.5 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                  >
                    {loading ? "Creating..." : "Create Report"}
                  </button>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}