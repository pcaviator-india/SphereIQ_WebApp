using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_DrillHolePlanning_Advanced : Layout_PopupSmall
{
	private IContainer components;

	private RibbonMenuButton button_Close;

	private GroupBox groupBox_StraightPath;

	private TextBox textBox_StraighPath_Easting;

	private TextBox textBox_StraighPath_Northing;

	private TextBox textBox_StraighPath_TVD;

	private TextBox textBox_StraighPath_Azimuth;

	private TextBox textBox_StraighPath_Dip;

	private TextBox textBox_StraighPath_MD;

	private Label label_Easting;

	private Label label_Northing;

	private Label label_TVD;

	private Label label_Azimuth;

	private Label label_Dip;

	private Label label_MD;

	private GroupBox groupBox_Sidetrack;

	private TextBox textBox_Sidetrack_Easting;

	private TextBox textBox_Sidetrack_Northing;

	private TextBox textBox_Sidetrack_TVD;

	private TextBox textBox_Sidetrack_Azimuth;

	private TextBox textBox_Sidetrack_Dip;

	private TextBox textBox_Sidetrack_MD;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private GroupBox groupBox_EndOfHole;

	private TextBox textBox_EndOfHole_Easting;

	private TextBox textBox_EndOfHole_Northing;

	private TextBox textBox_EndOfHole_TVD;

	private TextBox textBox_EndOfHole_Azimuth;

	private TextBox textBox_EndOfHole_Dip;

	private TextBox textBox_EndOfHole_MD;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private Label label13;

	private Panel panel1;

	private Panel panel2;

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

	private Panel panel3;

	public Popup_DrillHolePlanning_Advanced()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(AbsoluteSurvey endOfHole, AbsoluteSurvey sideTrack, AbsoluteSurvey straightPath, AbsoluteSurvey surveyAtTargetTVD)
	{
		if (straightPath != null)
		{
			SetStraightPath(straightPath);
			groupBox_StraightPath.Visible = true;
		}
		else
		{
			groupBox_StraightPath.Visible = false;
		}
		if (endOfHole != null)
		{
			SetEndOfHole(endOfHole);
			groupBox_EndOfHole.Visible = true;
		}
		else
		{
			groupBox_EndOfHole.Visible = false;
		}
		if (sideTrack != null)
		{
			SetSideTrack(sideTrack);
			groupBox_Sidetrack.Visible = true;
		}
		else
		{
			groupBox_Sidetrack.Visible = false;
		}
		if (surveyAtTargetTVD != null)
		{
			SetSurveyAtTargetTVD(surveyAtTargetTVD);
			groupBox_TargetTVDDistance.Visible = true;
		}
		else
		{
			groupBox_TargetTVDDistance.Visible = false;
		}
		return ShowDialog();
	}

	private void SetSurveyAtTargetTVD(AbsoluteSurvey survey)
	{
		textBox_TargetTVDDistance_MD.Text = survey.MD.ToString();
		textBox_TargetTVDDistance_Dip.Text = survey.Dip.ToString();
		textBox_TargetTVDDistance_Azimuth.Text = survey.Azimuth.ToString();
		textBox_TargetTVDDistance_TVD.Text = survey.TVD.ToString();
		textBox_TargetTVDDistance_Northing.Text = survey.Northing.ToString();
		textBox_TargetTVDDistance_Easting.Text = survey.Easting.ToString();
	}

	private void SetStraightPath(AbsoluteSurvey survey)
	{
		textBox_StraighPath_MD.Text = survey.MD.ToString();
		textBox_StraighPath_Dip.Text = survey.Dip.ToString();
		textBox_StraighPath_Azimuth.Text = survey.Azimuth.ToString();
		textBox_StraighPath_TVD.Text = survey.TVD.ToString();
		textBox_StraighPath_Easting.Text = survey.Easting.ToString();
		textBox_StraighPath_Northing.Text = survey.Northing.ToString();
	}

	private void SetEndOfHole(AbsoluteSurvey survey)
	{
		textBox_EndOfHole_MD.Text = survey.MD.ToString();
		textBox_EndOfHole_Dip.Text = survey.Dip.ToString();
		textBox_EndOfHole_Azimuth.Text = survey.Azimuth.ToString();
		textBox_EndOfHole_TVD.Text = survey.TVD.ToString();
		textBox_EndOfHole_Easting.Text = survey.Easting.ToString();
		textBox_EndOfHole_Northing.Text = survey.Northing.ToString();
	}

	private void SetSideTrack(AbsoluteSurvey survey)
	{
		textBox_Sidetrack_MD.Text = survey.MD.ToString();
		textBox_Sidetrack_Dip.Text = survey.Dip.ToString();
		textBox_Sidetrack_Azimuth.Text = survey.Azimuth.ToString();
		textBox_Sidetrack_TVD.Text = survey.TVD.ToString();
		textBox_Sidetrack_Easting.Text = survey.Easting.ToString();
		textBox_Sidetrack_Northing.Text = survey.Northing.ToString();
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		Close();
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
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.groupBox_StraightPath = new System.Windows.Forms.GroupBox();
		this.textBox_StraighPath_Easting = new System.Windows.Forms.TextBox();
		this.textBox_StraighPath_Northing = new System.Windows.Forms.TextBox();
		this.textBox_StraighPath_TVD = new System.Windows.Forms.TextBox();
		this.textBox_StraighPath_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_StraighPath_Dip = new System.Windows.Forms.TextBox();
		this.textBox_StraighPath_MD = new System.Windows.Forms.TextBox();
		this.label_Easting = new System.Windows.Forms.Label();
		this.label_Northing = new System.Windows.Forms.Label();
		this.label_TVD = new System.Windows.Forms.Label();
		this.label_Azimuth = new System.Windows.Forms.Label();
		this.label_Dip = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		this.groupBox_Sidetrack = new System.Windows.Forms.GroupBox();
		this.textBox_Sidetrack_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Sidetrack_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Sidetrack_TVD = new System.Windows.Forms.TextBox();
		this.textBox_Sidetrack_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_Sidetrack_Dip = new System.Windows.Forms.TextBox();
		this.textBox_Sidetrack_MD = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.groupBox_EndOfHole = new System.Windows.Forms.GroupBox();
		this.textBox_EndOfHole_Easting = new System.Windows.Forms.TextBox();
		this.textBox_EndOfHole_Northing = new System.Windows.Forms.TextBox();
		this.textBox_EndOfHole_TVD = new System.Windows.Forms.TextBox();
		this.textBox_EndOfHole_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_EndOfHole_Dip = new System.Windows.Forms.TextBox();
		this.textBox_EndOfHole_MD = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
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
		this.panel3 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_StraightPath.SuspendLayout();
		this.groupBox_Sidetrack.SuspendLayout();
		this.groupBox_EndOfHole.SuspendLayout();
		this.groupBox_TargetTVDDistance.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.groupBox_TargetTVDDistance);
		base.panel_Detail.Controls.Add(this.groupBox_StraightPath);
		base.panel_Detail.Controls.Add(this.groupBox_Sidetrack);
		base.panel_Detail.Controls.Add(this.groupBox_EndOfHole);
		base.panel_Detail.Controls.Add(this.panel1);
		base.panel_Detail.Controls.Add(this.panel2);
		base.panel_Detail.Location = new System.Drawing.Point(0, 48);
		base.panel_Detail.Size = new System.Drawing.Size(690, 427);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(235, 24);
		base.label1.Text = "Advanced status details:";
		base.panel_Footer.Controls.Add(this.button_Close);
		base.panel_Footer.Location = new System.Drawing.Point(0, 475);
		base.panel_Footer.Size = new System.Drawing.Size(690, 57);
		base.panel_Header.Controls.Add(this.panel3);
		base.panel_Header.Size = new System.Drawing.Size(690, 48);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel3, 0);
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
		this.button_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button_Close.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Close.ImageOffset = 0;
		this.button_Close.IsPressed = false;
		this.button_Close.KeepPress = false;
		this.button_Close.Location = new System.Drawing.Point(516, 6);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(164, 45);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 1;
		this.button_Close.Text = "Close";
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_Easting);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_Northing);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_TVD);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_Azimuth);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_Dip);
		this.groupBox_StraightPath.Controls.Add(this.textBox_StraighPath_MD);
		this.groupBox_StraightPath.Controls.Add(this.label_Easting);
		this.groupBox_StraightPath.Controls.Add(this.label_Northing);
		this.groupBox_StraightPath.Controls.Add(this.label_TVD);
		this.groupBox_StraightPath.Controls.Add(this.label_Azimuth);
		this.groupBox_StraightPath.Controls.Add(this.label_Dip);
		this.groupBox_StraightPath.Controls.Add(this.label_MD);
		this.groupBox_StraightPath.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_StraightPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_StraightPath.Location = new System.Drawing.Point(10, 184);
		this.groupBox_StraightPath.Name = "groupBox_StraightPath";
		this.groupBox_StraightPath.Size = new System.Drawing.Size(670, 89);
		this.groupBox_StraightPath.TabIndex = 4;
		this.groupBox_StraightPath.TabStop = false;
		this.groupBox_StraightPath.Text = "End of straight path to target TVD survey:";
		this.textBox_StraighPath_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_Easting.Location = new System.Drawing.Point(540, 52);
		this.textBox_StraighPath_Easting.Name = "textBox_StraighPath_Easting";
		this.textBox_StraighPath_Easting.ReadOnly = true;
		this.textBox_StraighPath_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_StraighPath_Easting.TabIndex = 1;
		this.textBox_StraighPath_Easting.TabStop = false;
		this.textBox_StraighPath_Easting.Text = "100000.50";
		this.textBox_StraighPath_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_Northing.Location = new System.Drawing.Point(409, 52);
		this.textBox_StraighPath_Northing.Name = "textBox_StraighPath_Northing";
		this.textBox_StraighPath_Northing.ReadOnly = true;
		this.textBox_StraighPath_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_StraighPath_Northing.TabIndex = 1;
		this.textBox_StraighPath_Northing.TabStop = false;
		this.textBox_StraighPath_Northing.Text = "10000000.50";
		this.textBox_StraighPath_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_TVD.Location = new System.Drawing.Point(297, 52);
		this.textBox_StraighPath_TVD.Name = "textBox_StraighPath_TVD";
		this.textBox_StraighPath_TVD.ReadOnly = true;
		this.textBox_StraighPath_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_StraighPath_TVD.TabIndex = 1;
		this.textBox_StraighPath_TVD.TabStop = false;
		this.textBox_StraighPath_TVD.Text = "1000.50";
		this.textBox_StraighPath_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_Azimuth.Location = new System.Drawing.Point(205, 52);
		this.textBox_StraighPath_Azimuth.Name = "textBox_StraighPath_Azimuth";
		this.textBox_StraighPath_Azimuth.ReadOnly = true;
		this.textBox_StraighPath_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_StraighPath_Azimuth.TabIndex = 1;
		this.textBox_StraighPath_Azimuth.TabStop = false;
		this.textBox_StraighPath_Azimuth.Text = "360.50";
		this.textBox_StraighPath_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_Dip.Location = new System.Drawing.Point(113, 52);
		this.textBox_StraighPath_Dip.Name = "textBox_StraighPath_Dip";
		this.textBox_StraighPath_Dip.ReadOnly = true;
		this.textBox_StraighPath_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_StraighPath_Dip.TabIndex = 1;
		this.textBox_StraighPath_Dip.TabStop = false;
		this.textBox_StraighPath_Dip.Text = "90.50";
		this.textBox_StraighPath_MD.BackColor = System.Drawing.Color.White;
		this.textBox_StraighPath_MD.Location = new System.Drawing.Point(11, 52);
		this.textBox_StraighPath_MD.Name = "textBox_StraighPath_MD";
		this.textBox_StraighPath_MD.ReadOnly = true;
		this.textBox_StraighPath_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_StraighPath_MD.TabIndex = 1;
		this.textBox_StraighPath_MD.TabStop = false;
		this.textBox_StraighPath_MD.Text = "10000.50";
		this.label_Easting.AutoSize = true;
		this.label_Easting.Location = new System.Drawing.Point(536, 25);
		this.label_Easting.Name = "label_Easting";
		this.label_Easting.Size = new System.Drawing.Size(72, 24);
		this.label_Easting.TabIndex = 0;
		this.label_Easting.Text = "Easting";
		this.label_Northing.AutoSize = true;
		this.label_Northing.Location = new System.Drawing.Point(405, 25);
		this.label_Northing.Name = "label_Northing";
		this.label_Northing.Size = new System.Drawing.Size(82, 24);
		this.label_Northing.TabIndex = 0;
		this.label_Northing.Text = "Northing";
		this.label_TVD.AutoSize = true;
		this.label_TVD.Location = new System.Drawing.Point(293, 25);
		this.label_TVD.Name = "label_TVD";
		this.label_TVD.Size = new System.Drawing.Size(48, 24);
		this.label_TVD.TabIndex = 0;
		this.label_TVD.Text = "TVD";
		this.label_Azimuth.AutoSize = true;
		this.label_Azimuth.Location = new System.Drawing.Point(201, 25);
		this.label_Azimuth.Name = "label_Azimuth";
		this.label_Azimuth.Size = new System.Drawing.Size(78, 24);
		this.label_Azimuth.TabIndex = 0;
		this.label_Azimuth.Text = "Azimuth";
		this.label_Dip.AutoSize = true;
		this.label_Dip.Location = new System.Drawing.Point(109, 25);
		this.label_Dip.Name = "label_Dip";
		this.label_Dip.Size = new System.Drawing.Size(38, 24);
		this.label_Dip.TabIndex = 0;
		this.label_Dip.Text = "Dip";
		this.label_MD.AutoSize = true;
		this.label_MD.Location = new System.Drawing.Point(7, 25);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(39, 24);
		this.label_MD.TabIndex = 0;
		this.label_MD.Text = "MD";
		this.groupBox_Sidetrack.BackColor = System.Drawing.Color.White;
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_Easting);
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_Northing);
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_TVD);
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_Azimuth);
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_Dip);
		this.groupBox_Sidetrack.Controls.Add(this.textBox_Sidetrack_MD);
		this.groupBox_Sidetrack.Controls.Add(this.label2);
		this.groupBox_Sidetrack.Controls.Add(this.label3);
		this.groupBox_Sidetrack.Controls.Add(this.label4);
		this.groupBox_Sidetrack.Controls.Add(this.label5);
		this.groupBox_Sidetrack.Controls.Add(this.label6);
		this.groupBox_Sidetrack.Controls.Add(this.label7);
		this.groupBox_Sidetrack.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Sidetrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Sidetrack.Location = new System.Drawing.Point(10, 90);
		this.groupBox_Sidetrack.Name = "groupBox_Sidetrack";
		this.groupBox_Sidetrack.Size = new System.Drawing.Size(670, 94);
		this.groupBox_Sidetrack.TabIndex = 5;
		this.groupBox_Sidetrack.TabStop = false;
		this.groupBox_Sidetrack.Text = "Sidetrack survey:";
		this.textBox_Sidetrack_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_Easting.Location = new System.Drawing.Point(540, 56);
		this.textBox_Sidetrack_Easting.Name = "textBox_Sidetrack_Easting";
		this.textBox_Sidetrack_Easting.ReadOnly = true;
		this.textBox_Sidetrack_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_Sidetrack_Easting.TabIndex = 1;
		this.textBox_Sidetrack_Easting.TabStop = false;
		this.textBox_Sidetrack_Easting.Text = "100000.50";
		this.textBox_Sidetrack_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_Northing.Location = new System.Drawing.Point(409, 56);
		this.textBox_Sidetrack_Northing.Name = "textBox_Sidetrack_Northing";
		this.textBox_Sidetrack_Northing.ReadOnly = true;
		this.textBox_Sidetrack_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_Sidetrack_Northing.TabIndex = 1;
		this.textBox_Sidetrack_Northing.TabStop = false;
		this.textBox_Sidetrack_Northing.Text = "10000000.50";
		this.textBox_Sidetrack_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_TVD.Location = new System.Drawing.Point(297, 56);
		this.textBox_Sidetrack_TVD.Name = "textBox_Sidetrack_TVD";
		this.textBox_Sidetrack_TVD.ReadOnly = true;
		this.textBox_Sidetrack_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_Sidetrack_TVD.TabIndex = 1;
		this.textBox_Sidetrack_TVD.TabStop = false;
		this.textBox_Sidetrack_TVD.Text = "10000.50";
		this.textBox_Sidetrack_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_Azimuth.Location = new System.Drawing.Point(205, 56);
		this.textBox_Sidetrack_Azimuth.Name = "textBox_Sidetrack_Azimuth";
		this.textBox_Sidetrack_Azimuth.ReadOnly = true;
		this.textBox_Sidetrack_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_Sidetrack_Azimuth.TabIndex = 1;
		this.textBox_Sidetrack_Azimuth.TabStop = false;
		this.textBox_Sidetrack_Azimuth.Text = "100.50";
		this.textBox_Sidetrack_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_Dip.Location = new System.Drawing.Point(113, 56);
		this.textBox_Sidetrack_Dip.Name = "textBox_Sidetrack_Dip";
		this.textBox_Sidetrack_Dip.ReadOnly = true;
		this.textBox_Sidetrack_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_Sidetrack_Dip.TabIndex = 1;
		this.textBox_Sidetrack_Dip.TabStop = false;
		this.textBox_Sidetrack_Dip.Text = "10.50";
		this.textBox_Sidetrack_MD.BackColor = System.Drawing.Color.White;
		this.textBox_Sidetrack_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_Sidetrack_MD.Name = "textBox_Sidetrack_MD";
		this.textBox_Sidetrack_MD.ReadOnly = true;
		this.textBox_Sidetrack_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_Sidetrack_MD.TabIndex = 1;
		this.textBox_Sidetrack_MD.TabStop = false;
		this.textBox_Sidetrack_MD.Text = "10000.50";
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
		this.label4.Location = new System.Drawing.Point(293, 25);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(48, 24);
		this.label4.TabIndex = 0;
		this.label4.Text = "TVD";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(201, 25);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(78, 24);
		this.label5.TabIndex = 0;
		this.label5.Text = "Azimuth";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(109, 25);
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
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_Easting);
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_Northing);
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_TVD);
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_Azimuth);
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_Dip);
		this.groupBox_EndOfHole.Controls.Add(this.textBox_EndOfHole_MD);
		this.groupBox_EndOfHole.Controls.Add(this.label8);
		this.groupBox_EndOfHole.Controls.Add(this.label9);
		this.groupBox_EndOfHole.Controls.Add(this.label10);
		this.groupBox_EndOfHole.Controls.Add(this.label11);
		this.groupBox_EndOfHole.Controls.Add(this.label12);
		this.groupBox_EndOfHole.Controls.Add(this.label13);
		this.groupBox_EndOfHole.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_EndOfHole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_EndOfHole.Location = new System.Drawing.Point(10, 0);
		this.groupBox_EndOfHole.Name = "groupBox_EndOfHole";
		this.groupBox_EndOfHole.Size = new System.Drawing.Size(670, 90);
		this.groupBox_EndOfHole.TabIndex = 6;
		this.groupBox_EndOfHole.TabStop = false;
		this.groupBox_EndOfHole.Text = "End of hole survey:";
		this.textBox_EndOfHole_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_Easting.Location = new System.Drawing.Point(540, 52);
		this.textBox_EndOfHole_Easting.Name = "textBox_EndOfHole_Easting";
		this.textBox_EndOfHole_Easting.ReadOnly = true;
		this.textBox_EndOfHole_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_EndOfHole_Easting.TabIndex = 1;
		this.textBox_EndOfHole_Easting.TabStop = false;
		this.textBox_EndOfHole_Easting.Text = "100000.50";
		this.textBox_EndOfHole_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_Northing.Location = new System.Drawing.Point(409, 52);
		this.textBox_EndOfHole_Northing.Name = "textBox_EndOfHole_Northing";
		this.textBox_EndOfHole_Northing.ReadOnly = true;
		this.textBox_EndOfHole_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_EndOfHole_Northing.TabIndex = 1;
		this.textBox_EndOfHole_Northing.TabStop = false;
		this.textBox_EndOfHole_Northing.Text = "10000000.50";
		this.textBox_EndOfHole_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_TVD.Location = new System.Drawing.Point(297, 52);
		this.textBox_EndOfHole_TVD.Name = "textBox_EndOfHole_TVD";
		this.textBox_EndOfHole_TVD.ReadOnly = true;
		this.textBox_EndOfHole_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_EndOfHole_TVD.TabIndex = 1;
		this.textBox_EndOfHole_TVD.TabStop = false;
		this.textBox_EndOfHole_TVD.Text = "10000.50";
		this.textBox_EndOfHole_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_Azimuth.Location = new System.Drawing.Point(205, 52);
		this.textBox_EndOfHole_Azimuth.Name = "textBox_EndOfHole_Azimuth";
		this.textBox_EndOfHole_Azimuth.ReadOnly = true;
		this.textBox_EndOfHole_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_EndOfHole_Azimuth.TabIndex = 1;
		this.textBox_EndOfHole_Azimuth.TabStop = false;
		this.textBox_EndOfHole_Azimuth.Text = "100.50";
		this.textBox_EndOfHole_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_Dip.Location = new System.Drawing.Point(113, 52);
		this.textBox_EndOfHole_Dip.Name = "textBox_EndOfHole_Dip";
		this.textBox_EndOfHole_Dip.ReadOnly = true;
		this.textBox_EndOfHole_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_EndOfHole_Dip.TabIndex = 1;
		this.textBox_EndOfHole_Dip.TabStop = false;
		this.textBox_EndOfHole_Dip.Text = "10.50";
		this.textBox_EndOfHole_MD.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfHole_MD.Location = new System.Drawing.Point(11, 52);
		this.textBox_EndOfHole_MD.Name = "textBox_EndOfHole_MD";
		this.textBox_EndOfHole_MD.ReadOnly = true;
		this.textBox_EndOfHole_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_EndOfHole_MD.TabIndex = 1;
		this.textBox_EndOfHole_MD.TabStop = false;
		this.textBox_EndOfHole_MD.Text = "10000.50";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(536, 25);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(72, 24);
		this.label8.TabIndex = 0;
		this.label8.Text = "Easting";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(405, 25);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(82, 24);
		this.label9.TabIndex = 0;
		this.label9.Text = "Northing";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(293, 25);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(48, 24);
		this.label10.TabIndex = 0;
		this.label10.Text = "TVD";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(201, 25);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(78, 24);
		this.label11.TabIndex = 0;
		this.label11.Text = "Azimuth";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(109, 25);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(38, 24);
		this.label12.TabIndex = 0;
		this.label12.Text = "Dip";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(7, 25);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(39, 24);
		this.label13.TabIndex = 0;
		this.label13.Text = "MD";
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 427);
		this.panel1.TabIndex = 7;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(680, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 427);
		this.panel2.TabIndex = 8;
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
		this.groupBox_TargetTVDDistance.Location = new System.Drawing.Point(10, 273);
		this.groupBox_TargetTVDDistance.Name = "groupBox_TargetTVDDistance";
		this.groupBox_TargetTVDDistance.Size = new System.Drawing.Size(670, 94);
		this.groupBox_TargetTVDDistance.TabIndex = 9;
		this.groupBox_TargetTVDDistance.TabStop = false;
		this.groupBox_TargetTVDDistance.Text = "Survey at target TVD:";
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
		this.panel3.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(626, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(64, 48);
		this.panel3.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(690, 532);
		base.Name = "Popup_DrillHolePlanning_Advanced";
		this.Text = "Advanced";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_StraightPath.ResumeLayout(false);
		this.groupBox_StraightPath.PerformLayout();
		this.groupBox_Sidetrack.ResumeLayout(false);
		this.groupBox_Sidetrack.PerformLayout();
		this.groupBox_EndOfHole.ResumeLayout(false);
		this.groupBox_EndOfHole.PerformLayout();
		this.groupBox_TargetTVDDistance.ResumeLayout(false);
		this.groupBox_TargetTVDDistance.PerformLayout();
		base.ResumeLayout(false);
	}
}
