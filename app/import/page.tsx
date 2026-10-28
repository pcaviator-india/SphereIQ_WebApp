"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter, useSearchParams } from "next/navigation";
import { parseEznFile, EznHoleFileDto } from "@/lib/ezn-format";

interface HoleOption {
  id: string;
  name: string;
  holeId: string;
}

export default function ImportPage() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const preselectedHole = searchParams.get("holeId");
  const initialTab = searchParams.get("tab") === "csv" ? "csv" : "ezn";

  const [activeTab, setActiveTab] = useState<"ezn" | "csv">(initialTab);

  // EZN Import states
  const [eznFile, setEznFile] = useState<File | null>(null);
  const [eznDto, setEznDto] = useState<EznHoleFileDto | null>(null);
  const [isImportingEzn, setIsImportingEzn] = useState(false);

  // CSV Import states
  const [holes, setHoles] = useState<HoleOption[]>([]);
  const [selectedHoleId, setSelectedHoleId] = useState(preselectedHole || "");
  const [csvFile, setCsvFile] = useState<File | null>(null);
  const [csvContent, setCsvContent] = useState("");
  const [headers, setHeaders] = useState<string[]>([]);
  const [previewRows, setPreviewRows] = useState<string[][]>([]);
  const [mdCol, setMdCol] = useState<number>(0);
  const [incCol, setIncCol] = useState<number>(1);
  const [azCol, setAzCol] = useState<number>(2);
  const [isImportingCsv, setIsImportingCsv] = useState(false);

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
      if (!selectedHoleId && data.holes?.length > 0) {
        setSelectedHoleId(data.holes[0].id);
      }
    } catch {
      setError("Failed to load existing boreholes");
    }
  };

  // Handle .ezn file selection
  const handleEznFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setError("");
    setSuccess("");
    setEznDto(null);
    const file = e.target.files?.[0];
    if (!file) return;

    setEznFile(file);
    const reader = new FileReader();
    reader.onload = (event) => {
      try {
        const text = (event.target?.result as string) || "";
        const dto = parseEznFile(text);
        setEznDto(dto);
      } catch (err: any) {
        setError(err.message || "Could not parse .ezn file. Make sure it is a valid SphereIQ or Ezy-Nav borehole file.");
      }
    };
    reader.readAsText(file);
  };

  const handleEznImport = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!eznFile || !eznDto) {
      setError("Please select a valid .ezn file first");
      return;
    }

    setIsImportingEzn(true);
    setError("");
    setSuccess("");

    try {
      const formData = new FormData();
      formData.append("file", eznFile);

      const res = await fetch("/api/import/ezn", {
        method: "POST",
        body: formData,
      });

      const data = await res.json();
      if (!res.ok) {
        throw new Error(data.error || "Import failed");
      }

      setSuccess(`Successfully imported borehole "${data.hole.name}" with ${data.surveysCount} surveys! Redirecting...`);
      setTimeout(() => {
        router.push(`/holes/${data.hole.id}`);
      }, 1200);
    } catch (err: any) {
      setError(err.message || "Failed to import borehole");
    } finally {
      setIsImportingEzn(false);
    }
  };

  // Handle CSV file selection
  const handleCsvFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setError("");
    setSuccess("");
    const selectedFile = e.target.files?.[0];
    if (!selectedFile) return;

    setCsvFile(selectedFile);
    const reader = new FileReader();
    reader.onload = (event) => {
      const text = (event.target?.result as string) || "";
      setCsvContent(text);

      const lines = text.split(/\r?\n/).filter((l) => l.trim().length > 0 && !l.trim().startsWith("#"));
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

  const handleCsvImport = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedHoleId) {
      setError("Please select a target borehole");
      return;
    }
    if (!csvContent) {
      setError("Please select a valid CSV file");
      return;
    }

    setIsImportingCsv(true);
    setError("");
    setSuccess("");

    try {
      const res = await fetch("/api/import/csv", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: selectedHoleId,
          csvContent,
          mdColIndex: mdCol,
          incColIndex: incCol,
          azColIndex: azCol,
        }),
      });

      const data = await res.json();
      if (!res.ok) {
        throw new Error(data.error || "CSV Import failed");
      }

      setSuccess(`Imported ${data.count} survey stations successfully! Redirecting...`);
      setTimeout(() => {
        router.push(`/holes/${selectedHoleId}`);
      }, 1200);
    } catch (err: any) {
      setError(err.message || "Failed to import surveys");
    } finally {
      setIsImportingCsv(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-4xl mx-auto">
          <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-4 inline-block">
            ← Back to boreholes
          </Link>

          <div className="flex flex-col md:flex-row md:items-center justify-between mb-6 gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-900">Import Borehole Data</h1>
              <p className="text-gray-600 mt-1">
                Import complete SphereIQ (.ezn) borehole files or batch import survey stations from CSV.
              </p>
            </div>
          </div>

          {/* Navigation Tabs */}
          <div className="flex border-b border-gray-200 mb-6 bg-white rounded-t-xl px-4 pt-2 shadow-xs">
            <button
              onClick={() => {
                setActiveTab("ezn");
                setError("");
                setSuccess("");
              }}
              className={`pb-3 px-4 font-semibold text-sm border-b-2 transition flex items-center gap-2 ${
                activeTab === "ezn"
                  ? "border-blue-600 text-blue-600"
                  : "border-transparent text-gray-500 hover:text-gray-700"
              }`}
            >
              <span>📁</span>
              <span>SphereIQ / Ezy-Nav File (.ezn)</span>
              <span className="text-[10px] bg-blue-100 text-blue-800 font-bold px-1.5 py-0.5 rounded-full">
                Full Borehole
              </span>
            </button>

            <button
              onClick={() => {
                setActiveTab("csv");
                setError("");
                setSuccess("");
              }}
              className={`pb-3 px-4 font-semibold text-sm border-b-2 transition flex items-center gap-2 ${
                activeTab === "csv"
                  ? "border-blue-600 text-blue-600"
                  : "border-transparent text-gray-500 hover:text-gray-700"
              }`}
            >
              <span>📊</span>
              <span>Survey Stations (.csv)</span>
              <span className="text-[10px] bg-gray-100 text-gray-600 font-bold px-1.5 py-0.5 rounded-full">
                Table
              </span>
            </button>
          </div>

          {error && (
            <div className="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 text-sm text-red-700 font-medium">
              ⚠️ {error}
            </div>
          )}

          {success && (
            <div className="mb-6 p-4 rounded-xl bg-green-50 border border-green-200 text-sm text-green-700 font-medium">
              ✓ {success}
            </div>
          )}

          {/* TAB 1: EZN FULL BOREHOLE IMPORT */}
          {activeTab === "ezn" && (
            <form onSubmit={handleEznImport} className="space-y-6">
              <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 space-y-6">
                <div>
                  <h2 className="text-lg font-bold text-gray-900 mb-1">Select SphereIQ (.ezn) File</h2>
                  <p className="text-xs text-gray-500 mb-4">
                    Upload an authentic .ezn file from the desktop application. It contains the complete Hole Identification, Rig, Collar, Target, and Survey stations.
                  </p>

                  <div className="border-2 border-dashed border-gray-300 hover:border-blue-500 rounded-xl p-8 text-center transition bg-gray-50/50">
                    <input
                      type="file"
                      accept=".ezn,application/json,text/plain"
                      onChange={handleEznFileChange}
                      className="hidden"
                      id="eznFileInput"
                    />
                    <label htmlFor="eznFileInput" className="cursor-pointer block">
                      <div className="text-4xl mb-2">📁</div>
                      <p className="text-sm font-semibold text-gray-900">
                        {eznFile ? eznFile.name : "Click to select or drag & drop an .ezn file"}
                      </p>
                      <p className="text-xs text-gray-500 mt-1">Supports SphereIQ & Ezy-Nav format (.ezn)</p>
                    </label>
                  </div>
                </div>

                {/* EZN File Preview Card */}
                {eznDto && (
                  <div className="border border-blue-200 bg-blue-50/50 rounded-xl p-5 space-y-4">
                    <div className="flex items-center justify-between border-b border-blue-200/80 pb-2">
                      <span className="font-bold text-blue-900 text-sm flex items-center gap-2">
                        <span>✓</span> Valid Borehole File Detected
                      </span>
                      <span className="text-xs bg-blue-200/70 text-blue-900 font-semibold px-2 py-0.5 rounded-full font-mono">
                        {eznDto.Surveys?.length || 0} Surveys
                      </span>
                    </div>

                    <div className="grid grid-cols-2 md:grid-cols-4 gap-4 text-xs">
                      <div>
                        <span className="text-gray-500 block">Hole ID:</span>
                        <span className="font-bold text-gray-900 text-sm">{eznDto.HoleID}</span>
                      </div>
                      <div>
                        <span className="text-gray-500 block">Rig ID:</span>
                        <span className="font-bold text-gray-900 text-sm">{eznDto.RigID || "RIG-01"}</span>
                      </div>
                      <div>
                        <span className="text-gray-500 block">Location:</span>
                        <span className="font-medium text-gray-800">{eznDto.Location || "—"}</span>
                      </div>
                      <div>
                        <span className="text-gray-500 block">Client:</span>
                        <span className="font-medium text-gray-800">{eznDto.Client || "—"}</span>
                      </div>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 text-xs pt-3 border-t border-blue-200/60">
                      <div className="bg-white p-3 rounded-lg border border-blue-100">
                        <span className="font-bold text-gray-800 block mb-1">Collar Coordinates:</span>
                        {eznDto.Collar ? (
                          <div className="space-y-0.5 font-mono text-gray-700">
                            <div>East: <span className="font-bold">{eznDto.Collar.Easting}</span></div>
                            <div>North: <span className="font-bold">{eznDto.Collar.Northing}</span></div>
                          </div>
                        ) : (
                          <span className="text-gray-400 italic">No collar defined</span>
                        )}
                      </div>

                      <div className="bg-white p-3 rounded-lg border border-blue-100">
                        <span className="font-bold text-gray-800 block mb-1">Target Coordinates:</span>
                        {eznDto.Target ? (
                          <div className="space-y-0.5 font-mono text-gray-700">
                            <div>East: <span className="font-bold">{eznDto.Target.Easting}</span></div>
                            <div>North: <span className="font-bold">{eznDto.Target.Northing}</span></div>
                            <div>TVD: <span className="font-bold">{eznDto.Target.TVD}</span></div>
                          </div>
                        ) : (
                          <span className="text-gray-400 italic">No target defined</span>
                        )}
                      </div>
                    </div>
                  </div>
                )}

                <div className="flex justify-end gap-3 pt-4 border-t border-gray-100">
                  <button
                    type="submit"
                    disabled={!eznDto || isImportingEzn}
                    className="px-6 py-2.5 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 disabled:opacity-50 transition text-sm flex items-center gap-2 shadow-sm"
                  >
                    {isImportingEzn ? (
                      <>
                        <span className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></span>
                        <span>Importing Borehole & Calculating Trajectory...</span>
                      </>
                    ) : (
                      <span>Import Full Borehole (.ezn)</span>
                    )}
                  </button>
                </div>
              </div>
            </form>
          )}

          {/* TAB 2: CSV SURVEY STATIONS IMPORT */}
          {activeTab === "csv" && (
            <form onSubmit={handleCsvImport} className="space-y-6">
              <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 space-y-6">
                <div>
                  <label className="block text-sm font-semibold text-gray-700 mb-1">
                    Select Target Borehole <span className="text-red-500">*</span>
                  </label>
                  <select
                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 bg-white text-sm"
                    value={selectedHoleId}
                    onChange={(e) => setSelectedHoleId(e.target.value)}
                    required
                  >
                    <option value="" disabled>
                      Select a borehole to receive these surveys...
                    </option>
                    {holes.map((h) => (
                      <option key={h.id} value={h.id}>
                        {h.name} ({h.holeId})
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block text-sm font-semibold text-gray-700 mb-1">
                    Survey CSV File <span className="text-red-500">*</span>
                  </label>
                  <div className="border-2 border-dashed border-gray-300 hover:border-blue-500 rounded-xl p-8 text-center transition bg-gray-50/50">
                    <input
                      type="file"
                      accept=".csv,text/csv,text/plain"
                      onChange={handleCsvFileChange}
                      className="hidden"
                      id="csvFileInput"
                    />
                    <label htmlFor="csvFileInput" className="cursor-pointer block">
                      <div className="text-4xl mb-2">📊</div>
                      <p className="text-sm font-semibold text-gray-900">
                        {csvFile ? csvFile.name : "Click to select or drag & drop a CSV file"}
                      </p>
                      <p className="text-xs text-gray-500 mt-1">Columns should include MD, Inclination/Dip, and Azimuth</p>
                    </label>
                  </div>
                </div>

                {headers.length > 0 && (
                  <div className="space-y-4 pt-4 border-t border-gray-200">
                    <h2 className="text-md font-bold text-gray-900">Column Mapping</h2>
                    <p className="text-xs text-gray-500">
                      Match the CSV columns to required survey station parameters.
                    </p>

                    <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                      <div>
                        <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                          Measured Depth (MD)
                        </label>
                        <select
                          className="w-full px-3 py-2 border border-gray-300 rounded text-sm bg-white"
                          value={mdCol}
                          onChange={(e) => setMdCol(parseInt(e.target.value))}
                        >
                          {headers.map((h, i) => (
                            <option key={i} value={i}>
                              Col {i + 1}: {h}
                            </option>
                          ))}
                        </select>
                      </div>

                      <div>
                        <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                          Inclination / Dip
                        </label>
                        <select
                          className="w-full px-3 py-2 border border-gray-300 rounded text-sm bg-white"
                          value={incCol}
                          onChange={(e) => setIncCol(parseInt(e.target.value))}
                        >
                          {headers.map((h, i) => (
                            <option key={i} value={i}>
                              Col {i + 1}: {h}
                            </option>
                          ))}
                        </select>
                      </div>

                      <div>
                        <label className="block text-xs font-semibold text-gray-700 uppercase mb-1">
                          Azimuth
                        </label>
                        <select
                          className="w-full px-3 py-2 border border-gray-300 rounded text-sm bg-white"
                          value={azCol}
                          onChange={(e) => setAzCol(parseInt(e.target.value))}
                        >
                          {headers.map((h, i) => (
                            <option key={i} value={i}>
                              Col {i + 1}: {h}
                            </option>
                          ))}
                        </select>
                      </div>
                    </div>

                    {/* Preview Table */}
                    {previewRows.length > 0 && (
                      <div className="pt-2">
                        <p className="text-xs font-semibold text-gray-700 mb-2">Data Preview (First 5 rows):</p>
                        <div className="overflow-x-auto border border-gray-200 rounded-lg">
                          <table className="w-full text-xs text-left text-gray-600">
                            <thead className="bg-gray-100 text-gray-700 font-semibold border-b border-gray-200">
                              <tr>
                                {headers.map((h, i) => (
                                  <th
                                    key={i}
                                    className={`px-3 py-2 ${
                                      i === mdCol
                                        ? "bg-blue-100 text-blue-900"
                                        : i === incCol
                                        ? "bg-amber-100 text-amber-900"
                                        : i === azCol
                                        ? "bg-green-100 text-green-900"
                                        : ""
                                    }`}
                                  >
                                    {h}
                                    {i === mdCol && " (MD)"}
                                    {i === incCol && " (Inc)"}
                                    {i === azCol && " (Az)"}
                                  </th>
                                ))}
                              </tr>
                            </thead>
                            <tbody className="divide-y divide-gray-100">
                              {previewRows.map((r, ri) => (
                                <tr key={ri} className="hover:bg-gray-50">
                                  {r.map((c, ci) => (
                                    <td key={ci} className="px-3 py-1.5 font-mono">
                                      {c}
                                    </td>
                                  ))}
                                </tr>
                              ))}
                            </tbody>
                          </table>
                        </div>
                      </div>
                    )}
                  </div>
                )}

                <div className="flex justify-end gap-3 pt-4 border-t border-gray-100">
                  <button
                    type="submit"
                    disabled={headers.length === 0 || isImportingCsv}
                    className="px-6 py-2.5 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 disabled:opacity-50 transition text-sm flex items-center gap-2 shadow-sm"
                  >
                    {isImportingCsv ? (
                      <>
                        <span className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></span>
                        <span>Importing Surveys...</span>
                      </>
                    ) : (
                      <span>Import Survey Stations (CSV)</span>
                    )}
                  </button>
                </div>
              </div>
            </form>
          )}
        </div>
      </div>
    </div>
  );
}
