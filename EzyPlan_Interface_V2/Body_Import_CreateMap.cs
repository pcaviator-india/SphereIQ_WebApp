using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using LumenWorks.Framework.IO.Csv;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_Import_CreateMap : Layout_Body_Wizard
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Save,
			Cancel
		}

		private FinishedStateEnum m_state;

		private ImportSurveyMapping m_mapping;

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

		public ImportSurveyMapping Mapping
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
	}

	private IContainer components;

	private GroupBox groupBox_data;

	private DataGridView dataGridView_SurveyData;

	private Panel panel_Mapping;

	private Panel panel2;

	private GroupBox groupBox_Options;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Save;

	private RibbonMenuButton button_Reset;

	private GroupBox groupBox_ColumnName;

	private ListBox listBox_Field;

	private GroupBox groupBox_SchemaName;

	private TextBox textBox_Name;

	private ContextMenuStrip contextMenuStrip_GridOptions;

	private ToolStripMenuItem mDToolStripMenuItem;

	private ToolStripMenuItem dipToolStripMenuItem;

	private ToolStripMenuItem azimuthToolStripMenuItem;

	private CheckBox checkBox_header;

	private Label label1;

	private Label label2;

	private Label label3;

	private Panel panel1;

	private bool m_headerPresent;

	private int m_mdIndex;

	private int m_dipIndex;

	private int m_azimuthIndex;

	private Color m_mdColour;

	private Color m_dipColour;

	private Color m_azimuthColour;

	public event EventHandler Finished;

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
		this.groupBox_data = new System.Windows.Forms.GroupBox();
		this.dataGridView_SurveyData = new System.Windows.Forms.DataGridView();
		this.contextMenuStrip_GridOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.azimuthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.panel_Mapping = new System.Windows.Forms.Panel();
		this.groupBox_ColumnName = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.checkBox_header = new System.Windows.Forms.CheckBox();
		this.listBox_Field = new System.Windows.Forms.ListBox();
		this.groupBox_SchemaName = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox_Name = new System.Windows.Forms.TextBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox_Options = new System.Windows.Forms.GroupBox();
		this.button_Reset = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Save = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_data.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView_SurveyData).BeginInit();
		this.contextMenuStrip_GridOptions.SuspendLayout();
		this.panel_Mapping.SuspendLayout();
		this.groupBox_ColumnName.SuspendLayout();
		this.groupBox_SchemaName.SuspendLayout();
		this.groupBox_Options.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Location = new System.Drawing.Point(6, 0);
		base.label_headerText1.Size = new System.Drawing.Size(249, 24);
		base.label_headerText1.Text = "Create a new import map:";
		base.groupBox_frame.Controls.Add(this.panel_Mapping);
		base.groupBox_frame.Size = new System.Drawing.Size(938, 487);
		base.groupBox_frame.Controls.SetChildIndex(this.panel_Mapping, 0);
		base.groupBox_frame.Controls.SetChildIndex(base.panel_scrollPanel, 0);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Save);
		base.panel_buttonPanel.Location = new System.Drawing.Point(637, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(315, 59);
		base.panel_body.Location = new System.Drawing.Point(0, 74);
		base.panel_body.Size = new System.Drawing.Size(952, 501);
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Size = new System.Drawing.Size(952, 74);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 575);
		base.panel_footer.Size = new System.Drawing.Size(952, 59);
		base.panel_footer.TabIndex = 1;
		base.label_headerText2.AutoSize = false;
		base.label_headerText2.Size = new System.Drawing.Size(879, 46);
		base.label_headerText2.Text = "Enter a name for the map, drag the required survey fields onto the columns in the CSV that contain the correct data, select whether or not the CSV file has header text and click save when done.";
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.groupBox_data);
		base.panel_scrollPanel.Location = new System.Drawing.Point(3, 230);
		base.panel_scrollPanel.Size = new System.Drawing.Size(932, 254);
		base.panel_scrollPanel.TabIndex = 1;
		this.groupBox_data.Controls.Add(this.dataGridView_SurveyData);
		this.groupBox_data.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_data.Location = new System.Drawing.Point(0, 0);
		this.groupBox_data.Name = "groupBox_data";
		this.groupBox_data.Size = new System.Drawing.Size(932, 254);
		this.groupBox_data.TabIndex = 29;
		this.groupBox_data.TabStop = false;
		this.groupBox_data.Text = "Survey Data:";
		this.dataGridView_SurveyData.AllowDrop = true;
		this.dataGridView_SurveyData.AllowUserToAddRows = false;
		this.dataGridView_SurveyData.AllowUserToDeleteRows = false;
		this.dataGridView_SurveyData.AllowUserToResizeColumns = false;
		this.dataGridView_SurveyData.AllowUserToResizeRows = false;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
		this.dataGridView_SurveyData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView_SurveyData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dataGridView_SurveyData.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView_SurveyData.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dataGridView_SurveyData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView_SurveyData.ContextMenuStrip = this.contextMenuStrip_GridOptions;
		this.dataGridView_SurveyData.Cursor = System.Windows.Forms.Cursors.Hand;
		this.dataGridView_SurveyData.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView_SurveyData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView_SurveyData.Location = new System.Drawing.Point(3, 25);
		this.dataGridView_SurveyData.MultiSelect = false;
		this.dataGridView_SurveyData.Name = "dataGridView_SurveyData";
		this.dataGridView_SurveyData.ReadOnly = true;
		this.dataGridView_SurveyData.RowHeadersVisible = false;
		this.dataGridView_SurveyData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView_SurveyData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dataGridView_SurveyData.Size = new System.Drawing.Size(926, 226);
		this.dataGridView_SurveyData.StandardTab = true;
		this.dataGridView_SurveyData.TabIndex = 1;
		this.dataGridView_SurveyData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_SurveyData_MouseDown);
		this.dataGridView_SurveyData.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_SurveyData_DragEnter);
		this.dataGridView_SurveyData.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_SurveyData_DragDrop);
		this.contextMenuStrip_GridOptions.Font = new System.Drawing.Font("Tahoma", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip_GridOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mDToolStripMenuItem, this.dipToolStripMenuItem, this.azimuthToolStripMenuItem });
		this.contextMenuStrip_GridOptions.Name = "contextMenuStrip_GridOptions";
		this.contextMenuStrip_GridOptions.Size = new System.Drawing.Size(167, 88);
		this.mDToolStripMenuItem.Name = "mDToolStripMenuItem";
		this.mDToolStripMenuItem.Size = new System.Drawing.Size(166, 28);
		this.mDToolStripMenuItem.Text = "MD";
		this.mDToolStripMenuItem.Click += new System.EventHandler(this.mDToolStripMenuItem_Click);
		this.dipToolStripMenuItem.Name = "dipToolStripMenuItem";
		this.dipToolStripMenuItem.Size = new System.Drawing.Size(166, 28);
		this.dipToolStripMenuItem.Text = "Dip";
		this.dipToolStripMenuItem.Click += new System.EventHandler(this.dipToolStripMenuItem_Click);
		this.azimuthToolStripMenuItem.Name = "azimuthToolStripMenuItem";
		this.azimuthToolStripMenuItem.Size = new System.Drawing.Size(166, 28);
		this.azimuthToolStripMenuItem.Text = "Azimuth";
		this.azimuthToolStripMenuItem.Click += new System.EventHandler(this.azimuthToolStripMenuItem_Click);
		this.panel_Mapping.Controls.Add(this.groupBox_ColumnName);
		this.panel_Mapping.Controls.Add(this.groupBox_SchemaName);
		this.panel_Mapping.Controls.Add(this.panel2);
		this.panel_Mapping.Controls.Add(this.groupBox_Options);
		this.panel_Mapping.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_Mapping.Location = new System.Drawing.Point(3, 16);
		this.panel_Mapping.Name = "panel_Mapping";
		this.panel_Mapping.Size = new System.Drawing.Size(932, 214);
		this.panel_Mapping.TabIndex = 0;
		this.groupBox_ColumnName.Controls.Add(this.label3);
		this.groupBox_ColumnName.Controls.Add(this.label1);
		this.groupBox_ColumnName.Controls.Add(this.checkBox_header);
		this.groupBox_ColumnName.Controls.Add(this.listBox_Field);
		this.groupBox_ColumnName.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_ColumnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_ColumnName.Location = new System.Drawing.Point(0, 66);
		this.groupBox_ColumnName.Name = "groupBox_ColumnName";
		this.groupBox_ColumnName.Size = new System.Drawing.Size(709, 148);
		this.groupBox_ColumnName.TabIndex = 1;
		this.groupBox_ColumnName.TabStop = false;
		this.groupBox_ColumnName.Text = "Mapping :";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(353, 99);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(351, 24);
		this.label3.TabIndex = 37;
		this.label3.Text = "(If ticked the first row will not be imported)";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(698, 24);
		this.label1.TabIndex = 36;
		this.label1.Text = "Drag the field names onto the \"Survey Data\" columns that contain the relevant data:";
		this.checkBox_header.AutoSize = true;
		this.checkBox_header.Cursor = System.Windows.Forms.Cursors.Hand;
		this.checkBox_header.Location = new System.Drawing.Point(420, 68);
		this.checkBox_header.Name = "checkBox_header";
		this.checkBox_header.Size = new System.Drawing.Size(201, 28);
		this.checkBox_header.TabIndex = 1;
		this.checkBox_header.Text = "CSV has header text";
		this.checkBox_header.UseVisualStyleBackColor = true;
		this.checkBox_header.CheckedChanged += new System.EventHandler(this.checkBox_header_CheckedChanged);
		this.listBox_Field.BackColor = System.Drawing.Color.White;
		this.listBox_Field.Cursor = System.Windows.Forms.Cursors.Hand;
		this.listBox_Field.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.listBox_Field.FormattingEnabled = true;
		this.listBox_Field.ItemHeight = 24;
		this.listBox_Field.Items.AddRange(new object[3] { "MD", "Dip", "Azimuth" });
		this.listBox_Field.Location = new System.Drawing.Point(6, 59);
		this.listBox_Field.Name = "listBox_Field";
		this.listBox_Field.Size = new System.Drawing.Size(327, 76);
		this.listBox_Field.TabIndex = 0;
		this.listBox_Field.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_Field_MouseDown);
		this.groupBox_SchemaName.Controls.Add(this.label2);
		this.groupBox_SchemaName.Controls.Add(this.textBox_Name);
		this.groupBox_SchemaName.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_SchemaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_SchemaName.Location = new System.Drawing.Point(0, 0);
		this.groupBox_SchemaName.Name = "groupBox_SchemaName";
		this.groupBox_SchemaName.Size = new System.Drawing.Size(709, 66);
		this.groupBox_SchemaName.TabIndex = 0;
		this.groupBox_SchemaName.TabStop = false;
		this.groupBox_SchemaName.Text = "Name:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(450, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(236, 24);
		this.label2.TabIndex = 1;
		this.label2.Text = "(List name of the new map)";
		this.textBox_Name.BackColor = System.Drawing.Color.White;
		this.textBox_Name.Location = new System.Drawing.Point(6, 28);
		this.textBox_Name.MaxLength = 40;
		this.textBox_Name.Name = "textBox_Name";
		this.textBox_Name.Size = new System.Drawing.Size(437, 29);
		this.textBox_Name.TabIndex = 0;
		this.textBox_Name.Enter += new System.EventHandler(this.textBox_Name_Enter);
		this.textBox_Name.DoubleClick += new System.EventHandler(this.textBox_Name_DoubleClick);
		this.textBox_Name.Leave += new System.EventHandler(this.textBox_Name_Leave);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(709, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 214);
		this.panel2.TabIndex = 34;
		this.groupBox_Options.Controls.Add(this.button_Reset);
		this.groupBox_Options.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupBox_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Options.Location = new System.Drawing.Point(719, 0);
		this.groupBox_Options.Name = "groupBox_Options";
		this.groupBox_Options.Size = new System.Drawing.Size(213, 214);
		this.groupBox_Options.TabIndex = 2;
		this.groupBox_Options.TabStop = false;
		this.groupBox_Options.Text = "Options:";
		this.button_Reset.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Reset.AutoEllipsis = true;
		this.button_Reset.BackColor = System.Drawing.Color.Transparent;
		this.button_Reset.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Reset.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Reset.ColorOn = System.Drawing.Color.Orange;
		this.button_Reset.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Reset.ColorPress = System.Drawing.Color.Orange;
		this.button_Reset.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Reset.FadingSpeed = 35;
		this.button_Reset.FlatAppearance.BorderSize = 0;
		this.button_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Reset.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Reset.Image = EzyPlan_Interface_V2.Properties.Resources.Refresh_Document;
		this.button_Reset.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Reset.ImageOffset = 4;
		this.button_Reset.IsPressed = false;
		this.button_Reset.KeepPress = false;
		this.button_Reset.Location = new System.Drawing.Point(6, 41);
		this.button_Reset.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Reset.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Reset.Name = "button_Reset";
		this.button_Reset.Radius = 10;
		this.button_Reset.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Reset.Size = new System.Drawing.Size(194, 45);
		this.button_Reset.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Reset.SplitDistance = 0;
		this.button_Reset.TabIndex = 0;
		this.button_Reset.Text = "Reset";
		this.button_Reset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Reset.Title = "";
		this.button_Reset.UseVisualStyleBackColor = true;
		this.button_Reset.Enter += new System.EventHandler(this.button_Reset_Enter);
		this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
		this.button_Reset.Leave += new System.EventHandler(this.button_Reset_Leave);
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
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Cancel_Save;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(160, 6);
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
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
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
		this.button_Save.Location = new System.Drawing.Point(6, 6);
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
		this.button_Save.Enter += new System.EventHandler(this.button_Save_Enter);
		this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
		this.button_Save.Leave += new System.EventHandler(this.button_Save_Leave);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Import_Doc;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(888, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 74);
		this.panel1.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_Import_CreateMap";
		base.Size = new System.Drawing.Size(952, 634);
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
		this.groupBox_data.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView_SurveyData).EndInit();
		this.contextMenuStrip_GridOptions.ResumeLayout(false);
		this.panel_Mapping.ResumeLayout(false);
		this.groupBox_ColumnName.ResumeLayout(false);
		this.groupBox_ColumnName.PerformLayout();
		this.groupBox_SchemaName.ResumeLayout(false);
		this.groupBox_SchemaName.PerformLayout();
		this.groupBox_Options.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_Import_CreateMap()
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_mdColour = Color.Blue;
		m_dipColour = Color.Blue;
		m_azimuthColour = Color.Blue;
		m_mdIndex = -1;
		m_dipIndex = -1;
		m_azimuthIndex = -1;
	}

	public void Display(CachedCsvReader csvReader)
	{
		InitialiseMapping();
		textBox_Name.Focus();
		if (!SetGridDataSource(ref csvReader))
		{
			FinishedEventArgs e = new FinishedEventArgs();
			e.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
			OnFinished(e);
		}
	}

	private void SetFieldValues(ImportSurveyMapping mapping)
	{
		textBox_Name.Text = mapping.Name;
		checkBox_header.Checked = mapping.Header;
	}

	private bool SetGridDataSource(ref CachedCsvReader csvReader)
	{
		try
		{
			dataGridView_SurveyData.DataSource = csvReader;
			foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			dataGridView_SurveyData.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
			dataGridView_SurveyData.Refresh();
			return true;
		}
		catch (MissingFieldCsvException ex)
		{
			dataGridView_SurveyData.DataSource = null;
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error in CSV file!", ex.Message);
			popup_MessageBox_OK.ShowDialog();
			return false;
		}
	}

	private void InitialiseMapping()
	{
		RemoveMDIndex();
		RemoveDipIndex();
		RemoveAzimuthIndex();
		SetHeaderPresent(headerPresent: false);
		checkBox_header.Checked = false;
		textBox_Name.Text = "";
	}

	private void SetHeaderPresent(bool headerPresent)
	{
		m_headerPresent = headerPresent;
		if (dataGridView_SurveyData.Rows.Count > 0)
		{
			if (headerPresent)
			{
				dataGridView_SurveyData.Rows[0].DefaultCellStyle.BackColor = Color.Red;
			}
			else
			{
				dataGridView_SurveyData.Rows[0].DefaultCellStyle.BackColor = dataGridView_SurveyData.DefaultCellStyle.BackColor;
			}
		}
	}

	private void SetMDIndex(int index)
	{
		m_mdIndex = index;
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "MD";
				column.DefaultCellStyle.ForeColor = m_mdColour;
			}
		}
	}

	private void RemoveMDIndex()
	{
		int mdIndex = m_mdIndex;
		if (mdIndex == -1)
		{
			return;
		}
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == mdIndex)
			{
				column.HeaderText = column.Name;
				column.DefaultCellStyle.ForeColor = dataGridView_SurveyData.DefaultCellStyle.ForeColor;
			}
		}
		m_mdIndex = -1;
	}

	private void SetDipIndex(int index)
	{
		m_dipIndex = index;
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "Dip";
				column.DefaultCellStyle.ForeColor = m_dipColour;
			}
		}
	}

	private void RemoveDipIndex()
	{
		if (m_dipIndex == -1)
		{
			return;
		}
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == m_dipIndex)
			{
				column.HeaderText = column.Name;
				column.DefaultCellStyle.ForeColor = dataGridView_SurveyData.DefaultCellStyle.ForeColor;
			}
		}
		m_dipIndex = -1;
	}

	private void SetAzimuthIndex(int index)
	{
		m_azimuthIndex = index;
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == index)
			{
				column.HeaderText = "Azimuth";
				column.DefaultCellStyle.ForeColor = m_azimuthColour;
			}
		}
	}

	private void RemoveAzimuthIndex()
	{
		if (m_azimuthIndex == -1)
		{
			return;
		}
		foreach (DataGridViewColumn column in dataGridView_SurveyData.Columns)
		{
			if (column.Index == m_azimuthIndex)
			{
				column.HeaderText = column.Name;
				column.DefaultCellStyle.ForeColor = dataGridView_SurveyData.DefaultCellStyle.ForeColor;
			}
		}
		m_azimuthIndex = -1;
	}

	private void RemoveAssignedIndex(int index)
	{
		if (m_mdIndex == index)
		{
			RemoveMDIndex();
		}
		else if (m_dipIndex == index)
		{
			RemoveDipIndex();
		}
		else if (m_azimuthIndex == index)
		{
			RemoveAzimuthIndex();
		}
	}

	private void SetSelectedColumnAsMDIndex()
	{
		try
		{
			int index = dataGridView_SurveyData.SelectedColumns[0].Index;
			RemoveMDIndex();
			RemoveAssignedIndex(index);
			SetMDIndex(index);
		}
		catch (ArgumentOutOfRangeException)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No column selected!", "A column is not selected - Please selected a column to assign the MD field to first.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void SetSelectedColumnAsDipIndex()
	{
		try
		{
			int index = dataGridView_SurveyData.SelectedColumns[0].Index;
			RemoveDipIndex();
			RemoveAssignedIndex(index);
			SetDipIndex(index);
		}
		catch (ArgumentOutOfRangeException)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No column selected!", "A column is not selected - Please selected a column to assign the Dip field to first.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void SetSelectedColumnAsAzimuthIndex()
	{
		try
		{
			int index = dataGridView_SurveyData.SelectedColumns[0].Index;
			RemoveAzimuthIndex();
			RemoveAssignedIndex(index);
			SetAzimuthIndex(index);
		}
		catch (ArgumentOutOfRangeException)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("No column selected!", "A column is not selected - Please selected a column to assign the Azimuth field to first.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void CreateImportMappingObject()
	{
		ImportSurveyMapping importSurveyMapping = new ImportSurveyMapping();
		importSurveyMapping.Header = m_headerPresent;
		if (SetName(importSurveyMapping) && SetMDIndex(importSurveyMapping) && SetDipIndex(importSurveyMapping) && SetAzimuithIndex(importSurveyMapping))
		{
			FinishedEventArgs e = new FinishedEventArgs();
			e.Mapping = importSurveyMapping;
			e.FinishedState = FinishedEventArgs.FinishedStateEnum.Save;
			OnFinished(e);
		}
	}

	private bool SetName(ImportSurveyMapping mapping)
	{
		if (textBox_Name.Text != "")
		{
			mapping.Name = textBox_Name.Text;
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Name not entered", "Enter a list name for the new mapping schema to continue.");
		popup_MessageBox_OK.ShowDialog();
		textBox_Name.Focus();
		return false;
	}

	private bool SetMDIndex(ImportSurveyMapping mapping)
	{
		if (m_mdIndex != -1)
		{
			try
			{
				mapping.MD = m_mdIndex;
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "An unexpected error has occered and the mapping schema could not be saved.");
				popup_MessageBox_OK.ShowDialog();
				return false;
			}
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("MD column not selected", "Select the MD column to continue.");
		popup_MessageBox_OK2.ShowDialog();
		return false;
	}

	private bool SetDipIndex(ImportSurveyMapping mapping)
	{
		if (m_dipIndex != -1)
		{
			try
			{
				mapping.Dip = m_dipIndex;
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "An unexpected error has occered and the mapping schema could not be saved.");
				popup_MessageBox_OK.ShowDialog();
				return false;
			}
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Dip column not selected", "Select the Dip column to continue.");
		popup_MessageBox_OK2.ShowDialog();
		return false;
	}

	private bool SetAzimuithIndex(ImportSurveyMapping mapping)
	{
		if (m_azimuthIndex != -1)
		{
			try
			{
				mapping.Azimuth = m_azimuthIndex;
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Critical Error", "An unexpected error has occered and the mapping schema could not be saved.");
				popup_MessageBox_OK.ShowDialog();
				return false;
			}
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Azimuth column not selected", "Select the Azimuth column to continue.");
		popup_MessageBox_OK2.ShowDialog();
		return false;
	}

	private void SetDroppedColumn(string field, int columnIndex)
	{
		if (columnIndex >= 0)
		{
			switch (field)
			{
			case "MD":
				RemoveMDIndex();
				RemoveAssignedIndex(columnIndex);
				SetMDIndex(columnIndex);
				break;
			case "Dip":
				RemoveDipIndex();
				RemoveAssignedIndex(columnIndex);
				SetDipIndex(columnIndex);
				break;
			case "Azimuth":
				RemoveAzimuthIndex();
				RemoveAssignedIndex(columnIndex);
				SetAzimuthIndex(columnIndex);
				break;
			}
		}
	}

	private void dataGridView_SurveyData_DragDrop(object sender, DragEventArgs e)
	{
		try
		{
			string field = (string)e.Data.GetData(DataFormats.StringFormat);
			Point point = dataGridView_SurveyData.PointToClient(new Point(e.X, e.Y));
			DataGridView.HitTestInfo hitTestInfo = dataGridView_SurveyData.HitTest(point.X, point.Y);
			SetDroppedColumn(field, hitTestInfo.ColumnIndex);
		}
		catch
		{
		}
	}

	private void dataGridView_SurveyData_DragEnter(object sender, DragEventArgs e)
	{
		try
		{
			_ = (string)e.Data.GetData(DataFormats.StringFormat);
			e.Effect = DragDropEffects.Link;
		}
		catch
		{
		}
	}

	private void listBox_Field_MouseDown(object sender, MouseEventArgs e)
	{
		listBox_Field.DoDragDrop(listBox_Field.SelectedItem.ToString(), DragDropEffects.Link);
	}

	private void OnFinished(FinishedEventArgs e)
	{
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		FinishedEventArgs e2 = new FinishedEventArgs();
		e2.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
		OnFinished(e2);
	}

	private void dataGridView_SurveyData_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			DataGridView.HitTestInfo hitTestInfo = dataGridView_SurveyData.HitTest(e.X, e.Y);
			if (hitTestInfo.ColumnIndex >= 0)
			{
				SelectColumn(hitTestInfo.ColumnIndex);
			}
		}
	}

	private void SelectColumn(int index)
	{
		if (dataGridView_SurveyData.Columns.Count > 0 && index <= dataGridView_SurveyData.Columns.Count - 1)
		{
			dataGridView_SurveyData.ClearSelection();
			dataGridView_SurveyData.Columns[index].Selected = true;
		}
	}

	private void button_Save_Click(object sender, EventArgs e)
	{
		CreateImportMappingObject();
	}

	private void checkBox_header_CheckedChanged(object sender, EventArgs e)
	{
		SetHeaderPresent(checkBox_header.Checked);
	}

	private void mDToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SetSelectedColumnAsMDIndex();
	}

	private void dipToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SetSelectedColumnAsDipIndex();
	}

	private void azimuthToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SetSelectedColumnAsAzimuthIndex();
	}

	private void button_Reset_Click(object sender, EventArgs e)
	{
		InitialiseMapping();
	}

	private void textBox_Name_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Name);
	}

	private void textBox_Name_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Name);
	}

	private void button_Reset_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Reset);
	}

	private void button_Reset_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Reset);
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

	private void textBox_Name_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Name);
	}
}
