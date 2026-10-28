using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_SideTrackHole : Layout_Body_Wizard
{
	private enum DepthStateEnum
	{
		Extrapolated,
		Found,
		OutOfRange,
		Invalid,
		NoneEntered
	}

	private enum Target_TVDStateEnum
	{
		OutOfRange,
		Invalid,
		NoneEntered,
		Accepted
	}

	private enum Target_NorthingStateEnum
	{
		OutOfRange,
		Invalid,
		NoneEntered,
		Accepted
	}

	private enum Target_StateEnum
	{
		Set,
		Incomplete,
		Invalid
	}

	private enum Target_EastingStateEnum
	{
		OutOfRange,
		Invalid,
		NoneEntered,
		Accepted
	}

	public enum MinDLStateEnum
	{
		NotCalculated,
		TargetNotAchieved,
		NormalDogleg,
		SevereDogleg,
		ExtremeDogleg,
		Error
	}

	private enum DoglegColourCodes
	{
		Extreme,
		Severe,
		Normal
	}

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Next,
			Cancel
		}

		private ButtonPressedEnum m_buttonPressed;

		private DrillHoleFile m_newDrillHole;

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

		public DrillHoleFile NewDrillHoleFile
		{
			get
			{
				return m_newDrillHole;
			}
			set
			{
				m_newDrillHole = value;
			}
		}
	}

	private IContainer components;

	private GroupBox groupBox1;

	private TextBox textBox_Depth;

	private Label label6;

	private GroupBox groupBox_target;

	private TextBox textBox_Target_Easting;

	private TextBox textBox_Target_Northing;

	private TextBox textBox_Target_TVD;

	private Label label_targetEasting;

	private Label label_targetNorthing;

	private Label label_targetTVD;

	private GroupBox groupBox_details;

	private TextBox textBox_rigID;

	private TextBox textBox_holeID;

	private Label label_RigID;

	private Label label_HoleID;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Cancel;

	private TabControl tabControl1;

	private TabPage tabPage_SideTrack;

	private TabPage tabPage_Chart;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private Label label_DepthState;

	private Label label_TargetEastingState;

	private Label label_TargetNorthingState;

	private Label label_TargetTVDState;

	private TextBox textBox_Client;

	private Label label_Client;

	private TextBox textBox_Area;

	private TextBox textBox_Location;

	private Label label_Area;

	private Label label_Location;

	private TextBox textBox_SurveyingContractor;

	private TextBox textBox_DrillingContractor;

	private Label label2;

	private Label label_DrillingContractor;

	private GroupBox groupBox_build;

	private Label label_BuildResult1;

	private Label label_targetStateLabel;

	private Label label_targetState;

	private Panel panel1;

	private Label label_TVDValue;

	private Label label_TVDLabel;

	private Label label1;

	private Label label3;

	private Panel panel_ExtremeDL;

	private Panel panel_SevereDL;

	private DrillHoleFile m_drillHoleFile;

	private EzyNavHoleChart m_chart;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	private DepthStateEnum m_depthState;

	private Target_TVDStateEnum m_targetTVDState;

	private Target_EastingStateEnum m_targetEastingState;

	private Target_NorthingStateEnum m_targetNorthingState;

	private MinDLStateEnum m_minDLState;

	private bool m_minDLPathBelowTarget;

	private Target_StateEnum m_targetState;

	private AbsoluteSurvey m_sideTrackStart;

	private Target m_target;

	private DrillHoleFile m_newDrillHoleFile;

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
		this.groupBox_target = new System.Windows.Forms.GroupBox();
		this.label_targetState = new System.Windows.Forms.Label();
		this.label_targetStateLabel = new System.Windows.Forms.Label();
		this.label_TargetEastingState = new System.Windows.Forms.Label();
		this.label_TargetNorthingState = new System.Windows.Forms.Label();
		this.label_TargetTVDState = new System.Windows.Forms.Label();
		this.textBox_Target_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Target_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Target_TVD = new System.Windows.Forms.TextBox();
		this.label_targetEasting = new System.Windows.Forms.Label();
		this.label_targetNorthing = new System.Windows.Forms.Label();
		this.label_targetTVD = new System.Windows.Forms.Label();
		this.groupBox_details = new System.Windows.Forms.GroupBox();
		this.textBox_SurveyingContractor = new System.Windows.Forms.TextBox();
		this.textBox_DrillingContractor = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label_DrillingContractor = new System.Windows.Forms.Label();
		this.textBox_Client = new System.Windows.Forms.TextBox();
		this.label_Client = new System.Windows.Forms.Label();
		this.textBox_Area = new System.Windows.Forms.TextBox();
		this.textBox_Location = new System.Windows.Forms.TextBox();
		this.label_Area = new System.Windows.Forms.Label();
		this.label_Location = new System.Windows.Forms.Label();
		this.textBox_rigID = new System.Windows.Forms.TextBox();
		this.textBox_holeID = new System.Windows.Forms.TextBox();
		this.label_RigID = new System.Windows.Forms.Label();
		this.label_HoleID = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label_TVDValue = new System.Windows.Forms.Label();
		this.label_DepthState = new System.Windows.Forms.Label();
		this.label_TVDLabel = new System.Windows.Forms.Label();
		this.textBox_Depth = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage_SideTrack = new System.Windows.Forms.TabPage();
		this.groupBox_build = new System.Windows.Forms.GroupBox();
		this.label_BuildResult1 = new System.Windows.Forms.Label();
		this.tabPage_Chart = new System.Windows.Forms.TabPage();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.panel_ExtremeDL = new System.Windows.Forms.Panel();
		this.panel_SevereDL = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_target.SuspendLayout();
		this.groupBox_details.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage_SideTrack.SuspendLayout();
		this.groupBox_build.SuspendLayout();
		this.tabPage_Chart.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(224, 24);
		base.label_headerText1.Text = "Sidetrack the drill hole:";
		base.groupBox_frame.Size = new System.Drawing.Size(420, 561);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Location = new System.Drawing.Point(149, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(285, 59);
		base.panel_buttonPanel.TabIndex = 4;
		base.panel_body.Location = new System.Drawing.Point(0, 53);
		base.panel_body.Size = new System.Drawing.Size(434, 575);
		base.panel_header.Controls.Add(this.label1);
		base.panel_header.Controls.Add(this.label3);
		base.panel_header.Controls.Add(this.panel_ExtremeDL);
		base.panel_header.Controls.Add(this.panel_SevereDL);
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Size = new System.Drawing.Size(434, 53);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_SevereDL, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_ExtremeDL, 0);
		base.panel_header.Controls.SetChildIndex(this.label3, 0);
		base.panel_header.Controls.SetChildIndex(this.label1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 628);
		base.panel_footer.Size = new System.Drawing.Size(434, 59);
		base.label_headerText2.Size = new System.Drawing.Size(0, 20);
		base.label_headerText2.Text = "";
		base.panel_scrollPanel.Controls.Add(this.tabControl1);
		base.panel_scrollPanel.Size = new System.Drawing.Size(414, 542);
		this.groupBox_target.Controls.Add(this.label_targetState);
		this.groupBox_target.Controls.Add(this.label_targetStateLabel);
		this.groupBox_target.Controls.Add(this.label_TargetEastingState);
		this.groupBox_target.Controls.Add(this.label_TargetNorthingState);
		this.groupBox_target.Controls.Add(this.label_TargetTVDState);
		this.groupBox_target.Controls.Add(this.textBox_Target_Easting);
		this.groupBox_target.Controls.Add(this.textBox_Target_Northing);
		this.groupBox_target.Controls.Add(this.textBox_Target_TVD);
		this.groupBox_target.Controls.Add(this.label_targetEasting);
		this.groupBox_target.Controls.Add(this.label_targetNorthing);
		this.groupBox_target.Controls.Add(this.label_targetTVD);
		this.groupBox_target.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_target.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_target.Location = new System.Drawing.Point(3, 106);
		this.groupBox_target.Name = "groupBox_target";
		this.groupBox_target.Size = new System.Drawing.Size(383, 152);
		this.groupBox_target.TabIndex = 2;
		this.groupBox_target.TabStop = false;
		this.groupBox_target.Text = "Target coordinates (m):";
		this.label_targetState.AutoSize = true;
		this.label_targetState.Location = new System.Drawing.Point(118, 125);
		this.label_targetState.Name = "label_targetState";
		this.label_targetState.Size = new System.Drawing.Size(40, 24);
		this.label_targetState.TabIndex = 14;
		this.label_targetState.Text = "null";
		this.label_targetStateLabel.AutoSize = true;
		this.label_targetStateLabel.Location = new System.Drawing.Point(1, 125);
		this.label_targetStateLabel.Name = "label_targetStateLabel";
		this.label_targetStateLabel.Size = new System.Drawing.Size(121, 24);
		this.label_targetStateLabel.TabIndex = 13;
		this.label_targetStateLabel.Text = "Target status:";
		this.label_TargetEastingState.AutoSize = true;
		this.label_TargetEastingState.ForeColor = System.Drawing.Color.DarkRed;
		this.label_TargetEastingState.Location = new System.Drawing.Point(240, 91);
		this.label_TargetEastingState.Name = "label_TargetEastingState";
		this.label_TargetEastingState.Size = new System.Drawing.Size(114, 24);
		this.label_TargetEastingState.TabIndex = 12;
		this.label_TargetEastingState.Text = "(set in code)";
		this.label_TargetEastingState.Visible = false;
		this.label_TargetNorthingState.AutoSize = true;
		this.label_TargetNorthingState.ForeColor = System.Drawing.Color.DarkRed;
		this.label_TargetNorthingState.Location = new System.Drawing.Point(240, 59);
		this.label_TargetNorthingState.Name = "label_TargetNorthingState";
		this.label_TargetNorthingState.Size = new System.Drawing.Size(114, 24);
		this.label_TargetNorthingState.TabIndex = 12;
		this.label_TargetNorthingState.Text = "(set in code)";
		this.label_TargetNorthingState.Visible = false;
		this.label_TargetTVDState.AutoSize = true;
		this.label_TargetTVDState.ForeColor = System.Drawing.Color.DarkRed;
		this.label_TargetTVDState.Location = new System.Drawing.Point(240, 27);
		this.label_TargetTVDState.Name = "label_TargetTVDState";
		this.label_TargetTVDState.Size = new System.Drawing.Size(114, 24);
		this.label_TargetTVDState.TabIndex = 12;
		this.label_TargetTVDState.Text = "(set in code)";
		this.label_TargetTVDState.Visible = false;
		this.textBox_Target_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_Easting.Location = new System.Drawing.Point(94, 92);
		this.textBox_Target_Easting.MaxLength = 15;
		this.textBox_Target_Easting.Name = "textBox_Target_Easting";
		this.textBox_Target_Easting.Size = new System.Drawing.Size(144, 26);
		this.textBox_Target_Easting.TabIndex = 2;
		this.textBox_Target_Easting.Enter += new System.EventHandler(this.textBox_targetEasting_Enter);
		this.textBox_Target_Easting.DoubleClick += new System.EventHandler(this.textBox_Target_Easting_DoubleClick);
		this.textBox_Target_Easting.Leave += new System.EventHandler(this.textBox_targetEasting_Leave);
		this.textBox_Target_Easting.TextChanged += new System.EventHandler(this.textBox_Target_Easting_TextChanged);
		this.textBox_Target_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Target_Northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_Northing.Location = new System.Drawing.Point(94, 60);
		this.textBox_Target_Northing.MaxLength = 15;
		this.textBox_Target_Northing.Name = "textBox_Target_Northing";
		this.textBox_Target_Northing.Size = new System.Drawing.Size(144, 26);
		this.textBox_Target_Northing.TabIndex = 1;
		this.textBox_Target_Northing.Enter += new System.EventHandler(this.textBox_targetNorthing_Enter);
		this.textBox_Target_Northing.DoubleClick += new System.EventHandler(this.textBox_Target_Northing_DoubleClick);
		this.textBox_Target_Northing.Leave += new System.EventHandler(this.textBox_targetNorthing_Leave);
		this.textBox_Target_Northing.TextChanged += new System.EventHandler(this.textBox_Target_Northing_TextChanged);
		this.textBox_Target_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_Target_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Target_TVD.Location = new System.Drawing.Point(94, 28);
		this.textBox_Target_TVD.MaxLength = 15;
		this.textBox_Target_TVD.Name = "textBox_Target_TVD";
		this.textBox_Target_TVD.Size = new System.Drawing.Size(144, 26);
		this.textBox_Target_TVD.TabIndex = 0;
		this.textBox_Target_TVD.Enter += new System.EventHandler(this.textBox_targetTVD_Enter);
		this.textBox_Target_TVD.DoubleClick += new System.EventHandler(this.textBox_Target_TVD_DoubleClick);
		this.textBox_Target_TVD.Leave += new System.EventHandler(this.textBox_targetTVD_Leave);
		this.textBox_Target_TVD.TextChanged += new System.EventHandler(this.textBox_Target_TVD_TextChanged);
		this.label_targetEasting.AutoSize = true;
		this.label_targetEasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetEasting.Location = new System.Drawing.Point(1, 91);
		this.label_targetEasting.Name = "label_targetEasting";
		this.label_targetEasting.Size = new System.Drawing.Size(75, 20);
		this.label_targetEasting.TabIndex = 0;
		this.label_targetEasting.Text = "Easting:";
		this.label_targetNorthing.AutoSize = true;
		this.label_targetNorthing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetNorthing.Location = new System.Drawing.Point(1, 60);
		this.label_targetNorthing.Name = "label_targetNorthing";
		this.label_targetNorthing.Size = new System.Drawing.Size(82, 20);
		this.label_targetNorthing.TabIndex = 0;
		this.label_targetNorthing.Text = "Northing:";
		this.label_targetTVD.AutoSize = true;
		this.label_targetTVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_targetTVD.Location = new System.Drawing.Point(1, 30);
		this.label_targetTVD.Name = "label_targetTVD";
		this.label_targetTVD.Size = new System.Drawing.Size(49, 20);
		this.label_targetTVD.TabIndex = 0;
		this.label_targetTVD.Text = "TVD:";
		this.groupBox_details.Controls.Add(this.textBox_SurveyingContractor);
		this.groupBox_details.Controls.Add(this.textBox_DrillingContractor);
		this.groupBox_details.Controls.Add(this.label2);
		this.groupBox_details.Controls.Add(this.label_DrillingContractor);
		this.groupBox_details.Controls.Add(this.textBox_Client);
		this.groupBox_details.Controls.Add(this.label_Client);
		this.groupBox_details.Controls.Add(this.textBox_Area);
		this.groupBox_details.Controls.Add(this.textBox_Location);
		this.groupBox_details.Controls.Add(this.label_Area);
		this.groupBox_details.Controls.Add(this.label_Location);
		this.groupBox_details.Controls.Add(this.textBox_rigID);
		this.groupBox_details.Controls.Add(this.textBox_holeID);
		this.groupBox_details.Controls.Add(this.label_RigID);
		this.groupBox_details.Controls.Add(this.label_HoleID);
		this.groupBox_details.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_details.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_details.Location = new System.Drawing.Point(3, 315);
		this.groupBox_details.Name = "groupBox_details";
		this.groupBox_details.Size = new System.Drawing.Size(383, 252);
		this.groupBox_details.TabIndex = 3;
		this.groupBox_details.TabStop = false;
		this.groupBox_details.Text = "Hole details:";
		this.textBox_SurveyingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_SurveyingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_SurveyingContractor.Location = new System.Drawing.Point(94, 189);
		this.textBox_SurveyingContractor.MaxLength = 30;
		this.textBox_SurveyingContractor.Name = "textBox_SurveyingContractor";
		this.textBox_SurveyingContractor.Size = new System.Drawing.Size(283, 26);
		this.textBox_SurveyingContractor.TabIndex = 5;
		this.textBox_SurveyingContractor.Enter += new System.EventHandler(this.textBox_SurveyingContractor_Enter);
		this.textBox_SurveyingContractor.DoubleClick += new System.EventHandler(this.textBox_SurveyingContractor_DoubleClick);
		this.textBox_SurveyingContractor.Leave += new System.EventHandler(this.textBox_SurveyingContractor_Leave);
		this.textBox_DrillingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_DrillingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_DrillingContractor.Location = new System.Drawing.Point(94, 157);
		this.textBox_DrillingContractor.MaxLength = 30;
		this.textBox_DrillingContractor.Name = "textBox_DrillingContractor";
		this.textBox_DrillingContractor.Size = new System.Drawing.Size(283, 26);
		this.textBox_DrillingContractor.TabIndex = 4;
		this.textBox_DrillingContractor.Enter += new System.EventHandler(this.textBox_DrillingContractor_Enter);
		this.textBox_DrillingContractor.DoubleClick += new System.EventHandler(this.textBox_DrillingContractor_DoubleClick);
		this.textBox_DrillingContractor.Leave += new System.EventHandler(this.textBox_DrillingContractor_Leave);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(0, 189);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 20);
		this.label2.TabIndex = 21;
		this.label2.Text = "Surveyour:";
		this.label_DrillingContractor.AutoSize = true;
		this.label_DrillingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DrillingContractor.Location = new System.Drawing.Point(0, 157);
		this.label_DrillingContractor.Name = "label_DrillingContractor";
		this.label_DrillingContractor.Size = new System.Drawing.Size(61, 20);
		this.label_DrillingContractor.TabIndex = 20;
		this.label_DrillingContractor.Text = "Driller:";
		this.textBox_Client.BackColor = System.Drawing.Color.White;
		this.textBox_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Client.Location = new System.Drawing.Point(94, 221);
		this.textBox_Client.MaxLength = 30;
		this.textBox_Client.Name = "textBox_Client";
		this.textBox_Client.Size = new System.Drawing.Size(283, 26);
		this.textBox_Client.TabIndex = 6;
		this.textBox_Client.Enter += new System.EventHandler(this.textBox_Client_Enter);
		this.textBox_Client.DoubleClick += new System.EventHandler(this.textBox_Client_DoubleClick);
		this.textBox_Client.Leave += new System.EventHandler(this.textBox_Client_Leave);
		this.label_Client.AutoSize = true;
		this.label_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Client.Location = new System.Drawing.Point(1, 221);
		this.label_Client.Name = "label_Client";
		this.label_Client.Size = new System.Drawing.Size(60, 20);
		this.label_Client.TabIndex = 17;
		this.label_Client.Text = "Client:";
		this.textBox_Area.BackColor = System.Drawing.Color.White;
		this.textBox_Area.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Area.Location = new System.Drawing.Point(94, 124);
		this.textBox_Area.MaxLength = 30;
		this.textBox_Area.Name = "textBox_Area";
		this.textBox_Area.Size = new System.Drawing.Size(283, 26);
		this.textBox_Area.TabIndex = 3;
		this.textBox_Area.Enter += new System.EventHandler(this.textBox_Area_Enter);
		this.textBox_Area.DoubleClick += new System.EventHandler(this.textBox_Area_DoubleClick);
		this.textBox_Area.Leave += new System.EventHandler(this.textBox_Area_Leave);
		this.textBox_Location.BackColor = System.Drawing.Color.White;
		this.textBox_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Location.Location = new System.Drawing.Point(94, 92);
		this.textBox_Location.MaxLength = 30;
		this.textBox_Location.Name = "textBox_Location";
		this.textBox_Location.Size = new System.Drawing.Size(283, 26);
		this.textBox_Location.TabIndex = 2;
		this.textBox_Location.Enter += new System.EventHandler(this.textBox_Location_Enter);
		this.textBox_Location.DoubleClick += new System.EventHandler(this.textBox_Location_DoubleClick);
		this.textBox_Location.Leave += new System.EventHandler(this.textBox_Location_Leave);
		this.label_Area.AutoSize = true;
		this.label_Area.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Area.Location = new System.Drawing.Point(0, 124);
		this.label_Area.Name = "label_Area";
		this.label_Area.Size = new System.Drawing.Size(52, 20);
		this.label_Area.TabIndex = 11;
		this.label_Area.Text = "Area:";
		this.label_Location.AutoSize = true;
		this.label_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Location.Location = new System.Drawing.Point(1, 92);
		this.label_Location.Name = "label_Location";
		this.label_Location.Size = new System.Drawing.Size(83, 20);
		this.label_Location.TabIndex = 10;
		this.label_Location.Text = "Location:";
		this.textBox_rigID.BackColor = System.Drawing.Color.White;
		this.textBox_rigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_rigID.Location = new System.Drawing.Point(94, 60);
		this.textBox_rigID.MaxLength = 15;
		this.textBox_rigID.Name = "textBox_rigID";
		this.textBox_rigID.Size = new System.Drawing.Size(283, 26);
		this.textBox_rigID.TabIndex = 1;
		this.textBox_rigID.Enter += new System.EventHandler(this.textBox_rigID_Enter);
		this.textBox_rigID.DoubleClick += new System.EventHandler(this.textBox_rigID_DoubleClick);
		this.textBox_rigID.Leave += new System.EventHandler(this.textBox_rigID_Leave);
		this.textBox_holeID.BackColor = System.Drawing.Color.White;
		this.textBox_holeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_holeID.Location = new System.Drawing.Point(94, 28);
		this.textBox_holeID.MaxLength = 15;
		this.textBox_holeID.Name = "textBox_holeID";
		this.textBox_holeID.Size = new System.Drawing.Size(283, 26);
		this.textBox_holeID.TabIndex = 0;
		this.textBox_holeID.Enter += new System.EventHandler(this.textBox_holeID_Enter);
		this.textBox_holeID.DoubleClick += new System.EventHandler(this.textBox_holeID_DoubleClick);
		this.textBox_holeID.Leave += new System.EventHandler(this.textBox_holeID_Leave);
		this.label_RigID.AutoSize = true;
		this.label_RigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_RigID.Location = new System.Drawing.Point(0, 60);
		this.label_RigID.Name = "label_RigID";
		this.label_RigID.Size = new System.Drawing.Size(60, 20);
		this.label_RigID.TabIndex = 0;
		this.label_RigID.Text = "RigID:";
		this.label_HoleID.AutoSize = true;
		this.label_HoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleID.Location = new System.Drawing.Point(0, 28);
		this.label_HoleID.Name = "label_HoleID";
		this.label_HoleID.Size = new System.Drawing.Size(70, 20);
		this.label_HoleID.TabIndex = 0;
		this.label_HoleID.Text = "HoleID:";
		this.groupBox1.Controls.Add(this.label_TVDValue);
		this.groupBox1.Controls.Add(this.label_DepthState);
		this.groupBox1.Controls.Add(this.label_TVDLabel);
		this.groupBox1.Controls.Add(this.textBox_Depth);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(383, 103);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Sidetrack depth (m):";
		this.label_TVDValue.AutoSize = true;
		this.label_TVDValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TVDValue.Location = new System.Drawing.Point(136, 78);
		this.label_TVDValue.Name = "label_TVDValue";
		this.label_TVDValue.Size = new System.Drawing.Size(33, 20);
		this.label_TVDValue.TabIndex = 14;
		this.label_TVDValue.Text = "null";
		this.label_DepthState.AutoSize = true;
		this.label_DepthState.ForeColor = System.Drawing.Color.DarkRed;
		this.label_DepthState.Location = new System.Drawing.Point(184, 45);
		this.label_DepthState.Name = "label_DepthState";
		this.label_DepthState.Size = new System.Drawing.Size(157, 24);
		this.label_DepthState.TabIndex = 12;
		this.label_DepthState.Text = "state (set in code)";
		this.label_DepthState.Visible = false;
		this.label_TVDLabel.AutoSize = true;
		this.label_TVDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TVDLabel.Location = new System.Drawing.Point(6, 78);
		this.label_TVDLabel.Name = "label_TVDLabel";
		this.label_TVDLabel.Size = new System.Drawing.Size(124, 20);
		this.label_TVDLabel.TabIndex = 13;
		this.label_TVDLabel.Text = "Calculated TVD:";
		this.textBox_Depth.BackColor = System.Drawing.Color.White;
		this.textBox_Depth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Depth.Location = new System.Drawing.Point(10, 49);
		this.textBox_Depth.MaxLength = 15;
		this.textBox_Depth.Name = "textBox_Depth";
		this.textBox_Depth.Size = new System.Drawing.Size(168, 26);
		this.textBox_Depth.TabIndex = 0;
		this.textBox_Depth.Enter += new System.EventHandler(this.textBox_Depth_Enter);
		this.textBox_Depth.DoubleClick += new System.EventHandler(this.textBox_Depth_DoubleClick);
		this.textBox_Depth.Leave += new System.EventHandler(this.textBox_Depth_Leave);
		this.textBox_Depth.TextChanged += new System.EventHandler(this.textBox_Depth_TextChanged);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(6, 25);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(267, 20);
		this.label6.TabIndex = 0;
		this.label6.Text = "MD at which to create sidetrack:";
		this.button_Next.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Next.AutoEllipsis = true;
		this.button_Next.BackColor = System.Drawing.Color.Transparent;
		this.button_Next.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Next.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorOn = System.Drawing.Color.Orange;
		this.button_Next.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
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
		this.button_Next.ImageOffset = 0;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(4, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(134, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 0;
		this.button_Next.Text = "Next";
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.button_Cancel.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Cancel.AutoEllipsis = true;
		this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
		this.button_Cancel.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Cancel.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorOn = System.Drawing.Color.Orange;
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
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
		this.button_Cancel.ImageOffset = 0;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(144, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(134, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.tabControl1.Controls.Add(this.tabPage_SideTrack);
		this.tabControl1.Controls.Add(this.tabPage_Chart);
		this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(414, 542);
		this.tabControl1.TabIndex = 4;
		this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
		this.tabPage_SideTrack.AutoScroll = true;
		this.tabPage_SideTrack.Controls.Add(this.groupBox_details);
		this.tabPage_SideTrack.Controls.Add(this.groupBox_build);
		this.tabPage_SideTrack.Controls.Add(this.groupBox_target);
		this.tabPage_SideTrack.Controls.Add(this.groupBox1);
		this.tabPage_SideTrack.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage_SideTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabPage_SideTrack.Location = new System.Drawing.Point(4, 33);
		this.tabPage_SideTrack.Name = "tabPage_SideTrack";
		this.tabPage_SideTrack.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_SideTrack.Size = new System.Drawing.Size(406, 505);
		this.tabPage_SideTrack.TabIndex = 0;
		this.tabPage_SideTrack.Text = "Sidetrack";
		this.tabPage_SideTrack.UseVisualStyleBackColor = true;
		this.groupBox_build.Controls.Add(this.label_BuildResult1);
		this.groupBox_build.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_build.Location = new System.Drawing.Point(3, 258);
		this.groupBox_build.Name = "groupBox_build";
		this.groupBox_build.Size = new System.Drawing.Size(383, 57);
		this.groupBox_build.TabIndex = 4;
		this.groupBox_build.TabStop = false;
		this.groupBox_build.Text = "Minimum dogleg to target (degrees/30m):";
		this.label_BuildResult1.AutoSize = true;
		this.label_BuildResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_BuildResult1.Location = new System.Drawing.Point(3, 25);
		this.label_BuildResult1.Name = "label_BuildResult1";
		this.label_BuildResult1.Size = new System.Drawing.Size(46, 25);
		this.label_BuildResult1.TabIndex = 0;
		this.label_BuildResult1.Text = "null";
		this.label_BuildResult1.Visible = false;
		this.tabPage_Chart.AutoScroll = true;
		this.tabPage_Chart.AutoScrollMinSize = new System.Drawing.Size(0, 450);
		this.tabPage_Chart.Controls.Add(this.ezyNavHoleChartController1);
		this.tabPage_Chart.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabPage_Chart.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Chart.Name = "tabPage_Chart";
		this.tabPage_Chart.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Chart.Size = new System.Drawing.Size(526, 454);
		this.tabPage_Chart.TabIndex = 1;
		this.tabPage_Chart.Text = "Chart Options";
		this.tabPage_Chart.UseVisualStyleBackColor = true;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Cursor = System.Windows.Forms.Cursors.Default;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(3, 3);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(520, 448);
		this.ezyNavHoleChartController1.TabIndex = 2;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(370, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 53);
		this.panel1.TabIndex = 11;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(9, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(132, 20);
		this.label1.TabIndex = 13;
		this.label1.Text = "Severe Dogleg:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(181, 25);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(142, 20);
		this.label3.TabIndex = 12;
		this.label3.Text = "Extreme Dogleg:";
		this.panel_ExtremeDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_ExtremeDL.Location = new System.Drawing.Point(328, 23);
		this.panel_ExtremeDL.Name = "panel_ExtremeDL";
		this.panel_ExtremeDL.Size = new System.Drawing.Size(36, 27);
		this.panel_ExtremeDL.TabIndex = 15;
		this.panel_SevereDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_SevereDL.Location = new System.Drawing.Point(141, 23);
		this.panel_SevereDL.Name = "panel_SevereDL";
		this.panel_SevereDL.Size = new System.Drawing.Size(34, 27);
		this.panel_SevereDL.TabIndex = 14;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "Body_SideTrackHole";
		base.Size = new System.Drawing.Size(434, 687);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_target.ResumeLayout(false);
		this.groupBox_target.PerformLayout();
		this.groupBox_details.ResumeLayout(false);
		this.groupBox_details.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage_SideTrack.ResumeLayout(false);
		this.groupBox_build.ResumeLayout(false);
		this.groupBox_build.PerformLayout();
		this.tabPage_Chart.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_SideTrackHole(DrillHoleFile drillHoleFile, EzyNavHoleChart ezyNavChart, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		Visible = false;
		m_drillHoleFile = drillHoleFile;
		m_chart = ezyNavChart;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		SetDLSeverityKey();
		m_target = new Target();
	}

	public void Display(bool refreshData)
	{
		Visible = true;
		tabControl1.SelectedIndex = 0;
		Focus();
		textBox_Depth.Focus();
		if (refreshData)
		{
			m_newDrillHoleFile = new DrillHoleFile();
			CopyDrillHoleFields(m_drillHoleFile, m_newDrillHoleFile);
			Utility.ClearTextBoxes(this);
			label_TVDValue.Text = "";
			SetDepthState(DepthStateEnum.NoneEntered);
			Set_Target_TVDState(Target_TVDStateEnum.NoneEntered);
			Set_Target_NorthingState(Target_NorthingStateEnum.NoneEntered);
			Set_Target_EastingState(Target_EastingStateEnum.NoneEntered);
			Set_TargetState(Target_StateEnum.Incomplete);
			SetMinDLState(MinDLStateEnum.NotCalculated, "Not calculated!", pathBelowTargetTVD: false);
			m_chart.ResetStraightPath();
			m_chart.ResetPlan();
			m_chart.ResetSideTrack();
			m_chart.ResetExtrapolated();
			m_chart.ResetDistanceFromTarget();
			DisplayFields(m_drillHoleFile);
		}
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
	}

	private void CopyDrillHoleFields(DrillHoleFile originalDrillHoleFile, DrillHoleFile newDrillHoleFile)
	{
		newDrillHoleFile.Collar = originalDrillHoleFile.Collar;
		newDrillHoleFile.Location = originalDrillHoleFile.Location;
		newDrillHoleFile.Area = originalDrillHoleFile.Area;
		newDrillHoleFile.DrillingContractor = originalDrillHoleFile.DrillingContractor;
		newDrillHoleFile.SurveyingContractor = originalDrillHoleFile.SurveyingContractor;
		newDrillHoleFile.Client = originalDrillHoleFile.Client;
	}

	private void DisplayFields(DrillHoleFile drillHoleFile)
	{
		textBox_holeID.Text = drillHoleFile.HoleID;
		textBox_rigID.Text = drillHoleFile.RigID;
		textBox_Location.Text = drillHoleFile.Location;
		textBox_Area.Text = drillHoleFile.Area;
		textBox_DrillingContractor.Text = drillHoleFile.DrillingContractor;
		textBox_SurveyingContractor.Text = drillHoleFile.SurveyingContractor;
		textBox_Client.Text = drillHoleFile.Client;
		textBox_Target_Easting.Text = drillHoleFile.Target.Easting.ToString();
		textBox_Target_Northing.Text = drillHoleFile.Target.Northing.ToString();
		textBox_Target_TVD.Text = drillHoleFile.Target.TVD.ToString();
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
	}

	private void SetDLSeverityKey()
	{
		panel_SevereDL.BackColor = Utility.m_SevereColor;
		panel_ExtremeDL.BackColor = Utility.m_ExtremeColor;
	}

	private void textBox_Depth_TextChanged(object sender, EventArgs e)
	{
		if (textBox_Depth.Text != "")
		{
			if (StringToDecimal(textBox_Depth.Text, out var value))
			{
				if (GetSidetrackSurvey(value, out var survey))
				{
					m_sideTrackStart = survey;
					m_chart.SetSideTrack(survey, visible: true);
				}
				else
				{
					m_chart.ResetSideTrack();
				}
			}
			else
			{
				SetDepthState(DepthStateEnum.Invalid);
				m_chart.ResetSideTrack();
			}
		}
		else
		{
			SetDepthState(DepthStateEnum.NoneEntered);
			m_chart.ResetSideTrack();
		}
		UpdatePlan();
	}

	private bool GetSidetrackSurvey(decimal depth, out AbsoluteSurvey survey)
	{
		NavMath navMath = new NavMath();
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		NavMath.SideTrackHoleEnum sideTrackHoleEnum = navMath.SideTrackHole(absoluteSurveyList, depth, out survey);
		if ((sideTrackHoleEnum == NavMath.SideTrackHoleEnum.Extrapolated) | (sideTrackHoleEnum == NavMath.SideTrackHoleEnum.Found))
		{
			if (sideTrackHoleEnum == NavMath.SideTrackHoleEnum.Extrapolated)
			{
				SetDepthState(DepthStateEnum.Extrapolated);
			}
			else
			{
				SetDepthState(DepthStateEnum.Found);
			}
			label_TVDValue.Text = survey.TVD + "m.";
			return true;
		}
		bool flag = false;
		switch (sideTrackHoleEnum)
		{
		case NavMath.SideTrackHoleEnum.DepthNotGreaterThanZero:
			SetDepthState(DepthStateEnum.Invalid);
			break;
		case NavMath.SideTrackHoleEnum.DepthOutsideSurveyRange:
			SetDepthState(DepthStateEnum.OutOfRange);
			break;
		case NavMath.SideTrackHoleEnum.InternalError:
			flag = true;
			break;
		case NavMath.SideTrackHoleEnum.NullParameter:
			flag = true;
			break;
		case NavMath.SideTrackHoleEnum.UnknownExtrapolateBetweenError:
			flag = true;
			break;
		case NavMath.SideTrackHoleEnum.UnknownExtrapolateBetweenException:
			flag = true;
			break;
		default:
			flag = true;
			break;
		}
		if (flag)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("System error!", NavMath.ToHumanReadable(sideTrackHoleEnum) + ".");
			popup_MessageBox_OK.ShowDialog();
		}
		return false;
	}

	private void SetDepthState(DepthStateEnum state)
	{
		m_depthState = state;
		switch (state)
		{
		case DepthStateEnum.Extrapolated:
			label_DepthState.ForeColor = Color.Green;
			label_DepthState.Text = "(Extrapolated)";
			label_DepthState.Visible = true;
			break;
		case DepthStateEnum.Found:
			label_DepthState.ForeColor = Color.Green;
			label_DepthState.Text = "(Found)";
			label_DepthState.Visible = true;
			break;
		case DepthStateEnum.OutOfRange:
			label_DepthState.ForeColor = Color.Red;
			label_DepthState.Text = "(Out Of Range)";
			label_DepthState.Visible = true;
			break;
		case DepthStateEnum.Invalid:
			label_DepthState.ForeColor = Color.Red;
			label_DepthState.Text = "(Invalid)";
			label_DepthState.Visible = true;
			break;
		case DepthStateEnum.NoneEntered:
			label_DepthState.Visible = false;
			break;
		}
	}

	private void textBox_Target_TVD_TextChanged(object sender, EventArgs e)
	{
		if (textBox_Target_TVD.Text != "")
		{
			if (StringToDecimal(textBox_Target_TVD.Text, out var value))
			{
				SetTargetTVD(value);
			}
			else
			{
				Set_Target_TVDState(Target_TVDStateEnum.Invalid);
			}
		}
		else
		{
			Set_Target_TVDState(Target_TVDStateEnum.NoneEntered);
		}
		SetDrillHoleFileTarget(m_target);
		UpdatePlan();
	}

	private void SetTargetTVD(decimal value)
	{
		switch (m_target.SetTVD(value))
		{
		case Target.SetTVDEnum.OutOfRange:
			Set_Target_TVDState(Target_TVDStateEnum.OutOfRange);
			break;
		case Target.SetTVDEnum.Set:
			Set_Target_TVDState(Target_TVDStateEnum.Accepted);
			break;
		}
	}

	private void Set_Target_TVDState(Target_TVDStateEnum state)
	{
		m_targetTVDState = state;
		switch (state)
		{
		case Target_TVDStateEnum.Accepted:
			label_TargetTVDState.ForeColor = Color.Green;
			label_TargetTVDState.Text = "(Accepted)";
			label_TargetTVDState.Visible = true;
			break;
		case Target_TVDStateEnum.OutOfRange:
			label_TargetTVDState.ForeColor = Color.Red;
			label_TargetTVDState.Text = "(Out Of Range)";
			label_TargetTVDState.Visible = true;
			break;
		case Target_TVDStateEnum.Invalid:
			label_TargetTVDState.ForeColor = Color.Red;
			label_TargetTVDState.Text = "(Invalid)";
			label_TargetTVDState.Visible = true;
			break;
		case Target_TVDStateEnum.NoneEntered:
			label_TargetTVDState.Visible = false;
			break;
		}
	}

	private void textBox_Target_Northing_TextChanged(object sender, EventArgs e)
	{
		if (textBox_Target_Northing.Text != "")
		{
			if (StringToDecimal(textBox_Target_Northing.Text, out var value))
			{
				SetTargetNorthing(value);
			}
			else
			{
				Set_Target_NorthingState(Target_NorthingStateEnum.Invalid);
			}
		}
		else
		{
			Set_Target_NorthingState(Target_NorthingStateEnum.NoneEntered);
		}
		SetDrillHoleFileTarget(m_target);
		UpdatePlan();
	}

	private void SetTargetNorthing(decimal value)
	{
		switch (m_target.SetNorthing(value))
		{
		case Target.SetNorthingEnum.OutOfRange:
			Set_Target_NorthingState(Target_NorthingStateEnum.OutOfRange);
			break;
		case Target.SetNorthingEnum.Set:
			Set_Target_NorthingState(Target_NorthingStateEnum.Accepted);
			break;
		}
	}

	private void Set_Target_NorthingState(Target_NorthingStateEnum state)
	{
		m_targetNorthingState = state;
		switch (state)
		{
		case Target_NorthingStateEnum.Accepted:
			label_TargetNorthingState.ForeColor = Color.Green;
			label_TargetNorthingState.Text = "(Accepted)";
			label_TargetNorthingState.Visible = true;
			break;
		case Target_NorthingStateEnum.OutOfRange:
			label_TargetNorthingState.ForeColor = Color.Red;
			label_TargetNorthingState.Text = "(Out Of Range)";
			label_TargetNorthingState.Visible = true;
			break;
		case Target_NorthingStateEnum.Invalid:
			label_TargetNorthingState.ForeColor = Color.Red;
			label_TargetNorthingState.Text = "(Invalid)";
			label_TargetNorthingState.Visible = true;
			break;
		case Target_NorthingStateEnum.NoneEntered:
			label_TargetNorthingState.Visible = false;
			break;
		}
	}

	private void textBox_Target_Easting_TextChanged(object sender, EventArgs e)
	{
		if (textBox_Target_Easting.Text != "")
		{
			if (StringToDecimal(textBox_Target_Easting.Text, out var value))
			{
				SetTargetEasting(value);
			}
			else
			{
				Set_Target_EastingState(Target_EastingStateEnum.Invalid);
			}
		}
		else
		{
			Set_Target_EastingState(Target_EastingStateEnum.NoneEntered);
		}
		SetDrillHoleFileTarget(m_target);
		UpdatePlan();
	}

	private void SetTargetEasting(decimal value)
	{
		switch (m_target.SetEasting(value))
		{
		case Target.SetEastingEnum.OutOfRange:
			Set_Target_EastingState(Target_EastingStateEnum.OutOfRange);
			break;
		case Target.SetEastingEnum.Set:
			Set_Target_EastingState(Target_EastingStateEnum.Accepted);
			break;
		}
	}

	private void SetDrillHoleFileTarget(Target target)
	{
		if ((m_targetEastingState == Target_EastingStateEnum.Accepted) & (m_targetNorthingState == Target_NorthingStateEnum.Accepted) & (m_targetTVDState == Target_TVDStateEnum.Accepted))
		{
			try
			{
				m_newDrillHoleFile.Target = target;
				Set_TargetState(Target_StateEnum.Set);
				return;
			}
			catch (ArgumentException)
			{
				Set_TargetState(Target_StateEnum.Invalid);
				return;
			}
		}
		Set_TargetState(Target_StateEnum.Incomplete);
	}

	private void Set_TargetState(Target_StateEnum state)
	{
		m_targetState = state;
		switch (state)
		{
		case Target_StateEnum.Set:
			Set_TargetStateLabel("Target accepted.", Color.Green);
			break;
		case Target_StateEnum.Incomplete:
			Set_TargetStateLabel("Target fields incomplete.", Color.Orange);
			break;
		case Target_StateEnum.Invalid:
			Set_TargetStateLabel("Target too far from collar!", Color.Red);
			break;
		default:
			Set_TargetStateLabel("Unexpected internal error!", Color.Red);
			break;
		}
	}

	private void Set_TargetStateLabel(string stateText, Color stateColour)
	{
		label_targetState.Text = stateText;
		label_targetState.ForeColor = stateColour;
	}

	private void Set_Target_EastingState(Target_EastingStateEnum state)
	{
		m_targetEastingState = state;
		switch (state)
		{
		case Target_EastingStateEnum.Accepted:
			label_TargetEastingState.ForeColor = Color.Green;
			label_TargetEastingState.Text = "(Accepted)";
			label_TargetEastingState.Visible = true;
			break;
		case Target_EastingStateEnum.OutOfRange:
			label_TargetEastingState.ForeColor = Color.Red;
			label_TargetEastingState.Text = "(Out Of Range)";
			label_TargetEastingState.Visible = true;
			break;
		case Target_EastingStateEnum.Invalid:
			label_TargetEastingState.ForeColor = Color.Red;
			label_TargetEastingState.Text = "(Invalid)";
			label_TargetEastingState.Visible = true;
			break;
		case Target_EastingStateEnum.NoneEntered:
			label_TargetEastingState.Visible = false;
			break;
		}
	}

	private void UpdatePlan()
	{
		if ((m_targetState == Target_StateEnum.Set) & ((m_depthState == DepthStateEnum.Extrapolated) | (m_depthState == DepthStateEnum.Found)))
		{
			m_chart.SetTarget(m_target, visible: true);
			m_chart.SetHolePlan(m_sideTrackStart, m_target, visible: true);
			MinDLToTarget_Attempt(m_sideTrackStart, m_target);
		}
		else if (m_targetState == Target_StateEnum.Set)
		{
			m_chart.ResetPlan();
			MinDLToTarget_Reset();
			m_chart.SetTarget(m_target, visible: true);
		}
		else
		{
			m_chart.ResetTarget();
			m_chart.ResetPlan();
			MinDLToTarget_Reset();
		}
	}

	private void MinDLToTarget_Attempt(AbsoluteSurvey buildStart, Target target)
	{
		NavMath navMath = new NavMath();
		decimal dogLeg = 0m;
		BuildAndHoldResults buildAndHold = new BuildAndHoldResults();
		if (buildStart != null && target != null)
		{
			switch (navMath.MinimumDogLegToTarget(buildStart, target, ref dogLeg, out buildAndHold))
			{
			case NavMath.MinimumDogLegEnum.NoError:
			{
				decimal num = Math.Round(dogLeg, 2, MidpointRounding.AwayFromZero);
				switch (GetDoglegColourCode(dogLeg))
				{
				case DoglegColourCodes.Extreme:
					SetMinDLState(MinDLStateEnum.ExtremeDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				case DoglegColourCodes.Severe:
					SetMinDLState(MinDLStateEnum.SevereDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				case DoglegColourCodes.Normal:
					SetMinDLState(MinDLStateEnum.NormalDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				default:
					SetMinDLState(MinDLStateEnum.NormalDogleg, num.ToString(), buildAndHold.BuildBelowTarget);
					break;
				}
				m_chart.SetBuild(buildAndHold.ProjectedSurveyData, visible: true);
				return;
			}
			case NavMath.MinimumDogLegEnum.InsufficientTVD:
				SetMinDLState(MinDLStateEnum.TargetNotAchieved, "Insufficient TVD!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.TargetCannotBeAchieved:
				SetMinDLState(MinDLStateEnum.TargetNotAchieved, "Cannot achieve target!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.UnknownError:
				SetMinDLState(MinDLStateEnum.Error, "Unknown error!", pathBelowTargetTVD: false);
				break;
			case NavMath.MinimumDogLegEnum.UnknownException:
				SetMinDLState(MinDLStateEnum.Error, "Unknown exception!", pathBelowTargetTVD: false);
				break;
			default:
				SetMinDLState(MinDLStateEnum.Error, "Unknown internal error!", pathBelowTargetTVD: false);
				break;
			}
			m_chart.ResetExtrapolated();
		}
		else
		{
			MinDLToTarget_Reset();
		}
	}

	private void MinDLToTarget_Reset()
	{
		SetMinDLState(MinDLStateEnum.NotCalculated, "Not calculated.", pathBelowTargetTVD: false);
		m_chart.SetBuild(null, visible: false);
	}

	private void SetMinDLState(MinDLStateEnum state, string text1, bool pathBelowTargetTVD)
	{
		m_minDLState = state;
		m_minDLPathBelowTarget = pathBelowTargetTVD;
		if (pathBelowTargetTVD)
		{
			text1 += " (Path goes below target)";
		}
		switch (state)
		{
		case MinDLStateEnum.NotCalculated:
			SetMinDLResultLabelTextColour(Color.Orange);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.TargetNotAchieved:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.Error:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.NormalDogleg:
			SetMinDLResultLabelTextColour(Color.Black);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.SevereDogleg:
			SetMinDLResultLabelTextColour(Utility.m_SevereColor);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		case MinDLStateEnum.ExtremeDogleg:
			SetMinDLResultLabelTextColour(Utility.m_ExtremeColor);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		default:
			SetMinDLResultLabelTextColour(Color.Red);
			setMinDLResultLabelText(text1);
			SetMinDLResultLabelVisible(label1: true);
			break;
		}
	}

	private void SetMinDLResultLabelTextColour(Color color)
	{
		label_BuildResult1.ForeColor = color;
	}

	private void setMinDLResultLabelText(string text1)
	{
		label_BuildResult1.Text = text1;
	}

	private void SetMinDLResultLabelVisible(bool label1)
	{
		label_BuildResult1.Visible = label1;
	}

	private DoglegColourCodes GetDoglegColourCode(decimal dogLeg)
	{
		if (dogLeg >= m_extremeDL)
		{
			return DoglegColourCodes.Extreme;
		}
		if (dogLeg >= m_severeDL)
		{
			return DoglegColourCodes.Severe;
		}
		return DoglegColourCodes.Normal;
	}

	private bool FinaliseSidetrack(DrillHoleFile newDrillHoleFile)
	{
		if (m_depthState == DepthStateEnum.Extrapolated || m_depthState == DepthStateEnum.Found)
		{
			if (m_targetState == Target_StateEnum.Set)
			{
				if (m_target.TVD > m_sideTrackStart.TVD)
				{
					if (SetDetails(newDrillHoleFile))
					{
						newDrillHoleFile.SurveyList = new SurveyList();
						SetNonRequiredFields(newDrillHoleFile);
						CopySurveyData(ref m_sideTrackStart, ref newDrillHoleFile);
						newDrillHoleFile.SideTrackMD = m_sideTrackStart.MD;
						if (m_minDLState == MinDLStateEnum.TargetNotAchieved)
						{
							return TargetNotAchievedWarning();
						}
						if (m_minDLPathBelowTarget)
						{
							return PathBelowTargetTVDWarning();
						}
						return true;
					}
					return false;
				}
				DisplayErrorPopup("Plan Error", "The Target TVD must be below the TVD of the Sidetrack start.");
				textBox_Target_TVD.Focus();
				return false;
			}
			DisplayErrorPopup("Target Error", "A valid target is required!");
			textBox_Target_TVD.Focus();
			return false;
		}
		DisplayErrorPopup("Sidetrack Depth Error", "A valid sidetrack depth is required!");
		textBox_Depth.Focus();
		return false;
	}

	private bool TargetNotAchievedWarning()
	{
		Popup_MessageBox_YesNo popup_MessageBox_YesNo = new Popup_MessageBox_YesNo("Warning!", "The target you have entered cannot be achieved from the sidetrack depth entered! Do you wish to continue?");
		popup_MessageBox_YesNo.ShowDialog();
		if (popup_MessageBox_YesNo.CustomDialogResult == Popup_MessageBox_YesNo.DialogResultEnum.Yes)
		{
			return true;
		}
		return false;
	}

	private bool PathBelowTargetTVDWarning()
	{
		Popup_MessageBox_YesNo popup_MessageBox_YesNo = new Popup_MessageBox_YesNo("Warning!", "The path of the minimum dogleg build goes below the TVD of the target! Do you wish to continue?");
		popup_MessageBox_YesNo.ShowDialog();
		if (popup_MessageBox_YesNo.CustomDialogResult == Popup_MessageBox_YesNo.DialogResultEnum.Yes)
		{
			return true;
		}
		return false;
	}

	private void CopySurveyData(ref AbsoluteSurvey sideTrackSurvey, ref DrillHoleFile newDrillHoleFile)
	{
		foreach (Survey survey in m_drillHoleFile.SurveyList)
		{
			if (survey.MD < sideTrackSurvey.MD)
			{
				newDrillHoleFile.SurveyList.Append(survey);
				continue;
			}
			Survey element = new Survey(sideTrackSurvey.MD, sideTrackSurvey.Dip, sideTrackSurvey.Azimuth);
			newDrillHoleFile.SurveyList.Append(element);
			break;
		}
	}

	private bool SetDetails(DrillHoleFile newDrillHoleFile)
	{
		if (textBox_holeID.Text != "")
		{
			newDrillHoleFile.HoleID = textBox_holeID.Text;
			if (textBox_rigID.Text != "")
			{
				newDrillHoleFile.RigID = textBox_rigID.Text;
				SetNonRequiredFields(newDrillHoleFile);
				return true;
			}
			DisplayErrorPopup("RigID Error", "A RigID value is required!");
			textBox_rigID.Focus();
			return false;
		}
		DisplayErrorPopup("HoleID Error", "A HoleID value is required!");
		textBox_holeID.Focus();
		return false;
	}

	private void SetNonRequiredFields(DrillHoleFile drillHoleFile)
	{
		drillHoleFile.Location = textBox_Location.Text;
		drillHoleFile.Area = textBox_Area.Text;
		drillHoleFile.DrillingContractor = textBox_DrillingContractor.Text;
		drillHoleFile.SurveyingContractor = textBox_SurveyingContractor.Text;
		drillHoleFile.Client = textBox_Client.Text;
	}

	private bool SetDepth(ref DrillHoleFile newDrillHole)
	{
		return false;
	}

	private void Next()
	{
		if (FinaliseSidetrack(m_newDrillHoleFile))
		{
			ButtonPressedEventArgs e = new ButtonPressedEventArgs();
			e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Next;
			e.NewDrillHoleFile = m_newDrillHoleFile;
			OnButtonPressed(e);
		}
	}

	private void Cancel()
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Cancel;
		OnButtonPressed(e);
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_next_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void button_cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void textBox_Depth_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Depth);
	}

	private void textBox_Depth_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Depth);
	}

	private void textBox_holeID_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_holeID);
	}

	private void textBox_holeID_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_holeID);
	}

	private void textBox_rigID_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_rigID);
	}

	private void textBox_rigID_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_rigID);
	}

	private void textBox_targetTVD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_TVD);
	}

	private void textBox_targetTVD_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_TVD);
	}

	private void textBox_targetNorthing_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_Northing);
	}

	private void textBox_targetNorthing_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_Northing);
	}

	private void textBox_targetEasting_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Target_Easting);
	}

	private void textBox_targetEasting_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Target_Easting);
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

	private void textBox_Location_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Location);
	}

	private void textBox_Location_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Location);
	}

	private void textBox_Area_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Area);
	}

	private void textBox_Area_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Area);
	}

	private void textBox_Client_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Client);
	}

	private void textBox_Client_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Client);
	}

	private void textBox_DrillingContractor_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_DrillingContractor);
	}

	private void textBox_DrillingContractor_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_DrillingContractor);
	}

	private void textBox_SurveyingContractor_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_SurveyingContractor);
	}

	private void textBox_SurveyingContractor_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_SurveyingContractor);
	}

	private bool StringToDecimal(string fieldValue, out decimal value)
	{
		if (fieldValue != "")
		{
			if (Utility.StringToDecimal(fieldValue, out value) == Utility.StringToDecimalResultEnum.NoError)
			{
				return true;
			}
			return false;
		}
		value = 0m;
		return false;
	}

	private void DisplayErrorPopup(string title, string description)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(title, description);
		popup_MessageBox_OK.ShowDialog();
	}

	private void tabControl1_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
	}

	private void textBox_Depth_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Depth);
	}

	private void textBox_Target_TVD_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_TVD);
	}

	private void textBox_Target_Northing_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_Northing);
	}

	private void textBox_Target_Easting_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Target_Easting);
	}

	private void textBox_holeID_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_holeID);
	}

	private void textBox_rigID_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_rigID);
	}

	private void textBox_Location_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Location);
	}

	private void textBox_Area_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Area);
	}

	private void textBox_DrillingContractor_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_DrillingContractor);
	}

	private void textBox_SurveyingContractor_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_SurveyingContractor);
	}

	private void textBox_Client_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Client);
	}
}
