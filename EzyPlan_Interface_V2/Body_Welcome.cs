using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_Welcome : UserControl
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			OpenExistingHole,
			OpenPreviousHole,
			CreateNewHole,
			HoleManagement,
			Close
		}

		private ButtonPressedEnum m_buttonPressed;

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

	private IContainer components;

	private Panel panel_Detail;

	private Panel panel3;

	private Panel panel4;

	private Panel panel5;

	private Label label1;

	private Panel panel1;

	private RibbonMenuButton button_OpenExistingHole;

	private RibbonMenuButton button_OpenPrevious;

	private RibbonMenuButton button_CreateNew;

	private RibbonMenuButton button_Close;

	private RibbonMenuButton button_HoleManagement;

	private Panel panel7;

	private Panel panel_Navigation;

	public event EventHandler ButtonPressed;

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
		this.panel_Detail = new System.Windows.Forms.Panel();
		this.panel_Navigation = new System.Windows.Forms.Panel();
		this.button_OpenExistingHole = new RibbonStyle.RibbonMenuButton();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.button_OpenPrevious = new RibbonStyle.RibbonMenuButton();
		this.button_HoleManagement = new RibbonStyle.RibbonMenuButton();
		this.button_CreateNew = new RibbonStyle.RibbonMenuButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel_Detail.SuspendLayout();
		this.panel_Navigation.SuspendLayout();
		this.panel4.SuspendLayout();
		base.SuspendLayout();
		this.panel_Detail.AutoScroll = true;
		this.panel_Detail.BackColor = System.Drawing.Color.White;
		this.panel_Detail.Controls.Add(this.panel_Navigation);
		this.panel_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Detail.Location = new System.Drawing.Point(0, 75);
		this.panel_Detail.Name = "panel_Detail";
		this.panel_Detail.Size = new System.Drawing.Size(1013, 589);
		this.panel_Detail.TabIndex = 0;
		this.panel_Detail.TabStop = true;
		this.panel_Navigation.BackColor = System.Drawing.Color.White;
		this.panel_Navigation.Controls.Add(this.button_OpenExistingHole);
		this.panel_Navigation.Controls.Add(this.button_Close);
		this.panel_Navigation.Controls.Add(this.button_OpenPrevious);
		this.panel_Navigation.Controls.Add(this.button_HoleManagement);
		this.panel_Navigation.Controls.Add(this.button_CreateNew);
		this.panel_Navigation.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel_Navigation.Location = new System.Drawing.Point(0, 0);
		this.panel_Navigation.Name = "panel_Navigation";
		this.panel_Navigation.Size = new System.Drawing.Size(666, 589);
		this.panel_Navigation.TabIndex = 6;
		this.button_OpenExistingHole.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_OpenExistingHole.AutoEllipsis = true;
		this.button_OpenExistingHole.BackColor = System.Drawing.Color.Transparent;
		this.button_OpenExistingHole.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_OpenExistingHole.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 76, 76, 76);
		this.button_OpenExistingHole.ColorOn = System.Drawing.Color.Orange;
		this.button_OpenExistingHole.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_OpenExistingHole.ColorPress = System.Drawing.Color.Orange;
		this.button_OpenExistingHole.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_OpenExistingHole.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_OpenExistingHole.FadingSpeed = 35;
		this.button_OpenExistingHole.FlatAppearance.BorderSize = 0;
		this.button_OpenExistingHole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_OpenExistingHole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_OpenExistingHole.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_OpenExistingHole.Image = EzyPlan_Interface_V2.Properties.Resources.Open_File;
		this.button_OpenExistingHole.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_OpenExistingHole.ImageOffset = 4;
		this.button_OpenExistingHole.IsPressed = false;
		this.button_OpenExistingHole.KeepPress = false;
		this.button_OpenExistingHole.Location = new System.Drawing.Point(3, 3);
		this.button_OpenExistingHole.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_OpenExistingHole.MenuPos = new System.Drawing.Point(0, 0);
		this.button_OpenExistingHole.Name = "button_OpenExistingHole";
		this.button_OpenExistingHole.Radius = 30;
		this.button_OpenExistingHole.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_OpenExistingHole.Size = new System.Drawing.Size(647, 61);
		this.button_OpenExistingHole.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_OpenExistingHole.SplitDistance = 0;
		this.button_OpenExistingHole.TabIndex = 0;
		this.button_OpenExistingHole.Text = "Open an existing SphereIQ hole file";
		this.button_OpenExistingHole.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_OpenExistingHole.Title = "Open Existing File";
		this.button_OpenExistingHole.UseVisualStyleBackColor = true;
		this.button_OpenExistingHole.Enter += new System.EventHandler(this.button_OpenExistingHole_Enter);
		this.button_OpenExistingHole.Click += new System.EventHandler(this.button_OpenExistingHole_Click);
		this.button_OpenExistingHole.Leave += new System.EventHandler(this.button_OpenExistingHole_Leave);
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
		this.button_Close.Image = EzyPlan_Interface_V2.Properties.Resources.Exit;
		this.button_Close.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Close.ImageOffset = 4;
		this.button_Close.IsPressed = false;
		this.button_Close.KeepPress = false;
		this.button_Close.Location = new System.Drawing.Point(3, 504);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 30;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(647, 61);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 5;
		this.button_Close.Text = "Exit this program";
		this.button_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_Close.Title = "Close";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.button_OpenPrevious.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_OpenPrevious.AutoEllipsis = true;
		this.button_OpenPrevious.BackColor = System.Drawing.Color.Transparent;
		this.button_OpenPrevious.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_OpenPrevious.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_OpenPrevious.ColorOn = System.Drawing.Color.Orange;
		this.button_OpenPrevious.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_OpenPrevious.ColorPress = System.Drawing.Color.Orange;
		this.button_OpenPrevious.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_OpenPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_OpenPrevious.FadingSpeed = 35;
		this.button_OpenPrevious.FlatAppearance.BorderSize = 0;
		this.button_OpenPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_OpenPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_OpenPrevious.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_OpenPrevious.Image = EzyPlan_Interface_V2.Properties.Resources.Open_Previous;
		this.button_OpenPrevious.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_OpenPrevious.ImageOffset = 4;
		this.button_OpenPrevious.IsPressed = false;
		this.button_OpenPrevious.KeepPress = false;
		this.button_OpenPrevious.Location = new System.Drawing.Point(3, 85);
		this.button_OpenPrevious.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_OpenPrevious.MenuPos = new System.Drawing.Point(0, 0);
		this.button_OpenPrevious.Name = "button_OpenPrevious";
		this.button_OpenPrevious.Radius = 30;
		this.button_OpenPrevious.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_OpenPrevious.Size = new System.Drawing.Size(647, 61);
		this.button_OpenPrevious.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_OpenPrevious.SplitDistance = 0;
		this.button_OpenPrevious.TabIndex = 1;
		this.button_OpenPrevious.Text = "Open the SphereIQ file that was last open";
		this.button_OpenPrevious.Title = "Open Previous File";
		this.button_OpenPrevious.UseVisualStyleBackColor = true;
		this.button_OpenPrevious.Enter += new System.EventHandler(this.button_OpenPreviouse_Enter);
		this.button_OpenPrevious.Click += new System.EventHandler(this.button_OpenPrevious_Click);
		this.button_OpenPrevious.Leave += new System.EventHandler(this.button_OpenPreviouse_Leave);
		this.button_HoleManagement.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_HoleManagement.AutoEllipsis = true;
		this.button_HoleManagement.BackColor = System.Drawing.Color.Transparent;
		this.button_HoleManagement.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_HoleManagement.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_HoleManagement.ColorOn = System.Drawing.Color.Orange;
		this.button_HoleManagement.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_HoleManagement.ColorPress = System.Drawing.Color.Orange;
		this.button_HoleManagement.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_HoleManagement.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_HoleManagement.FadingSpeed = 35;
		this.button_HoleManagement.FlatAppearance.BorderSize = 0;
		this.button_HoleManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_HoleManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_HoleManagement.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_HoleManagement.Image = EzyPlan_Interface_V2.Properties.Resources.Management;
		this.button_HoleManagement.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_HoleManagement.ImageOffset = 4;
		this.button_HoleManagement.IsPressed = false;
		this.button_HoleManagement.KeepPress = false;
		this.button_HoleManagement.Location = new System.Drawing.Point(3, 252);
		this.button_HoleManagement.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_HoleManagement.MenuPos = new System.Drawing.Point(0, 0);
		this.button_HoleManagement.Name = "button_HoleManagement";
		this.button_HoleManagement.Radius = 30;
		this.button_HoleManagement.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_HoleManagement.Size = new System.Drawing.Size(647, 61);
		this.button_HoleManagement.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_HoleManagement.SplitDistance = 0;
		this.button_HoleManagement.TabIndex = 3;
		this.button_HoleManagement.Text = "Manage hole data and perform calculations";
		this.button_HoleManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_HoleManagement.Title = "Hole Management";
		this.button_HoleManagement.UseVisualStyleBackColor = true;
		this.button_HoleManagement.Enter += new System.EventHandler(this.button_HoleManagement_Enter);
		this.button_HoleManagement.Click += new System.EventHandler(this.button_HoleManagement_Click);
		this.button_HoleManagement.Leave += new System.EventHandler(this.button_HoleManagement_Leave);
		this.button_CreateNew.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_CreateNew.AutoEllipsis = true;
		this.button_CreateNew.BackColor = System.Drawing.Color.Transparent;
		this.button_CreateNew.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_CreateNew.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_CreateNew.ColorOn = System.Drawing.Color.Orange;
		this.button_CreateNew.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_CreateNew.ColorPress = System.Drawing.Color.Orange;
		this.button_CreateNew.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_CreateNew.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_CreateNew.FadingSpeed = 35;
		this.button_CreateNew.FlatAppearance.BorderSize = 0;
		this.button_CreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_CreateNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_CreateNew.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_CreateNew.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.button_CreateNew.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_CreateNew.ImageOffset = 4;
		this.button_CreateNew.IsPressed = false;
		this.button_CreateNew.KeepPress = false;
		this.button_CreateNew.Location = new System.Drawing.Point(3, 167);
		this.button_CreateNew.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_CreateNew.MenuPos = new System.Drawing.Point(0, 0);
		this.button_CreateNew.Name = "button_CreateNew";
		this.button_CreateNew.Radius = 30;
		this.button_CreateNew.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_CreateNew.Size = new System.Drawing.Size(647, 61);
		this.button_CreateNew.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_CreateNew.SplitDistance = 0;
		this.button_CreateNew.TabIndex = 2;
		this.button_CreateNew.Text = "Create a new SphereIQ hole file";
		this.button_CreateNew.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_CreateNew.Title = "Create New File";
		this.button_CreateNew.UseVisualStyleBackColor = true;
		this.button_CreateNew.Enter += new System.EventHandler(this.button_CreateNew_Enter);
		this.button_CreateNew.Click += new System.EventHandler(this.button_CreateNew_Click);
		this.button_CreateNew.Leave += new System.EventHandler(this.button_CreateNew_Leave);
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 664);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1013, 10);
		this.panel3.TabIndex = 7;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.label1);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1013, 58);
		this.panel4.TabIndex = 8;
		this.panel5.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 39);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(949, 10);
		this.panel5.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.label1.Font = new System.Drawing.Font("Nina", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(146, 39);
		this.label1.TabIndex = 2;
		this.label1.Text = "WELCOME";
		this.panel7.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Home;
		this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel7.Location = new System.Drawing.Point(949, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(64, 58);
		this.panel7.TabIndex = 5;
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 58);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1013, 17);
		this.panel1.TabIndex = 9;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.panel_Detail);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel3);
		this.DoubleBuffered = true;
		base.Name = "Body_Welcome";
		base.Size = new System.Drawing.Size(1013, 674);
		this.panel_Detail.ResumeLayout(false);
		this.panel_Navigation.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		base.ResumeLayout(false);
	}

	public Body_Welcome()
	{
		InitializeComponent();
	}

	public void Display()
	{
		Visible = true;
		panel_Detail.Focus();
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void OpenExistingHoleFile()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.OpenExistingHole;
		OnButtonPressed(e);
	}

	private void OpenPreviousHoleFile()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.OpenPreviousHole;
		OnButtonPressed(e);
	}

	private void CreateNewHoleFile()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.CreateNewHole;
		OnButtonPressed(e);
	}

	private void HoleManagement()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.HoleManagement;
		OnButtonPressed(e);
	}

	private void Close()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Close;
		OnButtonPressed(e);
	}

	private void button_OpenExistingHole_Click(object sender, EventArgs e)
	{
		OpenExistingHoleFile();
	}

	private void button_OpenPrevious_Click(object sender, EventArgs e)
	{
		OpenPreviousHoleFile();
	}

	private void button_CreateNew_Click(object sender, EventArgs e)
	{
		CreateNewHoleFile();
	}

	private void button_HoleManagement_Click(object sender, EventArgs e)
	{
		HoleManagement();
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button_OpenExistingHole_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_OpenExistingHole);
	}

	private void button_OpenExistingHole_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_OpenExistingHole);
	}

	private void button_OpenPreviouse_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_OpenPrevious);
	}

	private void button_OpenPreviouse_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_OpenPrevious);
	}

	private void button_CreateNew_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_CreateNew);
	}

	private void button_CreateNew_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_CreateNew);
	}

	private void button_HoleManagement_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_HoleManagement);
	}

	private void button_HoleManagement_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_HoleManagement);
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
