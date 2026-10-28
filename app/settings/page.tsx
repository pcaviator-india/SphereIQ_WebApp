import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export default async function SettingsPage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  let userSettings = await prisma.userSettings.findUnique({
    where: { userId: session.user.id },
  });

  if (!userSettings) {
    userSettings = await prisma.userSettings.create({
      data: { userId: session.user.id, theme: "light", units: "metric" },
    });
  }

  const user = await prisma.user.findUnique({
    where: { id: session.user.id },
  });

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-2xl mx-auto">
          <h1 className="text-3xl font-bold text-gray-900 mb-8">Settings</h1>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Account</h2>
            <div className="space-y-4">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-gray-900 font-medium">{user?.name || "No name"}</p>
                  <p className="text-sm text-gray-500">{user?.email}</p>
                </div>
                <span
                  className={`inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-xs font-medium ${
                    user?.role === "admin"
                      ? "bg-blue-100 text-blue-700"
                      : "bg-gray-100 text-gray-700"
                  }`}
                >
                  {user?.role || "user"}
                </span>
              </div>
              <div className="flex items-center justify-between">
                <p className="text-sm text-gray-500">Member since</p>
                <p className="text-sm text-gray-500">
                  {new Date(user?.createdAt || Date.now()).toLocaleDateString()}
                </p>
              </div>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 mb-6">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Preferences</h2>
            <div className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Theme
                </label>
                <select
                  value={userSettings.theme}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
                >
                  <option value="light">Light</option>
                  <option value="dark">Dark</option>
                </select>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Units
                </label>
                <select
                  value={userSettings.units}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
                >
                  <option value="metric">Metric (meters)</option>
                  <option value="imperial">Imperial (feet)</option>
                </select>
              </div>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
            <h2 className="text-xl font-bold text-gray-900 mb-4">Danger Zone</h2>
            <div className="space-y-3">
              <button
                className="w-full px-4 py-2 text-left text-red-600 font-medium hover:text-red-700 hover:bg-red-50 rounded-lg transition"
              >
                Delete Account
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
