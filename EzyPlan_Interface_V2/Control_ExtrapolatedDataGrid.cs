using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Control_ExtrapolatedDataGrid : UserControl
{
	private IContainer components;

	private DataGridView dataGridView1;

	private GroupBox groupBox1;

	public string SurveyDataName
	{
		get
		{
			return groupBox1.Text;
		}
		set
		{
			groupBox1.Text = value;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.Location = new System.Drawing.Point(3, 25);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(599, 533);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 2;
		this.groupBox1.BackColor = System.Drawing.Color.Transparent;
		this.groupBox1.Controls.Add(this.dataGridView1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(605, 561);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "null";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox1);
		base.Name = "Control_ExtrapolatedDataGrid";
		base.Size = new System.Drawing.Size(605, 561);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Control_ExtrapolatedDataGrid()
	{
		InitializeComponent();
	}

	public void SetAbsoluteSurveyData(AbsoluteSurveyList surveyList)
	{
		if (surveyList != null)
		{
			dataGridView1.DataSource = surveyList.List;
		}
	}
}
