using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_TrendAnalysis : IEzyPlanProcedure
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

	private Body_TrendAnalysis_SelectRange m_rangeSelect;

	private Body_TrendAnalysis_Results m_analysisResults;

	private DrillHoleFile m_drillHoleFile;

	private decimal m_extremeDL;

	private decimal m_severeDL;

	public event EventHandler Finished;

	public Procedure_TrendAnalysis(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile, ref DrillHoleStatus drillHoleStatus)
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_extremeDL = Settings.Default.extremeDL;
		m_severeDL = Settings.Default.severeDL;
		m_rangeSelect = new Body_TrendAnalysis_SelectRange(m_drillHoleFile, ezyNavChart, m_severeDL, m_extremeDL);
		m_analysisResults = new Body_TrendAnalysis_Results(drillHoleFile, drillHoleStatus, ezyNavChart, m_severeDL, m_extremeDL);
	}

	public void DriveArrived(string driveLetter)
	{
		m_analysisResults.DriveArrived(driveLetter);
	}

	public void DriveRemoved()
	{
		m_analysisResults.DriveRemoved();
	}

	public void Start()
	{
		m_labelHeading.Text = "Trend Analysis";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_ezyNavChart.Dock = DockStyle.Fill;
		m_panelInfo.Controls.Add(m_rangeSelect);
		m_rangeSelect.Dock = DockStyle.Fill;
		m_rangeSelect.Visible = false;
		m_panelInfo.Controls.Add(m_analysisResults);
		m_analysisResults.Dock = DockStyle.Fill;
		m_rangeSelect.Visible = false;
		m_ezyNavChart.ResetChart();
		DisplayRangeSelect(refreshDataGrid: true, resetExtrapolated: true);
	}

	public void DisplayRangeSelect(bool refreshDataGrid, bool resetExtrapolated)
	{
		m_panelInfo.Width = m_rangeSelect.DesignTimeWidth;
		m_rangeSelect.ButtonPressed += m_rangeSelect_ButtonPressed;
		m_rangeSelect.Display(refreshDataGrid, resetExtrapolated);
	}

	private void m_rangeSelect_ButtonPressed(object sender, EventArgs e)
	{
		m_rangeSelect.RemoveFromDisplay();
		m_rangeSelect.ButtonPressed -= m_rangeSelect_ButtonPressed;
		Body_TrendAnalysis_SelectRange.ButtonPressedEventArgs e2 = (Body_TrendAnalysis_SelectRange.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_TrendAnalysis_SelectRange.ButtonPressedEventArgs.ButtonPressedEnum.Select:
			DisplayAnalysisResults(e2.TrendAnalysisResults, e2.AnalysedSurveys);
			break;
		case Body_TrendAnalysis_SelectRange.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Finished);
			break;
		}
	}

	public void DisplayAnalysisResults(TrendAnalysisResults analysisResults, SortableBindingList<AbsoluteSurvey> analysedSurveys)
	{
		m_panelInfo.Width = m_analysisResults.DesignTimeWidth;
		m_analysisResults.ButtonPressed += m_analysisResults_ButtonPressed;
		m_analysisResults.Display(analysisResults, m_drillHoleFile.Target, analysedSurveys);
	}

	private void m_analysisResults_ButtonPressed(object sender, EventArgs e)
	{
		m_analysisResults.RemoveFromDisplay();
		m_analysisResults.ButtonPressed -= m_analysisResults_ButtonPressed;
		Body_TrendAnalysis_Results.ButtonPressedEventArgs e2 = (Body_TrendAnalysis_Results.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_TrendAnalysis_Results.ButtonPressedEventArgs.ButtonPressedEnum.Back:
			DisplayRangeSelect(refreshDataGrid: false, resetExtrapolated: true);
			break;
		case Body_TrendAnalysis_Results.ButtonPressedEventArgs.ButtonPressedEnum.Finish:
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
