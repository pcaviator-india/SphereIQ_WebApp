using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_CreateNewHole_EnterDetails2 : Layout_Body_Wizard
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Next,
			Back,
			Cancel
		}

		private FinishedStateEnum m_finishedState;

		private DrillHoleFile m_drillHoleFile;

		public FinishedStateEnum FinishedState
		{
			get
			{
				return m_finishedState;
			}
			set
			{
				m_finishedState = value;
			}
		}

		public DrillHoleFile DrillHoleFile
		{
			get
			{
				return m_drillHoleFile;
			}
			set
			{
				m_drillHoleFile = value;
			}
		}
	}

	private IContainer components;

	private GroupBox groupBox_target;

	private TextBox textBox_Target_TVD;

	private Label label_targetTVD;

	private Panel panel_helpTarget;

	private Label label_helpTargetEasting;

	private Label label_helpTargetNorthing;

	private Label label_helpTargetTVD;

	private TextBox textBox_Target_Easting;

	private TextBox textBox_Target_Northing;

	private Label label_targetEasting;

	private Label label_targetNorthing;

	private GroupBox groupBox_collar;

	private Panel panel_helpCollar;

	private Label label_helpCollarEasting;

	private Label label_helpCollarNorthing;

	private TextBox textBox_Collar_Easting;

	private TextBox textBox_Collar_Northing;

	private Label label_easting;

	private Label label_northing;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Back;

	private ImageList imageList1;

	private Panel panel5;

	private DrillHoleFile m_drillHoleFile;

	public event EventHandler Finished;

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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EzyPlan_Interface_V2.Body_CreateNewHole_EnterDetails2));
		this.groupBox_target = new System.Windows.Forms.GroupBox();
		this.textBox_Target_TVD = new System.Windows.Forms.TextBox();
		this.label_targetTVD = new System.Windows.Forms.Label();
		this.panel_helpTarget = new System.Windows.Forms.Panel();
		this.label_helpTargetEasting = new System.Windows.Forms.Label();
		this.label_helpTargetNorthing = new System.Windows.Forms.Label();
		this.label_helpTargetTVD = new System.Windows.Forms.Label();
		this.textBox_Target_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Target_Northing = new System.Windows.Forms.TextBox();
		this.label_targetEasting = new System.Windows.Forms.Label();
		this.label_targetNorthing = new System.Windows.Forms.Label();
		this.groupBox_collar = new System.Windows.Forms.GroupBox();
		this.panel_helpCollar = new System.Windows.Forms.Panel();
		this.label_helpCollarEasting = new System.Windows.Forms.Label();
		this.label_helpCollarNorthing = new System.Windows.Forms.Label();
		this.textBox_Collar_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Collar_Northing = new System.Windows.Forms.TextBox();
		this.label_easting = new System.Windows.Forms.Label();
		this.label_northing = new System.Windows.Forms.Label();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_target.SuspendLayout();
		this.panel_helpTarget.SuspendLayout();
		this.groupBox_collar.SuspendLayout();
		this.panel_helpCollar.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(166, 24);
		base.label_headerText1.Text = "null - set in code";
		base.groupBox_frame.Size = new System.Drawing.Size(814, 420);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Location = new System.Drawing.Point(353, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(475, 61);
		base.panel_body.Location = new System.Drawing.Point(0, 53);
		base.panel_body.Size = new System.Drawing.Size(828, 434);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.panel_header.Size = new System.Drawing.Size(828, 53);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 487);
		base.panel_footer.Size = new System.Drawing.Size(828, 61);
		base.label_headerText2.Size = new System.Drawing.Size(215, 20);
		base.label_headerText2.Text = "All the fields are required.";
		base.panel_scrollPanel.Controls.Add(this.groupBox_target);
		base.panel_scrollPanel.Controls.Add(this.groupBox_collar);
		base.panel_scrollPanel.Size = new System.Drawing.Size(808, 401);
		this.groupBox_target.Controls.Add(this.textBox_Target_TVD);
		this.groupBox_target.Controls.Add(this.label_targetTVD);
		this.groupBox_target.Controls.Add(this.panel_helpTarget);
		this.groupBox_target.Controls.Add(this.textBox_Target_Easting);
		this.groupBox_target.Controls.Add(this.textBox_Target_Northing);
		this.groupBox_target.Controls.Add(this.label_targetEasting);
		this.groupBox_target.Controls.Add(this.label_targetNorthing);
		this.groupBox_target.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_target.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_target.Location = new System.Drawing.Point(0, 115);
		this.groupBox_target.Name = "groupBox_target";
		this.groupBox_target.Size = new System.Drawing.Size(808, 154);
		this.groupBox_target.TabIndex = 4;
		this.groupBox_target.TabStop = false;
		this.groupBox_target.Text = "Target Coordinates (Meters)";
		this.textBox_Target_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Target_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_TVD.Location = new System.Drawing.Point(136, 110);
		this.textBox_Target_TVD.MaxLength = 15;
		this.textBox_Target_TVD.Name = "textBox_Target_TVD";
		this.textBox_Target_TVD.Size = new System.Drawing.Size(218, 26);
		this.textBox_Target_TVD.TabIndex = 2;
		this.textBox_Target_TVD.Enter += new System.EventHandler(this.textBox_Target_TVD_Enter);
		this.textBox_Target_TVD.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Target_TVD_MouseDoubleClick);
		this.textBox_Target_TVD.Leave += new System.EventHandler(this.textBox_Target_TVD_Leave);
		this.label_targetTVD.AutoSize = true;
		this.label_targetTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetTVD.Location = new System.Drawing.Point(27, 112);
		this.label_targetTVD.Name = "label_targetTVD";
		this.label_targetTVD.Size = new System.Drawing.Size(49, 20);
		this.label_targetTVD.TabIndex = 34;
		this.label_targetTVD.Text = "TVD:";
		this.panel_helpTarget.Controls.Add(this.label_helpTargetEasting);
		this.panel_helpTarget.Controls.Add(this.label_helpTargetNorthing);
		this.panel_helpTarget.Controls.Add(this.label_helpTargetTVD);
		this.panel_helpTarget.Location = new System.Drawing.Point(501, 28);
		this.panel_helpTarget.Name = "panel_helpTarget";
		this.panel_helpTarget.Size = new System.Drawing.Size(263, 108);
		this.panel_helpTarget.TabIndex = 2;
		this.label_helpTargetEasting.AutoSize = true;
		this.label_helpTargetEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpTargetEasting.Location = new System.Drawing.Point(3, 46);
		this.label_helpTargetEasting.Name = "label_helpTargetEasting";
		this.label_helpTargetEasting.Size = new System.Drawing.Size(217, 18);
		this.label_helpTargetEasting.TabIndex = 4;
		this.label_helpTargetEasting.Text = "Easting coordinate of the target.";
		this.label_helpTargetNorthing.AutoSize = true;
		this.label_helpTargetNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpTargetNorthing.Location = new System.Drawing.Point(3, 8);
		this.label_helpTargetNorthing.Name = "label_helpTargetNorthing";
		this.label_helpTargetNorthing.Size = new System.Drawing.Size(224, 18);
		this.label_helpTargetNorthing.TabIndex = 4;
		this.label_helpTargetNorthing.Text = "Northing coordinate of the target.";
		this.label_helpTargetTVD.AutoSize = true;
		this.label_helpTargetTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpTargetTVD.Location = new System.Drawing.Point(3, 82);
		this.label_helpTargetTVD.Name = "label_helpTargetTVD";
		this.label_helpTargetTVD.Size = new System.Drawing.Size(258, 18);
		this.label_helpTargetTVD.TabIndex = 4;
		this.label_helpTargetTVD.Text = "The \"True Vertical Depth\" of the target.";
		this.textBox_Target_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_Easting.Location = new System.Drawing.Point(136, 74);
		this.textBox_Target_Easting.MaxLength = 15;
		this.textBox_Target_Easting.Name = "textBox_Target_Easting";
		this.textBox_Target_Easting.Size = new System.Drawing.Size(218, 26);
		this.textBox_Target_Easting.TabIndex = 1;
		this.textBox_Target_Easting.Enter += new System.EventHandler(this.textBox_Target_Easting_Enter);
		this.textBox_Target_Easting.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Target_Easting_MouseDoubleClick);
		this.textBox_Target_Easting.Leave += new System.EventHandler(this.textBox_Target_Easting_Leave);
		this.textBox_Target_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_Northing.Location = new System.Drawing.Point(136, 36);
		this.textBox_Target_Northing.MaxLength = 15;
		this.textBox_Target_Northing.Name = "textBox_Target_Northing";
		this.textBox_Target_Northing.Size = new System.Drawing.Size(218, 26);
		this.textBox_Target_Northing.TabIndex = 0;
		this.textBox_Target_Northing.Enter += new System.EventHandler(this.textBox_Target_Northing_Enter);
		this.textBox_Target_Northing.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Target_Northing_MouseDoubleClick);
		this.textBox_Target_Northing.Leave += new System.EventHandler(this.textBox_Target_Northing_Leave);
		this.label_targetEasting.AutoSize = true;
		this.label_targetEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetEasting.Location = new System.Drawing.Point(27, 74);
		this.label_targetEasting.Name = "label_targetEasting";
		this.label_targetEasting.Size = new System.Drawing.Size(75, 20);
		this.label_targetEasting.TabIndex = 0;
		this.label_targetEasting.Text = "Easting:";
		this.label_targetNorthing.AutoSize = true;
		this.label_targetNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetNorthing.Location = new System.Drawing.Point(27, 36);
		this.label_targetNorthing.Name = "label_targetNorthing";
		this.label_targetNorthing.Size = new System.Drawing.Size(82, 20);
		this.label_targetNorthing.TabIndex = 0;
		this.label_targetNorthing.Text = "Northing:";
		this.groupBox_collar.Controls.Add(this.panel_helpCollar);
		this.groupBox_collar.Controls.Add(this.textBox_Collar_Easting);
		this.groupBox_collar.Controls.Add(this.textBox_Collar_Northing);
		this.groupBox_collar.Controls.Add(this.label_easting);
		this.groupBox_collar.Controls.Add(this.label_northing);
		this.groupBox_collar.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_collar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_collar.Location = new System.Drawing.Point(0, 0);
		this.groupBox_collar.Name = "groupBox_collar";
		this.groupBox_collar.Size = new System.Drawing.Size(808, 115);
		this.groupBox_collar.TabIndex = 3;
		this.groupBox_collar.TabStop = false;
		this.groupBox_collar.Text = "Collar Coordinates (Meters)";
		this.panel_helpCollar.Controls.Add(this.label_helpCollarEasting);
		this.panel_helpCollar.Controls.Add(this.label_helpCollarNorthing);
		this.panel_helpCollar.Location = new System.Drawing.Point(501, 19);
		this.panel_helpCollar.Name = "panel_helpCollar";
		this.panel_helpCollar.Size = new System.Drawing.Size(261, 82);
		this.panel_helpCollar.TabIndex = 2;
		this.label_helpCollarEasting.AutoSize = true;
		this.label_helpCollarEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpCollarEasting.Location = new System.Drawing.Point(4, 48);
		this.label_helpCollarEasting.Name = "label_helpCollarEasting";
		this.label_helpCollarEasting.Size = new System.Drawing.Size(216, 18);
		this.label_helpCollarEasting.TabIndex = 4;
		this.label_helpCollarEasting.Text = "Easting coordinate of the collar.";
		this.label_helpCollarNorthing.AutoSize = true;
		this.label_helpCollarNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpCollarNorthing.Location = new System.Drawing.Point(4, 12);
		this.label_helpCollarNorthing.Name = "label_helpCollarNorthing";
		this.label_helpCollarNorthing.Size = new System.Drawing.Size(223, 18);
		this.label_helpCollarNorthing.TabIndex = 3;
		this.label_helpCollarNorthing.Text = "Northing coordinate of the collar.";
		this.textBox_Collar_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Collar_Easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Collar_Easting.Location = new System.Drawing.Point(136, 69);
		this.textBox_Collar_Easting.MaxLength = 15;
		this.textBox_Collar_Easting.Name = "textBox_Collar_Easting";
		this.textBox_Collar_Easting.Size = new System.Drawing.Size(218, 26);
		this.textBox_Collar_Easting.TabIndex = 1;
		this.textBox_Collar_Easting.Enter += new System.EventHandler(this.textBox_Collar_Easting_Enter);
		this.textBox_Collar_Easting.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Collar_Easting_MouseDoubleClick);
		this.textBox_Collar_Easting.Leave += new System.EventHandler(this.textBox_Collar_Easting_Leave);
		this.textBox_Collar_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Collar_Northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Collar_Northing.Location = new System.Drawing.Point(136, 31);
		this.textBox_Collar_Northing.MaxLength = 15;
		this.textBox_Collar_Northing.Name = "textBox_Collar_Northing";
		this.textBox_Collar_Northing.Size = new System.Drawing.Size(218, 26);
		this.textBox_Collar_Northing.TabIndex = 0;
		this.textBox_Collar_Northing.Enter += new System.EventHandler(this.textBox_Collar_Northing_Enter);
		this.textBox_Collar_Northing.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Collar_Northing_MouseDoubleClick);
		this.textBox_Collar_Northing.Leave += new System.EventHandler(this.textBox_Collar_Northing_Leave);
		this.label_easting.AutoSize = true;
		this.label_easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_easting.Location = new System.Drawing.Point(27, 67);
		this.label_easting.Name = "label_easting";
		this.label_easting.Size = new System.Drawing.Size(75, 20);
		this.label_easting.TabIndex = 0;
		this.label_easting.Text = "Easting:";
		this.label_northing.AutoSize = true;
		this.label_northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_northing.Location = new System.Drawing.Point(27, 31);
		this.label_northing.Name = "label_northing";
		this.label_northing.Size = new System.Drawing.Size(82, 20);
		this.label_northing.TabIndex = 0;
		this.label_northing.Text = "Northing:";
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
		this.button_Cancel.Location = new System.Drawing.Point(320, 6);
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
		this.button_Next.ImageIndex = 0;
		this.button_Next.ImageList = this.imageList1;
		this.button_Next.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Next.ImageOffset = 4;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(12, 6);
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
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Save.png");
		this.imageList1.Images.SetKeyName(1, "Forward.png");
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
		this.button_Back.Location = new System.Drawing.Point(166, 6);
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
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Edit_Document;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(764, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 53);
		this.panel5.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "Body_CreateNewHole_EnterDetails2";
		base.Size = new System.Drawing.Size(828, 548);
		base.Resize += new System.EventHandler(this.Body_CreateNewHole_EnterDetails2_Resize);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_target.ResumeLayout(false);
		this.groupBox_target.PerformLayout();
		this.panel_helpTarget.ResumeLayout(false);
		this.panel_helpTarget.PerformLayout();
		this.groupBox_collar.ResumeLayout(false);
		this.groupBox_collar.PerformLayout();
		this.panel_helpCollar.ResumeLayout(false);
		this.panel_helpCollar.PerformLayout();
		base.ResumeLayout(false);
	}

	public Body_CreateNewHole_EnterDetails2()
	{
		InitializeComponent();
	}

	private bool UpdateDrillHoleFile(DrillHoleFile drillHoleFile)
	{
		if (SetCollarAndTarget(drillHoleFile))
		{
			return true;
		}
		return false;
	}

	private bool StringToDecimal(string descText, string fieldValue, out decimal value)
	{
		if (fieldValue != "")
		{
			Utility.StringToDecimalResultEnum stringToDecimalResultEnum = Utility.StringToDecimal(fieldValue, out value);
			if (stringToDecimalResultEnum != Utility.StringToDecimalResultEnum.NoError)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(descText, descText + ": " + Utility.ToHumanReadable(stringToDecimalResultEnum) + ".");
				popup_MessageBox_OK.ShowDialog();
				return false;
			}
			return true;
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK(descText, descText + ": A field value is required.");
		popup_MessageBox_OK2.ShowDialog();
		value = 0m;
		return false;
	}

	private bool SetCollarAndTarget(DrillHoleFile drillHoleFile)
	{
		Target target = drillHoleFile.Target;
		Collar collar = drillHoleFile.Collar;
		drillHoleFile.Target = null;
		drillHoleFile.Collar = null;
		if (CreateCollar(out var collar2) && CreateTarget(out var target2))
		{
			drillHoleFile.Collar = collar2;
			try
			{
				drillHoleFile.Target = target2;
				return true;
			}
			catch (ArgumentException)
			{
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Drilling plan error", "The collar and target are too far appart! The maximum horizontal distance allowed between them is 24,000 meters.");
				popup_MessageBox_OK.ShowDialog();
				ResetCollarAndTarget(drillHoleFile, collar, target);
				return false;
			}
		}
		ResetCollarAndTarget(drillHoleFile, collar, target);
		return false;
	}

	private void ResetCollarAndTarget(DrillHoleFile drillHoleFile, Collar collar, Target target)
	{
		drillHoleFile.Target = null;
		drillHoleFile.Collar = null;
		drillHoleFile.Collar = collar;
		drillHoleFile.Target = target;
	}

	private bool CreateCollar(out Collar collar)
	{
		collar = new Collar();
		if (StringToDecimal("Collar Northing", textBox_Collar_Northing.Text, out var value))
		{
			if (!DisplayError("Collar Northing", collar.SetNorthing(value)))
			{
				if (StringToDecimal("Collar Easting", textBox_Collar_Easting.Text, out var value2))
				{
					if (!DisplayError("Collar Easting", collar.SetEasting(value2)))
					{
						return true;
					}
					return false;
				}
				textBox_Collar_Easting.Focus();
				return false;
			}
			return false;
		}
		textBox_Collar_Northing.Focus();
		return false;
	}

	private bool CreateTarget(out Target target)
	{
		target = new Target();
		if (StringToDecimal("Target Northing", textBox_Target_Northing.Text, out var value))
		{
			if (!DisplayError("Target Northing", target.SetNorthing(value)))
			{
				if (StringToDecimal("Target Easting", textBox_Target_Easting.Text, out var value2))
				{
					if (!DisplayError("Target Easting", target.SetEasting(value2)))
					{
						if (StringToDecimal("Target TVD", textBox_Target_TVD.Text, out var value3))
						{
							if (!DisplayError("Target TVD", target.SetTVD(value3)))
							{
								return true;
							}
							return false;
						}
						textBox_Target_TVD.Focus();
						return false;
					}
					return false;
				}
				textBox_Target_Easting.Focus();
				return false;
			}
			return false;
		}
		textBox_Target_Northing.Focus();
		return false;
	}

	private bool DisplayError(string desc, Collar.SetEastingEnum value)
	{
		if (value != Collar.SetEastingEnum.Set)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(desc, desc + ": " + Collar.ToHumanReadable(value) + ".");
			popup_MessageBox_OK.ShowDialog();
			textBox_Collar_Easting.Focus();
			return true;
		}
		return false;
	}

	private bool DisplayError(string desc, Collar.SetNorthingEnum value)
	{
		if (value != Collar.SetNorthingEnum.Set)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(desc, desc + ": " + Collar.ToHumanReadable(value) + ".");
			popup_MessageBox_OK.ShowDialog();
			textBox_Collar_Northing.Focus();
			return true;
		}
		return false;
	}

	private bool DisplayError(string desc, Target.SetEastingEnum value)
	{
		if (value != Target.SetEastingEnum.Set)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(desc, desc + ": " + Target.ToHumanReadable(value) + ".");
			popup_MessageBox_OK.ShowDialog();
			textBox_Target_Easting.Focus();
			return true;
		}
		return false;
	}

	private bool DisplayError(string desc, Target.SetNorthingEnum value)
	{
		if (value != Target.SetNorthingEnum.Set)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(desc, desc + ": " + Target.ToHumanReadable(value) + ".");
			popup_MessageBox_OK.ShowDialog();
			textBox_Target_Northing.Focus();
			return true;
		}
		return false;
	}

	private bool DisplayError(string desc, Target.SetTVDEnum value)
	{
		if (value != Target.SetTVDEnum.Set)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(desc, desc + ": " + Target.ToHumanReadable(value) + ".");
			popup_MessageBox_OK.ShowDialog();
			textBox_Target_TVD.Focus();
			return true;
		}
		return false;
	}

	public void Display(DrillHoleFile drillHoleFile, bool editMode, bool refreshFields)
	{
		m_drillHoleFile = drillHoleFile;
		if (editMode)
		{
			button_Next.Text = "Save";
			button_Next.Image = imageList1.Images[0];
			label_headerText1.Text = "Enter the required hole details below and press \"Save\" to continue:";
		}
		else
		{
			button_Next.Text = "Next";
			button_Next.Image = imageList1.Images[1];
			label_headerText1.Text = "Enter the required hole details below and press \"Next\" to continue:";
		}
		textBox_Collar_Northing.Focus();
		if (refreshFields)
		{
			DisplayFields(drillHoleFile);
		}
	}

	public void DisplayFields(DrillHoleFile drillHoleFile)
	{
		if (drillHoleFile.Collar != null)
		{
			textBox_Collar_Northing.Text = drillHoleFile.Collar.Northing.ToString();
			textBox_Collar_Easting.Text = drillHoleFile.Collar.Easting.ToString();
		}
		else
		{
			textBox_Collar_Northing.Text = "";
			textBox_Collar_Easting.Text = "";
		}
		if (drillHoleFile.Target != null)
		{
			textBox_Target_Northing.Text = drillHoleFile.Target.Northing.ToString();
			textBox_Target_Easting.Text = drillHoleFile.Target.Easting.ToString();
			textBox_Target_TVD.Text = drillHoleFile.Target.TVD.ToString();
		}
		else
		{
			textBox_Target_Northing.Text = "";
			textBox_Target_Easting.Text = "";
			textBox_Target_TVD.Text = "";
		}
	}

	private void OnFinished(FinishedEventArgs e)
	{
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void Next()
	{
		if (UpdateDrillHoleFile(m_drillHoleFile))
		{
			FinishedEventArgs e = new FinishedEventArgs();
			e.DrillHoleFile = m_drillHoleFile;
			e.FinishedState = FinishedEventArgs.FinishedStateEnum.Next;
			OnFinished(e);
		}
	}

	private void Back()
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = FinishedEventArgs.FinishedStateEnum.Back;
		e.DrillHoleFile = m_drillHoleFile;
		OnFinished(e);
	}

	private void Cancel()
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
		OnFinished(e);
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		Back();
	}

	private void AdjustLayout()
	{
		int num = Width;
		if (num < 840)
		{
			panel_helpCollar.Visible = false;
			panel_helpTarget.Visible = false;
		}
		else
		{
			panel_helpCollar.Visible = true;
			panel_helpTarget.Visible = true;
		}
	}

	private void Body_CreateNewHole_EnterDetails2_Resize(object sender, EventArgs e)
	{
		AdjustLayout();
	}

	private void textBox_Collar_Northing_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Collar_Northing);
	}

	private void textBox_Collar_Northing_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Collar_Northing);
	}

	private void textBox_Collar_Easting_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Collar_Easting);
	}

	private void textBox_Collar_Easting_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Collar_Easting);
	}

	private void textBox_Target_Northing_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_Northing);
	}

	private void textBox_Target_Northing_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_Northing);
	}

	private void textBox_Target_Easting_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_Easting);
	}

	private void textBox_Target_Easting_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_Easting);
	}

	private void textBox_Target_TVD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_TVD);
	}

	private void textBox_Target_TVD_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_TVD);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
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

	private void textBox_Collar_Northing_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Collar_Northing);
	}

	private void textBox_Collar_Easting_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Collar_Easting);
	}

	private void textBox_Target_Northing_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_Northing);
	}

	private void textBox_Target_Easting_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_Easting);
	}

	private void textBox_Target_TVD_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_TVD);
	}
}
