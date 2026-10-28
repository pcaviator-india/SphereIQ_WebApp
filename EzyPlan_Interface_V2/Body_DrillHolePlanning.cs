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

namespace EzyPlan_Interface_V2;

public class Body_DrillHolePlanning : Layout_InfoAndGraph
{
	public enum MinDLStateEnum
	{
		NotCalculated,
		TargetNotAchieved,
		NormalDogleg,
		SevereDogleg,
		ExtremeDogleg,
		Error
	}

	private enum DoglegColourCodes
	{
		Extreme,
		Severe,
		Normal
	}

	private delegate EzyReport.Report AsyncConsOperationsDelegate(bool surveyData, bool charts, string author, string notes, ref Popup_WorkingIndicator working);

	private delegate void DisplayGeneratedReportDelegate(EzyReport.Report report, ref Popup_WorkingIndicator working);

	private delegate void DriveArrivedDelegate(string driveLetter);

	private delegate void DriveRemovedDelegate();

	private EzyNavHoleChart m_chart;

	private DrillHoleFile m_drillHoleFile;

	private MinDLStateEnum m_minDLState;

	private Color m_designTimeColour;

	private DrillHoleStatus m_drillHoleStatus;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private DriveArrivedDelegate m_driveArrived;

	private DriveRemovedDelegate m_driveRemoved;

	private IContainer components;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TextBox textBox5;

	private TextBox textBox6;

	private TextBox textBox7;

	private TextBox textBox8;

	private TextBox textBox9;

	private Label label2;

	private Label label3;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private Panel panel_ScrollPanel;

	private GroupBox groupBox_Target;

	private Label label6;

	private Label label5;

	private Label label4;

	private GroupBox groupBox_StraightPathDistance;

	private GroupBox groupBox_Collar;

	private Label label7;

	private Label label8;

	private GroupBox groupBox_DistanceFromTarget;

	private GroupBox groupBox_Depth;

	private Label label1;

	private Label label9;

	private GroupBox groupBox_Sidetrack;

	private Label label10;

	private Label label11;

	private GroupBox groupBox_TargetTVDDistance;

	private TabPage tabPage2;

	private GroupBox groupBox_build;

	private Panel panel1;

	private Label label12;

	private Panel panel_SevereDL;

	private Panel panel_ExtremeDL;

	private Label label13;

	private Label label_SidetrackMD;

	private Label label_TargetTVDDistance_HD;

	private Label label_TargetTVDDistance_East;

	private Label label_TargetTVDDistance_North;

	private Label label_Distance_TVD;

	private Label label_Distance_Horizontal;

	private Label label_Distance_East;

	private Label label_Distance_North;

	private Label label_HoleTVD;

	private Label label_HoleMD;

	private Label label_SidetrackTVD;

	private Label label_CollarEasting;

	private Label label_TargetTVD;

	private Label label_TargetNorthing;

	private Label label_TargetEasting;

	private Label label_BuildResult;

	private Label label_Straight_Horizontal;

	private Label label_Straight_East;

	private Label label_Straight_North;

	private Label label_CollarNorthing;

	private Label label16;

	private Label label15;

	private Label label14;

	private Label label23;

	private Label label22;

	private Label label21;

	private Label label20;

	private Label label19;

	private Label label18;

	private Label label17;

	public Body_DrillHolePlanning(ref EzyNavHoleChart chart, ref DrillHoleFile drillHoleFile, ref DrillHoleStatus drillHoleStatus, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_chart = chart;
		m_drillHoleFile = drillHoleFile;
		m_drillHoleStatus = drillHoleStatus;
		m_designTimeColour = label_CollarEasting.BackColor;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		SetDLSeverityKey();
		Visible = false;
	}

