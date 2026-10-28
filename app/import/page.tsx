"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

interface HoleOption {
  id: string;
  name: string;
  holeId: string;
}

export default function ImportPage() {
  const router = useRouter();
  const [holes, setHoles] = useState<HoleOption[]>([]);
  const [selectedHoleId, setSelectedHoleId] = useState("");
  const [file, setFile] = useState<File | null>(null);
  const [csvContent, setCsvContent] = useState("");
  const [headers, setHeaders] = useState<string[]>([]);
  const [previewRows, setPreviewRows] = useState<string[][]>([]);
  const [mdCol, setMdCol] = useState<number>(0);
  const [incCol, setIncCol] = useState<number>(1);
  const [azCol, setAzCol] = useState<number>(2);

  const [isLoadingHoles, setIsLoadingHoles] = useState(true);
  const [isImporting, setIsImporting] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    fetchHoles();
  }, []);

  const fetchHoles = async () => {
    try {
      const res = await fetch("/api/holes");
      if (!res.ok) throw new Error();
      const data = await res.json();
      setHoles(data.holes || []);
      if (data.holes?.length > 0) {
        setSelectedHoleId(data.holes[0].id);
      }
    } catch {
      setError("Failed to load boreholes");
    } finally {
      setIsLoadingHoles(false);
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setError("");
    setSuccess("");
    const selectedFile = e.target.files?.[0];
    if (!selectedFile) return;

    setFile(selectedFile);
    const reader = new FileReader();
    reader.onload = (event) => {
      const text = (event.target?.result as string) || "";
      setCsvContent(text);

      const lines = text.split(/\r?\n/).filter((l) => l.trim().length > 0);
      if (lines.length > 0) {
        const rawHeaders = lines[0].split(",").map((h) => h.trim());
        setHeaders(rawHeaders);

        // Auto-match defaults
        const lower = rawHeaders.map((h) => h.toLowerCase());
        const foundMd = lower.findIndex((h) => h.includes("md") || h.includes("depth"));
        const foundInc = lower.findIndex((h) => h.includes("inc") || h.includes("dip"));
        const foundAz = lower.findIndex((h) => h.includes("az") || h.includes("bearing") || h.includes("dir"));

        if (foundMd >= 0) setMdCol(foundMd);
        if (foundInc >= 0) setIncCol(foundInc);
        if (foundAz >= 0) setAzCol(foundAz);

        // Preview up to 5 data rows
        const rows = lines.slice(1, 6).map((line) => line.split(",").map((c) => c.trim()));
        setPreviewRows(rows);
      }
    };
    reader.readAsText(selectedFile);
  };

  const handleImport = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedHoleId) {
      setError("Please select a target borehole");
      return;
    }
    if (!csvContent) {
      setError("Please select a valid CSV file");
      return;
    }

    setIsImporting(true);
    setError("");
    setSuccess("");

    try {
      const res = await fetch("/api/import/csv", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: selectedHoleId,
          csvContent,
          columnMap: {
            measuredDepth: Number(mdCol),
            inclination: Number(incCol),
            azimuth: Number(azCol),
          },
        }),
      });

      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Import failed");

      setSuccess(data.message || `Successfully imported ${data.imported} surveys!`);
      setTimeout(() => {
        router.push(`/holes/${selectedHoleId}/survey-entry`);
      }, 1500);
    } catch (err: any) {
      setError(err.message || "Failed to import CSV");
    } finally {
      setIsImporting(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <div className="mb-8">
            <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
              ← Back to holes
            </Link>
            <h1 className="text-3xl font-bold text-gray-900">Import Survey CSV</h1>
            <p className="text-gray-600 mt-1">
              Upload survey files (.csv) and map Measured Depth, Inclination, and Azimuth columns.
            </p>
          </div>

          {error && (
            <div className="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 text-sm text-red-600">
              {error}
            </div>
          )}

          {success && (
            <div className="mb-6 p-4 rounded-xl bg-green-50 border border-green-200 text-sm text-green-700">
              {success} Redirecting to survey list...
            </div>
          )}

          <form onSubmit={handleImport} className="space-y-6">
            {/* Target Hole */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">1. Select Target Borehole</h2>
              {isLoadingHoles ? (
                <p className="text-sm text-gray-500">Loading boreholes...</p>
              ) : holes.length === 0 ? (
                <div>
                  <p className="text-sm text-gray-500 mb-3">No boreholes found. Create one first.</p>
                  <Link
                    href="/holes/create"
                    className="inline-block px-4 py-2 bg-blue-600 text-white text-sm font-medium rounded-lg"
                  >
                    + Create Borehole
                  </Link>
                </div>
              ) : (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Target Borehole</label>
                  <select
                    value={selectedHoleId}
                    onChange={(e) => setSelectedHoleId(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 bg-white text-sm"
                  >
                    {holes.map((h) => (
                      <option key={h.id} value={h.id}>
                        {h.name} ({h.holeId})
                      </option>
                    ))}
                  </select>
                </div>
              )}
            </div>

            {/* File Upload */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">2. Choose CSV File</h2>
              <div className="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center hover:border-blue-400 transition">
                <input
                  type="file"
                  id="csvFile"
                  accept=".csv,text/csv"
                  onChange={handleFileChange}
                  className="hidden"
                />
                <label htmlFor="csvFile" className="cursor-pointer block">
                  <span className="text-3xl block mb-2">📁</span>
                  <span className="text-sm font-medium text-blue-600 hover:underline">
                    {file ? file.name : "Click to select a CSV file"}
                  </span>
                  <p className="text-xs text-gray-500 mt-1">Comma-separated values (.csv)</p>
                </label>
              </div>
            </div>

            {/* Column Mapping & Preview */}
            {headers.length > 0 && (
              <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                <h2 className="text-lg font-bold text-gray-900 mb-4">3. Map Columns</h2>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Measured Depth (MD) *
                    </label>
                    <select
                      value={mdCol}
                      onChange={(e) => setMdCol(Number(e.target.value))}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm bg-white focus:ring-2 focus:ring-blue-500"
                    >
                      {headers.map((h, i) => (
                        <option key={i} value={i}>
                          Col {i + 1}: {h}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Inclination (Dip) *
                    </label>
                    <select
                      value={incCol}
                      onChange={(e) => setIncCol(Number(e.target.value))}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm bg-white focus:ring-2 focus:ring-blue-500"
                    >
                      {headers.map((h, i) => (
                        <option key={i} value={i}>
                          Col {i + 1}: {h}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Azimuth (Bearing) *
                    </label>
                    <select
                      value={azCol}
                      onChange={(e) => setAzCol(Number(e.target.value))}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm bg-white focus:ring-2 focus:ring-blue-500"
                    >
                      {headers.map((h, i) => (
                        <option key={i} value={i}>
                          Col {i + 1}: {h}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>

                {/* Data Preview */}
                <h3 className="text-sm font-semibold text-gray-700 mb-2">Preview (first 5 rows)</h3>
                <div className="overflow-x-auto border border-gray-200 rounded-lg">
                  <table className="w-full text-left text-xs border-collapse">
                    <thead>
                      <tr className="bg-gray-50 border-b border-gray-200">
                        {headers.map((h, i) => (
                          <th
                            key={i}
                            className={`p-2 font-semibold uppercase ${
                              i === mdCol
                                ? "bg-blue-100 text-blue-800"
                                : i === incCol
                                ? "bg-green-100 text-green-800"
                                : i === azCol
                                ? "bg-purple-100 text-purple-800"
                                : "text-gray-500"
                            }`}
                          >
                            {h} {i === mdCol ? "(MD)" : i === incCol ? "(Inc)" : i === azCol ? "(Az)" : ""}
                          </th>
                        ))}
                      </tr>
                    </thead>
                    <tbody>
                      {previewRows.map((r, ri) => (
                        <tr key={ri} className="border-b border-gray-100">
                          {r.map((cell, ci) => (
                            <td key={ci} className="p-2 text-gray-700">
                              {cell}
                            </td>
                          ))}
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}

            {/* Submit */}
            <button
              type="submit"
              disabled={isImporting || !file || !selectedHoleId}
              className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
            >
              {isImporting ? "Importing Surveys..." : "Start Import"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
