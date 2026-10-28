export default function HomePage() {
  return (
    <main className="min-h-screen flex flex-col items-center justify-center p-4">
      <div className="text-center max-w-2xl">
        <h1 className="text-4xl font-bold text-gray-900 mb-2">SphereIQ</h1>
        <p className="text-lg text-gray-600 mb-8">
          Directional drilling / borehole planning — web application
        </p>
        <div className="flex flex-col sm:flex-row gap-4 justify-center">
          <a
            href="/auth/signin"
            className="px-6 py-3 bg-blue-600 text-white rounded-lg font-medium hover:bg-blue-700 transition"
          >
            Sign in
          </a>
          <a
            href="/auth/register"
            className="px-6 py-3 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50 transition"
          >
            Create account
          </a>
        </div>
        <p className="mt-8 text-sm text-gray-500">
          Already have an account? Sign in above, or create a new account to get
          started.
        </p>
      </div>
    </main>
  );
}
