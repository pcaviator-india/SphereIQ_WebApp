using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_Report_Components : Layout_PopupSmall
{
	public class ComponentsDialogResult
	{
		private bool m_surveyData;

		private bool m_buildSurveys;

		private bool m_extrapolatedSurveys;

		private bool m_analysedSurveys;

		private bool m_charts;

		private ButtonPressedEnum m_buttonPressed;

		public bool SurveyData
		{
			get
			{
				return m_surveyData;
			}
			set
			{
				m_surveyData = value;
			}
		}

		public bool BuildSurveys
		{
			get
			{
				return m_buildSurveys;
			}
			set
			{
				m_buildSurveys = value;
			}
		}

		public bool ExtrapolatedSurveys
		{
			get
			{
				return m_extrapolatedSurveys;
			}
			set
			{
				m_extrapolatedSurveys = value;
			}
		}

		public bool AnalysedSurveys
		{
			get
			{
				return m_analysedSurveys;
			}
			set
			{
				m_analysedSurveys = value;
			}
		}

		public bool Charts
		{
			get
			{
				return m_charts;
			}
			set
			{
				m_charts = value;
			}
		}

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

	public enum ButtonPressedEnum
	{
		Cancel,
		Back,
		Next
	}

	private ComponentsDialogResult m_customDialogResult;

	private IContainer components;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Back;

	private GroupBox groupBox_components;

	private CheckBox checkBox_Charts;

	private CheckBox checkBox_ExtrapolatedSurveys;

	private CheckBox checkBox_BuildSurveys;

	private CheckBox checkBox_SurveyData;

	private Panel panel1;

	private CheckBox checkBox_AnalysedSurveys;

	public ComponentsDialogResult CustomDialogResult
	{
		get
		{
			return m_customDialogResult;
		}
		set
		{
			m_customDialogResult = value;
		}
	}

	public Popup_Report_Components(bool surveyData, bool buildSurveys, bool extrapolatedSurveys, bool analysedSurveys, bool charts)
	{
		InitializeComponent();
		checkBox_SurveyData.Visible = surveyData;
		checkBox_BuildSurveys.Visible = buildSurveys;
		checkBox_ExtrapolatedSurveys.Visible = extrapolatedSurveys;
		checkBox_AnalysedSurveys.Visible = analysedSurveys;
		checkBox_Charts.Visible = charts;
		m_customDialogResult = new ComponentsDialogResult();
		m_customDialogResult.ButtonPressed = ButtonPressedEnum.Cancel;
		m_customDialogResult.SurveyData = checkBox_SurveyData.Checked;
		m_customDialogResult.BuildSurveys = checkBox_BuildSurveys.Checked;
		m_customDialogResult.ExtrapolatedSurveys = checkBox_ExtrapolatedSurveys.Checked;
		m_customDialogResult.AnalysedSurveys = checkBox_AnalysedSurveys.Checked;
		m_customDialogResult.Charts = checkBox_Charts.Checked;
	}

	public new ComponentsDialogResult ShowDialog()
	{
		m_customDialogResult.ButtonPressed = ButtonPressedEnum.Cancel;
		base.ShowDialog();
		return m_customDialogResult;
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

	private void OnButtonPressed(ButtonPressedEnum buttonPressed)
	{
		m_customDialogResult.ButtonPressed = buttonPressed;
		m_customDialogResult.SurveyData = checkBox_SurveyData.Checked;
		m_customDialogResult.BuildSurveys = checkBox_BuildSurveys.Checked;
		m_customDialogResult.ExtrapolatedSurveys = checkBox_ExtrapolatedSurveys.Checked;
		m_customDialogResult.Charts = checkBox_Charts.Checked;
		m_customDialogResult.AnalysedSurveys = checkBox_AnalysedSurveys.Checked;
		ResetAndClose();
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Next);
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Back);
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Cancel);
	}

	public void ResetAndClose()
	{
		checkBox_SurveyData.Focus();
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
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.groupBox_components = new System.Windows.Forms.GroupBox();
		this.checkBox_Charts = new System.Windows.Forms.CheckBox();
		this.checkBox_AnalysedSurveys = new System.Windows.Forms.CheckBox();
		this.checkBox_ExtrapolatedSurveys = new System.Windows.Forms.CheckBox();
		this.checkBox_BuildSurveys = new System.Windows.Forms.CheckBox();
		this.checkBox_SurveyData = new System.Windows.Forms.CheckBox();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_components.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.groupBox_components);
		base.panel_Detail.Location = new System.Drawing.Point(0, 55);
		base.panel_Detail.Size = new System.Drawing.Size(734, 267);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(615, 24);
		base.label1.Text = "Select the optional components you wish to include in the report:";
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.button_Back);
		base.panel_Footer.Controls.Add(this.button_Next);
		base.panel_Footer.Location = new System.Drawing.Point(0, 322);
		base.panel_Footer.Size = new System.Drawing.Size(734, 56);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(734, 55);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
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
		this.button_Cancel.Location = new System.Drawing.Point(583, 6);
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
		this.button_Next.Location = new System.Drawing.Point(275, 6);
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
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
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
		this.button_Back.Location = new System.Drawing.Point(429, 6);
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
		this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.groupBox_components.Controls.Add(this.checkBox_Charts);
		this.groupBox_components.Controls.Add(this.checkBox_AnalysedSurveys);
		this.groupBox_components.Controls.Add(this.checkBox_ExtrapolatedSurveys);
		this.groupBox_components.Controls.Add(this.checkBox_BuildSurveys);
		this.groupBox_components.Controls.Add(this.checkBox_SurveyData);
		this.groupBox_components.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_components.Location = new System.Drawing.Point(95, 27);
		this.groupBox_components.Name = "groupBox_components";
		this.groupBox_components.Size = new System.Drawing.Size(343, 211);
		this.groupBox_components.TabIndex = 0;
		this.groupBox_components.TabStop = false;
		this.groupBox_components.Text = "Optional components";
		this.checkBox_Charts.AutoSize = true;
		this.checkBox_Charts.Checked = true;
		this.checkBox_Charts.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox_Charts.Dock = System.Windows.Forms.DockStyle.Top;
		this.checkBox_Charts.Location = new System.Drawing.Point(3, 157);
		this.checkBox_Charts.Name = "checkBox_Charts";
		this.checkBox_Charts.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.checkBox_Charts.Size = new System.Drawing.Size(337, 33);
		this.checkBox_Charts.TabIndex = 4;
		this.checkBox_Charts.Text = "Charts";
		this.checkBox_Charts.UseVisualStyleBackColor = true;
		this.checkBox_AnalysedSurveys.AutoSize = true;
		this.checkBox_AnalysedSurveys.Checked = true;
		this.checkBox_AnalysedSurveys.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox_AnalysedSurveys.Dock = System.Windows.Forms.DockStyle.Top;
		this.checkBox_AnalysedSurveys.Location = new System.Drawing.Point(3, 124);
		this.checkBox_AnalysedSurveys.Name = "checkBox_AnalysedSurveys";
		this.checkBox_AnalysedSurveys.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.checkBox_AnalysedSurveys.Size = new System.Drawing.Size(337, 33);
		this.checkBox_AnalysedSurveys.TabIndex = 3;
		this.checkBox_AnalysedSurveys.Text = "Analysed Survey's";
		this.checkBox_AnalysedSurveys.UseVisualStyleBackColor = true;
		this.checkBox_ExtrapolatedSurveys.AutoSize = true;
		this.checkBox_ExtrapolatedSurveys.Checked = true;
		this.checkBox_ExtrapolatedSurveys.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox_ExtrapolatedSurveys.Dock = System.Windows.Forms.DockStyle.Top;
		this.checkBox_ExtrapolatedSurveys.Location = new System.Drawing.Point(3, 91);
		this.checkBox_ExtrapolatedSurveys.Name = "checkBox_ExtrapolatedSurveys";
		this.checkBox_ExtrapolatedSurveys.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.checkBox_ExtrapolatedSurveys.Size = new System.Drawing.Size(337, 33);
		this.checkBox_ExtrapolatedSurveys.TabIndex = 2;
		this.checkBox_ExtrapolatedSurveys.Text = "Extrapolated Survey's";
		this.checkBox_ExtrapolatedSurveys.UseVisualStyleBackColor = true;
		this.checkBox_BuildSurveys.AutoSize = true;
		this.checkBox_BuildSurveys.Checked = true;
		this.checkBox_BuildSurveys.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox_BuildSurveys.Dock = System.Windows.Forms.DockStyle.Top;
		this.checkBox_BuildSurveys.Location = new System.Drawing.Point(3, 58);
		this.checkBox_BuildSurveys.Name = "checkBox_BuildSurveys";
		this.checkBox_BuildSurveys.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.checkBox_BuildSurveys.Size = new System.Drawing.Size(337, 33);
		this.checkBox_BuildSurveys.TabIndex = 1;
		this.checkBox_BuildSurveys.Text = "Build Survey's";
		this.checkBox_BuildSurveys.UseVisualStyleBackColor = true;
		this.checkBox_SurveyData.AutoSize = true;
		this.checkBox_SurveyData.Checked = true;
		this.checkBox_SurveyData.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox_SurveyData.Dock = System.Windows.Forms.DockStyle.Top;
		this.checkBox_SurveyData.Location = new System.Drawing.Point(3, 25);
		this.checkBox_SurveyData.Name = "checkBox_SurveyData";
		this.checkBox_SurveyData.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.checkBox_SurveyData.Size = new System.Drawing.Size(337, 33);
		this.checkBox_SurveyData.TabIndex = 0;
		this.checkBox_SurveyData.Text = "Survey Data";
		this.checkBox_SurveyData.UseVisualStyleBackColor = true;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(670, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 55);
		this.panel1.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(734, 378);
		base.Name = "Popup_Report_Components";
		this.Text = "Report components";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_components.ResumeLayout(false);
		this.groupBox_components.PerformLayout();
		base.ResumeLayout(false);
	}
}
