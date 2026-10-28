"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

interface Survey {
  id: string;
  measuredDepth: number;
  inclination: number;
  azimuth: number;
}

interface HoleDetail {
  id: string;
  name: string;
  holeId: string;
  rigId: string;
  location: string | null;
  area: string | null;
  drillingContractor: string | null;
  surveyingContractor: string | null;
  client: string | null;
  collar: any | null;
  target: any | null;
  surveys: Survey[];
}

export default function SideTrackPage({ params }: { params: { id: string } }) {
  const router = useRouter();
  const [parentHole, setParentHole] = useState<HoleDetail | null>(null);
  const [surveys, setSurveys] = useState<Survey[]>([]);
  const [kickoffMD, setKickoffMD] = useState("");
  const [newHoleId, setNewHoleId] = useState("");
  const [newHoleName, setNewHoleName] = useState("");
  const [newRigId, setNewRigId] = useState("");
  const [location, setLocation] = useState("");
  const [area, setArea] = useState("");
  const [drillingContractor, setDrillingContractor] = useState("");
  const [surveyingContractor, setSurveyingContractor] = useState("");
  const [client, setClient] = useState("");

  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchData();
  }, [params.id]);

  const fetchData = async () => {
    try {
      const res = await fetch("/api/holes/" + params.id);
      if (!res.ok) throw new Error("Failed to load parent borehole");
      const data = await res.json();
      const h = data.hole;
      setParentHole(h);
      const sortedSurveys = (h.surveys || []).sort(
        (a: any, b: any) => Number(a.measuredDepth) - Number(b.measuredDepth)
      );
      setSurveys(sortedSurveys);

      // Prepopulate
      setNewHoleId(`${h.holeId}-ST1`);
      setNewHoleName(`${h.name} (SideTrack 1)`);
      setNewRigId(h.rigId || "");
      setLocation(h.location || "");
      setArea(h.area || "");
      drillingContractor;
      setDrillingContractor(h.drillingContractor || "");
      setSurveyingContractor(h.surveyingContractor || "");
      setClient(h.client || "");

      // Default kickoff MD to last survey or 0
      if (sortedSurveys.length > 0) {
        setKickoffMD(String(sortedSurveys[sortedSurveys.length - 1].measuredDepth));
      }
    } catch {
      setError("Failed to load borehole details");
    } finally {
      setIsLoading(false);
    }
  };

  const inheritedSurveys = surveys.filter(
    (s) => Number(s.measuredDepth) <= (parseFloat(kickoffMD) || 0)
  );

  const handleCreateSideTrack = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newHoleId || !newRigId || !newHoleName) {
      setError("Please fill all required fields");
      return;
    }

    const kMD = parseFloat(kickoffMD);
    if (isNaN(kMD) || kMD < 0) {
      setError("Please enter a valid Kickoff MD");
      return;
    }

    setIsSubmitting(true);
    setError("");

    try {
      // 1. Create the new hole
      const holeRes = await fetch("/api/holes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          holeId: newHoleId,
          rigId: newRigId,
          name: newHoleName,
          location: location || null,
          area: area || null,
          drillingContractor: drillingContractor || null,
          surveyingContractor: surveyingContractor || null,
          client: client || null,
          status: "drilling",
        }),
      });

      if (!holeRes.ok) {
        const err = await holeRes.json();
        throw new Error(err.error || "Failed to create sidetrack hole");
      }

      const createdData = await holeRes.json();
      const newId = createdData.hole.id;

      // 2. Copy collar if parent has collar
      if (parentHole?.collar) {
        await fetch(`/api/holes/${newId}/collar`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            northing: Number(parentHole.collar.northing),
            easting: Number(parentHole.collar.easting),
            elevation: Number(parentHole.collar.elevation),
          }),
        });
      }

      // 3. Copy target if parent has target
      if (parentHole?.target) {
        await fetch(`/api/holes/${newId}/target`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            tvd: Number(parentHole.target.tvd),
            northing: Number(parentHole.target.northing),
            easting: Number(parentHole.target.easting),
          }),
        });
      }

      // 4. Copy inherited surveys up to kickoff MD
      for (const s of inheritedSurveys) {
        await fetch(`/api/holes/${newId}/surveys`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            measuredDepth: Number(s.measuredDepth),
            inclination: Number(s.inclination),
            azimuth: Number(s.azimuth),
          }),
        });
      }

      router.push(`/holes/${newId}`);
    } catch (err: any) {
      setError(err.message || "Failed to complete sidetrack process");
      setIsSubmitting(false);
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading borehole details...</p>
      </div>
    );
  }

  if (!parentHole) {
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
          <Link
            href={`/holes/${params.id}`}
            className="text-sm text-blue-600 hover:underline mb-4 inline-block"
          >
            ← Back to {parentHole.name}
          </Link>

          <h1 className="text-3xl font-bold text-gray-900 mb-2">Create SideTrack Borehole</h1>
          <p className="text-gray-600 mb-8">
            Branch a new hole from <strong>{parentHole.name}</strong> ({parentHole.holeId}) at a specific Kickoff Depth.
          </p>

          {error && (
            <div className="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 text-sm text-red-600">
              {error}
            </div>
          )}

          <form onSubmit={handleCreateSideTrack} className="space-y-6">
            {/* Kickoff Setup */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">1. Kickoff Point Configuration</h2>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Kickoff Measured Depth (MD in meters) *
                </label>
                <input
                  type="number"
                  step="any"
                  required
                  value={kickoffMD}
                  onChange={(e) => setKickoffMD(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                />
                <p className="text-xs text-gray-500 mt-1">
                  Surveys from the parent hole at or above this depth ({inheritedSurveys.length} survey(s)) will be copied to the new hole.
                </p>
              </div>
            </div>

            {/* New Hole Identification */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-4">2. SideTrack Hole Identification</h2>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">New Hole ID *</label>
                  <input
                    type="text"
                    required
                    value={newHoleId}
                    onChange={(e) => setNewHoleId(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Rig ID *</label>
                  <input
                    type="text"
                    required
                    value={newRigId}
                    onChange={(e) => setNewRigId(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div className="md:col-span-2">
                  <label className="block text-sm font-medium text-gray-700 mb-1">SideTrack Name *</label>
                  <input
                    type="text"
                    required
                    value={newHoleName}
                    onChange={(e) => setNewHoleName(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Location</label>
                  <input
                    type="text"
                    value={location}
                    onChange={(e) => setLocation(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Area</label>
                  <input
                    type="text"
                    value={area}
                    onChange={(e) => setArea(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Drilling Contractor</label>
                  <input
                    type="text"
                    value={drillingContractor}
                    onChange={(e) => setDrillingContractor(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Surveying Contractor</label>
                  <input
                    type="text"
                    value={surveyingContractor}
                    onChange={(e) => setSurveyingContractor(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 text-sm"
                  />
                </div>
              </div>
            </div>

            {/* Inherited Surveys Preview */}
            <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
              <h2 className="text-lg font-bold text-gray-900 mb-2">
                Inherited Surveys Preview ({inheritedSurveys.length})
              </h2>
              {inheritedSurveys.length === 0 ? (
                <p className="text-sm text-gray-500">No surveys will be inherited at this kickoff depth.</p>
              ) : (
                <div className="max-h-48 overflow-y-auto border border-gray-200 rounded-lg">
                  <table className="w-full text-left text-xs border-collapse">
                    <thead className="bg-gray-50 border-b border-gray-200 sticky top-0">
                      <tr>
                        <th className="p-2 text-gray-500">MD (m)</th>
                        <th className="p-2 text-gray-500">Inc (°)</th>
                        <th className="p-2 text-gray-500">Az (°)</th>
                      </tr>
                    </thead>
                    <tbody>
                      {inheritedSurveys.map((s) => (
                        <tr key={s.id} className="border-b border-gray-100">
                          <td className="p-2 font-medium text-gray-900">{String(s.measuredDepth)}</td>
                          <td className="p-2 text-gray-700">{String(s.inclination)}°</td>
                          <td className="p-2 text-gray-700">{String(s.azimuth)}°</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </div>

            <div className="flex gap-3">
              <button
                type="submit"
                disabled={isSubmitting}
                className="flex-1 px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
              >
                {isSubmitting ? "Creating SideTrack..." : "Create SideTrack Borehole"}
              </button>
              <Link
                href={`/holes/${params.id}`}
                className="px-5 py-3 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition text-center"
              >
                Cancel
              </Link>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
