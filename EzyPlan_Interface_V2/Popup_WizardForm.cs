using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyWizard;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_WizardForm : Layout_PopupSmall, IWizardForm
{
	protected IWizardData m_data;

	private WizardFormResults m_results;

	private IContainer components;

	private RibbonMenuButton button_Finish;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Cancel;

	private TableLayoutPanel tableLayoutPanel1;

	public Popup_WizardForm()
	{
		InitializeComponent();
		m_results = new WizardFormResults();
	}

	public WizardFormResults ShowDialog(FormPositionEnum position, ref IWizardData data)
	{
		m_data = data;
		SetButtonsForWizardPosition(position);
		Show();
		return m_results;
	}

	private void SetButtonsForWizardPosition(FormPositionEnum position)
	{
		switch (position)
		{
		case FormPositionEnum.First:
			SetPositionFirst();
			break;
		case FormPositionEnum.Middle:
			SetPositionMiddle();
			break;
		case FormPositionEnum.Last:
			SetPositionLast();
			break;
		default:
			SetPositionLast();
			break;
		}
	}

	private void SetPositionFirst()
	{
		button_Next.Visible = true;
		button_Finish.Visible = false;
		button_Back.Visible = false;
	}

	private void SetPositionMiddle()
	{
		button_Next.Visible = true;
		button_Finish.Visible = false;
		button_Back.Visible = true;
	}

	private void SetPositionLast()
	{
		button_Next.Visible = false;
		button_Finish.Visible = true;
		button_Back.Visible = true;
	}

	public virtual void SaveUserInput()
	{
	}

	private void OnButtonPressed(ButtonPressedEnum buttonPressed)
	{
		switch (buttonPressed)
		{
		case ButtonPressedEnum.Next:
			SaveUserInput();
			break;
		case ButtonPressedEnum.Back:
			Close();
			break;
		case ButtonPressedEnum.Finish:
			SaveUserInput();
			break;
		case ButtonPressedEnum.Cancel:
			Close();
			break;
		}
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Cancel);
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Next);
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Back);
	}

	private void button_Finish_Click(object sender, EventArgs e)
	{
		OnButtonPressed(ButtonPressedEnum.Finish);
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
		this.button_Finish = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Size = new System.Drawing.Size(713, 197);
		base.panel_Footer.Controls.Add(this.tableLayoutPanel1);
		base.panel_Footer.Location = new System.Drawing.Point(0, 235);
		base.panel_Footer.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
		base.panel_Footer.Size = new System.Drawing.Size(713, 58);
		base.panel_Header.Size = new System.Drawing.Size(713, 38);
		this.button_Finish.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Finish.AutoEllipsis = true;
		this.button_Finish.BackColor = System.Drawing.Color.Transparent;
		this.button_Finish.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Finish.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Finish.ColorOn = System.Drawing.Color.Orange;
		this.button_Finish.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Finish.ColorPress = System.Drawing.Color.Orange;
		this.button_Finish.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Finish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Finish.FadingSpeed = 35;
		this.button_Finish.FlatAppearance.BorderSize = 0;
		this.button_Finish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Finish.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Finish.Image = EzyPlan_Interface_V2.Properties.Resources.Last;
		this.button_Finish.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Finish.ImageOffset = 4;
		this.button_Finish.IsPressed = false;
		this.button_Finish.KeepPress = false;
		this.button_Finish.Location = new System.Drawing.Point(450, 3);
		this.button_Finish.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Finish.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Finish.Name = "button_Finish";
		this.button_Finish.Radius = 10;
		this.button_Finish.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Finish.Size = new System.Drawing.Size(143, 42);
		this.button_Finish.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Finish.SplitDistance = 0;
		this.button_Finish.TabIndex = 3;
		this.button_Finish.Text = "Finish";
		this.button_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Finish.Title = "";
		this.button_Finish.UseVisualStyleBackColor = true;
		this.button_Finish.Click += new System.EventHandler(this.button_Finish_Click);
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
		this.button_Back.Location = new System.Drawing.Point(301, 3);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(143, 42);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 2;
		this.button_Back.Text = "Back";
		this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
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
		this.button_Next.Location = new System.Drawing.Point(152, 3);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(143, 42);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 4;
		this.button_Next.Text = "Next";
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
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
		this.button_Cancel.Location = new System.Drawing.Point(3, 3);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(143, 42);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 5;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.tableLayoutPanel1.ColumnCount = 4;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.button_Cancel, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.button_Finish, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.button_Next, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.button_Back, 2, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(111, 5);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(597, 48);
		this.tableLayoutPanel1.TabIndex = 6;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(713, 293);
		base.Name = "Popup_WizardForm";
		this.Text = "Popup_WizardForm";
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
