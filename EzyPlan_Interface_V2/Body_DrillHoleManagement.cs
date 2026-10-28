using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;

namespace EzyPlan_Interface_V2;

public class Body_DrillHoleManagement : Layout_InfoAndGraph
{
	private IContainer components;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Label label_CreatedDateLabel;

	private Label label_CreatedByLabel;

	private Label label_RigIDLabel;

	private Label label_HoleIDLabel;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private Panel panel_ScollPanel;

	private GroupBox groupBox_Collar;

	private Label label_CollarEastingLabel;

	private GroupBox groupBox_Target;

	private Label label_TargetTVDLabel;

	private Label label_TargetNorthingLabel;

	private Label label_TargetEastingLabel;

	private Label label_CollarNorthingLabel;

	private GroupBox groupBox_Identification;

	private Label label_ClientLabel;

	private Label label_DrillingContractorLabel;

	private Label label_AreaLabel;

	private GroupBox groupBox_Sidetrack;

	private Label label_SidetrackTVDLabel;

	private Label label_SidetrackMDLabel;

	private Panel panel1;

	private Label label_SurveyingContractorLabel;

	private Label label_DrillingContractor;

	private Label label_SurveyingContractor;

	private Label label_Client;

	private Label label_Area;

	private Label label_Location;

	private Label label_RigID;

	private Label label_HoleID;

	private Label label_SidetrackTVD;

	private Label label_SidetrackMD;

	private Label label_TargetTVD;

	private Label label_TargetNorthing;

	private Label label_TargetEasting;

	private Label label_CollarNorthing;

	private Label label_CollarEasting;

	private Label label_CreatedDate;

	private DrillHoleFile m_drillHoleFile;

