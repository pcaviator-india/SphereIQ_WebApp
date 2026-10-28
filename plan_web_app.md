# SphereIQ / Ezy-Nav — Web App Conversion Plan

**Source project:** `Ezy-Nav.csproj` (C# Windows Forms desktop application)  
**Location:** `c:\Users\Info-Tech\Downloads\recon dell backup\SphereIQ MIT License - WebApp\`  
**Status:** Analysis only — no code written  
**Verdict:** ✅ **Feasible to convert to a web app, but it is effectively a rebuild/rewrite, not a direct port.**

---

## 1. Executive Summary

The existing codebase is a **.NET Framework 2.0 Windows Forms (WinForms) desktop application** (`OutputType=WinExe`, `UseWindowsForms=True`, `[STAThread] Main`, `Application.Run(mainForm)`). It is a directional-drilling / borehole planning tool (SphereIQ / EzyPlan) built around a wizard-style UI (`Procedure_*` + `Body_*` + `Popup_*` pattern) with local file access, USB drive detection, GDI+ charting, and several proprietary third-party DLLs.

**Can it be converted to a web app?** Yes.  
**Is it a drop-in conversion?** No. WinForms cannot run in a browser. The UI layer must be completely rewritten. Much of the business logic (survey calculations, drilling math, report generation, file parsing) can be extracted and reused if it is decoupled from the UI.

**Recommended approach:** Re-architect as a **client–server web application** with an ASP.NET Core API backend and a modern frontend (Blazor, React, Angular, or Vue). The C# business logic can be ported to .NET 6/8/9 class libraries; the WinForms UI is replaced wholesale.

---

## 2. Current Architecture Analysis

### 2.1 Project file (`Ezy-Nav.csproj`)
| Setting | Value | Implication |
|---|---|---|
| OutputType | `WinExe` | Desktop application with a window |
| UseWindowsForms | `True` | Heavily tied to `System.Windows.Forms` |
| TargetFramework | `net20` | .NET Framework 2.0 (very old; will need retargeting) |
| PlatformTarget | `x86` | 32-bit only |
| LangVersion | `14.0` | Modern C# syntax despite old target framework |

### 2.2 Entry point (`Program.cs`)
- Single-instance enforcement via named `Mutex` (`PCAVIATOR SphereIQ`) — desktop-only concept
- `Splash` screen (synchronous loading form)
- Launches `Form_MainForm` and runs the WinForms message loop
- Accepts command-line args (file path)

### 2.3 Layering & naming convention
The app uses a mixed UI/logic pattern:

| Folder/Prefix | Role | Contains |
|---|---|---|
| `Procedure_*.cs` | "Controller" / workflow | Implements `IEzyPlanProcedure`; holds navigation state, calls business logic, AND directly manipulates WinForms `Panel`, `Label`, `Chart`, `Control` objects |
| `Body_*.cs` | Views / UserControls | `Body_Welcome`, `Body_DrillHolePlanning`, `Body_CreateNewHole_EnterDetails`, etc. |
| `Control_*.cs` | Custom toolbars/controls | `Toolbar_DrillHolePlanning`, `Control_DriveBox`, `Control_ExtrapolatedDataGrid` |
| `Popup_*.cs` | Modal dialogs | `Popup_MessageBox_OK`, `Popup_ReportPreview`, `Popup_Extrapolate`, etc. |
| `Report_*.cs` | Reporting | `Report_Chart`, `Report_BuildResults`, `Report_HoleStatus`, etc. (GDI+ `Bitmap`, `PictureBox`) |
| `Info_*.cs` | Data/info panels | `Info_Survey`, `Info_Depth`, `Info_TrendAnalysisResults`, etc. |
| `Layout_*.cs` | UI layout helpers | `Layout_Body_Wizard`, `Layout_Popup_Message`, etc. |
| `Utility.cs` | Shared helpers + UI color logic | Parsing, formatting, and direct WinForms color/focus manipulation |

### 2.4 External dependencies (DLLs referenced from outside the project)
These are referenced via `..\rstudio\2iC Australia\Ezy-Nav\` (compiled binaries confirmed present locally; **no source code visible**):

| DLL | Likely Purpose | Web Impact |
|---|---|---|
| `EzyPlanData.dll` | Data model (drill hole, survey, collar, target) | Needs source or replacement |
| `EzyPlanStorage.dll` | File I/O for hole files | Direct file system access — must become API upload/download |
| `EzyNavMath.dll` / `EzyNavCharting.dll` | Navigation math + charting (`NavMath`, `EzyNavHoleChart`) | Core logic + chart rendering — needs port or replacement |
| `EzyReport.dll` | Report generation | GDI+ based — needs rewrite for HTML/PDF |
| `EzyWizard.dll` | Wizard framework | UI-specific — replace with web wizard |
| `LumenWorks.Framework.IO.dll` | CSV reading | Open-source; easily replaceable |
| `Fixed RibbonMenuButton.dll` | Custom WinForms ribbon button | WinForms-only — replace with web UI |
| `Nevron.System.dll` | Commercial UI/charting suite | Proprietary UI — must be replaced |

### 2.5 Desktop-only features
- `Dolinay\DriveDetector*` — USB drive arrival/removal detection (hardware/OS specific)
- Named `Mutex` single-instance enforcement
- `Settings.Default` (user.config) for local preferences
- Direct local file open/save (`DrillHoleFile.Open`, `Settings.Default.PreviouseFile`)
- `SendKeys`, `System.Drawing.Color`, `Bitmap`, `PictureBox`, `Panel.Dock`
- `Application.Run` message loop

---

## 3. Feasibility & What Can Be Reused

### ✅ Reusable / Portable
- **Data models** (`DrillHoleFile`, `Collar`, `SurveyList`, `Target`, `AbsoluteSurveyList`, etc.) — port to C# classes
- **Navigation/survey math** (`NavMath`, `AbsoluteSurvey`, build/extrapolate/trend calculations) — port to .NET class library
- **File parsing logic** (CSV hole files) — port to backend service
- **Report calculation logic** — port, but rendering must change from GDI+ to HTML/PDF
- **Validation helpers** (`Utility.StringToDecimal`, etc.) — port with UI popups removed

### ❌ Must Be Rewritten
- **Entire UI layer** — all `Form_*`, `Body_*`, `Control_*`, `Popup_*`, `Layout_*` WinForms code
- **`Procedure_*` classes** — tightly coupled to `Panel`/`Label`/`Chart` controls; must be refactored into backend services or thin frontend view models
- **Report rendering** — `Bitmap`/`PictureBox` → HTML canvas, SVG, or server-side PDF
- **Charting** — `EzyNavHoleChart` / Nevron → Chart.js, Plotly, Apache ECharts, or Blazor charting
- **File system access** → REST API + file upload/download (or client-side IndexedDB for offline)
- **USB drive detection** → no direct browser equivalent; must be replaced by file upload or a local companion agent
- **Single-instance mutex** → handle via server-side sessions or browser tab management
- **`Settings.Default`** → backend user preferences database or browser local storage

---

## 4. Recommended Web Architecture

### Option A: ASP.NET Core Backend + Blazor Frontend (Recommended for C# reuse)
- **Backend:** ASP.NET Core Web API (.NET 8 LTS)
- **Frontend:** Blazor WebAssembly or Blazor Server (reuses C#; no JavaScript required for UI logic)
- **Charts:** Blazor charting component (e.g., Blazorise, Chart.js interop)
- **Reports:** Server-side PDF generation (QuestPDF, iText, or HtmlToPdf)
- **File access:** API endpoints for upload/download of hole files
- **Auth:** ASP.NET Core Identity or cookie-based auth

### Option B: ASP.NET Core Backend + React/Angular/Vue Frontend
- **Backend:** ASP.NET Core Web API
- **Frontend:** TypeScript SPA
- **Charts:** Chart.js / Plotly / ECharts
- **Reports:** Client-side PDF (jsPDF) or server-generated

### Shared data layer
- Ported C# class libraries for data models + navigation math (target .NET 8)
- EF Core or Dapper for user/hole storage (SQL Server, PostgreSQL, or SQLite)
- Azure Blob / AWS S3 or local file storage for uploaded hole files

---

## 5. Migration Plan (Phases)

### Phase 1 — Assessment & Extraction (Weeks 1–2)
1. Inventory all 70+ `.cs` files; classify each as UI, business logic, data model, or report.
2. Extract pure logic from `Procedure_*` classes into standalone C# classes (remove `Panel`, `Label`, `Control` references).
3. Identify which external DLLs have replaceable functionality vs. which need source code/licenses.
4. Confirm target .NET version (recommend .NET 8 LTS).
5. Decide on frontend stack (Blazor vs. React/Angular/Vue).

### Phase 2 — Backend API & Core Logic (Weeks 3–6)
1. Create ASP.NET Core Web API project.
2. Port data models (`DrillHoleFile`, `SurveyList`, `Collar`, `Target`, etc.).
3. Port navigation math (`EzyNavMath` equivalent).
4. Port file parsing (hole file import/export) as API services.
5. Implement API endpoints: hole CRUD, file upload/download, survey calculation, report generation.
6. Replace `Settings.Default` with database/user preferences.

### Phase 3 — Frontend UI Rewrite (Weeks 7–12)
1. Build shell layout (navigation, sidebar, main content area).
2. Port each wizard screen:
   - Welcome → dashboard/landing
   - Create New Hole → multi-step form
   - Drill Hole Planning → main workbench with chart + data grid
   - Import → CSV file upload
   - Trend Analysis / Build / Extrapolate / SideTrack → dedicated pages
3. Replace WinForms charts with web charting library.
4. Replace `Popup_*` dialogs with modal components.
5. Implement client-side form validation (port `Utility` parsing logic).

### Phase 4 — Reporting & Export (Weeks 13–14)
1. Replace `Report_*` GDI+ classes with HTML/PDF report generation.
2. Implement report preview and download endpoints.

### Phase 5 — Testing, Deployment & Cutover (Weeks 15–16)
1. Unit tests for ported math/parsing logic.
2. End-to-end UI testing.
3. Deploy backend (IIS, Azure App Service, AWS, or Docker).
4. Deploy frontend (static hosting or integrated with backend).
5. User acceptance testing against legacy desktop app results.

---

## 6. Risks & Mitigations

| Risk | Impact | Mitigation |
|---|---|---|
| Proprietary DLLs have no source (`EzyPlanData`, `EzyNavMath`, `EzyNavCharting`, `EzyReport`, `Nevron`) | High | Decompile/reference binaries only as temporary bridge; plan to rewrite or license modern equivalents |
| `.NET Framework 2.0` target with modern C# syntax | Medium | Retarget to .NET 8; fix breaking API changes |
| Heavy UI/logic coupling in `Procedure_*` | High | Refactor into pure services before porting |
| Direct local file system access | Medium | Replace with API file upload/download |
| USB drive detection (`Dolinay\DriveDetector`) | Medium | Replace with file upload workflow |
| Single-instance `Mutex` | Low | Handle via server sessions / single browser tab guidance |
| GDI+ charting (`Bitmap`, `PictureBox`) | Medium | Replace with Chart.js / ECharts / SVG |
| No visible test project | Medium | Add unit test project for ported logic |

---

## 7. Effort Estimate (Indicative)

| Phase | Effort |
|---|---|
| Phase 1: Assessment & extraction | 1–2 weeks |
| Phase 2: Backend API & core logic | 3–4 weeks |
| Phase 3: Frontend UI rewrite | 4–6 weeks |
| Phase 4: Reporting & export | 1–2 weeks |
| Phase 5: Testing & deployment | 1–2 weeks |
| **Total** | **~10–16 weeks** (1 small-to-medium team) |

---

## 8. Conclusion

**Yes, this project can be converted to a web application.** However, because it is a WinForms desktop app with the business logic intertwined with UI controls, the conversion is a **re-architecture and partial rewrite**, not a simple recompile. The strongest candidates for reuse are the data models, navigation math, and file-parsing logic; the entire user interface and all WinForms-specific controls must be rebuilt using web technologies.

No code was written or modified as part of this analysis.