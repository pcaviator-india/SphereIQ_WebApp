using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Dolinay;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlan_Interface_V2.VBusers.Windows.Forms.Utils;
using EzyPlanData;
using EzyPlanStorage;
using Nevron;

namespace EzyPlan_Interface_V2;

public class Form_MainForm : Form
{
	private DrillHoleFile.OpenFile_ErrorsEnum m_parameterPassedFile;

	private int m_designTimeToolbarWidth;



	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private IEzyPlanProcedure m_currentProcedure;

	private Procedure_Welcome m_welcome;

	private Procedure_HoleManagement m_holeManagement;

	private Procedure_HolePlanning m_holePlanning;

	private Procedure_OpenHoleFile m_openHoleFile;

	private Procedure_CreateNewHole m_createNewHole;

	private Procedure_EditSurveyData m_editSurveyData;

	private Procedure_Import m_importSurveyData;

	private Procedure_BuildToTarget m_buildToTarget;

	private Procedure_TrendAnalysis m_trendAnalysis;

	private Procedure_Extrapolate m_extrapolate;

	private Procedure_EditDetails m_editDetails;

	private Procedure_SideTrackHole m_sideTrackHole;

	private DriveDetector m_driveDetector;

	private IContainer components;

	private Panel panel_Header;

	private Panel panel2;

	private Label label8;

	private Label label_Version;

	private Label label2;

	private Panel panel6;

	private Label label_ScreenTitle;

	private Panel panel_Toolbar;

	private Panel panel_Fill;

	private Panel panel_Body;

	private Panel panel_Info;

	private EzyNavHoleChart ezyNavHoleChart1;

	private Label label_Header_HoleID;

	private ContextMenuStrip contextMenuStrip_ChartOptions;

	private ToolStripMenuItem resetChartToolStripMenuItem;

	private ToolStripMenuItem dViewToolStripMenuItem;

	private ToolStripMenuItem eastingProfileToolStripMenuItem;

	private ToolStripMenuItem northingProfileToolStripMenuItem;

	private ToolStripMenuItem topProfileToolStripMenuItem;