	private EzyNavHoleChart m_chart;

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
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.groupBox_Sidetrack = new System.Windows.Forms.GroupBox();
		this.label_SidetrackTVD = new System.Windows.Forms.Label();
		this.label_SidetrackMD = new System.Windows.Forms.Label();
		this.label_SidetrackTVDLabel = new System.Windows.Forms.Label();
		this.label_SidetrackMDLabel = new System.Windows.Forms.Label();
		this.groupBox_Target = new System.Windows.Forms.GroupBox();
		this.label_TargetTVD = new System.Windows.Forms.Label();
		this.label_TargetNorthing = new System.Windows.Forms.Label();
		this.label_TargetEasting = new System.Windows.Forms.Label();
		this.label_TargetTVDLabel = new System.Windows.Forms.Label();
		this.label_TargetNorthingLabel = new System.Windows.Forms.Label();
		this.label_TargetEastingLabel = new System.Windows.Forms.Label();
		this.groupBox_Collar = new System.Windows.Forms.GroupBox();
		this.label_CollarNorthing = new System.Windows.Forms.Label();
		this.label_CollarEasting = new System.Windows.Forms.Label();
		this.label_CollarNorthingLabel = new System.Windows.Forms.Label();
		this.label_CollarEastingLabel = new System.Windows.Forms.Label();
		this.groupBox_Identification = new System.Windows.Forms.GroupBox();
		this.label_CreatedDate = new System.Windows.Forms.Label();
		this.label_DrillingContractor = new System.Windows.Forms.Label();
		this.label_SurveyingContractor = new System.Windows.Forms.Label();
		this.label_Client = new System.Windows.Forms.Label();
		this.label_Area = new System.Windows.Forms.Label();
		this.label_Location = new System.Windows.Forms.Label();
		this.label_RigID = new System.Windows.Forms.Label();
		this.label_HoleID = new System.Windows.Forms.Label();
		this.label_HoleIDLabel = new System.Windows.Forms.Label();
		this.label_RigIDLabel = new System.Windows.Forms.Label();
		this.label_ClientLabel = new System.Windows.Forms.Label();
		this.label_SurveyingContractorLabel = new System.Windows.Forms.Label();
		this.label_DrillingContractorLabel = new System.Windows.Forms.Label();
		this.label_AreaLabel = new System.Windows.Forms.Label();
		this.label_CreatedByLabel = new System.Windows.Forms.Label();
		this.label_CreatedDateLabel = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.panel_ScollPanel = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.panel1 = new System.Windows.Forms.Panel();
		base.panel_Body.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox_Sidetrack.SuspendLayout();
		this.groupBox_Target.SuspendLayout();
		this.groupBox_Collar.SuspendLayout();
		this.groupBox_Identification.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.panel_ScollPanel.SuspendLayout();
		base.SuspendLayout();
		base.panel_Body.Controls.Add(this.tabControl1);
		base.panel_Body.Location = new System.Drawing.Point(0, 44);
		base.panel_Body.Size = new System.Drawing.Size(349, 732);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(349, 44);
		base.panel_Header.Controls.SetChildIndex(base.label_HeaderText1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		base.label_HeaderText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_HeaderText1.Size = new System.Drawing.Size(187, 24);
		base.label_HeaderText1.Text = "Hole Management:";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(3, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(341, 724);
		this.tabControl1.TabIndex = 0;
		this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
		this.tabPage1.AutoScroll = true;
		this.tabPage1.Controls.Add(this.groupBox_Sidetrack);
		this.tabPage1.Controls.Add(this.groupBox_Target);
		this.tabPage1.Controls.Add(this.groupBox_Collar);
		this.tabPage1.Controls.Add(this.groupBox_Identification);
		this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage1.Location = new System.Drawing.Point(4, 34);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(333, 686);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Hole Details";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackTVD);
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackMD);
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackTVDLabel);
		this.groupBox_Sidetrack.Controls.Add(this.label_SidetrackMDLabel);
		this.groupBox_Sidetrack.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Sidetrack.Location = new System.Drawing.Point(3, 534);
		this.groupBox_Sidetrack.Name = "groupBox_Sidetrack";
		this.groupBox_Sidetrack.Size = new System.Drawing.Size(327, 77);
		this.groupBox_Sidetrack.TabIndex = 7;
		this.groupBox_Sidetrack.TabStop = false;
		this.groupBox_Sidetrack.Text = "Sidetrack depth (m)";
		this.label_SidetrackTVD.AutoSize = true;
		this.label_SidetrackTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackTVD.Location = new System.Drawing.Point(101, 50);
		this.label_SidetrackTVD.Name = "label_SidetrackTVD";
		this.label_SidetrackTVD.Size = new System.Drawing.Size(150, 20);
		this.label_SidetrackTVD.TabIndex = 3;
		this.label_SidetrackTVD.Text = "label_SidetrackTVD";
		this.label_SidetrackTVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_SidetrackMD.AutoSize = true;
		this.label_SidetrackMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackMD.Location = new System.Drawing.Point(102, 30);
		this.label_SidetrackMD.Name = "label_SidetrackMD";
		this.label_SidetrackMD.Size = new System.Drawing.Size(143, 20);
		this.label_SidetrackMD.TabIndex = 3;
		this.label_SidetrackMD.Text = "label_SidetrackMD";
		this.label_SidetrackMD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_SidetrackTVDLabel.AutoSize = true;
		this.label_SidetrackTVDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackTVDLabel.Location = new System.Drawing.Point(6, 50);
		this.label_SidetrackTVDLabel.Name = "label_SidetrackTVDLabel";
		this.label_SidetrackTVDLabel.Size = new System.Drawing.Size(49, 20);
		this.label_SidetrackTVDLabel.TabIndex = 3;
		this.label_SidetrackTVDLabel.Text = "TVD:";
		this.label_SidetrackTVDLabel.Click += new System.EventHandler(this.label10_Click);
		this.label_SidetrackMDLabel.AutoSize = true;
		this.label_SidetrackMDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SidetrackMDLabel.Location = new System.Drawing.Point(6, 30);
		this.label_SidetrackMDLabel.Name = "label_SidetrackMDLabel";
		this.label_SidetrackMDLabel.Size = new System.Drawing.Size(41, 20);
		this.label_SidetrackMDLabel.TabIndex = 3;
		this.label_SidetrackMDLabel.Text = "MD:";
		this.label_SidetrackMDLabel.Click += new System.EventHandler(this.label11_Click);
		this.groupBox_Target.Controls.Add(this.label_TargetTVD);
		this.groupBox_Target.Controls.Add(this.label_TargetNorthing);
		this.groupBox_Target.Controls.Add(this.label_TargetEasting);
		this.groupBox_Target.Controls.Add(this.label_TargetTVDLabel);
		this.groupBox_Target.Controls.Add(this.label_TargetNorthingLabel);
		this.groupBox_Target.Controls.Add(this.label_TargetEastingLabel);
		this.groupBox_Target.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Target.Location = new System.Drawing.Point(3, 432);
		this.groupBox_Target.Name = "groupBox_Target";
		this.groupBox_Target.Size = new System.Drawing.Size(327, 102);
		this.groupBox_Target.TabIndex = 4;
		this.groupBox_Target.TabStop = false;
		this.groupBox_Target.Text = "Target (m)";
		this.label_TargetTVD.AutoSize = true;
		this.label_TargetTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVD.Location = new System.Drawing.Point(102, 73);
		this.label_TargetTVD.Name = "label_TargetTVD";
		this.label_TargetTVD.Size = new System.Drawing.Size(129, 20);
		this.label_TargetTVD.TabIndex = 3;
		this.label_TargetTVD.Text = "label_TargetTVD";
		this.label_TargetTVD.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetNorthing.AutoSize = true;
		this.label_TargetNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetNorthing.Location = new System.Drawing.Point(102, 53);
		this.label_TargetNorthing.Name = "label_TargetNorthing";
		this.label_TargetNorthing.Size = new System.Drawing.Size(157, 20);
		this.label_TargetNorthing.TabIndex = 3;
		this.label_TargetNorthing.Text = "label_TargetNorthing";
		this.label_TargetNorthing.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetEasting.AutoSize = true;
		this.label_TargetEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetEasting.Location = new System.Drawing.Point(102, 33);
		this.label_TargetEasting.Name = "label_TargetEasting";
		this.label_TargetEasting.Size = new System.Drawing.Size(151, 20);
		this.label_TargetEasting.TabIndex = 3;
		this.label_TargetEasting.Text = "label_TargetEasting";
		this.label_TargetEasting.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_TargetTVDLabel.AutoSize = true;
		this.label_TargetTVDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetTVDLabel.Location = new System.Drawing.Point(6, 73);
		this.label_TargetTVDLabel.Name = "label_TargetTVDLabel";
		this.label_TargetTVDLabel.Size = new System.Drawing.Size(49, 20);
		this.label_TargetTVDLabel.TabIndex = 3;
		this.label_TargetTVDLabel.Text = "TVD:";
		this.label_TargetTVDLabel.Click += new System.EventHandler(this.label6_Click);
		this.label_TargetNorthingLabel.AutoSize = true;
		this.label_TargetNorthingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetNorthingLabel.Location = new System.Drawing.Point(6, 53);
		this.label_TargetNorthingLabel.Name = "label_TargetNorthingLabel";
		this.label_TargetNorthingLabel.Size = new System.Drawing.Size(82, 20);
		this.label_TargetNorthingLabel.TabIndex = 3;
		this.label_TargetNorthingLabel.Text = "Northing:";
		this.label_TargetNorthingLabel.Click += new System.EventHandler(this.label5_Click);
		this.label_TargetEastingLabel.AutoSize = true;
		this.label_TargetEastingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TargetEastingLabel.Location = new System.Drawing.Point(6, 33);
		this.label_TargetEastingLabel.Name = "label_TargetEastingLabel";
		this.label_TargetEastingLabel.Size = new System.Drawing.Size(75, 20);
		this.label_TargetEastingLabel.TabIndex = 3;
		this.label_TargetEastingLabel.Text = "Easting:";
		this.label_TargetEastingLabel.Click += new System.EventHandler(this.label4_Click);
		this.groupBox_Collar.Controls.Add(this.label_CollarNorthing);
		this.groupBox_Collar.Controls.Add(this.label_CollarEasting);
		this.groupBox_Collar.Controls.Add(this.label_CollarNorthingLabel);
		this.groupBox_Collar.Controls.Add(this.label_CollarEastingLabel);
		this.groupBox_Collar.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Collar.Location = new System.Drawing.Point(3, 346);
		this.groupBox_Collar.Name = "groupBox_Collar";
		this.groupBox_Collar.Size = new System.Drawing.Size(327, 86);
		this.groupBox_Collar.TabIndex = 3;
		this.groupBox_Collar.TabStop = false;
		this.groupBox_Collar.Text = "Collar (m)";
		this.label_CollarNorthing.AutoSize = true;
		this.label_CollarNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarNorthing.Location = new System.Drawing.Point(102, 50);
		this.label_CollarNorthing.Name = "label_CollarNorthing";
		this.label_CollarNorthing.Size = new System.Drawing.Size(151, 20);
		this.label_CollarNorthing.TabIndex = 3;
		this.label_CollarNorthing.Text = "label_CollarNorthing";
		this.label_CollarNorthing.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_CollarEasting.AutoSize = true;
		this.label_CollarEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarEasting.Location = new System.Drawing.Point(102, 30);
		this.label_CollarEasting.Name = "label_CollarEasting";
		this.label_CollarEasting.Size = new System.Drawing.Size(145, 20);
		this.label_CollarEasting.TabIndex = 3;
		this.label_CollarEasting.Text = "label_CollarEasting";
		this.label_CollarEasting.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label_CollarNorthingLabel.AutoSize = true;
		this.label_CollarNorthingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarNorthingLabel.Location = new System.Drawing.Point(8, 50);
		this.label_CollarNorthingLabel.Name = "label_CollarNorthingLabel";
		this.label_CollarNorthingLabel.Size = new System.Drawing.Size(82, 20);
		this.label_CollarNorthingLabel.TabIndex = 3;
		this.label_CollarNorthingLabel.Text = "Northing:";
		this.label_CollarEastingLabel.AutoSize = true;
		this.label_CollarEastingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CollarEastingLabel.Location = new System.Drawing.Point(8, 30);
		this.label_CollarEastingLabel.Name = "label_CollarEastingLabel";
		this.label_CollarEastingLabel.Size = new System.Drawing.Size(75, 20);
		this.label_CollarEastingLabel.TabIndex = 3;
		this.label_CollarEastingLabel.Text = "Easting:";
		this.groupBox_Identification.Controls.Add(this.label_CreatedDate);
		this.groupBox_Identification.Controls.Add(this.label_DrillingContractor);
		this.groupBox_Identification.Controls.Add(this.label_SurveyingContractor);
		this.groupBox_Identification.Controls.Add(this.label_Client);
		this.groupBox_Identification.Controls.Add(this.label_Area);
		this.groupBox_Identification.Controls.Add(this.label_Location);
		this.groupBox_Identification.Controls.Add(this.label_RigID);
		this.groupBox_Identification.Controls.Add(this.label_HoleID);
		this.groupBox_Identification.Controls.Add(this.label_HoleIDLabel);
		this.groupBox_Identification.Controls.Add(this.label_RigIDLabel);
		this.groupBox_Identification.Controls.Add(this.label_ClientLabel);
		this.groupBox_Identification.Controls.Add(this.label_SurveyingContractorLabel);
		this.groupBox_Identification.Controls.Add(this.label_DrillingContractorLabel);
		this.groupBox_Identification.Controls.Add(this.label_AreaLabel);
		this.groupBox_Identification.Controls.Add(this.label_CreatedByLabel);
		this.groupBox_Identification.Controls.Add(this.label_CreatedDateLabel);
		this.groupBox_Identification.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Identification.Location = new System.Drawing.Point(3, 3);
		this.groupBox_Identification.Name = "groupBox_Identification";
		this.groupBox_Identification.Size = new System.Drawing.Size(327, 343);
		this.groupBox_Identification.TabIndex = 6;
		this.groupBox_Identification.TabStop = false;
		this.groupBox_Identification.Text = "Identification ";
		this.label_CreatedDate.AutoSize = true;
		this.label_CreatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CreatedDate.Location = new System.Drawing.Point(135, 316);
		this.label_CreatedDate.Name = "label_CreatedDate";
		this.label_CreatedDate.Size = new System.Drawing.Size(143, 20);
		this.label_CreatedDate.TabIndex = 3;
		this.label_CreatedDate.Text = "label_CreatedDate";
		this.label_DrillingContractor.AutoSize = true;
		this.label_DrillingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DrillingContractor.Location = new System.Drawing.Point(101, 277);
		this.label_DrillingContractor.Name = "label_DrillingContractor";
		this.label_DrillingContractor.Size = new System.Drawing.Size(177, 16);
		this.label_DrillingContractor.TabIndex = 3;
		this.label_DrillingContractor.Text = "label_DrillingContractorlabel";
		this.label_SurveyingContractor.AutoSize = true;
		this.label_SurveyingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SurveyingContractor.Location = new System.Drawing.Point(101, 228);
		this.label_SurveyingContractor.Name = "label_SurveyingContractor";
		this.label_SurveyingContractor.Size = new System.Drawing.Size(166, 16);
		this.label_SurveyingContractor.TabIndex = 3;
		this.label_SurveyingContractor.Text = "label_SurveyingContractor";
		this.label_Client.AutoSize = true;
		this.label_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Client.Location = new System.Drawing.Point(102, 169);
		this.label_Client.Name = "label_Client";
		this.label_Client.Size = new System.Drawing.Size(78, 16);
		this.label_Client.TabIndex = 3;
		this.label_Client.Text = "label_Client";
		this.label_Area.AutoSize = true;
		this.label_Area.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Area.Location = new System.Drawing.Point(102, 137);
		this.label_Area.Name = "label_Area";
		this.label_Area.Size = new System.Drawing.Size(74, 16);
		this.label_Area.TabIndex = 3;
		this.label_Area.Text = "label_Area";
		this.label_Location.AutoSize = true;
		this.label_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Location.Location = new System.Drawing.Point(101, 109);
		this.label_Location.Name = "label_Location";
		this.label_Location.Size = new System.Drawing.Size(96, 16);
		this.label_Location.TabIndex = 3;
		this.label_Location.Text = "label_Location";
		this.label_RigID.AutoSize = true;
		this.label_RigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_RigID.Location = new System.Drawing.Point(102, 57);
		this.label_RigID.Name = "label_RigID";
		this.label_RigID.Size = new System.Drawing.Size(92, 20);
		this.label_RigID.TabIndex = 3;
		this.label_RigID.Text = "label_RigID";
		this.label_HoleID.AutoSize = true;
		this.label_HoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleID.Location = new System.Drawing.Point(102, 27);
		this.label_HoleID.Name = "label_HoleID";
		this.label_HoleID.Size = new System.Drawing.Size(101, 20);
		this.label_HoleID.TabIndex = 3;
		this.label_HoleID.Text = "label_HoleID";
		this.label_HoleIDLabel.AutoSize = true;
		this.label_HoleIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleIDLabel.Location = new System.Drawing.Point(0, 28);
		this.label_HoleIDLabel.Name = "label_HoleIDLabel";
		this.label_HoleIDLabel.Size = new System.Drawing.Size(75, 20);
		this.label_HoleIDLabel.TabIndex = 0;
		this.label_HoleIDLabel.Text = "Hole ID:";
		this.label_RigIDLabel.AutoSize = true;
		this.label_RigIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_RigIDLabel.Location = new System.Drawing.Point(0, 57);
		this.label_RigIDLabel.Name = "label_RigIDLabel";
		this.label_RigIDLabel.Size = new System.Drawing.Size(60, 20);
		this.label_RigIDLabel.TabIndex = 0;
		this.label_RigIDLabel.Text = "RigID:";
		this.label_ClientLabel.AutoSize = true;
		this.label_ClientLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_ClientLabel.Location = new System.Drawing.Point(0, 169);
		this.label_ClientLabel.Name = "label_ClientLabel";
		this.label_ClientLabel.Size = new System.Drawing.Size(60, 20);
		this.label_ClientLabel.TabIndex = 1;
		this.label_ClientLabel.Text = "Client:";
		this.label_SurveyingContractorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SurveyingContractorLabel.Location = new System.Drawing.Point(0, 206);
		this.label_SurveyingContractorLabel.Name = "label_SurveyingContractorLabel";
		this.label_SurveyingContractorLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.label_SurveyingContractorLabel.Size = new System.Drawing.Size(100, 48);
		this.label_SurveyingContractorLabel.TabIndex = 1;
		this.label_SurveyingContractorLabel.Text = "Surveying Contractor:";
		this.label_DrillingContractorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DrillingContractorLabel.Location = new System.Drawing.Point(0, 254);
		this.label_DrillingContractorLabel.Name = "label_DrillingContractorLabel";
		this.label_DrillingContractorLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.label_DrillingContractorLabel.Size = new System.Drawing.Size(100, 51);
		this.label_DrillingContractorLabel.TabIndex = 1;
		this.label_DrillingContractorLabel.Text = "Drilling Contractor:";
		this.label_AreaLabel.AutoSize = true;
		this.label_AreaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_AreaLabel.Location = new System.Drawing.Point(0, 137);
		this.label_AreaLabel.Name = "label_AreaLabel";
		this.label_AreaLabel.Size = new System.Drawing.Size(52, 20);
		this.label_AreaLabel.TabIndex = 1;
		this.label_AreaLabel.Text = "Area:";
		this.label_CreatedByLabel.AutoSize = true;
		this.label_CreatedByLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CreatedByLabel.Location = new System.Drawing.Point(0, 105);
		this.label_CreatedByLabel.Name = "label_CreatedByLabel";
		this.label_CreatedByLabel.Size = new System.Drawing.Size(83, 20);
		this.label_CreatedByLabel.TabIndex = 1;
		this.label_CreatedByLabel.Text = "Location:";
		this.label_CreatedDateLabel.AutoSize = true;
		this.label_CreatedDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CreatedDateLabel.Location = new System.Drawing.Point(20, 316);
		this.label_CreatedDateLabel.Name = "label_CreatedDateLabel";
		this.label_CreatedDateLabel.Size = new System.Drawing.Size(122, 20);
		this.label_CreatedDateLabel.TabIndex = 1;
		this.label_CreatedDateLabel.Text = "Created Date:";
		this.tabPage2.AutoScroll = true;
		this.tabPage2.Controls.Add(this.panel_ScollPanel);
		this.tabPage2.Location = new System.Drawing.Point(4, 34);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(322, 559);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Chart Options";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.panel_ScollPanel.AutoScroll = true;
		this.panel_ScollPanel.AutoScrollMinSize = new System.Drawing.Size(0, 460);
		this.panel_ScollPanel.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScollPanel.Location = new System.Drawing.Point(3, 3);
		this.panel_ScollPanel.Name = "panel_ScollPanel";
		this.panel_ScollPanel.Size = new System.Drawing.Size(316, 553);
		this.panel_ScollPanel.TabIndex = 1;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Cursor = System.Windows.Forms.Cursors.Default;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(316, 553);
		this.ezyNavHoleChartController1.TabIndex = 0;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Management;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(285, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 44);
		this.panel1.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_DrillHoleManagement";
		base.Size = new System.Drawing.Size(351, 778);
		base.panel_Body.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.groupBox_Sidetrack.ResumeLayout(false);
		this.groupBox_Sidetrack.PerformLayout();
		this.groupBox_Target.ResumeLayout(false);
		this.groupBox_Target.PerformLayout();
		this.groupBox_Collar.ResumeLayout(false);
		this.groupBox_Collar.PerformLayout();
		this.groupBox_Identification.ResumeLayout(false);
		this.groupBox_Identification.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.panel_ScollPanel.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_DrillHoleManagement(ref DrillHoleFile drillHoleFile, ref EzyNavHoleChart chart)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_drillHoleFile = drillHoleFile;
		m_chart = chart;
		Visible = false;
	}

	public void Display()
	{
		tabControl1.SelectedIndex = 0;
		tabPage1.AutoScrollPosition = new Point(0, 0);
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		Visible = true;
		DisplayFields();
		DisplayCalculatedData(ref m_chart);
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	public void DisplayFields()
	{
		label_HoleID.Text = m_drillHoleFile.HoleID;
		label_RigID.Text = m_drillHoleFile.RigID;
		label_Location.Text = m_drillHoleFile.Location;
		label_Area.Text = m_drillHoleFile.Area;
		label_DrillingContractor.Text = m_drillHoleFile.DrillingContractor;
		label_SurveyingContractor.Text = m_drillHoleFile.SurveyingContractor;
		label_Client.Text = m_drillHoleFile.Client;
		label_CreatedDate.Text = m_drillHoleFile.CreatedDate.ToString("dd/MM/yyyy");
		label_CollarNorthing.Text = m_drillHoleFile.Collar.Northing.ToString();
		label_CollarEasting.Text = m_drillHoleFile.Collar.Easting.ToString();
		label_TargetNorthing.Text = m_drillHoleFile.Target.Northing.ToString();
		label_TargetEasting.Text = m_drillHoleFile.Target.Easting.ToString();
		label_TargetTVD.Text = m_drillHoleFile.Target.TVD.ToString();
	}

	private void DisplayCalculatedData(ref EzyNavHoleChart chart)
	{
		Collar collar = m_drillHoleFile.Collar;
		Target target = m_drillHoleFile.Target;
		SurveyList surveyList = m_drillHoleFile.SurveyList;
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		NavMath navMath = new NavMath();
		navMath.AbsoluteSurvey(collar, surveyList, ref absoluteSurveyList);
		AbsoluteSurvey extrapolatedSurvey;
		if (m_drillHoleFile.DrillHoleType == DrillHoleFile.DrillHoleTypeEnum.motherHole)
		{
			HideSidetrackFields();
			DisplayChartData(ref chart, collar, target, absoluteSurveyList);
		}
		else if (m_drillHoleFile.DrillHoleType == DrillHoleFile.DrillHoleTypeEnum.daughterHole)
		{
			navMath.SideTrackHole(absoluteSurveyList, m_drillHoleFile.SideTrackMD, out extrapolatedSurvey);
			DisplaySidetrackFields(m_drillHoleFile.SideTrackMD, extrapolatedSurvey.TVD);
			DisplayChartData(ref chart, collar, target, absoluteSurveyList, extrapolatedSurvey);
		}
		else
		{
			navMath.SideTrackHole(absoluteSurveyList, m_drillHoleFile.SideTrackMD, out extrapolatedSurvey);
			DisplaySidetrackFields(m_drillHoleFile.SideTrackMD, "No survey data");
			DisplayChartData(ref chart, collar, target, absoluteSurveyList, null);
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Daughter hole plan error!", "There is no survey data at the MD of the sidetrack. The drilling plan cannot be displayed until the survey data reaches a MD of " + m_drillHoleFile.SideTrackMD + " meters.");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void DisplayChartData(ref EzyNavHoleChart chart, Collar collar, Target target, AbsoluteSurveyList surveyData, AbsoluteSurvey sidetrackStart)
	{
		chart.ClearChartData();
		chart.ResetChart();
		chart.SetTarget(target, visible: true);
		chart.SetCollar(collar, visible: true);
		chart.SetSurveyData(surveyData, visible: true);
		chart.SetHolePlan(sidetrackStart, target, visible: true);
		chart.SetSideTrack(sidetrackStart, visible: true);
	}

	private void DisplayChartData(ref EzyNavHoleChart chart, Collar collar, Target target, AbsoluteSurveyList surveyData)
	{
		chart.ClearChartData();
		chart.ResetChart();
		chart.SetTarget(target, visible: true);
		chart.SetCollar(collar, visible: true);
		chart.SetSurveyData(surveyData, visible: true);
		chart.SetHolePlan(collar, target, visible: true);
	}

	private void DisplaySidetrackFields(decimal sidetrackMD, decimal sidetrackTVD)
	{
		DisplaySidetrackFields(sidetrackMD, sidetrackTVD.ToString());
	}

	private void DisplaySidetrackFields(decimal sidetrackMD, string sidetrackTVDText)
	{
		groupBox_Sidetrack.Visible = true;
		label_SidetrackTVD.Text = sidetrackTVDText;
		label_SidetrackMD.Text = sidetrackMD.ToString();
	}

	private void HideSidetrackFields()
	{
		groupBox_Sidetrack.Visible = false;
		label_SidetrackTVD.Text = "";
		label_SidetrackMD.Text = "";
	}

	private void tabControl1_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void label5_Click(object sender, EventArgs e)
	{
	}

	private void label6_Click(object sender, EventArgs e)
	{
	}

	private void label11_Click(object sender, EventArgs e)
	{
	}

	private void label10_Click(object sender, EventArgs e)
	{
	}

	private void textBox_CreatedDate_TextChanged(object sender, EventArgs e)
	{
	}

	private void textBox_TargetEasting_TextChanged(object sender, EventArgs e)
	{
	}
}
