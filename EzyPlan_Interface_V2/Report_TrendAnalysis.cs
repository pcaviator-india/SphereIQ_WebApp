using System.ComponentModel;
using System.Drawing;
using EzyPlanData;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_TrendAnalysis : Report_Template_Page
{
	private IContainer components;

	private Report_Info_Distance_4DOF info_extrapolatedDistanceFromTarget;

	private Report_Info_Survey info_surveyAtStartOfExtrapolation;

	private Report_Info_Survey info_SurveyAtEndOfExtrapolation;

	private Report_Info_TrendAnalysisResults report_Info_TrendAnalysisResults1;

	public TrendAnalysisResults TrendAnalysisResults
	{
		set
		{
			report_Info_TrendAnalysisResults1.TrendAnalysisResults = value;
		}
	}

	public RelativePosition ExtrapolatedDistanceFromTarget
	{
		set
		{
			if (value != null)
			{
				info_extrapolatedDistanceFromTarget.Distance = value;
				info_extrapolatedDistanceFromTarget.Visible = true;
			}
			else
			{
				info_extrapolatedDistanceFromTarget.Visible = false;
			}
		}
	}

	public AbsoluteSurvey SurveyAtStartOfExtrapolation
	{
		set
		{
			if (value != null)
			{
				info_surveyAtStartOfExtrapolation.Survey = value;
				info_surveyAtStartOfExtrapolation.Visible = true;
			}
			else
			{
				info_surveyAtStartOfExtrapolation.Visible = false;
			}
		}
	}

	public AbsoluteSurvey SurveyAtEndOfExtrapolation
	{
		set
		{
			if (value != null)
			{
				info_SurveyAtEndOfExtrapolation.Survey = value;
				info_SurveyAtEndOfExtrapolation.Visible = true;
			}
			else
			{
				info_SurveyAtEndOfExtrapolation.Visible = false;
			}
		}
	}

	public Report_TrendAnalysis(TrendAnalysisResults trendAnalysisResults, RelativePosition extrapolatedDistanceFromTarget)
	{
		InitializeComponent();
		ExtrapolatedDistanceFromTarget = extrapolatedDistanceFromTarget;
		if (trendAnalysisResults.ExtrapolatedSurveyData != null)
		{
			SurveyAtStartOfExtrapolation = trendAnalysisResults.ExtrapolatedSurveyData.RecordAt(0);
			SurveyAtEndOfExtrapolation = trendAnalysisResults.ExtrapolatedSurveyData.LastRecord();
		}
		TrendAnalysisResults = trendAnalysisResults;
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
		this.report_Info_TrendAnalysisResults1 = new EzyPlan_Interface_V2.Report_Info_TrendAnalysisResults();
		this.info_SurveyAtEndOfExtrapolation = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_surveyAtStartOfExtrapolation = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.info_extrapolatedDistanceFromTarget = new EzyPlan_Interface_V2.Report_Info_Distance_4DOF();
		base.SuspendLayout();
		this.report_Info_TrendAnalysisResults1.BackColor = System.Drawing.Color.White;
		this.report_Info_TrendAnalysisResults1.Location = new System.Drawing.Point(0, 0);
		this.report_Info_TrendAnalysisResults1.Name = "report_Info_TrendAnalysisResults1";
		this.report_Info_TrendAnalysisResults1.Size = new System.Drawing.Size(600, 397);
		this.report_Info_TrendAnalysisResults1.TabIndex = 62;
		this.info_SurveyAtEndOfExtrapolation.BackColor = System.Drawing.Color.White;
		this.info_SurveyAtEndOfExtrapolation.Heading = "Survey at end of extrapolation:";
		this.info_SurveyAtEndOfExtrapolation.Location = new System.Drawing.Point(0, 603);
		this.info_SurveyAtEndOfExtrapolation.Name = "info_SurveyAtEndOfExtrapolation";
		this.info_SurveyAtEndOfExtrapolation.Size = new System.Drawing.Size(600, 89);
		this.info_SurveyAtEndOfExtrapolation.TabIndex = 61;
		this.info_surveyAtStartOfExtrapolation.BackColor = System.Drawing.Color.White;
		this.info_surveyAtStartOfExtrapolation.Heading = "Survey at start of extrapolation:";
		this.info_surveyAtStartOfExtrapolation.Location = new System.Drawing.Point(0, 509);
		this.info_surveyAtStartOfExtrapolation.Name = "info_surveyAtStartOfExtrapolation";
		this.info_surveyAtStartOfExtrapolation.Size = new System.Drawing.Size(600, 88);
		this.info_surveyAtStartOfExtrapolation.TabIndex = 60;
		this.info_extrapolatedDistanceFromTarget.BackColor = System.Drawing.Color.White;
		this.info_extrapolatedDistanceFromTarget.Heading = "Extrapolated distance from target";
		this.info_extrapolatedDistanceFromTarget.Location = new System.Drawing.Point(0, 417);
		this.info_extrapolatedDistanceFromTarget.Name = "info_extrapolatedDistanceFromTarget";
		this.info_extrapolatedDistanceFromTarget.Size = new System.Drawing.Size(600, 86);
		this.info_extrapolatedDistanceFromTarget.TabIndex = 59;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Controls.Add(this.report_Info_TrendAnalysisResults1);
		base.Controls.Add(this.info_SurveyAtEndOfExtrapolation);
		base.Controls.Add(this.info_surveyAtStartOfExtrapolation);
		base.Controls.Add(this.info_extrapolatedDistanceFromTarget);
		base.Name = "Report_TrendAnalysis";
		base.ResumeLayout(false);
	}
}
