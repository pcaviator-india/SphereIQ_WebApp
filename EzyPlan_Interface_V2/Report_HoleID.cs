using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;
using EzyPlanStorage;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_HoleID : Report_Template_CoverPage
{
	private IContainer components;

	private GroupBox groupBox_Identification;

	private Label label_HoleID;

	private Label label_RigID;

	private TextBox textBox_HoleID;

	private TextBox textBox_RigID;

	private Label label7;

	private TextBox textBox_Location;

	private GroupBox groupBox1;

	private Label label10;

	private Label label11;

	private TextBox textBox_AuthorSignature;

	private TextBox textBox_ClientSignature;

	private Label label12;

	private TextBox textBox_DrillingContractor;

	private Label label14;

	private TextBox textBox_Area;

	private Label label8;

	private TextBox textBox_Client;

	private GroupBox groupBox2;

	private Label label13;

	private TextBox textBox_Notes;

	private Label label17;

	private TextBox textBox_Author;

	private Info_Depth info_SidetrackDepth;

	private Info_Collar info_Collar1;

	private Info_Target info_Target1;

	private Label label1;

	private TextBox textBox_surveyingContractor;

	public Report_HoleID()
	{
		InitializeComponent();
	}

	public Report_HoleID(DrillHoleFile drillHoleFile, AbsoluteSurvey sideTrackSurvey, string author, string notes)
		: this()
	{
		textBox_HoleID.Text = drillHoleFile.HoleID;
		textBox_RigID.Text = drillHoleFile.RigID;
		textBox_Location.Text = drillHoleFile.Location;
		textBox_Area.Text = drillHoleFile.Area;
		textBox_Client.Text = drillHoleFile.Client;
		textBox_DrillingContractor.Text = drillHoleFile.DrillingContractor;
		textBox_surveyingContractor.Text = drillHoleFile.SurveyingContractor;
		textBox_Author.Text = author;
		textBox_Notes.Text = notes;
		info_Target1.Target = drillHoleFile.Target;
		info_Collar1.Collar = drillHoleFile.Collar;
		if (sideTrackSurvey != null)
		{
			info_SidetrackDepth.AbsoluteSurvey = sideTrackSurvey;
			info_SidetrackDepth.Visible = true;
		}
		else
		{
			info_SidetrackDepth.Visible = false;
		}
		ResumeLayout(performLayout: true);
		Refresh();
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
		this.groupBox_Identification = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.textBox_Client = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label_HoleID = new System.Windows.Forms.Label();
		this.label_RigID = new System.Windows.Forms.Label();
		this.textBox_surveyingContractor = new System.Windows.Forms.TextBox();
		this.textBox_DrillingContractor = new System.Windows.Forms.TextBox();
		this.textBox_Area = new System.Windows.Forms.TextBox();
		this.textBox_Location = new System.Windows.Forms.TextBox();
		this.textBox_HoleID = new System.Windows.Forms.TextBox();
		this.textBox_RigID = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.textBox_AuthorSignature = new System.Windows.Forms.TextBox();
		this.textBox_ClientSignature = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label13 = new System.Windows.Forms.Label();
		this.textBox_Notes = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.textBox_Author = new System.Windows.Forms.TextBox();
		this.info_Target1 = new EzyPlan_Interface_V2.Info_Target();
		this.info_Collar1 = new EzyPlan_Interface_V2.Info_Collar();
		this.info_SidetrackDepth = new EzyPlan_Interface_V2.Info_Depth();
		this.groupBox_Identification.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Identification.Controls.Add(this.label8);
		this.groupBox_Identification.Controls.Add(this.textBox_Client);
		this.groupBox_Identification.Controls.Add(this.label1);
		this.groupBox_Identification.Controls.Add(this.label12);
		this.groupBox_Identification.Controls.Add(this.label14);
		this.groupBox_Identification.Controls.Add(this.label7);
		this.groupBox_Identification.Controls.Add(this.label_HoleID);
		this.groupBox_Identification.Controls.Add(this.label_RigID);
		this.groupBox_Identification.Controls.Add(this.textBox_surveyingContractor);
		this.groupBox_Identification.Controls.Add(this.textBox_DrillingContractor);
		this.groupBox_Identification.Controls.Add(this.textBox_Area);
		this.groupBox_Identification.Controls.Add(this.textBox_Location);
		this.groupBox_Identification.Controls.Add(this.textBox_HoleID);
		this.groupBox_Identification.Controls.Add(this.textBox_RigID);
		this.groupBox_Identification.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Identification.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Identification.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Identification.Name = "groupBox_Identification";
		this.groupBox_Identification.Size = new System.Drawing.Size(600, 228);
		this.groupBox_Identification.TabIndex = 10;
		this.groupBox_Identification.TabStop = false;
		this.groupBox_Identification.Text = "Hole Identification:";
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Location = new System.Drawing.Point(3, 191);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 20);
		this.label8.TabIndex = 3;
		this.label8.Text = "Client:";
		this.textBox_Client.BackColor = System.Drawing.Color.White;
		this.textBox_Client.ForeColor = System.Drawing.Color.Black;
		this.textBox_Client.Location = new System.Drawing.Point(171, 191);
		this.textBox_Client.Multiline = true;
		this.textBox_Client.Name = "textBox_Client";
		this.textBox_Client.ReadOnly = true;
		this.textBox_Client.Size = new System.Drawing.Size(410, 26);
		this.textBox_Client.TabIndex = 4;
		this.textBox_Client.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(3, 159);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(161, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "Surveying Contractor:";
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.Black;
		this.label12.Location = new System.Drawing.Point(3, 127);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(139, 20);
		this.label12.TabIndex = 0;
		this.label12.Text = "Drilling Contractor:";
		this.label14.AutoSize = true;
		this.label14.ForeColor = System.Drawing.Color.Black;
		this.label14.Location = new System.Drawing.Point(3, 101);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(47, 20);
		this.label14.TabIndex = 0;
		this.label14.Text = "Area:";
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.Color.Black;
		this.label7.Location = new System.Drawing.Point(3, 68);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(74, 20);
		this.label7.TabIndex = 0;
		this.label7.Text = "Location:";
		this.label_HoleID.AutoSize = true;
		this.label_HoleID.ForeColor = System.Drawing.Color.Black;
		this.label_HoleID.Location = new System.Drawing.Point(3, 30);
		this.label_HoleID.Name = "label_HoleID";
		this.label_HoleID.Size = new System.Drawing.Size(67, 20);
		this.label_HoleID.TabIndex = 0;
		this.label_HoleID.Text = "Hole ID:";
		this.label_RigID.AutoSize = true;
		this.label_RigID.ForeColor = System.Drawing.Color.Black;
		this.label_RigID.Location = new System.Drawing.Point(303, 30);
		this.label_RigID.Name = "label_RigID";
		this.label_RigID.Size = new System.Drawing.Size(54, 20);
		this.label_RigID.TabIndex = 0;
		this.label_RigID.Text = "RigID:";
		this.textBox_surveyingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_surveyingContractor.ForeColor = System.Drawing.Color.Black;
		this.textBox_surveyingContractor.Location = new System.Drawing.Point(171, 159);
		this.textBox_surveyingContractor.Multiline = true;
		this.textBox_surveyingContractor.Name = "textBox_surveyingContractor";
		this.textBox_surveyingContractor.ReadOnly = true;
		this.textBox_surveyingContractor.Size = new System.Drawing.Size(410, 26);
		this.textBox_surveyingContractor.TabIndex = 2;
		this.textBox_surveyingContractor.TabStop = false;
		this.textBox_DrillingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_DrillingContractor.ForeColor = System.Drawing.Color.Black;
		this.textBox_DrillingContractor.Location = new System.Drawing.Point(171, 127);
		this.textBox_DrillingContractor.Multiline = true;
		this.textBox_DrillingContractor.Name = "textBox_DrillingContractor";
		this.textBox_DrillingContractor.ReadOnly = true;
		this.textBox_DrillingContractor.Size = new System.Drawing.Size(410, 26);
		this.textBox_DrillingContractor.TabIndex = 2;
		this.textBox_DrillingContractor.TabStop = false;
		this.textBox_Area.BackColor = System.Drawing.Color.White;
		this.textBox_Area.ForeColor = System.Drawing.Color.Black;
		this.textBox_Area.Location = new System.Drawing.Point(171, 95);
		this.textBox_Area.Multiline = true;
		this.textBox_Area.Name = "textBox_Area";
		this.textBox_Area.ReadOnly = true;
		this.textBox_Area.Size = new System.Drawing.Size(410, 26);
		this.textBox_Area.TabIndex = 2;
		this.textBox_Area.TabStop = false;
		this.textBox_Location.BackColor = System.Drawing.Color.White;
		this.textBox_Location.ForeColor = System.Drawing.Color.Black;
		this.textBox_Location.Location = new System.Drawing.Point(171, 62);
		this.textBox_Location.Multiline = true;
		this.textBox_Location.Name = "textBox_Location";
		this.textBox_Location.ReadOnly = true;
		this.textBox_Location.Size = new System.Drawing.Size(409, 26);
		this.textBox_Location.TabIndex = 2;
		this.textBox_Location.TabStop = false;
		this.textBox_HoleID.BackColor = System.Drawing.Color.White;
		this.textBox_HoleID.ForeColor = System.Drawing.Color.Black;
		this.textBox_HoleID.Location = new System.Drawing.Point(83, 30);
		this.textBox_HoleID.Multiline = true;
		this.textBox_HoleID.Name = "textBox_HoleID";
		this.textBox_HoleID.ReadOnly = true;
		this.textBox_HoleID.Size = new System.Drawing.Size(214, 26);
		this.textBox_HoleID.TabIndex = 2;
		this.textBox_HoleID.TabStop = false;
		this.textBox_HoleID.Text = "123456789012345678";
		this.textBox_RigID.BackColor = System.Drawing.Color.White;
		this.textBox_RigID.ForeColor = System.Drawing.Color.Black;
		this.textBox_RigID.Location = new System.Drawing.Point(358, 30);
		this.textBox_RigID.Multiline = true;
		this.textBox_RigID.Name = "textBox_RigID";
		this.textBox_RigID.ReadOnly = true;
		this.textBox_RigID.Size = new System.Drawing.Size(223, 26);
		this.textBox_RigID.TabIndex = 2;
		this.textBox_RigID.TabStop = false;
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.textBox_AuthorSignature);
		this.groupBox1.Controls.Add(this.textBox_ClientSignature);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 600);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(600, 69);
		this.groupBox1.TabIndex = 18;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Signatures:";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(311, 25);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 20);
		this.label10.TabIndex = 3;
		this.label10.Text = "Client:";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(5, 25);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(61, 20);
		this.label11.TabIndex = 3;
		this.label11.Text = "Author:";
		this.textBox_AuthorSignature.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox_AuthorSignature.Location = new System.Drawing.Point(76, 28);
		this.textBox_AuthorSignature.Multiline = true;
		this.textBox_AuthorSignature.Name = "textBox_AuthorSignature";
		this.textBox_AuthorSignature.ReadOnly = true;
		this.textBox_AuthorSignature.Size = new System.Drawing.Size(210, 34);
		this.textBox_AuthorSignature.TabIndex = 2;
		this.textBox_AuthorSignature.TabStop = false;
		this.textBox_ClientSignature.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox_ClientSignature.Location = new System.Drawing.Point(370, 25);
		this.textBox_ClientSignature.Multiline = true;
		this.textBox_ClientSignature.Name = "textBox_ClientSignature";
		this.textBox_ClientSignature.ReadOnly = true;
		this.textBox_ClientSignature.Size = new System.Drawing.Size(210, 37);
		this.textBox_ClientSignature.TabIndex = 2;
		this.textBox_ClientSignature.TabStop = false;
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.textBox_Notes);
		this.groupBox2.Controls.Add(this.label17);
		this.groupBox2.Controls.Add(this.textBox_Author);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.Location = new System.Drawing.Point(0, 228);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(600, 125);
		this.groupBox2.TabIndex = 19;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "User fields:";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(14, 57);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(55, 20);
		this.label13.TabIndex = 8;
		this.label13.Text = "Notes:";
		this.textBox_Notes.BackColor = System.Drawing.Color.White;
		this.textBox_Notes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Notes.Location = new System.Drawing.Point(83, 57);
		this.textBox_Notes.Multiline = true;
		this.textBox_Notes.Name = "textBox_Notes";
		this.textBox_Notes.ReadOnly = true;
		this.textBox_Notes.Size = new System.Drawing.Size(497, 58);
		this.textBox_Notes.TabIndex = 9;
		this.textBox_Notes.TabStop = false;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(8, 25);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(61, 20);
		this.label17.TabIndex = 3;
		this.label17.Text = "Author:";
		this.textBox_Author.BackColor = System.Drawing.Color.White;
		this.textBox_Author.Location = new System.Drawing.Point(83, 22);
		this.textBox_Author.Multiline = true;
		this.textBox_Author.Name = "textBox_Author";
		this.textBox_Author.ReadOnly = true;
		this.textBox_Author.Size = new System.Drawing.Size(372, 26);
		this.textBox_Author.TabIndex = 6;
		this.textBox_Author.TabStop = false;
		this.info_Target1.BackColor = System.Drawing.Color.Transparent;
		this.info_Target1.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_Target1.Location = new System.Drawing.Point(0, 483);
		this.info_Target1.Name = "info_Target1";
		this.info_Target1.Size = new System.Drawing.Size(600, 109);
		this.info_Target1.TabIndex = 22;
		this.info_Target1.Target = null;
		this.info_Collar1.BackColor = System.Drawing.Color.Transparent;
		this.info_Collar1.Collar = null;
		this.info_Collar1.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_Collar1.Location = new System.Drawing.Point(0, 421);
		this.info_Collar1.Name = "info_Collar1";
		this.info_Collar1.Size = new System.Drawing.Size(600, 62);
		this.info_Collar1.TabIndex = 21;
		this.info_SidetrackDepth.AbsoluteSurvey = null;
		this.info_SidetrackDepth.BackColor = System.Drawing.Color.Transparent;
		this.info_SidetrackDepth.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_SidetrackDepth.Heading = "Sidetrack Depth";
		this.info_SidetrackDepth.Location = new System.Drawing.Point(0, 353);
		this.info_SidetrackDepth.Name = "info_SidetrackDepth";
		this.info_SidetrackDepth.Size = new System.Drawing.Size(600, 68);
		this.info_SidetrackDepth.TabIndex = 20;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.info_Target1);
		base.Controls.Add(this.info_Collar1);
		base.Controls.Add(this.info_SidetrackDepth);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox_Identification);
		base.Name = "Report_HoleID";
		this.groupBox_Identification.ResumeLayout(false);
		this.groupBox_Identification.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
