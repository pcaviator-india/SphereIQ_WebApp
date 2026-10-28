using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_Collar : UserControl
{
	private Collar m_collar;

	private IContainer components;

	private GroupBox groupBox_Collar;

	private FlowLayoutPanel flowLayoutPanel1;

	private TextBox textBox_CollarNorthing;

	private TableLayoutPanel tableLayoutPanel2;

	private Label label1;

	private TextBox textBox_CollarEasting;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label9;

	public Collar Collar
	{
		get
		{
			return m_collar;
		}
		set
		{
			if (value != null)
			{
				m_collar = value;
				textBox_CollarEasting.Text = m_collar.Easting.ToString();
				textBox_CollarNorthing.Text = m_collar.Northing.ToString();
			}
			else
			{
				textBox_CollarEasting.Text = "";
				textBox_CollarNorthing.Text = "";
			}
		}
	}

	public Info_Collar()
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
		this.groupBox_Collar = new System.Windows.Forms.GroupBox();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox_CollarEasting = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label9 = new System.Windows.Forms.Label();
		this.textBox_CollarNorthing = new System.Windows.Forms.TextBox();
		this.groupBox_Collar.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Collar.Controls.Add(this.flowLayoutPanel1);
		this.groupBox_Collar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Collar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox_Collar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Collar.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Collar.Name = "groupBox_Collar";
		this.groupBox_Collar.Size = new System.Drawing.Size(319, 130);
		this.groupBox_Collar.TabIndex = 8;
		this.groupBox_Collar.TabStop = false;
		this.groupBox_Collar.Text = "Collar (m):";
		this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
		this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 22);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(313, 105);
		this.flowLayoutPanel1.TabIndex = 4;
		this.tableLayoutPanel2.ColumnCount = 2;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.textBox_CollarEasting, 1, 0);
		this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(288, 39);
		this.tableLayoutPanel2.TabIndex = 5;
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "Easting:";
		this.textBox_CollarEasting.BackColor = System.Drawing.Color.White;
		this.textBox_CollarEasting.ForeColor = System.Drawing.Color.Black;
		this.textBox_CollarEasting.Location = new System.Drawing.Point(76, 3);
		this.textBox_CollarEasting.Multiline = true;
		this.textBox_CollarEasting.Name = "textBox_CollarEasting";
		this.textBox_CollarEasting.ReadOnly = true;
		this.textBox_CollarEasting.Size = new System.Drawing.Size(203, 26);
		this.textBox_CollarEasting.TabIndex = 2;
		this.textBox_CollarEasting.TabStop = false;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.textBox_CollarNorthing, 1, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 48);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 39);
		this.tableLayoutPanel1.TabIndex = 5;
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.Color.Black;
		this.label9.Location = new System.Drawing.Point(3, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(73, 20);
		this.label9.TabIndex = 3;
		this.label9.Text = "Northing:";
		this.textBox_CollarNorthing.BackColor = System.Drawing.Color.White;
		this.textBox_CollarNorthing.ForeColor = System.Drawing.Color.Black;
		this.textBox_CollarNorthing.Location = new System.Drawing.Point(82, 3);
		this.textBox_CollarNorthing.Multiline = true;
		this.textBox_CollarNorthing.Name = "textBox_CollarNorthing";
		this.textBox_CollarNorthing.ReadOnly = true;
		this.textBox_CollarNorthing.Size = new System.Drawing.Size(203, 26);
		this.textBox_CollarNorthing.TabIndex = 2;
		this.textBox_CollarNorthing.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.groupBox_Collar);
		base.Name = "Info_Collar";
		base.Size = new System.Drawing.Size(319, 130);
		this.groupBox_Collar.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
