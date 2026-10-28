using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate_Advanced : Layout_PopupSmall
{
	private IContainer components;

	private GroupBox groupBox_Target;

	private TextBox textBox_Target_Easting;

	private TextBox textBox_Target_Northing;

	private TextBox textBox_Target_TVD;

	private Label label8;

	private Label label9;

	private Label label10;

	private GroupBox groupBox_EndExtrapolation;

	private TextBox textBox_Ext_Easting;

	private TextBox textBox_Ext_Northing;

	private TextBox textBox_Ext_TVD;

	private TextBox textBox_Ext_Azi;

	private TextBox textBox_Ext_Dip;

	private TextBox textBox_Ext_MD;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private RibbonMenuButton button_Close;

	private GroupBox groupBox_StraightPath;

	private TextBox textBox_Str_Easting;

	private TextBox textBox_Str_Northing;

	private TextBox textBox_Str_TVD;

	private TextBox textBox_Str_Azi;

	private TextBox textBox_Str_Dip;

	private TextBox textBox_Str_MD;

	private Label label11;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label15;

	private Label label16;

	private GroupBox groupBox_TargetTVDDistance;

	private TextBox textBox_TargetTVDDistance_Easting;

	private TextBox textBox_TargetTVDDistance_Northing;

	private TextBox textBox_TargetTVDDistance_TVD;

	private TextBox textBox_TargetTVDDistance_Azimuth;

	private TextBox textBox_TargetTVDDistance_Dip;

	private TextBox textBox_TargetTVDDistance_MD;

	private Label label17;

	private Label label18;

	private Label label19;

	private Label label20;

	private Label label21;

	private Label label22;

	private Panel panel2;

	private Panel panel1;

	private Panel panel3;

	private Control_ExtrapolatedDataGrid extrapolatedDataGrid1;

	public AbsoluteSurveyList ExtrapolatedSurveyData
	{
		set
		{
			extrapolatedDataGrid1.SetAbsoluteSurveyData(value);
		}
	}

	public Popup_Extrapolate_Advanced()
	{
		InitializeComponent();
	}

	public Popup_Extrapolate_Advanced(AbsoluteSurvey endOfExtrapolate, AbsoluteSurvey endOfStraightPath, Target target, AbsoluteSurvey surveyATTargetTVD, AbsoluteSurveyList extrapolatedSurveyData)
		: this()
	{
		UpdateFields(endOfExtrapolate, endOfStraightPath, target, surveyATTargetTVD, extrapolatedSurveyData);
	}

	private void UpdateFields(AbsoluteSurvey endOfExtrapolate, AbsoluteSurvey endOfStraightPath, Target target, AbsoluteSurvey surveyATTargetTVD, AbsoluteSurveyList extrapolatedSurveyData)
	{
		SetEndOfExtrapolate(endOfExtrapolate);
		SetEndOfStraightPath(endOfStraightPath);
		SetSurveyAtTargetTVD(surveyATTargetTVD);
		ExtrapolatedSurveyData = extrapolatedSurveyData;
		SetTarget(target);
	}

	private void SetSurveyAtTargetTVD(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_TargetTVDDistance_MD.Text = survey.MD.ToString();
			textBox_TargetTVDDistance_Dip.Text = survey.Dip.ToString();
			textBox_TargetTVDDistance_Azimuth.Text = survey.Azimuth.ToString();
			textBox_TargetTVDDistance_TVD.Text = survey.TVD.ToString();
			textBox_TargetTVDDistance_Northing.Text = survey.Northing.ToString();
			textBox_TargetTVDDistance_Easting.Text = survey.Easting.ToString();
			groupBox_TargetTVDDistance.Visible = true;
		}
		else
		{
			groupBox_TargetTVDDistance.Visible = false;
		}
	}

	private void SetEndOfExtrapolate(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_Ext_MD.Text = survey.MD.ToString();
			textBox_Ext_Dip.Text = survey.Dip.ToString();
			textBox_Ext_Azi.Text = survey.Azimuth.ToString();
			textBox_Ext_TVD.Text = survey.TVD.ToString();
			textBox_Ext_Northing.Text = survey.Northing.ToString();
			textBox_Ext_Easting.Text = survey.Easting.ToString();
			groupBox_EndExtrapolation.Visible = true;
		}
		else
		{
			groupBox_EndExtrapolation.Visible = false;
		}
	}

	private void SetEndOfStraightPath(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_Str_MD.Text = survey.MD.ToString();
			textBox_Str_Dip.Text = survey.Dip.ToString();
			textBox_Str_Azi.Text = survey.Azimuth.ToString();
			textBox_Str_TVD.Text = survey.TVD.ToString();
			textBox_Str_Northing.Text = survey.Northing.ToString();
			textBox_Str_Easting.Text = survey.Easting.ToString();
			groupBox_StraightPath.Visible = true;
		}
		else
		{
			groupBox_StraightPath.Visible = false;
		}
	}

	private void SetTarget(Target target)
	{
		if (target != null)
		{
			textBox_Target_TVD.Text = target.TVD.ToString();
			textBox_Target_Northing.Text = target.Northing.ToString();
			textBox_Target_Easting.Text = target.Easting.ToString();
			groupBox_Target.Visible = true;
		}
		else
		{
			groupBox_Target.Visible = false;
		}
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button_Close_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Close);
	}

	private void button_Close_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Close);
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
		this.groupBox_Target = new System.Windows.Forms.GroupBox();
		this.textBox_Target_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Target_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Target_TVD = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox_EndExtrapolation = new System.Windows.Forms.GroupBox();
		this.textBox_Ext_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Ext_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Ext_TVD = new System.Windows.Forms.TextBox();
		this.textBox_Ext_Azi = new System.Windows.Forms.TextBox();
		this.textBox_Ext_Dip = new System.Windows.Forms.TextBox();
		this.textBox_Ext_MD = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.groupBox_StraightPath = new System.Windows.Forms.GroupBox();
		this.textBox_Str_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Str_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Str_TVD = new System.Windows.Forms.TextBox();
		this.textBox_Str_Azi = new System.Windows.Forms.TextBox();
		this.textBox_Str_Dip = new System.Windows.Forms.TextBox();
		this.textBox_Str_MD = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.groupBox_TargetTVDDistance = new System.Windows.Forms.GroupBox();
		this.textBox_TargetTVDDistance_Easting = new System.Windows.Forms.TextBox();
		this.textBox_TargetTVDDistance_Northing = new System.Windows.Forms.TextBox();
		this.textBox_TargetTVDDistance_TVD = new System.Windows.Forms.TextBox();
		this.textBox_TargetTVDDistance_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_TargetTVDDistance_Dip = new System.Windows.Forms.TextBox();
		this.textBox_TargetTVDDistance_MD = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.extrapolatedDataGrid1 = new EzyPlan_Interface_V2.Control_ExtrapolatedDataGrid();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_Target.SuspendLayout();
		this.groupBox_EndExtrapolation.SuspendLayout();
		this.groupBox_StraightPath.SuspendLayout();
		this.groupBox_TargetTVDDistance.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.extrapolatedDataGrid1);
		base.panel_Detail.Controls.Add(this.groupBox_TargetTVDDistance);
		base.panel_Detail.Controls.Add(this.groupBox_Target);
		base.panel_Detail.Controls.Add(this.groupBox_StraightPath);
		base.panel_Detail.Controls.Add(this.groupBox_EndExtrapolation);
		base.panel_Detail.Controls.Add(this.panel1);
		base.panel_Detail.Controls.Add(this.panel2);
		base.panel_Detail.Location = new System.Drawing.Point(0, 43);
		base.panel_Detail.Size = new System.Drawing.Size(758, 466);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(303, 24);
		base.label1.Text = "Advanced extrapolation results:";
		base.panel_Footer.Controls.Add(this.button_Close);
		base.panel_Footer.Location = new System.Drawing.Point(0, 509);
		base.panel_Footer.Size = new System.Drawing.Size(758, 54);
		base.panel_Header.Controls.Add(this.panel3);
		base.panel_Header.Size = new System.Drawing.Size(758, 43);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel3, 0);
		this.groupBox_Target.Controls.Add(this.textBox_Target_Easting);
		this.groupBox_Target.Controls.Add(this.textBox_Target_Northing);
		this.groupBox_Target.Controls.Add(this.textBox_Target_TVD);
		this.groupBox_Target.Controls.Add(this.label8);
		this.groupBox_Target.Controls.Add(this.label9);
		this.groupBox_Target.Controls.Add(this.label10);
		this.groupBox_Target.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Target.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Target.Location = new System.Drawing.Point(11, 190);
		this.groupBox_Target.Name = "groupBox_Target";
		this.groupBox_Target.Size = new System.Drawing.Size(737, 101);
		this.groupBox_Target.TabIndex = 7;
		this.groupBox_Target.TabStop = false;
		this.groupBox_Target.Text = "Target:";
		this.textBox_Target_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Easting.Location = new System.Drawing.Point(254, 62);
		this.textBox_Target_Easting.Name = "textBox_Target_Easting";
		this.textBox_Target_Easting.ReadOnly = true;
		this.textBox_Target_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_Target_Easting.TabIndex = 1;
		this.textBox_Target_Easting.TabStop = false;
		this.textBox_Target_Easting.Text = "100000.50";
		this.textBox_Target_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Northing.Location = new System.Drawing.Point(114, 62);
		this.textBox_Target_Northing.Name = "textBox_Target_Northing";
		this.textBox_Target_Northing.ReadOnly = true;
		this.textBox_Target_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_Target_Northing.TabIndex = 1;
		this.textBox_Target_Northing.TabStop = false;
		this.textBox_Target_Northing.Text = "10000000.50";
		this.textBox_Target_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Target_TVD.Location = new System.Drawing.Point(11, 62);
		this.textBox_Target_TVD.Name = "textBox_Target_TVD";
		this.textBox_Target_TVD.ReadOnly = true;
		this.textBox_Target_TVD.Size = new System.Drawing.Size(86, 29);
		this.textBox_Target_TVD.TabIndex = 1;
		this.textBox_Target_TVD.TabStop = false;
		this.textBox_Target_TVD.Text = "1000.50";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(250, 35);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(72, 24);
		this.label8.TabIndex = 0;
		this.label8.Text = "Easting";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(110, 35);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(82, 24);
		this.label9.TabIndex = 0;
		this.label9.Text = "Northing";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(7, 35);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(48, 24);
		this.label10.TabIndex = 0;
		this.label10.Text = "TVD";
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_Easting);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_Northing);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_TVD);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_Azi);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_Dip);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_Ext_MD);
		this.groupBox_EndExtrapolation.Controls.Add(this.label2);
		this.groupBox_EndExtrapolation.Controls.Add(this.label3);
		this.groupBox_EndExtrapolation.Controls.Add(this.label4);
		this.groupBox_EndExtrapolation.Controls.Add(this.label5);
		this.groupBox_EndExtrapolation.Controls.Add(this.label6);
		this.groupBox_EndExtrapolation.Controls.Add(this.label7);
		this.groupBox_EndExtrapolation.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_EndExtrapolation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_EndExtrapolation.Location = new System.Drawing.Point(11, 0);
		this.groupBox_EndExtrapolation.Name = "groupBox_EndExtrapolation";
		this.groupBox_EndExtrapolation.Size = new System.Drawing.Size(737, 95);
		this.groupBox_EndExtrapolation.TabIndex = 5;
		this.groupBox_EndExtrapolation.TabStop = false;
		this.groupBox_EndExtrapolation.Text = "Survey at end of extrapolation:";
		this.textBox_Ext_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_Easting.Location = new System.Drawing.Point(540, 56);
		this.textBox_Ext_Easting.Name = "textBox_Ext_Easting";
		this.textBox_Ext_Easting.ReadOnly = true;
		this.textBox_Ext_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_Ext_Easting.TabIndex = 1;
		this.textBox_Ext_Easting.TabStop = false;
		this.textBox_Ext_Easting.Text = "100000.50";
		this.textBox_Ext_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_Northing.Location = new System.Drawing.Point(409, 56);
		this.textBox_Ext_Northing.Name = "textBox_Ext_Northing";
		this.textBox_Ext_Northing.ReadOnly = true;
		this.textBox_Ext_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_Ext_Northing.TabIndex = 1;
		this.textBox_Ext_Northing.TabStop = false;
		this.textBox_Ext_Northing.Text = "10000000.50";
		this.textBox_Ext_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_TVD.Location = new System.Drawing.Point(297, 56);
		this.textBox_Ext_TVD.Name = "textBox_Ext_TVD";
		this.textBox_Ext_TVD.ReadOnly = true;
		this.textBox_Ext_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_Ext_TVD.TabIndex = 1;
		this.textBox_Ext_TVD.TabStop = false;
		this.textBox_Ext_TVD.Text = "1000.50";
		this.textBox_Ext_Azi.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_Azi.Location = new System.Drawing.Point(205, 56);
		this.textBox_Ext_Azi.Name = "textBox_Ext_Azi";
		this.textBox_Ext_Azi.ReadOnly = true;
		this.textBox_Ext_Azi.Size = new System.Drawing.Size(74, 29);
		this.textBox_Ext_Azi.TabIndex = 1;
		this.textBox_Ext_Azi.TabStop = false;
		this.textBox_Ext_Azi.Text = "360.50";
		this.textBox_Ext_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_Dip.Location = new System.Drawing.Point(113, 56);
		this.textBox_Ext_Dip.Name = "textBox_Ext_Dip";
		this.textBox_Ext_Dip.ReadOnly = true;
		this.textBox_Ext_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_Ext_Dip.TabIndex = 1;
		this.textBox_Ext_Dip.TabStop = false;
		this.textBox_Ext_Dip.Text = "90.50";
		this.textBox_Ext_MD.BackColor = System.Drawing.Color.White;
		this.textBox_Ext_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_Ext_MD.Name = "textBox_Ext_MD";
		this.textBox_Ext_MD.ReadOnly = true;
		this.textBox_Ext_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_Ext_MD.TabIndex = 1;
		this.textBox_Ext_MD.TabStop = false;
		this.textBox_Ext_MD.Text = "10000.50";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(536, 29);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 24);
		this.label2.TabIndex = 0;
		this.label2.Text = "Easting";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(405, 29);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(82, 24);
		this.label3.TabIndex = 0;
		this.label3.Text = "Northing";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(293, 29);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(48, 24);
		this.label4.TabIndex = 0;
		this.label4.Text = "TVD";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(201, 29);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(78, 24);
		this.label5.TabIndex = 0;
		this.label5.Text = "Azimuth";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(109, 29);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(38, 24);
		this.label6.TabIndex = 0;
		this.label6.Text = "Dip";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(7, 29);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(39, 24);
		this.label7.TabIndex = 0;
		this.label7.Text = "MD";
		this.button_Close.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Close.AutoEllipsis = true;
		this.button_Close.BackColor = System.Drawing.Color.Transparent;
		this.button_Close.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Close.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Close.ColorOn = System.Drawing.Color.Orange;
		this.button_Close.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Close.ColorPress = System.Drawing.Color.Orange;
		this.button_Close.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Close.FadingSpeed = 35;
		this.button_Close.FlatAppearance.BorderSize = 0;
		this.button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Close.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Close.Image = EzyPlan_Interface_V2.Properties.Resources.Close;
		this.button_Close.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Close.ImageOffset = 0;
		this.button_Close.IsPressed = false;
		this.button_Close.KeepPress = false;
		this.button_Close.Location = new System.Drawing.Point(584, 6);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(164, 41);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 38;
		this.button_Close.Text = "Close";
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_Easting);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_Northing);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_TVD);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_Azi);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_Dip);
		this.groupBox_StraightPath.Controls.Add(this.textBox_Str_MD);
		this.groupBox_StraightPath.Controls.Add(this.label11);
		this.groupBox_StraightPath.Controls.Add(this.label12);
		this.groupBox_StraightPath.Controls.Add(this.label13);
		this.groupBox_StraightPath.Controls.Add(this.label14);
		this.groupBox_StraightPath.Controls.Add(this.label15);
		this.groupBox_StraightPath.Controls.Add(this.label16);
		this.groupBox_StraightPath.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_StraightPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_StraightPath.Location = new System.Drawing.Point(11, 95);
		this.groupBox_StraightPath.Name = "groupBox_StraightPath";
		this.groupBox_StraightPath.Size = new System.Drawing.Size(737, 95);
		this.groupBox_StraightPath.TabIndex = 5;
		this.groupBox_StraightPath.TabStop = false;
		this.groupBox_StraightPath.Text = "Survey at end of straight path to target TVD:";
		this.textBox_Str_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Str_Easting.Location = new System.Drawing.Point(540, 56);
		this.textBox_Str_Easting.Name = "textBox_Str_Easting";
		this.textBox_Str_Easting.ReadOnly = true;
		this.textBox_Str_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_Str_Easting.TabIndex = 1;
		this.textBox_Str_Easting.TabStop = false;
		this.textBox_Str_Easting.Text = "100000.50";
		this.textBox_Str_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Str_Northing.Location = new System.Drawing.Point(409, 56);
		this.textBox_Str_Northing.Name = "textBox_Str_Northing";
		this.textBox_Str_Northing.ReadOnly = true;
		this.textBox_Str_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_Str_Northing.TabIndex = 1;
		this.textBox_Str_Northing.TabStop = false;
		this.textBox_Str_Northing.Text = "10000000.50";
		this.textBox_Str_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Str_TVD.Location = new System.Drawing.Point(297, 56);
		this.textBox_Str_TVD.Name = "textBox_Str_TVD";
		this.textBox_Str_TVD.ReadOnly = true;
		this.textBox_Str_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_Str_TVD.TabIndex = 1;
		this.textBox_Str_TVD.TabStop = false;
		this.textBox_Str_TVD.Text = "1000.50";
		this.textBox_Str_Azi.BackColor = System.Drawing.Color.White;
		this.textBox_Str_Azi.Location = new System.Drawing.Point(205, 56);
		this.textBox_Str_Azi.Name = "textBox_Str_Azi";
		this.textBox_Str_Azi.ReadOnly = true;
		this.textBox_Str_Azi.Size = new System.Drawing.Size(74, 29);
		this.textBox_Str_Azi.TabIndex = 1;
		this.textBox_Str_Azi.TabStop = false;
		this.textBox_Str_Azi.Text = "360.50";
		this.textBox_Str_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Str_Dip.Location = new System.Drawing.Point(113, 56);
		this.textBox_Str_Dip.Name = "textBox_Str_Dip";
		this.textBox_Str_Dip.ReadOnly = true;
		this.textBox_Str_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_Str_Dip.TabIndex = 1;
		this.textBox_Str_Dip.TabStop = false;
		this.textBox_Str_Dip.Text = "90.50";
		this.textBox_Str_MD.BackColor = System.Drawing.Color.White;
		this.textBox_Str_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_Str_MD.Name = "textBox_Str_MD";
		this.textBox_Str_MD.ReadOnly = true;
		this.textBox_Str_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_Str_MD.TabIndex = 1;
		this.textBox_Str_MD.TabStop = false;
		this.textBox_Str_MD.Text = "10000.50";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(536, 29);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(72, 24);
		this.label11.TabIndex = 0;
		this.label11.Text = "Easting";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(405, 29);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(82, 24);
		this.label12.TabIndex = 0;
		this.label12.Text = "Northing";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(293, 29);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(48, 24);
		this.label13.TabIndex = 0;
		this.label13.Text = "TVD";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(201, 29);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(78, 24);
		this.label14.TabIndex = 0;
		this.label14.Text = "Azimuth";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(109, 29);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(38, 24);
		this.label15.TabIndex = 0;
		this.label15.Text = "Dip";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(7, 29);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(39, 24);
		this.label16.TabIndex = 0;
		this.label16.Text = "MD";
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_Easting);
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_Northing);
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_TVD);
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_Azimuth);
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_Dip);
		this.groupBox_TargetTVDDistance.Controls.Add(this.textBox_TargetTVDDistance_MD);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label17);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label18);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label19);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label20);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label21);
		this.groupBox_TargetTVDDistance.Controls.Add(this.label22);
		this.groupBox_TargetTVDDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_TargetTVDDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_TargetTVDDistance.Location = new System.Drawing.Point(11, 291);
		this.groupBox_TargetTVDDistance.Name = "groupBox_TargetTVDDistance";
		this.groupBox_TargetTVDDistance.Size = new System.Drawing.Size(737, 94);
		this.groupBox_TargetTVDDistance.TabIndex = 8;
		this.groupBox_TargetTVDDistance.TabStop = false;
		this.groupBox_TargetTVDDistance.Text = "Extrapolated survey at target TVD:";
		this.textBox_TargetTVDDistance_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_Easting.Location = new System.Drawing.Point(540, 56);
		this.textBox_TargetTVDDistance_Easting.Name = "textBox_TargetTVDDistance_Easting";
		this.textBox_TargetTVDDistance_Easting.ReadOnly = true;
		this.textBox_TargetTVDDistance_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_TargetTVDDistance_Easting.TabIndex = 1;
		this.textBox_TargetTVDDistance_Easting.TabStop = false;
		this.textBox_TargetTVDDistance_Easting.Text = "100000.50";
		this.textBox_TargetTVDDistance_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_Northing.Location = new System.Drawing.Point(409, 56);
		this.textBox_TargetTVDDistance_Northing.Name = "textBox_TargetTVDDistance_Northing";
		this.textBox_TargetTVDDistance_Northing.ReadOnly = true;
		this.textBox_TargetTVDDistance_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_TargetTVDDistance_Northing.TabIndex = 1;
		this.textBox_TargetTVDDistance_Northing.TabStop = false;
		this.textBox_TargetTVDDistance_Northing.Text = "10000000.50";
		this.textBox_TargetTVDDistance_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_TVD.Location = new System.Drawing.Point(297, 56);
		this.textBox_TargetTVDDistance_TVD.Name = "textBox_TargetTVDDistance_TVD";
		this.textBox_TargetTVDDistance_TVD.ReadOnly = true;
		this.textBox_TargetTVDDistance_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_TargetTVDDistance_TVD.TabIndex = 1;
		this.textBox_TargetTVDDistance_TVD.TabStop = false;
		this.textBox_TargetTVDDistance_TVD.Text = "10000.50";
		this.textBox_TargetTVDDistance_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_Azimuth.Location = new System.Drawing.Point(205, 56);
		this.textBox_TargetTVDDistance_Azimuth.Name = "textBox_TargetTVDDistance_Azimuth";
		this.textBox_TargetTVDDistance_Azimuth.ReadOnly = true;
		this.textBox_TargetTVDDistance_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_TargetTVDDistance_Azimuth.TabIndex = 1;
		this.textBox_TargetTVDDistance_Azimuth.TabStop = false;
		this.textBox_TargetTVDDistance_Azimuth.Text = "100.50";
		this.textBox_TargetTVDDistance_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_Dip.Location = new System.Drawing.Point(113, 56);
		this.textBox_TargetTVDDistance_Dip.Name = "textBox_TargetTVDDistance_Dip";
		this.textBox_TargetTVDDistance_Dip.ReadOnly = true;
		this.textBox_TargetTVDDistance_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_TargetTVDDistance_Dip.TabIndex = 1;
		this.textBox_TargetTVDDistance_Dip.TabStop = false;
		this.textBox_TargetTVDDistance_Dip.Text = "10.50";
		this.textBox_TargetTVDDistance_MD.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVDDistance_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_TargetTVDDistance_MD.Name = "textBox_TargetTVDDistance_MD";
		this.textBox_TargetTVDDistance_MD.ReadOnly = true;
		this.textBox_TargetTVDDistance_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_TargetTVDDistance_MD.TabIndex = 1;
		this.textBox_TargetTVDDistance_MD.TabStop = false;
		this.textBox_TargetTVDDistance_MD.Text = "10000.50";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(536, 29);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(72, 24);
		this.label17.TabIndex = 0;
		this.label17.Text = "Easting";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(405, 29);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(82, 24);
		this.label18.TabIndex = 0;
		this.label18.Text = "Northing";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(293, 25);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(48, 24);
		this.label19.TabIndex = 0;
		this.label19.Text = "TVD";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(201, 25);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(78, 24);
		this.label20.TabIndex = 0;
		this.label20.Text = "Azimuth";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(109, 25);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(38, 24);
		this.label21.TabIndex = 0;
		this.label21.Text = "Dip";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(7, 29);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(39, 24);
		this.label22.TabIndex = 0;
		this.label22.Text = "MD";
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(11, 466);
		this.panel1.TabIndex = 9;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(748, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 466);
		this.panel2.TabIndex = 10;
		this.panel3.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(694, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(64, 43);
		this.panel3.TabIndex = 2;
		this.extrapolatedDataGrid1.BackColor = System.Drawing.Color.White;
		this.extrapolatedDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.extrapolatedDataGrid1.Location = new System.Drawing.Point(11, 385);
		this.extrapolatedDataGrid1.Name = "extrapolatedDataGrid1";
		this.extrapolatedDataGrid1.Size = new System.Drawing.Size(737, 81);
		this.extrapolatedDataGrid1.SurveyDataName = "Extrapolated survey data";
		this.extrapolatedDataGrid1.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(758, 563);
		base.Name = "Popup_Extrapolate_Advanced";
		this.Text = "Advanced Results";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_Target.ResumeLayout(false);
		this.groupBox_Target.PerformLayout();
		this.groupBox_EndExtrapolation.ResumeLayout(false);
		this.groupBox_EndExtrapolation.PerformLayout();
		this.groupBox_StraightPath.ResumeLayout(false);
		this.groupBox_StraightPath.PerformLayout();
		this.groupBox_TargetTVDDistance.ResumeLayout(false);
		this.groupBox_TargetTVDDistance.PerformLayout();
		base.ResumeLayout(false);
	}
}
