using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_TrendAnalysisResults : UserControl
{
	private IContainer components;

	private Label label_AngleChangeLabel;

	private GroupBox groupBox_DoglegSeverity;

	private Label label_Dogleg;

	private GroupBox groupBox_ToolFaceSetting;

	private Label label_ToolFaceSetting;

	private GroupBox groupBox_Azimuth;

	private Label label_AzimuthTurnRate;

	private Label label_AzimuthTrend;

	private GroupBox groupBox_Inclination;

	private Label label_InclinationBuildRate;

	private Label label_InclinationTrend;

	private GroupBox groupBox1;

	public TrendAnalysisResults TrendAnalysisResults
	{
		set
		{
			DisplayTrendAnalysisResults(value);
		}
	}

	public Info_TrendAnalysisResults()
	{
		InitializeComponent();
	}

	private void DisplayTrendAnalysisResults(TrendAnalysisResults trendAnalysisResults)
	{
		if (trendAnalysisResults != null)
		{
			if (trendAnalysisResults.InclinationTrend == InclinationTrend.Build)
			{
				label_InclinationTrend.Text = "The inclination is trending to build.";
				label_InclinationBuildRate.Text = "The build rate is " + trendAnalysisResults.BuildRate + " degrees/30m.";
			}
			else if (trendAnalysisResults.InclinationTrend == InclinationTrend.Drop)
			{
				label_InclinationTrend.Text = "The inclination is trending to drop.";
				label_InclinationBuildRate.Text = "The drop rate is " + trendAnalysisResults.BuildRate + " degrees/30m.";
			}
			else
			{
				label_InclinationTrend.Text = "There is no inclination trend.";
				label_InclinationBuildRate.Text = "";
			}
			if (trendAnalysisResults.AzimuthTrend != AzimuthTrend.NoTrend)
			{
				label_AzimuthTrend.Text = "The azimuth is trending " + trendAnalysisResults.AzimuthTrend.ToString() + ".";
				label_AzimuthTurnRate.Text = "The turn rate is " + trendAnalysisResults.TurnRate + " degrees/30m.";
			}
			else
			{
				label_AzimuthTrend.Text = "There is no azimuth trend.";
				label_AzimuthTurnRate.Text = "";
			}
			label_ToolFaceSetting.Text = trendAnalysisResults.ToolFaceSetting + " degrees off the high side.";
			label_Dogleg.Text = trendAnalysisResults.DogLegSeverity + " degrees/30m.";
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
		this.label_AngleChangeLabel = new System.Windows.Forms.Label();
		this.groupBox_DoglegSeverity = new System.Windows.Forms.GroupBox();
		this.label_Dogleg = new System.Windows.Forms.Label();
		this.groupBox_ToolFaceSetting = new System.Windows.Forms.GroupBox();
		this.label_ToolFaceSetting = new System.Windows.Forms.Label();
		this.groupBox_Azimuth = new System.Windows.Forms.GroupBox();
		this.label_AzimuthTurnRate = new System.Windows.Forms.Label();
		this.label_AzimuthTrend = new System.Windows.Forms.Label();
		this.groupBox_Inclination = new System.Windows.Forms.GroupBox();
		this.label_InclinationBuildRate = new System.Windows.Forms.Label();
		this.label_InclinationTrend = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox_DoglegSeverity.SuspendLayout();
		this.groupBox_ToolFaceSetting.SuspendLayout();
		this.groupBox_Azimuth.SuspendLayout();
		this.groupBox_Inclination.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.label_AngleChangeLabel.AutoSize = true;
		this.label_AngleChangeLabel.Location = new System.Drawing.Point(6, -61);
		this.label_AngleChangeLabel.Name = "label_AngleChangeLabel";
		this.label_AngleChangeLabel.Size = new System.Drawing.Size(141, 13);
		this.label_AngleChangeLabel.TabIndex = 50;
		this.label_AngleChangeLabel.Text = "The overall angle change is:";
		this.groupBox_DoglegSeverity.Controls.Add(this.label_Dogleg);
		this.groupBox_DoglegSeverity.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_DoglegSeverity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_DoglegSeverity.Location = new System.Drawing.Point(3, 79);
		this.groupBox_DoglegSeverity.Name = "groupBox_DoglegSeverity";
		this.groupBox_DoglegSeverity.Size = new System.Drawing.Size(573, 60);
		this.groupBox_DoglegSeverity.TabIndex = 53;
		this.groupBox_DoglegSeverity.TabStop = false;
		this.groupBox_DoglegSeverity.Text = "Dogleg severity:";
		this.label_Dogleg.AutoSize = true;
		this.label_Dogleg.ForeColor = System.Drawing.Color.Black;
		this.label_Dogleg.Location = new System.Drawing.Point(7, 25);
		this.label_Dogleg.Name = "label_Dogleg";
		this.label_Dogleg.Size = new System.Drawing.Size(33, 20);
		this.label_Dogleg.TabIndex = 0;
		this.label_Dogleg.Text = "null";
		this.groupBox_ToolFaceSetting.Controls.Add(this.label_ToolFaceSetting);
		this.groupBox_ToolFaceSetting.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_ToolFaceSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_ToolFaceSetting.Location = new System.Drawing.Point(3, 22);
		this.groupBox_ToolFaceSetting.Name = "groupBox_ToolFaceSetting";
		this.groupBox_ToolFaceSetting.Size = new System.Drawing.Size(573, 57);
		this.groupBox_ToolFaceSetting.TabIndex = 54;
		this.groupBox_ToolFaceSetting.TabStop = false;
		this.groupBox_ToolFaceSetting.Text = "Tool Face Setting:";
		this.label_ToolFaceSetting.AutoSize = true;
		this.label_ToolFaceSetting.ForeColor = System.Drawing.Color.Black;
		this.label_ToolFaceSetting.Location = new System.Drawing.Point(6, 25);
		this.label_ToolFaceSetting.Name = "label_ToolFaceSetting";
		this.label_ToolFaceSetting.Size = new System.Drawing.Size(33, 20);
		this.label_ToolFaceSetting.TabIndex = 0;
		this.label_ToolFaceSetting.Text = "null";
		this.groupBox_Azimuth.Controls.Add(this.label_AzimuthTurnRate);
		this.groupBox_Azimuth.Controls.Add(this.label_AzimuthTrend);
		this.groupBox_Azimuth.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Azimuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Azimuth.Location = new System.Drawing.Point(3, 139);
		this.groupBox_Azimuth.Name = "groupBox_Azimuth";
		this.groupBox_Azimuth.Size = new System.Drawing.Size(573, 78);
		this.groupBox_Azimuth.TabIndex = 52;
		this.groupBox_Azimuth.TabStop = false;
		this.groupBox_Azimuth.Text = "Azimuth:";
		this.label_AzimuthTurnRate.AutoSize = true;
		this.label_AzimuthTurnRate.ForeColor = System.Drawing.Color.Black;
		this.label_AzimuthTurnRate.Location = new System.Drawing.Point(7, 49);
		this.label_AzimuthTurnRate.Name = "label_AzimuthTurnRate";
		this.label_AzimuthTurnRate.Size = new System.Drawing.Size(33, 20);
		this.label_AzimuthTurnRate.TabIndex = 0;
		this.label_AzimuthTurnRate.Text = "null";
		this.label_AzimuthTrend.AutoSize = true;
		this.label_AzimuthTrend.ForeColor = System.Drawing.Color.Black;
		this.label_AzimuthTrend.Location = new System.Drawing.Point(7, 25);
		this.label_AzimuthTrend.Name = "label_AzimuthTrend";
		this.label_AzimuthTrend.Size = new System.Drawing.Size(33, 20);
		this.label_AzimuthTrend.TabIndex = 0;
		this.label_AzimuthTrend.Text = "null";
		this.groupBox_Inclination.Controls.Add(this.label_InclinationBuildRate);
		this.groupBox_Inclination.Controls.Add(this.label_InclinationTrend);
		this.groupBox_Inclination.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_Inclination.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Inclination.Location = new System.Drawing.Point(3, 217);
		this.groupBox_Inclination.Name = "groupBox_Inclination";
		this.groupBox_Inclination.Size = new System.Drawing.Size(573, 82);
		this.groupBox_Inclination.TabIndex = 51;
		this.groupBox_Inclination.TabStop = false;
		this.groupBox_Inclination.Text = "Inclination:";
		this.label_InclinationBuildRate.AutoSize = true;
		this.label_InclinationBuildRate.ForeColor = System.Drawing.Color.Black;
		this.label_InclinationBuildRate.Location = new System.Drawing.Point(7, 49);
		this.label_InclinationBuildRate.Name = "label_InclinationBuildRate";
		this.label_InclinationBuildRate.Size = new System.Drawing.Size(33, 20);
		this.label_InclinationBuildRate.TabIndex = 0;
		this.label_InclinationBuildRate.Text = "null";
		this.label_InclinationTrend.AutoSize = true;
		this.label_InclinationTrend.ForeColor = System.Drawing.Color.Black;
		this.label_InclinationTrend.Location = new System.Drawing.Point(7, 25);
		this.label_InclinationTrend.Name = "label_InclinationTrend";
		this.label_InclinationTrend.Size = new System.Drawing.Size(33, 20);
		this.label_InclinationTrend.TabIndex = 0;
		this.label_InclinationTrend.Text = "null";
		this.groupBox1.Controls.Add(this.groupBox_Inclination);
		this.groupBox1.Controls.Add(this.groupBox_Azimuth);
		this.groupBox1.Controls.Add(this.groupBox_DoglegSeverity);
		this.groupBox1.Controls.Add(this.groupBox_ToolFaceSetting);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(579, 308);
		this.groupBox1.TabIndex = 55;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Trend analysis summary:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label_AngleChangeLabel);
		base.Name = "Info_TrendAnalysisResults";
		base.Size = new System.Drawing.Size(579, 308);
		this.groupBox_DoglegSeverity.ResumeLayout(false);
		this.groupBox_DoglegSeverity.PerformLayout();
		this.groupBox_ToolFaceSetting.ResumeLayout(false);
		this.groupBox_ToolFaceSetting.PerformLayout();
		this.groupBox_Azimuth.ResumeLayout(false);
		this.groupBox_Azimuth.PerformLayout();
		this.groupBox_Inclination.ResumeLayout(false);
		this.groupBox_Inclination.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
