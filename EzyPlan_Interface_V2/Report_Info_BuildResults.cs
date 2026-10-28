using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Report_Info_BuildResults : UserControl
{
	private Info_BuildResults info_BuildResults = new Info_BuildResults();

	private IContainer components;

	private PictureBox pictureBox1;

	public BuildAndHoldResults BuildResults
	{
		set
		{
			info_BuildResults.BuildResults = value;
			RefreshImage();
		}
	}

	public Report_Info_BuildResults()
	{
		InitializeComponent();
		RefreshImage();
	}

	public void RefreshImage()
	{
		info_BuildResults.Size = Size;
		info_BuildResults.Refresh();
		Bitmap bitmap = new Bitmap(Width, Height);
		info_BuildResults.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), Size));
		pictureBox1.Image = bitmap;
	}

	protected override void OnResize(EventArgs e)
	{
		RefreshImage();
		base.OnResize(e);
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
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.pictureBox1.BackColor = System.Drawing.Color.White;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(479, 282);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.pictureBox1);
		base.Name = "Report_Info_BuildResults";
		base.Size = new System.Drawing.Size(479, 282);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
