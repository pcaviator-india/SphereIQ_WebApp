using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_Report_UserFields : Layout_PopupSmall
{
	public class UserFieldsDialogResult
	{
		private ButtonPressedEnum m_buttonPressed;

		private string m_userName;

		private string m_notes;

		public string UserName
		{
			get
			{
				return m_userName;
			}
			set
			{
				m_userName = value;
			}
		}

		public string Notes
		{
			get
			{
				return m_notes;
			}
			set
			{
				m_notes = value;
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
		Next
	}

	private UserFieldsDialogResult m_customDialogResult;

	private IContainer components;

	private Label label13;

	private TextBox textBox_Notes;

	private Label label16;

	private TextBox textBox_AuthorName;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Next;

	private Panel panel1;

	public Popup_Report_UserFields()
	{
		InitializeComponent();
		m_customDialogResult = new UserFieldsDialogResult();
		m_customDialogResult.ButtonPressed = ButtonPressedEnum.Cancel;
		m_customDialogResult.Notes = "";
		m_customDialogResult.UserName = "";
	}

	public new UserFieldsDialogResult ShowDialog()
	{
		m_customDialogResult.ButtonPressed = ButtonPressedEnum.Cancel;
		base.ShowDialog();
		return m_customDialogResult;
	}

	public new UserFieldsDialogResult ShowDialog(IWin32Window owner)
	{
		m_customDialogResult.ButtonPressed = ButtonPressedEnum.Cancel;
		base.ShowDialog(owner);
		return m_customDialogResult;
	}

	private void textBox_AuthorName_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_AuthorName);
	}

	private void textBox_AuthorName_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_AuthorName);
	}

	private void textBox_Notes_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Notes);
	}

	private void textBox_Notes_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Notes);
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
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
		m_customDialogResult.UserName = textBox_AuthorName.Text;
		m_customDialogResult.Notes = textBox_Notes.Text;
		ResetAndClose();
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Next);
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Cancel);
	}

	public void ResetAndClose()
	{
		textBox_AuthorName.Focus();
		Close();
	}

	private void textBox_AuthorName_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_AuthorName);
	}

	private void textBox_Notes_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Notes);
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
		this.label13 = new System.Windows.Forms.Label();
		this.textBox_Notes = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.textBox_AuthorName = new System.Windows.Forms.TextBox();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.label16);
		base.panel_Detail.Controls.Add(this.textBox_AuthorName);
		base.panel_Detail.Controls.Add(this.label13);
		base.panel_Detail.Controls.Add(this.textBox_Notes);
		base.panel_Detail.Location = new System.Drawing.Point(0, 47);
		base.panel_Detail.Size = new System.Drawing.Size(605, 213);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(484, 24);
		base.label1.Text = "Enter the following fields for inclusion in the report:";
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.button_Next);
		base.panel_Footer.Location = new System.Drawing.Point(0, 260);
		base.panel_Footer.Size = new System.Drawing.Size(605, 58);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(605, 47);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(3, 65);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(64, 24);
		this.label13.TabIndex = 3;
		this.label13.Text = "Notes:";
		this.textBox_Notes.BackColor = System.Drawing.Color.White;
		this.textBox_Notes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Notes.Location = new System.Drawing.Point(7, 92);
		this.textBox_Notes.MaxLength = 210;
		this.textBox_Notes.Multiline = true;
		this.textBox_Notes.Name = "textBox_Notes";
		this.textBox_Notes.Size = new System.Drawing.Size(586, 94);
		this.textBox_Notes.TabIndex = 1;
		this.textBox_Notes.Enter += new System.EventHandler(this.textBox_Notes_Enter);
		this.textBox_Notes.DoubleClick += new System.EventHandler(this.textBox_Notes_DoubleClick);
		this.textBox_Notes.Leave += new System.EventHandler(this.textBox_Notes_Leave);
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(3, 22);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(127, 24);
		this.label16.TabIndex = 7;
		this.label16.Text = "Author Name:";
		this.textBox_AuthorName.BackColor = System.Drawing.Color.White;
		this.textBox_AuthorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_AuthorName.Location = new System.Drawing.Point(136, 20);
		this.textBox_AuthorName.MaxLength = 40;
		this.textBox_AuthorName.Name = "textBox_AuthorName";
		this.textBox_AuthorName.Size = new System.Drawing.Size(457, 26);
		this.textBox_AuthorName.TabIndex = 0;
		this.textBox_AuthorName.Enter += new System.EventHandler(this.textBox_AuthorName_Enter);
		this.textBox_AuthorName.DoubleClick += new System.EventHandler(this.textBox_AuthorName_DoubleClick);
		this.textBox_AuthorName.Leave += new System.EventHandler(this.textBox_AuthorName_Leave);
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
		this.button_Cancel.Location = new System.Drawing.Point(454, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 3;
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
		this.button_Next.Location = new System.Drawing.Point(300, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(148, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 2;
		this.button_Next.Text = "Next";
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(541, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 47);
		this.panel1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(605, 318);
		base.Name = "Popup_Report_UserFields";
		this.Text = "Report Fields";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}
}
