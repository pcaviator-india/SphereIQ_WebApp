"use client";

import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";
import { useEffect, useState } from "react";
import Link from "next/link";
import { Line } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend, Filler } from "chart.js";
import { prepareTrajectoryChartData, prepareDoglegChartData } from "@/lib/chart-utils";

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

export default async function HoleChartsPage({
  params,
}: {
  params: { id: string };
}) {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const hole = await prisma.hole.findFirst({
    where: { id: params.id, userId: session.user.id },
    include: { collar: true, surveys: { orderBy: { measuredDepth: "asc" } } },
  });

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

  const [trajectory, setTrajectory] = useState<any[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    let mounted = true;
    const fetchTrajectory = async () => {
      setIsLoading(true);
      try {
        const res = await fetch("/api/survey/calculate", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            holeId: hole.id,
            collar: hole.collar
              ? {
                  northing: Number(hole.collar.northing),
                  easting: Number(hole.collar.easting),
                  elevation: Number(hole.collar.elevation),
                }
              : { northing: 0, easting: 0, elevation: 0 },
            stations: hole.surveys.map((s: any) => ({
              measuredDepth: s.measuredDepth,
              inclination: s.inclination,
              azimuth: s.azimuth,
            })),
          }),
        });
        if (res.ok && mounted) {
          const data = await res.json();
          setTrajectory(data.trajectory || []);
        }
      } catch {
        // silently fail
      } finally {
        if (mounted) setIsLoading(false);
      }
    };
    fetchTrajectory();
    return () => { mounted = false; };
  }, [hole.id, hole.surveys.length]);

  const trajectoryData = prepareTrajectoryChartData(trajectory);
  const doglegData = prepareDoglegChartData(trajectory);

  const trajectoryOptions = {
    responsive: true,
    plugins: {
      title: { display: true, text: "Borehole Trajectory — TVD vs Measured Depth" },
      legend: { position: "top" },
    },
    scales: {
      x: { type: "linear", title: { display: true, text: "Measured Depth (m)" } },
      y: { title: { display: true, text: "Depth / Position (m)" } },
    },
  };

  const doglegOptions = {
    responsive: true,
    plugins: {
      title: { display: true, text: "Dogleg Severity vs Measured Depth" },
      legend: { display: false },
    },
    scales: {
      x: { type: "linear", title: { display: true, text: "Measured Depth (m)" } },
      y: { title: { display: true, text: "DLS (°/30m)" } },
    },
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-6xl mx-auto">
          <div className="flex flex-col md:flex-row md:items-center justify-between mb-8 gap-4">
            <div>
              <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-2 inline-block">
                ← Back to holes
              </Link>
              <h1 className="text-3xl font-bold text-gray-900">Trajectory Charts</h1>
              <p className="text-gray-600">{hole.name} — {hole.holeId}</p>
            </div>
          </div>

          {isLoading && (
            <div className="flex justify-center py-8">
              <p className="text-gray-500">Calculating trajectory...</p>
            </div>
          )}

          {trajectory.length === 0 && !isLoading && hole.surveys.length > 0 && (
            <div className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 text-center">
              <p className="text-gray-500">
                No trajectory data. Run survey calculation first.
              </p>
            </div>
          )}

          {trajectory.length > 0 && (
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
              <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 h-[500px]">
                <h2 className="text-lg font-bold text-gray-900 mb-4">
                  3D Trajectory Projection
                </h2>
                <Line
                  height={400}
                  width={600}
                  data={{
                    datasets: trajectoryData.map((ds, i) => ({
                      ...ds,
                      data: ds.data.map((d) => ({ x: d.x, y: d.y })),
                      borderWidth: 2,
                      pointRadius: i === 0 ? 3 : 2,
                    })),
                  }}
                  options={trajectoryOptions as any}
                />
              </div>
              <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 h-[500px]">
                <h2 className="text-lg font-bold text-gray-900 mb-4">
                  Dogleg Severity
                </h2>
                <Line
                  height={400}
                  width={600}
                  data={{
                    datasets: doglegData,
                  }}
                  options={doglegOptions as any}
                />
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}