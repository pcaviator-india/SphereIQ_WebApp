# SphereIQ — Web App Conversion Plan

**Source project:** `Ezy-Nav.csproj` (.NET 2.0 WinForms desktop app — directional drilling / borehole planner)  
**Web stack:** Next.js 14 App Router · TypeScript · Prisma · PostgreSQL · NextAuth · Tailwind · Chart.js  
**Last updated:** 2026-10-28

---

## Legend

| Symbol | Meaning |
|--------|---------|
| ✅ | Done, tested, and working |
| ⚠️ | Exists but has minor items to polish |
| 🔴 | Critical bug — app-breaking |
| ❌ | Not started |

---

## 1. Infrastructure & Auth

| Item | Status | Notes |
|------|--------|-------|
| Next.js 14 App Router scaffold | ✅ | `app/`, `lib/`, `types/`, `prisma/` |
| Prisma schema (User, Hole, Collar, Survey, Target, Report, FileUpload, UserSettings) | ✅ | `prisma/schema.prisma` |
| NextAuth sign-in page | ✅ | `app/auth/signin/page.tsx` |
| NextAuth register page | ✅ | `app/auth/register/page.tsx` |
| Auth middleware (route protection) | ✅ | `middleware.ts` |
| Global navigation shell (sidebar) | ✅ | `app/components/SidebarNav.tsx` + `app/layout.tsx` |
| Settings save & delete account | ✅ | `app/api/settings/route.ts` (GET, PATCH, DELETE) + `app/settings/page.tsx` |

---

## 2. Holes — CRUD

| Item | Status | Notes |
|------|--------|-------|
| `GET /api/holes` — list user holes | ✅ | Includes collar, target, surveys |
| `POST /api/holes` — create hole | ✅ | Validated with zod |
| `GET /api/holes/[id]` — get hole | ✅ | Includes collar, target, surveys |
| `PUT /api/holes/[id]` — update hole | ✅ | Schema supports all hole fields |
| `DELETE /api/holes/[id]` — delete hole | ✅ | Cascades delete in database |
| Holes list page (`/holes`) | ✅ | `app/holes/page.tsx` |
| Create hole page (`/holes/create`) | ✅ | `app/holes/create/page.tsx` |
| **Hole detail page (`/holes/[id]`)** | ✅ | Full layout, collar/target editor, quick actions, delete |
| Hole edit page (`/holes/[id]/edit`) | ✅ | Pre-filled form updating all fields |
| Hole delete UI | ✅ | Working delete button with confirm dialog |

---

## 3. Collar & Target

| Item | Status | Notes |
|------|--------|-------|
| `GET/POST /api/holes/[id]/collar` | ✅ | Upsert support |
| `GET/POST /api/holes/[id]/target` | ✅ | Upsert support |
| Collar entry form (inside hole detail) | ✅ | Direct editing & updating |
| Target entry form (inside hole detail) | ✅ | Direct editing & updating |

---

## 4. Surveys

| Item | Status | Notes |
|------|--------|-------|
| `GET/POST /api/holes/[id]/surveys` | ✅ | Ordered by measured depth |
| `PUT/DELETE /api/holes/[id]/surveys/[surveyId]` | ✅ | Edit/delete individual survey records |
| Survey entry / edit / delete page (`/holes/[id]/survey-entry`) | ✅ | Full CRUD UI |
| Survey calculate API (`POST /api/survey/calculate`) | ✅ | Min-curvature trajectory via `lib/survey-math.ts` |

---

## 5. Charts

| Item | Status | Notes |
|------|--------|-------|
| Chart.js registered in project | ✅ | Line & Scatter chart support |
| `lib/chart-utils.ts` | ✅ | Dataset builders |
| Charts page (`/holes/[id]/charts`) | ✅ | Fixed client/server bug |
| TVD vs MD chart | ✅ | Inverted depth scale (depth goes down) |
| Dogleg severity chart | ✅ | DLS (°/30m) line plot |
| Plan view chart (Northing vs Easting) | ✅ | Horizontal trajectory projection |
| Vertical section chart (VS vs TVD) | ✅ | Vertical section projection |
| Data table | ✅ | Full calculated trajectory table (MD, Inc, Az, TVD, N, E, VS, DLS) |

---

## 6. Trend Analysis

| Item | Status | Notes |
|------|--------|-------|
| `POST /api/trend-analysis` | ✅ | Calculates range stats, averages, DLS trends |
| Trend analysis page (`/trend-analysis/[id]`) | ✅ | Range filter (From MD / To MD), analysis runner, stats card & DLS trend table |

---

## 7. Extrapolation

| Item | Status | Notes |
|------|--------|-------|
| `POST /api/extrapolate` | ✅ | Linear/tangential projection with VS |
| Extrapolation page (`/extrapolate/[id]`) | ✅ | Last survey reference card, target MD & VS azimuth inputs, calculated coordinates output |

---

## 8. Build to Target

| Item | Status | Notes |
|------|--------|-------|
| `POST /api/build-to-target` | ✅ | Minimum build rate & hold path calculation |
| Build-to-target page (`/build-to-target`) | ✅ | Fixed React hooks order, added borehole picker, target form, visual results & warnings |

---

## 9. SideTrack Hole

| Item | Status | Notes |
|------|--------|-------|
| SideTrack hole page (`/holes/[id]/sidetrack`) | ✅ | Kickoff MD input, inherits surveys, copies collar/target, creates branched hole |

---

## 10. Import / File Upload

| Item | Status | Notes |
|------|--------|-------|
| `POST /api/import/csv` | ✅ | Supports both FormData and JSON, custom column indexing |
| CSV import frontend (`/import`) | ✅ | File preview, automatic column detection, interactive column mapping dropdowns |
| `POST /api/upload` | ✅ | File upload backend API |

---

## 11. Reports

| Item | Status | Notes |
|------|--------|-------|
| `GET/POST /api/reports` | ✅ | Stub CRUD |
| `GET /api/reports/[id]` | ✅ | |
| Reports list page (`/reports`) | ✅ | |
| PDF generation | ⚠️ | PDF url schema present; server-side PDF generator can be wired to PDF library (e.g. `@react-pdf/renderer` or `pdfkit`) |

---

## 12. Dashboard & Navigation

| Item | Status | Notes |
|------|--------|-------|
| Dashboard (`/`) | ✅ | Metric counters, recent boreholes, quick tools, landing page for guests |
| Sidebar Navigation | ✅ | Links to Dashboard, Boreholes, Import CSV, Reports, Settings + Sign Out |
