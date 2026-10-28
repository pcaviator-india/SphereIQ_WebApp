using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_Chart : Report_Template_Page
{
	private IContainer components;

	private PictureBox pictureBox_Chart;

	public Image Chart
	{
		get
		{
			return pictureBox_Chart.Image;
		}
		set
		{
			pictureBox_Chart.Image = value;
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
		this.pictureBox_Chart = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox_Chart).BeginInit();
		base.SuspendLayout();
		this.pictureBox_Chart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox_Chart.Location = new System.Drawing.Point(0, 0);
		this.pictureBox_Chart.Name = "pictureBox_Chart";
		this.pictureBox_Chart.Size = new System.Drawing.Size(600, 715);
		this.pictureBox_Chart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox_Chart.TabIndex = 0;
		this.pictureBox_Chart.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Controls.Add(this.pictureBox_Chart);
		base.Name = "Report_Chart";
		((System.ComponentModel.ISupportInitialize)this.pictureBox_Chart).EndInit();
		base.ResumeLayout(false);
	}

	public Report_Chart()
	{
		InitializeComponent();
	}

	public Report_Chart(Bitmap chart)
		: this()
	{
		Chart = chart;
	}
}
