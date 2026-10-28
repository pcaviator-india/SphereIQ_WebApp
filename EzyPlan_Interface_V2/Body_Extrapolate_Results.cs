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

public class Body_Extrapolate_Results : Layout_Body_Wizard
{
	private delegate EzyReport.Report AsyncConsOperationsDelegate(bool surveyData, bool extrapolatedSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working);

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

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private EzyNavHoleChart m_chart;

	private ExtrapolatedSurveyList m_extrapolatedList;

	private AbsoluteSurveyList m_surveyList;

	private AbsoluteSurvey m_endOfStraightPath;

	private AbsoluteSurvey m_endOfExtrapolation;

	private AbsoluteSurvey m_surveyAtTargetTVD;

	private RelativePosition m_extrapolatedDistance;

	private RelativePosition m_straightPathDistance;

	private RelativePosition m_distanceFromTargetAtTargetTVD;

	private Popup_Extrapolate_Advanced m_advancedResultsPopup;

	private DriveArrivedDelegate m_driveArrived;

	private DriveRemovedDelegate m_driveRemoved;

	private IContainer components;

	private GroupBox groupBox1;

	private RibbonMenuButton button_GenerateReport;

	private RibbonMenuButton button_AdvancedResults;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Finish;

	private TabControl tabControl1;

	private TabPage tabPage_Results;

	private Panel panel_ScrollResults;

	private TabPage tabPage_ChartControl;

	private Panel panel_ScrollChart;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private GroupBox groupBox_StraightPathDistance;

	private GroupBox groupBox_DistanceFromTarget;

	private GroupBox groupBox_TargetTVDDistance;

	private Panel panel5;

	private Label label20;

	private Label label19;

	private Label label_Distance_TVD;

	private Label label18;

	private Label label_Distance_Horizontal;

	private Label label17;

	private Label label_Distance_East;

	private Label label_Distance_North;

	private Label label16;

	private Label label15;

	private Label label14;

	private Label label_TargetTVDDistance_HD;

	private Label label_TargetTVDDistance_East;

	private Label label_TargetTVDDistance_North;

	private Label label_Straight_Horizontal;

	private Label label23;

	private Label label_Straight_East;

	private Label label_Straight_North;

	private Label label22;

	private Label label21;

	public event EventHandler ButtonPressed;

