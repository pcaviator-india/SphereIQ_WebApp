using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using EzyReport;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_TrendAnalysis_Results : Layout_Body_Wizard
{
	private delegate EzyReport.Report AsyncConsOperationsDelegate(bool surveyData, bool extrapolatedSurveys, bool analysedSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working);

	private delegate void DisplayGeneratedReportDelegate(EzyReport.Report report, ref Popup_WorkingIndicator working);

	private delegate void DriveArrivedDelegate(string driveLetter);

	private delegate void DriveRemovedDelegate();

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Back,
			Finish
		}

		private ButtonPressedEnum m_buttonPressed;

		public ButtonPressedEnum ButtonPressed
		{
			get
			{
				return m_buttonPressed;
			}
			set
			{
				m_buttonPressed = value;
			}
		}
	}

	private TrendAnalysisResults m_trendAnalysisResults;

	private SortableBindingList<AbsoluteSurvey> m_analysedSurveys;

	private Target m_target;

	private RelativePosition m_distance;

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private EzyNavHoleChart m_chart;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private bool m_extrapolatedPath;

	private DriveArrivedDelegate m_driveArrived;

	private DriveRemovedDelegate m_driveRemoved;

	private IContainer components;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Finish;

	private TabControl tabControl_AnalysisResults;

	private TabPage tabPage_Results;

	private GroupBox groupBox_Options;

	private RibbonMenuButton button_GenerateReport;

	private RibbonMenuButton button_AdvancedResults;

	private TabPage tabPage_Chart;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private Panel panel_AutoScroll;

	private GroupBox groupBox_AngleChange;

	private GroupBox groupBox_DoglegSeverity;

	private GroupBox groupBox_Azimuth;

	private GroupBox groupBox_Inclination;

	private Label label_Dogleg;

	private Label label_AzimuthTurnRate;

	private Label label_AzimuthTrend;

	private Label label_InclinationBuildRate;

	private Label label_InclinationTrend;

	private Label label_AngleChangeLabel;

	private Panel panel_ScrollChart;

	private GroupBox groupBox_ToolFaceSetting;

	private Label label_ToolFaceSetting;

	private GroupBox groupBox_ExtrapolatedDistanceFromTarget;

	private Panel panel1;

	private Label label_AngleChange1;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private Label label_Distance_TVD;

	private Label label_Distance_Horizontal;

	private Label label_Distance_East;

	private Label label_Distance_North;

	public event EventHandler ButtonPressed;

	public Body_TrendAnalysis_Results(DrillHoleFile drillHoleFile, DrillHoleStatus drillHoleStatus, EzyNavHoleChart ezyNavChart, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_chart = ezyNavChart;
		m_drillHoleFile = drillHoleFile;
		m_drillHoleStatus = drillHoleStatus;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		Visible = false;
	}

	public void Display(TrendAnalysisResults trendAnalysisResults, Target target, SortableBindingList<AbsoluteSurvey> analysedSurveys)
	{
		m_trendAnalysisResults = trendAnalysisResults;
		m_analysedSurveys = analysedSurveys;
		m_target = target;
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		UpdateFields(ref trendAnalysisResults, ref target, out var absoluteSurveyData);
		AbsoluteSurveyList surveyList = trendAnalysisResults.ExtrapolatedSurveyData;
		UpdateChart(ref surveyList, ref absoluteSurveyData);
		tabControl_AnalysisResults.SelectedIndex = 0;
		Visible = true;
		Focus();
	}

	public void UpdateFields(ref TrendAnalysisResults trendAnalysisResults, ref Target target, out AbsoluteSurveyList absoluteSurveyData)
	{
		absoluteSurveyData = new AbsoluteSurveyList();
		ClearFields();
		if (trendAnalysisResults == null)
		{
			return;
		}
		if (trendAnalysisResults.InclinationTrend == InclinationTrend.Build)
		{
			label_InclinationTrend.Text = "The inclination is trending to build.";
			label_InclinationBuildRate.Text = "The build rate is " + trendAnalysisResults.BuildRate + " degrees/30m.";
		}
		else if (trendAnalysisResults.InclinationTrend == InclinationTrend.Drop)
		{
			label_InclinationTrend.Text = "The inclination is trending to drop.";
			label_InclinationBuildRate.Text = "The drop rate is " + trendAnalysisResults.BuildRate + " degrees/30m.";
		}
		else
		{
			label_InclinationTrend.Text = "There is no inclination trend.";
			label_InclinationBuildRate.Text = "";
		}
		if (trendAnalysisResults.AzimuthTrend != AzimuthTrend.NoTrend)
		{
			label_AzimuthTrend.Text = "The azimuth is trending " + trendAnalysisResults.AzimuthTrend.ToString() + ".";
			label_AzimuthTurnRate.Text = "The turn rate is " + trendAnalysisResults.TurnRate + " degrees/30m.";
		}
		else
		{
			label_AzimuthTrend.Text = "There is no azimuth trend.";
			label_AzimuthTurnRate.Text = "";
		}
		label_ToolFaceSetting.Text = trendAnalysisResults.ToolFaceSetting + " degrees off the high side.";
		label_Dogleg.Text = trendAnalysisResults.DogLegSeverity + " degrees/30m.";
		label_AngleChange1.Text = trendAnalysisResults.AngleChange + " degrees over " + trendAnalysisResults.AngleChangeOver + "m.";
		NavMath navMath = new NavMath();
		absoluteSurveyData = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyData);
		if (absoluteSurveyData.LastRecord().TVD < target.TVD)
		{
			m_extrapolatedPath = true;
			AbsoluteSurvey survey = trendAnalysisResults.ExtrapolatedSurveyData.LastRecord();
			m_distance = new RelativePosition();
			NavMath.DistanceFromTargetEnum distanceFromTargetEnum = navMath.DistanceFromTarget(target, survey, ref m_distance);
			if ((m_distance != null) & (distanceFromTargetEnum == NavMath.DistanceFromTargetEnum.NoError))
			{
				if (m_distance != null)
				{
					label_Distance_North.Text = m_distance.NorthingDistance.ToString();
					label_Distance_East.Text = m_distance.EastingDistance.ToString();
					label_Distance_Horizontal.Text = m_distance.HorizontalDeparture.ToString();
					label_Distance_TVD.Text = m_distance.VerticalDistance.ToString();
					groupBox_ExtrapolatedDistanceFromTarget.Visible = true;
				}
				else
				{
					groupBox_ExtrapolatedDistanceFromTarget.Visible = false;
				}
			}
		}
		else
		{
			groupBox_ExtrapolatedDistanceFromTarget.Visible = false;
			m_extrapolatedPath = false;
		}
	}

	public void ClearFields()
	{
		label_InclinationTrend.Text = "";
		label_InclinationBuildRate.Text = "";
		label_AzimuthTrend.Text = "";
		label_AzimuthTurnRate.Text = "";
		label_ToolFaceSetting.Text = "";
		label_Dogleg.Text = "";
		label_AngleChange1.Text = "";
	}

	public void UpdateChart(ref AbsoluteSurveyList surveyList, ref AbsoluteSurveyList absoluteSurveyData)
	{
		if (absoluteSurveyData.LastRecord().TVD < m_target.TVD)
		{
			m_chart.SetExtrapolated(surveyList.List, visible: true);
		}
		else
		{
			m_chart.ResetExtrapolated();
		}
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
		m_trendAnalysisResults = null;
		m_target = null;
		m_distance = null;
	}

	private void AdvancedResults()
	{
		if (m_trendAnalysisResults != null && m_extrapolatedPath)
		{
			Popup_TrendAnalysis_Advanced popup_TrendAnalysis_Advanced = new Popup_TrendAnalysis_Advanced();
			AbsoluteSurveyList extrapolatedSurveyData = m_trendAnalysisResults.ExtrapolatedSurveyData;
			AbsoluteSurvey startSurvey = extrapolatedSurveyData.RecordAt(0);
			AbsoluteSurvey endSurvey = extrapolatedSurveyData.LastRecord();
			popup_TrendAnalysis_Advanced.ShowDialog(startSurvey, endSurvey, m_trendAnalysisResults.ExtrapolatedSurveyData);
		}
		if (!m_extrapolatedPath)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No Advanced Details", "The Advanced details are for an extraplated path. There is currently no extrapolated path to display.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void ConstructReport()
	{
		if (GetUserInput(out var author, out var notes, out var surveyData, out var extrapolatedSurveys, out var analysedSurveys, out var charts))
		{
			Popup_WorkingIndicator working = new Popup_WorkingIndicator("Generating Report", "Generating Report", showCancelButton: true, 8);
			AsyncConsOperationsDelegate asyncConsOperationsDelegate = AsyncConsOperations;
			asyncConsOperationsDelegate.BeginInvoke(surveyData, extrapolatedSurveys, analysedSurveys, charts, author, notes, ref working, AsyncConsComplete, asyncConsOperationsDelegate);
			working.ShowDialog();
		}
	}

	private EzyReport.Report AsyncConsOperations(bool surveyData, bool extrapolatedSurveys, bool analysedSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working)
	{
		EzyReport.Report report = new EzyReport.Report("Trend Analysis Results: " + m_drillHoleFile.HoleID, author);
		if (UpdateProgress(ref working, "Adding: Hole Details"))
		{
			return null;
		}
		report.AppendPage(new Report_HoleID(m_drillHoleFile, m_drillHoleStatus.SidetrackSurvey, author, notes), "Drill Hole Details");
		if (UpdateProgress(ref working, "Adding: Hole Status"))
		{
			return null;
		}
		report.AppendPage(new Report_HoleStatus(m_drillHoleStatus.SurveyAtTargetTVD, m_drillHoleStatus.EndOfHoleSurvey, m_drillHoleStatus.SidetrackSurvey, m_drillHoleStatus.StraightPathSurvey, m_drillHoleStatus.CurrentDistanceFromTarget, m_drillHoleStatus.DistanceFromTargetAtTargetTVD, m_drillHoleStatus.StraightPathDistance, m_drillHoleStatus.MinimumDLStateString, m_drillHoleStatus.MinimumDLStateColor), "Hole Status");
		if (UpdateProgress(ref working, "Adding: Trend Analysis Results"))
		{
			return null;
		}
		report.AppendPage(new Report_TrendAnalysis(m_trendAnalysisResults, m_distance), "Trend Analysis Results");
		if (UpdateProgress(ref working, "Adding: Analysed Surveys"))
		{
			return null;
		}
		if (analysedSurveys)
		{
			report.AppendPage(m_analysedSurveys, "Analysed Surveys");
		}
		if (UpdateProgress(ref working, "Adding: Extrapolated Surveys"))
		{
			return null;
		}
		if (extrapolatedSurveys)
		{
			report.AppendPage(m_trendAnalysisResults.ExtrapolatedSurveyData, "Extrapolated Survey's");
		}
		if (UpdateProgress(ref working, "Adding: Survey Data"))
		{
			return null;
		}
		if (surveyData)
		{
			report.AppendPage(m_drillHoleStatus.AbsoluteSurveyList, "Survey Data");
		}
		if (UpdateProgress(ref working, "Adding: Charts"))
		{
			return null;
		}
		if (charts)
		{
			m_chart.GetImages(out var perspective, out var easting, out var northing, out var top);
			report.AppendPage(new Report_Chart(perspective), "3D Chart View");
			report.AppendPage(new Report_Chart(easting), "Easting Profile View");
			report.AppendPage(new Report_Chart(northing), "Northng Profile View");
			report.AppendPage(new Report_Chart(top), "Top Profile View");
			perspective = null;
			easting = null;
			northing = null;
			top = null;
		}
		if (UpdateProgress(ref working, "Generating Preview..."))
		{
			return null;
		}
		return report;
	}

	private bool UpdateProgress(ref Popup_WorkingIndicator working, string stateText)
	{
		Popup_WorkingIndicator.CancelStateArgs cancelState = new Popup_WorkingIndicator.CancelStateArgs();
		working.GetCancelStatus(ref cancelState);
		if (!cancelState.cancelled)
		{
			working.UpdateProgress(stateText, 1);
		}
		return cancelState.cancelled;
	}

	private bool GetUserInput(out string author, out string notes, out bool surveyData, out bool extrapolatedSurveys, out bool analysedSurveys, out bool charts)
	{
		Popup_Report_UserFields popup_Report_UserFields = new Popup_Report_UserFields();
		Popup_Report_Components popup_Report_Components = new Popup_Report_Components(surveyData: true, buildSurveys: false, extrapolatedSurveys: true, analysedSurveys: true, charts: true);
		Popup_Report_UserFields.UserFieldsDialogResult userFieldsDialogResult = null;
		Popup_Report_Components.ComponentsDialogResult componentsDialogResult = null;
		author = "";
		notes = "";
		surveyData = false;
		extrapolatedSurveys = false;
		analysedSurveys = false;
		charts = false;
		int num = 0;
		while (true)
		{
			switch (num)
			{
			case 0:
				userFieldsDialogResult = popup_Report_UserFields.ShowDialog();
				switch (userFieldsDialogResult.ButtonPressed)
				{
				case Popup_Report_UserFields.ButtonPressedEnum.Cancel:
					return false;
				case Popup_Report_UserFields.ButtonPressedEnum.Next:
					num = 1;
					break;
				}
				break;
			case 1:
				componentsDialogResult = popup_Report_Components.ShowDialog();
				switch (componentsDialogResult.ButtonPressed)
				{
				case Popup_Report_Components.ButtonPressedEnum.Cancel:
					return false;
				case Popup_Report_Components.ButtonPressedEnum.Back:
					num = 0;
					break;
				case Popup_Report_Components.ButtonPressedEnum.Next:
					num = 2;
					break;
				}
				break;
			case 2:
				author = userFieldsDialogResult.UserName;
				notes = userFieldsDialogResult.Notes;
				surveyData = componentsDialogResult.SurveyData;
				extrapolatedSurveys = componentsDialogResult.ExtrapolatedSurveys;
				analysedSurveys = componentsDialogResult.AnalysedSurveys;
				charts = componentsDialogResult.Charts;
				return true;
			}
		}
	}

	private void AsyncConsComplete(IAsyncResult result)
	{
		AsyncConsOperationsDelegate asyncConsOperationsDelegate = (AsyncConsOperationsDelegate)result.AsyncState;
		Popup_WorkingIndicator working = null;
		EzyReport.Report report = asyncConsOperationsDelegate.EndInvoke(ref working, result);
		if (report != null)
		{
			DisplayGeneratedReport(report, ref working);
			MethodInvoker methodInvoker = DelayedGarbageCollection;
			methodInvoker.BeginInvoke(null, null);
			report = null;
			result = null;
		}
		else
		{
			MethodInvoker methodInvoker2 = DelayedGarbageCollection;
			methodInvoker2.BeginInvoke(null, null);
			working.Close();
		}
	}

	private void DisplayGeneratedReport(EzyReport.Report report, ref Popup_WorkingIndicator working)
	{
		if (report != null)
		{
			if (InvokeRequired)
			{
				DisplayGeneratedReportDelegate method = DisplayGeneratedReport;
				Invoke(method, report, working);
				return;
			}
			Popup_ReportPreview popup_ReportPreview = new Popup_ReportPreview();
			popup_ReportPreview.SetReport(report);
			m_driveArrived = popup_ReportPreview.DriveChange;
			m_driveRemoved = popup_ReportPreview.DriveChange;
			working.Close();
			popup_ReportPreview.ShowDialog();
			report = null;
			working = null;
			popup_ReportPreview = null;
			m_driveArrived = null;
			m_driveRemoved = null;
		}
	}

	private void DelayedGarbageCollection()
	{
		Thread.Sleep(1000);
		GC.Collect();
	}

	public void DriveArrived(string driveLetter)
	{
		if (m_driveArrived != null)
		{
			m_driveArrived(driveLetter);
		}
	}

	public void DriveRemoved()
	{
		if (m_driveRemoved != null)
		{
			m_driveRemoved();
		}
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Back;
		OnButtonPressed(e2);
	}

	private void button_Finish_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Finish;
		OnButtonPressed(e2);
	}

	private void button_GenerateReport_Click(object sender, EventArgs e)
	{
		ConstructReport();
	}

	private void button_AdvancedResults_Click(object sender, EventArgs e)
	{
		AdvancedResults();
	}

	private void button_GenerateReport_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_GenerateReport);
	}

	private void button_GenerateReport_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_GenerateReport);
	}

	private void button_AdvancedResults_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AdvancedResults);
	}

	private void button_AdvancedResults_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AdvancedResults);
	}

	private void button_Finish_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Finish);
	}

	private void button_Finish_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Finish);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
	}

	private void tabControl_AnalysisResults_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
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
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_Finish = new RibbonStyle.RibbonMenuButton();
		this.tabPage_Chart = new System.Windows.Forms.TabPage();
		this.panel_ScrollChart = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.tabControl_AnalysisResults = new System.Windows.Forms.TabControl();
		this.tabPage_Results = new System.Windows.Forms.TabPage();
		this.panel_AutoScroll = new System.Windows.Forms.Panel();
		this.groupBox_ExtrapolatedDistanceFromTarget = new System.Windows.Forms.GroupBox();
		this.label_Distance_TVD = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label_Distance_Horizontal = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label_Distance_East = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label_Distance_North = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox_AngleChange = new System.Windows.Forms.GroupBox();
		this.label_AngleChange1 = new System.Windows.Forms.Label();
		this.label_AngleChangeLabel = new System.Windows.Forms.Label();
		this.groupBox_DoglegSeverity = new System.Windows.Forms.GroupBox();
		this.label_Dogleg = new System.Windows.Forms.Label();
		this.groupBox_ToolFaceSetting = new System.Windows.Forms.GroupBox();
		this.label_ToolFaceSetting = new System.Windows.Forms.Label();
		this.groupBox_Azimuth = new System.Windows.Forms.GroupBox();
		this.label_AzimuthTurnRate = new System.Windows.Forms.Label();
		this.label_AzimuthTrend = new System.Windows.Forms.Label();
		this.groupBox_Inclination = new System.Windows.Forms.GroupBox();
		this.label_InclinationBuildRate = new System.Windows.Forms.Label();
		this.label_InclinationTrend = new System.Windows.Forms.Label();
		this.groupBox_Options = new System.Windows.Forms.GroupBox();
		this.button_GenerateReport = new RibbonStyle.RibbonMenuButton();
		this.button_AdvancedResults = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.tabPage_Chart.SuspendLayout();
		this.panel_ScrollChart.SuspendLayout();
		this.tabControl_AnalysisResults.SuspendLayout();
		this.tabPage_Results.SuspendLayout();
		this.panel_AutoScroll.SuspendLayout();
		this.groupBox_ExtrapolatedDistanceFromTarget.SuspendLayout();
		this.groupBox_AngleChange.SuspendLayout();
		this.groupBox_DoglegSeverity.SuspendLayout();
		this.groupBox_ToolFaceSetting.SuspendLayout();
		this.groupBox_Azimuth.SuspendLayout();
		this.groupBox_Inclination.SuspendLayout();
		this.groupBox_Options.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(229, 24);
		base.label_headerText1.Text = "Trend Analysis Results:";
		base.groupBox_frame.Size = new System.Drawing.Size(398, 643);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.button_Finish);
		base.panel_buttonPanel.Location = new System.Drawing.Point(128, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(286, 55);
		base.panel_body.Location = new System.Drawing.Point(0, 60);
		base.panel_body.Size = new System.Drawing.Size(414, 659);
		base.panel_body.TabIndex = 0;
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Size = new System.Drawing.Size(414, 60);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 719);
		base.panel_footer.Size = new System.Drawing.Size(414, 55);
		base.panel_footer.TabIndex = 1;
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.tabControl_AnalysisResults);
		base.panel_scrollPanel.Size = new System.Drawing.Size(392, 624);
		this.button_Back.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Back.AutoEllipsis = true;
		this.button_Back.BackColor = System.Drawing.Color.Transparent;
		this.button_Back.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Back.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorOn = System.Drawing.Color.Orange;
		this.button_Back.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Back.ColorPress = System.Drawing.Color.Orange;
		this.button_Back.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Back.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Back.FadingSpeed = 35;
		this.button_Back.FlatAppearance.BorderSize = 0;
		this.button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Back.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Back.Image = EzyPlan_Interface_V2.Properties.Resources.back;
		this.button_Back.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Back.ImageOffset = 0;
		this.button_Back.IsPressed = false;
		this.button_Back.KeepPress = false;
		this.button_Back.Location = new System.Drawing.Point(145, 6);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(134, 45);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 1;
		this.button_Back.Text = "Back";
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
		this.button_Finish.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Finish.AutoEllipsis = true;
		this.button_Finish.BackColor = System.Drawing.Color.Transparent;
		this.button_Finish.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Finish.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Finish.ColorOn = System.Drawing.Color.Orange;
		this.button_Finish.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Finish.ColorPress = System.Drawing.Color.Orange;
		this.button_Finish.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Finish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Finish.FadingSpeed = 35;
		this.button_Finish.FlatAppearance.BorderSize = 0;
		this.button_Finish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Finish.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Finish.Image = EzyPlan_Interface_V2.Properties.Resources.Last;
		this.button_Finish.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Finish.ImageOffset = 0;
		this.button_Finish.IsPressed = false;
		this.button_Finish.KeepPress = false;
		this.button_Finish.Location = new System.Drawing.Point(5, 6);
		this.button_Finish.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Finish.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Finish.Name = "button_Finish";
		this.button_Finish.Radius = 10;
		this.button_Finish.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Finish.Size = new System.Drawing.Size(134, 45);
		this.button_Finish.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Finish.SplitDistance = 0;
		this.button_Finish.TabIndex = 0;
		this.button_Finish.Text = "Finish";
		this.button_Finish.Title = "";
		this.button_Finish.UseVisualStyleBackColor = true;
		this.button_Finish.Click += new System.EventHandler(this.button_Finish_Click);
		this.button_Finish.Leave += new System.EventHandler(this.button_Finish_Leave);
		this.button_Finish.Enter += new System.EventHandler(this.button_Finish_Enter);
		this.tabPage_Chart.Controls.Add(this.panel_ScrollChart);
		this.tabPage_Chart.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage_Chart.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Chart.Name = "tabPage_Chart";
		this.tabPage_Chart.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Chart.Size = new System.Drawing.Size(524, 447);
		this.tabPage_Chart.TabIndex = 1;
		this.tabPage_Chart.Text = "Chart Options";
		this.tabPage_Chart.UseVisualStyleBackColor = true;
		this.panel_ScrollChart.AutoScroll = true;
		this.panel_ScrollChart.AutoScrollMinSize = new System.Drawing.Size(0, 450);
		this.panel_ScrollChart.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScrollChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollChart.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollChart.Name = "panel_ScrollChart";
		this.panel_ScrollChart.Size = new System.Drawing.Size(518, 441);
		this.panel_ScrollChart.TabIndex = 2;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(501, 450);
		this.ezyNavHoleChartController1.TabIndex = 1;
		this.tabControl_AnalysisResults.Controls.Add(this.tabPage_Results);
		this.tabControl_AnalysisResults.Controls.Add(this.tabPage_Chart);
		this.tabControl_AnalysisResults.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl_AnalysisResults.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl_AnalysisResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl_AnalysisResults.Location = new System.Drawing.Point(0, 0);
		this.tabControl_AnalysisResults.Name = "tabControl_AnalysisResults";
		this.tabControl_AnalysisResults.SelectedIndex = 0;
		this.tabControl_AnalysisResults.Size = new System.Drawing.Size(392, 624);
		this.tabControl_AnalysisResults.TabIndex = 39;
		this.tabControl_AnalysisResults.Click += new System.EventHandler(this.tabControl_AnalysisResults_Click);
		this.tabPage_Results.Controls.Add(this.panel_AutoScroll);
		this.tabPage_Results.Controls.Add(this.groupBox_Options);
		this.tabPage_Results.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage_Results.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Results.Name = "tabPage_Results";
		this.tabPage_Results.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Results.Size = new System.Drawing.Size(384, 587);
		this.tabPage_Results.TabIndex = 0;
		this.tabPage_Results.Text = "Results";
		this.tabPage_Results.UseVisualStyleBackColor = true;
		this.panel_AutoScroll.AutoScroll = true;
		this.panel_AutoScroll.Controls.Add(this.groupBox_ExtrapolatedDistanceFromTarget);
		this.panel_AutoScroll.Controls.Add(this.groupBox_AngleChange);
		this.panel_AutoScroll.Controls.Add(this.groupBox_DoglegSeverity);
		this.panel_AutoScroll.Controls.Add(this.groupBox_ToolFaceSetting);
		this.panel_AutoScroll.Controls.Add(this.groupBox_Azimuth);
		this.panel_AutoScroll.Controls.Add(this.groupBox_Inclination);
		this.panel_AutoScroll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_AutoScroll.Location = new System.Drawing.Point(3, 3);
		this.panel_AutoScroll.Name = "panel_AutoScroll";
		this.panel_AutoScroll.Size = new System.Drawing.Size(378, 499);
		this.panel_AutoScroll.TabIndex = 43;
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label_Distance_TVD);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label4);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label_Distance_Horizontal);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label3);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label_Distance_East);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label2);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label_Distance_North);
		this.groupBox_ExtrapolatedDistanceFromTarget.Controls.Add(this.label1);
		this.groupBox_ExtrapolatedDistanceFromTarget.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_ExtrapolatedDistanceFromTarget.Location = new System.Drawing.Point(0, 362);
		this.groupBox_ExtrapolatedDistanceFromTarget.Name = "groupBox_ExtrapolatedDistanceFromTarget";
		this.groupBox_ExtrapolatedDistanceFromTarget.Size = new System.Drawing.Size(378, 128);
		this.groupBox_ExtrapolatedDistanceFromTarget.TabIndex = 49;
		this.groupBox_ExtrapolatedDistanceFromTarget.TabStop = false;
		this.groupBox_ExtrapolatedDistanceFromTarget.Text = "Extrapolated distance from target (m):";
		this.label_Distance_TVD.AutoSize = true;
		this.label_Distance_TVD.Location = new System.Drawing.Point(106, 97);
		this.label_Distance_TVD.Name = "label_Distance_TVD";
		this.label_Distance_TVD.Size = new System.Drawing.Size(180, 24);
		this.label_Distance_TVD.TabIndex = 5;
		this.label_Distance_TVD.Text = "label_Distance_TVD";
		this.label_Distance_TVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 97);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(53, 24);
		this.label4.TabIndex = 5;
		this.label4.Text = "TVD:";
		this.label_Distance_Horizontal.AutoSize = true;
		this.label_Distance_Horizontal.Location = new System.Drawing.Point(106, 73);
		this.label_Distance_Horizontal.Name = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.Size = new System.Drawing.Size(226, 24);
		this.label_Distance_Horizontal.TabIndex = 5;
		this.label_Distance_Horizontal.Text = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 73);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(87, 24);
		this.label3.TabIndex = 5;
		this.label3.Text = "Distance:";
		this.label_Distance_East.AutoSize = true;
		this.label_Distance_East.Location = new System.Drawing.Point(106, 49);
		this.label_Distance_East.Name = "label_Distance_East";
		this.label_Distance_East.Size = new System.Drawing.Size(178, 24);
		this.label_Distance_East.TabIndex = 5;
		this.label_Distance_East.Text = "label_Distance_East";
		this.label_Distance_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(51, 24);
		this.label2.TabIndex = 5;
		this.label2.Text = "East:";
		this.label_Distance_North.AutoSize = true;
		this.label_Distance_North.Location = new System.Drawing.Point(106, 25);
		this.label_Distance_North.Name = "label_Distance_North";
		this.label_Distance_North.Size = new System.Drawing.Size(188, 24);
		this.label_Distance_North.TabIndex = 5;
		this.label_Distance_North.Text = "label_Distance_North";
		this.label_Distance_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 24);
		this.label1.TabIndex = 5;
		this.label1.Text = "North:";
		this.groupBox_AngleChange.Controls.Add(this.label_AngleChange1);
		this.groupBox_AngleChange.Controls.Add(this.label_AngleChangeLabel);
		this.groupBox_AngleChange.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_AngleChange.Location = new System.Drawing.Point(0, 279);
		this.groupBox_AngleChange.Name = "groupBox_AngleChange";
		this.groupBox_AngleChange.Size = new System.Drawing.Size(378, 83);
		this.groupBox_AngleChange.TabIndex = 46;
		this.groupBox_AngleChange.TabStop = false;
		this.groupBox_AngleChange.Text = "Angle Change:";
		this.groupBox_AngleChange.Visible = false;
		this.label_AngleChange1.AutoSize = true;
		this.label_AngleChange1.Location = new System.Drawing.Point(7, 49);
		this.label_AngleChange1.Name = "label_AngleChange1";
		this.label_AngleChange1.Size = new System.Drawing.Size(40, 24);
		this.label_AngleChange1.TabIndex = 0;
		this.label_AngleChange1.Text = "null";
		this.label_AngleChangeLabel.AutoSize = true;
		this.label_AngleChangeLabel.Location = new System.Drawing.Point(6, 25);
		this.label_AngleChangeLabel.Name = "label_AngleChangeLabel";
		this.label_AngleChangeLabel.Size = new System.Drawing.Size(248, 24);
		this.label_AngleChangeLabel.TabIndex = 0;
		this.label_AngleChangeLabel.Text = "The overall angle change is:";
		this.groupBox_DoglegSeverity.Controls.Add(this.label_Dogleg);
		this.groupBox_DoglegSeverity.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_DoglegSeverity.Location = new System.Drawing.Point(0, 217);
		this.groupBox_DoglegSeverity.Name = "groupBox_DoglegSeverity";
		this.groupBox_DoglegSeverity.Size = new System.Drawing.Size(378, 62);
		this.groupBox_DoglegSeverity.TabIndex = 45;
		this.groupBox_DoglegSeverity.TabStop = false;
		this.groupBox_DoglegSeverity.Text = "Dogleg severity:";
		this.label_Dogleg.AutoSize = true;
		this.label_Dogleg.Location = new System.Drawing.Point(7, 25);
		this.label_Dogleg.Name = "label_Dogleg";
		this.label_Dogleg.Size = new System.Drawing.Size(40, 24);
		this.label_Dogleg.TabIndex = 0;
		this.label_Dogleg.Text = "null";
		this.groupBox_ToolFaceSetting.Controls.Add(this.label_ToolFaceSetting);
		this.groupBox_ToolFaceSetting.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_ToolFaceSetting.Location = new System.Drawing.Point(0, 158);
		this.groupBox_ToolFaceSetting.Name = "groupBox_ToolFaceSetting";
		this.groupBox_ToolFaceSetting.Size = new System.Drawing.Size(378, 59);
		this.groupBox_ToolFaceSetting.TabIndex = 48;
		this.groupBox_ToolFaceSetting.TabStop = false;
		this.groupBox_ToolFaceSetting.Text = "Toolface Setting:";
		this.label_ToolFaceSetting.AutoSize = true;
		this.label_ToolFaceSetting.Location = new System.Drawing.Point(6, 25);
		this.label_ToolFaceSetting.Name = "label_ToolFaceSetting";
		this.label_ToolFaceSetting.Size = new System.Drawing.Size(40, 24);
		this.label_ToolFaceSetting.TabIndex = 0;
		this.label_ToolFaceSetting.Text = "null";
		this.groupBox_Azimuth.Controls.Add(this.label_AzimuthTurnRate);
		this.groupBox_Azimuth.Controls.Add(this.label_AzimuthTrend);
		this.groupBox_Azimuth.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Azimuth.Location = new System.Drawing.Point(0, 78);
		this.groupBox_Azimuth.Name = "groupBox_Azimuth";
		this.groupBox_Azimuth.Size = new System.Drawing.Size(378, 80);
		this.groupBox_Azimuth.TabIndex = 44;
		this.groupBox_Azimuth.TabStop = false;
		this.groupBox_Azimuth.Text = "Azimuth:";
		this.label_AzimuthTurnRate.AutoSize = true;
		this.label_AzimuthTurnRate.Location = new System.Drawing.Point(7, 49);
		this.label_AzimuthTurnRate.Name = "label_AzimuthTurnRate";
		this.label_AzimuthTurnRate.Size = new System.Drawing.Size(40, 24);
		this.label_AzimuthTurnRate.TabIndex = 0;
		this.label_AzimuthTurnRate.Text = "null";
		this.label_AzimuthTrend.AutoSize = true;
		this.label_AzimuthTrend.Location = new System.Drawing.Point(7, 25);
		this.label_AzimuthTrend.Name = "label_AzimuthTrend";
		this.label_AzimuthTrend.Size = new System.Drawing.Size(40, 24);
		this.label_AzimuthTrend.TabIndex = 0;
		this.label_AzimuthTrend.Text = "null";
		this.groupBox_Inclination.Controls.Add(this.label_InclinationBuildRate);
		this.groupBox_Inclination.Controls.Add(this.label_InclinationTrend);
		this.groupBox_Inclination.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Inclination.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Inclination.Name = "groupBox_Inclination";
		this.groupBox_Inclination.Size = new System.Drawing.Size(378, 78);
		this.groupBox_Inclination.TabIndex = 43;
		this.groupBox_Inclination.TabStop = false;
		this.groupBox_Inclination.Text = "Inclination:";
		this.label_InclinationBuildRate.AutoSize = true;
		this.label_InclinationBuildRate.Location = new System.Drawing.Point(7, 49);
		this.label_InclinationBuildRate.Name = "label_InclinationBuildRate";
		this.label_InclinationBuildRate.Size = new System.Drawing.Size(40, 24);
		this.label_InclinationBuildRate.TabIndex = 0;
		this.label_InclinationBuildRate.Text = "null";
		this.label_InclinationTrend.AutoSize = true;
		this.label_InclinationTrend.Location = new System.Drawing.Point(7, 25);
		this.label_InclinationTrend.Name = "label_InclinationTrend";
		this.label_InclinationTrend.Size = new System.Drawing.Size(40, 24);
		this.label_InclinationTrend.TabIndex = 0;
		this.label_InclinationTrend.Text = "null";
		this.groupBox_Options.Controls.Add(this.button_GenerateReport);
		this.groupBox_Options.Controls.Add(this.button_AdvancedResults);
		this.groupBox_Options.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Options.Location = new System.Drawing.Point(3, 502);
		this.groupBox_Options.Name = "groupBox_Options";
		this.groupBox_Options.Size = new System.Drawing.Size(378, 82);
		this.groupBox_Options.TabIndex = 38;
		this.groupBox_Options.TabStop = false;
		this.groupBox_Options.Text = "Options";
		this.button_GenerateReport.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_GenerateReport.AutoEllipsis = true;
		this.button_GenerateReport.BackColor = System.Drawing.Color.Transparent;
		this.button_GenerateReport.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_GenerateReport.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_GenerateReport.ColorOn = System.Drawing.Color.Orange;
		this.button_GenerateReport.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_GenerateReport.ColorPress = System.Drawing.Color.Orange;
		this.button_GenerateReport.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_GenerateReport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_GenerateReport.FadingSpeed = 35;
		this.button_GenerateReport.FlatAppearance.BorderSize = 0;
		this.button_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_GenerateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_GenerateReport.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_GenerateReport.Image = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.button_GenerateReport.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_GenerateReport.ImageOffset = 0;
		this.button_GenerateReport.IsPressed = false;
		this.button_GenerateReport.KeepPress = false;
		this.button_GenerateReport.Location = new System.Drawing.Point(193, 28);
		this.button_GenerateReport.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_GenerateReport.MenuPos = new System.Drawing.Point(0, 0);
		this.button_GenerateReport.Name = "button_GenerateReport";
		this.button_GenerateReport.Radius = 10;
		this.button_GenerateReport.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_GenerateReport.Size = new System.Drawing.Size(179, 45);
		this.button_GenerateReport.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_GenerateReport.SplitDistance = 0;
		this.button_GenerateReport.TabIndex = 0;
		this.button_GenerateReport.Text = "Report";
		this.button_GenerateReport.Title = "";
		this.button_GenerateReport.UseVisualStyleBackColor = true;
		this.button_GenerateReport.Click += new System.EventHandler(this.button_GenerateReport_Click);
		this.button_GenerateReport.Leave += new System.EventHandler(this.button_GenerateReport_Leave);
		this.button_GenerateReport.Enter += new System.EventHandler(this.button_GenerateReport_Enter);
		this.button_AdvancedResults.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_AdvancedResults.AutoEllipsis = true;
		this.button_AdvancedResults.BackColor = System.Drawing.Color.Transparent;
		this.button_AdvancedResults.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AdvancedResults.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AdvancedResults.ColorOn = System.Drawing.Color.Orange;
		this.button_AdvancedResults.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_AdvancedResults.ColorPress = System.Drawing.Color.Orange;
		this.button_AdvancedResults.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AdvancedResults.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AdvancedResults.FadingSpeed = 35;
		this.button_AdvancedResults.FlatAppearance.BorderSize = 0;
		this.button_AdvancedResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AdvancedResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AdvancedResults.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AdvancedResults.Image = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.button_AdvancedResults.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AdvancedResults.ImageOffset = 0;
		this.button_AdvancedResults.IsPressed = false;
		this.button_AdvancedResults.KeepPress = false;
		this.button_AdvancedResults.Location = new System.Drawing.Point(6, 28);
		this.button_AdvancedResults.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AdvancedResults.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AdvancedResults.Name = "button_AdvancedResults";
		this.button_AdvancedResults.Radius = 10;
		this.button_AdvancedResults.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AdvancedResults.Size = new System.Drawing.Size(181, 45);
		this.button_AdvancedResults.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AdvancedResults.SplitDistance = 0;
		this.button_AdvancedResults.TabIndex = 1;
		this.button_AdvancedResults.Text = "Advanced";
		this.button_AdvancedResults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AdvancedResults.Title = "";
		this.button_AdvancedResults.UseVisualStyleBackColor = true;
		this.button_AdvancedResults.Click += new System.EventHandler(this.button_AdvancedResults_Click);
		this.button_AdvancedResults.Leave += new System.EventHandler(this.button_AdvancedResults_Leave);
		this.button_AdvancedResults.Enter += new System.EventHandler(this.button_AdvancedResults_Enter);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Trend_Analysis;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(350, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 60);
		this.panel1.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.Name = "Body_TrendAnalysis_Results";
		base.Size = new System.Drawing.Size(414, 774);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.tabPage_Chart.ResumeLayout(false);
		this.panel_ScrollChart.ResumeLayout(false);
		this.tabControl_AnalysisResults.ResumeLayout(false);
		this.tabPage_Results.ResumeLayout(false);
		this.panel_AutoScroll.ResumeLayout(false);
		this.groupBox_ExtrapolatedDistanceFromTarget.ResumeLayout(false);
		this.groupBox_ExtrapolatedDistanceFromTarget.PerformLayout();
		this.groupBox_AngleChange.ResumeLayout(false);
		this.groupBox_AngleChange.PerformLayout();
		this.groupBox_DoglegSeverity.ResumeLayout(false);
		this.groupBox_DoglegSeverity.PerformLayout();
		this.groupBox_ToolFaceSetting.ResumeLayout(false);
		this.groupBox_ToolFaceSetting.PerformLayout();
		this.groupBox_Azimuth.ResumeLayout(false);
		this.groupBox_Azimuth.PerformLayout();
		this.groupBox_Inclination.ResumeLayout(false);
		this.groupBox_Inclination.PerformLayout();
		this.groupBox_Options.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
