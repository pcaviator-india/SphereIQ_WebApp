using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_HoleManagement : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Home,
			EditDetails,
			HolePlanning,
			SideTrackHole,
			EditSurveyData,
			ImportSurveyData
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

	private Body_DrillHoleManagement m_controlInfo;

	private Toolbar_DrillHoleManagement m_controlToolbar;

	private DrillHoleFile m_drillHoleFile;

	public event EventHandler Finished;

	private void OnFinished(FinishedEventArgs.FinishState state)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.State = state;
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	public Procedure_HoleManagement(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_controlInfo = new Body_DrillHoleManagement(ref drillHoleFile, ref m_ezyNavChart);
		m_controlToolbar = new Toolbar_DrillHoleManagement();
		m_controlToolbar.ButtonPressed += m_toolbarControl_ButtonPressed;
	}

	public void Start()
	{
		m_labelHeading.Text = "Hole Management";
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
		m_controlInfo.Display();
		m_controlInfo.Focus();
	}

	public void DriveArrived(string driveLetter)
	{
	}

	public void DriveRemoved()
	{
	}

	private void m_toolbarControl_ButtonPressed(object sender, EventArgs e)
	{
		Toolbar_DrillHoleManagement.ButtonPressedEventArgs e2 = (Toolbar_DrillHoleManagement.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Toolbar_DrillHoleManagement.ButtonPressedEventArgs.ButtonPressedEnum.Home:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.Home);
			break;
		case Toolbar_DrillHoleManagement.ButtonPressedEventArgs.ButtonPressedEnum.EditDetails:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.EditDetails);
			break;
		case Toolbar_DrillHoleManagement.ButtonPressedEventArgs.ButtonPressedEnum.HolePlanning:
			if (m_drillHoleFile.SurveyList.Count() <= 0)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No Survey Data!", "Survey data must be entered before you can progress to \"Hole Navigation\"! \nClick on \"Edit Survey Data\" to enter survey data.");
				popup_MessageBox_OK.ShowDialog();
			}
			else
			{
				m_controlInfo.RemoveFromDisplay();
				OnFinished(FinishedEventArgs.FinishState.HolePlanning);
			}
			break;
		case Toolbar_DrillHoleManagement.ButtonPressedEventArgs.ButtonPressedEnum.EditSurveyData:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.EditSurveyData);
			break;
		case Toolbar_DrillHoleManagement.ButtonPressedEventArgs.ButtonPressedEnum.ImportSurveyData:
			m_controlInfo.RemoveFromDisplay();
			OnFinished(FinishedEventArgs.FinishState.ImportSurveyData);
			break;
		}
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
