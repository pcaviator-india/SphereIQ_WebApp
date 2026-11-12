"use client";

import { useRouter } from "next/navigation";

interface HoleDeleteButtonProps {
  holeId: string;
  holeName: string;
}

export function HoleDeleteButton({ holeId, holeName }: HoleDeleteButtonProps) {
  const router = useRouter();

  async function handleDelete() {
    if (
      !window.confirm(
        `Are you sure you want to delete the borehole "${holeName}"?\n\nThis will also delete all associated surveys, collar, target, and file data.`
      )
    ) {
      return;
    }

    try {
      const res = await fetch(`/api/holes/${holeId}`, {
        method: "DELETE",
      });

      if (!res.ok) throw new Error("Delete failed");

      router.refresh();
    } catch {
      alert("Failed to delete borehole. Please try again.");
    }
  }

  return (
    <button
      onClick={handleDelete}
      className="p-2 text-gray-400 hover:text-red-600 hover:bg-red-50 rounded-lg transition"
      title="Delete borehole"
      aria-label="Delete borehole"
    >
      <span className="text-sm font-bold">🗑️</span>
    </button>
  );
}