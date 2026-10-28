using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EzyNavCharting;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using EzyReport;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_BuildToTarget_Results : Layout_Body_Wizard
{
	private delegate EzyReport.Report AsyncConsOperationsDelegate(bool surveyData, bool buildSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working);

	private delegate void DisplayGeneratedReportDelegate(EzyReport.Report report, ref Popup_WorkingIndicator working);

	private delegate void DriveArrivedDelegate(string driveLetter);

	private delegate void DriveRemovedDelegate();

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Back,
			Finish
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

	private IContainer components;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Finish;

	private RibbonMenuButton button_AdvancedResults;

	private RibbonMenuButton button_GenerateReport;

	private TabControl tabControl1;

	private TabPage tabPage_Results;

	private TabPage tabPage_ChartOptions;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Panel panel_Scroll;

	private TextBox textBox_NaviStop;

	private TextBox textBox_NaviStart;

	private TextBox textBox_NaviDistance;

	private TextBox textBox_ToolFace;

	private TextBox textBox_DogLeg;

	private Label label_NaviStop;

	private Label label_NaviStart;

	private Label label1;

	private Label label_Toolface;

	private Label label_Dogleg;

	private Panel panel_ScrollChart;

	private TextBox textBox_StraightDistance;

	private Label label2;

	private Label label3;

	private Panel panel1;

	private Label label4;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private EzyNavHoleChart m_chart;

	private BuildAndHoldResults m_buildResults;

	private AbsoluteSurvey m_lastSurvey;

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleStatus m_drillHoleStatus;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private DriveArrivedDelegate m_driveArrived;

	private DriveRemovedDelegate m_driveRemoved;

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
		this.button_Finish = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_AdvancedResults = new RibbonStyle.RibbonMenuButton();
		this.button_GenerateReport = new RibbonStyle.RibbonMenuButton();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage_Results = new System.Windows.Forms.TabPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.panel_Scroll = new System.Windows.Forms.Panel();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox_NaviStop = new System.Windows.Forms.TextBox();
		this.textBox_NaviStart = new System.Windows.Forms.TextBox();
		this.textBox_StraightDistance = new System.Windows.Forms.TextBox();
		this.textBox_NaviDistance = new System.Windows.Forms.TextBox();
		this.textBox_ToolFace = new System.Windows.Forms.TextBox();
		this.textBox_DogLeg = new System.Windows.Forms.TextBox();
		this.label_NaviStop = new System.Windows.Forms.Label();
		this.label_NaviStart = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label_Toolface = new System.Windows.Forms.Label();
		this.label_Dogleg = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.tabPage_ChartOptions = new System.Windows.Forms.TabPage();
		this.panel_ScrollChart = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage_Results.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.panel_Scroll.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabPage_ChartOptions.SuspendLayout();
		this.panel_ScrollChart.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(245, 25);
		base.label_headerText1.Text = "Build to target results:";
		base.groupBox_frame.Size = new System.Drawing.Size(383, 639);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.button_Finish);
		base.panel_buttonPanel.Location = new System.Drawing.Point(114, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(285, 58);
		base.panel_body.Location = new System.Drawing.Point(0, 55);
		base.panel_body.Size = new System.Drawing.Size(399, 655);
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Size = new System.Drawing.Size(399, 55);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 710);
		base.panel_footer.Size = new System.Drawing.Size(399, 58);
		base.label_headerText2.Size = new System.Drawing.Size(174, 20);
		base.label_headerText2.Text = "(Drilling instructions)";
		base.panel_scrollPanel.Controls.Add(this.tabControl1);
		base.panel_scrollPanel.Size = new System.Drawing.Size(377, 620);
		this.button_Finish.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Finish.AutoEllipsis = true;
		this.button_Finish.BackColor = System.Drawing.Color.Transparent;
		this.button_Finish.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Finish.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Finish.ColorOn = System.Drawing.Color.Orange;
		this.button_Finish.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Finish.ColorPress = System.Drawing.Color.Orange;
		this.button_Finish.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Finish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Finish.FadingSpeed = 35;
		this.button_Finish.FlatAppearance.BorderSize = 0;
		this.button_Finish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Finish.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Finish.Image = EzyPlan_Interface_V2.Properties.Resources.Last;
		this.button_Finish.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Finish.ImageOffset = 0;
		this.button_Finish.IsPressed = false;
		this.button_Finish.KeepPress = false;
		this.button_Finish.Location = new System.Drawing.Point(3, 6);
		this.button_Finish.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Finish.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Finish.Name = "button_Finish";
		this.button_Finish.Radius = 10;
		this.button_Finish.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Finish.Size = new System.Drawing.Size(134, 45);
		this.button_Finish.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Finish.SplitDistance = 0;
		this.button_Finish.TabIndex = 0;
		this.button_Finish.Text = "Finish";
		this.button_Finish.Title = "";
		this.button_Finish.UseVisualStyleBackColor = true;
		this.button_Finish.Click += new System.EventHandler(this.button_Finish_Click);
		this.button_Finish.Leave += new System.EventHandler(this.button_Finish_Leave);
		this.button_Finish.Enter += new System.EventHandler(this.button_Finish_Enter);
		this.button_Back.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Back.AutoEllipsis = true;
		this.button_Back.BackColor = System.Drawing.Color.Transparent;
		this.button_Back.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Back.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorOn = System.Drawing.Color.Orange;
		this.button_Back.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
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
		this.button_Back.ImageOffset = 0;
		this.button_Back.IsPressed = false;
		this.button_Back.KeepPress = false;
		this.button_Back.Location = new System.Drawing.Point(143, 6);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(134, 45);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 1;
		this.button_Back.Text = "Back";
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
		this.button_AdvancedResults.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_AdvancedResults.AutoEllipsis = true;
		this.button_AdvancedResults.BackColor = System.Drawing.Color.Transparent;
		this.button_AdvancedResults.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AdvancedResults.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AdvancedResults.ColorOn = System.Drawing.Color.Orange;
		this.button_AdvancedResults.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_AdvancedResults.ColorPress = System.Drawing.Color.Orange;
		this.button_AdvancedResults.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AdvancedResults.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AdvancedResults.FadingSpeed = 35;
		this.button_AdvancedResults.FlatAppearance.BorderSize = 0;
		this.button_AdvancedResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AdvancedResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AdvancedResults.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AdvancedResults.Image = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.button_AdvancedResults.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AdvancedResults.ImageOffset = 0;
		this.button_AdvancedResults.IsPressed = false;
		this.button_AdvancedResults.KeepPress = false;
		this.button_AdvancedResults.Location = new System.Drawing.Point(6, 29);
		this.button_AdvancedResults.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AdvancedResults.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AdvancedResults.Name = "button_AdvancedResults";
		this.button_AdvancedResults.Radius = 20;
		this.button_AdvancedResults.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AdvancedResults.Size = new System.Drawing.Size(164, 45);
		this.button_AdvancedResults.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AdvancedResults.SplitDistance = 0;
		this.button_AdvancedResults.TabIndex = 0;
		this.button_AdvancedResults.Text = "Advanced";
		this.button_AdvancedResults.Title = "";
		this.button_AdvancedResults.UseVisualStyleBackColor = true;
		this.button_AdvancedResults.Click += new System.EventHandler(this.button_Advanced_Click);
		this.button_AdvancedResults.Leave += new System.EventHandler(this.button_AdvancedResults_Leave);
		this.button_AdvancedResults.Enter += new System.EventHandler(this.button_AdvancedResults_Enter);
		this.button_GenerateReport.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_GenerateReport.AutoEllipsis = true;
		this.button_GenerateReport.BackColor = System.Drawing.Color.Transparent;
		this.button_GenerateReport.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_GenerateReport.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_GenerateReport.ColorOn = System.Drawing.Color.Orange;
		this.button_GenerateReport.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_GenerateReport.ColorPress = System.Drawing.Color.Orange;
		this.button_GenerateReport.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_GenerateReport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_GenerateReport.FadingSpeed = 35;
		this.button_GenerateReport.FlatAppearance.BorderSize = 0;
		this.button_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_GenerateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_GenerateReport.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_GenerateReport.Image = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.button_GenerateReport.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_GenerateReport.ImageOffset = 0;
		this.button_GenerateReport.IsPressed = false;
		this.button_GenerateReport.KeepPress = false;
		this.button_GenerateReport.Location = new System.Drawing.Point(176, 29);
		this.button_GenerateReport.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_GenerateReport.MenuPos = new System.Drawing.Point(0, 0);
		this.button_GenerateReport.Name = "button_GenerateReport";
		this.button_GenerateReport.Radius = 20;
		this.button_GenerateReport.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_GenerateReport.Size = new System.Drawing.Size(174, 44);
		this.button_GenerateReport.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_GenerateReport.SplitDistance = 0;
		this.button_GenerateReport.TabIndex = 1;
		this.button_GenerateReport.Text = "Report";
		this.button_GenerateReport.Title = "";
		this.button_GenerateReport.UseVisualStyleBackColor = true;
		this.button_GenerateReport.Click += new System.EventHandler(this.button_GenerateReport_Click);
		this.button_GenerateReport.Leave += new System.EventHandler(this.button_GenerateReport_Leave);
		this.button_GenerateReport.Enter += new System.EventHandler(this.button_GenerateReport_Enter);
		this.tabControl1.Controls.Add(this.tabPage_Results);
		this.tabControl1.Controls.Add(this.tabPage_ChartOptions);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(377, 620);
		this.tabControl1.TabIndex = 0;
		this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
		this.tabPage_Results.Controls.Add(this.groupBox2);
		this.tabPage_Results.Controls.Add(this.groupBox1);
		this.tabPage_Results.Location = new System.Drawing.Point(4, 34);
		this.tabPage_Results.Name = "tabPage_Results";
		this.tabPage_Results.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Results.Size = new System.Drawing.Size(369, 582);
		this.tabPage_Results.TabIndex = 0;
		this.tabPage_Results.Text = "Build Results";
		this.tabPage_Results.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.panel_Scroll);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(3, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(363, 499);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Drilling Instructions";
		this.panel_Scroll.AutoScroll = true;
		this.panel_Scroll.Controls.Add(this.label9);
		this.panel_Scroll.Controls.Add(this.label8);
		this.panel_Scroll.Controls.Add(this.label7);
		this.panel_Scroll.Controls.Add(this.label6);
		this.panel_Scroll.Controls.Add(this.label4);
		this.panel_Scroll.Controls.Add(this.label3);
		this.panel_Scroll.Controls.Add(this.textBox_NaviStop);
		this.panel_Scroll.Controls.Add(this.textBox_NaviStart);
		this.panel_Scroll.Controls.Add(this.textBox_StraightDistance);
		this.panel_Scroll.Controls.Add(this.textBox_NaviDistance);
		this.panel_Scroll.Controls.Add(this.textBox_ToolFace);
		this.panel_Scroll.Controls.Add(this.textBox_DogLeg);
		this.panel_Scroll.Controls.Add(this.label_NaviStop);
		this.panel_Scroll.Controls.Add(this.label_NaviStart);
		this.panel_Scroll.Controls.Add(this.label2);
		this.panel_Scroll.Controls.Add(this.label1);
		this.panel_Scroll.Controls.Add(this.label_Toolface);
		this.panel_Scroll.Controls.Add(this.label_Dogleg);
		this.panel_Scroll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Scroll.Location = new System.Drawing.Point(3, 27);
		this.panel_Scroll.Name = "panel_Scroll";
		this.panel_Scroll.Size = new System.Drawing.Size(357, 469);
		this.panel_Scroll.TabIndex = 0;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.Red;
		this.label9.Location = new System.Drawing.Point(5, 312);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(52, 16);
		this.label9.TabIndex = 16;
		this.label9.Text = "meters.";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.Red;
		this.label8.Location = new System.Drawing.Point(6, 257);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(52, 16);
		this.label8.TabIndex = 16;
		this.label8.Text = "meters.";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.Red;
		this.label7.Location = new System.Drawing.Point(6, 201);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(52, 16);
		this.label7.TabIndex = 16;
		this.label7.Text = "meters.";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.Red;
		this.label6.Location = new System.Drawing.Point(5, 144);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(52, 16);
		this.label6.TabIndex = 16;
		this.label6.Text = "meters.";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Red;
		this.label4.Location = new System.Drawing.Point(6, 34);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(97, 16);
		this.label4.TabIndex = 16;
		this.label4.Text = "degrees / 30m.";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Red;
		this.label3.Location = new System.Drawing.Point(6, 87);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(157, 16);
		this.label3.TabIndex = 16;
		this.label3.Text = "degrees off the high side.";
		this.textBox_NaviStop.BackColor = System.Drawing.Color.White;
		this.textBox_NaviStop.Location = new System.Drawing.Point(182, 287);
		this.textBox_NaviStop.Name = "textBox_NaviStop";
		this.textBox_NaviStop.ReadOnly = true;
		this.textBox_NaviStop.Size = new System.Drawing.Size(144, 31);
		this.textBox_NaviStop.TabIndex = 11;
		this.textBox_NaviStop.TabStop = false;
		this.textBox_NaviStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textBox_NaviStart.BackColor = System.Drawing.Color.White;
		this.textBox_NaviStart.Location = new System.Drawing.Point(182, 229);
		this.textBox_NaviStart.Name = "textBox_NaviStart";
		this.textBox_NaviStart.ReadOnly = true;
		this.textBox_NaviStart.Size = new System.Drawing.Size(144, 31);
		this.textBox_NaviStart.TabIndex = 10;
		this.textBox_NaviStart.TabStop = false;
		this.textBox_NaviStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textBox_StraightDistance.BackColor = System.Drawing.Color.White;
		this.textBox_StraightDistance.Location = new System.Drawing.Point(182, 176);
		this.textBox_StraightDistance.Name = "textBox_StraightDistance";
		this.textBox_StraightDistance.ReadOnly = true;
		this.textBox_StraightDistance.Size = new System.Drawing.Size(144, 31);
		this.textBox_StraightDistance.TabIndex = 15;
		this.textBox_StraightDistance.TabStop = false;
		this.textBox_StraightDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textBox_NaviDistance.BackColor = System.Drawing.Color.White;
		this.textBox_NaviDistance.Location = new System.Drawing.Point(182, 119);
		this.textBox_NaviDistance.Name = "textBox_NaviDistance";
		this.textBox_NaviDistance.ReadOnly = true;
		this.textBox_NaviDistance.Size = new System.Drawing.Size(144, 31);
		this.textBox_NaviDistance.TabIndex = 15;
		this.textBox_NaviDistance.TabStop = false;
		this.textBox_NaviDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textBox_ToolFace.BackColor = System.Drawing.Color.White;
		this.textBox_ToolFace.Location = new System.Drawing.Point(182, 62);
		this.textBox_ToolFace.Name = "textBox_ToolFace";
		this.textBox_ToolFace.ReadOnly = true;
		this.textBox_ToolFace.Size = new System.Drawing.Size(144, 31);
		this.textBox_ToolFace.TabIndex = 14;
		this.textBox_ToolFace.TabStop = false;
		this.textBox_ToolFace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textBox_DogLeg.BackColor = System.Drawing.Color.White;
		this.textBox_DogLeg.Location = new System.Drawing.Point(109, 9);
		this.textBox_DogLeg.Name = "textBox_DogLeg";
		this.textBox_DogLeg.ReadOnly = true;
		this.textBox_DogLeg.Size = new System.Drawing.Size(217, 31);
		this.textBox_DogLeg.TabIndex = 13;
		this.textBox_DogLeg.TabStop = false;
		this.textBox_DogLeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label_NaviStop.AutoSize = true;
		this.label_NaviStop.Location = new System.Drawing.Point(3, 287);
		this.label_NaviStop.Name = "label_NaviStop";
		this.label_NaviStop.Size = new System.Drawing.Size(155, 25);
		this.label_NaviStop.TabIndex = 5;
		this.label_NaviStop.Text = "Build Stop MD:";
		this.label_NaviStart.AutoSize = true;
		this.label_NaviStart.Location = new System.Drawing.Point(4, 232);
		this.label_NaviStart.Name = "label_NaviStart";
		this.label_NaviStart.Size = new System.Drawing.Size(156, 25);
		this.label_NaviStart.TabIndex = 2;
		this.label_NaviStart.Text = "Build Start MD:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(4, 176);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(182, 25);
		this.label2.TabIndex = 3;
		this.label2.Text = "Straight Distance:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 119);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(156, 25);
		this.label1.TabIndex = 3;
		this.label1.Text = "Build Distance:";
		this.label_Toolface.AutoSize = true;
		this.label_Toolface.Location = new System.Drawing.Point(2, 62);
		this.label_Toolface.Name = "label_Toolface";
		this.label_Toolface.Size = new System.Drawing.Size(174, 25);
		this.label_Toolface.TabIndex = 8;
		this.label_Toolface.Text = "Toolface Setting:";
		this.label_Dogleg.AutoSize = true;
		this.label_Dogleg.Location = new System.Drawing.Point(3, 9);
		this.label_Dogleg.Name = "label_Dogleg";
		this.label_Dogleg.Size = new System.Drawing.Size(92, 25);
		this.label_Dogleg.TabIndex = 7;
		this.label_Dogleg.Text = "Dogleg: ";
		this.groupBox1.Controls.Add(this.button_GenerateReport);
		this.groupBox1.Controls.Add(this.button_AdvancedResults);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(3, 502);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(363, 77);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Options";
		this.tabPage_ChartOptions.Controls.Add(this.panel_ScrollChart);
		this.tabPage_ChartOptions.Location = new System.Drawing.Point(4, 34);
		this.tabPage_ChartOptions.Name = "tabPage_ChartOptions";
		this.tabPage_ChartOptions.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_ChartOptions.Size = new System.Drawing.Size(524, 448);
		this.tabPage_ChartOptions.TabIndex = 1;
		this.tabPage_ChartOptions.Text = "Chart Options";
		this.tabPage_ChartOptions.UseVisualStyleBackColor = true;
		this.panel_ScrollChart.AutoScroll = true;
		this.panel_ScrollChart.AutoScrollMinSize = new System.Drawing.Size(0, 450);
		this.panel_ScrollChart.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScrollChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollChart.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollChart.Name = "panel_ScrollChart";
		this.panel_ScrollChart.Size = new System.Drawing.Size(518, 442);
		this.panel_ScrollChart.TabIndex = 1;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(501, 450);
		this.ezyNavHoleChartController1.TabIndex = 0;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Build;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(335, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 55);
		this.panel1.TabIndex = 10;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_BuildToTarget_Results";
		base.Size = new System.Drawing.Size(399, 768);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage_Results.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.panel_Scroll.ResumeLayout(false);
		this.panel_Scroll.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.tabPage_ChartOptions.ResumeLayout(false);
		this.panel_ScrollChart.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_BuildToTarget_Results(EzyNavHoleChart chart, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_chart = chart;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		Visible = false;
	}

	public void Display(BuildAndHoldResults buildAndHold, AbsoluteSurvey lastSurvey, DrillHoleFile drillHoleFile, DrillHoleStatus drillHoleStatus)
	{
		tabControl1.SelectedIndex = 0;
		Initialise();
		m_buildResults = buildAndHold;
		m_lastSurvey = lastSurvey;
		m_drillHoleFile = drillHoleFile;
		m_drillHoleStatus = drillHoleStatus;
		textBox_DogLeg.Text = buildAndHold.DogLeg.ToString();
		textBox_ToolFace.Text = buildAndHold.ToolFaceSetting.ToString();
		textBox_NaviDistance.Text = buildAndHold.NaviDistance.ToString();
		textBox_StraightDistance.Text = buildAndHold.StraightDistance.ToString();
		textBox_NaviStart.Text = buildAndHold.NaviStartMVD.ToString();
		textBox_NaviStop.Text = buildAndHold.NaviStopMVD.ToString();
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		if (m_buildResults.DogLeg >= m_extremeDL)
		{
			textBox_DogLeg.BackColor = Color.Red;
		}
		else if (m_buildResults.DogLeg >= m_severeDL)
		{
			textBox_DogLeg.BackColor = Color.Orange;
		}
		else
		{
			textBox_DogLeg.BackColor = Color.White;
		}
		Visible = true;
		Focus();
	}

	private void Initialise()
	{
		Utility.ClearTextBoxes(panel_Scroll);
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	public void AdvancedDialog()
	{
		Popup_BuildToTarget_Advanced popup_BuildToTarget_Advanced = new Popup_BuildToTarget_Advanced();
		popup_BuildToTarget_Advanced.ShowDialog(m_lastSurvey, m_buildResults.EndOfBuildSurvey, m_buildResults.EndOfHoleSurvey, m_buildResults.ProjectedSurveyData);
	}

	private void ConstructReport()
	{
		if (GetUserInput(out var author, out var notes, out var surveyData, out var buildSurveys, out var charts))
		{
			Popup_WorkingIndicator working = new Popup_WorkingIndicator("Generating Report", "Generating Report", showCancelButton: true, 7);
			AsyncConsOperationsDelegate asyncConsOperationsDelegate = AsyncConsOperations;
			asyncConsOperationsDelegate.BeginInvoke(surveyData, buildSurveys, charts, author, notes, ref working, AsyncConsComplete, asyncConsOperationsDelegate);
			working.ShowDialog();
		}
	}

	private EzyReport.Report AsyncConsOperations(bool surveyData, bool buildSurveys, bool charts, string author, string notes, ref Popup_WorkingIndicator working)
	{
		EzyReport.Report report = new EzyReport.Report("Build To Target Results: " + m_drillHoleFile.HoleID, "User");
		if (UpdateProgress(ref working, "Adding: Hole Details"))
		{
			return null;
		}
		report.AppendPage(new Report_HoleID(m_drillHoleFile, m_drillHoleStatus.SidetrackSurvey, author, notes), "Hold Details");
		if (UpdateProgress(ref working, "Adding: Hole Status"))
		{
			return null;
		}
		report.AppendPage(new Report_HoleStatus(m_drillHoleStatus.SurveyAtTargetTVD, m_drillHoleStatus.EndOfHoleSurvey, m_drillHoleStatus.SidetrackSurvey, m_drillHoleStatus.StraightPathSurvey, m_drillHoleStatus.CurrentDistanceFromTarget, m_drillHoleStatus.DistanceFromTargetAtTargetTVD, m_drillHoleStatus.StraightPathDistance, m_drillHoleStatus.MinimumDLStateString, m_drillHoleStatus.MinimumDLStateColor), "Hole Status");
		if (UpdateProgress(ref working, "Adding: Build To Target Results"))
		{
			return null;
		}
		report.AppendPage(new Report_BuildResults(m_buildResults), "Build To Target Results");
		if (UpdateProgress(ref working, "Adding: Build Surveys"))
		{
			return null;
		}
		if (buildSurveys)
		{
			report.AppendPage(m_buildResults.ProjectedSurveyData, "Build Survey's");
		}
		if (UpdateProgress(ref working, "Adding: Survey Data"))
		{
			return null;
		}
		if (surveyData)
		{
			report.AppendPage(m_drillHoleStatus.AbsoluteSurveyList, "Survey Data");
		}
		if (UpdateProgress(ref working, "Adding: Charts"))
		{
			return null;
		}
		if (charts)
		{
			m_chart.GetImages(out var perspective, out var easting, out var northing, out var top);
			report.AppendPage(new Report_Chart(perspective), "3D Chart View");
			report.AppendPage(new Report_Chart(easting), "Easting Profile View");
			report.AppendPage(new Report_Chart(northing), "Northng Profile View");
			report.AppendPage(new Report_Chart(top), "Top Profile View");
		}
		if (UpdateProgress(ref working, "Generating Preview..."))
		{
			return null;
		}
		return report;
	}

	private bool UpdateProgress(ref Popup_WorkingIndicator working, string stateText)
	{
		Popup_WorkingIndicator.CancelStateArgs cancelState = new Popup_WorkingIndicator.CancelStateArgs();
		working.GetCancelStatus(ref cancelState);
		if (!cancelState.cancelled)
		{
			working.UpdateProgress(stateText, 1);
		}
		return cancelState.cancelled;
	}

	private void AsyncConsComplete(IAsyncResult result)
	{
		AsyncConsOperationsDelegate asyncConsOperationsDelegate = (AsyncConsOperationsDelegate)result.AsyncState;
		Popup_WorkingIndicator working = null;
		EzyReport.Report report = asyncConsOperationsDelegate.EndInvoke(ref working, result);
		if (report != null)
		{
			DisplayGeneratedReport(report, ref working);
			report = null;
			result = null;
		}
		else
		{
			MethodInvoker methodInvoker = DelayedGarbageCollection;
			methodInvoker.BeginInvoke(null, null);
			working.Close();
		}
	}

	private void DisplayGeneratedReport(EzyReport.Report report, ref Popup_WorkingIndicator working)
	{
		if (report != null)
		{
			if (InvokeRequired)
			{
				DisplayGeneratedReportDelegate method = DisplayGeneratedReport;
				BeginInvoke(method, report, working);
				report = null;
				return;
			}
			Popup_ReportPreview popup_ReportPreview = new Popup_ReportPreview();
			popup_ReportPreview.SetReport(report);
			m_driveArrived = popup_ReportPreview.DriveChange;
			m_driveRemoved = popup_ReportPreview.DriveChange;
			working.Close();
			popup_ReportPreview.ShowDialog();
			report = null;
			working = null;
			popup_ReportPreview = null;
			m_driveArrived = null;
			m_driveRemoved = null;
			popup_ReportPreview = null;
			MethodInvoker methodInvoker = DelayedGarbageCollection;
			methodInvoker.BeginInvoke(null, null);
		}
	}

	private bool GetUserInput(out string author, out string notes, out bool surveyData, out bool buildSurveys, out bool charts)
	{
		Popup_Report_UserFields popup_Report_UserFields = new Popup_Report_UserFields();
		Popup_Report_Components popup_Report_Components = new Popup_Report_Components(surveyData: true, buildSurveys: true, extrapolatedSurveys: false, analysedSurveys: false, charts: true);
		Popup_Report_UserFields.UserFieldsDialogResult userFieldsDialogResult = null;
		Popup_Report_Components.ComponentsDialogResult componentsDialogResult = null;
		author = "";
		notes = "";
		surveyData = false;
		buildSurveys = false;
		charts = false;
		int num = 0;
		while (true)
		{
			switch (num)
			{
			case 0:
				userFieldsDialogResult = popup_Report_UserFields.ShowDialog();
				switch (userFieldsDialogResult.ButtonPressed)
				{
				case Popup_Report_UserFields.ButtonPressedEnum.Cancel:
					return false;
				case Popup_Report_UserFields.ButtonPressedEnum.Next:
					num = 1;
					break;
				}
				break;
			case 1:
				componentsDialogResult = popup_Report_Components.ShowDialog();
				switch (componentsDialogResult.ButtonPressed)
				{
				case Popup_Report_Components.ButtonPressedEnum.Cancel:
					return false;
				case Popup_Report_Components.ButtonPressedEnum.Back:
					num = 0;
					break;
				case Popup_Report_Components.ButtonPressedEnum.Next:
					num = 2;
					break;
				}
				break;
			case 2:
				author = userFieldsDialogResult.UserName;
				notes = userFieldsDialogResult.Notes;
				surveyData = componentsDialogResult.SurveyData;
				buildSurveys = componentsDialogResult.BuildSurveys;
				charts = componentsDialogResult.Charts;
				return true;
			}
		}
	}

	private void DelayedGarbageCollection()
	{
		Thread.Sleep(1000);
		GC.Collect();
	}

	public void DriveArrived(string driveLetter)
	{
		if (m_driveArrived != null)
		{
			m_driveArrived(driveLetter);
		}
	}

	public void DriveRemoved()
	{
		if (m_driveRemoved != null)
		{
			m_driveRemoved();
		}
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Finish_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Finish;
		OnButtonPressed(e2);
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Back;
		OnButtonPressed(e2);
	}

	private void button_Advanced_Click(object sender, EventArgs e)
	{
		AdvancedDialog();
	}

	private void button_GenerateReport_Click(object sender, EventArgs e)
	{
		ConstructReport();
	}

	private void button_AdvancedResults_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AdvancedResults);
	}

	private void button_AdvancedResults_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AdvancedResults);
	}

	private void button_GenerateReport_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_GenerateReport);
	}

	private void button_GenerateReport_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_GenerateReport);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
	}

	private void button_Finish_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Finish);
	}

	private void button_Finish_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Finish);
	}

	private void tabControl1_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
	}
}
