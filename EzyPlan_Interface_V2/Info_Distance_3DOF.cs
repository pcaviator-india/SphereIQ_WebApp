using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_Distance_3DOF : UserControl
{
	private IContainer components;

	private GroupBox groupBox_Distance;

	private FlowLayoutPanel flowLayoutPanel1;

	private TextBox textBox_Distance_North;

	private TextBox textBox_Distance_Horizontal;

	private TextBox textBox_Distance_East;

	private string m_heading;

	public string Heading
	{
		get
		{
			return m_heading;
		}
		set
		{
			m_heading = value;
			groupBox_Distance.Text = value + " (m):";
		}
	}

	public RelativePosition Distance
	{
		set
		{
			textBox_Distance_North.Text = value.NorthingString;
			textBox_Distance_East.Text = value.EastingString;
			textBox_Distance_Horizontal.Text = value.HorizontalString;
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
		this.groupBox_Distance = new System.Windows.Forms.GroupBox();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.textBox_Distance_North = new System.Windows.Forms.TextBox();
		this.textBox_Distance_East = new System.Windows.Forms.TextBox();
		this.textBox_Distance_Horizontal = new System.Windows.Forms.TextBox();
		this.groupBox_Distance.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Distance.Controls.Add(this.flowLayoutPanel1);
		this.groupBox_Distance.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Distance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Distance.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Distance.Name = "groupBox_Distance";
		this.groupBox_Distance.Size = new System.Drawing.Size(286, 125);
		this.groupBox_Distance.TabIndex = 23;
		this.groupBox_Distance.TabStop = false;
		this.groupBox_Distance.Text = "Heading (m):";
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.flowLayoutPanel1.Controls.Add(this.textBox_Distance_North);
		this.flowLayoutPanel1.Controls.Add(this.textBox_Distance_East);
		this.flowLayoutPanel1.Controls.Add(this.textBox_Distance_Horizontal);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 26);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 96);
		this.flowLayoutPanel1.TabIndex = 5;
		this.textBox_Distance_North.BackColor = System.Drawing.Color.White;
		this.textBox_Distance_North.Dock = System.Windows.Forms.DockStyle.Top;
		this.textBox_Distance_North.ForeColor = System.Drawing.Color.Black;
		this.textBox_Distance_North.Location = new System.Drawing.Point(3, 3);
		this.textBox_Distance_North.Multiline = true;
		this.textBox_Distance_North.Name = "textBox_Distance_North";
		this.textBox_Distance_North.ReadOnly = true;
		this.textBox_Distance_North.Size = new System.Drawing.Size(269, 26);
		this.textBox_Distance_North.TabIndex = 4;
		this.textBox_Distance_North.TabStop = false;
		this.textBox_Distance_East.BackColor = System.Drawing.Color.White;
		this.textBox_Distance_East.Dock = System.Windows.Forms.DockStyle.Top;
		this.textBox_Distance_East.ForeColor = System.Drawing.Color.Black;
		this.textBox_Distance_East.Location = new System.Drawing.Point(3, 35);
		this.textBox_Distance_East.Multiline = true;
		this.textBox_Distance_East.Name = "textBox_Distance_East";
		this.textBox_Distance_East.ReadOnly = true;
		this.textBox_Distance_East.Size = new System.Drawing.Size(269, 26);
		this.textBox_Distance_East.TabIndex = 4;
		this.textBox_Distance_East.TabStop = false;
		this.textBox_Distance_Horizontal.BackColor = System.Drawing.Color.White;
		this.textBox_Distance_Horizontal.Dock = System.Windows.Forms.DockStyle.Top;
		this.textBox_Distance_Horizontal.ForeColor = System.Drawing.Color.Black;
		this.textBox_Distance_Horizontal.Location = new System.Drawing.Point(3, 67);
		this.textBox_Distance_Horizontal.Multiline = true;
		this.textBox_Distance_Horizontal.Name = "textBox_Distance_Horizontal";
		this.textBox_Distance_Horizontal.ReadOnly = true;
		this.textBox_Distance_Horizontal.Size = new System.Drawing.Size(269, 26);
		this.textBox_Distance_Horizontal.TabIndex = 4;
		this.textBox_Distance_Horizontal.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox_Distance);
		base.Name = "Info_Distance_3DOF";
		base.Size = new System.Drawing.Size(286, 125);
		this.groupBox_Distance.ResumeLayout(false);
		this.groupBox_Distance.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public Info_Distance_3DOF()
	{
		InitializeComponent();
	}
}
