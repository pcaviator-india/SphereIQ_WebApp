using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_BuildToTarget_Advanced : Layout_PopupSmall
{
	private IContainer components;

	private GroupBox groupBox_LastReal;

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

	private GroupBox groupBox2;

	private TextBox textBox_EndOfBuild_Easting;

	private TextBox textBox_EndOfBuild_Northing;

	private TextBox textBox_EndOfBuild_TVD;

	private TextBox textBox_EndOfBuild_Azimuth;

	private TextBox textBox_EndOfBuild_Dip;

	private TextBox textBox_EndOfBuild_MD;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private GroupBox groupBox3;

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

	private RibbonMenuButton button_Close;

	private Control_ExtrapolatedDataGrid extrapolatedDataGrid1;

	private Panel panel1;

	private AbsoluteSurvey m_lastSurvey;

	private AbsoluteSurvey m_endOfBuildSurvey;

	private AbsoluteSurvey m_endOfHoleSurvey;

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

	public AbsoluteSurvey EndOfBuildSurvey
	{
		get
		{
			return m_endOfBuildSurvey;
		}
		set
		{
			m_endOfBuildSurvey = value;
		}
	}

	public AbsoluteSurvey EndOfHoleSurvey
	{
		get
		{
			return m_endOfHoleSurvey;
		}
		set
		{
			m_endOfHoleSurvey = value;
		}
	}

	public AbsoluteSurveyList BuildSurveys
	{
		set
		{
			extrapolatedDataGrid1.SetAbsoluteSurveyData(value);
		}
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
		this.groupBox_LastReal = new System.Windows.Forms.GroupBox();
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
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.textBox_EndOfBuild_Easting = new System.Windows.Forms.TextBox();
		this.textBox_EndOfBuild_Northing = new System.Windows.Forms.TextBox();
		this.textBox_EndOfBuild_TVD = new System.Windows.Forms.TextBox();
		this.textBox_EndOfBuild_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_EndOfBuild_Dip = new System.Windows.Forms.TextBox();
		this.textBox_EndOfBuild_MD = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
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
		this.extrapolatedDataGrid1 = new EzyPlan_Interface_V2.Control_ExtrapolatedDataGrid();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_LastReal.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.extrapolatedDataGrid1);
		base.panel_Detail.Controls.Add(this.groupBox_LastReal);
		base.panel_Detail.Controls.Add(this.groupBox3);
		base.panel_Detail.Controls.Add(this.groupBox2);
		base.panel_Detail.Location = new System.Drawing.Point(0, 50);
		base.panel_Detail.Size = new System.Drawing.Size(700, 465);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(310, 24);
		base.label1.Text = "Advanced build to target results:";
		base.panel_Footer.Controls.Add(this.button_Close);
		base.panel_Footer.Location = new System.Drawing.Point(0, 515);
		base.panel_Footer.Size = new System.Drawing.Size(700, 53);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(700, 50);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_Easting);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_Northing);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_TVD);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_Azimuth);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_Dip);
		this.groupBox_LastReal.Controls.Add(this.textBox_LastSurvey_MD);
		this.groupBox_LastReal.Controls.Add(this.label_Easting);
		this.groupBox_LastReal.Controls.Add(this.label_Northing);
		this.groupBox_LastReal.Controls.Add(this.label_TVD);
		this.groupBox_LastReal.Controls.Add(this.label_Azimuth);
		this.groupBox_LastReal.Controls.Add(this.label_Dip);
		this.groupBox_LastReal.Controls.Add(this.label_MD);
		this.groupBox_LastReal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_LastReal.Location = new System.Drawing.Point(7, 3);
		this.groupBox_LastReal.Name = "groupBox_LastReal";
		this.groupBox_LastReal.Size = new System.Drawing.Size(681, 89);
		this.groupBox_LastReal.TabIndex = 1;
		this.groupBox_LastReal.TabStop = false;
		this.groupBox_LastReal.Text = "Last real survey";
		this.textBox_LastSurvey_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Easting.Location = new System.Drawing.Point(540, 52);
		this.textBox_LastSurvey_Easting.Name = "textBox_LastSurvey_Easting";
		this.textBox_LastSurvey_Easting.ReadOnly = true;
		this.textBox_LastSurvey_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_LastSurvey_Easting.TabIndex = 1;
		this.textBox_LastSurvey_Easting.TabStop = false;
		this.textBox_LastSurvey_Easting.Text = "100000.50";
		this.textBox_LastSurvey_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Northing.Location = new System.Drawing.Point(409, 52);
		this.textBox_LastSurvey_Northing.Name = "textBox_LastSurvey_Northing";
		this.textBox_LastSurvey_Northing.ReadOnly = true;
		this.textBox_LastSurvey_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_LastSurvey_Northing.TabIndex = 1;
		this.textBox_LastSurvey_Northing.TabStop = false;
		this.textBox_LastSurvey_Northing.Text = "10000000.50";
		this.textBox_LastSurvey_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_TVD.Location = new System.Drawing.Point(297, 52);
		this.textBox_LastSurvey_TVD.Name = "textBox_LastSurvey_TVD";
		this.textBox_LastSurvey_TVD.ReadOnly = true;
		this.textBox_LastSurvey_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_LastSurvey_TVD.TabIndex = 1;
		this.textBox_LastSurvey_TVD.TabStop = false;
		this.textBox_LastSurvey_TVD.Text = "1000.50";
		this.textBox_LastSurvey_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Azimuth.Location = new System.Drawing.Point(205, 52);
		this.textBox_LastSurvey_Azimuth.Name = "textBox_LastSurvey_Azimuth";
		this.textBox_LastSurvey_Azimuth.ReadOnly = true;
		this.textBox_LastSurvey_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_LastSurvey_Azimuth.TabIndex = 1;
		this.textBox_LastSurvey_Azimuth.TabStop = false;
		this.textBox_LastSurvey_Azimuth.Text = "360.50";
		this.textBox_LastSurvey_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_Dip.Location = new System.Drawing.Point(113, 52);
		this.textBox_LastSurvey_Dip.Name = "textBox_LastSurvey_Dip";
		this.textBox_LastSurvey_Dip.ReadOnly = true;
		this.textBox_LastSurvey_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_LastSurvey_Dip.TabIndex = 1;
		this.textBox_LastSurvey_Dip.TabStop = false;
		this.textBox_LastSurvey_Dip.Text = "90.50";
		this.textBox_LastSurvey_MD.BackColor = System.Drawing.Color.White;
		this.textBox_LastSurvey_MD.Location = new System.Drawing.Point(11, 52);
		this.textBox_LastSurvey_MD.Name = "textBox_LastSurvey_MD";
		this.textBox_LastSurvey_MD.ReadOnly = true;
		this.textBox_LastSurvey_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_LastSurvey_MD.TabIndex = 1;
		this.textBox_LastSurvey_MD.TabStop = false;
		this.textBox_LastSurvey_MD.Text = "10000.50";
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
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_Easting);
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_Northing);
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_TVD);
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_Azimuth);
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_Dip);
		this.groupBox2.Controls.Add(this.textBox_EndOfBuild_MD);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.Location = new System.Drawing.Point(7, 98);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(681, 94);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "End of build survey";
		this.textBox_EndOfBuild_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_Easting.Location = new System.Drawing.Point(540, 56);
		this.textBox_EndOfBuild_Easting.Name = "textBox_EndOfBuild_Easting";
		this.textBox_EndOfBuild_Easting.ReadOnly = true;
		this.textBox_EndOfBuild_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_EndOfBuild_Easting.TabIndex = 1;
		this.textBox_EndOfBuild_Easting.TabStop = false;
		this.textBox_EndOfBuild_Easting.Text = "100000.50";
		this.textBox_EndOfBuild_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_Northing.Location = new System.Drawing.Point(409, 56);
		this.textBox_EndOfBuild_Northing.Name = "textBox_EndOfBuild_Northing";
		this.textBox_EndOfBuild_Northing.ReadOnly = true;
		this.textBox_EndOfBuild_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_EndOfBuild_Northing.TabIndex = 1;
		this.textBox_EndOfBuild_Northing.TabStop = false;
		this.textBox_EndOfBuild_Northing.Text = "10000000.50";
		this.textBox_EndOfBuild_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_TVD.Location = new System.Drawing.Point(297, 56);
		this.textBox_EndOfBuild_TVD.Name = "textBox_EndOfBuild_TVD";
		this.textBox_EndOfBuild_TVD.ReadOnly = true;
		this.textBox_EndOfBuild_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_EndOfBuild_TVD.TabIndex = 1;
		this.textBox_EndOfBuild_TVD.TabStop = false;
		this.textBox_EndOfBuild_TVD.Text = "10000.50";
		this.textBox_EndOfBuild_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_Azimuth.Location = new System.Drawing.Point(205, 56);
		this.textBox_EndOfBuild_Azimuth.Name = "textBox_EndOfBuild_Azimuth";
		this.textBox_EndOfBuild_Azimuth.ReadOnly = true;
		this.textBox_EndOfBuild_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_EndOfBuild_Azimuth.TabIndex = 1;
		this.textBox_EndOfBuild_Azimuth.TabStop = false;
		this.textBox_EndOfBuild_Azimuth.Text = "100.50";
		this.textBox_EndOfBuild_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_Dip.Location = new System.Drawing.Point(113, 56);
		this.textBox_EndOfBuild_Dip.Name = "textBox_EndOfBuild_Dip";
		this.textBox_EndOfBuild_Dip.ReadOnly = true;
		this.textBox_EndOfBuild_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_EndOfBuild_Dip.TabIndex = 1;
		this.textBox_EndOfBuild_Dip.TabStop = false;
		this.textBox_EndOfBuild_Dip.Text = "10.50";
		this.textBox_EndOfBuild_MD.BackColor = System.Drawing.Color.White;
		this.textBox_EndOfBuild_MD.Location = new System.Drawing.Point(11, 56);
		this.textBox_EndOfBuild_MD.Name = "textBox_EndOfBuild_MD";
		this.textBox_EndOfBuild_MD.ReadOnly = true;
		this.textBox_EndOfBuild_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_EndOfBuild_MD.TabIndex = 1;
		this.textBox_EndOfBuild_MD.TabStop = false;
		this.textBox_EndOfBuild_MD.Text = "10000.50";
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
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_Easting);
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_Northing);
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_TVD);
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_Azimuth);
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_Dip);
		this.groupBox3.Controls.Add(this.textBox_EndOfHole_MD);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(7, 198);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(681, 90);
		this.groupBox3.TabIndex = 4;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "End of hole survey";
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
		this.extrapolatedDataGrid1.BackColor = System.Drawing.Color.White;
		this.extrapolatedDataGrid1.Location = new System.Drawing.Point(7, 294);
		this.extrapolatedDataGrid1.Name = "extrapolatedDataGrid1";
		this.extrapolatedDataGrid1.Size = new System.Drawing.Size(681, 173);
		this.extrapolatedDataGrid1.SurveyDataName = "Calculated build surveys";
		this.extrapolatedDataGrid1.TabIndex = 5;
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
		this.button_Close.Location = new System.Drawing.Point(524, 6);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(164, 41);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 0;
		this.button_Close.Text = "Close";
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(636, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 50);
		this.panel1.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(700, 568);
		base.Name = "Popup_BuildToTarget_Advanced";
		this.Text = "Advanced Results";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_LastReal.ResumeLayout(false);
		this.groupBox_LastReal.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_BuildToTarget_Advanced()
	{
		InitializeComponent();
	}

	public void UpdateFields()
	{
		Utility.ClearTextBoxes(this);
		SetLastSruveyFields(m_lastSurvey);
		SetEndOfBuildSurveyFields(m_endOfBuildSurvey);
		SetEndOfHoleSurveyFields(m_endOfHoleSurvey);
	}

	public void SetLastSruveyFields(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_LastSurvey_MD.Text = survey.MD.ToString();
			textBox_LastSurvey_Dip.Text = survey.Dip.ToString();
			textBox_LastSurvey_Azimuth.Text = survey.Azimuth.ToString();
			textBox_LastSurvey_TVD.Text = survey.TVD.ToString();
			textBox_LastSurvey_Northing.Text = survey.Northing.ToString();
			textBox_LastSurvey_Easting.Text = survey.Easting.ToString();
		}
	}

	public void SetEndOfHoleSurveyFields(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_EndOfHole_MD.Text = survey.MD.ToString();
			textBox_EndOfHole_Dip.Text = survey.Dip.ToString();
			textBox_EndOfHole_Azimuth.Text = survey.Azimuth.ToString();
			textBox_EndOfHole_TVD.Text = survey.TVD.ToString();
			textBox_EndOfHole_Northing.Text = survey.Northing.ToString();
			textBox_EndOfHole_Easting.Text = survey.Easting.ToString();
		}
	}

	public void SetEndOfBuildSurveyFields(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_EndOfBuild_MD.Text = survey.MD.ToString();
			textBox_EndOfBuild_Dip.Text = survey.Dip.ToString();
			textBox_EndOfBuild_Azimuth.Text = survey.Azimuth.ToString();
			textBox_EndOfBuild_TVD.Text = survey.TVD.ToString();
			textBox_EndOfBuild_Northing.Text = survey.Northing.ToString();
			textBox_EndOfBuild_Easting.Text = survey.Easting.ToString();
		}
	}

	public DialogResult ShowDialog(AbsoluteSurvey lastSurvey, AbsoluteSurvey endOfBuild, AbsoluteSurvey endOfHole, AbsoluteSurveyList buildSurveys)
	{
		LastSurvey = lastSurvey;
		EndOfBuildSurvey = endOfBuild;
		EndOfHoleSurvey = endOfHole;
		BuildSurveys = buildSurveys;
		UpdateFields();
		return ShowDialog();
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
}
