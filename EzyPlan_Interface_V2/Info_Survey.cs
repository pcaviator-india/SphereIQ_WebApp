using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_Survey : UserControl
{
	private IContainer components;

	private GroupBox groupBox_Survey;

	private TableLayoutPanel tableLayoutPanel1;

	private TextBox textBox_Northing;

	private TextBox textBox_Easting;

	private TextBox textBox_Azimuth;

	private TextBox textBox_TVD;

	private TextBox textBox_Dip;

	private Label label_Dip;

	private Label label_Easting;

	private Label label_TVD;

	private Label label_Azimuth;

	private Label label_Northing;

	private TextBox textBox_MD;

	private Label label_MD;

	public string Heading
	{
		get
		{
			return groupBox_Survey.Text;
		}
		set
		{
			groupBox_Survey.Text = value;
		}
	}

	public AbsoluteSurvey Survey
	{
		set
		{
			textBox_MD.Text = value.MD.ToString();
			textBox_Dip.Text = value.Dip.ToString();
			textBox_Azimuth.Text = value.Azimuth.ToString();
			textBox_TVD.Text = value.TVD.ToString();
			textBox_Northing.Text = value.Northing.ToString();
			textBox_Easting.Text = value.Easting.ToString();
		}
	}

	public Info_Survey()
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
		this.groupBox_Survey = new System.Windows.Forms.GroupBox();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.textBox_Northing = new System.Windows.Forms.TextBox();
		this.textBox_Easting = new System.Windows.Forms.TextBox();
		this.textBox_Azimuth = new System.Windows.Forms.TextBox();
		this.textBox_TVD = new System.Windows.Forms.TextBox();
		this.textBox_Dip = new System.Windows.Forms.TextBox();
		this.label_Dip = new System.Windows.Forms.Label();
		this.label_Easting = new System.Windows.Forms.Label();
		this.label_TVD = new System.Windows.Forms.Label();
		this.label_Azimuth = new System.Windows.Forms.Label();
		this.label_Northing = new System.Windows.Forms.Label();
		this.textBox_MD = new System.Windows.Forms.TextBox();
		this.label_MD = new System.Windows.Forms.Label();
		this.groupBox_Survey.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Survey.Controls.Add(this.tableLayoutPanel1);
		this.groupBox_Survey.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Survey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Survey.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Survey.Name = "groupBox_Survey";
		this.groupBox_Survey.Padding = new System.Windows.Forms.Padding(5);
		this.groupBox_Survey.Size = new System.Drawing.Size(576, 94);
		this.groupBox_Survey.TabIndex = 25;
		this.groupBox_Survey.TabStop = false;
		this.groupBox_Survey.Text = "heading";
		this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanel1.ColumnCount = 6;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.textBox_Northing, 4, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox_Easting, 5, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox_Azimuth, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox_TVD, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox_Dip, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.label_Dip, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label_Easting, 5, 0);
		this.tableLayoutPanel1.Controls.Add(this.label_TVD, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.label_Azimuth, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.label_Northing, 4, 0);
		this.tableLayoutPanel1.Controls.Add(this.textBox_MD, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.label_MD, 0, 0);
		this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
		this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 27);
		this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(554, 167);
		this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(554, 53);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 53);
		this.tableLayoutPanel1.TabIndex = 4;
		this.textBox_Northing.BackColor = System.Drawing.Color.White;
		this.textBox_Northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Northing.Location = new System.Drawing.Point(334, 23);
		this.textBox_Northing.Multiline = true;
		this.textBox_Northing.Name = "textBox_Northing";
		this.textBox_Northing.ReadOnly = true;
		this.textBox_Northing.Size = new System.Drawing.Size(104, 20);
		this.textBox_Northing.TabIndex = 14;
		this.textBox_Northing.TabStop = false;
		this.textBox_Northing.Text = "00000000.00";
		this.textBox_Easting.BackColor = System.Drawing.Color.White;
		this.textBox_Easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Easting.Location = new System.Drawing.Point(444, 23);
		this.textBox_Easting.Multiline = true;
		this.textBox_Easting.Name = "textBox_Easting";
		this.textBox_Easting.ReadOnly = true;
		this.textBox_Easting.Size = new System.Drawing.Size(104, 20);
		this.textBox_Easting.TabIndex = 11;
		this.textBox_Easting.TabStop = false;
		this.textBox_Easting.Text = "000000.00";
		this.textBox_Azimuth.BackColor = System.Drawing.Color.White;
		this.textBox_Azimuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Azimuth.Location = new System.Drawing.Point(173, 23);
		this.textBox_Azimuth.Multiline = true;
		this.textBox_Azimuth.Name = "textBox_Azimuth";
		this.textBox_Azimuth.ReadOnly = true;
		this.textBox_Azimuth.Size = new System.Drawing.Size(74, 20);
		this.textBox_Azimuth.TabIndex = 13;
		this.textBox_Azimuth.TabStop = false;
		this.textBox_Azimuth.Text = "000.00";
		this.textBox_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_TVD.Location = new System.Drawing.Point(253, 23);
		this.textBox_TVD.Multiline = true;
		this.textBox_TVD.Name = "textBox_TVD";
		this.textBox_TVD.ReadOnly = true;
		this.textBox_TVD.Size = new System.Drawing.Size(75, 20);
		this.textBox_TVD.TabIndex = 12;
		this.textBox_TVD.TabStop = false;
		this.textBox_TVD.Text = "0000.00";
		this.textBox_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Dip.Location = new System.Drawing.Point(94, 23);
		this.textBox_Dip.Multiline = true;
		this.textBox_Dip.Name = "textBox_Dip";
		this.textBox_Dip.ReadOnly = true;
		this.textBox_Dip.Size = new System.Drawing.Size(73, 20);
		this.textBox_Dip.TabIndex = 15;
		this.textBox_Dip.TabStop = false;
		this.textBox_Dip.Text = "00.00";
		this.label_Dip.AutoSize = true;
		this.label_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Dip.Location = new System.Drawing.Point(94, 0);
		this.label_Dip.Name = "label_Dip";
		this.label_Dip.Size = new System.Drawing.Size(33, 20);
		this.label_Dip.TabIndex = 9;
		this.label_Dip.Text = "Dip";
		this.label_Easting.AutoSize = true;
		this.label_Easting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Easting.Location = new System.Drawing.Point(444, 0);
		this.label_Easting.Name = "label_Easting";
		this.label_Easting.Size = new System.Drawing.Size(63, 20);
		this.label_Easting.TabIndex = 4;
		this.label_Easting.Text = "Easting";
		this.label_TVD.AutoSize = true;
		this.label_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TVD.Location = new System.Drawing.Point(253, 0);
		this.label_TVD.Name = "label_TVD";
		this.label_TVD.Size = new System.Drawing.Size(41, 20);
		this.label_TVD.TabIndex = 5;
		this.label_TVD.Text = "TVD";
		this.label_Azimuth.AutoSize = true;
		this.label_Azimuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Azimuth.Location = new System.Drawing.Point(173, 0);
		this.label_Azimuth.Name = "label_Azimuth";
		this.label_Azimuth.Size = new System.Drawing.Size(67, 20);
		this.label_Azimuth.TabIndex = 6;
		this.label_Azimuth.Text = "Azimuth";
		this.label_Northing.AutoSize = true;
		this.label_Northing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Northing.Location = new System.Drawing.Point(334, 0);
		this.label_Northing.Name = "label_Northing";
		this.label_Northing.Size = new System.Drawing.Size(69, 20);
		this.label_Northing.TabIndex = 7;
		this.label_Northing.Text = "Northing";
		this.textBox_MD.BackColor = System.Drawing.Color.White;
		this.textBox_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_MD.Location = new System.Drawing.Point(3, 23);
		this.textBox_MD.Multiline = true;
		this.textBox_MD.Name = "textBox_MD";
		this.textBox_MD.ReadOnly = true;
		this.textBox_MD.Size = new System.Drawing.Size(85, 20);
		this.textBox_MD.TabIndex = 10;
		this.textBox_MD.TabStop = false;
		this.textBox_MD.Text = "00000.00";
		this.label_MD.AutoSize = true;
		this.label_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_MD.Location = new System.Drawing.Point(3, 0);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(34, 20);
		this.label_MD.TabIndex = 8;
		this.label_MD.Text = "MD";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox_Survey);
		this.DoubleBuffered = true;
		base.Name = "Info_Survey";
		base.Size = new System.Drawing.Size(576, 94);
		this.groupBox_Survey.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
