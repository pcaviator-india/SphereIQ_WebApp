using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavCharting;
using EzyNavMath;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_TrendAnalysis_SelectRange : Layout_Body_Wizard
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Select,
			Cancel
		}

		private ButtonPressedEnum m_buttonPressed;

		private TrendAnalysisResults m_trendAnalysisResults;

		private SortableBindingList<AbsoluteSurvey> m_analysedSurveys;

		public ButtonPressedEnum ButtonPressed
		{
			get
			{
				return m_buttonPressed;
			}
			set
			{
				m_buttonPressed = value;
			}
		}

		public TrendAnalysisResults TrendAnalysisResults
		{
			get
			{
				return m_trendAnalysisResults;
			}
			set
			{
				m_trendAnalysisResults = value;
			}
		}

		public SortableBindingList<AbsoluteSurvey> AnalysedSurveys
		{
			get
			{
				return m_analysedSurveys;
			}
			set
			{
				m_analysedSurveys = value;
			}
		}
	}

	private IContainer components;

	private DataGridView dataGridView1;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Cancel;

	private TabControl tabControl_Analysis;

	private TabPage tabPage_Select;

	private TabPage tabPage_Chart;

	private EzyNavHoleChartController ezyNavHoleChartController1;

	private Panel panel_ScrollChart;

	private Panel panel_ScollPanel;

	private GroupBox groupBox_ChartControlBorder;

	private Panel panel_SevereDL;

	private Panel panel_ExtremeDL;

	private Label label2;

	private Label label1;

	private Panel panel1;

	private DrillHoleFile m_drillHoleFile;

	private EzyNavHoleChart m_chart;

	private decimal m_severeDL;

	private decimal m_extremeDL;

	public event EventHandler ButtonPressed;

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
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.tabControl_Analysis = new System.Windows.Forms.TabControl();
		this.tabPage_Select = new System.Windows.Forms.TabPage();
		this.tabPage_Chart = new System.Windows.Forms.TabPage();
		this.panel_ScrollChart = new System.Windows.Forms.Panel();
		this.groupBox_ChartControlBorder = new System.Windows.Forms.GroupBox();
		this.panel_ScollPanel = new System.Windows.Forms.Panel();
		this.ezyNavHoleChartController1 = new EzyNavCharting.EzyNavHoleChartController();
		this.panel_SevereDL = new System.Windows.Forms.Panel();
		this.panel_ExtremeDL = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.tabControl_Analysis.SuspendLayout();
		this.tabPage_Select.SuspendLayout();
		this.tabPage_Chart.SuspendLayout();
		this.panel_ScrollChart.SuspendLayout();
		this.groupBox_ChartControlBorder.SuspendLayout();
		this.panel_ScollPanel.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(337, 24);
		base.label_headerText1.Text = "Select the range of data to analyse:";
		base.groupBox_frame.Size = new System.Drawing.Size(596, 601);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Location = new System.Drawing.Point(311, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(299, 57);
		base.panel_body.Location = new System.Drawing.Point(0, 62);
		base.panel_body.Size = new System.Drawing.Size(610, 615);
		base.panel_body.TabIndex = 0;
		base.panel_header.Controls.Add(this.panel1);
		base.panel_header.Controls.Add(this.panel_SevereDL);
		base.panel_header.Controls.Add(this.panel_ExtremeDL);
		base.panel_header.Controls.Add(this.label2);
		base.panel_header.Controls.Add(this.label1);
		base.panel_header.Size = new System.Drawing.Size(610, 62);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.label1, 0);
		base.panel_header.Controls.SetChildIndex(this.label2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_ExtremeDL, 0);
		base.panel_header.Controls.SetChildIndex(this.panel_SevereDL, 0);
		base.panel_header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 677);
		base.panel_footer.Size = new System.Drawing.Size(610, 57);
		base.panel_footer.TabIndex = 1;
		base.label_headerText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText2.Location = new System.Drawing.Point(6, 29);
		base.label_headerText2.Size = new System.Drawing.Size(44, 24);
		base.label_headerText2.Visible = false;
		base.panel_scrollPanel.Controls.Add(this.tabControl_Analysis);
		base.panel_scrollPanel.Size = new System.Drawing.Size(590, 582);
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
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
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
		this.dataGridView1.Size = new System.Drawing.Size(576, 539);
		this.dataGridView1.StandardTab = true;
		this.dataGridView1.TabIndex = 1;
		this.button_Next.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Next.AutoEllipsis = true;
		this.button_Next.BackColor = System.Drawing.Color.Transparent;
		this.button_Next.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Next.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorOn = System.Drawing.Color.Orange;
		this.button_Next.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Next.ColorPress = System.Drawing.Color.Orange;
		this.button_Next.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Next.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Next.FadingSpeed = 35;
		this.button_Next.FlatAppearance.BorderSize = 0;
		this.button_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Next.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Next.Image = EzyPlan_Interface_V2.Properties.Resources.forward;
		this.button_Next.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Next.ImageOffset = 0;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(16, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(134, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 2;
		this.button_Next.Text = "Next";
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.button_Cancel.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Cancel.AutoEllipsis = true;
		this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
		this.button_Cancel.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Cancel.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorOn = System.Drawing.Color.Orange;
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Cancel.ColorPress = System.Drawing.Color.Orange;
		this.button_Cancel.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Cancel.FadingSpeed = 35;
		this.button_Cancel.FlatAppearance.BorderSize = 0;
		this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Cancel.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Stop_Document;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 0;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(156, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(134, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 3;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.tabControl_Analysis.Controls.Add(this.tabPage_Select);
		this.tabControl_Analysis.Controls.Add(this.tabPage_Chart);
		this.tabControl_Analysis.Cursor = System.Windows.Forms.Cursors.Hand;
		this.tabControl_Analysis.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl_Analysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl_Analysis.Location = new System.Drawing.Point(0, 0);
		this.tabControl_Analysis.Name = "tabControl_Analysis";
		this.tabControl_Analysis.SelectedIndex = 0;
		this.tabControl_Analysis.Size = new System.Drawing.Size(590, 582);
		this.tabControl_Analysis.TabIndex = 0;
		this.tabControl_Analysis.Click += new System.EventHandler(this.tabControl_Analysis_Click);
		this.tabPage_Select.Controls.Add(this.dataGridView1);
		this.tabPage_Select.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Select.Name = "tabPage_Select";
		this.tabPage_Select.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Select.Size = new System.Drawing.Size(582, 545);
		this.tabPage_Select.TabIndex = 0;
		this.tabPage_Select.Text = "Select";
		this.tabPage_Select.UseVisualStyleBackColor = true;
		this.tabPage_Chart.Controls.Add(this.panel_ScrollChart);
		this.tabPage_Chart.Location = new System.Drawing.Point(4, 33);
		this.tabPage_Chart.Name = "tabPage_Chart";
		this.tabPage_Chart.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_Chart.Size = new System.Drawing.Size(526, 445);
		this.tabPage_Chart.TabIndex = 1;
		this.tabPage_Chart.Text = "Chart Options";
		this.tabPage_Chart.UseVisualStyleBackColor = true;
		this.panel_ScrollChart.AutoScroll = true;
		this.panel_ScrollChart.Controls.Add(this.groupBox_ChartControlBorder);
		this.panel_ScrollChart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScrollChart.Location = new System.Drawing.Point(3, 3);
		this.panel_ScrollChart.Name = "panel_ScrollChart";
		this.panel_ScrollChart.Size = new System.Drawing.Size(520, 439);
		this.panel_ScrollChart.TabIndex = 2;
		this.groupBox_ChartControlBorder.Controls.Add(this.panel_ScollPanel);
		this.groupBox_ChartControlBorder.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupBox_ChartControlBorder.Location = new System.Drawing.Point(0, 0);
		this.groupBox_ChartControlBorder.Name = "groupBox_ChartControlBorder";
		this.groupBox_ChartControlBorder.Size = new System.Drawing.Size(382, 439);
		this.groupBox_ChartControlBorder.TabIndex = 3;
		this.groupBox_ChartControlBorder.TabStop = false;
		this.panel_ScollPanel.AutoScroll = true;
		this.panel_ScollPanel.AutoScrollMinSize = new System.Drawing.Size(0, 460);
		this.panel_ScollPanel.Controls.Add(this.ezyNavHoleChartController1);
		this.panel_ScollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_ScollPanel.Location = new System.Drawing.Point(3, 25);
		this.panel_ScollPanel.Name = "panel_ScollPanel";
		this.panel_ScollPanel.Size = new System.Drawing.Size(376, 411);
		this.panel_ScollPanel.TabIndex = 2;
		this.ezyNavHoleChartController1.BackColor = System.Drawing.Color.Transparent;
		this.ezyNavHoleChartController1.Cursor = System.Windows.Forms.Cursors.Default;
		this.ezyNavHoleChartController1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ezyNavHoleChartController1.Location = new System.Drawing.Point(0, 0);
		this.ezyNavHoleChartController1.Name = "ezyNavHoleChartController1";
		this.ezyNavHoleChartController1.Size = new System.Drawing.Size(359, 460);
		this.ezyNavHoleChartController1.TabIndex = 1;
		this.panel_SevereDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_SevereDL.Location = new System.Drawing.Point(131, 29);
		this.panel_SevereDL.Name = "panel_SevereDL";
		this.panel_SevereDL.Size = new System.Drawing.Size(71, 27);
		this.panel_SevereDL.TabIndex = 7;
		this.panel_ExtremeDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_ExtremeDL.Location = new System.Drawing.Point(344, 29);
		this.panel_ExtremeDL.Name = "panel_ExtremeDL";
		this.panel_ExtremeDL.Size = new System.Drawing.Size(79, 27);
		this.panel_ExtremeDL.TabIndex = 8;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(215, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(123, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "Extreme Dogleg:";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(8, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(117, 16);
		this.label1.TabIndex = 6;
		this.label1.Text = "Severe Dogleg:";
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Trend_Analysis;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(546, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 62);
		this.panel1.TabIndex = 10;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "Body_TrendAnalysis_SelectRange";
		base.Size = new System.Drawing.Size(610, 734);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.tabControl_Analysis.ResumeLayout(false);
		this.tabPage_Select.ResumeLayout(false);
		this.tabPage_Chart.ResumeLayout(false);
		this.panel_ScrollChart.ResumeLayout(false);
		this.groupBox_ChartControlBorder.ResumeLayout(false);
		this.panel_ScollPanel.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_TrendAnalysis_SelectRange(DrillHoleFile drillHoleFile, EzyNavHoleChart ezyNavChart, decimal severeDL, decimal extremeDL)
	{
		InitializeComponent();
		m_designTimeWidth = Width;
		m_drillHoleFile = drillHoleFile;
		m_chart = ezyNavChart;
		m_severeDL = severeDL;
		m_extremeDL = extremeDL;
		Visible = false;
	}

	public void Display(bool refreshDataGrid, bool resetChart)
	{
		SetDLSeverityKey();
		tabControl_Analysis.SelectedIndex = 0;
		dataGridView1.Focus();
		Visible = true;
		Focus();
		if (refreshDataGrid)
		{
			UpdateDataGrid();
		}
		else if (dataGridView1.DataSource == null)
		{
			UpdateDataGrid();
		}
		if (resetChart)
		{
			m_chart.ResetExtrapolated();
		}
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
		ezyNavHoleChartController1.SetEzyNavChart(ref m_chart);
		dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
		ezyNavHoleChartController1.RemoveEzyNavChart();
		dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
	}

	private void SetDLSeverityKey()
	{
		panel_SevereDL.BackColor = Utility.m_SevereColor;
		panel_ExtremeDL.BackColor = Utility.m_ExtremeColor;
	}

	public void UpdateDataGrid()
	{
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		NavMath navMath = new NavMath();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		dataGridView1.DataSource = absoluteSurveyList.List;
		absoluteSurveyList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		dataGridView1.DataSource = absoluteSurveyList.List;
		ColourCodeSurveys(dataGridView1);
	}

	private void ColourCodeSurveys(DataGridView dataGridView)
	{
		foreach (DataGridViewRow item in (IEnumerable)dataGridView.Rows)
		{
			Utility.StringToDecimal(item.Cells[6].Value.ToString(), out var number);
			if (number >= m_extremeDL)
			{
				item.DefaultCellStyle.BackColor = Utility.m_ExtremeColor;
			}
			else if (number >= m_severeDL)
			{
				item.DefaultCellStyle.BackColor = Utility.m_SevereColor;
			}
		}
	}

	private void AnalyseRange()
	{
		NavMath.TrendAnalysisEnum trendAnalysisEnum = TrendAnalysis(out var analysisResults, out var analysedSurveys);
		switch (trendAnalysisEnum)
		{
		case NavMath.TrendAnalysisEnum.NotEnoughSurveys:
			DisplayErrorPopup(trendAnalysisEnum);
			break;
		case NavMath.TrendAnalysisEnum.NullParameter:
			DisplayErrorPopup(trendAnalysisEnum);
			break;
		case NavMath.TrendAnalysisEnum.Success:
			DisplayResults(analysisResults, analysedSurveys);
			break;
		case NavMath.TrendAnalysisEnum.TargetTVDCannotBeAchieved:
			DisplayErrorPopup(trendAnalysisEnum);
			DisplayResults(analysisResults, analysedSurveys);
			break;
		case NavMath.TrendAnalysisEnum.StraightPath:
			DisplayErrorPopup(NavMath.TrendAnalysisEnum.StraightPath);
			DisplayResults(analysisResults, analysedSurveys);
			break;
		case NavMath.TrendAnalysisEnum.TrendCannotBeAchieved:
			DisplayErrorPopup(trendAnalysisEnum);
			break;
		case NavMath.TrendAnalysisEnum.UnknownError:
			DisplayErrorPopup(trendAnalysisEnum);
			break;
		case NavMath.TrendAnalysisEnum.UnknownException:
			DisplayErrorPopup(trendAnalysisEnum);
			break;
		}
	}

	private void DisplayResults(TrendAnalysisResults analysisResults, SortableBindingList<AbsoluteSurvey> analysedSurveys)
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Select;
		e.TrendAnalysisResults = analysisResults;
		e.AnalysedSurveys = analysedSurveys;
		OnButtonPressed(e);
	}

	private void DisplayErrorPopup(NavMath.TrendAnalysisEnum result)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Trend analysis error!", NavMath.ToHumanReadable(result) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private NavMath.TrendAnalysisEnum TrendAnalysis(out TrendAnalysisResults analysisResults, out SortableBindingList<AbsoluteSurvey> analysedSurveys)
	{
		NavMath navMath = new NavMath();
		AbsoluteSurveyList absoluteSurveyList = new AbsoluteSurveyList();
		navMath.AbsoluteSurvey(m_drillHoleFile.Collar, m_drillHoleFile.SurveyList, ref absoluteSurveyList);
		AbsoluteSurvey endOfRealHoleSurvey = absoluteSurveyList.LastRecord();
		GetSelectedSurveys(out analysedSurveys);
		SortSurveysByMD(ref analysedSurveys);
		analysisResults = new TrendAnalysisResults();
		NavMath.TrendAnalysisEnum result = navMath.TrendAnalysis(m_drillHoleFile.Target, analysedSurveys, ref analysisResults, ref endOfRealHoleSurvey);
		new PrintPreviewDialog();
		return result;
	}

	private void RangeSelectionChanged()
	{
		GetSelectedSurveys(out var surveyList);
		m_chart.SetSelectedSurveys(surveyList, visible: true);
	}

	private void dataGridView1_SelectionChanged(object sender, EventArgs e)
	{
		RangeSelectionChanged();
	}

	private void GetSelectedSurveys(out SortableBindingList<AbsoluteSurvey> surveyList)
	{
		surveyList = new SortableBindingList<AbsoluteSurvey>();
		foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
		{
			AbsoluteSurvey absoluteSurvey = new AbsoluteSurvey();
			absoluteSurvey.MD = (decimal)selectedRow.Cells[0].Value;
			absoluteSurvey.Dip = (decimal)selectedRow.Cells[1].Value;
			absoluteSurvey.Azimuth = (decimal)selectedRow.Cells[2].Value;
			absoluteSurvey.TVD = (decimal)selectedRow.Cells[3].Value;
			absoluteSurvey.Northing = (decimal)selectedRow.Cells[4].Value;
			absoluteSurvey.Easting = (decimal)selectedRow.Cells[5].Value;
			absoluteSurvey.DL = (decimal)selectedRow.Cells[6].Value;
			surveyList.Add(absoluteSurvey);
		}
	}

	private void SortSurveysByMD(ref SortableBindingList<AbsoluteSurvey> surveyList)
	{
		SortableBindingList<AbsoluteSurvey> sortableBindingList = new SortableBindingList<AbsoluteSurvey>();
		while (surveyList.Count > 0)
		{
			AbsoluteSurvey absoluteSurvey = null;
			foreach (AbsoluteSurvey survey in surveyList)
			{
				if (absoluteSurvey == null)
				{
					absoluteSurvey = survey;
				}
				else if (survey.MD < absoluteSurvey.MD)
				{
					absoluteSurvey = survey;
				}
			}
			sortableBindingList.Add(new AbsoluteSurvey(absoluteSurvey));
			surveyList.Remove(absoluteSurvey);
		}
		surveyList = sortableBindingList;
	}

	private void OnButtonPressed(ButtonPressedEventArgs e)
	{
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		AnalyseRange();
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		ButtonPressedEventArgs e2 = new ButtonPressedEventArgs();
		e2.ButtonPressed = ButtonPressedEventArgs.ButtonPressedEnum.Cancel;
		OnButtonPressed(e2);
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void tabControl_Analysis_Click(object sender, EventArgs e)
	{
		m_chart.Tool = EzyNavHoleChart.Tools.TrackBall;
	}
}
