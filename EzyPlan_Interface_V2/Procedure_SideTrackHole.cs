using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_SideTrackHole : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Saved,
			Canceled
		}

		private FinishState m_state;

		private string m_filePath;

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

		public string FilePath
		{
			get
			{
				return m_filePath;
			}
			set
			{
				m_filePath = value;
			}
		}
	}

	private Panel m_panelBody;

	private Panel m_panelToolbar;

	private Panel m_panelInfo;

	private Panel m_panelFill;

	private EzyNavHoleChart m_ezyNavChart;

	private Label m_labelHeading;

	private Body_SideTrackHole m_createSideTrack;

	private Body_CreateNewHole_SaveFile m_saveFile;

	private DrillHoleFile m_drillHoleFile;

	private decimal m_extremeDL;

	private decimal m_severeDL;

	public event EventHandler Finished;

	public Procedure_SideTrackHole(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
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
		m_createSideTrack = new Body_SideTrackHole(drillHoleFile, ezyNavChart, m_severeDL, m_extremeDL);
		m_saveFile = new Body_CreateNewHole_SaveFile(ref drillHoleFile);
	}

	public void DriveArrived(string driveLetter)
	{
		m_saveFile.DriveChange(driveLetter);
	}

	public void DriveRemoved()
	{
		m_saveFile.DriveChange();
	}

	public void Start()
	{
		m_labelHeading.Text = "Sidetrack Hole";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_ezyNavChart.Dock = DockStyle.Fill;
		m_createSideTrack.Visible = false;
		m_panelInfo.Controls.Add(m_createSideTrack);
		m_createSideTrack.Dock = DockStyle.Fill;
		m_saveFile.Visible = false;
		m_panelInfo.Controls.Add(m_saveFile);
		m_saveFile.Dock = DockStyle.Fill;
		m_ezyNavChart.ResetChart();
		DisplayCreateSideTrack(refreshData: true);
	}

	public void DisplayCreateSideTrack(bool refreshData)
	{
		HideToolbarPanel();
		ShowInfoPanel();
		m_panelInfo.Width = m_createSideTrack.DesignTimeWidth;
		m_panelFill.Controls.Add(m_ezyNavChart);
		m_createSideTrack.Display(refreshData);
		m_createSideTrack.ButtonPressed += m_createSideTrack_ButtonPressed;
	}

	private void m_createSideTrack_ButtonPressed(object sender, EventArgs e)
	{
		m_createSideTrack.Visible = false;
		m_createSideTrack.RemoveFromDisplay();
		m_createSideTrack.ButtonPressed -= m_createSideTrack_ButtonPressed;
		Body_SideTrackHole.ButtonPressedEventArgs e2 = (Body_SideTrackHole.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_SideTrackHole.ButtonPressedEventArgs.ButtonPressedEnum.Next:
			Display_SaveFile(e2.NewDrillHoleFile);
			break;
		case Body_SideTrackHole.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Canceled);
			break;
		}
	}

	public void Display_SaveFile(DrillHoleFile newDrillHole)
	{
		HideInfoPanel();
		HideToolbarPanel();
		ShowFillPanel();
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_saveFile);
		m_saveFile.Dock = DockStyle.Fill;
		m_saveFile.Display(newDrillHole);
		m_saveFile.Finished += SaveFile_Events;
	}

	private void SaveFile_Events(object sender, EventArgs e)
	{
		m_saveFile.Visible = false;
		m_saveFile.Finished -= SaveFile_Events;
		Body_CreateNewHole_SaveFile.FinishedEventArgs e2 = (Body_CreateNewHole_SaveFile.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Back:
			DisplayCreateSideTrack(refreshData: false);
			break;
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Save:
			OnFinished(FinishedEventArgs.FinishState.Saved, e2.FilePath);
			break;
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Canceled);
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

	private void OnFinished(FinishedEventArgs.FinishState state, string filePath)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.State = state;
		e.FilePath = filePath;
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
		m_panelToolbar.Visible = false;
	}

	private void ShowFillPanel()
	{
		m_panelFill.Visible = true;
	}

	public void Resize()
	{
	}

	private void HideInfoPanel()
	{
		m_panelInfo.Visible = false;
	}
}
