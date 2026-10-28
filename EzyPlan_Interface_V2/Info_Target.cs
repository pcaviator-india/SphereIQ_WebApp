using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_Target : UserControl
{
	private IContainer components;

	private GroupBox groupBox_Target;

	private TextBox textBox_TargetEasting;

	private TextBox textBox_TargetNorthing;

	private TextBox textBox_TargetTVD;

	private FlowLayoutPanel flowLayoutPanel1;

	private TableLayoutPanel tableLayoutPanel2;

	private Label label1;

	private TableLayoutPanel tableLayoutPanel3;

	private Label label3;

	private FlowLayoutPanel flowLayoutPanel2;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label2;

	private TextBox textBox1;

	private TableLayoutPanel tableLayoutPanel4;

	private Label label4;

	private Target m_target;

	public Target Target
	{
		get
		{
			return m_target;
		}
		set
		{
			if (value != null)
			{
				m_target = value;
				textBox_TargetEasting.Text = value.Easting.ToString();
				textBox_TargetNorthing.Text = value.Northing.ToString();
				textBox_TargetTVD.Text = value.TVD.ToString();
			}
			else
			{
				textBox_TargetEasting.Text = "";
				textBox_TargetNorthing.Text = "";
				textBox_TargetTVD.Text = "";
			}
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
		this.groupBox_Target = new System.Windows.Forms.GroupBox();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox_TargetEasting = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox_TargetNorthing = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
		this.label4 = new System.Windows.Forms.Label();
		this.textBox_TargetTVD = new System.Windows.Forms.TextBox();
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.groupBox_Target.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
		this.tableLayoutPanel4.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Target.Controls.Add(this.flowLayoutPanel1);
		this.groupBox_Target.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Target.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Target.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Target.Name = "groupBox_Target";
		this.groupBox_Target.Size = new System.Drawing.Size(310, 174);
		this.groupBox_Target.TabIndex = 9;
		this.groupBox_Target.TabStop = false;
		this.groupBox_Target.Text = "Target m):";
		this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
		this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel3);
		this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel4);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.ForeColor = System.Drawing.Color.Black;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 22);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(304, 149);
		this.flowLayoutPanel1.TabIndex = 4;
		this.tableLayoutPanel2.ColumnCount = 2;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.textBox_TargetEasting, 1, 0);
		this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(288, 39);
		this.tableLayoutPanel2.TabIndex = 6;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "Easting:";
		this.textBox_TargetEasting.BackColor = System.Drawing.Color.White;
		this.textBox_TargetEasting.ForeColor = System.Drawing.Color.Black;
		this.textBox_TargetEasting.Location = new System.Drawing.Point(76, 3);
		this.textBox_TargetEasting.Multiline = true;
		this.textBox_TargetEasting.Name = "textBox_TargetEasting";
		this.textBox_TargetEasting.ReadOnly = true;
		this.textBox_TargetEasting.Size = new System.Drawing.Size(199, 26);
		this.textBox_TargetEasting.TabIndex = 2;
		this.textBox_TargetEasting.TabStop = false;
		this.tableLayoutPanel3.ColumnCount = 2;
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
		this.tableLayoutPanel3.Controls.Add(this.textBox_TargetNorthing, 1, 0);
		this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 48);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 1;
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel3.Size = new System.Drawing.Size(288, 39);
		this.tableLayoutPanel3.TabIndex = 6;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 20);
		this.label3.TabIndex = 3;
		this.label3.Text = "Northing:";
		this.textBox_TargetNorthing.BackColor = System.Drawing.Color.White;
		this.textBox_TargetNorthing.ForeColor = System.Drawing.Color.Black;
		this.textBox_TargetNorthing.Location = new System.Drawing.Point(82, 3);
		this.textBox_TargetNorthing.Multiline = true;
		this.textBox_TargetNorthing.Name = "textBox_TargetNorthing";
		this.textBox_TargetNorthing.ReadOnly = true;
		this.textBox_TargetNorthing.Size = new System.Drawing.Size(199, 26);
		this.textBox_TargetNorthing.TabIndex = 2;
		this.textBox_TargetNorthing.TabStop = false;
		this.tableLayoutPanel4.ColumnCount = 2;
		this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
		this.tableLayoutPanel4.Controls.Add(this.textBox_TargetTVD, 1, 0);
		this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 93);
		this.tableLayoutPanel4.Name = "tableLayoutPanel4";
		this.tableLayoutPanel4.RowCount = 1;
		this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel4.Size = new System.Drawing.Size(288, 39);
		this.tableLayoutPanel4.TabIndex = 6;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(3, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(45, 20);
		this.label4.TabIndex = 3;
		this.label4.Text = "TVD:";
		this.textBox_TargetTVD.BackColor = System.Drawing.Color.White;
		this.textBox_TargetTVD.ForeColor = System.Drawing.Color.Black;
		this.textBox_TargetTVD.Location = new System.Drawing.Point(54, 3);
		this.textBox_TargetTVD.Multiline = true;
		this.textBox_TargetTVD.Name = "textBox_TargetTVD";
		this.textBox_TargetTVD.ReadOnly = true;
		this.textBox_TargetTVD.Size = new System.Drawing.Size(199, 26);
		this.textBox_TargetTVD.TabIndex = 2;
		this.textBox_TargetTVD.TabStop = false;
		this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 100);
		this.flowLayoutPanel2.TabIndex = 0;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
		this.tableLayoutPanel1.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(45, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Easting:";
		this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.textBox1.Location = new System.Drawing.Point(76, 3);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(199, 26);
		this.textBox1.TabIndex = 2;
		this.textBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.groupBox_Target);
		base.Name = "Info_Target";
		base.Size = new System.Drawing.Size(310, 174);
		this.groupBox_Target.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		this.tableLayoutPanel4.ResumeLayout(false);
		this.tableLayoutPanel4.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public Info_Target()
	{
		InitializeComponent();
	}
}
