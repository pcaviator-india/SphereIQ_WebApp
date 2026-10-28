using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_MessageBox_YesNo : Layout_Popup_Message
{
	public enum DialogResultEnum
	{
		Yes,
		No
	}

	private DialogResultEnum m_dialogResult;

	private IContainer components;

	private RibbonMenuButton button_No;

	private RibbonMenuButton button_Yes;

	private Panel panel1;

	private Panel panel2;

	public DialogResultEnum CustomDialogResult
	{
		get
		{
			return m_dialogResult;
		}
		set
		{
			m_dialogResult = value;
		}
	}

	public Popup_MessageBox_YesNo()
	{
		InitializeComponent();
	}

	public Popup_MessageBox_YesNo(string title, string text)
		: this()
	{
		Text = title;
		richTextBox_text.Text = text;
	}

	private void button_Yes_Click(object sender, EventArgs e)
	{
		CustomDialogResult = DialogResultEnum.Yes;
		Close();
	}

	private void button_No_Click(object sender, EventArgs e)
	{
		CustomDialogResult = DialogResultEnum.No;
		Close();
	}

	private void button_No_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_No);
	}

	private void button_No_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_No);
	}

	private void button_Yes_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Yes);
	}

	private void button_Yes_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Yes);
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
		this.button_Yes = new RibbonStyle.RibbonMenuButton();
		this.button_No = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Footer.SuspendLayout();
		base.panel_Detail.SuspendLayout();
		base.SuspendLayout();
		base.panel_Footer.Controls.Add(this.button_No);
		base.panel_Footer.Controls.Add(this.button_Yes);
		base.panel_Footer.Location = new System.Drawing.Point(0, 99);
		base.panel_Footer.Size = new System.Drawing.Size(755, 56);
		base.panel_Detail.Controls.Add(this.panel2);
		base.panel_Detail.Controls.Add(this.panel1);
		base.panel_Detail.Padding = new System.Windows.Forms.Padding(10);
		base.panel_Detail.Size = new System.Drawing.Size(755, 99);
		base.panel_Detail.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Detail.Controls.SetChildIndex(this.panel2, 0);
		base.panel_Detail.Controls.SetChildIndex(base.richTextBox_text, 0);
		base.richTextBox_text.Location = new System.Drawing.Point(84, 10);
		base.richTextBox_text.Size = new System.Drawing.Size(659, 77);
		this.button_Yes.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Yes.AutoEllipsis = true;
		this.button_Yes.BackColor = System.Drawing.Color.Transparent;
		this.button_Yes.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Yes.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Yes.ColorOn = System.Drawing.Color.Orange;
		this.button_Yes.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Yes.ColorPress = System.Drawing.Color.Orange;
		this.button_Yes.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Yes.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Yes.FadingSpeed = 35;
		this.button_Yes.FlatAppearance.BorderSize = 0;
		this.button_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Yes.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Yes.Image = EzyPlan_Interface_V2.Properties.Resources.OK;
		this.button_Yes.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Yes.ImageOffset = 0;
		this.button_Yes.IsPressed = false;
		this.button_Yes.KeepPress = false;
		this.button_Yes.Location = new System.Drawing.Point(486, 6);
		this.button_Yes.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Yes.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Yes.Name = "button_Yes";
		this.button_Yes.Radius = 10;
		this.button_Yes.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Yes.Size = new System.Drawing.Size(130, 45);
		this.button_Yes.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Yes.SplitDistance = 0;
		this.button_Yes.TabIndex = 0;
		this.button_Yes.Text = "Yes";
		this.button_Yes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Yes.Title = "";
		this.button_Yes.UseVisualStyleBackColor = true;
		this.button_Yes.Click += new System.EventHandler(this.button_Yes_Click);
		this.button_Yes.Leave += new System.EventHandler(this.button_Yes_Leave);
		this.button_Yes.Enter += new System.EventHandler(this.button_Yes_Enter);
		this.button_No.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_No.AutoEllipsis = true;
		this.button_No.BackColor = System.Drawing.Color.Transparent;
		this.button_No.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_No.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_No.ColorOn = System.Drawing.Color.Orange;
		this.button_No.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_No.ColorPress = System.Drawing.Color.Orange;
		this.button_No.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_No.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_No.FadingSpeed = 35;
		this.button_No.FlatAppearance.BorderSize = 0;
		this.button_No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_No.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_No.Image = EzyPlan_Interface_V2.Properties.Resources.No;
		this.button_No.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_No.ImageOffset = 0;
		this.button_No.IsPressed = false;
		this.button_No.KeepPress = false;
		this.button_No.Location = new System.Drawing.Point(622, 6);
		this.button_No.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_No.MenuPos = new System.Drawing.Point(0, 0);
		this.button_No.Name = "button_No";
		this.button_No.Radius = 10;
		this.button_No.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_No.Size = new System.Drawing.Size(130, 45);
		this.button_No.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_No.SplitDistance = 0;
		this.button_No.TabIndex = 1;
		this.button_No.Text = "No";
		this.button_No.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_No.Title = "";
		this.button_No.UseVisualStyleBackColor = true;
		this.button_No.Click += new System.EventHandler(this.button_No_Click);
		this.button_No.Leave += new System.EventHandler(this.button_No_Leave);
		this.button_No.Enter += new System.EventHandler(this.button_No_Enter);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel2.Location = new System.Drawing.Point(74, 10);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 77);
		this.panel2.TabIndex = 5;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Question;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(10, 10);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 77);
		this.panel1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(755, 155);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "Popup_MessageBox_YesNo";
		base.panel_Footer.ResumeLayout(false);
		base.panel_Detail.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
