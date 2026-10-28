using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using LumenWorks.Framework.IO.Csv;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_Import_SelectMap : Layout_Body_Wizard
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Create,
			Import,
			Cancel,
			Back
		}

		private FinishedStateEnum m_state;

		private CachedCsvReader m_csvReader;

		private ImportSurveyMapping m_mapping;

		private SurveyList m_importedSurveyData;

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

		public CachedCsvReader CSVReader
		{
			get
			{
				return m_csvReader;
			}
			set
			{
				m_csvReader = value;
			}
		}

		public ImportSurveyMapping ImportMapping
		{
			get
			{
				return m_mapping;
			}
			set
			{
				m_mapping = value;
			}
		}

		public SurveyList ImportedSurveyData
		{
			get
			{
				return m_importedSurveyData;
			}
			set
			{
				m_importedSurveyData = value;
			}
		}
	}

	private DrillHoleFile m_drillHoleFile;

	private CachedCsvReader m_csvReader;

	private StreamReader m_streamReader;

	private ImportSurveyMappingList m_mappingList;

	private string m_mappingListXMLPath = Application.StartupPath + "\\ImportMapping.xml";

	private ImportSurveyMapping m_defaultMapping;

	private Color m_mdColour;

	private Color m_dipColour;

	private Color m_azimuthColour;

	private IContainer components;

	private GroupBox groupBox_data;

	private Panel panel_Mapping;

	private GroupBox groupBox_Options;

	private Panel panel2;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Next;

	private GroupBox groupBox_SelectMap;

	private ListBox listBox_Maps;

	private RibbonMenuButton button_Delete;

	private RibbonMenuButton button_AddNew;

	private DataGridView dataGridView1;

	private Panel panel1;

	public event EventHandler Finished;

	public Body_Import_SelectMap(ref DrillHoleFile drillHoleFile)
	{
		m_drillHoleFile = drillHoleFile;
		InitializeComponent();
		m_designTimeWidth = Width;
		m_mappingList = new ImportSurveyMappingList();
		m_mdColour = Color.Blue;
		m_dipColour = Color.Blue;
		m_azimuthColour = Color.Blue;
		m_defaultMapping = new ImportSurveyMapping("Standard Map", 3, 2, 1, setHeader: true);
	}

	private void CloseFileStream()
	{
		if (m_streamReader != null)
		{
			m_csvReader = null;
			m_streamReader.Close();
		}
	}

	private void OnFinished(FinishedEventArgs e)
	{
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	public void Display(ref StreamReader streamReader)
	{
		m_streamReader = streamReader;
		if (ReadCSVFile(streamReader))
		{
			if (LoadMappingList(m_mappingListXMLPath))
			{
				InitialiseMapping();
				UpdateMappingList();
				SelectDefaultMappingListItem();
			}
		}
		else
		{
			Back();
		}
		listBox_Maps.Focus();
	}

	public void Display(ImportSurveyMapping mapping)
	{
		listBox_Maps.Focus();
		AppendImportMappingToList(mapping);
		UpdateMappingList();
		SelectLastMappingListItem();
	}

	public void Display()
	{
		Focus();
		SelectDefaultMappingListItem();
	}

	private void InitialiseMapping()
	{
		SetHeaderPresent(headerPresent: false);
		ResetColumnsToDefault();
	}

	public void RemoveFromDisplay()
	{
	}

	private bool LoadMappingList(string xmlPath)
	{
		if (m_mappingList.LoadFromXML(xmlPath) == ImportSurveyMappingList.LoadFromXML_ErrorsEnum.NoError)
		{
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("System Error", "The mapping schema list could not be loaded. A new empty list will be created.");
		popup_MessageBox_OK.ShowDialog();
		CreateMappingListFile(xmlPath);
		return false;
	}

	private void UpdateMappingList()
	{
		listBox_Maps.Items.Clear();
		listBox_Maps.Items.Add(m_defaultMapping.Name);
		foreach (ImportSurveyMapping mapping in m_mappingList)
		{
			listBox_Maps.Items.Add(mapping.Name);
		}
	}

	private void SelectLastMappingListItem()
	{
		if (listBox_Maps.Items.Count > 0)
		{
			listBox_Maps.SelectedIndex = listBox_Maps.Items.Count - 1;
		}
	}

	private void SelectDefaultMappingListItem()
	{
		if (listBox_Maps.Items.Count > 0)
		{
			listBox_Maps.SelectedIndex = 0;
		}
	}

	private bool AppendImportMappingToList(ImportSurveyMapping mapping)
	{
		if (m_mappingList.Add(mapping))
		{
			if (m_mappingList.SaveToXML(m_mappingListXMLPath) != ImportSurveyMappingList.SaveToXML_ErrorsEnum.NoError)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "The modified \"Import Mapping\" list could not be saved.");
				popup_MessageBox_OK.ShowDialog();
				return false;
			}
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Critical Error", "An unexpected error has occured and the new \"Import Mapping\" could not be added.");
		popup_MessageBox_OK2.ShowDialog();
		return false;
	}

	private bool CreateMappingListFile(string filePath)
	{
		m_mappingList.Clear();
		if (m_mappingList.SaveToXML(filePath) != ImportSurveyMappingList.SaveToXML_ErrorsEnum.NoError)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "A new mapping schema list could not be created.");
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
		UpdateMappingList();
		return true;
	}

	private void CreateNewMap()
	{
		if (m_csvReader != null)
		{
			FinishedEventArgs e = new FinishedEventArgs();
			e.FinishedState = FinishedEventArgs.FinishedStateEnum.Create;
			e.CSVReader = m_csvReader;
			OnFinished(e);
		}
	}

	private void DeleteSelectedMap()
	{
		if (listBox_Maps.SelectedItems.Count <= 0)
		{
			return;
		}
		int selectedIndex = listBox_Maps.SelectedIndex;
		if (selectedIndex != 0)
		{
			m_mappingList.Remove(selectedIndex - 1);
			UpdateMappingList();
			if (m_mappingList.SaveToXML(m_mappingListXMLPath) != ImportSurveyMappingList.SaveToXML_ErrorsEnum.NoError)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "The \"Import Mapping\" list changes could not be saved.");
				popup_MessageBox_OK.ShowDialog();
			}
		}
	}

	private ImportSurveyMapping GetSelectedImportSurveyMapping()
	{
		if (listBox_Maps.SelectedItems.Count > 0)
		{
			int selectedIndex = listBox_Maps.SelectedIndex;
			if (selectedIndex == 0)
			{
				return m_defaultMapping;
			}
			return m_mappingList.RecordAt(selectedIndex - 1);
		}
		return null;
	}

	private void DisplayMappingOnDataGird(ImportSurveyMapping mapping)
	{
		InitialiseMapping();
		SetHeaderPresent(mapping.Header);
		SetMDIndex(mapping.MD);
		SetDipIndex(mapping.Dip);
		SetAzimuthIndex(mapping.Azimuth);
	}

	private void SetHeaderPresent(bool headerPresent)
	{
		if (dataGridView1.Rows.Count > 0)
		{
			if (headerPresent)
			{
				dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Red;
			}
			else
			{
				dataGridView1.Rows[0].DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
			}
		}
	}

	private void SetMDIndex(int index)
	{
		foreach (DataGridViewColumn column in dataGridView1.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "MD";
				column.DefaultCellStyle.ForeColor = m_mdColour;
			}
		}
	}

	private void SetDipIndex(int index)
	{
		foreach (DataGridViewColumn column in dataGridView1.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "Dip";
				column.DefaultCellStyle.ForeColor = m_dipColour;
			}
		}
	}

	private void SetAzimuthIndex(int index)
	{
		foreach (DataGridViewColumn column in dataGridView1.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "Azimuth";
				column.DefaultCellStyle.ForeColor = m_azimuthColour;
			}
		}
	}

	private void ResetColumnsToDefault()
	{
		foreach (DataGridViewColumn column in dataGridView1.Columns)
		{
			column.HeaderText = column.Name;
			column.DefaultCellStyle.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
		}
	}

	private bool ReadCSVFile(StreamReader streamReader)
	{
		if (streamReader != null)
		{
			Utility_FileOperations.OpenCSV_ErrorsEnum openCSV_ErrorsEnum = Utility_FileOperations.OpenCSVFile(ref streamReader, out var csv);
			if (openCSV_ErrorsEnum == Utility_FileOperations.OpenCSV_ErrorsEnum.NoError)
			{
				m_csvReader = csv;
				if (SetGridDataSource(ref csv))
				{
					return true;
				}
				return false;
			}
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error opening CSV file", Utility_FileOperations.ExplainError(openCSV_ErrorsEnum));
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
		return false;
	}

	private bool SetGridDataSource(ref CachedCsvReader csvReader)
	{
		try
		{
			dataGridView1.DataSource = csvReader;
			foreach (DataGridViewColumn column in dataGridView1.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			dataGridView1.Refresh();
			return true;
		}
		catch (MissingFieldCsvException ex)
		{
			dataGridView1.DataSource = null;
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error in CSV file!", ex.Message);
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
	}

	private void ImportSurveyData()
	{
		ImportSurveyMapping selectedImportSurveyMapping = GetSelectedImportSurveyMapping();
		if (selectedImportSurveyMapping == null)
		{
			return;
		}
		SurveyList surveyList = new SurveyList();
		SurveyList.ImportCSV_ErrorsEnum importCSV_ErrorsEnum = surveyList.ImportCSV(ref m_csvReader, selectedImportSurveyMapping);
		if (importCSV_ErrorsEnum == SurveyList.ImportCSV_ErrorsEnum.NoError)
		{
			if (m_drillHoleFile.SurveyList.Count() > 0)
			{
				Popup_MessageBox_YesNo popup_MessageBox_YesNo = new Popup_MessageBox_YesNo("Import successfull!", "There are survey's already present. Do you want to replace them with the newly imported survey's?");
				popup_MessageBox_YesNo.ShowDialog();
				if (popup_MessageBox_YesNo.CustomDialogResult == Popup_MessageBox_YesNo.DialogResultEnum.Yes)
				{
					FinishedEventArgs e = new FinishedEventArgs();
					e.FinishedState = FinishedEventArgs.FinishedStateEnum.Import;
					e.ImportedSurveyData = surveyList;
					OnFinished(e);
				}
			}
			else
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Import Successfull!", "The survey's were succesfully imported from the CSV file.");
				popup_MessageBox_OK.ShowDialog();
				FinishedEventArgs e2 = new FinishedEventArgs();
				e2.FinishedState = FinishedEventArgs.FinishedStateEnum.Import;
				e2.ImportedSurveyData = surveyList;
				OnFinished(e2);
			}
		}
		else
		{
			Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Import Unsuccessfull!", SurveyList.ExplainError(importCSV_ErrorsEnum) + ".");
			popup_MessageBox_OK2.ShowDialog();
		}
	}

	private void Back()
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = FinishedEventArgs.FinishedStateEnum.Back;
		CloseFileStream();
		OnFinished(e);
	}

	private void Cancel()
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
		CloseFileStream();
		OnFinished(e);
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		Back();
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		ImportSurveyData();
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void button_AddNew_Click(object sender, EventArgs e)
	{
		CreateNewMap();
	}

	private void button_Delete_Click(object sender, EventArgs e)
	{
		DeleteSelectedMap();
	}

	private void listBox_Maps_SelectedIndexChanged(object sender, EventArgs e)
	{
		ImportSurveyMapping selectedImportSurveyMapping = GetSelectedImportSurveyMapping();
		if (selectedImportSurveyMapping != null)
		{
			DisplayMappingOnDataGird(selectedImportSurveyMapping);
		}
	}

	private void button_AddNew_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AddNew);
	}

	private void button_AddNew_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AddNew);
	}

	private void button_Delete_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Delete);
	}

	private void button_Delete_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Delete);
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox_data = new System.Windows.Forms.GroupBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.panel_Mapping = new System.Windows.Forms.Panel();
		this.groupBox_SelectMap = new System.Windows.Forms.GroupBox();
		this.listBox_Maps = new System.Windows.Forms.ListBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox_Options = new System.Windows.Forms.GroupBox();
		this.button_Delete = new RibbonStyle.RibbonMenuButton();
		this.button_AddNew = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_data.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.panel_Mapping.SuspendLayout();
		this.groupBox_SelectMap.SuspendLayout();
		this.groupBox_Options.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(763, 24);
		base.label_headerText1.Text = "Select the map to use when importing the survey data and click Next to continue:";
		base.groupBox_frame.Size = new System.Drawing.Size(884, 471);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Location = new System.Drawing.Point(432, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(466, 55);
		base.panel_body.Location = new System.Drawing.Point(0, 71);
		base.panel_body.Size = new System.Drawing.Size(898, 485);
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Size = new System.Drawing.Size(898, 71);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 556);
		base.panel_footer.Size = new System.Drawing.Size(898, 55);
		base.label_headerText2.AutoSize = false;
		base.label_headerText2.Location = new System.Drawing.Point(3, 24);
		base.label_headerText2.Size = new System.Drawing.Size(825, 44);
		base.label_headerText2.Text = "The import map tells SphereIQ which columns of the CSV contain the required data. If the Default Map is not correct for the format of the CSV then click Add New to create a new custom map.";
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.groupBox_data);
		base.panel_scrollPanel.Controls.Add(this.panel_Mapping);
		base.panel_scrollPanel.Size = new System.Drawing.Size(878, 452);
		base.panel_scrollPanel.TabIndex = 1;
		this.groupBox_data.Controls.Add(this.dataGridView1);
		this.groupBox_data.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_data.Location = new System.Drawing.Point(0, 195);
		this.groupBox_data.Name = "groupBox_data";
		this.groupBox_data.Size = new System.Drawing.Size(878, 257);
		this.groupBox_data.TabIndex = 1;
		this.groupBox_data.TabStop = false;
		this.groupBox_data.Text = "CSV Data:";
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.Location = new System.Drawing.Point(3, 25);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView1.Size = new System.Drawing.Size(872, 229);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 2;
		this.panel_Mapping.Controls.Add(this.groupBox_SelectMap);
		this.panel_Mapping.Controls.Add(this.panel2);
		this.panel_Mapping.Controls.Add(this.groupBox_Options);
		this.panel_Mapping.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_Mapping.Location = new System.Drawing.Point(0, 0);
		this.panel_Mapping.Name = "panel_Mapping";
		this.panel_Mapping.Size = new System.Drawing.Size(878, 195);
		this.panel_Mapping.TabIndex = 0;
		this.groupBox_SelectMap.Controls.Add(this.listBox_Maps);
		this.groupBox_SelectMap.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_SelectMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_SelectMap.Location = new System.Drawing.Point(0, 0);
		this.groupBox_SelectMap.Name = "groupBox_SelectMap";
		this.groupBox_SelectMap.Size = new System.Drawing.Size(656, 195);
		this.groupBox_SelectMap.TabIndex = 0;
		this.groupBox_SelectMap.TabStop = false;
		this.groupBox_SelectMap.Text = "Select Import Map:";
		this.listBox_Maps.BackColor = System.Drawing.Color.White;
		this.listBox_Maps.Cursor = System.Windows.Forms.Cursors.Hand;
		this.listBox_Maps.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listBox_Maps.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.listBox_Maps.FormattingEnabled = true;
		this.listBox_Maps.ItemHeight = 24;
		this.listBox_Maps.Location = new System.Drawing.Point(3, 25);
		this.listBox_Maps.Name = "listBox_Maps";
		this.listBox_Maps.Size = new System.Drawing.Size(650, 148);
		this.listBox_Maps.TabIndex = 35;
		this.listBox_Maps.SelectedIndexChanged += new System.EventHandler(this.listBox_Maps_SelectedIndexChanged);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(656, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 195);
		this.panel2.TabIndex = 34;
		this.groupBox_Options.Controls.Add(this.button_Delete);
		this.groupBox_Options.Controls.Add(this.button_AddNew);
		this.groupBox_Options.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupBox_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Options.Location = new System.Drawing.Point(666, 0);
		this.groupBox_Options.Name = "groupBox_Options";
		this.groupBox_Options.Size = new System.Drawing.Size(212, 195);
		this.groupBox_Options.TabIndex = 1;
		this.groupBox_Options.TabStop = false;
		this.groupBox_Options.Text = "Map Options:";
		this.button_Delete.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Delete.AutoEllipsis = true;
		this.button_Delete.BackColor = System.Drawing.Color.Transparent;
		this.button_Delete.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Delete.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Delete.ColorOn = System.Drawing.Color.Orange;
		this.button_Delete.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Delete.ColorPress = System.Drawing.Color.Orange;
		this.button_Delete.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Delete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Delete.FadingSpeed = 35;
		this.button_Delete.FlatAppearance.BorderSize = 0;
		this.button_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Delete.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Delete.Image = EzyPlan_Interface_V2.Properties.Resources.Delete_Document;
		this.button_Delete.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Delete.ImageOffset = 4;
		this.button_Delete.IsPressed = false;
		this.button_Delete.KeepPress = false;
		this.button_Delete.Location = new System.Drawing.Point(6, 106);
		this.button_Delete.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Delete.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Delete.Name = "button_Delete";
		this.button_Delete.Radius = 10;
		this.button_Delete.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Delete.Size = new System.Drawing.Size(200, 50);
		this.button_Delete.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Delete.SplitDistance = 0;
		this.button_Delete.TabIndex = 2;
		this.button_Delete.Text = "Delete Selected";
		this.button_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Delete.Title = "";
		this.button_Delete.UseVisualStyleBackColor = true;
		this.button_Delete.Enter += new System.EventHandler(this.button_Delete_Enter);
		this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
		this.button_Delete.Leave += new System.EventHandler(this.button_Delete_Leave);
		this.button_AddNew.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_AddNew.AutoEllipsis = true;
		this.button_AddNew.BackColor = System.Drawing.Color.Transparent;
		this.button_AddNew.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AddNew.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AddNew.ColorOn = System.Drawing.Color.Orange;
		this.button_AddNew.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_AddNew.ColorPress = System.Drawing.Color.Orange;
		this.button_AddNew.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AddNew.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AddNew.FadingSpeed = 35;
		this.button_AddNew.FlatAppearance.BorderSize = 0;
		this.button_AddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AddNew.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AddNew.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.button_AddNew.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AddNew.ImageOffset = 4;
		this.button_AddNew.IsPressed = false;
		this.button_AddNew.KeepPress = false;
		this.button_AddNew.Location = new System.Drawing.Point(6, 40);
		this.button_AddNew.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AddNew.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AddNew.Name = "button_AddNew";
		this.button_AddNew.Radius = 10;
		this.button_AddNew.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AddNew.Size = new System.Drawing.Size(200, 50);
		this.button_AddNew.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AddNew.SplitDistance = 0;
		this.button_AddNew.TabIndex = 0;
		this.button_AddNew.Text = "Add New";
		this.button_AddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AddNew.Title = "";
		this.button_AddNew.UseVisualStyleBackColor = true;
		this.button_AddNew.Enter += new System.EventHandler(this.button_AddNew_Enter);
		this.button_AddNew.Click += new System.EventHandler(this.button_AddNew_Click);
		this.button_AddNew.Leave += new System.EventHandler(this.button_AddNew_Leave);
		this.button_Cancel.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Cancel.AutoEllipsis = true;
		this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
		this.button_Cancel.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Cancel.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorOn = System.Drawing.Color.Orange;
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorPress = System.Drawing.Color.Orange;
		this.button_Cancel.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Cancel.FadingSpeed = 35;
		this.button_Cancel.FlatAppearance.BorderSize = 0;
		this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Cancel.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Stop_Document;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(311, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 2;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Back.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Back.AutoEllipsis = true;
		this.button_Back.BackColor = System.Drawing.Color.Transparent;
		this.button_Back.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Back.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorOn = System.Drawing.Color.Orange;
		this.button_Back.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorPress = System.Drawing.Color.Orange;
		this.button_Back.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Back.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Back.FadingSpeed = 35;
		this.button_Back.FlatAppearance.BorderSize = 0;
		this.button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Back.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Back.Image = EzyPlan_Interface_V2.Properties.Resources.back;
		this.button_Back.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Back.ImageOffset = 4;
		this.button_Back.IsPressed = false;
		this.button_Back.KeepPress = false;
		this.button_Back.Location = new System.Drawing.Point(157, 6);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(148, 45);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 1;
		this.button_Back.Text = "Back";
		this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.button_Next.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Next.AutoEllipsis = true;
		this.button_Next.BackColor = System.Drawing.Color.Transparent;
		this.button_Next.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Next.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorOn = System.Drawing.Color.Orange;
		this.button_Next.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorPress = System.Drawing.Color.Orange;
		this.button_Next.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Next.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Next.FadingSpeed = 35;
		this.button_Next.FlatAppearance.BorderSize = 0;
		this.button_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Next.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Next.Image = EzyPlan_Interface_V2.Properties.Resources.forward;
		this.button_Next.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Next.ImageOffset = 4;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(3, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(148, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 0;
		this.button_Next.Text = "Next";
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Import_Doc;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(834, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 71);
		this.panel1.TabIndex = 12;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_Import_SelectMap";
		base.Size = new System.Drawing.Size(898, 611);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_data.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.panel_Mapping.ResumeLayout(false);
		this.groupBox_SelectMap.ResumeLayout(false);
		this.groupBox_Options.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
