using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Layout_Body_Wizard : UserControl
{
	private IContainer components;

	public Label label_headerText1;

	public GroupBox groupBox_frame;

	public Panel panel_buttonPanel;

	public Panel panel_body;

	public Panel panel_header;

	public Panel panel_footer;

	public Label label_headerText2;

	public Panel panel_scrollPanel;

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
		this.panel_header = new System.Windows.Forms.Panel();
		this.label_headerText2 = new System.Windows.Forms.Label();
		this.label_headerText1 = new System.Windows.Forms.Label();
		this.panel_buttonPanel = new System.Windows.Forms.Panel();
		this.panel_footer = new System.Windows.Forms.Panel();
		this.panel_body = new System.Windows.Forms.Panel();
		this.groupBox_frame = new System.Windows.Forms.GroupBox();
		this.panel_scrollPanel = new System.Windows.Forms.Panel();
		this.panel_header.SuspendLayout();
		this.panel_footer.SuspendLayout();
		this.panel_body.SuspendLayout();
		this.groupBox_frame.SuspendLayout();
		base.SuspendLayout();
		this.panel_header.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_header.Controls.Add(this.label_headerText2);
		this.panel_header.Controls.Add(this.label_headerText1);
		this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panel_header.Location = new System.Drawing.Point(0, 0);
		this.panel_header.Name = "panel_header";
		this.panel_header.Size = new System.Drawing.Size(554, 52);
		this.panel_header.TabIndex = 0;
		this.label_headerText2.AutoSize = true;
		this.label_headerText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_headerText2.Location = new System.Drawing.Point(3, 25);
		this.label_headerText2.Name = "label_headerText2";
		this.label_headerText2.Size = new System.Drawing.Size(37, 20);
		this.label_headerText2.TabIndex = 2;
		this.label_headerText2.Text = "null";
		this.label_headerText1.AutoSize = true;
		this.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_headerText1.Location = new System.Drawing.Point(3, 0);
		this.label_headerText1.Name = "label_headerText1";
		this.label_headerText1.Size = new System.Drawing.Size(37, 20);
		this.label_headerText1.TabIndex = 2;
		this.label_headerText1.Text = "null";
		this.panel_buttonPanel.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel_buttonPanel.Location = new System.Drawing.Point(272, 0);
		this.panel_buttonPanel.Name = "panel_buttonPanel";
		this.panel_buttonPanel.Size = new System.Drawing.Size(282, 41);
		this.panel_buttonPanel.TabIndex = 0;
		this.panel_footer.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_footer.Controls.Add(this.panel_buttonPanel);
		this.panel_footer.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_footer.Location = new System.Drawing.Point(0, 593);
		this.panel_footer.Name = "panel_footer";
		this.panel_footer.Size = new System.Drawing.Size(554, 41);
		this.panel_footer.TabIndex = 2;
		this.panel_body.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_body.Controls.Add(this.groupBox_frame);
		this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_body.Location = new System.Drawing.Point(0, 52);
		this.panel_body.Name = "panel_body";
		this.panel_body.Padding = new System.Windows.Forms.Padding(7);
		this.panel_body.Size = new System.Drawing.Size(554, 541);
		this.panel_body.TabIndex = 1;
		this.groupBox_frame.BackColor = System.Drawing.Color.White;
		this.groupBox_frame.Controls.Add(this.panel_scrollPanel);
		this.groupBox_frame.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_frame.Location = new System.Drawing.Point(7, 7);
		this.groupBox_frame.Name = "groupBox_frame";
		this.groupBox_frame.Size = new System.Drawing.Size(538, 525);
		this.groupBox_frame.TabIndex = 0;
		this.groupBox_frame.TabStop = false;
		this.panel_scrollPanel.AutoScroll = true;
		this.panel_scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_scrollPanel.Location = new System.Drawing.Point(3, 16);
		this.panel_scrollPanel.Name = "panel_scrollPanel";
		this.panel_scrollPanel.Size = new System.Drawing.Size(532, 506);
		this.panel_scrollPanel.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.panel_body);
		base.Controls.Add(this.panel_footer);
		base.Controls.Add(this.panel_header);
		base.Name = "Layout_Body_Wizard";
		base.Size = new System.Drawing.Size(554, 634);
		this.panel_header.ResumeLayout(false);
		this.panel_header.PerformLayout();
		this.panel_footer.ResumeLayout(false);
		this.panel_body.ResumeLayout(false);
		this.groupBox_frame.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Layout_Body_Wizard()
	{
		InitializeComponent();
	}
}
