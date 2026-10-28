using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Toolbar_DrillHolePlanning : UserControl
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			DrillHoleManagement,
			Close,
			Build,
			Extrapolate,
			TrendAnalysis,
			SideTrackHole,
			Advanced,
			Report
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

	private Panel panel5;

	private Panel panel7;

	private Panel panel3;

	private Panel panel2;

	private Panel panel1;

	private RibbonMenuButton button_DrillHoleManagement;

	private RibbonMenuButton button_TrendAnalysis;

	private RibbonMenuButton button_Automatic;

	private RibbonMenuButton button_Advanced;

	private RibbonMenuButton button_Extrapolate;

	private RibbonMenuButton button_SideTrackHole;

	private RibbonMenuButton button_Report;

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
		this.panel3 = new System.Windows.Forms.Panel();
		this.button_Report = new RibbonStyle.RibbonMenuButton();
		this.button_SideTrackHole = new RibbonStyle.RibbonMenuButton();
		this.button_Advanced = new RibbonStyle.RibbonMenuButton();
		this.button_Extrapolate = new RibbonStyle.RibbonMenuButton();
		this.button_TrendAnalysis = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.button_Automatic = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button_DrillHoleManagement = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel3.Controls.Add(this.button_Report);
		this.panel3.Controls.Add(this.button_SideTrackHole);
		this.panel3.Controls.Add(this.button_Advanced);
		this.panel3.Controls.Add(this.button_Extrapolate);
		this.panel3.Controls.Add(this.button_TrendAnalysis);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 158);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(227, 290);
		this.panel3.TabIndex = 4;
		this.button_Report.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Report.AutoEllipsis = true;
		this.button_Report.BackColor = System.Drawing.Color.Transparent;
		this.button_Report.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Report.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Report.ColorOn = System.Drawing.Color.Orange;
		this.button_Report.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Report.ColorPress = System.Drawing.Color.Orange;
		this.button_Report.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Report.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Report.FadingSpeed = 35;
		this.button_Report.FlatAppearance.BorderSize = 0;
		this.button_Report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Report.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Report.ForeColor = System.Drawing.Color.White;
		this.button_Report.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Report.Image = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.button_Report.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Report.ImageOffset = 4;
		this.button_Report.IsPressed = false;
		this.button_Report.KeepPress = false;
		this.button_Report.Location = new System.Drawing.Point(4, 231);
		this.button_Report.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Report.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Report.Name = "button_Report";
		this.button_Report.Radius = 20;
		this.button_Report.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Report.Size = new System.Drawing.Size(218, 51);
		this.button_Report.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Report.SplitDistance = 0;
		this.button_Report.TabIndex = 5;
		this.button_Report.Text = "Report";
		this.button_Report.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Report.Title = "";
		this.button_Report.UseVisualStyleBackColor = true;
		this.button_Report.Click += new System.EventHandler(this.button_Report_Click);
		this.button_SideTrackHole.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_SideTrackHole.AutoEllipsis = true;
		this.button_SideTrackHole.BackColor = System.Drawing.Color.Transparent;
		this.button_SideTrackHole.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_SideTrackHole.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_SideTrackHole.ColorOn = System.Drawing.Color.Orange;
		this.button_SideTrackHole.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_SideTrackHole.ColorPress = System.Drawing.Color.Orange;
		this.button_SideTrackHole.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_SideTrackHole.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_SideTrackHole.FadingSpeed = 35;
		this.button_SideTrackHole.FlatAppearance.BorderSize = 0;
		this.button_SideTrackHole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_SideTrackHole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_SideTrackHole.ForeColor = System.Drawing.Color.White;
		this.button_SideTrackHole.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_SideTrackHole.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.button_SideTrackHole.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_SideTrackHole.ImageOffset = 4;
		this.button_SideTrackHole.IsPressed = false;
		this.button_SideTrackHole.KeepPress = false;
		this.button_SideTrackHole.Location = new System.Drawing.Point(4, 117);
		this.button_SideTrackHole.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_SideTrackHole.MenuPos = new System.Drawing.Point(0, 0);
		this.button_SideTrackHole.Name = "button_SideTrackHole";
		this.button_SideTrackHole.Radius = 20;
		this.button_SideTrackHole.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_SideTrackHole.Size = new System.Drawing.Size(218, 51);
		this.button_SideTrackHole.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_SideTrackHole.SplitDistance = 0;
		this.button_SideTrackHole.TabIndex = 4;
		this.button_SideTrackHole.Text = "Sidetrack Hole";
		this.button_SideTrackHole.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_SideTrackHole.Title = "";
		this.button_SideTrackHole.UseVisualStyleBackColor = true;
		this.button_SideTrackHole.Enter += new System.EventHandler(this.button_SideTrackHole_Enter);
		this.button_SideTrackHole.Click += new System.EventHandler(this.button_SideTrackHole_Click);
		this.button_SideTrackHole.Leave += new System.EventHandler(this.button_SideTrackHole_Leave);
		this.button_Advanced.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Advanced.AutoEllipsis = true;
		this.button_Advanced.BackColor = System.Drawing.Color.Transparent;
		this.button_Advanced.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Advanced.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Advanced.ColorOn = System.Drawing.Color.Orange;
		this.button_Advanced.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Advanced.ColorPress = System.Drawing.Color.Orange;
		this.button_Advanced.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Advanced.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Advanced.FadingSpeed = 35;
		this.button_Advanced.FlatAppearance.BorderSize = 0;
		this.button_Advanced.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Advanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Advanced.ForeColor = System.Drawing.Color.White;
		this.button_Advanced.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Advanced.Image = EzyPlan_Interface_V2.Properties.Resources.Advanced;
		this.button_Advanced.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Advanced.ImageOffset = 4;
		this.button_Advanced.IsPressed = false;
		this.button_Advanced.KeepPress = false;
		this.button_Advanced.Location = new System.Drawing.Point(4, 174);
		this.button_Advanced.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Advanced.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Advanced.Name = "button_Advanced";
		this.button_Advanced.Radius = 20;
		this.button_Advanced.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Advanced.Size = new System.Drawing.Size(218, 51);
		this.button_Advanced.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Advanced.SplitDistance = 0;
		this.button_Advanced.TabIndex = 2;
		this.button_Advanced.Text = "Advanced";
		this.button_Advanced.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Advanced.Title = "";
		this.button_Advanced.UseVisualStyleBackColor = true;
		this.button_Advanced.Enter += new System.EventHandler(this.button_Advanced_Enter);
		this.button_Advanced.Click += new System.EventHandler(this.button_Advanced_Click);
		this.button_Advanced.Leave += new System.EventHandler(this.button_Advanced_Leave);
		this.button_Extrapolate.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Extrapolate.AutoEllipsis = true;
		this.button_Extrapolate.BackColor = System.Drawing.Color.Transparent;
		this.button_Extrapolate.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Extrapolate.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Extrapolate.ColorOn = System.Drawing.Color.Orange;
		this.button_Extrapolate.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Extrapolate.ColorPress = System.Drawing.Color.Orange;
		this.button_Extrapolate.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Extrapolate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Extrapolate.FadingSpeed = 35;
		this.button_Extrapolate.FlatAppearance.BorderSize = 0;
		this.button_Extrapolate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Extrapolate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Extrapolate.ForeColor = System.Drawing.Color.White;
		this.button_Extrapolate.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Extrapolate.Image = EzyPlan_Interface_V2.Properties.Resources.Extrapolate;
		this.button_Extrapolate.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Extrapolate.ImageOffset = 4;
		this.button_Extrapolate.IsPressed = false;
		this.button_Extrapolate.KeepPress = false;
		this.button_Extrapolate.Location = new System.Drawing.Point(4, 60);
		this.button_Extrapolate.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Extrapolate.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Extrapolate.Name = "button_Extrapolate";
		this.button_Extrapolate.Radius = 20;
		this.button_Extrapolate.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Extrapolate.Size = new System.Drawing.Size(218, 51);
		this.button_Extrapolate.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Extrapolate.SplitDistance = 0;
		this.button_Extrapolate.TabIndex = 1;
		this.button_Extrapolate.Text = "Extrapolate";
		this.button_Extrapolate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Extrapolate.Title = "";
		this.button_Extrapolate.UseVisualStyleBackColor = true;
		this.button_Extrapolate.Enter += new System.EventHandler(this.button_Extrapolate_Enter);
		this.button_Extrapolate.Click += new System.EventHandler(this.button_Extrapolate_Click);
		this.button_Extrapolate.Leave += new System.EventHandler(this.button_Extrapolate_Leave);
		this.button_TrendAnalysis.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_TrendAnalysis.AutoEllipsis = true;
		this.button_TrendAnalysis.BackColor = System.Drawing.Color.Transparent;
		this.button_TrendAnalysis.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_TrendAnalysis.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_TrendAnalysis.ColorOn = System.Drawing.Color.Orange;
		this.button_TrendAnalysis.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_TrendAnalysis.ColorPress = System.Drawing.Color.Orange;
		this.button_TrendAnalysis.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_TrendAnalysis.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_TrendAnalysis.FadingSpeed = 35;
		this.button_TrendAnalysis.FlatAppearance.BorderSize = 0;
		this.button_TrendAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_TrendAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_TrendAnalysis.ForeColor = System.Drawing.Color.White;
		this.button_TrendAnalysis.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_TrendAnalysis.Image = EzyPlan_Interface_V2.Properties.Resources.Trend_Analysis;
		this.button_TrendAnalysis.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_TrendAnalysis.ImageOffset = 4;
		this.button_TrendAnalysis.IsPressed = false;
		this.button_TrendAnalysis.KeepPress = false;
		this.button_TrendAnalysis.Location = new System.Drawing.Point(4, 3);
		this.button_TrendAnalysis.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_TrendAnalysis.MenuPos = new System.Drawing.Point(0, 0);
		this.button_TrendAnalysis.Name = "button_TrendAnalysis";
		this.button_TrendAnalysis.Radius = 20;
		this.button_TrendAnalysis.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_TrendAnalysis.Size = new System.Drawing.Size(218, 51);
		this.button_TrendAnalysis.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_TrendAnalysis.SplitDistance = 0;
		this.button_TrendAnalysis.TabIndex = 0;
		this.button_TrendAnalysis.Text = "Trend Analysis";
		this.button_TrendAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_TrendAnalysis.Title = "";
		this.button_TrendAnalysis.UseVisualStyleBackColor = true;
		this.button_TrendAnalysis.Enter += new System.EventHandler(this.button_TrendAnalysis_Enter);
		this.button_TrendAnalysis.Click += new System.EventHandler(this.button_TrendAnalysis_Click);
		this.button_TrendAnalysis.Leave += new System.EventHandler(this.button_TrendAnalysis_Leave);
		this.panel2.Controls.Add(this.button_Automatic);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 95);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(227, 63);
		this.panel2.TabIndex = 3;
		this.button_Automatic.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Automatic.AutoEllipsis = true;
		this.button_Automatic.BackColor = System.Drawing.Color.Transparent;
		this.button_Automatic.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_Automatic.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_Automatic.ColorOn = System.Drawing.Color.Orange;
		this.button_Automatic.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_Automatic.ColorPress = System.Drawing.Color.Orange;
		this.button_Automatic.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Automatic.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Automatic.FadingSpeed = 35;
		this.button_Automatic.FlatAppearance.BorderSize = 0;
		this.button_Automatic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Automatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Automatic.ForeColor = System.Drawing.Color.White;
		this.button_Automatic.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Automatic.Image = EzyPlan_Interface_V2.Properties.Resources.Build;
		this.button_Automatic.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Automatic.ImageOffset = 4;
		this.button_Automatic.IsPressed = false;
		this.button_Automatic.KeepPress = false;
		this.button_Automatic.Location = new System.Drawing.Point(4, 6);
		this.button_Automatic.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Automatic.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Automatic.Name = "button_Automatic";
		this.button_Automatic.Radius = 20;
		this.button_Automatic.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Automatic.Size = new System.Drawing.Size(218, 51);
		this.button_Automatic.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Automatic.SplitDistance = 0;
		this.button_Automatic.TabIndex = 0;
		this.button_Automatic.Text = "Build To Target";
		this.button_Automatic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Automatic.Title = "";
		this.button_Automatic.UseVisualStyleBackColor = true;
		this.button_Automatic.Enter += new System.EventHandler(this.button_Automatic_Enter);
		this.button_Automatic.Click += new System.EventHandler(this.button_Build_Click);
		this.button_Automatic.Leave += new System.EventHandler(this.button_Automatic_Leave);
		this.panel1.Controls.Add(this.button_DrillHoleManagement);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 10);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(227, 75);
		this.panel1.TabIndex = 1;
		this.button_DrillHoleManagement.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_DrillHoleManagement.AutoEllipsis = true;
		this.button_DrillHoleManagement.BackColor = System.Drawing.Color.Transparent;
		this.button_DrillHoleManagement.ColorBase = System.Drawing.Color.FromArgb(25, 25, 112);
		this.button_DrillHoleManagement.ColorBaseStroke = System.Drawing.Color.FromArgb(100, 16, 16, 76);
		this.button_DrillHoleManagement.ColorOn = System.Drawing.Color.Orange;
		this.button_DrillHoleManagement.ColorOnStroke = System.Drawing.Color.MidnightBlue;
		this.button_DrillHoleManagement.ColorPress = System.Drawing.Color.Orange;
		this.button_DrillHoleManagement.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_DrillHoleManagement.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_DrillHoleManagement.FadingSpeed = 35;
		this.button_DrillHoleManagement.FlatAppearance.BorderSize = 0;
		this.button_DrillHoleManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_DrillHoleManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_DrillHoleManagement.ForeColor = System.Drawing.Color.White;
		this.button_DrillHoleManagement.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_DrillHoleManagement.Image = EzyPlan_Interface_V2.Properties.Resources.Management;
		this.button_DrillHoleManagement.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_DrillHoleManagement.ImageOffset = 4;
		this.button_DrillHoleManagement.IsPressed = false;
		this.button_DrillHoleManagement.KeepPress = false;
		this.button_DrillHoleManagement.Location = new System.Drawing.Point(4, 6);
		this.button_DrillHoleManagement.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_DrillHoleManagement.MenuPos = new System.Drawing.Point(0, 0);
		this.button_DrillHoleManagement.Name = "button_DrillHoleManagement";
		this.button_DrillHoleManagement.Radius = 20;
		this.button_DrillHoleManagement.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_DrillHoleManagement.Size = new System.Drawing.Size(218, 59);
		this.button_DrillHoleManagement.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_DrillHoleManagement.SplitDistance = 0;
		this.button_DrillHoleManagement.TabIndex = 0;
		this.button_DrillHoleManagement.Text = "Hole Management";
		this.button_DrillHoleManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_DrillHoleManagement.Title = "Back";
		this.button_DrillHoleManagement.UseVisualStyleBackColor = true;
		this.button_DrillHoleManagement.Enter += new System.EventHandler(this.button_DrillHoleManagement_Enter);
		this.button_DrillHoleManagement.Click += new System.EventHandler(this.button_DrillHoleManagement_Click);
		this.button_DrillHoleManagement.Leave += new System.EventHandler(this.button_DrillHoleManagement_Leave);
		this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ep_subhead4;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 85);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(227, 10);
		this.panel5.TabIndex = 2;
		this.panel7.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel7.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ep_subhead4;
		this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(227, 10);
		this.panel7.TabIndex = 0;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pictureBox1.Image = EzyPlan_Interface_V2.Properties.Resources.ezynav_logo_small;
		this.pictureBox1.Location = new System.Drawing.Point(0, 455);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(227, 75);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 5;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.GhostWhite;
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel7);
		base.Name = "Toolbar_DrillHolePlanning";
		base.Size = new System.Drawing.Size(227, 530);
		this.panel3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Toolbar_DrillHolePlanning()
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

	private void button_DrillHoleManagement_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.DrillHoleManagement;
		OnButtonPressed(e2);
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Close;
		OnButtonPressed(e2);
	}

	private void button_Build_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Build;
		OnButtonPressed(e2);
	}

	private void button_Extrapolate_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Extrapolate;
		OnButtonPressed(e2);
	}

	private void button_TrendAnalysis_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.TrendAnalysis;
		OnButtonPressed(e2);
	}

	private void button_Advanced_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Advanced;
		OnButtonPressed(e2);
	}

	private void button_SideTrackHole_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.SideTrackHole;
		OnButtonPressed(e2);
	}

	private void button_Report_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Report;
		OnButtonPressed(e2);
	}

	private void button_DrillHoleManagement_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_DrillHoleManagement);
	}

	private void button_DrillHoleManagement_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_DrillHoleManagement);
	}

	private void button_Automatic_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_Automatic);
	}

	private void button_Automatic_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_Automatic);
	}

	private void button_TrendAnalysis_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_TrendAnalysis);
	}

	private void button_TrendAnalysis_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_TrendAnalysis);
	}

	private void button_Extrapolate_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_Extrapolate);
	}

	private void button_Extrapolate_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_Extrapolate);
	}

	private void button_Advanced_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_Advanced);
	}

	private void button_Advanced_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_Advanced);
	}

	private void button_SideTrackHole_Enter(object sender, EventArgs e)
	{
		Utility.MenuButtonGotFocus(ref button_SideTrackHole);
	}

	private void button_SideTrackHole_Leave(object sender, EventArgs e)
	{
		Utility.MenuButtonLostFocus(ref button_SideTrackHole);
	}
}
