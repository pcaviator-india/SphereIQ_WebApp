using System.ComponentModel;
using System.Drawing;
using EzyPlanData;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_BuildResults : Report_Template_Page, IReportTemplate
{
	private IContainer components;

	private Report_Info_BuildResults Info_BuildResults;

	private Report_Info_Survey Info_EndOfHoleSurvey;

	private Report_Info_Survey Info_EndOfBuildSurvey;

	public BuildAndHoldResults BuildResults
	{
		set
		{
			Info_BuildResults.BuildResults = value;
			Info_EndOfBuildSurvey.Survey = value.EndOfBuildSurvey;
			Info_EndOfHoleSurvey.Survey = value.EndOfHoleSurvey;
		}
	}

	public Report_BuildResults(BuildAndHoldResults buildResults)
	{
		InitializeComponent();
		BuildResults = buildResults;
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
		this.Info_EndOfBuildSurvey = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.Info_EndOfHoleSurvey = new EzyPlan_Interface_V2.Report_Info_Survey();
		this.Info_BuildResults = new EzyPlan_Interface_V2.Report_Info_BuildResults();
		base.SuspendLayout();
		this.Info_EndOfBuildSurvey.BackColor = System.Drawing.Color.White;
		this.Info_EndOfBuildSurvey.Heading = "Survey at end of build:";
		this.Info_EndOfBuildSurvey.Location = new System.Drawing.Point(4, 294);
		this.Info_EndOfBuildSurvey.Name = "Info_EndOfBuildSurvey";
		this.Info_EndOfBuildSurvey.Size = new System.Drawing.Size(587, 89);
		this.Info_EndOfBuildSurvey.TabIndex = 29;
		this.Info_EndOfHoleSurvey.BackColor = System.Drawing.Color.White;
		this.Info_EndOfHoleSurvey.Heading = "Survey at end of hole:";
		this.Info_EndOfHoleSurvey.Location = new System.Drawing.Point(4, 389);
		this.Info_EndOfHoleSurvey.Name = "Info_EndOfHoleSurvey";
		this.Info_EndOfHoleSurvey.Size = new System.Drawing.Size(588, 89);
		this.Info_EndOfHoleSurvey.TabIndex = 28;
		this.Info_BuildResults.BackColor = System.Drawing.Color.White;
		this.Info_BuildResults.Location = new System.Drawing.Point(4, 4);
		this.Info_BuildResults.Name = "Info_BuildResults";
		this.Info_BuildResults.Size = new System.Drawing.Size(587, 233);
		this.Info_BuildResults.TabIndex = 27;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Controls.Add(this.Info_EndOfBuildSurvey);
		base.Controls.Add(this.Info_EndOfHoleSurvey);
		base.Controls.Add(this.Info_BuildResults);
		this.MaximumSize = new System.Drawing.Size(600, 778);
		this.MinimumSize = new System.Drawing.Size(600, 778);
		base.Name = "Report_BuildResults";
		base.Size = new System.Drawing.Size(600, 778);
		base.ResumeLayout(false);
	}
}
