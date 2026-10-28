using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_CreateNewHole : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Created,
			Cancel
		}

		private FinishedStateEnum m_state;

		private string m_filePath;

		public FinishedStateEnum FinishedState
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

	private Body_CreateNewHole_SaveFile m_controlSaveFile;

	private Body_CreateNewHole_EnterDetails m_controlEnterDetails1;

	private Body_CreateNewHole_EnterDetails2 m_controlEnterDetails2;

	private DrillHoleFile m_drillHoleFile;

	private bool m_enterDetails2FirstRun;

	public event EventHandler Finished;

	public void Start()
	{
		m_labelHeading.Text = "Create New Hole";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_enterDetails2FirstRun = true;
		Display_EnterDetails1(new DrillHoleFile());
	}

	public Procedure_CreateNewHole()
	{
	}

	public Procedure_CreateNewHole(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
		: this()
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_controlEnterDetails1 = new Body_CreateNewHole_EnterDetails();
		m_controlEnterDetails2 = new Body_CreateNewHole_EnterDetails2();
		m_controlSaveFile = new Body_CreateNewHole_SaveFile(ref drillHoleFile);
	}

	public void DriveArrived(string driveLetter)
	{
		m_controlSaveFile.DriveChange(driveLetter);
	}

	public void DriveRemoved()
	{
		m_controlSaveFile.DriveChange();
	}

	private void OnFinished(FinishedEventArgs.FinishedStateEnum state)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = state;
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void OnFinished(FinishedEventArgs.FinishedStateEnum state, string filePath)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = state;
		e.FilePath = filePath;
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void Display_EnterDetails1(DrillHoleFile drillHoleFile)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEnterDetails1);
		m_controlEnterDetails1.Dock = DockStyle.Fill;
		m_controlEnterDetails1.Display(drillHoleFile);
		m_controlEnterDetails1.Finished += m_controlEnterDetails1_Finished;
	}

	private void m_controlEnterDetails1_Finished(object sender, EventArgs e)
	{
		m_controlEnterDetails1.Finished -= m_controlEnterDetails1_Finished;
		Body_CreateNewHole_EnterDetails.FinishedEventArgs e2 = (Body_CreateNewHole_EnterDetails.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_EnterDetails.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishedStateEnum.Cancel);
			break;
		case Body_CreateNewHole_EnterDetails.FinishedEventArgs.FinishedStateEnum.Next:
			if (m_enterDetails2FirstRun)
			{
				Display_EnterDetails2(e2.DrillHoleFile, refreshFields: true);
				m_enterDetails2FirstRun = false;
			}
			else
			{
				Display_EnterDetails2(e2.DrillHoleFile, refreshFields: false);
			}
			break;
		}
	}

	private void Display_EnterDetails2(DrillHoleFile drillHoleFile, bool refreshFields)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEnterDetails2);
		m_controlEnterDetails2.Dock = DockStyle.Fill;
		m_controlEnterDetails2.Display(drillHoleFile, editMode: false, refreshFields);
		m_controlEnterDetails2.Finished += m_controlEnterDetails2_Finished;
	}

	private void m_controlEnterDetails2_Finished(object sender, EventArgs e)
	{
		m_controlEnterDetails2.Finished -= m_controlEnterDetails2_Finished;
		Body_CreateNewHole_EnterDetails2.FinishedEventArgs e2 = (Body_CreateNewHole_EnterDetails2.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishedStateEnum.Cancel);
			break;
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Back:
			Display_EnterDetails1(e2.DrillHoleFile);
			break;
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Next:
			Display_SaveFile(e2.DrillHoleFile);
			break;
		}
	}

	public void Display_SaveFile(DrillHoleFile newDrillHole)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlSaveFile);
		m_controlSaveFile.Dock = DockStyle.Fill;
		m_controlSaveFile.Display(newDrillHole);
		m_controlSaveFile.Finished += Display_SaveFile_Events;
	}

	private void Display_SaveFile_Events(object sender, EventArgs e)
	{
		m_controlSaveFile.Finished -= Display_SaveFile_Events;
		Body_CreateNewHole_SaveFile.FinishedEventArgs e2 = (Body_CreateNewHole_SaveFile.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Save:
			OnFinished(FinishedEventArgs.FinishedStateEnum.Created, e2.FilePath);
			break;
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishedStateEnum.Cancel);
			break;
		case Body_CreateNewHole_SaveFile.FinishedEventArgs.FinishedStateEnum.Back:
			Display_EnterDetails2(e2.DrillHoleFile, refreshFields: true);
			break;
		}
	}

	private void ConfigureLayout()
	{
		HideToolbarPanel();
		HideInfoPanel();
		ShowFillPanel();
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
}
