using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_Extrapolate : Report_Template_Page
{
	private IContainer components;

	private Report_Info_Distance_3DOF Info_DistanceAtTargetTVD;

	private Report_Info_Distance_3DOF Info_StraightPathDistance;

	private Report_Info_Distance_4DOF Info_ExtrapolatedDistanceFromTarget;

	private Report_Info_Survey Info_SurveyAtEndOfExtrapolation;

	private Panel panel1;

	private Report_Info_Survey Info_SurveyAtEndOfStraightPath;

	private Report_Info_Survey Info_ExtrapolatedSurveyAtTargetTVD;

	public AbsoluteSurvey SurveyAtEndOfExtrapolation
	{
		set
		{
			if (value != null)
			{
				Info_SurveyAtEndOfExtrapolation.Survey = value;
				Info_SurveyAtEndOfExtrapolation.Visible = true;
			}
			else
			{
				Info_SurveyAtEndOfExtrapolation.Visible = false;
			}
		}
	}

	public AbsoluteSurvey SurveyAtEndOfStraightPath
	{
		set
		{
			if (value != null)
			{
				Info_SurveyAtEndOfStraightPath.Survey = value;
				Info_SurveyAtEndOfStraightPath.Visible = true;
			}
			else
			{
				Info_SurveyAtEndOfStraightPath.Visible = false;
			}
		}
	}

	public AbsoluteSurvey ExtrapolatedSurveyAtTargetTVD
	{
		set
		{
			if (value != null)
			{
				Info_ExtrapolatedSurveyAtTargetTVD.Survey = value;
				Info_ExtrapolatedSurveyAtTargetTVD.Visible = true;
			}
			else
			{
				Info_ExtrapolatedSurveyAtTargetTVD.Visible = false;
			}
		}
	}

	public RelativePosition ExtrapolatedDistanceFromTarget
	{
		set
		{
			if (value != null)
			{
				Info_ExtrapolatedDistanceFromTarget.Distance = value;
				Info_ExtrapolatedDistanceFromTarget.Visible = true;
			}
			else
			{
				Info_ExtrapolatedDistanceFromTarget.Visible = false;
			}
		}
	}

	public RelativePosition DistanceAtTargetTVD
	{
		set
		{
			if (value != null)
			{
				Info_DistanceAtTargetTVD.Distance = value;
				Info_DistanceAtTargetTVD.Visible = true;
			}
			else
			{
				Info_DistanceAtTargetTVD.Visible = false;
			}
		}
	}

	public RelativePosition StraightPathDistance
	{
		set
		{
			if (value != null)
			{
				Info_StraightPathDistance.Distance = value;
				Info_StraightPathDistance.Visible = true;
			}
			else
			{
				Info_StraightPathDistance.Visible = false;
			}
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
		this.Info_ExtrapolatedSurveyAtTargetTVD = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.Info_SurveyAtEndOfStraightPath = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.Info_SurveyAtEndOfExtrapolation = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.Info_StraightPathDistance = new EzyPlan_Interface_V2.Report_Info_Distance_3DOF();
		this.Info_DistanceAtTargetTVD = new EzyPlan_Interface_V2.Report_Info_Distance_3DOF();
		this.Info_ExtrapolatedDistanceFromTarget = new EzyPlan_Interface_V2.Report_Info_Distance_4DOF();
		base.SuspendLayout();
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 280);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(600, 32);
		this.panel1.TabIndex = 14;
		this.Info_ExtrapolatedSurveyAtTargetTVD.BackColor = System.Drawing.Color.White;
		this.Info_ExtrapolatedSurveyAtTargetTVD.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_ExtrapolatedSurveyAtTargetTVD.Heading = "Extrapolated survey at target TVD:";
		this.Info_ExtrapolatedSurveyAtTargetTVD.Location = new System.Drawing.Point(0, 486);
		this.Info_ExtrapolatedSurveyAtTargetTVD.Name = "Info_ExtrapolatedSurveyAtTargetTVD";
		this.Info_ExtrapolatedSurveyAtTargetTVD.Size = new System.Drawing.Size(600, 88);
		this.Info_ExtrapolatedSurveyAtTargetTVD.TabIndex = 16;
		this.Info_SurveyAtEndOfStraightPath.BackColor = System.Drawing.Color.White;
		this.Info_SurveyAtEndOfStraightPath.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_SurveyAtEndOfStraightPath.Heading = "Survey at end of extrapolated straight path to target TVD:";
		this.Info_SurveyAtEndOfStraightPath.Location = new System.Drawing.Point(0, 399);
		this.Info_SurveyAtEndOfStraightPath.Name = "Info_SurveyAtEndOfStraightPath";
		this.Info_SurveyAtEndOfStraightPath.Size = new System.Drawing.Size(600, 87);
		this.Info_SurveyAtEndOfStraightPath.TabIndex = 15;
		this.Info_SurveyAtEndOfExtrapolation.BackColor = System.Drawing.Color.White;
		this.Info_SurveyAtEndOfExtrapolation.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_SurveyAtEndOfExtrapolation.Heading = "Survey at end of extrapolation:";
		this.Info_SurveyAtEndOfExtrapolation.Location = new System.Drawing.Point(0, 312);
		this.Info_SurveyAtEndOfExtrapolation.Name = "Info_SurveyAtEndOfExtrapolation";
		this.Info_SurveyAtEndOfExtrapolation.Size = new System.Drawing.Size(600, 87);
		this.Info_SurveyAtEndOfExtrapolation.TabIndex = 13;
		this.Info_StraightPathDistance.BackColor = System.Drawing.Color.White;
		this.Info_StraightPathDistance.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_StraightPathDistance.Heading = "Extrapolated straight path distance from target";
		this.Info_StraightPathDistance.Location = new System.Drawing.Point(0, 187);
		this.Info_StraightPathDistance.Name = "Info_StraightPathDistance";
		this.Info_StraightPathDistance.Size = new System.Drawing.Size(600, 93);
		this.Info_StraightPathDistance.TabIndex = 11;
		this.Info_DistanceAtTargetTVD.BackColor = System.Drawing.Color.White;
		this.Info_DistanceAtTargetTVD.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_DistanceAtTargetTVD.Heading = "Extrapolated distance from target at target TVD";
		this.Info_DistanceAtTargetTVD.Location = new System.Drawing.Point(0, 94);
		this.Info_DistanceAtTargetTVD.Name = "Info_DistanceAtTargetTVD";
		this.Info_DistanceAtTargetTVD.Size = new System.Drawing.Size(600, 93);
		this.Info_DistanceAtTargetTVD.TabIndex = 10;
		this.Info_ExtrapolatedDistanceFromTarget.BackColor = System.Drawing.Color.White;
		this.Info_ExtrapolatedDistanceFromTarget.Dock = System.Windows.Forms.DockStyle.Top;
		this.Info_ExtrapolatedDistanceFromTarget.Heading = "Extrapolated distance from target";
		this.Info_ExtrapolatedDistanceFromTarget.Location = new System.Drawing.Point(0, 0);
		this.Info_ExtrapolatedDistanceFromTarget.Name = "Info_ExtrapolatedDistanceFromTarget";
		this.Info_ExtrapolatedDistanceFromTarget.Size = new System.Drawing.Size(600, 94);
		this.Info_ExtrapolatedDistanceFromTarget.TabIndex = 12;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Controls.Add(this.Info_ExtrapolatedSurveyAtTargetTVD);
		base.Controls.Add(this.Info_SurveyAtEndOfStraightPath);
		base.Controls.Add(this.Info_SurveyAtEndOfExtrapolation);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.Info_StraightPathDistance);
		base.Controls.Add(this.Info_DistanceAtTargetTVD);
		base.Controls.Add(this.Info_ExtrapolatedDistanceFromTarget);
		base.Name = "Report_Extrapolate";
		base.ResumeLayout(false);
	}

	public Report_Extrapolate(AbsoluteSurvey surveyAtEndOfExtrapolation, AbsoluteSurvey surveyAtEndOfStraightPath, AbsoluteSurvey extrapolatedSurveyAtTargetTVD, RelativePosition extrapolatedDistanceFromTarget, RelativePosition distanceAtTargetTVD, RelativePosition straightPathDistance)
	{
		InitializeComponent();
		SurveyAtEndOfExtrapolation = surveyAtEndOfExtrapolation;
		SurveyAtEndOfStraightPath = surveyAtEndOfStraightPath;
		ExtrapolatedSurveyAtTargetTVD = extrapolatedSurveyAtTargetTVD;
		ExtrapolatedDistanceFromTarget = extrapolatedDistanceFromTarget;
		DistanceAtTargetTVD = distanceAtTargetTVD;
		StraightPathDistance = straightPathDistance;
	}
}
