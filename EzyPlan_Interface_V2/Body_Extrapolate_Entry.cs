using System;
using System.Collections;
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

public class Body_Extrapolate_Entry : Layout_Body_Wizard
{
	public enum AddButtonStates
	{
		ExtrapolateToMD,
		ExtrapolateToTVD,
		ExtrapolateToDirection,
		ExtrapolateToDip
	}

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Next,
			Cancel
		}

		private ButtonPressedEnum m_buttonPressed;

		private ExtrapolatedSurveyList m_extrapolated;

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

		public ExtrapolatedSurveyList Extrapolated
		{
			get
			{
				return m_extrapolated;
			}
			set
			{
				m_extrapolated = value;
			}
		}
	}

	private DrillHoleFile m_drillHoleFile;

	private ExtrapolatedSurveyList m_extrapolatedList;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private AddButtonStates m_addButtonState;

	private Popup_Extrapolate_ToMD m_extrapolateToMD;

	private Popup_Extrapolate_ToTVD m_extrapolateToTVD;

	private Popup_Extrapolate_ToDirection m_extrapolateToDirection;

	private Popup_Extrapolate_ToInclination m_extrapolateToInclination;

	private IContainer components;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Add;

	private ContextMenuStrip contextMenuStrip_AddOptions;

	private ToolStripMenuItem menuButton_Add_MD;

	private GroupBox groupBox_LastSurvey;

	private TextBox textBox_LastSurvey_Easting;

	private TextBox textBox_LastSurvey_Northing;

	private TextBox textBox_LastSurvey_TVD;

	private TextBox textBox_LastSurvey_Azimuth;

	private TextBox textBox_LastSurvey_Dip;

	private TextBox textBox_LastSurvey_MD;

	private Label label_Easting;

	private Label label_Northing;

	private Label label_TVD;

	private Label label_Azimuth;

	private Label label_Dip;

	private Label label_MD;

	private RibbonMenuButton button_Remove;

	private ContextMenuStrip contextMenuStrip_DataGrid;

	private ToolStripMenuItem appendMDToolStripMenuItem;

	private ToolStripMenuItem removeLastExtrapolatedSurveyToolStripMenuItem;

	private DataGridView dataGridView1;

	private RibbonMenuButton button_ClearList;

	private ToolStripMenuItem clearListToolStripMenuItem;

	private Panel panel5;

	private GroupBox groupBox_options;

	private ToolStripMenuItem appendExtrapolationToTrueVerivalDepthToolStripMenuItem;

	private ToolStripMenuItem appendExtrapolationToDirectionToolStripMenuItem;

	private ToolStripMenuItem appendExtrapolationToInclinationToolStripMenuItem;

	private ToolStripMenuItem appendExtrapolationToTrueVerticalDepthToolStripMenuItem;

	private ToolStripMenuItem appendExtrapolationToDirectionToolStripMenuItem1;

	private ToolStripMenuItem appendExtrapolationToInclinationToolStripMenuItem1;

	private ToolStripMenuItem copySelectedToolStripMenuItem;

	private ToolStripMenuItem selectAllToolStripMenuItem;

	public AddButtonStates AddButtonState
	{
		get
		{
			return m_addButtonState;
		}
		set
		{
			SetAddButtonState(value);
		}
	}

	public event EventHandler ButtonPressed;

	public Body_Extrapolate_Entry(ref DrillHoleFile drillHoleFile)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		AddButtonState = AddButtonStates.ExtrapolateToMD;
		m_drillHoleFile = drillHoleFile;
	}

	public void CalculateAbsoluteSurvey(DrillHoleFile drillHole, out AbsoluteSurveyList absoluteList)
	{
		NavMath navMath = new NavMath();
		absoluteList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(drillHole.Collar, drillHole.SurveyList, ref absoluteList);
	}

	private void SetAddButtonState(AddButtonStates state)
	{
		switch (state)
		{
		case AddButtonStates.ExtrapolateToMD:
			button_Add.Text = "Append - Extrapolate to MD";
			m_addButtonState = state;
			break;
		case AddButtonStates.ExtrapolateToTVD:
			button_Add.Text = "Append - Extrapolate to TVD";
			m_addButtonState = state;
			break;
		case AddButtonStates.ExtrapolateToDirection:
			button_Add.Text = "Append - Extrapolate to Direction";
			m_addButtonState = state;
			break;
		case AddButtonStates.ExtrapolateToDip:
			button_Add.Text = "Append - Extrapolate to Dip";
			m_addButtonState = state;
			break;
		}
	}

	private void button_Add_Click(object sender, EventArgs e)
	{
		switch (m_addButtonState)
		{
		case AddButtonStates.ExtrapolateToMD:
			AddExtrapolateToMD();
			break;
		case AddButtonStates.ExtrapolateToTVD:
			AddExtrapolateToTVD();
			break;
		case AddButtonStates.ExtrapolateToDirection:
			AddExtrapolateToDirection();
			break;
		case AddButtonStates.ExtrapolateToDip:
			AddExtrapolateToInclination();
			break;
		default:
			AddExtrapolateToMD();
			break;
		}
	}

	public void AddExtrapolateToMD()
	{
		m_extrapolateToMD = new Popup_Extrapolate_ToMD(m_extrapolatedList.GetLastSurvey());
		m_extrapolateToMD.ExtrapolatedSurveyCreated += ExtrapolatedSurveyCreated;
		SelectLastRow();
		m_extrapolateToMD.ShowDialog();
	}

	private void AddExtrapolateToTVD()
	{
		m_extrapolateToTVD = new Popup_Extrapolate_ToTVD(m_extrapolatedList.GetLastSurvey());
		m_extrapolateToTVD.ExtrapolatedSurveyCreated += ExtrapolatedSurveyCreated;
		SelectLastRow();
		m_extrapolateToTVD.ShowDialog();
	}

	private void AddExtrapolateToDirection()
	{
		m_extrapolateToDirection = new Popup_Extrapolate_ToDirection(m_extrapolatedList.GetLastSurvey());
		m_extrapolateToDirection.ExtrapolatedSurveyCreated += ExtrapolatedSurveyCreated;
		SelectLastRow();
		m_extrapolateToDirection.ShowDialog();
	}

	private void AddExtrapolateToInclination()
	{
		m_extrapolateToInclination = new Popup_Extrapolate_ToInclination(m_extrapolatedList.GetLastSurvey());
		m_extrapolateToInclination.ExtrapolatedSurveyCreated += ExtrapolatedSurveyCreated;
		SelectLastRow();
		m_extrapolateToInclination.ShowDialog();
	}

	private void ExtrapolatedSurveyCreated(object sender, EventArgs e)
	{
		Popup_Extrapolate.ExtrapolatedSurveyCreatedEventArgs e2 = (Popup_Extrapolate.ExtrapolatedSurveyCreatedEventArgs)e;
		if (e2.LastExtrapolatedSurvey != null)
		{
			AddExtrapolatedSurveyData(e2.LastExtrapolatedSurvey);
		}
	}

	private bool AddExtrapolatedSurveyData(ExtrapolatedAbsoluteSurvey extrapolatedSurvey)
	{
		if (m_extrapolatedList.Append(extrapolatedSurvey) != ExtrapolatedSurveyList.AppendEnum.Appended)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not append extrapolated survey!", "The MD of the extrapolated survey is not greater than the MD of the last survey.");
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
		SelectLastRow();
		return true;
	}

	public void Remove()
	{
		m_extrapolatedList.Remove();
		SelectLastRow();
	}

	private void ClearExtrapolatedList()
	{
		m_extrapolatedList.List.Clear();
	}

	public void Display(bool reset)
	{
		if (reset | (m_extrapolatedList == null))
		{
			CalculateAbsoluteSurvey(m_drillHoleFile, out var absoluteList);
			m_extrapolatedList = new ExtrapolatedSurveyList(absoluteList.LastRecord());
			UpdateLastSurvey(m_extrapolatedList.LastRealSurvey);
			dataGridView1.DataSource = m_extrapolatedList.List;
			dataGridView1.Columns[dataGridView1.Columns.Count - 1].FillWeight = 150f;
			dataGridView1.Refresh();
			m_extremeDL = Settings.Default.extremeDL;
			m_severeDL = Settings.Default.severeDL;
			Focus();
		}
		else
		{
			Focus();
		}
	}

	public void RemoveFromDisplay()
	{
	}

	public void UpdateLastSurvey(AbsoluteSurvey lastSurvey)
	{
		if (lastSurvey != null)
		{
			textBox_LastSurvey_MD.Text = lastSurvey.MD.ToString();
			textBox_LastSurvey_Dip.Text = lastSurvey.Dip.ToString();
			textBox_LastSurvey_Azimuth.Text = lastSurvey.Azimuth.ToString();
			textBox_LastSurvey_TVD.Text = lastSurvey.TVD.ToString();
			textBox_LastSurvey_Northing.Text = lastSurvey.Northing.ToString();
			textBox_LastSurvey_Easting.Text = lastSurvey.Easting.ToString();
		}
	}

	public void UpdateGrid()
	{
		dataGridView1.Refresh();
	}

	private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		ColourCodeRows();
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

	public void ColourCodeRows()
	{
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			Utility.StringToDecimal(item.Cells[6].Value.ToString(), out var number);
			if (number >= m_extremeDL)
			{
				item.DefaultCellStyle.BackColor = Color.Red;
			}
			else if (number >= m_severeDL)
			{
				item.DefaultCellStyle.BackColor = Color.Orange;
			}
		}
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

	private void OnNext()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Next;
		e.Extrapolated = m_extrapolatedList;
		OnButtonPressed(e);
	}

	private void OnCancel()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Cancel;
		OnButtonPressed(e);
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		OnNext();
	}

	private void Cancel_Click(object sender, EventArgs e)
	{
		OnCancel();
	}

	private void ExtrapolateToMD_Click(object sender, EventArgs e)
	{
		AddButtonState = AddButtonStates.ExtrapolateToMD;
		AddExtrapolateToMD();
	}

	private void ExtrapolateToTVD_Click(object sender, EventArgs e)
	{
		AddButtonState = AddButtonStates.ExtrapolateToTVD;
		AddExtrapolateToTVD();
	}

	private void ExtrapolateToDirection_Click(object sender, EventArgs e)
	{
		AddButtonState = AddButtonStates.ExtrapolateToDirection;
		AddExtrapolateToDirection();
	}

	private void ExtrapolateToInclination_Click(object sender, EventArgs e)
	{
		AddButtonState = AddButtonStates.ExtrapolateToDip;
		AddExtrapolateToInclination();
	}

	private void RemoveLast_Click(object sender, EventArgs e)
	{
		Remove();
	}

	private void ClearList_Click(object sender, EventArgs e)
	{
		ClearExtrapolatedList();
	}

	private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SelectAllSurveys();
	}

	private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		CopySelectedToClipboard();
	}

	private void button_Add_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Add);
	}

	private void button_Add_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Add);
	}

	private void button_Remove_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Remove);
	}

	private void button_Remove_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Remove);
	}

	private void button_ClearList_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_ClearList);
	}

	private void button_ClearList_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_ClearList);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
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
		this.contextMenuStrip_AddOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.menuButton_Add_MD = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToDirectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToInclinationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox_LastSurvey = new System.Windows.Forms.GroupBox();
		this.textBox_LastSurvey_Easting = new System.Windows.Forms.TextBox();
		this.textBox_LastSurvey_Northing = new System.Windows.Forms.TextBox();
		this.textBox_LastSurvey_TVD = new System.Windows.Forms.TextBox();
		this.textBox_LastSurvey_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_LastSurvey_Dip = new System.Windows.Forms.TextBox();
		this.textBox_LastSurvey_MD = new System.Windows.Forms.TextBox();
		this.label_Easting = new System.Windows.Forms.Label();
		this.label_Northing = new System.Windows.Forms.Label();
		this.label_TVD = new System.Windows.Forms.Label();
		this.label_Azimuth = new System.Windows.Forms.Label();
		this.label_Dip = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		this.contextMenuStrip_DataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.appendMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.appendExtrapolationToInclinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.removeLastExtrapolatedSurveyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.groupBox_options = new System.Windows.Forms.GroupBox();
		this.button_ClearList = new RibbonStyle.RibbonMenuButton();
		this.button_Remove = new RibbonStyle.RibbonMenuButton();
		this.button_Add = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.contextMenuStrip_AddOptions.SuspendLayout();
		this.groupBox_LastSurvey.SuspendLayout();
		this.contextMenuStrip_DataGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox_options.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Dock = System.Windows.Forms.DockStyle.Top;
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Location = new System.Drawing.Point(0, 0);
		base.label_headerText1.Size = new System.Drawing.Size(293, 24);
		base.label_headerText1.Text = "Append extrapolated survey's:";
		base.groupBox_frame.Size = new System.Drawing.Size(1004, 578);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Location = new System.Drawing.Point(683, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(337, 57);
		base.panel_buttonPanel.TabIndex = 3;
		base.panel_body.Location = new System.Drawing.Point(0, 70);
		base.panel_body.Size = new System.Drawing.Size(1020, 594);
		base.panel_header.AutoSize = true;
		base.panel_header.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(1020, 70);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 664);
		base.panel_footer.Size = new System.Drawing.Size(1020, 57);
		base.label_headerText2.AutoSize = false;
		base.label_headerText2.Dock = System.Windows.Forms.DockStyle.Top;
		base.label_headerText2.Location = new System.Drawing.Point(0, 24);
		base.label_headerText2.Size = new System.Drawing.Size(956, 46);
		base.label_headerText2.Text = "Append extrapolated surveys to the list and click Next to see the results. Click on the right part of the \"Append\" button to select a different type of extrapolation.";
		base.panel_scrollPanel.Controls.Add(this.dataGridView1);
		base.panel_scrollPanel.Controls.Add(this.groupBox_LastSurvey);
		base.panel_scrollPanel.Controls.Add(this.groupBox_options);
		base.panel_scrollPanel.Size = new System.Drawing.Size(998, 559);
		this.contextMenuStrip_AddOptions.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_AddOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.menuButton_Add_MD, this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem, this.appendExtrapolationToDirectionToolStripMenuItem1, this.appendExtrapolationToInclinationToolStripMenuItem1 });
		this.contextMenuStrip_AddOptions.Name = "contextMenuStrip_EditOptions";
		this.contextMenuStrip_AddOptions.Size = new System.Drawing.Size(483, 140);
		this.menuButton_Add_MD.Name = "menuButton_Add_MD";
		this.menuButton_Add_MD.Size = new System.Drawing.Size(482, 34);
		this.menuButton_Add_MD.Text = "Append - Extrapolation to MD";
		this.menuButton_Add_MD.Click += new System.EventHandler(this.ExtrapolateToMD_Click);
		this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem.Name = "appendExtrapolationToTrueVerticalDepthToolStripMenuItem";
		this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem.Size = new System.Drawing.Size(482, 34);
		this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem.Text = "Append - Extrapolation to TVD";
		this.appendExtrapolationToTrueVerticalDepthToolStripMenuItem.Click += new System.EventHandler(this.ExtrapolateToTVD_Click);
		this.appendExtrapolationToDirectionToolStripMenuItem1.Name = "appendExtrapolationToDirectionToolStripMenuItem1";
		this.appendExtrapolationToDirectionToolStripMenuItem1.Size = new System.Drawing.Size(482, 34);
		this.appendExtrapolationToDirectionToolStripMenuItem1.Text = "Append - Extrapolation to Direction";
		this.appendExtrapolationToDirectionToolStripMenuItem1.Click += new System.EventHandler(this.ExtrapolateToDirection_Click);
		this.appendExtrapolationToInclinationToolStripMenuItem1.Name = "appendExtrapolationToInclinationToolStripMenuItem1";
		this.appendExtrapolationToInclinationToolStripMenuItem1.Size = new System.Drawing.Size(482, 34);
		this.appendExtrapolationToInclinationToolStripMenuItem1.Text = "Append - Extrapolation to Dip";
		this.appendExtrapolationToInclinationToolStripMenuItem1.Click += new System.EventHandler(this.ExtrapolateToInclination_Click);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_Easting);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_Northing);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_TVD);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_Azimuth);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_Dip);
		this.groupBox_LastSurvey.Controls.Add(this.textBox_LastSurvey_MD);
		this.groupBox_LastSurvey.Controls.Add(this.label_Easting);
		this.groupBox_LastSurvey.Controls.Add(this.label_Northing);
		this.groupBox_LastSurvey.Controls.Add(this.label_TVD);
		this.groupBox_LastSurvey.Controls.Add(this.label_Azimuth);
		this.groupBox_LastSurvey.Controls.Add(this.label_Dip);
		this.groupBox_LastSurvey.Controls.Add(this.label_MD);
		this.groupBox_LastSurvey.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_LastSurvey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_LastSurvey.Location = new System.Drawing.Point(0, 0);
		this.groupBox_LastSurvey.Name = "groupBox_LastSurvey";
		this.groupBox_LastSurvey.Size = new System.Drawing.Size(998, 92);
		this.groupBox_LastSurvey.TabIndex = 3;
		this.groupBox_LastSurvey.TabStop = false;
		this.groupBox_LastSurvey.Text = "Last Real Survey";
		this.textBox_LastSurvey_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Easting.Location = new System.Drawing.Point(630, 56);
		this.textBox_LastSurvey_Easting.Name = "textBox_LastSurvey_Easting";
		this.textBox_LastSurvey_Easting.ReadOnly = true;
		this.textBox_LastSurvey_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_LastSurvey_Easting.TabIndex = 1;
		this.textBox_LastSurvey_Easting.TabStop = false;
		this.textBox_LastSurvey_Easting.Text = "100000.50";
		this.textBox_LastSurvey_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Northing.Location = new System.Drawing.Point(486, 56);
		this.textBox_LastSurvey_Northing.Name = "textBox_LastSurvey_Northing";
		this.textBox_LastSurvey_Northing.ReadOnly = true;
		this.textBox_LastSurvey_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_LastSurvey_Northing.TabIndex = 1;
		this.textBox_LastSurvey_Northing.TabStop = false;
		this.textBox_LastSurvey_Northing.Text = "10000000.50";
		this.textBox_LastSurvey_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_TVD.Location = new System.Drawing.Point(368, 56);
		this.textBox_LastSurvey_TVD.Name = "textBox_LastSurvey_TVD";
		this.textBox_LastSurvey_TVD.ReadOnly = true;
		this.textBox_LastSurvey_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_LastSurvey_TVD.TabIndex = 1;
		this.textBox_LastSurvey_TVD.TabStop = false;
		this.textBox_LastSurvey_TVD.Text = "1000.50";
		this.textBox_LastSurvey_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Azimuth.Location = new System.Drawing.Point(224, 56);
		this.textBox_LastSurvey_Azimuth.Name = "textBox_LastSurvey_Azimuth";
		this.textBox_LastSurvey_Azimuth.ReadOnly = true;
		this.textBox_LastSurvey_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_LastSurvey_Azimuth.TabIndex = 1;
		this.textBox_LastSurvey_Azimuth.TabStop = false;
		this.textBox_LastSurvey_Azimuth.Text = "360.50";
		this.textBox_LastSurvey_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Dip.Location = new System.Drawing.Point(122, 56);
		this.textBox_LastSurvey_Dip.Name = "textBox_LastSurvey_Dip";
		this.textBox_LastSurvey_Dip.ReadOnly = true;
		this.textBox_LastSurvey_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_LastSurvey_Dip.TabIndex = 1;
		this.textBox_LastSurvey_Dip.TabStop = false;
		this.textBox_LastSurvey_Dip.Text = "90.50";
		this.textBox_LastSurvey_MD.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_LastSurvey_MD.Name = "textBox_LastSurvey_MD";
		this.textBox_LastSurvey_MD.ReadOnly = true;
		this.textBox_LastSurvey_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_LastSurvey_MD.TabIndex = 1;
		this.textBox_LastSurvey_MD.TabStop = false;
		this.textBox_LastSurvey_MD.Text = "10000.50";
		this.label_Easting.AutoSize = true;
		this.label_Easting.Location = new System.Drawing.Point(626, 29);
		this.label_Easting.Name = "label_Easting";
		this.label_Easting.Size = new System.Drawing.Size(72, 24);
		this.label_Easting.TabIndex = 0;
		this.label_Easting.Text = "Easting";
		this.label_Northing.AutoSize = true;
		this.label_Northing.Location = new System.Drawing.Point(482, 29);
		this.label_Northing.Name = "label_Northing";
		this.label_Northing.Size = new System.Drawing.Size(82, 24);
		this.label_Northing.TabIndex = 0;
		this.label_Northing.Text = "Northing";
		this.label_TVD.AutoSize = true;
		this.label_TVD.Location = new System.Drawing.Point(364, 29);
		this.label_TVD.Name = "label_TVD";
		this.label_TVD.Size = new System.Drawing.Size(48, 24);
		this.label_TVD.TabIndex = 0;
		this.label_TVD.Text = "TVD";
		this.label_Azimuth.AutoSize = true;
		this.label_Azimuth.Location = new System.Drawing.Point(220, 29);
		this.label_Azimuth.Name = "label_Azimuth";
		this.label_Azimuth.Size = new System.Drawing.Size(78, 24);
		this.label_Azimuth.TabIndex = 0;
		this.label_Azimuth.Text = "Azimuth";
		this.label_Dip.AutoSize = true;
		this.label_Dip.Location = new System.Drawing.Point(118, 29);
		this.label_Dip.Name = "label_Dip";
		this.label_Dip.Size = new System.Drawing.Size(38, 24);
		this.label_Dip.TabIndex = 0;
		this.label_Dip.Text = "Dip";
		this.label_MD.AutoSize = true;
		this.label_MD.Location = new System.Drawing.Point(7, 29);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(39, 24);
		this.label_MD.TabIndex = 0;
		this.label_MD.Text = "MD";
		this.contextMenuStrip_DataGrid.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_DataGrid.ImageScalingSize = new System.Drawing.Size(24, 24);
		this.contextMenuStrip_DataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.appendMDToolStripMenuItem, this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem, this.appendExtrapolationToDirectionToolStripMenuItem, this.appendExtrapolationToInclinationToolStripMenuItem, this.removeLastExtrapolatedSurveyToolStripMenuItem, this.copySelectedToolStripMenuItem, this.selectAllToolStripMenuItem, this.clearListToolStripMenuItem });
		this.contextMenuStrip_DataGrid.Name = "contextMenuStrip_DataGrid";
		this.contextMenuStrip_DataGrid.Size = new System.Drawing.Size(478, 276);
		this.appendMDToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.appendMDToolStripMenuItem.Name = "appendMDToolStripMenuItem";
		this.appendMDToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.appendMDToolStripMenuItem.Text = "Append Extrapolation to MD";
		this.appendMDToolStripMenuItem.Click += new System.EventHandler(this.ExtrapolateToMD_Click);
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem.Name = "appendExtrapolationToTrueVerivalDepthToolStripMenuItem";
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem.Text = "Append Extrapolation to TVD";
		this.appendExtrapolationToTrueVerivalDepthToolStripMenuItem.Click += new System.EventHandler(this.ExtrapolateToTVD_Click);
		this.appendExtrapolationToDirectionToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.appendExtrapolationToDirectionToolStripMenuItem.Name = "appendExtrapolationToDirectionToolStripMenuItem";
		this.appendExtrapolationToDirectionToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.appendExtrapolationToDirectionToolStripMenuItem.Text = "Append Extrapolation to Direction";
		this.appendExtrapolationToDirectionToolStripMenuItem.Click += new System.EventHandler(this.ExtrapolateToDirection_Click);
		this.appendExtrapolationToInclinationToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.appendExtrapolationToInclinationToolStripMenuItem.Name = "appendExtrapolationToInclinationToolStripMenuItem";
		this.appendExtrapolationToInclinationToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.appendExtrapolationToInclinationToolStripMenuItem.Text = "Append Extrapolation to Dip";
		this.appendExtrapolationToInclinationToolStripMenuItem.Click += new System.EventHandler(this.ExtrapolateToInclination_Click);
		this.removeLastExtrapolatedSurveyToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Delete_Record;
		this.removeLastExtrapolatedSurveyToolStripMenuItem.Name = "removeLastExtrapolatedSurveyToolStripMenuItem";
		this.removeLastExtrapolatedSurveyToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.removeLastExtrapolatedSurveyToolStripMenuItem.Text = "Remove Last Extrapolated Survey";
		this.removeLastExtrapolatedSurveyToolStripMenuItem.Click += new System.EventHandler(this.RemoveLast_Click);
		this.copySelectedToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Copy;
		this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
		this.copySelectedToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.copySelectedToolStripMenuItem.Text = "Copy Selected To Clipboard";
		this.copySelectedToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToolStripMenuItem_Click);
		this.selectAllToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Select_All;
		this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
		this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.selectAllToolStripMenuItem.Text = "Select All";
		this.clearListToolStripMenuItem.Image = EzyPlan_Interface_V2.Properties.Resources.Refresh_Document;
		this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
		this.clearListToolStripMenuItem.Size = new System.Drawing.Size(477, 34);
		this.clearListToolStripMenuItem.Text = "Clear All";
		this.clearListToolStripMenuItem.Click += new System.EventHandler(this.ClearList_Click);
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
		this.dataGridView1.ContextMenuStrip = this.contextMenuStrip_DataGrid;
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
		this.dataGridView1.Location = new System.Drawing.Point(0, 92);
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
		this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.ShowCellErrors = false;
		this.dataGridView1.ShowCellToolTips = false;
		this.dataGridView1.ShowEditingIcon = false;
		this.dataGridView1.ShowRowErrors = false;
		this.dataGridView1.Size = new System.Drawing.Size(998, 394);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 0;
		this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
		this.groupBox_options.Controls.Add(this.button_ClearList);
		this.groupBox_options.Controls.Add(this.button_Remove);
		this.groupBox_options.Controls.Add(this.button_Add);
		this.groupBox_options.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox_options.Location = new System.Drawing.Point(0, 486);
		this.groupBox_options.Name = "groupBox_options";
		this.groupBox_options.Size = new System.Drawing.Size(998, 73);
		this.groupBox_options.TabIndex = 4;
		this.groupBox_options.TabStop = false;
		this.button_ClearList.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_ClearList.AutoEllipsis = true;
		this.button_ClearList.BackColor = System.Drawing.Color.Transparent;
		this.button_ClearList.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_ClearList.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_ClearList.ColorOn = System.Drawing.Color.Orange;
		this.button_ClearList.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_ClearList.ColorPress = System.Drawing.Color.Orange;
		this.button_ClearList.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_ClearList.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_ClearList.FadingSpeed = 35;
		this.button_ClearList.FlatAppearance.BorderSize = 0;
		this.button_ClearList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_ClearList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_ClearList.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_ClearList.Image = EzyPlan_Interface_V2.Properties.Resources.Refresh_Document;
		this.button_ClearList.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_ClearList.ImageOffset = 4;
		this.button_ClearList.IsPressed = false;
		this.button_ClearList.KeepPress = false;
		this.button_ClearList.Location = new System.Drawing.Point(694, 19);
		this.button_ClearList.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_ClearList.MenuPos = new System.Drawing.Point(0, 0);
		this.button_ClearList.Name = "button_ClearList";
		this.button_ClearList.Radius = 10;
		this.button_ClearList.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_ClearList.Size = new System.Drawing.Size(179, 45);
		this.button_ClearList.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_ClearList.SplitDistance = 80;
		this.button_ClearList.TabIndex = 2;
		this.button_ClearList.Text = "Clear All";
		this.button_ClearList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_ClearList.Title = "";
		this.button_ClearList.UseVisualStyleBackColor = true;
		this.button_ClearList.Click += new System.EventHandler(this.ClearList_Click);
		this.button_ClearList.Leave += new System.EventHandler(this.button_ClearList_Leave);
		this.button_ClearList.Enter += new System.EventHandler(this.button_ClearList_Enter);
		this.button_Remove.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Remove.AutoEllipsis = true;
		this.button_Remove.BackColor = System.Drawing.Color.Transparent;
		this.button_Remove.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Remove.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Remove.ColorOn = System.Drawing.Color.Orange;
		this.button_Remove.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Remove.ColorPress = System.Drawing.Color.Orange;
		this.button_Remove.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Remove.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Remove.FadingSpeed = 35;
		this.button_Remove.FlatAppearance.BorderSize = 0;
		this.button_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Remove.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Remove.Image = EzyPlan_Interface_V2.Properties.Resources.Delete_Record;
		this.button_Remove.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Remove.ImageOffset = 4;
		this.button_Remove.IsPressed = false;
		this.button_Remove.KeepPress = false;
		this.button_Remove.Location = new System.Drawing.Point(405, 19);
		this.button_Remove.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Remove.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Remove.Name = "button_Remove";
		this.button_Remove.Radius = 10;
		this.button_Remove.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Remove.Size = new System.Drawing.Size(283, 45);
		this.button_Remove.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Remove.SplitDistance = 80;
		this.button_Remove.TabIndex = 1;
		this.button_Remove.Text = "Remove Last Extrapolation";
		this.button_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Remove.Title = "";
		this.button_Remove.UseVisualStyleBackColor = true;
		this.button_Remove.Click += new System.EventHandler(this.RemoveLast_Click);
		this.button_Remove.Leave += new System.EventHandler(this.button_Remove_Leave);
		this.button_Remove.Enter += new System.EventHandler(this.button_Remove_Enter);
		this.button_Add.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
		this.button_Add.AutoEllipsis = true;
		this.button_Add.BackColor = System.Drawing.Color.Transparent;
		this.button_Add.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Add.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorOn = System.Drawing.Color.Orange;
		this.button_Add.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorPress = System.Drawing.Color.Orange;
		this.button_Add.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Add.ContextMenuStrip = this.contextMenuStrip_AddOptions;
		this.button_Add.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Add.FadingSpeed = 35;
		this.button_Add.FlatAppearance.BorderSize = 0;
		this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Add.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Add.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.button_Add.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Add.ImageOffset = 4;
		this.button_Add.IsPressed = false;
		this.button_Add.KeepPress = false;
		this.button_Add.Location = new System.Drawing.Point(6, 19);
		this.button_Add.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Add.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Add.Name = "button_Add";
		this.button_Add.Radius = 10;
		this.button_Add.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Add.Size = new System.Drawing.Size(380, 45);
		this.button_Add.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
		this.button_Add.SplitDistance = 32;
		this.button_Add.TabIndex = 0;
		this.button_Add.Text = "Append";
		this.button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Add.Title = "";
		this.button_Add.UseVisualStyleBackColor = true;
		this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
		this.button_Add.Leave += new System.EventHandler(this.button_Add_Leave);
		this.button_Add.Enter += new System.EventHandler(this.button_Add_Enter);
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Extrapolate;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(956, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 70);
		this.panel5.TabIndex = 4;
		this.button_Next.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Next.AutoEllipsis = true;
		this.button_Next.BackColor = System.Drawing.Color.Transparent;
		this.button_Next.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Next.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorOn = System.Drawing.Color.Orange;
		this.button_Next.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
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
		this.button_Next.ImageOffset = 0;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(10, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(150, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 0;
		this.button_Next.Text = "Next";
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Cancel.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Cancel.AutoEllipsis = true;
		this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
		this.button_Cancel.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Cancel.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorOn = System.Drawing.Color.Orange;
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
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
		this.button_Cancel.ImageOffset = 0;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(169, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(161, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_Extrapolate_Entry";
		base.Size = new System.Drawing.Size(1020, 721);
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
		this.contextMenuStrip_AddOptions.ResumeLayout(false);
		this.groupBox_LastSurvey.ResumeLayout(false);
		this.groupBox_LastSurvey.PerformLayout();
		this.contextMenuStrip_DataGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox_options.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
