using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_BuildToTarget_Select : Layout_Body_Wizard
{
	public enum BuildStates
	{
		OK,
		BuildBelowTarget
	}

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Cancel,
			Next
		}

		private ButtonPressedEnum m_buttonPressed;

		private BuildAndHoldResults m_buildSelected;

		private AbsoluteSurvey m_lastSurvey;

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

		public BuildAndHoldResults BuildSelected
		{
			get
			{
				return m_buildSelected;
			}
			set
			{
				m_buildSelected = value;
			}
		}

		public AbsoluteSurvey LastSurvey
		{
			get
			{
				return m_lastSurvey;
			}
			set
			{
				m_lastSurvey = value;
			}
		}
	}

	private IContainer components;

	private Panel panel_BodyFooter;

	private Button button_Select;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Next;

	private TabControl tabControl_ChartOptions;

	private TabPage tabPage_Select;

	private TabPage tabPage_ChartOptions;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private DataGridView dataGridView1;

	private Label label1;

	private ComboBox comboBox_StepBy;

	private Panel panel_Sub;

	private Panel panel_ScrollChart;

	private Panel panel_SevereDL;

	private Panel panel_ExtremeDL;

	private Label label2;

	private Label label3;

	private Panel panel1;

	private DataGridViewTextBoxColumn DogLeg;

	private DataGridViewTextBoxColumn BuildLength;

	private DataGridViewTextBoxColumn StraightLength;

	private Panel panel_StepBy;

	private Panel panel_BuildBelow;

	private Label label_BuildState;

	private DrillHoleFile m_drillHoleFile;

	private EzyNavHoleChart m_chart;

	private List<BuildAndHoldResults> m_buildList;

	private AbsoluteSurvey m_lastSurvey;

	private BuildStates m_selectedBuildState;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	public event EventHandler ButtonPressed;

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
		this.panel_BodyFooter = new System.Windows.Forms.Panel();
		this.button_Select = new System.Windows.Forms.Button();
		this.tabControl_ChartOptions = new System.Windows.Forms.TabControl();
		this.tabPage_Select = new System.Windows.Forms.TabPage();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.DogLeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.BuildLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.StraightLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel_Sub = new System.Windows.Forms.Panel();
		this.panel_BuildBelow = new System.Windows.Forms.Panel();
		this.label_BuildState = new System.Windows.Forms.Label();
		this.panel_StepBy = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.comboBox_StepBy = new System.Windows.Forms.ComboBox();
		this.tabPage_ChartOptions = new System.Windows.Forms.TabPage();
		this.panel_ScrollChart = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.panel_SevereDL = new System.Windows.Forms.Panel();
		this.panel_ExtremeDL = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.panel_BodyFooter.SuspendLayout();
		this.tabControl_ChartOptions.SuspendLayout();
		this.tabPage_Select.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.panel_Sub.SuspendLayout();
		this.panel_BuildBelow.SuspendLayout();
		this.panel_StepBy.SuspendLayout();
		this.tabPage_ChartOptions.SuspendLayout();
		this.panel_ScrollChart.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Location = new System.Drawing.Point(4, 3);
		base.label_headerText1.Size = new System.Drawing.Size(291, 25);
		base.label_headerText1.Text = "Select the desired Dogleg:";
		base.groupBox_frame.Size = new System.Drawing.Size(383, 558);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Location = new System.Drawing.Point(117, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(282, 56);
		base.panel_body.Location = new System.Drawing.Point(0, 61);
		base.panel_body.Size = new System.Drawing.Size(399, 574);
		base.panel_body.TabIndex = 0;
		base.panel_header.Controls.Add(this.panel_ExtremeDL);
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Controls.Add(this.panel_SevereDL);
		base.panel_header.Controls.Add(this.label2);
		base.panel_header.Controls.Add(this.label3);
		base.panel_header.Size = new System.Drawing.Size(399, 61);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.label3, 0);
		base.panel_header.Controls.SetChildIndex(this.label2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_SevereDL, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_ExtremeDL, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 635);
		base.panel_footer.Size = new System.Drawing.Size(399, 56);
		base.panel_footer.TabIndex = 1;
		base.label_headerText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText2.Location = new System.Drawing.Point(5, 28);
		base.label_headerText2.Size = new System.Drawing.Size(50, 25);
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.tabControl_ChartOptions);
		base.panel_scrollPanel.Size = new System.Drawing.Size(377, 539);
		this.panel_BodyFooter.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_BodyFooter.Controls.Add(this.button_Select);
		this.panel_BodyFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_BodyFooter.Location = new System.Drawing.Point(3, 541);
		this.panel_BodyFooter.Name = "panel_BodyFooter";
		this.panel_BodyFooter.Size = new System.Drawing.Size(335, 60);
		this.panel_BodyFooter.TabIndex = 1;
		this.button_Select.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Select.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Select.Location = new System.Drawing.Point(189, 9);
		this.button_Select.Name = "button_Select";
		this.button_Select.Size = new System.Drawing.Size(131, 42);
		this.button_Select.TabIndex = 0;
		this.button_Select.Text = "Select";
		this.button_Select.UseVisualStyleBackColor = true;
		this.tabControl_ChartOptions.Controls.Add(this.tabPage_Select);
		this.tabControl_ChartOptions.Controls.Add(this.tabPage_ChartOptions);
		this.tabControl_ChartOptions.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl_ChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl_ChartOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl_ChartOptions.Location = new System.Drawing.Point(0, 0);
		this.tabControl_ChartOptions.Name = "tabControl_ChartOptions";
		this.tabControl_ChartOptions.SelectedIndex = 0;
		this.tabControl_ChartOptions.Size = new System.Drawing.Size(377, 539);
		this.tabControl_ChartOptions.TabIndex = 0;
		this.tabControl_ChartOptions.Click += new System.EventHandler(this.tabControl_ChartOptions_Click);
		this.tabPage_Select.Controls.Add(this.dataGridView1);
		this.tabPage_Select.Controls.Add(this.panel_Sub);
		this.tabPage_Select.Location = new System.Drawing.Point(4, 34);
		this.tabPage_Select.Name = "tabPage_Select";
		this.tabPage_Select.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Select.Size = new System.Drawing.Size(369, 501);
		this.tabPage_Select.TabIndex = 0;
		this.tabPage_Select.Text = "Select Dogleg";
		this.tabPage_Select.UseVisualStyleBackColor = true;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.DogLeg, this.BuildLength, this.StraightLength);
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.ShowCellErrors = false;
		this.dataGridView1.ShowEditingIcon = false;
		this.dataGridView1.ShowRowErrors = false;
		this.dataGridView1.Size = new System.Drawing.Size(363, 425);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 0;
		this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
		this.DogLeg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.DogLeg.HeaderText = "Dogleg";
		this.DogLeg.Name = "DogLeg";
		this.DogLeg.ReadOnly = true;
		this.DogLeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.BuildLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.BuildLength.HeaderText = "Build Length";
		this.BuildLength.MinimumWidth = 120;
		this.BuildLength.Name = "BuildLength";
		this.BuildLength.ReadOnly = true;
		this.BuildLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.BuildLength.Width = 120;
		this.StraightLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.StraightLength.HeaderText = "Straight Length";
		this.StraightLength.MinimumWidth = 120;
		this.StraightLength.Name = "StraightLength";
		this.StraightLength.ReadOnly = true;
		this.StraightLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.StraightLength.Width = 120;
		this.panel_Sub.Controls.Add(this.panel_BuildBelow);
		this.panel_Sub.Controls.Add(this.panel_StepBy);
		this.panel_Sub.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_Sub.Location = new System.Drawing.Point(3, 428);
		this.panel_Sub.Name = "panel_Sub";
		this.panel_Sub.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.panel_Sub.Size = new System.Drawing.Size(363, 70);
		this.panel_Sub.TabIndex = 3;
		this.panel_BuildBelow.Controls.Add(this.label_BuildState);
		this.panel_BuildBelow.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_BuildBelow.Location = new System.Drawing.Point(0, 2);
		this.panel_BuildBelow.Name = "panel_BuildBelow";
		this.panel_BuildBelow.Size = new System.Drawing.Size(363, 35);
		this.panel_BuildBelow.TabIndex = 4;
		this.label_BuildState.AutoSize = true;
		this.label_BuildState.Location = new System.Drawing.Point(3, 3);
		this.label_BuildState.Name = "label_BuildState";
		this.label_BuildState.Size = new System.Drawing.Size(46, 25);
		this.label_BuildState.TabIndex = 0;
		this.label_BuildState.Text = "null";
		this.panel_StepBy.Controls.Add(this.label1);
		this.panel_StepBy.Controls.Add(this.comboBox_StepBy);
		this.panel_StepBy.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_StepBy.Location = new System.Drawing.Point(0, 37);
		this.panel_StepBy.Name = "panel_StepBy";
		this.panel_StepBy.Size = new System.Drawing.Size(363, 33);
		this.panel_StepBy.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Dock = System.Windows.Forms.DockStyle.Left;
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(165, 25);
		this.label1.TabIndex = 2;
		this.label1.Text = "Step Dogleg by:";
		this.comboBox_StepBy.BackColor = System.Drawing.Color.White;
		this.comboBox_StepBy.DisplayMember = "0";
		this.comboBox_StepBy.Dock = System.Windows.Forms.DockStyle.Right;
		this.comboBox_StepBy.FormatString = "N2";
		this.comboBox_StepBy.FormattingEnabled = true;
		this.comboBox_StepBy.Items.AddRange(new object[10] { "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0" });
		this.comboBox_StepBy.Location = new System.Drawing.Point(199, 0);
		this.comboBox_StepBy.Name = "comboBox_StepBy";
		this.comboBox_StepBy.Size = new System.Drawing.Size(164, 33);
		this.comboBox_StepBy.TabIndex = 1;
		this.comboBox_StepBy.ValueMember = "0";
		this.comboBox_StepBy.SelectedIndexChanged += new System.EventHandler(this.comboBox_StepBy_SelectedIndexChanged);
		this.comboBox_StepBy.TextUpdate += new System.EventHandler(this.comboBox_StepBy_TextUpdate);
		this.tabPage_ChartOptions.Controls.Add(this.panel_ScrollChart);
		this.tabPage_ChartOptions.Location = new System.Drawing.Point(4, 34);
		this.tabPage_ChartOptions.Name = "tabPage_ChartOptions";
		this.tabPage_ChartOptions.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_ChartOptions.Size = new System.Drawing.Size(524, 444);
		this.tabPage_ChartOptions.TabIndex = 1;
		this.tabPage_ChartOptions.Text = "Chart Options";
		this.tabPage_ChartOptions.UseVisualStyleBackColor = true;
		this.panel_ScrollChart.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScrollChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollChart.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollChart.Name = "panel_ScrollChart";
		this.panel_ScrollChart.Size = new System.Drawing.Size(518, 438);
		this.panel_ScrollChart.TabIndex = 1;
		this.ezyNavHoleChartController1.AutoScroll = true;
		this.ezyNavHoleChartController1.AutoScrollMinSize = new System.Drawing.Size(0, 450);
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(518, 438);
		this.ezyNavHoleChartController1.TabIndex = 0;
		this.panel_SevereDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_SevereDL.Location = new System.Drawing.Point(126, 31);
		this.panel_SevereDL.Name = "panel_SevereDL";
		this.panel_SevereDL.Size = new System.Drawing.Size(41, 27);
		this.panel_SevereDL.TabIndex = 7;
		this.panel_ExtremeDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_ExtremeDL.Location = new System.Drawing.Point(302, 31);
		this.panel_ExtremeDL.Name = "panel_ExtremeDL";
		this.panel_ExtremeDL.Size = new System.Drawing.Size(40, 27);
		this.panel_ExtremeDL.TabIndex = 8;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(173, 30);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(123, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "Extreme Dogleg:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(3, 30);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(117, 16);
		this.label3.TabIndex = 6;
		this.label3.Text = "Severe Dogleg:";
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
		this.button_Next.Location = new System.Drawing.Point(3, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(134, 45);
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
		this.button_Cancel.Location = new System.Drawing.Point(141, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(134, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click_1);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Build;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(335, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 61);
		this.panel1.TabIndex = 9;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "Body_BuildToTarget_Select";
		base.Size = new System.Drawing.Size(399, 691);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.panel_BodyFooter.ResumeLayout(false);
		this.tabControl_ChartOptions.ResumeLayout(false);
		this.tabPage_Select.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.panel_Sub.ResumeLayout(false);
		this.panel_BuildBelow.ResumeLayout(false);
		this.panel_BuildBelow.PerformLayout();
		this.panel_StepBy.ResumeLayout(false);
		this.panel_StepBy.PerformLayout();
		this.tabPage_ChartOptions.ResumeLayout(false);
		this.panel_ScrollChart.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_BuildToTarget_Select(DrillHoleFile drillHoleFile, EzyNavHoleChart chart, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_drillHoleFile = drillHoleFile;
		m_designTimeWidth = Width;
		m_chart = chart;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		Visible = false;
	}

	public void Display()
	{
		setDLSeverityKey();
		tabControl_ChartOptions.SelectedIndex = 0;
		Initialise();
		DisplayCalculatedFields(0.5m);
		Visible = true;
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		Focus();
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	public void Initialise()
	{
		dataGridView1.Rows.Clear();
		comboBox_StepBy.Text = "";
	}

	private void DisplayCalculatedFields(decimal steps)
	{
		NavMath navMath = new NavMath();
		Target target = m_drillHoleFile.Target;
		Collar collar = m_drillHoleFile.Collar;
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		m_lastSurvey = absoluteSurveyList.LastRecord();
		NavMath.BuildToTargetListEnum buildToTargetListEnum = navMath.BuildToTargetList(ref target, ref collar, ref m_lastSurvey, steps, 15, out m_buildList);
		if (buildToTargetListEnum == NavMath.BuildToTargetListEnum.Success)
		{
			UpdateDataGrid(m_buildList);
			return;
		}
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Build to target failed!", NavMath.ToHumanReadable(buildToTargetListEnum) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void UpdateDataGrid(List<BuildAndHoldResults> buildList)
	{
		dataGridView1.Rows.Clear();
		foreach (BuildAndHoldResults build in buildList)
		{
			dataGridView1.Rows.Add(build.DogLeg.ToString(), build.NaviDistance.ToString(), build.StraightDistance.ToString());
		}
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			Utility.StringToDecimal(item.Cells[0].Value.ToString(), out var number);
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

	private void setDLSeverityKey()
	{
		panel_SevereDL.BackColor = Utility.m_SevereColor;
		panel_ExtremeDL.BackColor = Utility.m_ExtremeColor;
	}

	private void SelectionChanged()
	{
		if (dataGridView1.SelectedRows.Count == 1)
		{
			int index = dataGridView1.SelectedRows[0].Index;
			BuildAndHoldResults buildAndHoldResults = m_buildList[index];
			SetBuildState(buildAndHoldResults);
			m_chart.SetBuild(buildAndHoldResults.ProjectedSurveyData, visible: true);
			AbsoluteSurvey endOfHoleSurvey = buildAndHoldResults.EndOfHoleSurvey;
			new Target(endOfHoleSurvey.Easting, endOfHoleSurvey.Northing, endOfHoleSurvey.TVD);
			m_chart.SetStraightPathToTarget(buildAndHoldResults.EndOfBuildSurvey, buildAndHoldResults.EndOfHoleSurvey, visible: true);
		}
	}

	private void dataGridView1_SelectionChanged(object sender, EventArgs e)
	{
		SelectionChanged();
	}

	private void SetBuildState(BuildAndHoldResults build)
	{
		if (build.BuildBelowTarget)
		{
			SetBuildState(BuildStates.BuildBelowTarget);
		}
		else
		{
			SetBuildState(BuildStates.OK);
		}
	}

	private void SetBuildState(BuildStates state)
	{
		m_selectedBuildState = state;
		switch (state)
		{
		case BuildStates.OK:
			DisplayBuildState_OK();
			break;
		case BuildStates.BuildBelowTarget:
			DisplayBuildState_BuildBelowTargetTVD();
			break;
		}
	}

	private void DisplayBuildState_OK()
	{
		label_BuildState.Visible = false;
	}

	private void DisplayBuildState_BuildBelowTargetTVD()
	{
		label_BuildState.Text = "Selected build goes below target!";
		label_BuildState.ForeColor = Color.Red;
		label_BuildState.Visible = true;
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void Next()
	{
		if (dataGridView1.SelectedRows.Count == 1)
		{
			if (m_selectedBuildState == BuildStates.OK)
			{
				ProcedeWithSelectedBuild();
				return;
			}
			Popup_MessageBox_YesNo popup_MessageBox_YesNo = new Popup_MessageBox_YesNo("Warning!", "The path of the selected build goes below the target TVD! \nAre you sure you wan't to continue with the selected build?");
			popup_MessageBox_YesNo.ShowDialog();
			if (popup_MessageBox_YesNo.CustomDialogResult == Popup_MessageBox_YesNo.DialogResultEnum.Yes)
			{
				ProcedeWithSelectedBuild();
			}
		}
		else
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error!", "No build scenario has been selected from the list on the right hand side.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void ProcedeWithSelectedBuild()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Next;
		int index = dataGridView1.SelectedRows[0].Index;
		e.BuildSelected = m_buildList[index];
		e.LastSurvey = m_lastSurvey;
		OnButtonPressed(e);
	}

	private void Cancel()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Cancel;
		e.BuildSelected = null;
		OnButtonPressed(e);
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void button_Cancel_Click_1(object sender, EventArgs e)
	{
		Cancel();
	}

	private void StepSelectionChagned()
	{
		string s = comboBox_StepBy.Text;
		decimal steps = decimal.Parse(s);
		DisplayCalculatedFields(steps);
	}

	private void StepManuallyEntered()
	{
		string s = comboBox_StepBy.Text;
		try
		{
			decimal num = decimal.Parse(s);
			if (num >= 0.01m)
			{
				DisplayCalculatedFields(num);
			}
		}
		catch
		{
		}
	}

	private void comboBox_StepBy_TextUpdate(object sender, EventArgs e)
	{
		StepManuallyEntered();
	}

	private void comboBox_StepBy_SelectedIndexChanged(object sender, EventArgs e)
	{
		StepSelectionChagned();
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void tabControl_ChartOptions_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
	}
}