	public void Display()
	{
		tabControl1.SelectedIndex = 0;
		tabPage2.AutoScrollPosition = new Point(0, 0);
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		Visible = true;
		DisplayFields();
		DisplayCalculatedData(ref m_chart);
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	public void DisplayFields()
	{
		label_CollarNorthing.Text = m_drillHoleFile.Collar.Northing.ToString();
		label_CollarEasting.Text = m_drillHoleFile.Collar.Easting.ToString();
		label_TargetNorthing.Text = m_drillHoleFile.Target.Northing.ToString();
		label_TargetEasting.Text = m_drillHoleFile.Target.Easting.ToString();
		label_TargetTVD.Text = m_drillHoleFile.Target.TVD.ToString();
	}

	private void DisplayCalculatedData(ref EzyNavHoleChart chart)
	{
		m_drillHoleStatus.Reset();
		Collar collar = m_drillHoleFile.Collar;
		Target target = m_drillHoleFile.Target;
		SurveyList surveyList = m_drillHoleFile.SurveyList;
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		RelativePosition distance = new RelativePosition();
		AbsoluteSurvey surveyAtTargetTVD = null;
		RelativePosition distanceFromTarget = null;
		AbsoluteSurvey endOfHoleSurvey = null;
		RelativePosition distance2 = null;
		NavMath navMath = new NavMath();
		navMath.AbsoluteSurvey(collar, surveyList, ref absoluteSurveyList);
		AbsoluteSurvey absoluteSurvey = absoluteSurveyList.RecordAt(absoluteSurveyList.Count() - 1);
		navMath.DistanceFromTarget(target, absoluteSurvey, ref distance);
		AbsoluteSurvey extrapolatedSurvey = null;
		if (m_drillHoleFile.DrillHoleType == DrillHoleFile.DrillHoleTypeEnum.daughterHole)
		{
			navMath.SideTrackHole(absoluteSurveyList, m_drillHoleFile.SideTrackMD, out extrapolatedSurvey);
		}
		bool flag;
		if (absoluteSurvey.TVD >= target.TVD)
		{
			flag = false;
			navMath.DistanceFromTargetAtTargetTVD(target, absoluteSurveyList.List, out surveyAtTargetTVD, out distanceFromTarget);
			groupBox_build.Visible = false;
			MinDLToTarget_Reset();
		}
		else
		{
			endOfHoleSurvey = new AbsoluteSurvey();
			distance2 = new RelativePosition();
			NavMath.StraightPathToTargetTVDEnum straightPathToTargetTVDEnum = navMath.StraightPathToTargetTVD(target, absoluteSurveyList.RecordAt(absoluteSurveyList.Count() - 1), ref endOfHoleSurvey);
			if (straightPathToTargetTVDEnum == NavMath.StraightPathToTargetTVDEnum.NoError)
			{
				navMath.DistanceFromTarget(target, endOfHoleSurvey, ref distance2);
			}
			else
			{
				endOfHoleSurvey = null;
				distance2 = null;
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Straight path to target TVD could not be calculated", NavMath.ToHumanReadable(straightPathToTargetTVDEnum));
				popup_MessageBox_OK.ShowDialog();
			}
			MinDLToTarget_Attempt(absoluteSurvey, target);
			groupBox_build.Visible = true;
			flag = true;
		}
		if (m_drillHoleFile.DrillHoleType == DrillHoleFile.DrillHoleTypeEnum.daughterHole)
		{
			if (flag)
			{
				DisplayFieldData(distance, distance2, absoluteSurvey, null);
				DisplaySidetrackFields(extrapolatedSurvey.MD, extrapolatedSurvey.TVD);
				DisplayChartData(ref chart, collar, target, absoluteSurveyList, extrapolatedSurvey, endOfHoleSurvey, null, displayStandardPlan: false);
			}
			else
			{
				DisplayFieldData(distance, null, absoluteSurvey, distanceFromTarget);
				DisplaySidetrackFields(extrapolatedSurvey.MD, extrapolatedSurvey.TVD);
				DisplayChartData(ref chart, collar, target, absoluteSurveyList, extrapolatedSurvey, null, surveyAtTargetTVD, displayStandardPlan: false);
			}
		}
		else if (m_drillHoleFile.DrillHoleType == DrillHoleFile.DrillHoleTypeEnum.motherHole)
		{
			if (flag)
			{
				DisplayFieldData(distance, distance2, absoluteSurvey, null);
				HideSidetrackFields();
				DisplayChartData(ref chart, collar, target, absoluteSurveyList, null, endOfHoleSurvey, null, displayStandardPlan: true);
			}
			else
			{
				DisplayFieldData(distance, null, absoluteSurvey, distanceFromTarget);
				HideSidetrackFields();
				DisplayChartData(ref chart, collar, target, absoluteSurveyList, null, null, surveyAtTargetTVD, displayStandardPlan: true);
			}
		}
		else if (flag)
		{
			DisplayFieldData(distance, distance2, absoluteSurvey, null);
			DisplaySidetrackFields(m_drillHoleFile.SideTrackMD, "No survey data");
			DisplayChartData(ref chart, collar, target, absoluteSurveyList, null, endOfHoleSurvey, null, displayStandardPlan: false);
		}
		else
		{
			DisplayFieldData(distance, null, absoluteSurvey, distanceFromTarget);
			DisplaySidetrackFields(m_drillHoleFile.SideTrackMD, "No survey data");
			DisplayChartData(ref chart, collar, target, absoluteSurveyList, null, null, surveyAtTargetTVD, displayStandardPlan: false);
		}
		m_drillHoleStatus.AbsoluteSurveyList = absoluteSurveyList;
		m_drillHoleStatus.EndOfHoleSurvey = absoluteSurvey;
		m_drillHoleStatus.StraightPathSurvey = endOfHoleSurvey;
		m_drillHoleStatus.SidetrackSurvey = extrapolatedSurvey;
		m_drillHoleStatus.SurveyAtTargetTVD = surveyAtTargetTVD;
		m_drillHoleStatus.CurrentDistanceFromTarget = distance;
		m_drillHoleStatus.StraightPathDistance = distance2;
		m_drillHoleStatus.DistanceFromTargetAtTargetTVD = distanceFromTarget;
	}

	private void DisplayChartData(ref EzyNavHoleChart chart, Collar collar, Target target, AbsoluteSurveyList surveyData, AbsoluteSurvey sidetrackStart, AbsoluteSurvey straightPathSurvey, AbsoluteSurvey surveyAtTargetTVD, bool displayStandardPlan)
	{
		chart.ClearChartData();
		chart.ResetChart();
		chart.SetTarget(target, visible: true);
		chart.SetCollar(collar, visible: true);
		chart.SetSurveyData(surveyData, visible: true);
		if (sidetrackStart != null)
		{
			chart.SetHolePlan(sidetrackStart, target, visible: true);
			chart.SetSideTrack(sidetrackStart, visible: true);
		}
		else if (displayStandardPlan)
		{
			chart.SetHolePlan(collar, target, visible: true);
		}
		if (straightPathSurvey != null)
		{
			EzyNavHoleChart.SetStraightPathEnum setStraightPathEnum = chart.SetStraightPath(surveyData.RecordAt(surveyData.Count() - 1), straightPathSurvey, visible: true);
			if (setStraightPathEnum != EzyNavHoleChart.SetStraightPathEnum.Set)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Chart warning: ", "Straight path to target could not be charted.\n" + EzyNavHoleChart.ToHumanReadable(setStraightPathEnum) + ".");
				popup_MessageBox_OK.ShowDialog();
			}
		}
		if (surveyAtTargetTVD != null)
		{
			chart.SetSurveyAtTargetTVD(surveyAtTargetTVD, visible: true);
			chart.SetDistanceFromTarget(surveyAtTargetTVD, target, visible: true);
		}
	}

