import { useState } from "react";
import { useRouter } from "next/navigation";
import { getServerAuthSession } from "@/lib/auth";
import { prisma } from "@/lib/prisma";

export default async function CreateHolePage() {
  const session = await getServerAuthSession();
  if (!session?.user?.id) {
    return null;
  }

  const router = useRouter();
  const [holeId, setHoleId] = useState("");
  const [name, setName] = useState("");
  const [status, setStatus] = useState("planned");
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setIsLoading(true);

    try {
      const res = await fetch("/api/holes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ holeId, name, status }),
      });

      if (!res.ok) {
        const err = await res.json();
        setError(err.error || "Failed to create hole");
        return;
      }

      const data = await res.json();
      router.push(`/holes/${data.hole.id}`);
    } catch {
      setError("Something went wrong");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="px-6 py-8">
        <div className="max-w-2xl">
          <Link href="/holes" className="text-sm text-blue-600 hover:underline mb-4 inline-block">
            ← Back to holes
          </Link>

          <h1 className="text-3xl font-bold text-gray-900 mb-2">Create New Hole</h1>
          <p className="text-gray-600 mb-8">
            Enter the borehole details to begin planning
          </p>

          <form onSubmit={handleSubmit} className="bg-white p-8 rounded-xl shadow-sm border border-gray-200 space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Hole ID
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., H-001"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={holeId}
                  onChange={(e) => setHoleId(e.target.value)}
                />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Name
                </label>
                <input
                  type="text"
                  required
                  placeholder="e.g., Exploration Hole 1"
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </div>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Status
              </label>
              <select
                value={status}
                onChange={(e) => setStatus(e.target.value as any)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
              >
                <option value="planned">Planned</option>
                <option value="drilling">Drilling</option>
                <option value="completed">Completed</option>
                <option value="archived">Archived</option>
              </select>
            </div>

            {error && (
              <div className="text-sm text-red-600 bg-red-50 px-3 py-2 rounded-md border border-red-100">
                {error}
              </div>
            )}

            <button
              type="submit"
              disabled={isLoading}
              className="w-full px-5 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 disabled:opacity-50 transition"
            >
              {isLoading ? "Creating..." : "Create Hole"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