	public Form_MainForm()
	{
		NLicense license2 = new NLicense("6395a0bc-3212-6319-468c-12c90b9c7a7c");
		NLicenseManager.Instance.SetLicense(license2);
		NLicenseManager.Instance.LockLicense = true;
		InitializeComponent();
		m_parameterPassedFile = DrillHoleFile.OpenFile_ErrorsEnum.Null;
		m_drillHoleFile = new DrillHoleFile();
		m_drillHoleStatus = new DrillHoleStatus();
		m_welcome = new Procedure_Welcome(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_holeManagement = new Procedure_HoleManagement(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_holePlanning = new Procedure_HolePlanning(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile, ref m_drillHoleStatus);
		m_openHoleFile = new Procedure_OpenHoleFile(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_createNewHole = new Procedure_CreateNewHole(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_editSurveyData = new Procedure_EditSurveyData(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_importSurveyData = new Procedure_Import(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_buildToTarget = new Procedure_BuildToTarget(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile, ref m_drillHoleStatus);
		m_trendAnalysis = new Procedure_TrendAnalysis(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile, ref m_drillHoleStatus);
		m_extrapolate = new Procedure_Extrapolate(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile, ref m_drillHoleStatus);
		m_editDetails = new Procedure_EditDetails(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_sideTrackHole = new Procedure_SideTrackHole(ref panel_Body, ref panel_Toolbar, ref panel_Info, ref panel_Fill, ref label_ScreenTitle, ref ezyNavHoleChart1, ref m_drillHoleFile);
		m_designTimeToolbarWidth = panel_Toolbar.Width;
		label_Version.Text = Settings.Default.ProductVersion;
		m_driveDetector = new DriveDetector(this);
		m_driveDetector.DeviceArrived += m_driveDetector_DeviceArrived;
		m_driveDetector.DeviceRemoved += m_driveDetector_DeviceRemoved;
	}

	public Form_MainForm(string filePath)
		: this()
	{
		if ((m_parameterPassedFile = m_drillHoleFile.Open(filePath)) == DrillHoleFile.OpenFile_ErrorsEnum.NoError)
		{
			FileOpened(filePath);
		}
	}

	private void SetStartToolbarAndBody()
	{
		StartProcedure_Welcome();
	}

	private void Start()
	{
		if (m_parameterPassedFile != DrillHoleFile.OpenFile_ErrorsEnum.Null)
		{
			if (m_parameterPassedFile == DrillHoleFile.OpenFile_ErrorsEnum.NoError)
			{
				StartProcedure_DrillHoleManagement();
			}
			else
			{
				StartProcedure_Welcome();
			}
		}
		else
		{
			StartProcedure_Welcome();
		}
	}

	private void m_driveDetector_DeviceRemoved(object sender, DriveDetectorEventArgs e)
	{
		if (m_currentProcedure != null)
		{
			m_currentProcedure.DriveRemoved();
		}
	}

	private void m_driveDetector_DeviceArrived(object sender, DriveDetectorEventArgs e)
	{
		if (m_currentProcedure != null)
		{
			m_currentProcedure.DriveArrived(e.Drive);
		}
	}

	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
		if (m_driveDetector != null)
		{
			m_driveDetector.WndProc(ref m);
		}
	}

	private void StartProcedure_Welcome()
	{
		m_currentProcedure = m_welcome;
		m_currentProcedure.Finished += m_Welcome_Finished;
		m_currentProcedure.Start();
	}

	private void m_Welcome_Finished(object sender, EventArgs e)
	{
		Procedure_Welcome.FinishedEventArgs e2 = (Procedure_Welcome.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_Welcome.FinishedEventArgs.FinishState.Close:
			m_welcome.Finished -= m_Welcome_Finished;
			Close();
			break;
		case Procedure_Welcome.FinishedEventArgs.FinishState.HoleManagement:
			if (m_drillHoleFile.Status == DrillHoleFile.FileStreamStatus.Connected)
			{
				m_welcome.Finished -= m_Welcome_Finished;
				StartProcedure_DrillHoleManagement();
			}
			else
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No SphereIQ file open", "Please open a SphereIQ hole file first.");
				popup_MessageBox_OK.ShowDialog();
			}
			break;
		case Procedure_Welcome.FinishedEventArgs.FinishState.CreateNewHole:
			m_welcome.Finished -= m_Welcome_Finished;
			StartProcedure_CreateNewHole_FromHome();
			break;
		case Procedure_Welcome.FinishedEventArgs.FinishState.OpenPreviouseHole:
			FileOpened(e2.FilePath);
			m_welcome.Finished -= m_Welcome_Finished;
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_Welcome.FinishedEventArgs.FinishState.OpenExistingHole:
			m_welcome.Finished -= m_Welcome_Finished;
			StartProcedure_OpenHoleFile_FromWelcome();
			break;
		}
	}

	private void StartProcedure_DrillHolePlanning()
	{
		m_currentProcedure = m_holePlanning;
		m_currentProcedure.Start();
		m_currentProcedure.Finished += DrillHolePlanning_Finished;
	}

	private void DrillHolePlanning_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= DrillHolePlanning_Finished;
		Procedure_HolePlanning.FinishedEventArgs e2 = (Procedure_HolePlanning.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.DrillHoleManagement:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.Build:
			StartProcedure_BuildToTarget();
			break;
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.Extrapolate:
			StartProcedure_Extrapolate();
			break;
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.TrendAnalysis:
			StartProcedure_TrendAnalysis();
			break;
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.SideTrackHole:
			StartProcedure_SideTrackHole();
			break;
		case Procedure_HolePlanning.FinishedEventArgs.FinishState.Close:
			break;
		}
	}

	private void StartProcedure_DrillHoleManagement()
	{
		m_currentProcedure = m_holeManagement;
		m_currentProcedure.Start();
		m_currentProcedure.Finished += DrillHoleManagement_Finished;
	}

	private void DrillHoleManagement_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= DrillHoleManagement_Finished;
		Procedure_HoleManagement.FinishedEventArgs e2 = (Procedure_HoleManagement.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.Home:
			StartProcedure_Welcome();
			break;
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.EditDetails:
			StartProcedure_EditDetails();
			break;
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.HolePlanning:
			StartProcedure_DrillHolePlanning();
			break;
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.EditSurveyData:
			StartProcedure_EditSurveyData();
			break;
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.ImportSurveyData:
			StartProcedure_ImportSurveyData();
			break;
		case Procedure_HoleManagement.FinishedEventArgs.FinishState.SideTrackHole:
			break;
		}
	}

	public void StartProcedure_CreateNewHole_FromHome()
	{
		m_currentProcedure = m_createNewHole;
		m_currentProcedure.Finished += CreateNewHole_FromHome_Finished;
		m_currentProcedure.Start();
	}

	private void CreateNewHole_FromHome_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= CreateNewHole_FromHome_Finished;
		Procedure_CreateNewHole.FinishedEventArgs e2 = (Procedure_CreateNewHole.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Procedure_CreateNewHole.FinishedEventArgs.FinishedStateEnum.Created:
			FileOpened(e2.FilePath);
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_CreateNewHole.FinishedEventArgs.FinishedStateEnum.Cancel:
			StartProcedure_Welcome();
			break;
		}
	}

	public void StartProcedure_EditDetails()
	{
		m_currentProcedure = m_editDetails;
		m_currentProcedure.Finished += EditDetails_Finished;
		m_currentProcedure.Start();
	}

	private void EditDetails_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= EditDetails_Finished;
		Procedure_EditDetails.FinishedEventArgs e2 = (Procedure_EditDetails.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_EditDetails.FinishedEventArgs.FinishState.Update:
			UpdateHoleIDLabel();
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_EditDetails.FinishedEventArgs.FinishState.Cancel:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_EditDetails.FinishedEventArgs.FinishState.UpdateError:
			m_drillHoleFile.Close();
			NoFileOpen();
			StartProcedure_Welcome();
			break;
		}
	}

	public void StartProcedure_OpenHoleFile_FromWelcome()
	{
		m_currentProcedure = m_openHoleFile;
		m_openHoleFile.Finished += OpenHoleFile_FromWelcome_Finished;
		m_openHoleFile.Start();
	}

	private void OpenHoleFile_FromWelcome_Finished(object sender, EventArgs e)
	{
		m_openHoleFile.Finished -= OpenHoleFile_FromWelcome_Finished;
		Procedure_OpenHoleFile.FinishedEventArgs e2 = (Procedure_OpenHoleFile.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_OpenHoleFile.FinishedEventArgs.FinishState.Open:
			FileOpened(e2.FilePath);
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_OpenHoleFile.FinishedEventArgs.FinishState.Cancel:
			StartProcedure_Welcome();
			break;
		}
	}

	private void FileOpened(string filePath)
	{
		UpdateHoleIDLabel();
		UpdateMainWindowName(filePath);
		SetPreviouseFileOpened(filePath);
	}

	private void NoFileOpen()
	{
		ClearHoleIDLabel();
		ClearMainWindowName();
	}

	private void UpdateHoleIDLabel()
	{
		label_Header_HoleID.Text = "HoleID: " + m_drillHoleFile.HoleID;
		label_Header_HoleID.Visible = true;
	}

	private void ClearHoleIDLabel()
	{
		label_Header_HoleID.Text = "null";
		label_Header_HoleID.Visible = false;
	}

	private void UpdateMainWindowName(string fileName)
	{
		Text = "SphereIQ: " + fileName;
	}

	private void ClearMainWindowName()
	{
		Text = "SphereIQ";
	}

	private void SetPreviouseFileOpened(string filePath)
	{
		Settings.Default.PreviouseFile = filePath;
		Settings.Default.Save();
	}

	public void Display_SideTrackHole()
	{
		label_ScreenTitle.Text = "Side Track Hole";
	}

	private void StartProcedure_BuildToTarget()
	{
		m_currentProcedure = m_buildToTarget;
		m_currentProcedure.Start();
		m_currentProcedure.Finished += Procedure_BuildToTarget_Finished;
	}

	private void Procedure_BuildToTarget_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= Procedure_BuildToTarget_Finished;
		Procedure_BuildToTarget.FinishedEventArgs e2 = (Procedure_BuildToTarget.FinishedEventArgs)e;
		if (e2.State == Procedure_BuildToTarget.FinishedEventArgs.FinishState.Finished)
		{
			StartProcedure_DrillHolePlanning();
		}
	}

