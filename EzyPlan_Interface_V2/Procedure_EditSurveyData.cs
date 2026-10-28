using System;
using System.IO;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using LumenWorks.Framework.IO.Csv;

namespace EzyPlan_Interface_V2;

internal class Procedure_EditSurveyData : IEzyPlanProcedure
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishState
		{
			Save,
			Cancel,
			SaveError
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

	private Body_EditSurveyData m_controlEditSurveyData;

	private Body_Import_OpenCSVFile m_controlSelectFile;

	private Body_Import_SelectMap m_controlSelectMap;

	private Body_Import_CreateMap m_controlCreateMap;

	private DrillHoleFile m_drillHoleFile;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	public event EventHandler Finished;

	public Procedure_EditSurveyData(ref Panel body, ref Panel toolbar, ref Panel info, ref Panel fillPanel, ref Label heading, ref EzyNavHoleChart ezyNavChart, ref DrillHoleFile drillHoleFile)
	{
		m_drillHoleFile = drillHoleFile;
		m_panelBody = body;
		m_panelToolbar = toolbar;
		m_panelInfo = info;
		m_panelFill = fillPanel;
		m_ezyNavChart = ezyNavChart;
		m_labelHeading = heading;
		m_extremeDL = Settings.Default.extremeDL;
		m_severeDL = Settings.Default.severeDL;
		m_controlEditSurveyData = new Body_EditSurveyData(ref drillHoleFile, m_severeDL, m_extremeDL, "Manually enter or edit the survey data:");
		m_controlEditSurveyData.ButtonPressed += m_controlEditSurveyData_ButtonPressed;
		m_controlSelectFile = new Body_Import_OpenCSVFile();
		m_controlSelectMap = new Body_Import_SelectMap(ref drillHoleFile);
		m_controlCreateMap = new Body_Import_CreateMap();
	}

	public void DriveArrived(string driveLetter)
	{
		m_controlSelectFile.DriveChange(driveLetter);
	}

	public void DriveRemoved()
	{
		m_controlSelectFile.DriveChange();
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
		m_labelHeading.Text = "Edit Survey Data";
		m_panelInfo.Controls.Clear();
		m_panelToolbar.Controls.Clear();
		m_panelFill.Controls.Clear();
		ConfigureLayout();
		Display_EditSurveyData();
	}

	private void Display_EditSurveyData()
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEditSurveyData);
		m_controlEditSurveyData.Dock = DockStyle.Fill;
		m_controlEditSurveyData.Display();
	}

	private void Display_EditSurveyData(SurveyList importedData)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlEditSurveyData);
		m_controlEditSurveyData.Dock = DockStyle.Fill;
		m_controlEditSurveyData.Display(importedData);
	}

	private void m_controlEditSurveyData_ButtonPressed(object sender, EventArgs e)
	{
		Body_EditSurveyData.ButtonPressedEventArgs e2 = (Body_EditSurveyData.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_EditSurveyData.ButtonPressedEventArgs.ButtonPressedEnum.Save:
			if (SaveChangesToFile())
			{
				OnFinished(FinishedEventArgs.FinishState.Save);
			}
			else
			{
				OnFinished(FinishedEventArgs.FinishState.SaveError);
			}
			break;
		case Body_EditSurveyData.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			OnFinished(FinishedEventArgs.FinishState.Cancel);
			break;
		case Body_EditSurveyData.ButtonPressedEventArgs.ButtonPressedEnum.Import:
			Display_SelectFile(refresh: true);
			break;
		case Body_EditSurveyData.ButtonPressedEventArgs.ButtonPressedEnum.Back:
			break;
		}
	}

	private bool SaveChangesToFile()
	{
		DrillHoleFile.Save_ErrorsEnum save_ErrorsEnum = m_drillHoleFile.Save();
		if (save_ErrorsEnum == DrillHoleFile.Save_ErrorsEnum.NoError)
		{
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error saving changes!", DrillHoleFile.ExplainError(save_ErrorsEnum) + ".");
		popup_MessageBox_OK.ShowDialog();
		return false;
	}

	private void Display_SelectMap(ref StreamReader streamReader)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlSelectMap);
		m_controlSelectMap.Dock = DockStyle.Fill;
		m_controlSelectMap.Finished += m_controlSelectMap_Finished;
		m_controlSelectMap.Display(ref streamReader);
	}

	private void Display_SelectMap(ImportSurveyMapping mapping)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlSelectMap);
		m_controlSelectMap.Dock = DockStyle.Fill;
		m_controlSelectMap.Finished += m_controlSelectMap_Finished;
		m_controlSelectMap.Display(mapping);
	}

	private void Display_SelectMap()
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlSelectMap);
		m_controlSelectMap.Dock = DockStyle.Fill;
		m_controlSelectMap.Finished += m_controlSelectMap_Finished;
		m_controlSelectMap.Display();
	}

	private void m_controlSelectMap_Finished(object sender, EventArgs e)
	{
		m_controlSelectMap.Finished -= m_controlSelectMap_Finished;
		Body_Import_SelectMap.FinishedEventArgs e2 = (Body_Import_SelectMap.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_Import_SelectMap.FinishedEventArgs.FinishedStateEnum.Create:
		{
			CachedCsvReader cSVReader = e2.CSVReader;
			Display_CreateMap(cSVReader);
			break;
		}
		case Body_Import_SelectMap.FinishedEventArgs.FinishedStateEnum.Import:
		{
			SurveyList importedSurveyData = e2.ImportedSurveyData;
			Display_EditSurveyData(importedSurveyData);
			break;
		}
		case Body_Import_SelectMap.FinishedEventArgs.FinishedStateEnum.Back:
			Display_SelectFile(refresh: false);
			break;
		case Body_Import_SelectMap.FinishedEventArgs.FinishedStateEnum.Cancel:
			Display_EditSurveyData();
			break;
		}
	}

	private void Display_CreateMap(CachedCsvReader csvReader)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlCreateMap);
		m_controlCreateMap.Dock = DockStyle.Fill;
		m_controlCreateMap.Display(csvReader);
		m_controlCreateMap.Finished += m_controlCreateMap_Finished;
	}

	private void m_controlCreateMap_Finished(object sender, EventArgs e)
	{
		m_controlCreateMap.Finished -= m_controlCreateMap_Finished;
		Body_Import_CreateMap.FinishedEventArgs e2 = (Body_Import_CreateMap.FinishedEventArgs)e;
		switch (e2.FinishedState)
		{
		case Body_Import_CreateMap.FinishedEventArgs.FinishedStateEnum.Save:
		{
			ImportSurveyMapping mapping = e2.Mapping;
			Display_SelectMap(mapping);
			break;
		}
		case Body_Import_CreateMap.FinishedEventArgs.FinishedStateEnum.Cancel:
			Display_SelectMap();
			break;
		}
	}

	private void Display_SelectFile(bool refresh)
	{
		m_panelFill.Controls.Clear();
		m_panelFill.Controls.Add(m_controlSelectFile);
		m_controlSelectFile.Dock = DockStyle.Fill;
		m_controlSelectFile.ButtonPressed += m_controlSelectFile_ButtonPressed;
		m_controlSelectFile.Display(refresh);
	}

	private void m_controlSelectFile_ButtonPressed(object sender, EventArgs e)
	{
		m_controlSelectFile.ButtonPressed -= m_controlSelectFile_ButtonPressed;
		Body_Import_OpenCSVFile.ButtonPressedEventArgs e2 = (Body_Import_OpenCSVFile.ButtonPressedEventArgs)e;
		switch (e2.ButtonPressed)
		{
		case Body_Import_OpenCSVFile.ButtonPressedEventArgs.ButtonPressedEnum.Open:
		{
			StreamReader streamReader = e2.StreamReader;
			Display_SelectMap(ref streamReader);
			break;
		}
		case Body_Import_OpenCSVFile.ButtonPressedEventArgs.ButtonPressedEnum.Cancel:
			Display_EditSurveyData();
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
