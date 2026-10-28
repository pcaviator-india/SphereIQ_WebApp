using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_MessageBox_OK : Layout_Popup_Message
{
	private IContainer components;

	private RibbonMenuButton button_OK;

	private Panel panel1;

	private Panel panel2;

	public string Title
	{
		get
		{
			return Text;
		}
		set
		{
			Text = value;
		}
	}

	public string DescriptiveText
	{
		get
		{
			return richTextBox_text.Text;
		}
		set
		{
			richTextBox_text.Text = value;
		}
	}

	public Popup_MessageBox_OK()
	{
		InitializeComponent();
	}

	public Popup_MessageBox_OK(string title, string text)
		: this()
	{
		Text = title;
		richTextBox_text.Text = text;
	}

	private void button_OK_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button_OK_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_OK);
	}

	private void button_OK_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_OK);
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
		this.button_OK = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Footer.SuspendLayout();
		base.panel_Detail.SuspendLayout();
		base.SuspendLayout();
		base.panel_Footer.Controls.Add(this.button_OK);
		base.panel_Footer.Location = new System.Drawing.Point(0, 96);
		base.panel_Footer.Size = new System.Drawing.Size(766, 59);
		base.panel_Detail.Controls.Add(this.panel2);
		base.panel_Detail.Controls.Add(this.panel1);
		base.panel_Detail.Size = new System.Drawing.Size(766, 96);
		base.panel_Detail.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Detail.Controls.SetChildIndex(this.panel2, 0);
		base.panel_Detail.Controls.SetChildIndex(base.richTextBox_text, 0);
		base.richTextBox_text.Location = new System.Drawing.Point(89, 15);
		base.richTextBox_text.Size = new System.Drawing.Size(662, 66);
		this.button_OK.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_OK.AutoEllipsis = true;
		this.button_OK.BackColor = System.Drawing.Color.Transparent;
		this.button_OK.ColorBase = System.Drawing.Color.Silver;
		this.button_OK.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_OK.ColorOn = System.Drawing.Color.Orange;
		this.button_OK.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_OK.ColorPress = System.Drawing.Color.Orange;
		this.button_OK.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_OK.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_OK.FadingSpeed = 35;
		this.button_OK.FlatAppearance.BorderSize = 0;
		this.button_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_OK.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_OK.Image = EzyPlan_Interface_V2.Properties.Resources.OK;
		this.button_OK.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_OK.ImageOffset = 0;
		this.button_OK.Location = new System.Drawing.Point(615, 6);
		this.button_OK.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_OK.Name = "button_OK";
		this.button_OK.Radius = 10;
		this.button_OK.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_OK.Size = new System.Drawing.Size(148, 45);
		this.button_OK.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_OK.SplitDistance = 0;
		this.button_OK.TabIndex = 33;
		this.button_OK.Text = "OK";
		this.button_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_OK.Title = "";
		this.button_OK.UseVisualStyleBackColor = true;
		this.button_OK.Enter += new System.EventHandler(this.button_OK_Enter);
		this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
		this.button_OK.Leave += new System.EventHandler(this.button_OK_Leave);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel2.Location = new System.Drawing.Point(79, 15);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 66);
		this.panel2.TabIndex = 4;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Exclamation_Red;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(15, 15);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 66);
		this.panel1.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.ClientSize = new System.Drawing.Size(766, 155);
		base.ControlBox = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "Popup_MessageBox_OK";
		base.panel_Footer.ResumeLayout(false);
		base.panel_Detail.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
