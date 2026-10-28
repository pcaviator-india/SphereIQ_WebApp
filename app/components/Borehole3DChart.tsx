"use client";

import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls.js";

export interface Point3D {
  northing: number;
  easting: number;
  tvd: number;
}

export interface Borehole3DChartProps {
  collar?: { northing: number; easting: number; elevation?: number } | null;
  target?: { tvd: number; northing: number; easting: number } | null;
  surveys?: {
    measuredDepth: number;
    inclination: number;
    azimuth: number;
    trueVerticalDepth: number | null;
    northing: number | null;
    easting: number | null;
  }[];
  straightPath?: {
    md: number;
    tvd: number;
    northing: number;
    easting: number;
  } | null;
  showStraightPath?: boolean;
  className?: string;
}

export default function Borehole3DChart({
  collar,
  target,
  surveys = [],
  straightPath,
  showStraightPath = false,
  className = "w-full h-full min-h-[500px]",
}: Borehole3DChartProps) {
  const containerRef = useRef<HTMLDivElement>(null);
  const chartWrapperRef = useRef<HTMLDivElement>(null);
  const [isFullscreen, setIsFullscreen] = useState(false);
  const [showLegend, setShowLegend] = useState(false);
  const [tooltip, setTooltip] = useState<{
    text: string;
    x: number;
    y: number;
  } | null>(null);

  const toggleFullscreen = () => {
    setIsFullscreen((prev) => !prev);
  };

  useEffect(() => {
    // Trigger window resize so Three.js camera/renderer adjusts to fullscreen or exit
    const timer = setTimeout(() => {
      window.dispatchEvent(new Event("resize"));
    }, 100);
    return () => clearTimeout(timer);
  }, [isFullscreen]);

  const [visibleLayers, setVisibleLayers] = useState({
    collar: true,
    target: true,
    plan: true,
    surveys: true,
    straightPath: true,
    surface: true,
  });

  useEffect(() => {
    if (!containerRef.current) return;
    const container = containerRef.current;
    const width = container.clientWidth || 800;
    const height = container.clientHeight || 550;

    // Scene setup
    const scene = new THREE.Scene();
    scene.background = new THREE.Color(0xffffff);

    // Camera setup
    const camera = new THREE.PerspectiveCamera(45, width / height, 0.1, 2000);
    camera.position.set(70, 60, 90);

    // Renderer
    const renderer = new THREE.WebGLRenderer({ antialias: true });
    renderer.setSize(width, height);
    renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
    container.innerHTML = "";
    container.appendChild(renderer.domElement);

    // OrbitControls
    const controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.05;

    // Lighting
    const ambientLight = new THREE.AmbientLight(0xffffff, 0.85);
    scene.add(ambientLight);

    const dirLight = new THREE.DirectionalLight(0xffffff, 0.5);
    dirLight.position.set(50, 100, 50);
    scene.add(dirLight);

    // Collect all points to compute bounding box
    const allPoints: Point3D[] = [];

    if (collar) {
      allPoints.push({
        northing: Number(collar.northing),
        easting: Number(collar.easting),
        tvd: 0,
      });
    }

    if (target) {
      allPoints.push({
        northing: Number(target.northing),
        easting: Number(target.easting),
        tvd: Number(target.tvd),
      });
    }

    surveys.forEach((s) => {
      if (s.northing !== null && s.easting !== null && s.trueVerticalDepth !== null) {
        allPoints.push({
          northing: Number(s.northing),
          easting: Number(s.easting),
          tvd: Number(s.trueVerticalDepth),
        });
      }
    });

    if (straightPath && showStraightPath) {
      allPoints.push({
        northing: Number(straightPath.northing),
        easting: Number(straightPath.easting),
        tvd: Number(straightPath.tvd),
      });
    }

    if (allPoints.length === 0) {
      // Default empty grid
      allPoints.push({ northing: 0, easting: 0, tvd: 0 });
      allPoints.push({ northing: 100, easting: 100, tvd: 100 });
    }

    const minN = Math.min(...allPoints.map((p) => p.northing));
    const maxN = Math.max(...allPoints.map((p) => p.northing));
    const minE = Math.min(...allPoints.map((p) => p.easting));
    const maxE = Math.max(...allPoints.map((p) => p.easting));
    const minTVD = 0; // Surface level
    const maxTVD = Math.max(10, ...allPoints.map((p) => p.tvd));

    // Pad ranges so points aren't right on boundary
    const rangeN = Math.max(maxN - minN, 20);
    const rangeE = Math.max(maxE - minE, 20);
    const rangeTVD = Math.max(maxTVD - minTVD, 20);

    const padN = rangeN * 0.15;
    const padE = rangeE * 0.15;
    const padTVD = rangeTVD * 0.1;

    const bMinN = minN - padN;
    const bMaxN = maxN + padN;
    const bMinE = minE - padE;
    const bMaxE = maxE + padE;
    const bMinTVD = 0;
    const bMaxTVD = maxTVD + padTVD;

    // Normalization Box in 3D: width=50 (Easting, X), height=50 (TVD, Y down), depth=50 (Northing, Z)
    const BOX_X = 50; // Easting
    const BOX_Y = 50; // TVD (downward)
    const BOX_Z = 50; // Northing

    // Transformation: (Easting, TVD, Northing) -> (X, Y, Z) in Three.js
    // Three.js: X = Easting, Y = -TVD (depth down), Z = Northing
    const toThree = (e: number, tvd: number, n: number): THREE.Vector3 => {
      const x = ((e - bMinE) / (bMaxE - bMinE) - 0.5) * BOX_X;
      const y = (0.5 - (tvd - bMinTVD) / (bMaxTVD - bMinTVD)) * BOX_Y;
      const z = ((n - bMinN) / (bMaxN - bMinN) - 0.5) * BOX_Z;
      return new THREE.Vector3(x, y, z);
    };

    // 1. Draw 3D Bounding Cage / Grid Box
    const boxGeo = new THREE.BoxGeometry(BOX_X, BOX_Y, BOX_Z);
    const boxEdges = new THREE.EdgesGeometry(boxGeo);
    const boxMat = new THREE.LineBasicMaterial({ color: 0xd1d5db, linewidth: 1 });
    const boxWire = new THREE.LineSegments(boxEdges, boxMat);
    scene.add(boxWire);

    // 2. Draw Translucent Blue Surface Plane at TVD = 0
    if (visibleLayers.surface) {
      const surfaceY = BOX_Y / 2; // top of the box
      const surfaceGeo = new THREE.PlaneGeometry(BOX_X, BOX_Z);
      const surfaceMat = new THREE.MeshBasicMaterial({
        color: 0x93c5fd,
        transparent: true,
        opacity: 0.25,
        side: THREE.DoubleSide,
      });
      const surfaceMesh = new THREE.Mesh(surfaceGeo, surfaceMat);
      surfaceMesh.rotation.x = Math.PI / 2;
      surfaceMesh.position.y = surfaceY;
      scene.add(surfaceMesh);
    }

    // Grid lines on bottom and back walls
    const gridBack = new THREE.GridHelper(BOX_X, 10, 0xe5e7eb, 0xf3f4f6);
    gridBack.rotation.x = Math.PI / 2;
    gridBack.position.z = -BOX_Z / 2;
    scene.add(gridBack);

    const gridBottom = new THREE.GridHelper(BOX_X, 10, 0xe5e7eb, 0xf3f4f6);
    gridBottom.position.y = -BOX_Y / 2;
    scene.add(gridBottom);

    // 3. Draw Planned Hole Path (Black line from Collar to Target)
    if (collar && target && visibleLayers.plan) {
      const pCollar = toThree(Number(collar.easting), 0, Number(collar.northing));
      const pTarget = toThree(Number(target.easting), Number(target.tvd), Number(target.northing));

      const planGeo = new THREE.BufferGeometry().setFromPoints([pCollar, pTarget]);
      const planMat = new THREE.LineBasicMaterial({ color: 0x1f2937, linewidth: 2 });
      const planLine = new THREE.Line(planGeo, planMat);
      scene.add(planLine);
    }

    // 4. Draw Survey Data (Green points + trajectory line)
    if (surveys.length > 0 && visibleLayers.surveys) {
      const surveyPoints: THREE.Vector3[] = [];

      // Include collar as start point if survey doesn't have 0
      if (collar && (surveys[0]?.measuredDepth || 0) > 0) {
        surveyPoints.push(toThree(Number(collar.easting), 0, Number(collar.northing)));
      }

      surveys.forEach((s) => {
        if (s.northing !== null && s.easting !== null && s.trueVerticalDepth !== null) {
          const pt = toThree(Number(s.easting), Number(s.trueVerticalDepth), Number(s.northing));
          surveyPoints.push(pt);

          // Green sphere for each survey station
          const sphereGeo = new THREE.SphereGeometry(0.65, 12, 12);
          const sphereMat = new THREE.MeshStandardMaterial({
            color: 0x22c55e,
            roughness: 0.3,
            metalness: 0.1,
          });
          const sphere = new THREE.Mesh(sphereGeo, sphereMat);
          sphere.position.copy(pt);
          scene.add(sphere);
        }
      });

      if (surveyPoints.length > 1) {
        const pathGeo = new THREE.BufferGeometry().setFromPoints(surveyPoints);
        const pathMat = new THREE.LineBasicMaterial({ color: 0x15803d, linewidth: 2 });
        const pathLine = new THREE.Line(pathGeo, pathMat);
        scene.add(pathLine);
      }
    }

    // 5. Draw Straight Path To Target TVD (Red line)
    if (straightPath && showStraightPath && visibleLayers.straightPath) {
      let startPoint: THREE.Vector3 | null = null;
      if (surveys.length > 0) {
        const last = surveys[surveys.length - 1];
        if (last.northing !== null && last.easting !== null && last.trueVerticalDepth !== null) {
          startPoint = toThree(Number(last.easting), Number(last.trueVerticalDepth), Number(last.northing));
        }
      } else if (collar) {
        startPoint = toThree(Number(collar.easting), 0, Number(collar.northing));
      }

      if (startPoint) {
        const endPoint = toThree(Number(straightPath.easting), Number(straightPath.tvd), Number(straightPath.northing));

        const spGeo = new THREE.BufferGeometry().setFromPoints([startPoint, endPoint]);
        const spMat = new THREE.LineBasicMaterial({ color: 0xef4444, linewidth: 3 });
        const spLine = new THREE.Line(spGeo, spMat);
        scene.add(spLine);

        // Small red sphere at end
        const redSphereGeo = new THREE.SphereGeometry(0.75, 12, 12);
        const redSphereMat = new THREE.MeshStandardMaterial({ color: 0xdc2626 });
        const redSphere = new THREE.Mesh(redSphereGeo, redSphereMat);
        redSphere.position.copy(endPoint);
        scene.add(redSphere);
      }
    }

    // 6. Draw Collar (Magenta point at surface)
    if (collar && visibleLayers.collar) {
      const pCollar = toThree(Number(collar.easting), 0, Number(collar.northing));
      const collarGeo = new THREE.SphereGeometry(1.2, 16, 16);
      const collarMat = new THREE.MeshStandardMaterial({
        color: 0xd946ef, // Magenta
        roughness: 0.2,
      });
      const collarMesh = new THREE.Mesh(collarGeo, collarMat);
      collarMesh.position.copy(pCollar);
      scene.add(collarMesh);
    }

    // 7. Draw Target (Orange point at target TVD)
    if (target && visibleLayers.target) {
      const pTarget = toThree(Number(target.easting), Number(target.tvd), Number(target.northing));
      const targetGeo = new THREE.SphereGeometry(1.2, 16, 16);
      const targetMat = new THREE.MeshStandardMaterial({
        color: 0xf97316, // Orange
        roughness: 0.2,
      });
      const targetMesh = new THREE.Mesh(targetGeo, targetMat);
      targetMesh.position.copy(pTarget);
      scene.add(targetMesh);
    }

    // Camera initial position to look at center
    controls.target.set(0, 0, 0);
    controls.update();

    // Animation loop
    let animId: number;
    const animate = () => {
      animId = requestAnimationFrame(animate);
      controls.update();
      renderer.render(scene, camera);
    };
    animate();

    // Resize handler
    const handleResize = () => {
      if (!container) return;
      const w = container.clientWidth;
      const h = container.clientHeight;
      camera.aspect = w / h;
      camera.updateProjectionMatrix();
      renderer.setSize(w, h);
    };
    window.addEventListener("resize", handleResize);

    return () => {
      cancelAnimationFrame(animId);
      window.removeEventListener("resize", handleResize);
      renderer.dispose();
      if (container) container.innerHTML = "";
    };
  }, [collar, target, surveys, straightPath, showStraightPath, visibleLayers]);

  return (
    <div
      ref={chartWrapperRef}
      className={`relative bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden select-none ${
        isFullscreen
          ? "!fixed !inset-0 !z-50 !w-screen !h-screen !rounded-none !border-none !p-0"
          : className
      }`}
    >
      {/* 3D Canvas Container with touch-action: none */}
      <div
        ref={containerRef}
        className="w-full h-full min-h-[350px] md:min-h-[500px]"
        style={{ touchAction: "none" }}
      />

      {/* Top Floating Action Bar (Controls Hint, Legend Toggle, Fullscreen Toggle) */}
      <div className="absolute top-3 left-3 right-3 flex items-center justify-between pointer-events-none z-10 gap-2">
        {/* Controls hint */}
        <div className="bg-white/85 backdrop-blur-xs px-2.5 py-1 rounded-md border border-gray-200 text-[10px] md:text-xs text-gray-600 shadow-xs pointer-events-auto">
          <span className="hidden sm:inline">🖱️ Left click drag: Rotate · Right click: Pan · Scroll: Zoom</span>
          <span className="sm:hidden">👆 Drag: Rotate · ✌️ Pinch: Zoom</span>
        </div>

        {/* Action Buttons: Legend Toggle (mobile) + Fullscreen */}
        <div className="flex items-center gap-1.5 pointer-events-auto">
          <button
            onClick={() => setShowLegend((prev) => !prev)}
            className="sm:hidden bg-white/90 backdrop-blur-xs px-2 py-1 rounded-md border border-gray-200 text-xs font-medium text-gray-700 shadow-xs hover:bg-gray-50 transition"
            aria-label="Toggle Legend"
          >
            🏷️ Legend
          </button>

          <button
            onClick={toggleFullscreen}
            className="bg-white/90 backdrop-blur-xs px-2.5 py-1 rounded-md border border-gray-200 text-xs font-semibold text-gray-800 shadow-xs hover:bg-gray-50 transition flex items-center gap-1"
            title={isFullscreen ? "Exit Fullscreen" : "Fullscreen 3D View"}
          >
            <span>{isFullscreen ? "✕" : "⛶"}</span>
            <span className="hidden xs:inline">{isFullscreen ? "Exit" : "Fullscreen"}</span>
          </button>
        </div>
      </div>

      {/* Floating Legend Top Right (Collapsible on mobile, visible on desktop) */}
      <div
        className={`absolute top-12 sm:top-12 right-3 bg-white/90 backdrop-blur-xs p-2.5 sm:p-3 rounded-lg border border-gray-200 text-xs shadow-xs space-y-1.5 z-10 transition-all ${
          showLegend ? "block" : "hidden sm:block"
        }`}
      >
        {collar && (
          <div className="flex items-center gap-2">
            <span className="w-2.5 h-2.5 sm:w-3 sm:h-3 rounded-full bg-[#d946ef] inline-block shadow-xs shrink-0"></span>
            <span className="text-gray-700 font-medium text-[11px] sm:text-xs">Collar</span>
          </div>
        )}
        {target && (
          <div className="flex items-center gap-2">
            <span className="w-2.5 h-2.5 sm:w-3 sm:h-3 rounded-full bg-[#f97316] inline-block shadow-xs shrink-0"></span>
            <span className="text-gray-700 font-medium text-[11px] sm:text-xs">Target</span>
          </div>
        )}
        {collar && target && (
          <div className="flex items-center gap-2">
            <span className="w-3.5 h-0.5 bg-gray-900 inline-block shrink-0"></span>
            <span className="text-gray-700 font-medium text-[11px] sm:text-xs">Hole Plan</span>
          </div>
        )}
        <div className="flex items-center gap-2">
          <span className="w-2.5 h-2.5 sm:w-3 sm:h-3 rounded-full bg-[#22c55e] inline-block shadow-xs shrink-0"></span>
          <span className="text-gray-700 font-medium text-[11px] sm:text-xs">Survey Data</span>
        </div>
        {showStraightPath && straightPath && (
          <div className="flex items-center gap-2">
            <span className="w-3.5 h-0.5 bg-red-600 inline-block shrink-0"></span>
            <span className="text-gray-700 font-medium text-[11px] sm:text-xs">Straight Path To Target</span>
          </div>
        )}
      </div>

      {/* Bottom Red Warning Note (Matching original desktop app) */}
      <div className="absolute bottom-2 left-2 right-2 text-center pointer-events-none z-10">
        <p className="text-[10px] sm:text-xs font-semibold text-red-600/90 tracking-wide drop-shadow-xs px-2 py-0.5 bg-white/60 sm:bg-transparent rounded inline-block">
          Note: Scaling is not consistent across axis — The hole path may appear exaggerated!
        </p>
      </div>
    </div>
  );
}

