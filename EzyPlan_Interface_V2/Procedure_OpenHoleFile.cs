using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_OpenHoleFile : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Open,
			Cancel
		}

		private FinishState m_state;

		private string m_fileName;

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

		public string FileName
		{
			get
			{
				return m_fileName;
			}
			set
			{
				m_fileName = value;
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

	private Body_OpenHoleFile m_controlOpenFile;

	private DrillHoleFile m_drillHoleFile;

	public event EventHandler Finished;

	public Procedure_OpenHoleFile(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
		m_controlOpenFile = new Body_OpenHoleFile(ref drillHoleFile);
		m_controlOpenFile.ButtonPressed += m_infoControl_ButtonPressed;
	}

	public void DriveArrived(string driveLetter)
	{
		m_controlOpenFile.DriveChange(driveLetter);
	}

	public void DriveRemoved()
	{
		m_controlOpenFile.DriveChange();
	}

	private void OnFinished(FinishedEventArgs.FinishState state, string fileName, string filePath)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.State = state;
		e.FileName = fileName;
		e.FilePath = filePath;
		if (Finished != null)
		{
			Finished(this, e);
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

	public void Start()
	{
		m_labelHeading.Text = "Open Existing Hole";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_panelFill.Controls.Add(m_controlOpenFile);
		m_controlOpenFile.Dock = DockStyle.Fill;
		m_controlOpenFile.Display();
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

	private void m_infoControl_ButtonPressed(object sender, EventArgs e)
	{
		Body_OpenHoleFile.ButtonPressedEventArgs e2 = (Body_OpenHoleFile.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_OpenHoleFile.ButtonPressedEventArgs.ButtonPressedEnum.Open:
			OnFinished(FinishedEventArgs.FinishState.Open, e2.FileName, e2.FilePath);
			break;
		case Body_OpenHoleFile.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		}
	}
}