	public Body_Extrapolate_Results(DrillHoleFile drillHoleFile, DrillHoleStatus drillHoleStatus, EzyNavHoleChart chart)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_drillHoleFile = drillHoleFile;
		m_drillHoleStatus = drillHoleStatus;
		m_chart = chart;
		Visible = false;
	}

	public void Display(ExtrapolatedSurveyList extrapolatedList)
	{
		tabControl1.SelectedIndex = 0;
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		m_extrapolatedList = extrapolatedList;
		ResetCalculatedFields();
		Calculations(out m_surveyList, out m_endOfStraightPath, out m_endOfExtrapolation, out m_straightPathDistance, out m_extrapolatedDistance, out m_surveyAtTargetTVD, out m_distanceFromTargetAtTargetTVD);
		UpdateFields(m_straightPathDistance, m_extrapolatedDistance, m_distanceFromTargetAtTargetTVD);
		UpdateChart(m_surveyList, m_endOfStraightPath, m_surveyAtTargetTVD, m_drillHoleFile.Target);
		UpdateAdvancedPopup(m_endOfExtrapolation, m_endOfStraightPath, m_drillHoleFile.Target, m_surveyAtTargetTVD, extrapolatedList.GetFullExtrapolatedPath());
		Visible = true;
		Focus();
	}

	private void UpdateFields(RelativePosition straightPathDistance, RelativePosition distanceFromTarget, RelativePosition distanceFromTargetAtTargetTVD)
	{
		if (distanceFromTarget != null)
		{
			label_Distance_North.Text = distanceFromTarget.NorthingDistance.ToString();
			label_Distance_East.Text = distanceFromTarget.EastingDistance.ToString();
			label_Distance_Horizontal.Text = distanceFromTarget.HorizontalDeparture.ToString();
			label_Distance_TVD.Text = distanceFromTarget.VerticalDistance.ToString();
			groupBox_DistanceFromTarget.Visible = true;
		}
		else
		{
			groupBox_DistanceFromTarget.Visible = false;
		}
		if (straightPathDistance != null)
		{
			label_Straight_North.Text = straightPathDistance.NorthingDistance.ToString();
			label_Straight_East.Text = straightPathDistance.EastingDistance.ToString();
			label_Straight_Horizontal.Text = straightPathDistance.HorizontalDeparture.ToString();
			groupBox_StraightPathDistance.Visible = true;
		}
		else
		{
			groupBox_StraightPathDistance.Visible = false;
		}
		if (distanceFromTargetAtTargetTVD != null)
		{
			label_TargetTVDDistance_North.Text = distanceFromTargetAtTargetTVD.NorthingDistance.ToString();
			label_TargetTVDDistance_East.Text = distanceFromTargetAtTargetTVD.EastingDistance.ToString();
			label_TargetTVDDistance_HD.Text = distanceFromTargetAtTargetTVD.HorizontalDeparture.ToString();
			groupBox_TargetTVDDistance.Visible = true;
		}
		else
		{
			groupBox_TargetTVDDistance.Visible = false;
		}
	}

	private void UpdateChart(AbsoluteSurveyList surveyList, AbsoluteSurvey straightPathFromExtrapolation, AbsoluteSurvey surveyAtTargetTVD, Target target)
	{
		if ((m_extrapolatedList != null) & (m_drillHoleFile != null))
		{
			m_chart.ResetChart();
			m_chart.ResetStraightPath();
			m_chart.ResetDistanceFromTarget();
			m_chart.ResetSurveyAtTargetTVD();
			m_chart.SetExtrapolated(m_extrapolatedList, visible: true);
			if (straightPathFromExtrapolation != null)
			{
				m_chart.SetStraightPath(m_extrapolatedList.GetLastSurvey(), straightPathFromExtrapolation, visible: true);
			}
			if ((surveyAtTargetTVD != null) & (target != null))
			{
				m_chart.SetSurveyAtTargetTVD(surveyAtTargetTVD, visible: true);
				m_chart.SetDistanceFromTarget(surveyAtTargetTVD, target, visible: true);
			}
		}
	}

	private void UpdateAdvancedPopup(AbsoluteSurvey endOfExtrapolate, AbsoluteSurvey endOfStraightPath, Target target, AbsoluteSurvey surveyAtTargetTVD, AbsoluteSurveyList extrapolatedSurveyData)
	{
		m_advancedResultsPopup = new Popup_Extrapolate_Advanced(endOfExtrapolate, endOfStraightPath, target, surveyAtTargetTVD, extrapolatedSurveyData);
	}

	private void Calculations(out AbsoluteSurveyList surveyList, out AbsoluteSurvey straightPathFromExtrapolation, out AbsoluteSurvey endOfExtrapolation, out RelativePosition straightPathDistance, out RelativePosition extrapolateDistance, out AbsoluteSurvey surveyAtTargetTVD, out RelativePosition distanceFromTargetAtTargetTVD)
	{
		surveyList = new AbsoluteSurveyList();
		NavMath navMath = new NavMath();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref surveyList);
		if (AboveTarget(m_drillHoleFile.Target, m_extrapolatedList.GetLastSurvey()))
		{
			distanceFromTargetAtTargetTVD = null;
			surveyAtTargetTVD = null;
			straightPathFromExtrapolation = new AbsoluteSurvey();
			navMath.StraightPathToTargetTVD(m_drillHoleFile.Target, m_extrapolatedList.GetLastSurvey(), ref straightPathFromExtrapolation);
			straightPathDistance = new RelativePosition();
			navMath.DistanceFromTarget(m_drillHoleFile.Target, straightPathFromExtrapolation, ref straightPathDistance);
		}
		else
		{
			straightPathDistance = null;
			straightPathFromExtrapolation = null;
			navMath.DistanceFromTargetAtTargetTVD(m_drillHoleFile.Target, m_extrapolatedList, out surveyAtTargetTVD, out distanceFromTargetAtTargetTVD);
		}
		extrapolateDistance = new RelativePosition();
		navMath.DistanceFromTarget(m_drillHoleFile.Target, m_extrapolatedList.GetLastSurvey(), ref extrapolateDistance);
		endOfExtrapolation = m_extrapolatedList.GetLastSurvey();
	}

	private void ResetCalculatedFields()
	{
		m_surveyList = null;
		m_endOfStraightPath = null;
		m_endOfExtrapolation = null;
		m_surveyAtTargetTVD = null;
		m_extrapolatedDistance = null;
		m_straightPathDistance = null;
		m_distanceFromTargetAtTargetTVD = null;
	}

	private bool AboveTarget(Target target, AbsoluteSurvey lastSurvey)
	{
		if (lastSurvey == null)
		{
			return false;
		}
		if (lastSurvey.TVD < target.TVD)
		{
			return true;
		}
		return false;
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	private void Display_AdvancedResults()
	{
		m_advancedResultsPopup.ShowDialog();
	}

	private void ConstructReport()
	{
		if (GetUserInput(out var author, out var notes, out var surveyData, out var extrapolatedSurveys, out var charts))
		{
			Popup_WorkingIndicator working = new Popup_WorkingIndicator("Generating Report", "Generating Report", showCancelButton: true, 7);
			AsyncConsOperationsDelegate asyncConsOperationsDelegate = AsyncConsOperations;
			asyncConsOperationsDelegate.BeginInvoke(surveyData, extrapolatedSurveys, charts, author, notes, ref working, AsyncConsComplete, asyncConsOperationsDelegate);
			working.ShowDialog();
		}
	}

	private bool GetUserInput(out string author, out string notes, out bool surveyData, out bool extrapolatedSurveys, out bool charts)
	{
		Popup_Report_UserFields popup_Report_UserFields = new Popup_Report_UserFields();
		Popup_Report_Components popup_Report_Components = new Popup_Report_Components(surveyData: true, buildSurveys: false, extrapolatedSurveys: true, analysedSurveys: false, charts: true);
		Popup_Report_UserFields.UserFieldsDialogResult userFieldsDialogResult = null;
		Popup_Report_Components.ComponentsDialogResult componentsDialogResult = null;
		author = "";
		notes = "";
		surveyData = false;
		extrapolatedSurveys = false;
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
				charts = componentsDialogResult.Charts;
				return true;
			}
		}
	}

	private EzyReport.Report AsyncConsOperations(bool surveyData, bool extrapolatedSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working)
	{
		EzyReport.Report report = new EzyReport.Report("Extrapolation Results: " + m_drillHoleFile.HoleID, author);
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
		if (UpdateProgress(ref working, "Adding: Extrapolation Results"))
		{
			return null;
		}
		report.AppendPage(new Report_Extrapolate(m_endOfExtrapolation, m_endOfStraightPath, m_surveyAtTargetTVD, m_extrapolatedDistance, m_distanceFromTargetAtTargetTVD, m_straightPathDistance), "Extrapolation Results");
		if (UpdateProgress(ref working, "Adding: Extrapolated Surveys"))
		{
			return null;
		}
		if (extrapolatedSurveys)
		{
			report.AppendPage(m_extrapolatedList.List, "Extrapolated Survey's");
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

	private void AsyncConsComplete(IAsyncResult result)
	{
		AsyncConsOperationsDelegate asyncConsOperationsDelegate = (AsyncConsOperationsDelegate)result.AsyncState;
		Popup_WorkingIndicator working = null;
		EzyReport.Report report = asyncConsOperationsDelegate.EndInvoke(ref working, result);
		if (report != null)
		{
			DisplayGeneratedReport(report, ref working);
			report = null;
			result = null;
		}
		else
		{
			MethodInvoker methodInvoker = DelayedGarbageCollection;
			methodInvoker.BeginInvoke(null, null);
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
			MethodInvoker methodInvoker = DelayedGarbageCollection;
			methodInvoker.BeginInvoke(null, null);
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

	private void button_Finish_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Finish;
		OnButtonPressed(e2);
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Back;
		OnButtonPressed(e2);
	}

	private void button_GenerateReport_Click(object sender, EventArgs e)
	{
		ConstructReport();
	}

	private void button_AdvancedResults_Click(object sender, EventArgs e)
	{
		Display_AdvancedResults();
	}

	private void button_AdvancedResults_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AdvancedResults);
	}

	private void button_AdvancedResults_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AdvancedResults);
	}

	private void button_GenerateReport_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_GenerateReport);
	}

	private void button_GenerateReport_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_GenerateReport);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
	}

	private void button_Finish_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Finish);
	}

	private void button_Finish_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Finish);
	}

	private void tabControl1_Click(object sender, EventArgs e)
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button_GenerateReport = new RibbonStyle.RibbonMenuButton();
		this.button_AdvancedResults = new RibbonStyle.RibbonMenuButton();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage_Results = new System.Windows.Forms.TabPage();
		this.panel_ScrollResults = new System.Windows.Forms.Panel();
		this.groupBox_StraightPathDistance = new System.Windows.Forms.GroupBox();
		this.label_Straight_Horizontal = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label_Straight_East = new System.Windows.Forms.Label();
		this.label_Straight_North = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.groupBox_DistanceFromTarget = new System.Windows.Forms.GroupBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label_Distance_TVD = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label_Distance_Horizontal = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label_Distance_East = new System.Windows.Forms.Label();
		this.label_Distance_North = new System.Windows.Forms.Label();
		this.groupBox_TargetTVDDistance = new System.Windows.Forms.GroupBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_HD = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_East = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_North = new System.Windows.Forms.Label();
		this.tabPage_ChartControl = new System.Windows.Forms.TabPage();
		this.panel_ScrollChart = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.button_Finish = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage_Results.SuspendLayout();
		this.panel_ScrollResults.SuspendLayout();
		this.groupBox_StraightPathDistance.SuspendLayout();
		this.groupBox_DistanceFromTarget.SuspendLayout();
		this.groupBox_TargetTVDDistance.SuspendLayout();
		this.tabPage_ChartControl.SuspendLayout();
		this.panel_ScrollChart.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(212, 24);
		base.label_headerText1.Text = "Extrapolation Results:";
		base.groupBox_frame.Size = new System.Drawing.Size(372, 667);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.button_Finish);
		base.panel_buttonPanel.Location = new System.Drawing.Point(100, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(288, 56);
		base.panel_body.Location = new System.Drawing.Point(0, 54);
		base.panel_body.Size = new System.Drawing.Size(388, 683);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(388, 54);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 737);
		base.panel_footer.Size = new System.Drawing.Size(388, 56);
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.tabControl1);
		base.panel_scrollPanel.Size = new System.Drawing.Size(366, 648);
		this.groupBox1.Controls.Add(this.button_GenerateReport);
		this.groupBox1.Controls.Add(this.button_AdvancedResults);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(3, 517);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(352, 91);
		this.groupBox1.TabIndex = 37;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Options";
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
		this.button_GenerateReport.Location = new System.Drawing.Point(174, 28);
		this.button_GenerateReport.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_GenerateReport.MenuPos = new System.Drawing.Point(0, 0);
		this.button_GenerateReport.Name = "button_GenerateReport";
		this.button_GenerateReport.Radius = 20;
		this.button_GenerateReport.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_GenerateReport.Size = new System.Drawing.Size(172, 50);
		this.button_GenerateReport.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_GenerateReport.SplitDistance = 0;
		this.button_GenerateReport.TabIndex = 1;
		this.button_GenerateReport.Text = "Report";
		this.button_GenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
		this.button_AdvancedResults.Location = new System.Drawing.Point(3, 28);
		this.button_AdvancedResults.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AdvancedResults.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AdvancedResults.Name = "button_AdvancedResults";
		this.button_AdvancedResults.Radius = 20;
		this.button_AdvancedResults.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AdvancedResults.Size = new System.Drawing.Size(165, 50);
		this.button_AdvancedResults.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AdvancedResults.SplitDistance = 0;
		this.button_AdvancedResults.TabIndex = 0;
		this.button_AdvancedResults.Text = "Advanced";
		this.button_AdvancedResults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AdvancedResults.Title = "";
		this.button_AdvancedResults.UseVisualStyleBackColor = true;
		this.button_AdvancedResults.Click += new System.EventHandler(this.button_AdvancedResults_Click);
		this.button_AdvancedResults.Leave += new System.EventHandler(this.button_AdvancedResults_Leave);
		this.button_AdvancedResults.Enter += new System.EventHandler(this.button_AdvancedResults_Enter);
		this.tabControl1.Controls.Add(this.tabPage_Results);
		this.tabControl1.Controls.Add(this.tabPage_ChartControl);
		this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(366, 648);
		this.tabControl1.TabIndex = 0;
		this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
		this.tabPage_Results.Controls.Add(this.panel_ScrollResults);
		this.tabPage_Results.Controls.Add(this.groupBox1);
		this.tabPage_Results.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Results.Name = "tabPage_Results";
		this.tabPage_Results.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Results.Size = new System.Drawing.Size(358, 611);
		this.tabPage_Results.TabIndex = 0;
		this.tabPage_Results.Text = "Results";
		this.tabPage_Results.UseVisualStyleBackColor = true;
		this.panel_ScrollResults.AutoScroll = true;
		this.panel_ScrollResults.Controls.Add(this.groupBox_StraightPathDistance);
		this.panel_ScrollResults.Controls.Add(this.groupBox_DistanceFromTarget);
		this.panel_ScrollResults.Controls.Add(this.groupBox_TargetTVDDistance);
		this.panel_ScrollResults.Cursor = System.Windows.Forms.Cursors.Default;
		this.panel_ScrollResults.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollResults.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollResults.Name = "panel_ScrollResults";
		this.panel_ScrollResults.Size = new System.Drawing.Size(352, 514);
		this.panel_ScrollResults.TabIndex = 38;
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_Horizontal);
		this.groupBox_StraightPathDistance.Controls.Add(this.label23);
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_East);
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_North);
		this.groupBox_StraightPathDistance.Controls.Add(this.label22);
		this.groupBox_StraightPathDistance.Controls.Add(this.label21);
		this.groupBox_StraightPathDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_StraightPathDistance.Location = new System.Drawing.Point(0, 254);
		this.groupBox_StraightPathDistance.Name = "groupBox_StraightPathDistance";
		this.groupBox_StraightPathDistance.Size = new System.Drawing.Size(352, 131);
		this.groupBox_StraightPathDistance.TabIndex = 18;
		this.groupBox_StraightPathDistance.TabStop = false;
		this.groupBox_StraightPathDistance.Text = "Extrapolated straight path distance from target (m):";
		this.label_Straight_Horizontal.AutoSize = true;
		this.label_Straight_Horizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_Horizontal.Location = new System.Drawing.Point(64, 100);
		this.label_Straight_Horizontal.Name = "label_Straight_Horizontal";
		this.label_Straight_Horizontal.Size = new System.Drawing.Size(188, 20);
		this.label_Straight_Horizontal.TabIndex = 11;
		this.label_Straight_Horizontal.Text = "label_Straight_Horizontal";
		this.label_Straight_Horizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(6, 100);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(40, 20);
		this.label23.TabIndex = 14;
		this.label23.Text = "HD:";
		this.label_Straight_East.AutoSize = true;
		this.label_Straight_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_East.Location = new System.Drawing.Point(64, 80);
		this.label_Straight_East.Name = "label_Straight_East";
		this.label_Straight_East.Size = new System.Drawing.Size(149, 20);
		this.label_Straight_East.TabIndex = 10;
		this.label_Straight_East.Text = "label_Straight_East";
		this.label_Straight_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_Straight_North.AutoSize = true;
		this.label_Straight_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_North.Location = new System.Drawing.Point(64, 60);
		this.label_Straight_North.Name = "label_Straight_North";
		this.label_Straight_North.Size = new System.Drawing.Size(155, 20);
		this.label_Straight_North.TabIndex = 9;
		this.label_Straight_North.Text = "label_Straight_North";
		this.label_Straight_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(6, 80);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(51, 20);
		this.label22.TabIndex = 13;
		this.label22.Text = "East:";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(6, 60);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(58, 20);
		this.label21.TabIndex = 12;
		this.label21.Text = "North:";
		this.groupBox_DistanceFromTarget.Controls.Add(this.label20);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label19);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label_Distance_TVD);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label18);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label_Distance_Horizontal);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label17);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label_Distance_East);
		this.groupBox_DistanceFromTarget.Controls.Add(this.label_Distance_North);
		this.groupBox_DistanceFromTarget.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_DistanceFromTarget.Location = new System.Drawing.Point(0, 130);
		this.groupBox_DistanceFromTarget.Name = "groupBox_DistanceFromTarget";
		this.groupBox_DistanceFromTarget.Size = new System.Drawing.Size(352, 124);
		this.groupBox_DistanceFromTarget.TabIndex = 19;
		this.groupBox_DistanceFromTarget.TabStop = false;
		this.groupBox_DistanceFromTarget.Text = "Extrapolated distance from target (m):";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(6, 94);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(49, 20);
		this.label20.TabIndex = 12;
		this.label20.Text = "TVD:";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(5, 74);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(40, 20);
		this.label19.TabIndex = 14;
		this.label19.Text = "HD:";
		this.label_Distance_TVD.AutoSize = true;
		this.label_Distance_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_TVD.Location = new System.Drawing.Point(63, 94);
		this.label_Distance_TVD.Name = "label_Distance_TVD";
		this.label_Distance_TVD.Size = new System.Drawing.Size(155, 20);
		this.label_Distance_TVD.TabIndex = 15;
		this.label_Distance_TVD.Text = "label_Distance_TVD";
		this.label_Distance_TVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(6, 54);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(51, 20);
		this.label18.TabIndex = 16;
		this.label18.Text = "East:";
		this.label_Distance_Horizontal.AutoSize = true;
		this.label_Distance_Horizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_Horizontal.Location = new System.Drawing.Point(63, 74);
		this.label_Distance_Horizontal.Name = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.Size = new System.Drawing.Size(195, 20);
		this.label_Distance_Horizontal.TabIndex = 11;
		this.label_Distance_Horizontal.Text = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(6, 34);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(58, 20);
		this.label17.TabIndex = 13;
		this.label17.Text = "North:";
		this.label_Distance_East.AutoSize = true;
		this.label_Distance_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_East.Location = new System.Drawing.Point(63, 54);
		this.label_Distance_East.Name = "label_Distance_East";
		this.label_Distance_East.Size = new System.Drawing.Size(156, 20);
		this.label_Distance_East.TabIndex = 10;
		this.label_Distance_East.Text = "label_Distance_East";
		this.label_Distance_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_Distance_North.AutoSize = true;
		this.label_Distance_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_North.Location = new System.Drawing.Point(63, 34);
		this.label_Distance_North.Name = "label_Distance_North";
		this.label_Distance_North.Size = new System.Drawing.Size(162, 20);
		this.label_Distance_North.TabIndex = 9;
		this.label_Distance_North.Text = "label_Distance_North";
		this.label_Distance_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.groupBox_TargetTVDDistance.Controls.Add(this.label16);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label15);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label14);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_HD);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_East);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_North);
		this.groupBox_TargetTVDDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_TargetTVDDistance.Location = new System.Drawing.Point(0, 0);
		this.groupBox_TargetTVDDistance.Name = "groupBox_TargetTVDDistance";
		this.groupBox_TargetTVDDistance.Size = new System.Drawing.Size(352, 130);
		this.groupBox_TargetTVDDistance.TabIndex = 20;
		this.groupBox_TargetTVDDistance.TabStop = false;
		this.groupBox_TargetTVDDistance.Text = "Extrapolated distance from target at target TVD (m):";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(6, 98);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(40, 20);
		this.label16.TabIndex = 12;
		this.label16.Text = "HD:";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(5, 78);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(51, 20);
		this.label15.TabIndex = 13;
		this.label15.Text = "East:";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(6, 58);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(58, 20);
		this.label14.TabIndex = 14;
		this.label14.Text = "North:";
		this.label_TargetTVDDistance_HD.AutoSize = true;
		this.label_TargetTVDDistance_HD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_HD.Location = new System.Drawing.Point(66, 98);
		this.label_TargetTVDDistance_HD.Name = "label_TargetTVDDistance_HD";
		this.label_TargetTVDDistance_HD.Size = new System.Drawing.Size(225, 20);
		this.label_TargetTVDDistance_HD.TabIndex = 11;
		this.label_TargetTVDDistance_HD.Text = "label_TargetTVDDistance_HD";
		this.label_TargetTVDDistance_HD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetTVDDistance_East.AutoSize = true;
		this.label_TargetTVDDistance_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_East.Location = new System.Drawing.Point(66, 78);
		this.label_TargetTVDDistance_East.Name = "label_TargetTVDDistance_East";
		this.label_TargetTVDDistance_East.Size = new System.Drawing.Size(234, 20);
		this.label_TargetTVDDistance_East.TabIndex = 10;
		this.label_TargetTVDDistance_East.Text = "label_TargetTVDDistance_East";
		this.label_TargetTVDDistance_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetTVDDistance_North.AutoSize = true;
		this.label_TargetTVDDistance_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_North.Location = new System.Drawing.Point(66, 58);
		this.label_TargetTVDDistance_North.Name = "label_TargetTVDDistance_North";
		this.label_TargetTVDDistance_North.Size = new System.Drawing.Size(240, 20);
		this.label_TargetTVDDistance_North.TabIndex = 9;
		this.label_TargetTVDDistance_North.Text = "label_TargetTVDDistance_North";
		this.label_TargetTVDDistance_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tabPage_ChartControl.Controls.Add(this.panel_ScrollChart);
		this.tabPage_ChartControl.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage_ChartControl.Location = new System.Drawing.Point(4, 33);
		this.tabPage_ChartControl.Name = "tabPage_ChartControl";
		this.tabPage_ChartControl.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_ChartControl.Size = new System.Drawing.Size(524, 452);
		this.tabPage_ChartControl.TabIndex = 1;
		this.tabPage_ChartControl.Text = "Chart Control";
		this.tabPage_ChartControl.UseVisualStyleBackColor = true;
		this.panel_ScrollChart.AutoScroll = true;
		this.panel_ScrollChart.AutoScrollMinSize = new System.Drawing.Size(0, 450);
		this.panel_ScrollChart.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScrollChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollChart.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollChart.Name = "panel_ScrollChart";
		this.panel_ScrollChart.Size = new System.Drawing.Size(518, 446);
		this.panel_ScrollChart.TabIndex = 0;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(501, 450);
		this.ezyNavHoleChartController1.TabIndex = 1;
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
		this.button_Finish.Location = new System.Drawing.Point(3, 6);
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
		this.button_Back.Location = new System.Drawing.Point(143, 6);
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
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Extrapolate;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(324, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 54);
		this.panel5.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_Extrapolate_Results";
		base.Size = new System.Drawing.Size(388, 793);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage_Results.ResumeLayout(false);
		this.panel_ScrollResults.ResumeLayout(false);
		this.groupBox_StraightPathDistance.ResumeLayout(false);
		this.groupBox_StraightPathDistance.PerformLayout();
		this.groupBox_DistanceFromTarget.ResumeLayout(false);
		this.groupBox_DistanceFromTarget.PerformLayout();
		this.groupBox_TargetTVDDistance.ResumeLayout(false);
		this.groupBox_TargetTVDDistance.PerformLayout();
		this.tabPage_ChartControl.ResumeLayout(false);
		this.panel_ScrollChart.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
