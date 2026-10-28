using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_EditDetails : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Update,
			UpdateError,
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

	private Body_CreateNewHole_EnterDetails m_controlEditDetails1;

	private Body_CreateNewHole_EnterDetails2 m_controlEditDetails2;

	private DrillHoleFile m_drillHoleFile;

	private bool m_enterDetails2FirstRun;

	public event EventHandler Finished;

	public void Start()
	{
		m_labelHeading.Text = "Edit Hole Details";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_enterDetails2FirstRun = true;
		DrillHoleFile drillHoleFile = new DrillHoleFile(m_drillHoleFile, copyFileStream: false);
		Display_EditDetails1(drillHoleFile);
	}

	public Procedure_EditDetails()
	{
	}

	public Procedure_EditDetails(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
		: this()
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_controlEditDetails1 = new Body_CreateNewHole_EnterDetails();
		m_controlEditDetails2 = new Body_CreateNewHole_EnterDetails2();
	}

	public void DriveArrived(string driveLetter)
	{
	}

	public void DriveRemoved()
	{
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

	private void Display_EditDetails1(DrillHoleFile drillHoleFile)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEditDetails1);
		m_controlEditDetails1.Dock = DockStyle.Fill;
		m_controlEditDetails1.Display(drillHoleFile);
		m_controlEditDetails1.Finished += m_controlEditDetails1_Finished;
	}

	private void m_controlEditDetails1_Finished(object sender, EventArgs e)
	{
		m_controlEditDetails1.Finished -= m_controlEditDetails1_Finished;
		Body_CreateNewHole_EnterDetails.FinishedEventArgs e2 = (Body_CreateNewHole_EnterDetails.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_EnterDetails.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		case Body_CreateNewHole_EnterDetails.FinishedEventArgs.FinishedStateEnum.Next:
			if (m_enterDetails2FirstRun)
			{
				Display_EditDetails2(e2.DrillHoleFile, refreshFields: true);
				m_enterDetails2FirstRun = false;
			}
			else
			{
				Display_EditDetails2(e2.DrillHoleFile, refreshFields: false);
			}
			break;
		}
	}

	private void Display_EditDetails2(DrillHoleFile drillHoleFile, bool refreshFields)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEditDetails2);
		m_controlEditDetails2.Dock = DockStyle.Fill;
		m_controlEditDetails2.Display(drillHoleFile, editMode: true, refreshFields);
		m_controlEditDetails2.Finished += m_controlEditDetails2_Finished;
	}

	private void m_controlEditDetails2_Finished(object sender, EventArgs e)
	{
		m_controlEditDetails2.Finished -= m_controlEditDetails2_Finished;
		Body_CreateNewHole_EnterDetails2.FinishedEventArgs e2 = (Body_CreateNewHole_EnterDetails2.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Next:
			if (SaveChanges(e2.DrillHoleFile))
			{
				OnFinished(FinishedEventArgs.FinishState.Update);
			}
			else
			{
				OnFinished(FinishedEventArgs.FinishState.UpdateError);
			}
			break;
		case Body_CreateNewHole_EnterDetails2.FinishedEventArgs.FinishedStateEnum.Back:
			Display_EditDetails1(e2.DrillHoleFile);
			break;
		}
	}

	private bool SaveChanges(DrillHoleFile updated)
	{
		if (!m_drillHoleFile.Equals(updated))
		{
			m_drillHoleFile.Copy(updated, copyStream: false);
			DrillHoleFile.Save_ErrorsEnum save_ErrorsEnum = m_drillHoleFile.Save();
			if (save_ErrorsEnum == DrillHoleFile.Save_ErrorsEnum.NoError)
			{
				return true;
			}
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error saving changes!", DrillHoleFile.ExplainError(save_ErrorsEnum) + ".");
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
		return true;
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
