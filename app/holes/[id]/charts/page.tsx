"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  Filler,
} from "chart.js";
import { Line, Scatter } from "react-chartjs-2";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  Filler
);

interface TrajectoryPoint {
  measuredDepth: number;
  inclination: number;
  azimuth: number;
  trueVerticalDepth: number;
  northing: number;
  easting: number;
  verticalSection: number;
  doglegSeverity: number;
}

export default function HoleChartsPage({
  params,
}: {
  params: { id: string };
}) {
  const [hole, setHole] = useState<any>(null);
  const [trajectory, setTrajectory] = useState<TrajectoryPoint[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [calcLoading, setCalcLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    const load = async () => {
      try {
        const res = await fetch("/api/holes/" + params.id);
        if (!res.ok) throw new Error();
        const data = await res.json();
        setHole(data.hole);

        if (
          data.hole?.surveys?.length > 0 &&
          data.hole?.collar
        ) {
          setCalcLoading(true);
          const calcRes = await fetch("/api/survey/calculate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              holeId: data.hole.id,
              collar: {
                northing: Number(data.hole.collar.northing),
                easting: Number(data.hole.collar.easting),
                elevation: Number(data.hole.collar.elevation),
              },
              stations: data.hole.surveys.map((s: any) => ({
                measuredDepth: Number(s.measuredDepth),
                inclination: Number(s.inclination),
                azimuth: Number(s.azimuth),
              })),
            }),
          });
          if (calcRes.ok) {
            const calcData = await calcRes.json();
            setTrajectory(calcData.trajectory || []);
          }
          setCalcLoading(false);
        }
      } catch {
        setError("Failed to load hole data");
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, [params.id]);

  if (isLoading)
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading...</p>
      </div>
    );

  if (!hole)
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="bg-white p-8 rounded-lg shadow-sm border border-gray-200 text-center">
          <h1 className="text-2xl font-bold text-gray-900 mb-2">
            Hole not found
          </h1>
          <Link href="/holes" className="text-blue-600 hover:underline">
            Back to holes
          </Link>
        </div>
      </div>
    );

  const tvdVsMd = {
    datasets: [
      {
        label: "TVD (m)",
        data: trajectory.map((t) => ({
          x: t.measuredDepth,
          y: -Math.abs(t.trueVerticalDepth),
        })),
        borderColor: "#ef4444",
        backgroundColor: "rgba(239, 68, 68, 0.1)",
        borderWidth: 2,
        pointRadius: 3,
        fill: false,
        tension: 0.3,
        showLine: true,
      },
    ],
  };

  const doglegData = {
    datasets: [
      {
        label: "DLS (°/30m)",
        data: trajectory.map((t) => ({
          x: t.measuredDepth,
          y: t.doglegSeverity,
        })),
        borderColor: "#f59e0b",
        backgroundColor: "rgba(245, 158, 11, 0.2)",
        borderWidth: 2,
        pointRadius: 2,
        fill: true,
        tension: 0.3,
        showLine: true,
      },
    ],
  };

  const planViewData = {
    datasets: [
      {
        label: "Plan View (N vs E)",
        data: trajectory.map((t) => ({
          x: t.easting,
          y: t.northing,
        })),
        borderColor: "#3b82f6",
        backgroundColor: "rgba(59, 130, 246, 0.3)",
        borderWidth: 2,
        pointRadius: 4,
        showLine: true,
        tension: 0.3,
      },
    ],
  };

  const vsVsTvd = {
    datasets: [
      {
        label: "VS vs TVD",
        data: trajectory.map((t) => ({
          x: t.verticalSection,
          y: -Math.abs(t.trueVerticalDepth),
        })),
        borderColor: "#8b5cf6",
        backgroundColor: "rgba(139, 92, 246, 0.2)",
        borderWidth: 2,
        pointRadius: 3,
        showLine: true,
        tension: 0.3,
      },
    ],
  };

  const chartOpts = (
    title: string,
    xLabel: string,
    yLabel: string
  ): any => ({
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      title: { display: true, text: title, font: { size: 14 } },
      legend: { display: false },
    },
    scales: {
      x: {
        type: "linear" as const,
        title: { display: true, text: xLabel },
      },
      y: { title: { display: true, text: yLabel } },
    },
  });

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-6xl mx-auto">
          <div className="mb-8">
            <Link
              href={"/holes/" + params.id}
              className="text-sm text-blue-600 hover:underline mb-2 inline-block"
            >
              ← Back to hole
            </Link>
            <h1 className="text-3xl font-bold text-gray-900">
              Trajectory Charts
            </h1>
            <p className="text-gray-600">
              {hole.name} — {hole.holeId}
            </p>
          </div>

          {calcLoading && (
            <div className="flex justify-center py-8">
              <p className="text-gray-500">Calculating trajectory...</p>
            </div>
          )}

          {!calcLoading && trajectory.length === 0 && (
            <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 text-center">
              <p className="text-gray-500">
                {hole.surveys?.length === 0
                  ? "No survey data. Add surveys first."
                  : !hole.collar
                  ? "No collar set. Set collar data to calculate trajectory."
                  : "No trajectory data available."}
              </p>
            </div>
          )}

          {trajectory.length > 0 && (
            <>
              <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
                <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                  <div style={{ height: 400 }}>
                    <Line
                      data={tvdVsMd}
                      options={chartOpts(
                        "TVD vs Measured Depth",
                        "Measured Depth (m)",
                        "TVD (m, depth ↓)"
                      )}
                    />
                  </div>
                </div>
                <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                  <div style={{ height: 400 }}>
                    <Line
                      data={doglegData}
                      options={chartOpts(
                        "Dogleg Severity vs MD",
                        "Measured Depth (m)",
                        "DLS (°/30m)"
                      )}
                    />
                  </div>
                </div>
              </div>
              <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
                <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                  <div style={{ height: 400 }}>
                    <Line
                      data={planViewData}
                      options={chartOpts(
                        "Plan View — Northing vs Easting",
                        "Easting (m)",
                        "Northing (m)"
                      )}
                    />
                  </div>
                </div>
                <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                  <div style={{ height: 400 }}>
                    <Line
                      data={vsVsTvd}
                      options={chartOpts(
                        "Vertical Section vs TVD",
                        "Vertical Section (m)",
                        "TVD (m, depth ↓)"
                      )}
                    />
                  </div>
                </div>
              </div>

              {/* Data Table */}
              <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
                <h2 className="text-lg font-bold text-gray-900 mb-4">
                  Calculated Trajectory Data
                </h2>
                <div className="overflow-x-auto">
                  <table className="w-full text-left border-collapse text-sm">
                    <thead>
                      <tr className="border-b border-gray-200">
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          MD (m)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          Inc (°)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          Az (°)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          TVD (m)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          North (m)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          East (m)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          VS (m)
                        </th>
                        <th className="pb-3 text-xs font-semibold text-gray-400 uppercase">
                          DLS (°/30m)
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {trajectory.map((t, i) => (
                        <tr key={i} className="border-b border-gray-100">
                          <td className="py-2 text-gray-900">
                            {t.measuredDepth.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.inclination.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.azimuth.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.trueVerticalDepth.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.northing.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.easting.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.verticalSection.toFixed(2)}
                          </td>
                          <td className="py-2 text-gray-900">
                            {t.doglegSeverity.toFixed(2)}
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            </>
          )}
        </div>
      </div>
    </div>
  );
}