	private void DisplayFieldData(RelativePosition distanceFromTarget, RelativePosition straightPathDistance, AbsoluteSurvey lastSurvey, RelativePosition distanceFromTargetAtTargetTVD)
	{
		if (lastSurvey != null)
		{
			label_HoleMD.Text = lastSurvey.MD.ToString();
			label_HoleTVD.Text = lastSurvey.TVD.ToString();
			groupBox_Depth.Visible = true;
		}
		else
		{
			groupBox_Depth.Visible = false;
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

	private void DisplaySidetrackFields(decimal sidetrackMD, decimal sidetrackTVD)
	{
		DisplaySidetrackFields(sidetrackMD, sidetrackTVD.ToString());
	}

	private void DisplaySidetrackFields(decimal sidetrackMD, string sidetrackTVDText)
	{
		groupBox_Sidetrack.Visible = true;
		label_SidetrackTVD.Text = sidetrackTVDText;
		label_SidetrackMD.Text = sidetrackMD.ToString();
	}

	private void HideSidetrackFields()
	{
		groupBox_Sidetrack.Visible = false;
		label_SidetrackTVD.Text = "";
		label_SidetrackMD.Text = "";
	}

	private void SetDLSeverityKey()
	{
		panel_SevereDL.BackColor = Utility.m_SevereColor;
		panel_ExtremeDL.BackColor = Utility.m_ExtremeColor;
	}

	private void MinDLToTarget_Attempt(AbsoluteSurvey buildStart, Target target)
	{
		NavMath navMath = new NavMath();
		decimal dogLeg = 0m;
		BuildAndHoldResults buildAndHold = new BuildAndHoldResults();
		if (buildStart != null && target != null)
		{
			switch (navMath.MinimumDogLegToTarget(buildStart, target, ref dogLeg, out buildAndHold))
			{
			case NavMath.MinimumDogLegEnum.NoError:
			{
				decimal num = Math.Round(dogLeg, 2, MidpointRounding.AwayFromZero);
				switch (GetDoglegColourCode(dogLeg))
				{
				case DoglegColourCodes.Extreme:
					SetMinDLState(MinDLStateEnum.ExtremeDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				case DoglegColourCodes.Severe:
					SetMinDLState(MinDLStateEnum.SevereDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				case DoglegColourCodes.Normal:
					SetMinDLState(MinDLStateEnum.NormalDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				default:
					SetMinDLState(MinDLStateEnum.NormalDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				}
				break;
			}
			case NavMath.MinimumDogLegEnum.InsufficientTVD:
				SetMinDLState(MinDLStateEnum.TargetNotAchieved, "Insufficient TVD!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.TargetCannotBeAchieved:
				SetMinDLState(MinDLStateEnum.TargetNotAchieved, "Cannot achieve target!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.UnknownError:
				SetMinDLState(MinDLStateEnum.Error, "Unknown error!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.UnknownException:
				SetMinDLState(MinDLStateEnum.Error, "Unknown exception!", pathBelowTargetTVD: false);
				break;
			default:
				SetMinDLState(MinDLStateEnum.Error, "Unknown internal error!", pathBelowTargetTVD: false);
				break;
			}
		}
		else
		{
			MinDLToTarget_Reset();
		}
	}

	private void MinDLToTarget_Reset()
	{
		SetMinDLState(MinDLStateEnum.NotCalculated, "Not calculated.", pathBelowTargetTVD: false);
	}

	private void SetMinDLState(MinDLStateEnum state, string text1, bool pathBelowTargetTVD)
	{
		m_minDLState = state;
		if (pathBelowTargetTVD)
		{
			text1 += " (Path goes below target)";
		}
		switch (state)
		{
		case MinDLStateEnum.NotCalculated:
			SetMinDLResultLabelTextColour(Color.Orange);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.TargetNotAchieved:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.Error:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.NormalDogleg:
			SetMinDLResultLabelTextColour(Color.Black);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.SevereDogleg:
			SetMinDLResultLabelTextColour(Utility.m_SevereColor);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.ExtremeDogleg:
			SetMinDLResultLabelTextColour(Utility.m_ExtremeColor);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		default:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		}
	}

	private void SetMinDLResultLabelTextColour(Color color)
	{
		label_BuildResult.ForeColor = color;
		m_drillHoleStatus.MinimumDLStateColor = color;
	}

	private void setMinDLResultLabelText(string text1)
	{
		label_BuildResult.Text = text1;
		m_drillHoleStatus.MinimumDLStateString = text1;
	}

	private void SetMinDLResultLabelVisible(bool label1)
	{
		label_BuildResult.Visible = label1;
	}

	private DoglegColourCodes GetDoglegColourCode(decimal dogLeg)
	{
		if (dogLeg >= m_extremeDL)
		{
			return DoglegColourCodes.Extreme;
		}
		if (dogLeg >= m_severeDL)
		{
			return DoglegColourCodes.Severe;
		}
		return DoglegColourCodes.Normal;
	}

	public void DisplayAdvancedDialog()
	{
		Popup_DrillHolePlanning_Advanced popup_DrillHolePlanning_Advanced = new Popup_DrillHolePlanning_Advanced();
		popup_DrillHolePlanning_Advanced.ShowDialog(m_drillHoleStatus.EndOfHoleSurvey, m_drillHoleStatus.SidetrackSurvey, m_drillHoleStatus.StraightPathSurvey, m_drillHoleStatus.SurveyAtTargetTVD);
	}

	public void ConstructReport()
	{
		if (GetUserInput(out var author, out var notes, out var surveyData, out var charts))
		{
			Popup_WorkingIndicator working = new Popup_WorkingIndicator("Generating Report", "Generating Report", showCancelButton: true, 5);
			AsyncConsOperationsDelegate asyncConsOperationsDelegate = AsyncConsOperations;
			asyncConsOperationsDelegate.BeginInvoke(surveyData, charts, author, notes, ref working, AsyncConsComplete, asyncConsOperationsDelegate);
			working.ShowDialog();
		}
	}

	private bool GetUserInput(out string author, out string notes, out bool surveyData, out bool charts)
	{
		Popup_Report_UserFields popup_Report_UserFields = new Popup_Report_UserFields();
		Popup_Report_Components popup_Report_Components = new Popup_Report_Components(surveyData: true, buildSurveys: false, extrapolatedSurveys: false, analysedSurveys: false, charts: true);
		Popup_Report_UserFields.UserFieldsDialogResult userFieldsDialogResult = null;
		Popup_Report_Components.ComponentsDialogResult componentsDialogResult = null;
		author = "";
		notes = "";
		surveyData = false;
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
				charts = componentsDialogResult.Charts;
				return true;
			}
		}
	}

	private EzyReport.Report AsyncConsOperations(bool surveyData, bool charts, string author, string notes, ref Popup_WorkingIndicator working)
	{
		EzyReport.Report report = new EzyReport.Report("Hole Status: " + m_drillHoleFile.HoleID, author);
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
			Bitmap perspective = null;
			Bitmap easting = null;
			Bitmap northing = null;
			Bitmap top = null;
			m_chart.GetImages(out perspective, out easting, out northing, out top);
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

	private void ResetTextBoxes()
	{
		label_CollarEasting.Text = "";
		label_CollarNorthing.Text = "";
		label_CollarEasting.BackColor = Color.DimGray;
		label_CollarNorthing.BackColor = Color.DimGray;
		label_TargetEasting.Text = "";
		label_TargetNorthing.Text = "";
		label_TargetTVD.Text = "";
		label_TargetEasting.BackColor = Color.DimGray;
		label_TargetNorthing.BackColor = Color.DimGray;
		label_TargetTVD.BackColor = Color.DimGray;
		label_Distance_East.Text = "";
		label_Distance_North.Text = "";
		label_Distance_TVD.Text = "";
		label_Distance_Horizontal.Text = "";
		label_Distance_East.BackColor = Color.DimGray;
		label_Distance_North.BackColor = Color.DimGray;
		label_Distance_TVD.BackColor = Color.DimGray;
		label_Distance_Horizontal.BackColor = Color.DimGray;
		label_Straight_East.Text = "";
		label_Straight_North.Text = "";
		label_Straight_Horizontal.Text = "";
		label_Straight_East.BackColor = Color.DimGray;
		label_Straight_North.BackColor = Color.DimGray;
		label_Straight_Horizontal.BackColor = Color.DimGray;
		label_HoleMD.Text = "";
		label_HoleTVD.Text = "";
		label_HoleTVD.BackColor = Color.DimGray;
		label_HoleMD.BackColor = Color.DimGray;
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
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.groupBox_Collar = new System.Windows.Forms.GroupBox();
		this.label_CollarNorthing = new System.Windows.Forms.Label();
		this.label_CollarEasting = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox_Target = new System.Windows.Forms.GroupBox();
		this.label_TargetTVD = new System.Windows.Forms.Label();
		this.label_TargetNorthing = new System.Windows.Forms.Label();
		this.label_TargetEasting = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox_build = new System.Windows.Forms.GroupBox();
		this.label_BuildResult = new System.Windows.Forms.Label();
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
		this.groupBox_Depth = new System.Windows.Forms.GroupBox();
		this.label_HoleTVD = new System.Windows.Forms.Label();
		this.label_HoleMD = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox_Sidetrack = new System.Windows.Forms.GroupBox();
		this.label_SidetrackTVD = new System.Windows.Forms.Label();
		this.label_SidetrackMD = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.groupBox_TargetTVDDistance = new System.Windows.Forms.GroupBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_HD = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_East = new System.Windows.Forms.Label();
		this.label_TargetTVDDistance_North = new System.Windows.Forms.Label();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.panel_ScrollPanel = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.textBox7 = new System.Windows.Forms.TextBox();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.textBox9 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label12 = new System.Windows.Forms.Label();
		this.panel_SevereDL = new System.Windows.Forms.Panel();
		this.label13 = new System.Windows.Forms.Label();
		this.panel_ExtremeDL = new System.Windows.Forms.Panel();
		base.panel_Body.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox_Collar.SuspendLayout();
		this.groupBox_Target.SuspendLayout();
		this.groupBox_build.SuspendLayout();
		this.groupBox_StraightPathDistance.SuspendLayout();
		this.groupBox_DistanceFromTarget.SuspendLayout();
		this.groupBox_Depth.SuspendLayout();
		this.groupBox_Sidetrack.SuspendLayout();
		this.groupBox_TargetTVDDistance.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.panel_ScrollPanel.SuspendLayout();
		base.SuspendLayout();
		base.panel_Body.Controls.Add(this.tabControl1);
		base.panel_Body.Location = new System.Drawing.Point(0, 76);
		base.panel_Body.Size = new System.Drawing.Size(293, 1034);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Controls.Add(this.panel_ExtremeDL);
		base.panel_Header.Controls.Add(this.label12);
		base.panel_Header.Controls.Add(this.panel_SevereDL);
		base.panel_Header.Controls.Add(this.label13);
		base.panel_Header.Size = new System.Drawing.Size(293, 76);
		base.panel_Header.Controls.SetChildIndex(this.label13, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel_SevereDL, 0);
		base.panel_Header.Controls.SetChildIndex(this.label12, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel_ExtremeDL, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Header.Controls.SetChildIndex(base.label_HeaderText1, 0);
		base.label_HeaderText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_HeaderText1.Size = new System.Drawing.Size(164, 24);
		base.label_HeaderText1.Text = "Hole Navigation:";
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(3, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(285, 1026);
		this.tabControl1.TabIndex = 1;
		this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
		this.tabPage2.AutoScroll = true;
		this.tabPage2.Controls.Add(this.groupBox_Collar);
		this.tabPage2.Controls.Add(this.groupBox_Target);
		this.tabPage2.Controls.Add(this.groupBox_build);
		this.tabPage2.Controls.Add(this.groupBox_StraightPathDistance);
		this.tabPage2.Controls.Add(this.groupBox_DistanceFromTarget);
		this.tabPage2.Controls.Add(this.groupBox_Depth);
		this.tabPage2.Controls.Add(this.groupBox_Sidetrack);
		this.tabPage2.Controls.Add(this.groupBox_TargetTVDDistance);
		this.tabPage2.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage2.Location = new System.Drawing.Point(4, 34);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(277, 988);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Hole Status";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.groupBox_Collar.Controls.Add(this.label_CollarNorthing);
		this.groupBox_Collar.Controls.Add(this.label_CollarEasting);
		this.groupBox_Collar.Controls.Add(this.label7);
		this.groupBox_Collar.Controls.Add(this.label8);
		this.groupBox_Collar.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Collar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Collar.Location = new System.Drawing.Point(3, 704);
		this.groupBox_Collar.Name = "groupBox_Collar";
		this.groupBox_Collar.Size = new System.Drawing.Size(271, 78);
		this.groupBox_Collar.TabIndex = 12;
		this.groupBox_Collar.TabStop = false;
		this.groupBox_Collar.Text = "Collar (m):";
		this.label_CollarNorthing.AutoSize = true;
		this.label_CollarNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarNorthing.Location = new System.Drawing.Point(89, 47);
		this.label_CollarNorthing.Name = "label_CollarNorthing";
		this.label_CollarNorthing.Size = new System.Drawing.Size(151, 20);
		this.label_CollarNorthing.TabIndex = 5;
		this.label_CollarNorthing.Text = "label_CollarNorthing";
		this.label_CollarNorthing.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_CollarEasting.AutoSize = true;
		this.label_CollarEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarEasting.Location = new System.Drawing.Point(89, 27);
		this.label_CollarEasting.Name = "label_CollarEasting";
		this.label_CollarEasting.Size = new System.Drawing.Size(145, 20);
		this.label_CollarEasting.TabIndex = 4;
		this.label_CollarEasting.Text = "label_CollarEasting";
		this.label_CollarEasting.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(8, 47);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(82, 20);
		this.label7.TabIndex = 3;
		this.label7.Text = "Northing:";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(8, 25);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(75, 20);
		this.label8.TabIndex = 3;
		this.label8.Text = "Easting:";
		this.groupBox_Target.Controls.Add(this.label_TargetTVD);
		this.groupBox_Target.Controls.Add(this.label_TargetNorthing);
		this.groupBox_Target.Controls.Add(this.label_TargetEasting);
		this.groupBox_Target.Controls.Add(this.label6);
		this.groupBox_Target.Controls.Add(this.label5);
		this.groupBox_Target.Controls.Add(this.label4);
		this.groupBox_Target.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Target.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Target.Location = new System.Drawing.Point(3, 611);
		this.groupBox_Target.Name = "groupBox_Target";
		this.groupBox_Target.Size = new System.Drawing.Size(271, 93);
		this.groupBox_Target.TabIndex = 11;
		this.groupBox_Target.TabStop = false;
		this.groupBox_Target.Text = "Target (m):";
		this.label_TargetTVD.AutoSize = true;
		this.label_TargetTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVD.Location = new System.Drawing.Point(89, 65);
		this.label_TargetTVD.Name = "label_TargetTVD";
		this.label_TargetTVD.Size = new System.Drawing.Size(129, 20);
		this.label_TargetTVD.TabIndex = 6;
		this.label_TargetTVD.Text = "label_TargetTVD";
		this.label_TargetTVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetNorthing.AutoSize = true;
		this.label_TargetNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetNorthing.Location = new System.Drawing.Point(89, 45);
		this.label_TargetNorthing.Name = "label_TargetNorthing";
		this.label_TargetNorthing.Size = new System.Drawing.Size(157, 20);
		this.label_TargetNorthing.TabIndex = 5;
		this.label_TargetNorthing.Text = "label_TargetNorthing";
		this.label_TargetNorthing.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetEasting.AutoSize = true;
		this.label_TargetEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetEasting.Location = new System.Drawing.Point(89, 25);
		this.label_TargetEasting.Name = "label_TargetEasting";
		this.label_TargetEasting.Size = new System.Drawing.Size(151, 20);
		this.label_TargetEasting.TabIndex = 4;
		this.label_TargetEasting.Text = "label_TargetEasting";
		this.label_TargetEasting.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(8, 65);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(49, 20);
		this.label6.TabIndex = 3;
		this.label6.Text = "TVD:";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(8, 45);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(82, 20);
		this.label5.TabIndex = 3;
		this.label5.Text = "Northing:";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(8, 25);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(75, 20);
		this.label4.TabIndex = 3;
		this.label4.Text = "Easting:";
		this.groupBox_build.Controls.Add(this.label_BuildResult);
		this.groupBox_build.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_build.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_build.Location = new System.Drawing.Point(3, 529);
		this.groupBox_build.Name = "groupBox_build";
		this.groupBox_build.Size = new System.Drawing.Size(271, 82);
		this.groupBox_build.TabIndex = 18;
		this.groupBox_build.TabStop = false;
		this.groupBox_build.Text = "Minimum dogleg to target (degrees/30m):";
		this.label_BuildResult.AutoSize = true;
		this.label_BuildResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_BuildResult.Location = new System.Drawing.Point(6, 55);
		this.label_BuildResult.Name = "label_BuildResult";
		this.label_BuildResult.Size = new System.Drawing.Size(132, 20);
		this.label_BuildResult.TabIndex = 3;
		this.label_BuildResult.Text = "label_BuildResult";
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_Horizontal);
		this.groupBox_StraightPathDistance.Controls.Add(this.label23);
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_East);
		this.groupBox_StraightPathDistance.Controls.Add(this.label_Straight_North);
		this.groupBox_StraightPathDistance.Controls.Add(this.label22);
		this.groupBox_StraightPathDistance.Controls.Add(this.label21);
		this.groupBox_StraightPathDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_StraightPathDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_StraightPathDistance.Location = new System.Drawing.Point(3, 412);
		this.groupBox_StraightPathDistance.Name = "groupBox_StraightPathDistance";
		this.groupBox_StraightPathDistance.Size = new System.Drawing.Size(271, 117);
		this.groupBox_StraightPathDistance.TabIndex = 13;
		this.groupBox_StraightPathDistance.TabStop = false;
		this.groupBox_StraightPathDistance.Text = "Straight path to target TVD distance from target (m):";
		this.label_Straight_Horizontal.AutoSize = true;
		this.label_Straight_Horizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_Horizontal.Location = new System.Drawing.Point(68, 92);
		this.label_Straight_Horizontal.Name = "label_Straight_Horizontal";
		this.label_Straight_Horizontal.Size = new System.Drawing.Size(188, 20);
		this.label_Straight_Horizontal.TabIndex = 7;
		this.label_Straight_Horizontal.Text = "label_Straight_Horizontal";
		this.label_Straight_Horizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(10, 92);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(40, 20);
		this.label23.TabIndex = 8;
		this.label23.Text = "HD:";
		this.label_Straight_East.AutoSize = true;
		this.label_Straight_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_East.Location = new System.Drawing.Point(68, 72);
		this.label_Straight_East.Name = "label_Straight_East";
		this.label_Straight_East.Size = new System.Drawing.Size(149, 20);
		this.label_Straight_East.TabIndex = 6;
		this.label_Straight_East.Text = "label_Straight_East";
		this.label_Straight_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_Straight_North.AutoSize = true;
		this.label_Straight_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Straight_North.Location = new System.Drawing.Point(68, 52);
		this.label_Straight_North.Name = "label_Straight_North";
		this.label_Straight_North.Size = new System.Drawing.Size(155, 20);
		this.label_Straight_North.TabIndex = 5;
		this.label_Straight_North.Text = "label_Straight_North";
		this.label_Straight_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(10, 72);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(51, 20);
		this.label22.TabIndex = 8;
		this.label22.Text = "East:";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(10, 52);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(58, 20);
		this.label21.TabIndex = 8;
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
		this.groupBox_DistanceFromTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_DistanceFromTarget.Location = new System.Drawing.Point(3, 271);
		this.groupBox_DistanceFromTarget.Name = "groupBox_DistanceFromTarget";
		this.groupBox_DistanceFromTarget.Size = new System.Drawing.Size(271, 141);
		this.groupBox_DistanceFromTarget.TabIndex = 14;
		this.groupBox_DistanceFromTarget.TabStop = false;
		this.groupBox_DistanceFromTarget.Text = "End of hole distance from target (m):";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(8, 115);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(49, 20);
		this.label20.TabIndex = 8;
		this.label20.Text = "TVD:";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(7, 95);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(40, 20);
		this.label19.TabIndex = 8;
		this.label19.Text = "HD:";
		this.label_Distance_TVD.AutoSize = true;
		this.label_Distance_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_TVD.Location = new System.Drawing.Point(68, 115);
		this.label_Distance_TVD.Name = "label_Distance_TVD";
		this.label_Distance_TVD.Size = new System.Drawing.Size(155, 20);
		this.label_Distance_TVD.TabIndex = 8;
		this.label_Distance_TVD.Text = "label_Distance_TVD";
		this.label_Distance_TVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(8, 75);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(51, 20);
		this.label18.TabIndex = 8;
		this.label18.Text = "East:";
		this.label_Distance_Horizontal.AutoSize = true;
		this.label_Distance_Horizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_Horizontal.Location = new System.Drawing.Point(68, 95);
		this.label_Distance_Horizontal.Name = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.Size = new System.Drawing.Size(195, 20);
		this.label_Distance_Horizontal.TabIndex = 7;
		this.label_Distance_Horizontal.Text = "label_Distance_Horizontal";
		this.label_Distance_Horizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(8, 55);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(58, 20);
		this.label17.TabIndex = 8;
		this.label17.Text = "North:";
		this.label_Distance_East.AutoSize = true;
		this.label_Distance_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_East.Location = new System.Drawing.Point(68, 75);
		this.label_Distance_East.Name = "label_Distance_East";
		this.label_Distance_East.Size = new System.Drawing.Size(156, 20);
		this.label_Distance_East.TabIndex = 6;
		this.label_Distance_East.Text = "label_Distance_East";
		this.label_Distance_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_Distance_North.AutoSize = true;
		this.label_Distance_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Distance_North.Location = new System.Drawing.Point(68, 55);
		this.label_Distance_North.Name = "label_Distance_North";
		this.label_Distance_North.Size = new System.Drawing.Size(162, 20);
		this.label_Distance_North.TabIndex = 5;
		this.label_Distance_North.Text = "label_Distance_North";
		this.label_Distance_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.groupBox_Depth.Controls.Add(this.label_HoleTVD);
		this.groupBox_Depth.Controls.Add(this.label_HoleMD);
		this.groupBox_Depth.Controls.Add(this.label1);
		this.groupBox_Depth.Controls.Add(this.label9);
		this.groupBox_Depth.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Depth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Depth.Location = new System.Drawing.Point(3, 197);
		this.groupBox_Depth.Name = "groupBox_Depth";
		this.groupBox_Depth.Size = new System.Drawing.Size(271, 74);
		this.groupBox_Depth.TabIndex = 15;
		this.groupBox_Depth.TabStop = false;
		this.groupBox_Depth.Text = "Drill hole depth (m):";
		this.label_HoleTVD.AutoSize = true;
		this.label_HoleTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleTVD.Location = new System.Drawing.Point(52, 45);
		this.label_HoleTVD.Name = "label_HoleTVD";
		this.label_HoleTVD.Size = new System.Drawing.Size(116, 20);
		this.label_HoleTVD.TabIndex = 5;
		this.label_HoleTVD.Text = "label_HoleTVD";
		this.label_HoleTVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_HoleMD.AutoSize = true;
		this.label_HoleMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleMD.Location = new System.Drawing.Point(52, 25);
		this.label_HoleMD.Name = "label_HoleMD";
		this.label_HoleMD.Size = new System.Drawing.Size(109, 20);
		this.label_HoleMD.TabIndex = 4;
		this.label_HoleMD.Text = "label_HoleMD";
		this.label_HoleMD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(6, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(49, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "TVD:";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(6, 25);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(41, 20);
		this.label9.TabIndex = 3;
		this.label9.Text = "MD:";
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackTVD);
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackMD);
		this.groupBox_Sidetrack.Controls.Add(this.label10);
		this.groupBox_Sidetrack.Controls.Add(this.label11);
		this.groupBox_Sidetrack.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Sidetrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Sidetrack.Location = new System.Drawing.Point(3, 123);
		this.groupBox_Sidetrack.Name = "groupBox_Sidetrack";
		this.groupBox_Sidetrack.Size = new System.Drawing.Size(271, 74);
		this.groupBox_Sidetrack.TabIndex = 16;
		this.groupBox_Sidetrack.TabStop = false;
		this.groupBox_Sidetrack.Text = "Sidetrack depth (m):";
		this.label_SidetrackTVD.AutoSize = true;
		this.label_SidetrackTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackTVD.Location = new System.Drawing.Point(54, 45);
		this.label_SidetrackTVD.Name = "label_SidetrackTVD";
		this.label_SidetrackTVD.Size = new System.Drawing.Size(150, 20);
		this.label_SidetrackTVD.TabIndex = 5;
		this.label_SidetrackTVD.Text = "label_SidetrackTVD";
		this.label_SidetrackTVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_SidetrackMD.AutoSize = true;
		this.label_SidetrackMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackMD.Location = new System.Drawing.Point(53, 25);
		this.label_SidetrackMD.Name = "label_SidetrackMD";
		this.label_SidetrackMD.Size = new System.Drawing.Size(143, 20);
		this.label_SidetrackMD.TabIndex = 4;
		this.label_SidetrackMD.Text = "label_SidetrackMD";
		this.label_SidetrackMD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(10, 45);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(49, 20);
		this.label10.TabIndex = 3;
		this.label10.Text = "TVD:";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(10, 25);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(41, 20);
		this.label11.TabIndex = 3;
		this.label11.Text = "MD:";
		this.groupBox_TargetTVDDistance.Controls.Add(this.label16);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label15);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label14);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_HD);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_East);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label_TargetTVDDistance_North);
		this.groupBox_TargetTVDDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_TargetTVDDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_TargetTVDDistance.Location = new System.Drawing.Point(3, 3);
		this.groupBox_TargetTVDDistance.Name = "groupBox_TargetTVDDistance";
		this.groupBox_TargetTVDDistance.Size = new System.Drawing.Size(271, 120);
		this.groupBox_TargetTVDDistance.TabIndex = 17;
		this.groupBox_TargetTVDDistance.TabStop = false;
		this.groupBox_TargetTVDDistance.Text = "Distance from target at target TVD (m):";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(8, 94);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(40, 20);
		this.label16.TabIndex = 8;
		this.label16.Text = "HD:";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(7, 74);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(51, 20);
		this.label15.TabIndex = 8;
		this.label15.Text = "East:";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(8, 54);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(58, 20);
		this.label14.TabIndex = 8;
		this.label14.Text = "North:";
		this.label_TargetTVDDistance_HD.AutoSize = true;
		this.label_TargetTVDDistance_HD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_HD.Location = new System.Drawing.Point(68, 94);
		this.label_TargetTVDDistance_HD.Name = "label_TargetTVDDistance_HD";
		this.label_TargetTVDDistance_HD.Size = new System.Drawing.Size(225, 20);
		this.label_TargetTVDDistance_HD.TabIndex = 7;
		this.label_TargetTVDDistance_HD.Text = "label_TargetTVDDistance_HD";
		this.label_TargetTVDDistance_HD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetTVDDistance_East.AutoSize = true;
		this.label_TargetTVDDistance_East.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_East.Location = new System.Drawing.Point(68, 74);
		this.label_TargetTVDDistance_East.Name = "label_TargetTVDDistance_East";
		this.label_TargetTVDDistance_East.Size = new System.Drawing.Size(234, 20);
		this.label_TargetTVDDistance_East.TabIndex = 6;
		this.label_TargetTVDDistance_East.Text = "label_TargetTVDDistance_East";
		this.label_TargetTVDDistance_East.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetTVDDistance_North.AutoSize = true;
		this.label_TargetTVDDistance_North.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDDistance_North.Location = new System.Drawing.Point(68, 54);
		this.label_TargetTVDDistance_North.Name = "label_TargetTVDDistance_North";
		this.label_TargetTVDDistance_North.Size = new System.Drawing.Size(240, 20);
		this.label_TargetTVDDistance_North.TabIndex = 5;
		this.label_TargetTVDDistance_North.Text = "label_TargetTVDDistance_North";
		this.label_TargetTVDDistance_North.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tabPage1.AutoScroll = true;
		this.tabPage1.Controls.Add(this.panel_ScrollPanel);
		this.tabPage1.Location = new System.Drawing.Point(4, 34);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(322, 527);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Chart Options";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.panel_ScrollPanel.AutoScroll = true;
		this.panel_ScrollPanel.AutoScrollMinSize = new System.Drawing.Size(0, 460);
		this.panel_ScrollPanel.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollPanel.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollPanel.Name = "panel_ScrollPanel";
		this.panel_ScrollPanel.Size = new System.Drawing.Size(316, 521);
		this.panel_ScrollPanel.TabIndex = 1;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Cursor = System.Windows.Forms.Cursors.Default;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(316, 521);
		this.ezyNavHoleChartController1.TabIndex = 0;
		this.textBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox5.Location = new System.Drawing.Point(15, 113);
		this.textBox5.Name = "textBox5";
		this.textBox5.ReadOnly = true;
		this.textBox5.Size = new System.Drawing.Size(275, 20);
		this.textBox5.TabIndex = 4;
		this.textBox5.TabStop = false;
		this.textBox6.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox6.Location = new System.Drawing.Point(15, 236);
		this.textBox6.Name = "textBox6";
		this.textBox6.ReadOnly = true;
		this.textBox6.Size = new System.Drawing.Size(275, 20);
		this.textBox6.TabIndex = 4;
		this.textBox6.TabStop = false;
		this.textBox7.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox7.Location = new System.Drawing.Point(15, 76);
		this.textBox7.Name = "textBox7";
		this.textBox7.ReadOnly = true;
		this.textBox7.Size = new System.Drawing.Size(275, 20);
		this.textBox7.TabIndex = 4;
		this.textBox7.TabStop = false;
		this.textBox8.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox8.Location = new System.Drawing.Point(15, 199);
		this.textBox8.Name = "textBox8";
		this.textBox8.ReadOnly = true;
		this.textBox8.Size = new System.Drawing.Size(275, 20);
		this.textBox8.TabIndex = 4;
		this.textBox8.TabStop = false;
		this.textBox9.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox9.Location = new System.Drawing.Point(15, 39);
		this.textBox9.Name = "textBox9";
		this.textBox9.ReadOnly = true;
		this.textBox9.Size = new System.Drawing.Size(275, 20);
		this.textBox9.TabIndex = 4;
		this.textBox9.TabStop = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(10, 171);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(113, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Straight path distance:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(10, 11);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(105, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "Distance from target:";
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Navigation;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(239, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(54, 76);
		this.panel1.TabIndex = 3;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(7, 30);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(117, 16);
		this.label12.TabIndex = 15;
		this.label12.Text = "Severe Dogleg:";
		this.panel_SevereDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_SevereDL.Location = new System.Drawing.Point(129, 30);
		this.panel_SevereDL.Name = "panel_SevereDL";
		this.panel_SevereDL.Size = new System.Drawing.Size(54, 20);
		this.panel_SevereDL.TabIndex = 16;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(7, 52);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(123, 16);
		this.label13.TabIndex = 17;
		this.label13.Text = "Extreme Dogleg:";
		this.panel_ExtremeDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_ExtremeDL.Location = new System.Drawing.Point(129, 52);
		this.panel_ExtremeDL.Name = "panel_ExtremeDL";
		this.panel_ExtremeDL.Size = new System.Drawing.Size(54, 20);
		this.panel_ExtremeDL.TabIndex = 18;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "Body_DrillHolePlanning";
		base.Size = new System.Drawing.Size(295, 1112);
		base.panel_Body.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.groupBox_Collar.ResumeLayout(false);
		this.groupBox_Collar.PerformLayout();
		this.groupBox_Target.ResumeLayout(false);
		this.groupBox_Target.PerformLayout();
		this.groupBox_build.ResumeLayout(false);
		this.groupBox_build.PerformLayout();
		this.groupBox_StraightPathDistance.ResumeLayout(false);
		this.groupBox_StraightPathDistance.PerformLayout();
		this.groupBox_DistanceFromTarget.ResumeLayout(false);
		this.groupBox_DistanceFromTarget.PerformLayout();
		this.groupBox_Depth.ResumeLayout(false);
		this.groupBox_Depth.PerformLayout();
		this.groupBox_Sidetrack.ResumeLayout(false);
		this.groupBox_Sidetrack.PerformLayout();
		this.groupBox_TargetTVDDistance.ResumeLayout(false);
		this.groupBox_TargetTVDDistance.PerformLayout();
		this.tabPage1.ResumeLayout(false);
		this.panel_ScrollPanel.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
