using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_HolePlanning : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			DrillHoleManagement,
			Close,
			Build,
			Extrapolate,
			TrendAnalysis,
			SideTrackHole
		}

		private FinishState m_state;

		public FinishState State
		{
			get
			{
				return m_state;
			}
			set
			{
				m_state = value;
			}
		}
	}

	private Panel m_panelBody;

	private Panel m_panelToolbar;

	private Panel m_panelInfo;

	private Panel m_panelFill;

	private EzyNavHoleChart m_ezyNavChart;

	private Label m_labelHeading;

	private Body_DrillHolePlanning m_controlInfo;

	private Toolbar_DrillHolePlanning m_controlToolbar;

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	public event EventHandler Finished;

	public Procedure_HolePlanning(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile, ref DrillHoleStatus drillHoleStatus)
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_drillHoleStatus = drillHoleStatus;
		m_extremeDL = Settings.Default.extremeDL;
		m_severeDL = Settings.Default.severeDL;
		m_controlInfo = new Body_DrillHolePlanning(ref m_ezyNavChart, ref m_drillHoleFile, ref drillHoleStatus, m_severeDL, m_extremeDL);
		m_controlToolbar = new Toolbar_DrillHolePlanning();
		m_controlToolbar.ButtonPressed += m_toolbarControl_ButtonPressed;
	}

	public void DriveArrived(string driveLetter)
	{
		m_controlInfo.DriveArrived(driveLetter);
	}

	public void DriveRemoved()
	{
		m_controlInfo.DriveRemoved();
	}

	private void OnFinished(FinishedEventArgs.FinishState state)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.State = state;
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	public void Start()
	{
		m_labelHeading.Text = "Hole Navigation";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_panelInfo.Controls.Add(m_controlInfo);
		m_panelToolbar.Controls.Add(m_controlToolbar);
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_controlToolbar.Dock = DockStyle.Fill;
		m_controlInfo.Dock = DockStyle.Fill;
		m_ezyNavChart.Dock = DockStyle.Fill;
		NavMath navMath = new NavMath();
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		m_drillHoleStatus.AbsoluteSurveyList = absoluteSurveyList;
		m_controlInfo.Display();
		m_controlInfo.Focus();
	}

	private void DisplayAdvancedDialog()
	{
		m_controlInfo.DisplayAdvancedDialog();
	}

	private void DisplayReportDialog()
	{
		m_controlInfo.ConstructReport();
	}

	private void m_toolbarControl_ButtonPressed(object sender, EventArgs e)
	{
		Toolbar_DrillHolePlanning.ButtonPressedEventArgs e2 = (Toolbar_DrillHolePlanning.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.DrillHoleManagement:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.DrillHoleManagement);
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.Close:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.Close);
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.Build:
			if (AboveTarget(m_drillHoleFile.Target, m_drillHoleStatus.AbsoluteSurveyList))
			{
				m_controlInfo.RemoveFromDisplay();
				OnFinished(FinishedEventArgs.FinishState.Build);
			}
			else
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Cannot perform build to target!", "A build to target cannot be performed when the hole is at or below the target TVD.");
				popup_MessageBox_OK.ShowDialog();
			}
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.Extrapolate:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.Extrapolate);
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.TrendAnalysis:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.TrendAnalysis);
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.SideTrackHole:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.SideTrackHole);
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.Advanced:
			DisplayAdvancedDialog();
			break;
		case Toolbar_DrillHolePlanning.ButtonPressedEventArgs.ButtonPressedEnum.Report:
			DisplayReportDialog();
			break;
		}
	}

	private bool AboveTarget(Target target, AbsoluteSurveyList absoluteSurveyList)
	{
		if (absoluteSurveyList.Count() == 0)
		{
			return true;
		}
		if (absoluteSurveyList.LastRecord().TVD < target.TVD)
		{
			return true;
		}
		return false;
	}

	private void ConfigureLayout()
	{
		ShowToolbarPanel();
		ShowInfoPanel();
		ShowFillPanel();
		m_ezyNavChart.Visible = true;
	}

	private void ShowInfoPanel()
	{
		m_panelInfo.Width = m_controlInfo.DesignTimeWidth;
		m_panelInfo.Visible = true;
	}

	private void ShowToolbarPanel()
	{
		m_panelToolbar.Width = m_controlToolbar.DesignTimeWidth;
		m_panelToolbar.Visible = true;
	}

	private void ShowFillPanel()
	{
		m_panelFill.Visible = true;
	}

	public void Resize()
	{
	}
}
