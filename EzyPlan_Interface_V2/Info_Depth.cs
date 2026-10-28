using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_Depth : UserControl
{
	private string m_heading;

	private AbsoluteSurvey m_absoluteSurvey;

	private IContainer components;

	private GroupBox groupBox_Depth;

	private TableLayoutPanel tableLayoutPanel1;

	private TextBox textBox_HoleMD;

	private Label label9;

	private TableLayoutPanel tableLayoutPanel2;

	private Label label1;

	private TextBox textBox_HoleTVD;

	public string Heading
	{
		get
		{
			return m_heading;
		}
		set
		{
			m_heading = value;
			groupBox_Depth.Text = value + " (m):";
		}
	}

	public AbsoluteSurvey AbsoluteSurvey
	{
		get
		{
			return m_absoluteSurvey;
		}
		set
		{
			if (value != null)
			{
				m_absoluteSurvey = value;
				textBox_HoleMD.Text = m_absoluteSurvey.MD.ToString();
				textBox_HoleTVD.Text = m_absoluteSurvey.TVD.ToString();
			}
			else
			{
				textBox_HoleMD.Text = "";
				textBox_HoleTVD.Text = "";
			}
		}
	}

	public Info_Depth()
	{
		InitializeComponent();
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
		this.groupBox_Depth = new System.Windows.Forms.GroupBox();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.textBox_HoleMD = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox_HoleTVD = new System.Windows.Forms.TextBox();
		this.groupBox_Depth.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Depth.BackColor = System.Drawing.Color.White;
		this.groupBox_Depth.Controls.Add(this.tableLayoutPanel1);
		this.groupBox_Depth.Controls.Add(this.tableLayoutPanel2);
		this.groupBox_Depth.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Depth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Depth.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Depth.Name = "groupBox_Depth";
		this.groupBox_Depth.Size = new System.Drawing.Size(646, 70);
		this.groupBox_Depth.TabIndex = 33;
		this.groupBox_Depth.TabStop = false;
		this.groupBox_Depth.Text = "heading (m):";
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.textBox_HoleMD, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 25);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 33);
		this.tableLayoutPanel1.TabIndex = 8;
		this.textBox_HoleMD.BackColor = System.Drawing.Color.White;
		this.textBox_HoleMD.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBox_HoleMD.ForeColor = System.Drawing.Color.Black;
		this.textBox_HoleMD.Location = new System.Drawing.Point(47, 3);
		this.textBox_HoleMD.Multiline = true;
		this.textBox_HoleMD.Name = "textBox_HoleMD";
		this.textBox_HoleMD.ReadOnly = true;
		this.textBox_HoleMD.Size = new System.Drawing.Size(238, 27);
		this.textBox_HoleMD.TabIndex = 2;
		this.textBox_HoleMD.TabStop = false;
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.Color.Black;
		this.label9.Location = new System.Drawing.Point(3, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(38, 20);
		this.label9.TabIndex = 3;
		this.label9.Text = "MD:";
		this.tableLayoutPanel2.ColumnCount = 2;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.textBox_HoleTVD, 1, 0);
		this.tableLayoutPanel2.Location = new System.Drawing.Point(305, 25);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(288, 33);
		this.tableLayoutPanel2.TabIndex = 9;
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(45, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "TVD:";
		this.textBox_HoleTVD.BackColor = System.Drawing.Color.White;
		this.textBox_HoleTVD.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBox_HoleTVD.ForeColor = System.Drawing.Color.Black;
		this.textBox_HoleTVD.Location = new System.Drawing.Point(54, 3);
		this.textBox_HoleTVD.Multiline = true;
		this.textBox_HoleTVD.Name = "textBox_HoleTVD";
		this.textBox_HoleTVD.ReadOnly = true;
		this.textBox_HoleTVD.Size = new System.Drawing.Size(231, 27);
		this.textBox_HoleTVD.TabIndex = 2;
		this.textBox_HoleTVD.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox_Depth);
		base.Name = "Info_Depth";
		base.Size = new System.Drawing.Size(646, 70);
		this.groupBox_Depth.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
