using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Info_MinDL : UserControl
{
	private IContainer components;

	private GroupBox groupBox_MinDL;

	private Label label_minDL;

	public string StatusString
	{
		set
		{
			label_minDL.Text = value;
		}
	}

	public Color StatusColor
	{
		set
		{
			label_minDL.ForeColor = value;
		}
	}

	public Info_MinDL()
	{
		InitializeComponent();
		label_minDL.ForeColor = Color.Black;
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
		this.groupBox_MinDL = new System.Windows.Forms.GroupBox();
		this.label_minDL = new System.Windows.Forms.Label();
		this.groupBox_MinDL.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_MinDL.BackColor = System.Drawing.Color.White;
		this.groupBox_MinDL.Controls.Add(this.label_minDL);
		this.groupBox_MinDL.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_MinDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_MinDL.Location = new System.Drawing.Point(0, 0);
		this.groupBox_MinDL.Name = "groupBox_MinDL";
		this.groupBox_MinDL.Size = new System.Drawing.Size(508, 53);
		this.groupBox_MinDL.TabIndex = 0;
		this.groupBox_MinDL.TabStop = false;
		this.groupBox_MinDL.Text = "Minimum dogleg to achieve target (degrees/30m):";
		this.label_minDL.AutoSize = true;
		this.label_minDL.ForeColor = System.Drawing.Color.Black;
		this.label_minDL.Location = new System.Drawing.Point(6, 22);
		this.label_minDL.Name = "label_minDL";
		this.label_minDL.Size = new System.Drawing.Size(33, 20);
		this.label_minDL.TabIndex = 0;
		this.label_minDL.Text = "null";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox_MinDL);
		base.Name = "Info_MinDL";
		base.Size = new System.Drawing.Size(508, 53);
		this.groupBox_MinDL.ResumeLayout(false);
		this.groupBox_MinDL.PerformLayout();
		base.ResumeLayout(false);
	}
}
