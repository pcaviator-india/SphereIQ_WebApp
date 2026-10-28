using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Report_Info_MinDL : UserControl
{
	private IContainer components;

	private PictureBox pictureBox1;

	private Info_MinDL info_minDL = new Info_MinDL();

	public string StatusString
	{
		set
		{
			info_minDL.StatusString = value;
			RefreshImage();
		}
	}

	public Color StatusColor
	{
		set
		{
			info_minDL.StatusColor = value;
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
		this.pictureBox1.BackColor = System.Drawing.Color.White;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(551, 73);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.pictureBox1);
		base.Name = "Report_Info_MinDL";
		base.Size = new System.Drawing.Size(551, 73);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Report_Info_MinDL()
	{
		InitializeComponent();
		RefreshImage();
	}

	public void RefreshImage()
	{
		info_minDL.Size = Size;
		info_minDL.Refresh();
		Bitmap bitmap = new Bitmap(Width, Height);
		info_minDL.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), Size));
		pictureBox1.Image = bitmap;
	}

	protected override void OnResize(EventArgs e)
	{
		RefreshImage();
		base.OnResize(e);
	}
}
