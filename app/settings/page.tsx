"use client";

import { useState, useEffect } from "react";
import { signOut } from "next-auth/react";

interface UserProfile {
  name: string | null;
  email: string | null;
  role: string;
  createdAt: string;
}

interface SettingsState {
  theme: "light" | "dark";
  units: "metric" | "imperial";
}

export default function SettingsPage() {
  const [user, setUser] = useState<UserProfile | null>(null);
  const [theme, setTheme] = useState<"light" | "dark">("light");
  const [units, setUnits] = useState<"metric" | "imperial">("metric");
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [message, setMessage] = useState<{ type: "success" | "error"; text: string } | null>(null);

  useEffect(() => {
    fetchSettings();
  }, []);

  const fetchSettings = async () => {
    try {
      const res = await fetch("/api/settings");
      if (!res.ok) throw new Error("Failed to load settings");
      const data = await res.json();
      if (data.user) setUser(data.user);
      if (data.settings) {
        setTheme(data.settings.theme || "light");
        setUnits(data.settings.units || "metric");
      }
    } catch {
      setMessage({ type: "error", text: "Failed to load preferences" });
    } finally {
      setIsLoading(false);
    }
  };

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setMessage(null);

    try {
      const res = await fetch("/api/settings", {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ theme, units }),
      });

      if (!res.ok) throw new Error("Failed to save");
      setMessage({ type: "success", text: "Preferences saved successfully!" });
    } catch {
      setMessage({ type: "error", text: "Error updating preferences" });
    } finally {
      setIsSaving(false);
    }
  };

  const handleDeleteAccount = async () => {
    if (
      !confirm(
        "Are you sure you want to permanently delete your account and all associated holes, surveys, and reports? This action cannot be undone."
      )
    ) {
      return;
    }

    try {
      const res = await fetch("/api/settings", { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      signOut({ callbackUrl: "/auth/signin" });
    } catch {
      alert("Failed to delete account. Please try again.");
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <p className="text-gray-500">Loading settings...</p>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-2xl mx-auto">
          <h1 className="text-3xl font-bold text-gray-900 mb-8">Settings</h1>

          {message && (
            <div
              className={`mb-6 p-4 rounded-xl text-sm border ${
                message.type === "success"
                  ? "bg-green-50 border-green-200 text-green-700"
                  : "bg-red-50 border-red-200 text-red-700"
              }`}
            >
              {message.text}
            </div>
          )}

          {/* Account Profile Card */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Account</h2>
            <div className="space-y-4">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-gray-900 font-medium">{user?.name || "No name set"}</p>
                  <p className="text-sm text-gray-500">{user?.email}</p>
                </div>
                <span
                  className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
                    user?.role === "admin"
                      ? "bg-purple-100 text-purple-700"
                      : "bg-blue-100 text-blue-700"
                  }`}
                >
                  {user?.role || "user"}
                </span>
              </div>
              <div className="flex items-center justify-between pt-2 border-t border-gray-100">
                <p className="text-sm text-gray-500">Member since</p>
                <p className="text-sm text-gray-700 font-medium">
                  {user?.createdAt ? new Date(user.createdAt).toLocaleDateString() : "—"}
                </p>
              </div>
            </div>
          </div>

          {/* Preferences Form */}
          <form onSubmit={handleSave} className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Preferences</h2>
            <div className="space-y-5">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Appearance Theme
                </label>
                <select
                  value={theme}
                  onChange={(e) => setTheme(e.target.value as "light" | "dark")}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white text-sm"
                >
                  <option value="light">Light</option>
                  <option value="dark">Dark</option>
                </select>
                <p className="text-xs text-gray-500 mt-1">Select application color scheme.</p>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Measurement Units
                </label>
                <select
                  value={units}
                  onChange={(e) => setUnits(e.target.value as "metric" | "imperial")}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white text-sm"
                >
                  <option value="metric">Metric (Meters, °/30m)</option>
                  <option value="imperial">Imperial (Feet, °/100ft)</option>
                </select>
                <p className="text-xs text-gray-500 mt-1">Default calculation unit system.</p>
              </div>

              <div className="pt-2">
                <button
                  type="submit"
                  disabled={isSaving}
                  className="px-5 py-2.5 bg-blue-600 text-white rounded-lg text-sm font-medium hover:bg-blue-700 disabled:opacity-50 transition"
                >
                  {isSaving ? "Saving..." : "Save Preferences"}
                </button>
              </div>
            </div>
          </form>

          {/* Danger Zone */}
          <div className="bg-white p-6 rounded-xl shadow-sm border border-red-200">
            <h2 className="text-xl font-bold text-red-600 mb-2">Danger Zone</h2>
            <p className="text-sm text-gray-500 mb-4">
              Permanently delete your account, saved boreholes, trajectory calculations, and uploaded files.
            </p>
            <button
              onClick={handleDeleteAccount}
              className="px-4 py-2 bg-red-50 text-red-600 border border-red-200 rounded-lg text-sm font-medium hover:bg-red-100 hover:text-red-700 transition"
            >
              Delete Account
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
