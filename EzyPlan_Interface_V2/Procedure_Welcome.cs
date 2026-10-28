using System;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

internal class Procedure_Welcome : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Close,
			HoleManagement,
			CreateNewHole,
			OpenPreviouseHole,
			OpenExistingHole
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

	private DrillHoleFile m_drillHoleFile;

	private Body_Welcome m_controlWelcome;

	public event EventHandler Finished;

	public Procedure_Welcome()
	{
		m_controlWelcome = new Body_Welcome();
		m_controlWelcome.ButtonPressed += m_Welcome_ButtonPressed;
	}

	public Procedure_Welcome(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
		: this()
	{
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_drillHoleFile = drillHoleFile;
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

	private void OnFinished(FinishedEventArgs.FinishState state, string filePath)
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FilePath = filePath;
		e.State = state;
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void m_Welcome_ButtonPressed(object sender, EventArgs e)
	{
		Body_Welcome.ButtonPressedEventArgs e2 = (Body_Welcome.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_Welcome.ButtonPressedEventArgs.ButtonPressedEnum.OpenExistingHole:
			OnFinished(FinishedEventArgs.FinishState.OpenExistingHole);
			break;
		case Body_Welcome.ButtonPressedEventArgs.ButtonPressedEnum.OpenPreviousHole:
		{
			if (OpenPreviousFile(out var filePath))
			{
				OnFinished(FinishedEventArgs.FinishState.OpenPreviouseHole, filePath);
			}
			break;
		}
		case Body_Welcome.ButtonPressedEventArgs.ButtonPressedEnum.CreateNewHole:
			OnFinished(FinishedEventArgs.FinishState.CreateNewHole);
			break;
		case Body_Welcome.ButtonPressedEventArgs.ButtonPressedEnum.HoleManagement:
			OnFinished(FinishedEventArgs.FinishState.HoleManagement);
			break;
		case Body_Welcome.ButtonPressedEventArgs.ButtonPressedEnum.Close:
			OnFinished(FinishedEventArgs.FinishState.Close);
			break;
		}
	}

	private bool OpenPreviousFile(string filePath)
	{
		DrillHoleFile.OpenFile_ErrorsEnum openFile_ErrorsEnum = m_drillHoleFile.Open(filePath);
		if (openFile_ErrorsEnum == DrillHoleFile.OpenFile_ErrorsEnum.NoError)
		{
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error opening file!", DrillHoleFile.ExplainError(openFile_ErrorsEnum) + ".");
		popup_MessageBox_OK.ShowDialog();
		return false;
	}

	private bool OpenPreviousFile(out string filePath)
	{
		filePath = Settings.Default.PreviouseFile;
		if (filePath != "")
		{
			return OpenPreviousFile(filePath);
		}
		return false;
	}

	public void Start()
	{
		m_labelHeading.Text = "WELCOME";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		m_panelFill.Controls.Add(m_controlWelcome);
		m_controlWelcome.Dock = DockStyle.Fill;
		m_controlWelcome.Display();
	}

	private void ConfigureLayout()
	{
		HideToolbarPanel();
		HideInfoPanel();
	}

	private void HideInfoPanel()
	{
		m_panelInfo.Visible = false;
	}

	private void HideToolbarPanel()
	{
		m_panelToolbar.Visible = false;
	}

	public void Resize()
	{
	}
}