	private void StartProcedure_TrendAnalysis()
	{
		m_currentProcedure = m_trendAnalysis;
		m_currentProcedure.Start();
		m_currentProcedure.Finished += Procedure_TrendAnalysis_Finished;
	}

	private void Procedure_TrendAnalysis_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= Procedure_TrendAnalysis_Finished;
		Procedure_TrendAnalysis.FinishedEventArgs e2 = (Procedure_TrendAnalysis.FinishedEventArgs)e;
		if (e2.State == Procedure_TrendAnalysis.FinishedEventArgs.FinishState.Finished)
		{
			StartProcedure_DrillHolePlanning();
		}
	}

	public void StartProcedure_Extrapolate()
	{
		m_currentProcedure = m_extrapolate;
		m_currentProcedure.Finished += Extrapolate_Finished;
		m_currentProcedure.Start();
	}

	private void Extrapolate_Finished(object caller, EventArgs e)
	{
		m_currentProcedure.Finished -= Extrapolate_Finished;
		Procedure_Extrapolate.FinishedEventArgs e2 = (Procedure_Extrapolate.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_Extrapolate.FinishedEventArgs.FinishState.Finished:
			StartProcedure_DrillHolePlanning();
			break;
		case Procedure_Extrapolate.FinishedEventArgs.FinishState.Cancel:
			StartProcedure_DrillHolePlanning();
			break;
		}
	}

	private void StartProcedure_SideTrackHole()
	{
		m_currentProcedure = m_sideTrackHole;
		m_currentProcedure.Start();
		m_currentProcedure.Finished += Procedure_SideTrackHole_Finished;
	}

	private void Procedure_SideTrackHole_Finished(object sender, EventArgs e)
	{
		m_currentProcedure.Finished -= Procedure_SideTrackHole_Finished;
		Procedure_SideTrackHole.FinishedEventArgs e2 = (Procedure_SideTrackHole.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_SideTrackHole.FinishedEventArgs.FinishState.Saved:
			FileOpened(e2.FilePath);
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_SideTrackHole.FinishedEventArgs.FinishState.Canceled:
			StartProcedure_DrillHolePlanning();
			break;
		}
	}

	public void StartProcedure_EditSurveyData()
	{
		m_currentProcedure = m_editSurveyData;
		m_currentProcedure.Finished += EditSurveyData_Finished;
		m_currentProcedure.Start();
	}

	private void EditSurveyData_Finished(object caller, EventArgs e)
	{
		m_currentProcedure.Finished -= EditSurveyData_Finished;
		Procedure_EditSurveyData.FinishedEventArgs e2 = (Procedure_EditSurveyData.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_EditSurveyData.FinishedEventArgs.FinishState.Save:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_EditSurveyData.FinishedEventArgs.FinishState.Cancel:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_EditSurveyData.FinishedEventArgs.FinishState.SaveError:
			m_drillHoleFile.Close();
			NoFileOpen();
			StartProcedure_Welcome();
			break;
		}
	}

	public void StartProcedure_ImportSurveyData()
	{
		m_currentProcedure = m_importSurveyData;
		m_currentProcedure.Finished += ImportSurveyData_Finished;
		m_currentProcedure.Start();
	}

	private void ImportSurveyData_Finished(object caller, EventArgs e)
	{
		m_currentProcedure.Finished -= ImportSurveyData_Finished;
		Procedure_Import.FinishedEventArgs e2 = (Procedure_Import.FinishedEventArgs)e;
		switch (e2.State)
		{
		case Procedure_Import.FinishedEventArgs.FinishState.Save:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_Import.FinishedEventArgs.FinishState.Cancel:
			StartProcedure_DrillHoleManagement();
			break;
		case Procedure_Import.FinishedEventArgs.FinishState.SaveError:
			m_drillHoleFile.Close();
			NoFileOpen();
			StartProcedure_Welcome();
			break;
		}
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		AutoWaitCursor.Cursor = Cursors.WaitCursor;
		AutoWaitCursor.Delay = new TimeSpan(0, 0, 0, 0, 25);
		AutoWaitCursor.MainWindowHandle = Handle;
		AutoWaitCursor.Start();
		Start();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		base.OnFormClosing(e);
		if (m_drillHoleFile != null)
		{
			m_drillHoleFile.Close();
		}
	}

	private void Form_MainForm_Resize(object sender, EventArgs e)
	{
		if (m_currentProcedure != null)
		{
			m_currentProcedure.Resize();
		}
	}

	private void resetChartToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ezyNavHoleChart1.ResetChart();
	}

	private void dViewToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ezyNavHoleChart1.ChartViewMode = EzyNavHoleChart.ChartViewModes.ThreeD;
	}

	private void eastingProfileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ezyNavHoleChart1.ChartViewMode = EzyNavHoleChart.ChartViewModes.TwoDEasting;
	}

	private void northingProfileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ezyNavHoleChart1.ChartViewMode = EzyNavHoleChart.ChartViewModes.TwoDNorthing;
	}

	private void topProfileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ezyNavHoleChart1.ChartViewMode = EzyNavHoleChart.ChartViewModes.TwoDTop;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EzyPlan_Interface_V2.Form_MainForm));
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label_Version = new System.Windows.Forms.Label();
		this.panel_Header = new System.Windows.Forms.Panel();
		this.label_Header_HoleID = new System.Windows.Forms.Label();
		this.label_ScreenTitle = new System.Windows.Forms.Label();
		this.panel_Toolbar = new System.Windows.Forms.Panel();
		this.panel_Fill = new System.Windows.Forms.Panel();
		this.ezyNavHoleChart1 = new EzyNavCharting.EzyNavHoleChart();
		this.contextMenuStrip_ChartOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.resetChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.eastingProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.northingProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.topProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.panel_Body = new System.Windows.Forms.Panel();
		this.panel_Info = new System.Windows.Forms.Panel();
		this.panel2.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel_Header.SuspendLayout();
		this.panel_Fill.SuspendLayout();
		this.contextMenuStrip_ChartOptions.SuspendLayout();
		base.SuspendLayout();
		this.panel2.BackColor = System.Drawing.Color.MidnightBlue;
		this.panel2.Controls.Add(this.panel6);
		this.panel2.Controls.Add(this.label8);
		this.panel2.Controls.Add(this.label_Version);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 712);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1016, 22);
		this.panel2.TabIndex = 3;
		this.panel6.Controls.Add(this.label2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Location = new System.Drawing.Point(889, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(127, 22);
		this.panel6.TabIndex = 3;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(3, 3);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(118, 15);
		this.label2.TabIndex = 2;
		this.label2.Text = "PCAVIATOR";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.White;
		this.label8.Location = new System.Drawing.Point(6, 3);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label8.Size = new System.Drawing.Size(49, 15);
		this.label8.TabIndex = 2;
		this.label8.Text = "Version";
		this.label_Version.AutoSize = true;
		this.label_Version.Font = new System.Drawing.Font("Times New Roman", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Version.ForeColor = System.Drawing.Color.White;
		this.label_Version.Location = new System.Drawing.Point(61, 3);
		this.label_Version.Name = "label_Version";
		this.label_Version.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label_Version.Size = new System.Drawing.Size(27, 15);
		this.label_Version.TabIndex = 2;
		this.label_Version.Text = "null";
		this.panel_Header.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.panel_Header.Controls.Add(this.label_Header_HoleID);
		this.panel_Header.Controls.Add(this.label_ScreenTitle);
		this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_Header.Location = new System.Drawing.Point(0, 0);
		this.panel_Header.Name = "panel_Header";
		this.panel_Header.Size = new System.Drawing.Size(1016, 29);
		this.panel_Header.TabIndex = 0;
		this.label_Header_HoleID.AutoSize = true;
		this.label_Header_HoleID.BackColor = System.Drawing.Color.Transparent;
		this.label_Header_HoleID.Dock = System.Windows.Forms.DockStyle.Right;
		this.label_Header_HoleID.Font = new System.Drawing.Font("Century Schoolbook", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Header_HoleID.Location = new System.Drawing.Point(970, 0);
		this.label_Header_HoleID.Name = "label_Header_HoleID";
		this.label_Header_HoleID.Size = new System.Drawing.Size(46, 23);
		this.label_Header_HoleID.TabIndex = 0;
		this.label_Header_HoleID.Text = "null";
		this.label_Header_HoleID.Visible = false;
		this.label_ScreenTitle.AutoSize = true;
		this.label_ScreenTitle.BackColor = System.Drawing.Color.Transparent;
		this.label_ScreenTitle.Dock = System.Windows.Forms.DockStyle.Left;
		this.label_ScreenTitle.Font = new System.Drawing.Font("Century Schoolbook", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_ScreenTitle.Location = new System.Drawing.Point(0, 0);
		this.label_ScreenTitle.Name = "label_ScreenTitle";
		this.label_ScreenTitle.Size = new System.Drawing.Size(52, 25);
		this.label_ScreenTitle.TabIndex = 0;
		this.label_ScreenTitle.Text = "null";
		this.panel_Toolbar.BackColor = System.Drawing.Color.WhiteSmoke;
		this.panel_Toolbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel_Toolbar.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel_Toolbar.Location = new System.Drawing.Point(0, 29);
		this.panel_Toolbar.Name = "panel_Toolbar";
		this.panel_Toolbar.Size = new System.Drawing.Size(193, 683);
		this.panel_Toolbar.TabIndex = 2;
		this.panel_Fill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel_Fill.Controls.Add(this.ezyNavHoleChart1);
		this.panel_Fill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Fill.Location = new System.Drawing.Point(447, 29);
		this.panel_Fill.Name = "panel_Fill";
		this.panel_Fill.Size = new System.Drawing.Size(569, 683);
		this.panel_Fill.TabIndex = 5;
		this.ezyNavHoleChart1.BackColor = System.Drawing.Color.White;
		this.ezyNavHoleChart1.BuildVisible = false;
		this.ezyNavHoleChart1.ChartViewMode = EzyNavCharting.EzyNavHoleChart.ChartViewModes.ThreeD;
		this.ezyNavHoleChart1.CollarVisible = false;
		this.ezyNavHoleChart1.ContextMenuStrip = this.contextMenuStrip_ChartOptions;
		this.ezyNavHoleChart1.DataZoomAxis = EzyNavCharting.EzyNavHoleChart.Axis.Easting;
		this.ezyNavHoleChart1.DistanceFromTargetVisible = false;
		this.ezyNavHoleChart1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChart1.ExtrapolatedVisible = false;
		this.ezyNavHoleChart1.HolePlanVisible = false;
		this.ezyNavHoleChart1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChart1.Name = "ezyNavHoleChart1";
		this.ezyNavHoleChart1.SelectedSurveysVisible = false;
		this.ezyNavHoleChart1.SideTrackVisible = false;
		this.ezyNavHoleChart1.Size = new System.Drawing.Size(565, 679);
		this.ezyNavHoleChart1.StraightPathToTargetVisible = false;
		this.ezyNavHoleChart1.StraightPathVisible = false;
		this.ezyNavHoleChart1.SurveyAtTargetTVDVisible = false;
		this.ezyNavHoleChart1.SurveyDataVisible = false;
		this.ezyNavHoleChart1.TabIndex = 0;
		this.ezyNavHoleChart1.TabStop = false;
		this.ezyNavHoleChart1.TargetVisible = false;
		this.ezyNavHoleChart1.Tool = EzyNavCharting.EzyNavHoleChart.Tools.TrackBall;
		this.ezyNavHoleChart1.UserExtrapolatedVisible = false;
		this.contextMenuStrip_ChartOptions.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_ChartOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.resetChartToolStripMenuItem, this.dViewToolStripMenuItem, this.eastingProfileToolStripMenuItem, this.northingProfileToolStripMenuItem, this.topProfileToolStripMenuItem });
		this.contextMenuStrip_ChartOptions.Name = "contextMenuStrip_ChartOptions";
		this.contextMenuStrip_ChartOptions.Size = new System.Drawing.Size(275, 174);
		this.resetChartToolStripMenuItem.Name = "resetChartToolStripMenuItem";
		this.resetChartToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
		this.resetChartToolStripMenuItem.Text = "Reset Chart";
		this.resetChartToolStripMenuItem.Click += new System.EventHandler(this.resetChartToolStripMenuItem_Click);
		this.dViewToolStripMenuItem.Name = "dViewToolStripMenuItem";
		this.dViewToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
		this.dViewToolStripMenuItem.Text = "3D View";
		this.dViewToolStripMenuItem.Click += new System.EventHandler(this.dViewToolStripMenuItem_Click);
		this.eastingProfileToolStripMenuItem.Name = "eastingProfileToolStripMenuItem";
		this.eastingProfileToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
		this.eastingProfileToolStripMenuItem.Text = "Easting Profile";
		this.eastingProfileToolStripMenuItem.Click += new System.EventHandler(this.eastingProfileToolStripMenuItem_Click);
		this.northingProfileToolStripMenuItem.Name = "northingProfileToolStripMenuItem";
		this.northingProfileToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
		this.northingProfileToolStripMenuItem.Text = "Northing Profile";
		this.northingProfileToolStripMenuItem.Click += new System.EventHandler(this.northingProfileToolStripMenuItem_Click);
		this.topProfileToolStripMenuItem.Name = "topProfileToolStripMenuItem";
		this.topProfileToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
		this.topProfileToolStripMenuItem.Text = "Top Profile";
		this.topProfileToolStripMenuItem.Click += new System.EventHandler(this.topProfileToolStripMenuItem_Click);
		this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Body.Location = new System.Drawing.Point(0, 0);
		this.panel_Body.Name = "panel_Body";
		this.panel_Body.Size = new System.Drawing.Size(1016, 734);
		this.panel_Body.TabIndex = 6;
		this.panel_Info.AutoScroll = true;
		this.panel_Info.BackColor = System.Drawing.Color.White;
		this.panel_Info.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel_Info.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel_Info.Location = new System.Drawing.Point(193, 29);
		this.panel_Info.Name = "panel_Info";
		this.panel_Info.Size = new System.Drawing.Size(254, 683);
		this.panel_Info.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(1016, 734);
		base.Controls.Add(this.panel_Fill);
		base.Controls.Add(this.panel_Info);
		base.Controls.Add(this.panel_Toolbar);
		base.Controls.Add(this.panel_Header);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel_Body);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Form_MainForm";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SphereIQ";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Resize += new System.EventHandler(this.Form_MainForm_Resize);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.panel_Header.ResumeLayout(false);
		this.panel_Header.PerformLayout();
		this.panel_Fill.ResumeLayout(false);
		this.contextMenuStrip_ChartOptions.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
