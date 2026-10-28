using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_BuildToTarget : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Finished
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

	private Body_BuildToTarget_Select m_buildSelect;

	private Body_BuildToTarget_Results m_buildResults;

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private decimal m_extremeDL;

	private decimal m_severeDL;

	public event EventHandler Finished;

	public Procedure_BuildToTarget(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile, ref DrillHoleStatus drillHoleStatus)
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
		m_buildSelect = new Body_BuildToTarget_Select(m_drillHoleFile, ezyNavChart, m_severeDL, m_extremeDL);
		m_buildResults = new Body_BuildToTarget_Results(ezyNavChart, m_severeDL, m_extremeDL);
	}

	public void DriveArrived(string driveLetter)
	{
		m_buildResults.DriveArrived(driveLetter);
	}

	public void DriveRemoved()
	{
		m_buildResults.DriveRemoved();
	}

	public void Start()
	{
		m_labelHeading.Text = "Build To Target";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_ezyNavChart.ResetChart();
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_ezyNavChart.Dock = DockStyle.Fill;
		DisplayBuildSelect();
	}

	public void DisplayBuildSelect()
	{
		m_panelInfo.Controls.Clear();
		m_panelInfo.Width = m_buildSelect.DesignTimeWidth;
		m_panelInfo.Controls.Add(m_buildSelect);
		m_buildSelect.Dock = DockStyle.Fill;
		m_buildSelect.ButtonPressed += m_buildSelect_ButtonPressed;
		m_buildSelect.Display();
	}

	private void m_buildSelect_ButtonPressed(object sender, EventArgs e)
	{
		m_buildSelect.RemoveFromDisplay();
		m_buildSelect.ButtonPressed -= m_buildSelect_ButtonPressed;
		Body_BuildToTarget_Select.ButtonPressedEventArgs e2 = (Body_BuildToTarget_Select.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_BuildToTarget_Select.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Finished);
			break;
		case Body_BuildToTarget_Select.ButtonPressedEventArgs.ButtonPressedEnum.Next:
			DisplayBuildResults(e2.BuildSelected, e2.LastSurvey);
			break;
		}
	}

	public void DisplayBuildResults(BuildAndHoldResults buildSelected, AbsoluteSurvey lastSurvey)
	{
		m_panelInfo.Controls.Clear();
		m_panelInfo.Width = m_buildResults.DesignTimeWidth;
		m_panelInfo.Controls.Add(m_buildResults);
		m_buildResults.Dock = DockStyle.Fill;
		m_buildResults.ButtonPressed += m_buildResults_ButtonPressed;
		m_buildResults.Display(buildSelected, lastSurvey, m_drillHoleFile, m_drillHoleStatus);
	}

	private void m_buildResults_ButtonPressed(object sender, EventArgs e)
	{
		m_buildResults.RemoveFromDisplay();
		m_buildResults.ButtonPressed -= m_buildResults_ButtonPressed;
		Body_BuildToTarget_Results.ButtonPressedEventArgs e2 = (Body_BuildToTarget_Results.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_BuildToTarget_Results.ButtonPressedEventArgs.ButtonPressedEnum.Back:
			DisplayBuildSelect();
			break;
		case Body_BuildToTarget_Results.ButtonPressedEventArgs.ButtonPressedEnum.Finish:
			OnFinished(FinishedEventArgs.FinishState.Finished);
			break;
		}
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

	private void ConfigureLayout()
	{
		HideToolbarPanel();
		ShowInfoPanel();
		ShowFillPanel();
		m_ezyNavChart.Visible = true;
	}

	private void ShowInfoPanel()
	{
		m_panelInfo.Visible = true;
	}

	private void HideToolbarPanel()
	{
		m_panelToolbar.Width = 0;
		m_panelToolbar.Visible = false;
	}

	private void ShowFillPanel()
	{
		m_panelFill.Visible = true;
	}

	public void Resize()
	{
	}
}
