using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;
using EzyReport;

namespace EzyPlan_Interface_V2;

public class Report_Info_Survey : UserControl, IReportComponent
{
	private IContainer components;

	private PictureBox pictureBox1;

	private Info_Survey info_Survey = new Info_Survey();

	public AbsoluteSurvey Survey
	{
		set
		{
			info_Survey.Survey = value;
			RefreshImage();
		}
	}

	public string Heading
	{
		get
		{
			return info_Survey.Heading;
		}
		set
		{
			info_Survey.Heading = value;
			RefreshImage();
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

	public void DrawToBitmap(out Bitmap bitmap)
	{
		bitmap = new Bitmap(1, 1);
	}

	private void InitializeComponent()
	{
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.pictureBox1.BackColor = System.Drawing.Color.White;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(617, 80);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.WaitOnLoad = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.pictureBox1);
		base.Name = "Report_Info_Survey";
		base.Size = new System.Drawing.Size(617, 80);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Report_Info_Survey()
	{
		InitializeComponent();
		RefreshImage();
	}

	public void RefreshImage()
	{
		info_Survey.Size = Size;
		info_Survey.Refresh();
		Bitmap bitmap = new Bitmap(Width, Height);
		info_Survey.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), Size));
		pictureBox1.Image = bitmap;
	}

	protected override void OnResize(EventArgs e)
	{
		RefreshImage();
		base.OnResize(e);
	}

	protected override void OnMove(EventArgs e)
	{
		base.OnMove(e);
		RefreshImage();
	}
}
