# SphereIQ — Web App Conversion Plan

**Source project:** `Ezy-Nav.csproj` (.NET 2.0 WinForms desktop app — directional drilling / borehole planner)  
**Web stack:** Next.js 14 App Router · TypeScript · Prisma · PostgreSQL (Neon) · NextAuth · Tailwind · Three.js (WebGL 3D) · Chart.js (2D)  
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
| Prisma schema (User, Hole, Collar, Survey, Target, Report, FileUpload, UserSettings) | ✅ | `prisma/schema.prisma` with `@default("RIG-01")` on `rigId` |
| NextAuth sign-in page | ✅ | `app/auth/signin/page.tsx` |
| NextAuth register page | ✅ | `app/auth/register/page.tsx` |
| Auth middleware (route protection) | ✅ | `middleware.ts` |
| Global navigation shell (sidebar) | ✅ | `app/components/SidebarNav.tsx` + `app/layout.tsx` |
| Settings save & delete account | ✅ | `app/api/settings/route.ts` (GET, PATCH, DELETE) + `app/settings/page.tsx` |

---

## 2. Hole Management Screen (Image 1 Parity)

| Item | Status | Notes |
|------|--------|-------|
| Top header | ✅ | "Hole Management: [Name/ID]" with book icon |
| Action buttons toolbar | ✅ | Back Welcome Screen, Edit Details, Edit Survey Data, Import Survey Data, Hole Navigation (green) |
| Tabbed details panel | ✅ | "Hole Details" and "Chart Options" tabs |
| Identification card | ✅ | Hole ID, RigID, Location, Area, Client, Surveying Contractor, Drilling Contractor, Created Date |
| Collar card | ✅ | Easting, Northing, Elevation + quick edit modal |
| Target card | ✅ | Easting, Northing, TVD + quick edit modal |
| 3D WebGL Chart integration | ✅ | Three.js canvas with Collar (magenta), Target (orange), Hole Plan (black line), Survey Data (green points) |
| Scaling warning banner | ✅ | Verbatim desktop text: *"Note: Scaling is not consistant across axis - The hole path may appear exaggerated!"* |
| Sphere IQ brand mark | ✅ | 3D Intelligence Platform watermark badge |

---

## 3. Hole Navigation & Planning (Image 2 Parity)

| Item | Status | Notes |
|------|--------|-------|
| Hole Navigation screen (`/holes/[id]/navigation`) | ✅ | High-fidelity port of `DrillHolePlanningScreen.cs` |
| Severe / Extreme dogleg badges | ✅ | Real-time threshold warnings (>3° Severe orange, >7° Extreme red) with radar indicator |
| Action toolbar | ✅ | Hole Management, Build To Target, Trend Analysis, Extrapolate, Sidetrack Hole, Advanced, Report |
| Drill hole depth card | ✅ | Real-time MD & TVD of active bottom-hole assembly |
| End of hole distance card | ✅ | North, East, Horizontal Distance (HD), and TVD deltas to target |
| Straight path to target TVD card | ✅ | Projected end-of-hole straight path at Target TVD (North, East, HD) |
| Minimum dogleg to target | ✅ | Calculated minimum curvature dogleg rate (°/30m) required to hit target |
| Target & Collar coordinates | ✅ | Display of collar and target reference points |
| 3D visualization | ✅ | Embedded 3D viewer rendering the red Straight Path to Target line alongside plan and surveys |

---

## 4. 3D WebGL Graphics (`app/components/Borehole3DChart.tsx`)

| Item | Status | Notes |
|------|--------|-------|
| Three.js & OrbitControls integration | ✅ | High performance 3D trajectory engine |
| Coordinate normalization | ✅ | Auto-centers bounding box with proper vertical aspect ratio |
| Collar marker | ✅ | Magenta sphere with coordinate hover label |
| Target marker | ✅ | Orange sphere with coordinate hover label |
| Hole Plan line | ✅ | Black line connecting Collar (0,0,0) to Target |
| Survey trajectory | ✅ | Green line connecting computed survey stations with sphere nodes |
| Straight path line | ✅ | Red line projecting from last survey to target depth |
| Surface plane | ✅ | Translucent blue ground plane at TVD = 0 |
| Bounding grid cage | ✅ | 3D cubic coordinate frame with axis tick labels |
| Mouse interactions | ✅ | Left-drag rotate, right-drag pan, wheel zoom |

---

## 5. Surveys CRUD & Trajectory Calculation

| Item | Status | Notes |
|------|--------|-------|
| First survey station (MD = 0) | ✅ | Validated properly with `z.coerce.number().min(0)` |
| `GET/POST /api/holes/[id]/surveys` | ✅ | Ordered by measured depth |
| `PUT/DELETE /api/holes/[id]/surveys/[surveyId]` | ✅ | Fully editable stations; auto-recalculates hole trajectory on change |
| Survey entry / edit / delete page (`/holes/[id]/survey-entry`) | ✅ | Full CRUD UI with step="any" precision and comma-to-dot sanitization |
| Survey calculate API (`POST /api/survey/calculate`) | ✅ | Minimum curvature trajectory math via `lib/survey-math.ts` |

