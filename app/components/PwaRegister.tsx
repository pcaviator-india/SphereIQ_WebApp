"use client";

import { useEffect, useState } from "react";

interface BeforeInstallPromptEvent extends Event {
  prompt: () => Promise<void>;
  userChoice: Promise<{ outcome: "accepted" | "dismissed" }>;
}

export default function PwaRegister() {
  const [deferredPrompt, setDeferredPrompt] = useState<BeforeInstallPromptEvent | null>(null);
  const [isInstallable, setIsInstallable] = useState(false);
  const [isInstalled, setIsInstalled] = useState(false);
  const [isOffline, setIsOffline] = useState(false);
  const [isIos, setIsIos] = useState(false);
  const [showIosPrompt, setShowIosPrompt] = useState(false);

  useEffect(() => {
    // 1. Register Service Worker
    if (typeof window !== "undefined" && "serviceWorker" in navigator) {
      navigator.serviceWorker
        .register("/sw.js")
        .then((reg) => {
          console.log("[PWA] Service Worker registered with scope:", reg.scope);
        })
        .catch((err) => {
          console.warn("[PWA] Service Worker registration failed:", err);
        });
    }

    // 2. Check if already installed in standalone mode
    if (typeof window !== "undefined") {
      const isStandalone =
        window.matchMedia("(display-mode: standalone)").matches ||
        (window.navigator as any).standalone === true;
      setIsInstalled(isStandalone);

      // Check if iOS
      const ua = window.navigator.userAgent.toLowerCase();
      const isIosDevice = /iphone|ipad|ipod/.test(ua);
      setIsIos(isIosDevice);

      // Listen for beforeinstallprompt (Android / Chrome / Edge)
      const handleBeforeInstall = (e: Event) => {
        e.preventDefault();
        setDeferredPrompt(e as BeforeInstallPromptEvent);
        setIsInstallable(true);
      };

      window.addEventListener("beforeinstallprompt", handleBeforeInstall);

      // Listen for online/offline status
      setIsOffline(!navigator.onLine);
      const handleOnline = () => setIsOffline(false);
      const handleOffline = () => setIsOffline(true);
      window.addEventListener("online", handleOnline);
      window.addEventListener("offline", handleOffline);

      return () => {
        window.removeEventListener("beforeinstallprompt", handleBeforeInstall);
        window.removeEventListener("online", handleOnline);
        window.removeEventListener("offline", handleOffline);
      };
    }
  }, []);

  const handleInstallClick = async () => {
    if (deferredPrompt) {
      await deferredPrompt.prompt();
      const choice = await deferredPrompt.userChoice;
      if (choice.outcome === "accepted") {
        setIsInstalled(true);
        setIsInstallable(false);
      }
      setDeferredPrompt(null);
    } else if (isIos) {
      setShowIosPrompt(true);
    }
  };

  return (
    <>
      {/* Offline banner */}
      {isOffline && (
        <div className="fixed top-0 left-0 right-0 z-50 bg-amber-600 text-white text-xs font-semibold py-1 px-4 text-center shadow-md flex items-center justify-center gap-2">
          <span>⚠️</span>
          <span>You are currently working offline. Cached data and trajectory tools remain accessible.</span>
        </div>
      )}

      {/* Floating Install Prompt Banner (if installable and not yet installed) */}
      {(isInstallable || (isIos && !isInstalled)) && !isInstalled && (
        <div className="fixed bottom-4 right-4 z-50">
          <button
            onClick={handleInstallClick}
            className="flex items-center gap-2 px-3.5 py-2.5 rounded-full bg-blue-600 hover:bg-blue-700 text-white text-xs font-bold shadow-lg hover:shadow-xl transition transform active:scale-95 border border-blue-400"
            title="Install SphereIQ on your device"
          >
            <span className="text-base">📲</span>
            <span>Install App</span>
          </button>
        </div>
      )}

      {/* iOS Instructions Modal */}
      {showIosPrompt && (
        <div className="fixed inset-0 z-50 bg-black/60 backdrop-blur-xs flex items-end sm:items-center justify-center p-4">
          <div className="bg-white rounded-2xl max-w-sm w-full p-6 text-center shadow-2xl border border-gray-100">
            <div className="w-12 h-12 rounded-full bg-blue-50 text-blue-600 flex items-center justify-center mx-auto mb-3 text-2xl">
              📲
            </div>
            <h3 className="text-lg font-bold text-gray-900 mb-1">Install SphereIQ</h3>
            <p className="text-xs text-gray-600 mb-4 leading-relaxed">
              Install this app on your iPhone or iPad for fullscreen access and offline field use:
            </p>
            <ol className="text-xs text-left text-gray-700 space-y-2.5 mb-5 bg-gray-50 p-4 rounded-xl border border-gray-200">
              <li className="flex items-center gap-2">
                <span className="font-bold text-blue-600">1.</span> Tap the <strong className="inline-flex items-center gap-1 font-semibold">Share <span className="text-sm">⎋</span></strong> button in Safari's bottom toolbar.
              </li>
              <li className="flex items-center gap-2">
                <span className="font-bold text-blue-600">2.</span> Scroll down and tap <strong className="inline-flex items-center gap-1 font-semibold">Add to Home Screen ⊞</strong>.
              </li>
              <li className="flex items-center gap-2">
                <span className="font-bold text-blue-600">3.</span> Tap <strong className="font-semibold text-blue-600">Add</strong> in the top right corner.
              </li>
            </ol>
            <button
              onClick={() => setShowIosPrompt(false)}
              className="w-full py-2.5 bg-slate-900 hover:bg-slate-800 text-white text-xs font-semibold rounded-xl transition"
            >
              Got it
            </button>
          </div>
        </div>
      )}
    </>
  );
}
