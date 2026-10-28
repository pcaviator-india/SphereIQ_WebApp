using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_EnterPassword : Layout_PopupSmall
{
	public new enum DialogResult
	{
		PasswordAccepted,
		Cancelled
	}

	private string m_password;

	private DialogResult m_dialogResult;

	private static Popup_EnterPassword m_instance;

	private IContainer components;

	private Label label2;

	private TextBox textBox_Password;

	private Label label_SubInstructionText;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_OK;

	private Panel panel3;

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

	public string InstructionText
	{
		get
		{
			return label1.Text;
		}
		set
		{
			label1.Text = value;
		}
	}

	public string InstructionSubText
	{
		get
		{
			return label_SubInstructionText.Text;
		}
		set
		{
			label_SubInstructionText.Text = value;
		}
	}

	private Popup_EnterPassword()
	{
		InitializeComponent();
	}

	private Popup_EnterPassword(string password)
		: this()
	{
		m_password = password;
	}

	private Popup_EnterPassword(string title, string text, string subText, string password)
		: this()
	{
		Title = title;
		InstructionText = text;
		InstructionSubText = subText;
		m_password = password;
	}

	public static DialogResult ShowDialog(string password)
	{
		m_instance = new Popup_EnterPassword(password);
		m_instance.Start();
		return m_instance.m_dialogResult;
	}

	public static DialogResult ShowDialog(string title, string text, string subText, string password)
	{
		m_instance = new Popup_EnterPassword(title, text, subText, password);
		m_instance.Start();
		return m_instance.m_dialogResult;
	}

	private void Start()
	{
		ShowDialog();
	}

	private void textBox_Password_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Password);
	}

	private void textBox_Password_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Password);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void button_OK_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_OK);
	}

	private void button_OK_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_OK);
	}

	private void button_OK_Click(object sender, EventArgs e)
	{
		if (m_password == textBox_Password.Text)
		{
			m_dialogResult = DialogResult.PasswordAccepted;
			Close();
		}
		else
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Incorrect Password", "The password you entered is incorrect!");
			popup_MessageBox_OK.ShowDialog();
			textBox_Password.Focus();
		}
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		m_dialogResult = DialogResult.Cancelled;
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
		this.textBox_Password = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label_SubInstructionText = new System.Windows.Forms.Label();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_OK = new RibbonStyle.RibbonMenuButton();
		this.panel3 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.label2);
		base.panel_Detail.Controls.Add(this.textBox_Password);
		base.panel_Detail.Location = new System.Drawing.Point(0, 55);
		base.panel_Detail.Size = new System.Drawing.Size(680, 119);
		base.label1.Location = new System.Drawing.Point(7, 0);
		base.label1.Size = new System.Drawing.Size(341, 24);
		base.label1.Text = "A password is required to continue!";
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.button_OK);
		base.panel_Footer.Location = new System.Drawing.Point(0, 174);
		base.panel_Footer.Size = new System.Drawing.Size(680, 56);
		base.panel_Header.Controls.Add(this.panel3);
		base.panel_Header.Controls.Add(this.label_SubInstructionText);
		base.panel_Header.Size = new System.Drawing.Size(680, 55);
		base.panel_Header.Controls.SetChildIndex(this.label_SubInstructionText, 0);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel3, 0);
		this.textBox_Password.BackColor = System.Drawing.Color.White;
		this.textBox_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Password.Location = new System.Drawing.Point(116, 41);
		this.textBox_Password.MaxLength = 30;
		this.textBox_Password.Name = "textBox_Password";
		this.textBox_Password.PasswordChar = '*';
		this.textBox_Password.Size = new System.Drawing.Size(273, 29);
		this.textBox_Password.TabIndex = 0;
		this.textBox_Password.Enter += new System.EventHandler(this.textBox_Password_Enter);
		this.textBox_Password.Leave += new System.EventHandler(this.textBox_Password_Leave);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(13, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(97, 24);
		this.label2.TabIndex = 1;
		this.label2.Text = "Password:";
		this.label_SubInstructionText.AutoSize = true;
		this.label_SubInstructionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SubInstructionText.Location = new System.Drawing.Point(7, 24);
		this.label_SubInstructionText.Name = "label_SubInstructionText";
		this.label_SubInstructionText.Size = new System.Drawing.Size(456, 24);
		this.label_SubInstructionText.TabIndex = 1;
		this.label_SubInstructionText.Text = "Enter the password below and click \"OK\" to continue.";
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
		this.button_Cancel.Location = new System.Drawing.Point(529, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_OK.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_OK.AutoEllipsis = true;
		this.button_OK.BackColor = System.Drawing.Color.Transparent;
		this.button_OK.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_OK.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_OK.ColorOn = System.Drawing.Color.Orange;
		this.button_OK.ColorOnStroke = System.Drawing.Color.Silver;
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
		this.button_OK.ImageOffset = 4;
		this.button_OK.IsPressed = false;
		this.button_OK.KeepPress = false;
		this.button_OK.Location = new System.Drawing.Point(375, 6);
		this.button_OK.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_OK.MenuPos = new System.Drawing.Point(0, 0);
		this.button_OK.Name = "button_OK";
		this.button_OK.Radius = 10;
		this.button_OK.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_OK.Size = new System.Drawing.Size(148, 45);
		this.button_OK.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_OK.SplitDistance = 0;
		this.button_OK.TabIndex = 0;
		this.button_OK.Text = "OK";
		this.button_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_OK.Title = "";
		this.button_OK.UseVisualStyleBackColor = true;
		this.button_OK.Enter += new System.EventHandler(this.button_OK_Enter);
		this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
		this.button_OK.Leave += new System.EventHandler(this.button_OK_Leave);
		this.panel3.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Activation;
		this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(616, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(64, 55);
		this.panel3.TabIndex = 7;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(680, 230);
		base.Name = "Popup_EnterPassword";
		this.Text = "Password Required";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}
}
