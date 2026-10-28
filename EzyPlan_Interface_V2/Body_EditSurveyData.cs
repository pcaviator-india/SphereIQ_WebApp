using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_EditSurveyData : Layout_Body_Wizard
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Save,
			Cancel,
			Back,
			Import
		}

		private ButtonPressedEnum m_buttonPressed;

		public ButtonPressedEnum ButtonPressed
		{
			get
			{
				return m_buttonPressed;
			}
			set
			{
				m_buttonPressed = value;
			}
		}
	}

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private Popup_EditSurveyData_EditSurvey m_popup_Edit;

	private Popup_EditSurveyData_Add m_popup_AddNew;

	private DrillHoleFile m_drillHoleFile;

	private SurveyList m_surveyData;

	private AbsoluteSurveyList m_absoluteSurveyList;

	private object m_threadLock;

	private NavMath m_math;

	private IContainer components;

	private DataGridView dataGridView1;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Save;

	private ContextMenuStrip contextMenuStrip_EditOptions;

	private ToolStripMenuItem menuButton_Edit_Insert;

	private ToolStripMenuItem menuButton_Edit_Delete;

	private ToolStripMenuItem menuButton_Edit_Edit;

	private ContextMenuStrip contextMenuStrip_AddOptions;

	private ToolStripMenuItem menuButton_Add_Append;

	private ToolStripMenuItem menuButton_Add_Insert;

	private Panel panel1;

	private RibbonMenuButton button_EditSelected;

	private Panel panel2;

	private RibbonMenuButton buttonRemoveSelected;

	private Panel panel3;

	private RibbonMenuButton button_AddNew;

	private Panel panel4;

	private Panel panel5;

	private ToolStripMenuItem menuButton_Edit_Copy;

	private RibbonMenuButton button_Copy;

	private Panel panel7;

	private RibbonMenuButton button_Back;

	private Panel panel8;

	private ToolStripMenuItem menuButton_Edit_SelectAll;

	private Label label1;

	private Label label2;

	private Panel panel_SevereDL;

	private Panel panel_ExtremeDL;

	private Panel panel_Key;

	private Panel panel6;

	private Label label3;

	private Label label4;

	public event EventHandler ButtonPressed;

	public Body_EditSurveyData(ref DrillHoleFile drillHoleFile, decimal severeDL, decimal extremeDL, string descriptionText)
	{
		InitializeComponent();
		label_headerText1.Text = descriptionText;
		m_drillHoleFile = drillHoleFile;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		m_popup_Edit = new Popup_EditSurveyData_EditSurvey();
		m_popup_AddNew = new Popup_EditSurveyData_Add();
		m_absoluteSurveyList = new AbsoluteSurveyList();
		m_absoluteSurveyList.List.RaiseListChangedEvents = false;
		dataGridView1.DataSource = m_absoluteSurveyList.List;
		m_math = new NavMath();
		SetDLSeverityKey();
		m_threadLock = new object();
	}

	public void Display()
	{
		ResetDataModified();
		DisableBack();
		m_surveyData = new SurveyList(m_drillHoleFile.SurveyList);
		UpdateDataGrid();
		dataGridView1.Focus();
	}

	public void Display(SurveyList surveyList)
	{
		SetDLSeverityKey();
		DataModified();
		EnableBack();
		m_surveyData = surveyList;
		UpdateDataGrid();
		dataGridView1.Focus();
	}

	private void SetDLSeverityKey()
	{
		panel_SevereDL.BackColor = Utility.m_SevereColor;
		panel_ExtremeDL.BackColor = Utility.m_ExtremeColor;
	}

	private void ClearGrid()
	{
		if (dataGridView1.InvokeRequired)
		{
			MethodInvoker method = ClearGrid;
			dataGridView1.Invoke(method);
		}
		else
		{
			dataGridView1.Rows.Clear();
		}
	}

	public void UpdateDataGrid()
	{
		m_absoluteSurveyList.Clear();
		m_math.AbsoluteSurvey(m_drillHoleFile.Collar, m_surveyData, ref m_absoluteSurveyList);
		m_absoluteSurveyList.List.ForceChangeEvent();
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			Utility.StringToDecimal(item.Cells[6].Value.ToString(), out var number);
			if (number >= m_extremeDL)
			{
				item.DefaultCellStyle.BackColor = Utility.m_ExtremeColor;
			}
			else if (number >= m_severeDL)
			{
				item.DefaultCellStyle.BackColor = Utility.m_SevereColor;
			}
		}
	}

	private void SelectLastRow()
	{
		if (dataGridView1.Rows.Count > 0)
		{
			dataGridView1.ClearSelection();
			dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
			dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
		}
	}

	private void SelectRowAndScrollTo(int index)
	{
		if (dataGridView1.Rows.Count > 0 && index <= dataGridView1.Rows.Count - 1 && index <= dataGridView1.Rows.Count - 1)
		{
			dataGridView1.ClearSelection();
			dataGridView1.Rows[index].Selected = true;
			dataGridView1.FirstDisplayedScrollingRowIndex = index;
		}
	}

	private void SelectRow(int index)
	{
		if (dataGridView1.Rows.Count > 0 && index <= dataGridView1.Rows.Count - 1 && index <= dataGridView1.Rows.Count - 1)
		{
			dataGridView1.ClearSelection();
			dataGridView1.Rows[index].Selected = true;
		}
	}

	private void DataModified()
	{
		button_Cancel.Text = "Cancel";
		button_Save.Visible = true;
	}

	private void ResetDataModified()
	{
		button_Cancel.Text = "Cancel";
		button_Save.Visible = false;
	}

	private void EnableBack()
	{
		button_Back.Visible = true;
	}

	private void DisableBack()
	{
		button_Back.Visible = false;
	}

	private void Edit()
	{
		m_popup_Edit.Save += popup_Edit_Save;
		m_popup_Edit.Cancel += popup_Edit_Cancel;
		if (dataGridView1.SelectedRows.Count > 0)
		{
			if (dataGridView1.SelectedRows.Count == 1)
			{
				int index = dataGridView1.SelectedRows[0].Index;
				m_popup_Edit.ShowDialog(m_surveyData.RecordAt(index), index);
			}
			else
			{
				new Popup_MessageBox_OK("You can't edit multiple surveys!", "Select a single survey to edit.");
			}
		}
	}

	private void popup_Edit_Cancel(object sender, EventArgs e)
	{
		m_popup_Edit.Save -= popup_Edit_Save;
		m_popup_Edit.Cancel -= popup_Edit_Cancel;
		m_popup_Edit.Close();
	}

	private void popup_Edit_Save(object sender, EventArgs e)
	{
		Popup_EditSurveyData_EditSurvey.SaveSurveyEventArgs e2 = (Popup_EditSurveyData_EditSurvey.SaveSurveyEventArgs)e;
		int index = dataGridView1.SelectedRows[0].Index;
		SurveyList.UpdateSurveyAtEnum updateSurveyAtEnum = m_surveyData.UpdateSurveyAt(index, e2.Survey);
		if (updateSurveyAtEnum == SurveyList.UpdateSurveyAtEnum.UpdateSuccessful)
		{
			m_popup_Edit.Save -= popup_Edit_Save;
			m_popup_Edit.Cancel -= popup_Edit_Cancel;
			UpdateDataGrid();
			DataModified();
			m_popup_Edit.Close();
			dataGridView1.Focus();
		}
		else
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not save changes!", SurveyList.ToHumanReadable(updateSurveyAtEnum));
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void Remove()
	{
		if (dataGridView1.SelectedRows.Count <= 0)
		{
			return;
		}
		Popup_MessageBox_YesNo popup_MessageBox_YesNo = new Popup_MessageBox_YesNo("Delete Selected Survey(s)", "Are you sure you want to delete the selected survey(s)?");
		popup_MessageBox_YesNo.ShowDialog();
		if (popup_MessageBox_YesNo.CustomDialogResult == Popup_MessageBox_YesNo.DialogResultEnum.Yes)
		{
			GetSelectedRows(out var mdList);
			SurveyList.RemoveEnum removeEnum = m_surveyData.Remove(mdList);
			if (removeEnum == SurveyList.RemoveEnum.RemoveSuccessfull)
			{
				UpdateDataGrid();
				DataModified();
				dataGridView1.Focus();
			}
			else
			{
				DisplayPopup(removeEnum);
			}
		}
	}

	private void GetSelectedRows(out List<decimal> mdList)
	{
		mdList = new List<decimal>();
		foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
		{
			mdList.Add((decimal)selectedRow.Cells[0].Value);
		}
	}

	public void DisplayPopup(SurveyList.RemoveEnum value)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Remove Error!", SurveyList.ToHumanReadable(value) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void Append()
	{
		Survey survey = new Survey();
		m_popup_AddNew.Add += Append_Add;
		m_popup_AddNew.AddFinish += Append_AddFinish;
		m_popup_AddNew.Cancel += Append_Cancel;
		SelectLastRow();
		m_popup_AddNew.ShowDialog(survey, "Append new survey:");
	}

	private void Append_Cancel(object sender, EventArgs e)
	{
		m_popup_AddNew.Add -= Append_Add;
		m_popup_AddNew.AddFinish -= Append_AddFinish;
		m_popup_AddNew.Cancel -= Append_Cancel;
		m_popup_AddNew.Close();
		dataGridView1.Focus();
	}

	private void Append_AddFinish(object sender, EventArgs e)
	{
		Popup_EditSurveyData_Add.SaveSurveyEventArgs e2 = (Popup_EditSurveyData_Add.SaveSurveyEventArgs)e;
		SurveyList.AppendErrorsEnum appendErrorsEnum = m_surveyData.Append(e2.Survey);
		if (appendErrorsEnum == SurveyList.AppendErrorsEnum.NoError)
		{
			m_popup_AddNew.Add -= Append_Add;
			m_popup_AddNew.AddFinish -= Append_AddFinish;
			m_popup_AddNew.Cancel -= Append_Cancel;
			UpdateDataGrid();
			SelectLastRow();
			DataModified();
			m_popup_AddNew.Close();
			dataGridView1.Focus();
		}
		else
		{
			DisplayPopup(appendErrorsEnum);
		}
	}

	private void Append_Add(object sender, EventArgs e)
	{
		Popup_EditSurveyData_Add.SaveSurveyEventArgs e2 = (Popup_EditSurveyData_Add.SaveSurveyEventArgs)e;
		SurveyList.AppendErrorsEnum appendErrorsEnum = m_surveyData.Append(e2.Survey);
		if (appendErrorsEnum == SurveyList.AppendErrorsEnum.NoError)
		{
			UpdateDataGrid();
			DataModified();
			SelectLastRow();
		}
		else
		{
			DisplayPopup(appendErrorsEnum);
		}
	}

	private void DisplayPopup(SurveyList.AppendErrorsEnum value)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not append new survey!", SurveyList.ToHumanReadable(value) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void Insert()
	{
		if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count == 1)
		{
			Survey survey = new Survey();
			int index = dataGridView1.SelectedRows[0].Index;
			m_popup_AddNew.Add += Insert_Add;
			m_popup_AddNew.AddFinish += Insert_AddFinish;
			m_popup_AddNew.Cancel += Insert_Cancel;
			m_popup_AddNew.ShowDialog(index, survey, "Insert new survey above selected:");
		}
	}

	private void Insert_Cancel(object sender, EventArgs e)
	{
		m_popup_AddNew.Add -= Insert_Add;
		m_popup_AddNew.AddFinish -= Insert_AddFinish;
		m_popup_AddNew.Cancel -= Insert_Cancel;
		m_popup_AddNew.Close();
		dataGridView1.Focus();
	}

	private void Insert_AddFinish(object sender, EventArgs e)
	{
		Popup_EditSurveyData_Add.SaveSurveyEventArgs e2 = (Popup_EditSurveyData_Add.SaveSurveyEventArgs)e;
		SurveyList.InsertErrorsEnum insertErrorsEnum = m_surveyData.Insert(e2.Index, e2.Survey);
		if (insertErrorsEnum == SurveyList.InsertErrorsEnum.NoError)
		{
			m_popup_AddNew.Add -= Insert_Add;
			m_popup_AddNew.AddFinish -= Insert_AddFinish;
			m_popup_AddNew.Cancel -= Insert_Cancel;
			UpdateDataGrid();
			DataModified();
			m_popup_AddNew.Close();
			dataGridView1.Focus();
		}
		else
		{
			DisplayPopup(insertErrorsEnum);
		}
	}

	private void Insert_Add(object sender, EventArgs e)
	{
		Popup_EditSurveyData_Add.SaveSurveyEventArgs e2 = (Popup_EditSurveyData_Add.SaveSurveyEventArgs)e;
		SurveyList.InsertErrorsEnum insertErrorsEnum = m_surveyData.Insert(e2.Index, e2.Survey);
		if (insertErrorsEnum == SurveyList.InsertErrorsEnum.NoError)
		{
			UpdateDataGrid();
			DataModified();
		}
		else
		{
			DisplayPopup(insertErrorsEnum);
		}
	}

	private void DisplayPopup(SurveyList.InsertErrorsEnum value)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not insert new survey!", SurveyList.ToHumanReadable(value) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void SelectAllSurveys()
	{
		dataGridView1.SelectAll();
	}

	private void CopySelectedToClipboard()
	{
		if (dataGridView1.GetClipboardContent() != null)
		{
			try
			{
				Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
			}
			catch (ExternalException)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error copying to clipboard", "The systems clipboard could not be accessed.");
				popup_MessageBox_OK.ShowDialog();
			}
		}
	}

	private void OnButtonPressed(ButtonPressedEventArgs.ButtonPressedEnum state)
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = state;
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void Save()
	{
		m_drillHoleFile.SurveyList = m_surveyData;
		OnButtonPressed(ButtonPressedEventArgs.ButtonPressedEnum.Save);
	}

	private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			DataGridView.HitTestInfo hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
			if (hitTestInfo.RowIndex >= 0 && !dataGridView1.Rows[hitTestInfo.RowIndex].Selected)
			{
				SelectRow(hitTestInfo.RowIndex);
			}
		}
	}

	private void Cancel()
	{
		OnButtonPressed(ButtonPressedEventArgs.ButtonPressedEnum.Cancel);
	}

	private void Back()
	{
		OnButtonPressed(ButtonPressedEventArgs.ButtonPressedEnum.Back);
	}

	private void Import()
	{
		OnButtonPressed(ButtonPressedEventArgs.ButtonPressedEnum.Import);
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		Back();
	}

	private void button_EditSelected_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void button_RemoveSelected_Click(object sender, EventArgs e)
	{
		Remove();
	}

	private void button_AddNew_Click(object sender, EventArgs e)
	{
		Append();
	}

	private void button_Save_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void button_Import_Click(object sender, EventArgs e)
	{
		Import();
	}

	private void menuButton_Edit_Insert_Click(object sender, EventArgs e)
	{
		Insert();
	}

	private void menuButton_Edit_Delete_Click(object sender, EventArgs e)
	{
		Remove();
	}

	private void menuButton_Edit_Edit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void menuButton_Add_Append_Click(object sender, EventArgs e)
	{
		Append();
	}

	private void menuButton_Add_Insert_Click(object sender, EventArgs e)
	{
		Insert();
	}

	private void menuButton_Edit_Copy_Click(object sender, EventArgs e)
	{
		CopySelectedToClipboard();
	}

	private void button_Copy_Click(object sender, EventArgs e)
	{
		CopySelectedToClipboard();
	}

	private void menuButton_Edit_SelectAll_Click(object sender, EventArgs e)
	{
		SelectAllSurveys();
	}

	private void dataGridView1_Enter(object sender, EventArgs e)
	{
	}

	private void dataGridView1_Leave(object sender, EventArgs e)
	{
	}

	private void button_AddNew_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AddNew);
	}

	private void button_AddNew_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AddNew);
	}

	private void button_DeleteSelected_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref buttonRemoveSelected);
	}

	private void button_DeleteSelected_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref buttonRemoveSelected);
	}

	private void button_EditSelected_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_EditSelected);
	}

	private void button_EditSelected_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_EditSelected);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void button_Save_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Save);
	}

	private void button_Save_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Save);
	}

	private void button_Copy_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Copy);
	}

	private void button_Copy_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Copy);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.contextMenuStrip_EditOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.menuButton_Edit_Insert = new System.Windows.Forms.ToolStripMenuItem();
		this.menuButton_Edit_Delete = new System.Windows.Forms.ToolStripMenuItem();
		this.menuButton_Edit_Edit = new System.Windows.Forms.ToolStripMenuItem();
		this.menuButton_Edit_Copy = new System.Windows.Forms.ToolStripMenuItem();
		this.menuButton_Edit_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip_AddOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.menuButton_Add_Append = new System.Windows.Forms.ToolStripMenuItem();
		this.menuButton_Add_Insert = new System.Windows.Forms.ToolStripMenuItem();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button_Copy = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.button_EditSelected = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.buttonRemoveSelected = new RibbonStyle.RibbonMenuButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.button_AddNew = new RibbonStyle.RibbonMenuButton();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.panel_SevereDL = new System.Windows.Forms.Panel();
		this.panel_ExtremeDL = new System.Windows.Forms.Panel();
		this.panel_Key = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.button_Save = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.contextMenuStrip_EditOptions.SuspendLayout();
		this.contextMenuStrip_AddOptions.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel_Key.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(259, 24);
		base.label_headerText1.Text = "Descriptive text goes here.";
		base.groupBox_frame.Controls.Add(this.panel1);
		base.groupBox_frame.Size = new System.Drawing.Size(999, 557);
		base.groupBox_frame.Controls.SetChildIndex(this.panel1, 0);
		base.groupBox_frame.Controls.SetChildIndex(base.panel_scrollPanel, 0);
		base.panel_buttonPanel.Controls.Add(this.button_Save);
		base.panel_buttonPanel.Controls.Add(this.panel7);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.panel8);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Location = new System.Drawing.Point(530, 0);
		base.panel_buttonPanel.Padding = new System.Windows.Forms.Padding(5);
		base.panel_buttonPanel.Size = new System.Drawing.Size(485, 55);
		base.panel_body.BackColor = System.Drawing.Color.White;
		base.panel_body.Location = new System.Drawing.Point(0, 70);
		base.panel_body.Size = new System.Drawing.Size(1015, 573);
		base.panel_header.Controls.Add(this.panel_Key);
		base.panel_header.Controls.Add(this.label3);
		base.panel_header.Controls.Add(this.label4);
		base.panel_header.Size = new System.Drawing.Size(1015, 70);
		base.panel_header.Controls.SetChildIndex(this.label4, 0);
		base.panel_header.Controls.SetChildIndex(this.label3, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_Key, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 643);
		base.panel_footer.Size = new System.Drawing.Size(1015, 55);
		base.label_headerText2.Size = new System.Drawing.Size(0, 20);
		base.label_headerText2.Text = "";
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.dataGridView1);
		base.panel_scrollPanel.Controls.Add(this.panel4);
		base.panel_scrollPanel.Size = new System.Drawing.Size(993, 493);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.NullValue = null;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		this.dataGridView1.RowTemplate.ContextMenuStrip = this.contextMenuStrip_EditOptions;
		this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.ShowCellErrors = false;
		this.dataGridView1.ShowEditingIcon = false;
		this.dataGridView1.ShowRowErrors = false;
		this.dataGridView1.Size = new System.Drawing.Size(993, 483);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 0;
		this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
		this.dataGridView1.Enter += new System.EventHandler(this.dataGridView1_Enter);
		this.dataGridView1.Leave += new System.EventHandler(this.dataGridView1_Leave);
		this.contextMenuStrip_EditOptions.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_EditOptions.ImageScalingSize = new System.Drawing.Size(24, 24);
		this.contextMenuStrip_EditOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.menuButton_Edit_Insert, this.menuButton_Edit_Delete, this.menuButton_Edit_Edit, this.menuButton_Edit_Copy, this.menuButton_Edit_SelectAll });
		this.contextMenuStrip_EditOptions.Name = "contextMenuStrip_EditOptions";
		this.contextMenuStrip_EditOptions.Size = new System.Drawing.Size(409, 174);
		this.menuButton_Edit_Insert.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.menuButton_Edit_Insert.Name = "menuButton_Edit_Insert";
		this.menuButton_Edit_Insert.Size = new System.Drawing.Size(408, 34);
		this.menuButton_Edit_Insert.Text = "Insert Above Selected";
		this.menuButton_Edit_Insert.Click += new System.EventHandler(this.menuButton_Edit_Insert_Click);
		this.menuButton_Edit_Delete.Image = EzyPlan_Interface_V2.Properties.Resources.Delete_Record;
		this.menuButton_Edit_Delete.Name = "menuButton_Edit_Delete";
		this.menuButton_Edit_Delete.Size = new System.Drawing.Size(408, 34);
		this.menuButton_Edit_Delete.Text = "Remove Selected";
		this.menuButton_Edit_Delete.Click += new System.EventHandler(this.menuButton_Edit_Delete_Click);
		this.menuButton_Edit_Edit.Image = EzyPlan_Interface_V2.Properties.Resources.Edit_Record;
		this.menuButton_Edit_Edit.Name = "menuButton_Edit_Edit";
		this.menuButton_Edit_Edit.Size = new System.Drawing.Size(408, 34);
		this.menuButton_Edit_Edit.Text = "Edit Selected";
		this.menuButton_Edit_Edit.Click += new System.EventHandler(this.menuButton_Edit_Edit_Click);
		this.menuButton_Edit_Copy.Image = EzyPlan_Interface_V2.Properties.Resources.Copy;
		this.menuButton_Edit_Copy.Name = "menuButton_Edit_Copy";
		this.menuButton_Edit_Copy.Size = new System.Drawing.Size(408, 34);
		this.menuButton_Edit_Copy.Text = "Copy Selected To Clipboard";
		this.menuButton_Edit_Copy.Click += new System.EventHandler(this.menuButton_Edit_Copy_Click);
		this.menuButton_Edit_SelectAll.Image = EzyPlan_Interface_V2.Properties.Resources.Select_All;
		this.menuButton_Edit_SelectAll.Name = "menuButton_Edit_SelectAll";
		this.menuButton_Edit_SelectAll.Size = new System.Drawing.Size(408, 34);
		this.menuButton_Edit_SelectAll.Text = "Select All";
		this.menuButton_Edit_SelectAll.Click += new System.EventHandler(this.menuButton_Edit_SelectAll_Click);
		this.contextMenuStrip_AddOptions.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_AddOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.menuButton_Add_Append, this.menuButton_Add_Insert });
		this.contextMenuStrip_AddOptions.Name = "contextMenuStrip_AddOptions";
		this.contextMenuStrip_AddOptions.Size = new System.Drawing.Size(344, 72);
		this.menuButton_Add_Append.Name = "menuButton_Add_Append";
		this.menuButton_Add_Append.Size = new System.Drawing.Size(343, 34);
		this.menuButton_Add_Append.Text = "Append";
		this.menuButton_Add_Append.Click += new System.EventHandler(this.menuButton_Add_Append_Click);
		this.menuButton_Add_Insert.Name = "menuButton_Add_Insert";
		this.menuButton_Add_Insert.Size = new System.Drawing.Size(343, 34);
		this.menuButton_Add_Insert.Text = "Insert Above Selected";
		this.menuButton_Add_Insert.Click += new System.EventHandler(this.menuButton_Add_Insert_Click);
		this.panel1.Controls.Add(this.button_Copy);
		this.panel1.Controls.Add(this.panel5);
		this.panel1.Controls.Add(this.button_EditSelected);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.buttonRemoveSelected);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.button_AddNew);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(3, 509);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(993, 45);
		this.panel1.TabIndex = 2;
		this.button_Copy.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Copy.AutoEllipsis = true;
		this.button_Copy.BackColor = System.Drawing.Color.Transparent;
		this.button_Copy.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Copy.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Copy.ColorOn = System.Drawing.Color.Orange;
		this.button_Copy.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Copy.ColorPress = System.Drawing.Color.Orange;
		this.button_Copy.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Copy.Dock = System.Windows.Forms.DockStyle.Left;
		this.button_Copy.FadingSpeed = 35;
		this.button_Copy.FlatAppearance.BorderSize = 0;
		this.button_Copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Copy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Copy.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Copy.Image = EzyPlan_Interface_V2.Properties.Resources.Copy;
		this.button_Copy.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Copy.ImageOffset = 4;
		this.button_Copy.IsPressed = false;
		this.button_Copy.KeepPress = false;
		this.button_Copy.Location = new System.Drawing.Point(684, 0);
		this.button_Copy.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Copy.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Copy.Name = "button_Copy";
		this.button_Copy.Radius = 10;
		this.button_Copy.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Copy.Size = new System.Drawing.Size(210, 45);
		this.button_Copy.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Copy.SplitDistance = 0;
		this.button_Copy.TabIndex = 4;
		this.button_Copy.Text = "Copy Selected";
		this.button_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Copy.Title = "";
		this.button_Copy.UseVisualStyleBackColor = true;
		this.button_Copy.Click += new System.EventHandler(this.button_Copy_Click);
		this.button_Copy.Leave += new System.EventHandler(this.button_Copy_Leave);
		this.button_Copy.Enter += new System.EventHandler(this.button_Copy_Enter);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(674, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(10, 45);
		this.panel5.TabIndex = 43;
		this.button_EditSelected.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_EditSelected.AutoEllipsis = true;
		this.button_EditSelected.BackColor = System.Drawing.Color.Transparent;
		this.button_EditSelected.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_EditSelected.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_EditSelected.ColorOn = System.Drawing.Color.Orange;
		this.button_EditSelected.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_EditSelected.ColorPress = System.Drawing.Color.Orange;
		this.button_EditSelected.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_EditSelected.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_EditSelected.Dock = System.Windows.Forms.DockStyle.Left;
		this.button_EditSelected.FadingSpeed = 35;
		this.button_EditSelected.FlatAppearance.BorderSize = 0;
		this.button_EditSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_EditSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_EditSelected.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_EditSelected.Image = EzyPlan_Interface_V2.Properties.Resources.Edit_Record;
		this.button_EditSelected.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_EditSelected.ImageOffset = 4;
		this.button_EditSelected.IsPressed = false;
		this.button_EditSelected.KeepPress = false;
		this.button_EditSelected.Location = new System.Drawing.Point(464, 0);
		this.button_EditSelected.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_EditSelected.MenuPos = new System.Drawing.Point(0, 0);
		this.button_EditSelected.Name = "button_EditSelected";
		this.button_EditSelected.Radius = 10;
		this.button_EditSelected.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_EditSelected.Size = new System.Drawing.Size(210, 45);
		this.button_EditSelected.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_EditSelected.SplitDistance = 0;
		this.button_EditSelected.TabIndex = 2;
		this.button_EditSelected.Text = "Edit Selected";
		this.button_EditSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_EditSelected.Title = "";
		this.button_EditSelected.UseVisualStyleBackColor = true;
		this.button_EditSelected.Click += new System.EventHandler(this.button_EditSelected_Click);
		this.button_EditSelected.Leave += new System.EventHandler(this.button_EditSelected_Leave);
		this.button_EditSelected.Enter += new System.EventHandler(this.button_EditSelected_Enter);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel2.Location = new System.Drawing.Point(454, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 45);
		this.panel2.TabIndex = 40;
		this.buttonRemoveSelected.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.buttonRemoveSelected.AutoEllipsis = true;
		this.buttonRemoveSelected.BackColor = System.Drawing.Color.Transparent;
		this.buttonRemoveSelected.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.buttonRemoveSelected.ColorBaseStroke = System.Drawing.Color.Silver;
		this.buttonRemoveSelected.ColorOn = System.Drawing.Color.Orange;
		this.buttonRemoveSelected.ColorOnStroke = System.Drawing.Color.Silver;
		this.buttonRemoveSelected.ColorPress = System.Drawing.Color.Orange;
		this.buttonRemoveSelected.ColorPressStroke = System.Drawing.Color.Silver;
		this.buttonRemoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
		this.buttonRemoveSelected.Dock = System.Windows.Forms.DockStyle.Left;
		this.buttonRemoveSelected.FadingSpeed = 35;
		this.buttonRemoveSelected.FlatAppearance.BorderSize = 0;
		this.buttonRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.buttonRemoveSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.buttonRemoveSelected.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.buttonRemoveSelected.Image = EzyPlan_Interface_V2.Properties.Resources.Delete_Record;
		this.buttonRemoveSelected.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.buttonRemoveSelected.ImageOffset = 4;
		this.buttonRemoveSelected.IsPressed = false;
		this.buttonRemoveSelected.KeepPress = false;
		this.buttonRemoveSelected.Location = new System.Drawing.Point(244, 0);
		this.buttonRemoveSelected.MaxImageSize = new System.Drawing.Point(40, 40);
		this.buttonRemoveSelected.MenuPos = new System.Drawing.Point(0, 0);
		this.buttonRemoveSelected.Name = "buttonRemoveSelected";
		this.buttonRemoveSelected.Radius = 10;
		this.buttonRemoveSelected.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.buttonRemoveSelected.Size = new System.Drawing.Size(210, 45);
		this.buttonRemoveSelected.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.buttonRemoveSelected.SplitDistance = 0;
		this.buttonRemoveSelected.TabIndex = 1;
		this.buttonRemoveSelected.Text = "Remove Selected";
		this.buttonRemoveSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.buttonRemoveSelected.Title = "";
		this.buttonRemoveSelected.UseVisualStyleBackColor = true;
		this.buttonRemoveSelected.Click += new System.EventHandler(this.button_RemoveSelected_Click);
		this.buttonRemoveSelected.Leave += new System.EventHandler(this.button_DeleteSelected_Leave);
		this.buttonRemoveSelected.Enter += new System.EventHandler(this.button_DeleteSelected_Enter);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(234, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(10, 45);
		this.panel3.TabIndex = 41;
		this.button_AddNew.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
		this.button_AddNew.AutoEllipsis = true;
		this.button_AddNew.BackColor = System.Drawing.Color.Transparent;
		this.button_AddNew.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AddNew.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AddNew.ColorOn = System.Drawing.Color.Orange;
		this.button_AddNew.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_AddNew.ColorPress = System.Drawing.Color.Orange;
		this.button_AddNew.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AddNew.ContextMenuStrip = this.contextMenuStrip_AddOptions;
		this.button_AddNew.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AddNew.Dock = System.Windows.Forms.DockStyle.Left;
		this.button_AddNew.FadingSpeed = 35;
		this.button_AddNew.FlatAppearance.BorderSize = 0;
		this.button_AddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AddNew.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AddNew.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.button_AddNew.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AddNew.ImageOffset = 4;
		this.button_AddNew.IsPressed = false;
		this.button_AddNew.KeepPress = false;
		this.button_AddNew.Location = new System.Drawing.Point(0, 0);
		this.button_AddNew.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AddNew.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AddNew.Name = "button_AddNew";
		this.button_AddNew.Radius = 10;
		this.button_AddNew.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AddNew.Size = new System.Drawing.Size(234, 45);
		this.button_AddNew.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
		this.button_AddNew.SplitDistance = 50;
		this.button_AddNew.TabIndex = 0;
		this.button_AddNew.Text = "Add New Survey(s)";
		this.button_AddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AddNew.Title = "";
		this.button_AddNew.UseVisualStyleBackColor = true;
		this.button_AddNew.Click += new System.EventHandler(this.button_AddNew_Click);
		this.button_AddNew.Leave += new System.EventHandler(this.button_AddNew_Leave);
		this.button_AddNew.Enter += new System.EventHandler(this.button_AddNew_Enter);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 483);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(993, 10);
		this.panel4.TabIndex = 1;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel7.Location = new System.Drawing.Point(164, 5);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(10, 45);
		this.panel7.TabIndex = 2;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel8.Location = new System.Drawing.Point(322, 5);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(10, 45);
		this.panel8.TabIndex = 4;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(19, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(132, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "Severe Dogleg:";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(9, 36);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(142, 20);
		this.label2.TabIndex = 3;
		this.label2.Text = "Extreme Dogleg:";
		this.panel_SevereDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_SevereDL.Location = new System.Drawing.Point(157, 3);
		this.panel_SevereDL.Name = "panel_SevereDL";
		this.panel_SevereDL.Size = new System.Drawing.Size(67, 27);
		this.panel_SevereDL.TabIndex = 4;
		this.panel_ExtremeDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_ExtremeDL.Location = new System.Drawing.Point(157, 36);
		this.panel_ExtremeDL.Name = "panel_ExtremeDL";
		this.panel_ExtremeDL.Size = new System.Drawing.Size(67, 27);
		this.panel_ExtremeDL.TabIndex = 4;
		this.panel_Key.Controls.Add(this.panel6);
		this.panel_Key.Controls.Add(this.label1);
		this.panel_Key.Controls.Add(this.label2);
		this.panel_Key.Controls.Add(this.panel_ExtremeDL);
		this.panel_Key.Controls.Add(this.panel_SevereDL);
		this.panel_Key.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel_Key.Location = new System.Drawing.Point(717, 0);
		this.panel_Key.Name = "panel_Key";
		this.panel_Key.Size = new System.Drawing.Size(298, 70);
		this.panel_Key.TabIndex = 2;
		this.panel6.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Edit_Record;
		this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Location = new System.Drawing.Point(234, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(64, 70);
		this.panel6.TabIndex = 5;
		this.button_Save.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Save.AutoEllipsis = true;
		this.button_Save.BackColor = System.Drawing.Color.Transparent;
		this.button_Save.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Save.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Save.ColorOn = System.Drawing.Color.Orange;
		this.button_Save.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Save.ColorPress = System.Drawing.Color.Orange;
		this.button_Save.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Save.Dock = System.Windows.Forms.DockStyle.Right;
		this.button_Save.FadingSpeed = 35;
		this.button_Save.FlatAppearance.BorderSize = 0;
		this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Save.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Save.Image = EzyPlan_Interface_V2.Properties.Resources.Save;
		this.button_Save.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Save.ImageOffset = 4;
		this.button_Save.IsPressed = false;
		this.button_Save.KeepPress = false;
		this.button_Save.Location = new System.Drawing.Point(16, 5);
		this.button_Save.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Save.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Save.Name = "button_Save";
		this.button_Save.Radius = 10;
		this.button_Save.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Save.Size = new System.Drawing.Size(148, 45);
		this.button_Save.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Save.SplitDistance = 0;
		this.button_Save.TabIndex = 0;
		this.button_Save.Text = "Save";
		this.button_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Save.Title = "";
		this.button_Save.UseVisualStyleBackColor = true;
		this.button_Save.Visible = false;
		this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
		this.button_Save.Leave += new System.EventHandler(this.button_Save_Leave);
		this.button_Save.Enter += new System.EventHandler(this.button_Save_Enter);
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
		this.button_Back.Dock = System.Windows.Forms.DockStyle.Right;
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
		this.button_Back.Location = new System.Drawing.Point(174, 5);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(148, 45);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 3;
		this.button_Back.Text = "Back";
		this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Visible = false;
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
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
		this.button_Cancel.Dock = System.Windows.Forms.DockStyle.Right;
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
		this.button_Cancel.Location = new System.Drawing.Point(332, 5);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 25);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(581, 20);
		this.label3.TabIndex = 3;
		this.label3.Text = "Dip can be entered as mineral or oil and gas standard but is stored as oil and gas.";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(3, 45);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(607, 20);
		this.label4.TabIndex = 3;
		this.label4.Text = "The Azimuth values must be relative to grid north. (Corrected for magnetic delination).";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Name = "Body_EditSurveyData";
		base.Size = new System.Drawing.Size(1015, 698);
		base.Controls.SetChildIndex(base.panel_header, 0);
		base.Controls.SetChildIndex(base.panel_footer, 0);
		base.Controls.SetChildIndex(base.panel_body, 0);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.contextMenuStrip_EditOptions.ResumeLayout(false);
		this.contextMenuStrip_AddOptions.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel_Key.ResumeLayout(false);
		this.panel_Key.PerformLayout();
		base.ResumeLayout(false);
	}
}
