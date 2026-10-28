using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_TrendAnalysis_Advanced : Layout_PopupSmall
{
	private AbsoluteSurvey m_startSurvey;

	private AbsoluteSurvey m_endSurvey;

	private Target m_target;

	private IContainer components;

	private GroupBox groupBox_EndExtrapolation;

	private TextBox textBox_End_Easting;

	private TextBox textBox_End_Northing;

	private TextBox textBox_End_TVD;

	private TextBox textBox_End_Azi;

	private TextBox textBox_End_Dip;

	private TextBox textBox_End_MD;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private GroupBox groupBox_StartExtrapolate;

	private TextBox textBox_Start_Easting;

	private TextBox textBox_Start_Northing;

	private TextBox textBox_Start_TVD;

	private TextBox textBox_Start_Azimuth;

	private TextBox textBox_Start_Dip;

	private TextBox textBox_Start_MD;

	private Label label_Easting;

	private Label label_Northing;

	private Label label_TVD;

	private Label label_Azimuth;

	private Label label_Dip;

	private Label label_MD;

	private RibbonMenuButton button_Close;

	private Control_ExtrapolatedDataGrid extrapolatedDataGrid1;

	private Panel panel1;

	public AbsoluteSurvey StartSurvey
	{
		get
		{
			return m_startSurvey;
		}
		set
		{
			m_startSurvey = value;
		}
	}

	public AbsoluteSurvey EndSurvey
	{
		get
		{
			return m_endSurvey;
		}
		set
		{
			m_endSurvey = value;
		}
	}

	public Target Target
	{
		get
		{
			return m_target;
		}
		set
		{
			m_target = value;
		}
	}

	public AbsoluteSurveyList ExtrapolatedSurveyData
	{
		set
		{
			extrapolatedDataGrid1.SetAbsoluteSurveyData(value);
		}
	}

	public Popup_TrendAnalysis_Advanced()
	{
		InitializeComponent();
	}

	public void UpdateFields()
	{
		Utility.ClearTextBoxes(this);
		SetStartSruveyFields(m_startSurvey);
		SetEndSurveyFields(m_endSurvey);
		SetTargetFields(m_target);
	}

	public void SetStartSruveyFields(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_Start_MD.Text = survey.MD.ToString();
			textBox_Start_Dip.Text = survey.Dip.ToString();
			textBox_Start_Azimuth.Text = survey.Azimuth.ToString();
			textBox_Start_TVD.Text = survey.TVD.ToString();
			textBox_Start_Northing.Text = survey.Northing.ToString();
			textBox_Start_Easting.Text = survey.Easting.ToString();
		}
	}

	public void SetEndSurveyFields(AbsoluteSurvey survey)
	{
		if (survey != null)
		{
			textBox_End_MD.Text = survey.MD.ToString();
			textBox_End_Dip.Text = survey.Dip.ToString();
			textBox_End_Azi.Text = survey.Azimuth.ToString();
			textBox_End_TVD.Text = survey.TVD.ToString();
			textBox_End_Northing.Text = survey.Northing.ToString();
			textBox_End_Easting.Text = survey.Easting.ToString();
		}
	}

	public void SetTargetFields(Target target)
	{
	}

	public DialogResult ShowDialog(AbsoluteSurvey startSurvey, AbsoluteSurvey endSurvey, AbsoluteSurveyList extrapolatedData)
	{
		StartSurvey = startSurvey;
		EndSurvey = endSurvey;
		ExtrapolatedSurveyData = extrapolatedData;
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
		this.groupBox_StartExtrapolate = new System.Windows.Forms.GroupBox();
		this.textBox_Start_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Start_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Start_TVD = new System.Windows.Forms.TextBox();
		this.textBox_Start_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_Start_Dip = new System.Windows.Forms.TextBox();
		this.textBox_Start_MD = new System.Windows.Forms.TextBox();
		this.label_Easting = new System.Windows.Forms.Label();
		this.label_Northing = new System.Windows.Forms.Label();
		this.label_TVD = new System.Windows.Forms.Label();
		this.label_Azimuth = new System.Windows.Forms.Label();
		this.label_Dip = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		this.groupBox_EndExtrapolation = new System.Windows.Forms.GroupBox();
		this.textBox_End_Easting = new System.Windows.Forms.TextBox();
		this.textBox_End_Northing = new System.Windows.Forms.TextBox();
		this.textBox_End_TVD = new System.Windows.Forms.TextBox();
		this.textBox_End_Azi = new System.Windows.Forms.TextBox();
		this.textBox_End_Dip = new System.Windows.Forms.TextBox();
		this.textBox_End_MD = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.extrapolatedDataGrid1 = new EzyPlan_Interface_V2.Control_ExtrapolatedDataGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_StartExtrapolate.SuspendLayout();
		this.groupBox_EndExtrapolation.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.extrapolatedDataGrid1);
		base.panel_Detail.Controls.Add(this.groupBox_EndExtrapolation);
		base.panel_Detail.Controls.Add(this.groupBox_StartExtrapolate);
		base.panel_Detail.Location = new System.Drawing.Point(0, 50);
		base.panel_Detail.Size = new System.Drawing.Size(777, 478);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(257, 24);
		base.label1.Text = "Advanced analysis results:";
		base.panel_Footer.Controls.Add(this.button_Close);
		base.panel_Footer.Location = new System.Drawing.Point(0, 528);
		base.panel_Footer.Size = new System.Drawing.Size(777, 55);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(777, 50);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_Easting);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_Northing);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_TVD);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_Azimuth);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_Dip);
		this.groupBox_StartExtrapolate.Controls.Add(this.textBox_Start_MD);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_Easting);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_Northing);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_TVD);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_Azimuth);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_Dip);
		this.groupBox_StartExtrapolate.Controls.Add(this.label_MD);
		this.groupBox_StartExtrapolate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_StartExtrapolate.Location = new System.Drawing.Point(7, 3);
		this.groupBox_StartExtrapolate.Name = "groupBox_StartExtrapolate";
		this.groupBox_StartExtrapolate.Size = new System.Drawing.Size(758, 88);
		this.groupBox_StartExtrapolate.TabIndex = 2;
		this.groupBox_StartExtrapolate.TabStop = false;
		this.groupBox_StartExtrapolate.Text = "Start of extrapolation survey";
		this.textBox_Start_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Start_Easting.Location = new System.Drawing.Point(540, 52);
		this.textBox_Start_Easting.Name = "textBox_Start_Easting";
		this.textBox_Start_Easting.ReadOnly = true;
		this.textBox_Start_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_Start_Easting.TabIndex = 1;
		this.textBox_Start_Easting.TabStop = false;
		this.textBox_Start_Easting.Text = "100000.50";
		this.textBox_Start_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Start_Northing.Location = new System.Drawing.Point(409, 52);
		this.textBox_Start_Northing.Name = "textBox_Start_Northing";
		this.textBox_Start_Northing.ReadOnly = true;
		this.textBox_Start_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_Start_Northing.TabIndex = 1;
		this.textBox_Start_Northing.TabStop = false;
		this.textBox_Start_Northing.Text = "10000000.50";
		this.textBox_Start_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Start_TVD.Location = new System.Drawing.Point(297, 52);
		this.textBox_Start_TVD.Name = "textBox_Start_TVD";
		this.textBox_Start_TVD.ReadOnly = true;
		this.textBox_Start_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_Start_TVD.TabIndex = 1;
		this.textBox_Start_TVD.TabStop = false;
		this.textBox_Start_TVD.Text = "1000.50";
		this.textBox_Start_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_Start_Azimuth.Location = new System.Drawing.Point(205, 52);
		this.textBox_Start_Azimuth.Name = "textBox_Start_Azimuth";
		this.textBox_Start_Azimuth.ReadOnly = true;
		this.textBox_Start_Azimuth.Size = new System.Drawing.Size(74, 29);
		this.textBox_Start_Azimuth.TabIndex = 1;
		this.textBox_Start_Azimuth.TabStop = false;
		this.textBox_Start_Azimuth.Text = "360.50";
		this.textBox_Start_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Start_Dip.Location = new System.Drawing.Point(113, 52);
		this.textBox_Start_Dip.Name = "textBox_Start_Dip";
		this.textBox_Start_Dip.ReadOnly = true;
		this.textBox_Start_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_Start_Dip.TabIndex = 1;
		this.textBox_Start_Dip.TabStop = false;
		this.textBox_Start_Dip.Text = "90.50";
		this.textBox_Start_MD.BackColor = System.Drawing.Color.White;
		this.textBox_Start_MD.Location = new System.Drawing.Point(11, 52);
		this.textBox_Start_MD.Name = "textBox_Start_MD";
		this.textBox_Start_MD.ReadOnly = true;
		this.textBox_Start_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_Start_MD.TabIndex = 1;
		this.textBox_Start_MD.TabStop = false;
		this.textBox_Start_MD.Text = "10000.50";
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
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_Easting);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_Northing);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_TVD);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_Azi);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_Dip);
		this.groupBox_EndExtrapolation.Controls.Add(this.textBox_End_MD);
		this.groupBox_EndExtrapolation.Controls.Add(this.label2);
		this.groupBox_EndExtrapolation.Controls.Add(this.label3);
		this.groupBox_EndExtrapolation.Controls.Add(this.label4);
		this.groupBox_EndExtrapolation.Controls.Add(this.label5);
		this.groupBox_EndExtrapolation.Controls.Add(this.label6);
		this.groupBox_EndExtrapolation.Controls.Add(this.label7);
		this.groupBox_EndExtrapolation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_EndExtrapolation.Location = new System.Drawing.Point(7, 97);
		this.groupBox_EndExtrapolation.Name = "groupBox_EndExtrapolation";
		this.groupBox_EndExtrapolation.Size = new System.Drawing.Size(758, 87);
		this.groupBox_EndExtrapolation.TabIndex = 2;
		this.groupBox_EndExtrapolation.TabStop = false;
		this.groupBox_EndExtrapolation.Text = "End of extrapolation survey";
		this.textBox_End_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_End_Easting.Location = new System.Drawing.Point(540, 52);
		this.textBox_End_Easting.Name = "textBox_End_Easting";
		this.textBox_End_Easting.ReadOnly = true;
		this.textBox_End_Easting.Size = new System.Drawing.Size(119, 29);
		this.textBox_End_Easting.TabIndex = 1;
		this.textBox_End_Easting.TabStop = false;
		this.textBox_End_Easting.Text = "100000.50";
		this.textBox_End_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_End_Northing.Location = new System.Drawing.Point(409, 52);
		this.textBox_End_Northing.Name = "textBox_End_Northing";
		this.textBox_End_Northing.ReadOnly = true;
		this.textBox_End_Northing.Size = new System.Drawing.Size(119, 29);
		this.textBox_End_Northing.TabIndex = 1;
		this.textBox_End_Northing.TabStop = false;
		this.textBox_End_Northing.Text = "10000000.50";
		this.textBox_End_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_End_TVD.Location = new System.Drawing.Point(297, 52);
		this.textBox_End_TVD.Name = "textBox_End_TVD";
		this.textBox_End_TVD.ReadOnly = true;
		this.textBox_End_TVD.Size = new System.Drawing.Size(92, 29);
		this.textBox_End_TVD.TabIndex = 1;
		this.textBox_End_TVD.TabStop = false;
		this.textBox_End_TVD.Text = "1000.50";
		this.textBox_End_Azi.BackColor = System.Drawing.Color.White;
		this.textBox_End_Azi.Location = new System.Drawing.Point(205, 52);
		this.textBox_End_Azi.Name = "textBox_End_Azi";
		this.textBox_End_Azi.ReadOnly = true;
		this.textBox_End_Azi.Size = new System.Drawing.Size(74, 29);
		this.textBox_End_Azi.TabIndex = 1;
		this.textBox_End_Azi.TabStop = false;
		this.textBox_End_Azi.Text = "360.50";
		this.textBox_End_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_End_Dip.Location = new System.Drawing.Point(113, 52);
		this.textBox_End_Dip.Name = "textBox_End_Dip";
		this.textBox_End_Dip.ReadOnly = true;
		this.textBox_End_Dip.Size = new System.Drawing.Size(73, 29);
		this.textBox_End_Dip.TabIndex = 1;
		this.textBox_End_Dip.TabStop = false;
		this.textBox_End_Dip.Text = "90.50";
		this.textBox_End_MD.BackColor = System.Drawing.Color.White;
		this.textBox_End_MD.Location = new System.Drawing.Point(11, 52);
		this.textBox_End_MD.Name = "textBox_End_MD";
		this.textBox_End_MD.ReadOnly = true;
		this.textBox_End_MD.Size = new System.Drawing.Size(85, 29);
		this.textBox_End_MD.TabIndex = 1;
		this.textBox_End_MD.TabStop = false;
		this.textBox_End_MD.Text = "10000.50";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(536, 25);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 24);
		this.label2.TabIndex = 0;
		this.label2.Text = "Easting";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(405, 25);
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
		this.label7.Location = new System.Drawing.Point(7, 25);
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
		this.button_Close.Location = new System.Drawing.Point(601, 7);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(164, 40);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 37;
		this.button_Close.Text = "Close";
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.extrapolatedDataGrid1.BackColor = System.Drawing.Color.White;
		this.extrapolatedDataGrid1.Location = new System.Drawing.Point(7, 190);
		this.extrapolatedDataGrid1.Name = "extrapolatedDataGrid1";
		this.extrapolatedDataGrid1.Size = new System.Drawing.Size(758, 275);
		this.extrapolatedDataGrid1.SurveyDataName = "Extrapolated survey data";
		this.extrapolatedDataGrid1.TabIndex = 5;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(713, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 50);
		this.panel1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(777, 583);
		base.Name = "Popup_TrendAnalysis_Advanced";
		this.Text = "Advanced Trend Analysis Results";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_StartExtrapolate.ResumeLayout(false);
		this.groupBox_StartExtrapolate.PerformLayout();
		this.groupBox_EndExtrapolation.ResumeLayout(false);
		this.groupBox_EndExtrapolation.PerformLayout();
		base.ResumeLayout(false);
	}
}
