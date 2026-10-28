using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Report_Info_Distance_3DOF : UserControl
{
	private IContainer components;

	private PictureBox pictureBox1;

	private Info_Distance_3DOF info_Distance = new Info_Distance_3DOF();

	public string Heading
	{
		get
		{
			return info_Distance.Heading;
		}
		set
		{
			info_Distance.Heading = value;
			RefreshImage();
		}
	}

	public RelativePosition Distance
	{
		set
		{
			info_Distance.Distance = value;
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

	private void InitializeComponent()
	{
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(203, 236);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.pictureBox1);
		base.Name = "Report_Info_Distance_3DOF";
		base.Size = new System.Drawing.Size(203, 236);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Report_Info_Distance_3DOF()
	{
		InitializeComponent();
		RefreshImage();
	}

	public void RefreshImage()
	{
		info_Distance.Size = Size;
		info_Distance.Refresh();
		Bitmap bitmap = new Bitmap(Width, Height);
		info_Distance.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), Size));
		pictureBox1.Image = bitmap;
	}

	protected override void OnResize(EventArgs e)
	{
		RefreshImage();
		base.OnResize(e);
	}
}
