using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Toolbar_DrillHoleManagement : UserControl
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Home,
			EditDetails,
			EditSurveyData,
			ImportSurveyData,
			HolePlanning
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

	private Panel panel7;

	private Panel panel1;

	private Panel panel3;

	private Panel panel6;

	private Panel panel8;

	private RibbonMenuButton button_Home;

	private RibbonMenuButton button_EditDetails;

	private RibbonMenuButton button_EditSurveyData;

	private RibbonMenuButton button_HolePlanning;

	private Panel panel4;

	private RibbonMenuButton button_Import;

	private PictureBox pictureBox1;

	private int m_designTimeWidth;

	public int DesignTimeWidth => m_designTimeWidth;

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
		this.panel1 = new System.Windows.Forms.Panel();
		this.button_Home = new RibbonStyle.RibbonMenuButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.button_Import = new RibbonStyle.RibbonMenuButton();
		this.button_EditSurveyData = new RibbonStyle.RibbonMenuButton();
		this.button_EditDetails = new RibbonStyle.RibbonMenuButton();
		this.panel6 = new System.Windows.Forms.Panel();
		this.button_HolePlanning = new RibbonStyle.RibbonMenuButton();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.button_Home);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 10);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(232, 74);
		this.panel1.TabIndex = 1;
		this.button_Home.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Home.AutoEllipsis = true;
		this.button_Home.BackColor = System.Drawing.Color.Transparent;
		this.button_Home.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Home.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Home.ColorOn = System.Drawing.Color.Orange;
		this.button_Home.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Home.ColorPress = System.Drawing.Color.Orange;
		this.button_Home.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Home.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Home.FadingSpeed = 35;
		this.button_Home.FlatAppearance.BorderSize = 0;
		this.button_Home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Home.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Home.ForeColor = System.Drawing.Color.White;
		this.button_Home.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Home.Image = EzyPlan_Interface_V2.Properties.Resources.Home;
		this.button_Home.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Home.ImageOffset = 4;
		this.button_Home.IsPressed = false;
		this.button_Home.KeepPress = false;
		this.button_Home.Location = new System.Drawing.Point(3, 6);
		this.button_Home.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Home.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Home.Name = "button_Home";
		this.button_Home.Radius = 20;
		this.button_Home.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Home.Size = new System.Drawing.Size(222, 58);
		this.button_Home.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Home.SplitDistance = 0;
		this.button_Home.TabIndex = 0;
		this.button_Home.Text = "Welcome Screen";
		this.button_Home.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button_Home.Title = "Back";
		this.button_Home.UseVisualStyleBackColor = true;
		this.button_Home.Enter += new System.EventHandler(this.button_Home_Enter);
		this.button_Home.Click += new System.EventHandler(this.button_Home_Click);
		this.button_Home.Leave += new System.EventHandler(this.button_Home_Leave);
		this.panel3.BackColor = System.Drawing.Color.GhostWhite;
		this.panel3.Controls.Add(this.button_Import);
		this.panel3.Controls.Add(this.button_EditSurveyData);
		this.panel3.Controls.Add(this.button_EditDetails);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 94);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(232, 179);
		this.panel3.TabIndex = 3;
		this.button_Import.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Import.AutoEllipsis = true;
		this.button_Import.BackColor = System.Drawing.Color.Transparent;
		this.button_Import.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Import.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Import.ColorOn = System.Drawing.Color.Orange;
		this.button_Import.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Import.ColorPress = System.Drawing.Color.Orange;
		this.button_Import.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Import.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Import.FadingSpeed = 35;
		this.button_Import.FlatAppearance.BorderSize = 0;
		this.button_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Import.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Import.ForeColor = System.Drawing.Color.White;
		this.button_Import.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Import.Image = EzyPlan_Interface_V2.Properties.Resources.Import_Doc;
		this.button_Import.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Import.ImageOffset = 4;
		this.button_Import.IsPressed = false;
		this.button_Import.KeepPress = false;
		this.button_Import.Location = new System.Drawing.Point(3, 120);
		this.button_Import.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Import.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Import.Name = "button_Import";
		this.button_Import.Radius = 20;
		this.button_Import.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Import.Size = new System.Drawing.Size(222, 51);
		this.button_Import.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Import.SplitDistance = 0;
		this.button_Import.TabIndex = 2;
		this.button_Import.Text = "Import Survey Data";
		this.button_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Import.Title = "";
		this.button_Import.UseVisualStyleBackColor = true;
		this.button_Import.Enter += new System.EventHandler(this.button_Import_Enter);
		this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
		this.button_Import.Leave += new System.EventHandler(this.button_Import_Leave);
		this.button_EditSurveyData.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_EditSurveyData.AutoEllipsis = true;
		this.button_EditSurveyData.BackColor = System.Drawing.Color.Transparent;
		this.button_EditSurveyData.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_EditSurveyData.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_EditSurveyData.ColorOn = System.Drawing.Color.Orange;
		this.button_EditSurveyData.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_EditSurveyData.ColorPress = System.Drawing.Color.Orange;
		this.button_EditSurveyData.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_EditSurveyData.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_EditSurveyData.FadingSpeed = 35;
		this.button_EditSurveyData.FlatAppearance.BorderSize = 0;
		this.button_EditSurveyData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_EditSurveyData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_EditSurveyData.ForeColor = System.Drawing.Color.White;
		this.button_EditSurveyData.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_EditSurveyData.Image = EzyPlan_Interface_V2.Properties.Resources.Edit_Record;
		this.button_EditSurveyData.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_EditSurveyData.ImageOffset = 4;
		this.button_EditSurveyData.IsPressed = false;
		this.button_EditSurveyData.KeepPress = false;
		this.button_EditSurveyData.Location = new System.Drawing.Point(3, 63);
		this.button_EditSurveyData.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_EditSurveyData.MenuPos = new System.Drawing.Point(0, 0);
		this.button_EditSurveyData.Name = "button_EditSurveyData";
		this.button_EditSurveyData.Radius = 20;
		this.button_EditSurveyData.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_EditSurveyData.Size = new System.Drawing.Size(222, 51);
		this.button_EditSurveyData.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_EditSurveyData.SplitDistance = 0;
		this.button_EditSurveyData.TabIndex = 1;
		this.button_EditSurveyData.Text = "Edit Survey Data";
		this.button_EditSurveyData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_EditSurveyData.Title = "";
		this.button_EditSurveyData.UseVisualStyleBackColor = true;
		this.button_EditSurveyData.Enter += new System.EventHandler(this.button_EditSurveyData_Enter);
		this.button_EditSurveyData.Click += new System.EventHandler(this.button_EditSurveyData_Click);
		this.button_EditSurveyData.Leave += new System.EventHandler(this.button_EditSurveyData_Leave);
		this.button_EditDetails.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_EditDetails.AutoEllipsis = true;
		this.button_EditDetails.BackColor = System.Drawing.Color.Transparent;
		this.button_EditDetails.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_EditDetails.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_EditDetails.ColorOn = System.Drawing.Color.Orange;
		this.button_EditDetails.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_EditDetails.ColorPress = System.Drawing.Color.Orange;
		this.button_EditDetails.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_EditDetails.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_EditDetails.FadingSpeed = 35;
		this.button_EditDetails.FlatAppearance.BorderSize = 0;
		this.button_EditDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_EditDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_EditDetails.ForeColor = System.Drawing.Color.White;
		this.button_EditDetails.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_EditDetails.Image = EzyPlan_Interface_V2.Properties.Resources.Edit_Document;
		this.button_EditDetails.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_EditDetails.ImageOffset = 4;
		this.button_EditDetails.IsPressed = false;
		this.button_EditDetails.KeepPress = false;
		this.button_EditDetails.Location = new System.Drawing.Point(3, 6);
		this.button_EditDetails.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_EditDetails.MenuPos = new System.Drawing.Point(0, 0);
		this.button_EditDetails.Name = "button_EditDetails";
		this.button_EditDetails.Radius = 20;
		this.button_EditDetails.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_EditDetails.Size = new System.Drawing.Size(222, 51);
		this.button_EditDetails.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_EditDetails.SplitDistance = 0;
		this.button_EditDetails.TabIndex = 0;
		this.button_EditDetails.Text = "Edit Details";
		this.button_EditDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_EditDetails.Title = "";
		this.button_EditDetails.UseVisualStyleBackColor = true;
		this.button_EditDetails.Enter += new System.EventHandler(this.button_EditDetails_Enter);
		this.button_EditDetails.Click += new System.EventHandler(this.button_EditDetails_Click);
		this.button_EditDetails.Leave += new System.EventHandler(this.button_EditDetails_Leave);
		this.panel6.Controls.Add(this.button_HolePlanning);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 283);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(232, 72);
		this.panel6.TabIndex = 5;
		this.button_HolePlanning.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_HolePlanning.AutoEllipsis = true;
		this.button_HolePlanning.BackColor = System.Drawing.Color.Transparent;
		this.button_HolePlanning.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_HolePlanning.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_HolePlanning.ColorOn = System.Drawing.Color.Orange;
		this.button_HolePlanning.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_HolePlanning.ColorPress = System.Drawing.Color.Orange;
		this.button_HolePlanning.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_HolePlanning.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_HolePlanning.FadingSpeed = 35;
		this.button_HolePlanning.FlatAppearance.BorderSize = 0;
		this.button_HolePlanning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_HolePlanning.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_HolePlanning.ForeColor = System.Drawing.Color.White;
		this.button_HolePlanning.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_HolePlanning.Image = EzyPlan_Interface_V2.Properties.Resources.Navigation;
		this.button_HolePlanning.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_HolePlanning.ImageOffset = 4;
		this.button_HolePlanning.IsPressed = false;
		this.button_HolePlanning.KeepPress = false;
		this.button_HolePlanning.Location = new System.Drawing.Point(3, 6);
		this.button_HolePlanning.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_HolePlanning.MenuPos = new System.Drawing.Point(0, 0);
		this.button_HolePlanning.Name = "button_HolePlanning";
		this.button_HolePlanning.Radius = 20;
		this.button_HolePlanning.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_HolePlanning.Size = new System.Drawing.Size(222, 56);
		this.button_HolePlanning.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_HolePlanning.SplitDistance = 0;
		this.button_HolePlanning.TabIndex = 0;
		this.button_HolePlanning.Text = "Navi work functions";
		this.button_HolePlanning.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_HolePlanning.Title = "Hole Navigation";
		this.button_HolePlanning.UseVisualStyleBackColor = true;
		this.button_HolePlanning.Enter += new System.EventHandler(this.button_HolePlanning_Enter);
		this.button_HolePlanning.Click += new System.EventHandler(this.button_HolePlanning_Click);
		this.button_HolePlanning.Leave += new System.EventHandler(this.button_HolePlanning_Leave);
		this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel8.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ep_subhead4;
		this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 273);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(232, 10);
		this.panel8.TabIndex = 4;
		this.panel4.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ep_subhead4;
		this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 84);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(232, 10);
		this.panel4.TabIndex = 2;
		this.panel7.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel7.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ep_subhead4;
		this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(232, 10);
		this.panel7.TabIndex = 0;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pictureBox1.Image = EzyPlan_Interface_V2.Properties.Resources.ezynav_logo_small;
		this.pictureBox1.Location = new System.Drawing.Point(0, 362);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(232, 79);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 6;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.GhostWhite;
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel7);
		base.Name = "Toolbar_DrillHoleManagement";
		base.Size = new System.Drawing.Size(232, 441);
		this.panel1.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Toolbar_DrillHoleManagement()
	{
		InitializeComponent();
		m_designTimeWidth = Width;
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void HolePlanning()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.HolePlanning;
		OnButtonPressed(e);
	}

	private void EditSurveyData()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.EditSurveyData;
		OnButtonPressed(e);
	}

	private void ImportSurveyData()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.ImportSurveyData;
		OnButtonPressed(e);
	}

	private void EditDetails()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.EditDetails;
		OnButtonPressed(e);
	}

	private void Home()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Home;
		OnButtonPressed(e);
	}

	private void button_Home_Click(object sender, EventArgs e)
	{
		Home();
	}

	private void button_EditDetails_Click(object sender, EventArgs e)
	{
		EditDetails();
	}

	private void button_EditSurveyData_Click(object sender, EventArgs e)
	{
		EditSurveyData();
	}

	private void button_Import_Click(object sender, EventArgs e)
	{
		ImportSurveyData();
	}

	private void button_HolePlanning_Click(object sender, EventArgs e)
	{
		HolePlanning();
	}

	private void button_Home_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_Home);
	}

	private void button_Home_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_Home);
	}

	private void button_EditDetails_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_EditDetails);
	}

	private void button_EditDetails_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_EditDetails);
	}

	private void button_EditSurveyData_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_EditSurveyData);
	}

	private void button_EditSurveyData_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_EditSurveyData);
	}

	private void button_Import_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_Import);
	}

	private void button_Import_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_Import);
	}

	private void button_HolePlanning_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_HolePlanning);
	}

	private void button_HolePlanning_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_HolePlanning);
	}
}
