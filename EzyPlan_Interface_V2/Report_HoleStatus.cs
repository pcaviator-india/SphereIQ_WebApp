using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_HoleStatus : Report_Template_Page
{
	private IContainer components;

	private Report_Info_Survey info_SurveyAtTargetTVD;

	private Report_Info_Survey info_SidetrackSurvey;

	private Report_Info_Survey info_SurveyAtEndOfStraightPath;

	private Report_Info_Survey info_EndOfHoleSurvey;

	private Panel panel1;

	private Report_Info_Distance_3DOF info_StraightPathDistance;

	private Report_Info_Distance_3DOF info_DistanceAtTargetTVD;

	private Report_Info_Distance_4DOF info_DistanceFromTarget;

	private Panel panel2;

	private Report_Info_Depth info_CurrentDepth;

	private Report_Info_MinDL info_MinDL;

	public AbsoluteSurvey SurveyAtTargetTVD
	{
		set
		{
			if (value != null)
			{
				info_SurveyAtTargetTVD.Survey = value;
				info_SurveyAtTargetTVD.Visible = true;
			}
			else
			{
				info_SurveyAtTargetTVD.Visible = false;
			}
		}
	}

	public AbsoluteSurvey EndOfHoleSurvey
	{
		set
		{
			info_EndOfHoleSurvey.Survey = value;
			info_CurrentDepth.Survey = value;
		}
	}

	public AbsoluteSurvey SidetrackSurvey
	{
		set
		{
			if (value != null)
			{
				info_SidetrackSurvey.Survey = value;
				info_SidetrackSurvey.Visible = true;
			}
			else
			{
				info_SidetrackSurvey.Visible = false;
			}
		}
	}

	public AbsoluteSurvey SurveyAtEndOfStraightPathToTargetTVD
	{
		set
		{
			if (value != null)
			{
				info_SurveyAtEndOfStraightPath.Survey = value;
				info_SurveyAtEndOfStraightPath.Visible = true;
			}
			else
			{
				info_SurveyAtEndOfStraightPath.Visible = false;
			}
		}
	}

	public RelativePosition DistanceFromTarget
	{
		set
		{
			if (value != null)
			{
				info_DistanceFromTarget.Distance = value;
				info_DistanceFromTarget.Visible = true;
			}
			else
			{
				info_DistanceFromTarget.Visible = false;
			}
		}
	}

	public RelativePosition DistanceFromTargetAtTargetTVD
	{
		set
		{
			if (value != null)
			{
				info_DistanceAtTargetTVD.Distance = value;
				info_DistanceAtTargetTVD.Visible = true;
			}
			else
			{
				info_DistanceAtTargetTVD.Visible = false;
			}
		}
	}

	public RelativePosition StraightPathDistanceFromTarget
	{
		set
		{
			if (value != null)
			{
				info_StraightPathDistance.Distance = value;
				info_StraightPathDistance.Visible = true;
			}
			else
			{
				info_StraightPathDistance.Visible = false;
			}
		}
	}

	public string MinDLStatusString
	{
		set
		{
			info_MinDL.StatusString = value;
		}
	}

	public Color MinDLStatusColor
	{
		set
		{
			info_MinDL.StatusColor = value;
		}
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.info_SurveyAtTargetTVD = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_SidetrackSurvey = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_SurveyAtEndOfStraightPath = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_EndOfHoleSurvey = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_StraightPathDistance = new EzyPlan_Interface_V2.Report_Info_Distance_3DOF();
		this.info_DistanceAtTargetTVD = new EzyPlan_Interface_V2.Report_Info_Distance_3DOF();
		this.info_DistanceFromTarget = new EzyPlan_Interface_V2.Report_Info_Distance_4DOF();
		this.info_CurrentDepth = new EzyPlan_Interface_V2.Report_Info_Depth();
		this.info_MinDL = new EzyPlan_Interface_V2.Report_Info_MinDL();
		base.SuspendLayout();
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 393);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(600, 10);
		this.panel1.TabIndex = 102;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 111);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(600, 10);
		this.panel2.TabIndex = 103;
		this.info_SurveyAtTargetTVD.BackColor = System.Drawing.Color.White;
		this.info_SurveyAtTargetTVD.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_SurveyAtTargetTVD.Heading = "Survey at target TVD:";
		this.info_SurveyAtTargetTVD.Location = new System.Drawing.Point(0, 649);
		this.info_SurveyAtTargetTVD.Name = "info_SurveyAtTargetTVD";
		this.info_SurveyAtTargetTVD.Size = new System.Drawing.Size(600, 82);
		this.info_SurveyAtTargetTVD.TabIndex = 97;
		this.info_SidetrackSurvey.BackColor = System.Drawing.Color.White;
		this.info_SidetrackSurvey.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_SidetrackSurvey.Heading = "Sidetrack survey:";
		this.info_SidetrackSurvey.Location = new System.Drawing.Point(0, 566);
		this.info_SidetrackSurvey.Name = "info_SidetrackSurvey";
		this.info_SidetrackSurvey.Size = new System.Drawing.Size(600, 83);
		this.info_SidetrackSurvey.TabIndex = 105;
		this.info_SurveyAtEndOfStraightPath.BackColor = System.Drawing.Color.White;
		this.info_SurveyAtEndOfStraightPath.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_SurveyAtEndOfStraightPath.Heading = "Survey at end of straight path to target TVD:";
		this.info_SurveyAtEndOfStraightPath.Location = new System.Drawing.Point(0, 484);
		this.info_SurveyAtEndOfStraightPath.Name = "info_SurveyAtEndOfStraightPath";
		this.info_SurveyAtEndOfStraightPath.Size = new System.Drawing.Size(600, 82);
		this.info_SurveyAtEndOfStraightPath.TabIndex = 96;
		this.info_EndOfHoleSurvey.BackColor = System.Drawing.Color.White;
		this.info_EndOfHoleSurvey.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_EndOfHoleSurvey.Heading = "End of hole survey:";
		this.info_EndOfHoleSurvey.Location = new System.Drawing.Point(0, 403);
		this.info_EndOfHoleSurvey.Name = "info_EndOfHoleSurvey";
		this.info_EndOfHoleSurvey.Size = new System.Drawing.Size(600, 81);
		this.info_EndOfHoleSurvey.TabIndex = 95;
		this.info_StraightPathDistance.BackColor = System.Drawing.Color.Transparent;
		this.info_StraightPathDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_StraightPathDistance.Heading = "Distance from target at end of straight path to target TVD";
		this.info_StraightPathDistance.Location = new System.Drawing.Point(0, 302);
		this.info_StraightPathDistance.Name = "info_StraightPathDistance";
		this.info_StraightPathDistance.Size = new System.Drawing.Size(600, 91);
		this.info_StraightPathDistance.TabIndex = 98;
		this.info_DistanceAtTargetTVD.BackColor = System.Drawing.Color.Transparent;
		this.info_DistanceAtTargetTVD.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_DistanceAtTargetTVD.Heading = "Distance from target at target TVD";
		this.info_DistanceAtTargetTVD.Location = new System.Drawing.Point(0, 212);
		this.info_DistanceAtTargetTVD.Name = "info_DistanceAtTargetTVD";
		this.info_DistanceAtTargetTVD.Size = new System.Drawing.Size(600, 90);
		this.info_DistanceAtTargetTVD.TabIndex = 99;
		this.info_DistanceFromTarget.BackColor = System.Drawing.Color.Transparent;
		this.info_DistanceFromTarget.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_DistanceFromTarget.Heading = "Distance from target at current end of hole";
		this.info_DistanceFromTarget.Location = new System.Drawing.Point(0, 121);
		this.info_DistanceFromTarget.Name = "info_DistanceFromTarget";
		this.info_DistanceFromTarget.Size = new System.Drawing.Size(600, 91);
		this.info_DistanceFromTarget.TabIndex = 100;
		this.info_CurrentDepth.BackColor = System.Drawing.Color.White;
		this.info_CurrentDepth.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_CurrentDepth.Heading = "Drill Hole Depth";
		this.info_CurrentDepth.Location = new System.Drawing.Point(0, 0);
		this.info_CurrentDepth.Name = "info_CurrentDepth";
		this.info_CurrentDepth.Size = new System.Drawing.Size(600, 60);
		this.info_CurrentDepth.TabIndex = 101;
		this.info_MinDL.BackColor = System.Drawing.Color.White;
		this.info_MinDL.Dock = System.Windows.Forms.DockStyle.Top;
		this.info_MinDL.Location = new System.Drawing.Point(0, 60);
		this.info_MinDL.Name = "info_MinDL";
		this.info_MinDL.Size = new System.Drawing.Size(600, 51);
		this.info_MinDL.TabIndex = 106;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.Controls.Add(this.info_SurveyAtTargetTVD);
		base.Controls.Add(this.info_SidetrackSurvey);
		base.Controls.Add(this.info_SurveyAtEndOfStraightPath);
		base.Controls.Add(this.info_EndOfHoleSurvey);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.info_StraightPathDistance);
		base.Controls.Add(this.info_DistanceAtTargetTVD);
		base.Controls.Add(this.info_DistanceFromTarget);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.info_MinDL);
		base.Controls.Add(this.info_CurrentDepth);
		this.DoubleBuffered = true;
		this.MaximumSize = new System.Drawing.Size(0, 0);
		base.Name = "Report_HoleStatus";
		base.Size = new System.Drawing.Size(600, 767);
		base.ResumeLayout(false);
	}

	public Report_HoleStatus()
	{
		InitializeComponent();
	}

	public Report_HoleStatus(AbsoluteSurvey surveyAtTargetTVD, AbsoluteSurvey endOfHoleSurvey, AbsoluteSurvey sidetrackSurvey, AbsoluteSurvey surveyAtEndOfStraightPathToTargetTVD, RelativePosition distanceFromTarget, RelativePosition distanceFromTargetAtTargetTVD, RelativePosition straightPathDistanceFromTarget, string minDLStatusString, Color minDLStatusColor)
		: this()
	{
		EndOfHoleSurvey = endOfHoleSurvey;
		SidetrackSurvey = sidetrackSurvey;
		SurveyAtEndOfStraightPathToTargetTVD = surveyAtEndOfStraightPathToTargetTVD;
		SurveyAtTargetTVD = surveyAtTargetTVD;
		DistanceFromTargetAtTargetTVD = distanceFromTargetAtTargetTVD;
		StraightPathDistanceFromTarget = straightPathDistanceFromTarget;
		DistanceFromTarget = distanceFromTarget;
		MinDLStatusString = minDLStatusString;
		MinDLStatusColor = minDLStatusColor;
		ResumeLayout(performLayout: true);
	}
}
