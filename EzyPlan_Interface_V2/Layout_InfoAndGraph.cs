using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Layout_InfoAndGraph : UserControl
{
	private IContainer components;

	private Panel panel_DrillHole;

	public Panel panel_Body;

	public Panel panel_Header;

	public Label label_HeaderText1;

	protected int m_designTimeWidth;

	public int DesignTimeWidth => m_designTimeWidth;

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
		this.panel_DrillHole = new System.Windows.Forms.Panel();
		this.panel_Body = new System.Windows.Forms.Panel();
		this.panel_Header = new System.Windows.Forms.Panel();
		this.label_HeaderText1 = new System.Windows.Forms.Label();
		this.panel_DrillHole.SuspendLayout();
		this.panel_Header.SuspendLayout();
		base.SuspendLayout();
		this.panel_DrillHole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_DrillHole.Controls.Add(this.panel_Body);
		this.panel_DrillHole.Controls.Add(this.panel_Header);
		this.panel_DrillHole.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_DrillHole.Location = new System.Drawing.Point(0, 0);
		this.panel_DrillHole.Name = "panel_DrillHole";
		this.panel_DrillHole.Size = new System.Drawing.Size(340, 651);
		this.panel_DrillHole.TabIndex = 6;
		this.panel_Body.AutoScroll = true;
		this.panel_Body.BackColor = System.Drawing.Color.White;
		this.panel_Body.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Body.Location = new System.Drawing.Point(0, 31);
		this.panel_Body.Name = "panel_Body";
		this.panel_Body.Padding = new System.Windows.Forms.Padding(3);
		this.panel_Body.Size = new System.Drawing.Size(338, 618);
		this.panel_Body.TabIndex = 10;
		this.panel_Header.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_Header.Controls.Add(this.label_HeaderText1);
		this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.panel_Header.Location = new System.Drawing.Point(0, 0);
		this.panel_Header.Name = "panel_Header";
		this.panel_Header.Size = new System.Drawing.Size(338, 31);
		this.panel_Header.TabIndex = 8;
		this.label_HeaderText1.AutoSize = true;
		this.label_HeaderText1.Location = new System.Drawing.Point(3, 3);
		this.label_HeaderText1.Name = "label_HeaderText1";
		this.label_HeaderText1.Size = new System.Drawing.Size(37, 20);
		this.label_HeaderText1.TabIndex = 0;
		this.label_HeaderText1.Text = "null";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
		base.Controls.Add(this.panel_DrillHole);
		base.Name = "Layout_InfoAndGraph";
		base.Size = new System.Drawing.Size(340, 651);
		this.panel_DrillHole.ResumeLayout(false);
		this.panel_Header.ResumeLayout(false);
		this.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Layout_InfoAndGraph()
	{
		InitializeComponent();
	}
}
