using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate : Layout_PopupSmall
{
	public class ExtrapolatedSurveyCreatedEventArgs : EventArgs
	{
		private ExtrapolatedAbsoluteSurvey m_lastExtrapolatedSurvey;

		public ExtrapolatedAbsoluteSurvey LastExtrapolatedSurvey
		{
			get
			{
				return m_lastExtrapolatedSurvey;
			}
			set
			{
				m_lastExtrapolatedSurvey = value;
			}
		}

		public ExtrapolatedSurveyCreatedEventArgs(ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
		{
			m_lastExtrapolatedSurvey = new ExtrapolatedAbsoluteSurvey(lastExtrapolatedSurvey);
		}
	}

	private IContainer components;

	private RibbonMenuButton button_AddFinish;

	private RibbonMenuButton button_Close;

	private RibbonMenuButton button_Add;

	private Panel panel1;

	public Label label_subText;

	private AbsoluteSurvey m_lastSurvey;

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

	public event EventHandler ExtrapolatedSurveyCreated;

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
		this.button_AddFinish = new RibbonStyle.RibbonMenuButton();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.button_Add = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label_subText = new System.Windows.Forms.Label();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Location = new System.Drawing.Point(0, 70);
		base.panel_Detail.Size = new System.Drawing.Size(802, 160);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.panel_Footer.Controls.Add(this.button_AddFinish);
		base.panel_Footer.Controls.Add(this.button_Close);
		base.panel_Footer.Controls.Add(this.button_Add);
		base.panel_Footer.Location = new System.Drawing.Point(0, 230);
		base.panel_Footer.Size = new System.Drawing.Size(802, 56);
		base.panel_Header.Controls.Add(this.label_subText);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(802, 70);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Header.Controls.SetChildIndex(this.label_subText, 0);
		this.button_AddFinish.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_AddFinish.AutoEllipsis = true;
		this.button_AddFinish.BackColor = System.Drawing.Color.Transparent;
		this.button_AddFinish.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AddFinish.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.ColorOn = System.Drawing.Color.Orange;
		this.button_AddFinish.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.ColorPress = System.Drawing.Color.Orange;
		this.button_AddFinish.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AddFinish.FadingSpeed = 35;
		this.button_AddFinish.FlatAppearance.BorderSize = 0;
		this.button_AddFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AddFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AddFinish.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AddFinish.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Element;
		this.button_AddFinish.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AddFinish.ImageOffset = 4;
		this.button_AddFinish.IsPressed = false;
		this.button_AddFinish.KeepPress = false;
		this.button_AddFinish.Location = new System.Drawing.Point(378, 6);
		this.button_AddFinish.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AddFinish.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AddFinish.Name = "button_AddFinish";
		this.button_AddFinish.Radius = 10;
		this.button_AddFinish.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AddFinish.Size = new System.Drawing.Size(261, 45);
		this.button_AddFinish.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AddFinish.SplitDistance = 0;
		this.button_AddFinish.TabIndex = 1;
		this.button_AddFinish.Text = "Add and finish";
		this.button_AddFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AddFinish.Title = "";
		this.button_AddFinish.UseVisualStyleBackColor = true;
		this.button_AddFinish.Click += new System.EventHandler(this.button_AddFinish_Click);
		this.button_AddFinish.Leave += new System.EventHandler(this.button_AddFinish_Leave);
		this.button_AddFinish.Enter += new System.EventHandler(this.button_AddFinish_Enter);
		this.button_Close.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Close.AutoEllipsis = true;
		this.button_Close.BackColor = System.Drawing.Color.Transparent;
		this.button_Close.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Close.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Close.ColorOn = System.Drawing.Color.Orange;
		this.button_Close.ColorOnStroke = System.Drawing.Color.Silver;
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
		this.button_Close.ImageOffset = 4;
		this.button_Close.IsPressed = false;
		this.button_Close.KeepPress = false;
		this.button_Close.Location = new System.Drawing.Point(645, 6);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(151, 45);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 2;
		this.button_Close.Text = "Close";
		this.button_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.button_Add.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Add.AutoEllipsis = true;
		this.button_Add.BackColor = System.Drawing.Color.Transparent;
		this.button_Add.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Add.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorOn = System.Drawing.Color.Orange;
		this.button_Add.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorPress = System.Drawing.Color.Orange;
		this.button_Add.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Add.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Add.FadingSpeed = 35;
		this.button_Add.FlatAppearance.BorderSize = 0;
		this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Add.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Add.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.button_Add.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Add.ImageOffset = 4;
		this.button_Add.IsPressed = false;
		this.button_Add.KeepPress = false;
		this.button_Add.Location = new System.Drawing.Point(111, 6);
		this.button_Add.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Add.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Add.Name = "button_Add";
		this.button_Add.Radius = 10;
		this.button_Add.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Add.Size = new System.Drawing.Size(261, 45);
		this.button_Add.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Add.SplitDistance = 0;
		this.button_Add.TabIndex = 0;
		this.button_Add.Text = "Add and enter another";
		this.button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Add.Title = "";
		this.button_Add.UseVisualStyleBackColor = true;
		this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
		this.button_Add.Leave += new System.EventHandler(this.button_Add_Leave);
		this.button_Add.Enter += new System.EventHandler(this.button_Add_Enter);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(738, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 70);
		this.panel1.TabIndex = 3;
		this.label_subText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_subText.Location = new System.Drawing.Point(3, 24);
		this.label_subText.Name = "label_subText";
		this.label_subText.Size = new System.Drawing.Size(729, 40);
		this.label_subText.TabIndex = 4;
		this.label_subText.Text = "Click \"Add and enter another\" to add multiple surveys and \"Add and finish\" to add a single survey.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(802, 286);
		base.Name = "Popup_Extrapolate";
		this.Text = "Popup_Extrapolate";
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_Extrapolate()
	{
		InitializeComponent();
	}

	public Popup_Extrapolate(AbsoluteSurvey lastSurvey)
		: this()
	{
		m_lastSurvey = lastSurvey;
	}

	protected virtual void SaveExtrapolatedSurvey(out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		throw new NotImplementedException();
	}

	private void SetLastSurvey(ExtrapolatedAbsoluteSurvey extrapolatedSurvey)
	{
		m_lastSurvey = new AbsoluteSurvey(extrapolatedSurvey);
	}

	private bool OnAdd()
	{
		SaveExtrapolatedSurvey(out var lastExtrapolatedSurvey);
		if (lastExtrapolatedSurvey != null && ExtrapolatedSurveyCreated != null)
		{
			SetLastSurvey(lastExtrapolatedSurvey);
			ExtrapolatedSurveyCreatedEventArgs e = new ExtrapolatedSurveyCreatedEventArgs(lastExtrapolatedSurvey);
			ExtrapolatedSurveyCreated(null, e);
			return true;
		}
		return false;
	}

	private void OnAddFinish()
	{
		if (OnAdd())
		{
			Close();
		}
	}

	protected virtual void ResetForDataEntry()
	{
	}

	private void button_Add_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Add);
	}

	private void button_Add_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Add);
	}

	private void button_AddFinish_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AddFinish);
	}

	private void button_AddFinish_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AddFinish);
	}

	private void button_Close_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Close);
	}

	private void button_Close_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Close);
	}

	private void button_Add_Click(object sender, EventArgs e)
	{
		OnAdd();
	}

	private void button_AddFinish_Click(object sender, EventArgs e)
	{
		OnAddFinish();
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		Close();
	}
}
