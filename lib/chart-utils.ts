/**
 * Chart data utilities for trajectory visualization
 */

import { CalculatedSurvey } from "@/lib/survey-math";

export interface ChartDataset {
  label: string;
  data: { x: number; y: number }[];
  borderColor: string;
  backgroundColor: string;
  fill: boolean;
  tension: number;
  pointRadius: number;
}

export function prepareTrajectoryChartData(
  trajectory: CalculatedSurvey[]
): ChartDataset[] {
  const tvdData: { x: number; y: number }[] = [];
  const northingData: { x: number; y: number }[] = [];
  const eastingData: { x: number; y: number }[] = [];
  const vsData: { x: number; y: number }[] = [];

  trajectory.forEach((s) => {
    tvdData.push({ x: s.measuredDepth, y: -Math.abs(s.trueVerticalDepth) });
    northingData.push({ x: s.measuredDepth, y: s.northing });
    eastingData.push({ x: s.measuredDepth, y: s.easting });
    vsData.push({ x: s.measuredDepth, y: s.verticalSection });
  });

  return [
    {
      label: "TVD (m)",
      data: tvdData,
      borderColor: "#ef4444",
      backgroundColor: "rgba(239, 68, 68, 0.1)",
      fill: false,
      tension: 0.3,
      pointRadius: 2,
    },
    {
      label: "Northing (m)",
      data: northingData,
      borderColor: "#3b82f6",
      backgroundColor: "rgba(59, 130, 246, 0.1)",
      fill: false,
      tension: 0.3,
      pointRadius: 2,
    },
    {
      label: "Easting (m)",
      data: eastingData,
      borderColor: "#10b981",
      backgroundColor: "rgba(16, 185, 129, 0.1)",
      fill: false,
      tension: 0.3,
      pointRadius: 2,
    },
    {
      label: "Vertical Section (m)",
      data: vsData,
      borderColor: "#8b5cf6",
      backgroundColor: "rgba(139, 92, 246, 0.1)",
      fill: false,
      tension: 0.3,
      pointRadius: 2,
    },
  ];
}

export function prepareDoglegChartData(
  trajectory: CalculatedSurvey[]
): ChartDataset[] {
  const dlsData: { x: number; y: number }[] = [];

  trajectory.forEach((s) => {
    dlsData.push({ x: s.measuredDepth, y: s.doglegSeverity });
  });

  return [
    {
      label: "Dogleg Severity (°/30m)",
      data: dlsData,
      borderColor: "#f59e0b",
      backgroundColor: "rgba(245, 158, 11, 0.1)",
      fill: true,
      tension: 0.3,
      pointRadius: 2,
    },
  ];
}
