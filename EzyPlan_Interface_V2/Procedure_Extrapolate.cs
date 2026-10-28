using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlanData;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_Extrapolate : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Finished,
			Cancel
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

	private Body_Extrapolate_Entry m_controlExtrapolateEntry;

	private Body_Extrapolate_Results m_controlExtrapolateResults;

	private DrillHoleFile m_drillHoleFile;

	public event EventHandler Finished;

	public Procedure_Extrapolate(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile, ref DrillHoleStatus drillHoleStatus)
	{
		m_drillHoleFile = drillHoleFile;
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_controlExtrapolateEntry = new Body_Extrapolate_Entry(ref drillHoleFile);
		m_controlExtrapolateResults = new Body_Extrapolate_Results(m_drillHoleFile, drillHoleStatus, m_ezyNavChart);
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

	public void DriveArrived(string driveLetter)
	{
		m_controlExtrapolateResults.DriveArrived(driveLetter);
	}

	public void DriveRemoved()
	{
		m_controlExtrapolateResults.DriveRemoved();
	}

	public void Start()
	{
		m_labelHeading.Text = "Extrapolate Survey Data";
		DisplayEntry(reset: true);
	}

	public void DisplayEntry(bool reset)
	{
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		EntryLayout();
		m_panelFill.Controls.Add(m_controlExtrapolateEntry);
		m_controlExtrapolateEntry.Dock = DockStyle.Fill;
		m_controlExtrapolateEntry.Display(reset);
		m_controlExtrapolateEntry.ButtonPressed += m_controlExtrapolateEntry_ButtonPressed;
	}

	private void m_controlExtrapolateEntry_ButtonPressed(object sender, EventArgs e)
	{
		m_controlExtrapolateEntry.ButtonPressed -= m_controlExtrapolateEntry_ButtonPressed;
		m_controlExtrapolateEntry.RemoveFromDisplay();
		Body_Extrapolate_Entry.ButtonPressedEventArgs e2 = (Body_Extrapolate_Entry.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_Extrapolate_Entry.ButtonPressedEventArgs.ButtonPressedEnum.Next:
			DisplayResults(e2.Extrapolated);
			break;
		case Body_Extrapolate_Entry.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		default:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		}
	}

	public void DisplayResults(ExtrapolatedSurveyList extrapolatedList)
	{
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ResultsLayout();
		m_panelInfo.Controls.Add(m_controlExtrapolateResults);
		m_controlExtrapolateResults.Dock = DockStyle.Fill;
		m_controlExtrapolateResults.ButtonPressed += m_controlExtrapolateResults_ButtonPressed;
		m_controlExtrapolateResults.Display(extrapolatedList);
	}

	private void m_controlExtrapolateResults_ButtonPressed(object sender, EventArgs e)
	{
		m_controlExtrapolateResults.RemoveFromDisplay();
		m_controlExtrapolateResults.ButtonPressed -= m_controlExtrapolateResults_ButtonPressed;
		Body_Extrapolate_Results.ButtonPressedEventArgs e2 = (Body_Extrapolate_Results.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_Extrapolate_Results.ButtonPressedEventArgs.ButtonPressedEnum.Finish:
			OnFinished(FinishedEventArgs.FinishState.Finished);
			break;
		case Body_Extrapolate_Results.ButtonPressedEventArgs.ButtonPressedEnum.Back:
			DisplayEntry(reset: false);
			break;
		}
	}

	private void EntryLayout()
	{
		HideToolbarPanel();
		HideInfoPanel();
		ShowFillPanel();
	}

	private void ResultsLayout()
	{
		HideToolbarPanel();
		ShowInfoPanel(m_controlExtrapolateResults.DesignTimeWidth);
		ShowFillPanel();
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_ezyNavChart.Visible = true;
	}

	private void HideInfoPanel()
	{
		m_panelInfo.Visible = false;
	}

	private void HideToolbarPanel()
	{
		m_panelToolbar.Visible = false;
	}

	private void ShowFillPanel()
	{
		m_panelFill.Visible = true;
	}

	public void Resize()
	{
	}

	private void ShowInfoPanel(int width)
	{
		m_panelInfo.Width = width;
		m_panelInfo.Visible = true;
	}
}