---

## 6. Directional Drilling Tools

| Item | Status | Notes |
|------|--------|-------|
| Trend Analysis (`/trend-analysis/[id]`) | ✅ | MD range filter, dogleg severity statistics, and trend summaries |
| Extrapolation (`/extrapolate/[id]`) | ✅ | Last survey reference card, target MD & VS azimuth inputs, projected coordinates output |
| Build to Target (`/build-to-target`) | ✅ | Minimum build rate & hold path calculation, target form, visual results & warnings |
| SideTrack Hole (`/holes/[id]/sidetrack`) | ✅ | Kickoff MD input, inherits surveys, copies collar/target, creates branched hole |
| CSV Import (`/import`) | ✅ | Preview table, automatic column detection, interactive column mapping dropdowns |

---

## 7. Reports & Documentation

| Item | Status | Notes |
|------|--------|-------|
| `GET/POST /api/reports` | ✅ | CRUD reports endpoint |
| Reports list page (`/reports`) | ✅ | Report browser by hole and type |
| Report detail (`/reports/[id]`) | ✅ | PDF viewer / download launcher |
| PDF generation | ⚠️ | PDF url schema present; printable web views available |


---

## 8. Original Borehole File (.ezn) & CSV Import / Export

| Item | Status | Notes |
|------|--------|-------|
| EZN parser & serializer (`lib/ezn-format.ts`) | ✅ | Full `EZN1\n` header + JSON `HoleFileDto` parsing and formatting |
| Universal NRBF binary parser (`lib/nrbf-parser.ts`) | ✅ | Pure TypeScript parser for legacy .NET 2.0 BinaryFormatter `.ezn` files (Demo001, Demo002, Demo003) |
| Dual format detection in `parseEznFile` | ✅ | Automatically detects binary NRBF or EZN1 JSON format |
| ArrayBuffer binary upload handling | ✅ | Client `readAsArrayBuffer` + API `file.arrayBuffer()` without UTF-8 corruption |
| Import .ezn API (`POST /api/import/ezn`) | ✅ | 1-click borehole creation (identification, collar, target, surveys + trajectory calc) |
| Export .ezn API (`GET /api/holes/[id]/export/ezn`) | ✅ | Generates byte-compatible `.ezn` file ready for original desktop app |
| Export CSV API (`GET /api/holes/[id]/export/csv`) | ✅ | Generates CSV with metadata header comments and survey coordinates |
| Import Page UI (`/import`) | ✅ | Dual tabs: "SphereIQ / Ezy-Nav File (.ezn)" and "Survey Stations (.csv)" |
| Hole Management Export UI (`/holes/[id]`) | ✅ | Direct 1-click download buttons for `.ezn` and `.csv` |
| Hole Navigation Export UI (`/holes/[id]/navigation`) | ✅ | Quick export links in action menu |
| Holes list quick-import (`/holes`) | ✅ | Direct shortcut button to `Import .ezn` |

---

## 9. Progressive Web App (PWA) & Mobile Responsiveness

| Item | Status | Notes |
|------|--------|-------|
| Web App Manifest (`app/manifest.ts`) | ✅ | Standalone PWA configuration with theme/background colors |
| PWA Icons & Favicon | ✅ | 192x192, 512x512, maskable icons & apple-touch-icon in `public/icons/` |
| Viewport & iOS Meta Tags (`app/layout.tsx`) | ✅ | `viewport` export, `apple-mobile-web-app-capable`, safe area insets |
| Service Worker (`public/sw.js`) | ✅ | Offline caching of core assets, Three.js shell, and offline notice |
| Service Worker Registration & In-App Install Prompt | ✅ | `app/components/PwaRegister.tsx`: in-app "Install App" button & iOS guide |
| Mobile Navigation Bar & Drawer | ✅ | `app/components/SidebarNav.tsx`: sticky topbar with hamburger (☰) + slide-out drawer on `< md` screens |
| Responsive Layout (`app/layout.tsx`) | ✅ | `md:ml-56` on desktop, `pt-14 md:pt-0` on mobile |
| Hole Management Mobile UI (`/holes/[id]`) | ✅ | Mobile segmented view `[🧭 3D Trajectory]` / `[📋 Hole Details]` + scrollable toolbar ribbon |
| Hole Navigation Mobile UI (`/holes/[id]/navigation`) | ✅ | Mobile segmented view `[🧭 3D Trajectory]` / `[📊 Navigation Status]` |
| 3D Fullscreen Toggle & Touch Optimization | ✅ | Fullscreen toggle button (`⛶`) + `touch-action: none` orbit controls + mobile collapsible legend |
| Survey Table Mobile Optimization | ✅ | Sticky frozen `MD` column + `inputMode="decimal"` for mobile keypads |

