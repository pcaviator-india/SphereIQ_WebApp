using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyReport;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_ReportPreview : Layout_PopupSmall
{
	public class PreviewDialogResult
	{
		private ResultEnum m_result;

		public ResultEnum Result
		{
			get
			{
				return m_result;
			}
			set
			{
				m_result = value;
			}
		}

		public PreviewDialogResult()
		{
			m_result = ResultEnum.Close;
		}
	}

	public enum ResultEnum
	{
		Close,
		Saved,
		Printed,
		SavedAndPrinted
	}

	private enum SaveFileResultsEnum
	{
		Error,
		Saved
	}

	private IContainer components;

	private GroupBox groupBox_Report;

	private Panel panel1;

	private RibbonMenuButton button_Close;

	private RibbonMenuButton button_Save;

	private RibbonMenuButton button_Print;

	private ReportViewer reportViewer1;

	private Panel panel2;

	private Label label2;

	private Popup_SaveFile m_saveFile;

	private PreviewDialogResult m_result;

	private Report m_report;

	private string m_defaultFilename;

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
		this.groupBox_Report = new System.Windows.Forms.GroupBox();
		this.reportViewer1 = new EzyReport.ReportViewer();
		this.button_Close = new RibbonStyle.RibbonMenuButton();
		this.button_Save = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button_Print = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		this.groupBox_Report.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.groupBox_Report);
		base.panel_Detail.Location = new System.Drawing.Point(0, 50);
		base.panel_Detail.Size = new System.Drawing.Size(754, 514);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(270, 24);
		base.label1.Text = "Review and save the report:";
		base.panel_Footer.Controls.Add(this.panel1);
		base.panel_Footer.Location = new System.Drawing.Point(0, 564);
		base.panel_Footer.Size = new System.Drawing.Size(754, 56);
		base.panel_Footer.TabIndex = 0;
		base.panel_Header.Controls.Add(this.label2);
		base.panel_Header.Controls.Add(this.panel2);
		base.panel_Header.Size = new System.Drawing.Size(754, 50);
		base.panel_Header.TabIndex = 2;
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel2, 0);
		base.panel_Header.Controls.SetChildIndex(this.label2, 0);
		this.groupBox_Report.Controls.Add(this.reportViewer1);
		this.groupBox_Report.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Report.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Report.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Report.Name = "groupBox_Report";
		this.groupBox_Report.Size = new System.Drawing.Size(754, 514);
		this.groupBox_Report.TabIndex = 1;
		this.groupBox_Report.TabStop = false;
		this.groupBox_Report.Text = "Report Preview";
		this.reportViewer1.AutoScroll = true;
		this.reportViewer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
		this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.reportViewer1.Location = new System.Drawing.Point(3, 25);
		this.reportViewer1.Margin = new System.Windows.Forms.Padding(6);
		this.reportViewer1.Name = "reportViewer1";
		this.reportViewer1.Padding = new System.Windows.Forms.Padding(92, 37, 92, 37);
		this.reportViewer1.Size = new System.Drawing.Size(748, 486);
		this.reportViewer1.TabIndex = 0;
		this.button_Close.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Close.AutoEllipsis = true;
		this.button_Close.BackColor = System.Drawing.Color.Transparent;
		this.button_Close.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Close.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Close.ColorOn = System.Drawing.Color.Orange;
		this.button_Close.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Close.ColorPress = System.Drawing.Color.Orange;
		this.button_Close.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Close.FadingSpeed = 35;
		this.button_Close.FlatAppearance.BorderSize = 0;
		this.button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Close.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Close.Image = EzyPlan_Interface_V2.Properties.Resources.Close;
		this.button_Close.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Close.ImageOffset = 0;
		this.button_Close.IsPressed = false;
		this.button_Close.KeepPress = false;
		this.button_Close.Location = new System.Drawing.Point(359, 6);
		this.button_Close.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Close.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Close.Name = "button_Close";
		this.button_Close.Radius = 10;
		this.button_Close.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Close.Size = new System.Drawing.Size(172, 45);
		this.button_Close.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Close.SplitDistance = 0;
		this.button_Close.TabIndex = 2;
		this.button_Close.Text = "Close";
		this.button_Close.Title = "";
		this.button_Close.UseVisualStyleBackColor = true;
		this.button_Close.Enter += new System.EventHandler(this.button_Close_Enter);
		this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
		this.button_Close.Leave += new System.EventHandler(this.button_Close_Leave);
		this.button_Save.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.button_Save.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Save.AutoEllipsis = true;
		this.button_Save.BackColor = System.Drawing.Color.Transparent;
		this.button_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_Save.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Save.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Save.ColorOn = System.Drawing.Color.Orange;
		this.button_Save.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Save.ColorPress = System.Drawing.Color.Orange;
		this.button_Save.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Save.FadingSpeed = 35;
		this.button_Save.FlatAppearance.BorderSize = 0;
		this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Save.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Save.Image = EzyPlan_Interface_V2.Properties.Resources.Save_File;
		this.button_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button_Save.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Save.ImageOffset = 0;
		this.button_Save.IsPressed = false;
		this.button_Save.KeepPress = false;
		this.button_Save.Location = new System.Drawing.Point(3, 6);
		this.button_Save.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Save.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Save.Name = "button_Save";
		this.button_Save.Radius = 10;
		this.button_Save.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Save.Size = new System.Drawing.Size(172, 45);
		this.button_Save.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Save.SplitDistance = 0;
		this.button_Save.TabIndex = 0;
		this.button_Save.Text = "Save PDF";
		this.button_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Save.Title = "";
		this.button_Save.UseVisualStyleBackColor = true;
		this.button_Save.Enter += new System.EventHandler(this.button_Save_Enter);
		this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
		this.button_Save.Leave += new System.EventHandler(this.button_Save_Leave);
		this.panel1.Controls.Add(this.button_Print);
		this.panel1.Controls.Add(this.button_Close);
		this.panel1.Controls.Add(this.button_Save);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(213, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(541, 56);
		this.panel1.TabIndex = 4;
		this.button_Print.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Print.AutoEllipsis = true;
		this.button_Print.BackColor = System.Drawing.Color.Transparent;
		this.button_Print.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Print.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Print.ColorOn = System.Drawing.Color.Orange;
		this.button_Print.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Print.ColorPress = System.Drawing.Color.Orange;
		this.button_Print.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Print.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Print.FadingSpeed = 35;
		this.button_Print.FlatAppearance.BorderSize = 0;
		this.button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Print.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Print.Image = EzyPlan_Interface_V2.Properties.Resources.Printer;
		this.button_Print.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Print.ImageOffset = 0;
		this.button_Print.IsPressed = false;
		this.button_Print.KeepPress = false;
		this.button_Print.Location = new System.Drawing.Point(181, 6);
		this.button_Print.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Print.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Print.Name = "button_Print";
		this.button_Print.Radius = 10;
		this.button_Print.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Print.Size = new System.Drawing.Size(172, 45);
		this.button_Print.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Print.SplitDistance = 0;
		this.button_Print.TabIndex = 1;
		this.button_Print.Text = "Print";
		this.button_Print.Title = "";
		this.button_Print.UseVisualStyleBackColor = true;
		this.button_Print.Enter += new System.EventHandler(this.button_Print_Enter);
		this.button_Print.Click += new System.EventHandler(this.button_Print_Click);
		this.button_Print.Leave += new System.EventHandler(this.button_Print_Leave);
		this.panel2.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Report;
		this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(690, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(64, 50);
		this.panel2.TabIndex = 4;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(3, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(410, 20);
		this.label2.TabIndex = 5;
		this.label2.Text = "You can either save a PDF of the report or print it.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(754, 620);
		base.Name = "Popup_ReportPreview";
		this.Text = "Report Preview";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		this.groupBox_Report.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Popup_ReportPreview()
	{
		InitializeComponent();
		m_result = new PreviewDialogResult();
	}

	public void DriveChange(string path)
	{
		if (m_saveFile != null)
		{
			m_saveFile.DriveChange(path);
		}
	}

	public void DriveChange()
	{
		if (m_saveFile != null)
		{
			m_saveFile.DriveChange();
		}
	}

	public void ShowDialog(Report report, string defaultFilename, IWin32Window parent)
	{
		SetReport(report);
		SetDefaultFilename(defaultFilename);
		base.ShowDialog(parent);
	}

	public void ShowDialog(Report report, string depth)
	{
		ShowDialog(report, depth, null);
	}

	public new void ShowDialog()
	{
		base.ShowDialog();
	}

	public new void ShowDialog(IWin32Window parent)
	{
		base.ShowDialog(parent);
	}

	public void SetReport(Report report)
	{
		if (report != null)
		{
			m_report = report;
			reportViewer1.SetReportToView(report);
			reportViewer1.Size = reportViewer1.GetPreferredSize(new Size(10, 10));
		}
	}

	public void SetDefaultFilename(string defaultFilename)
	{
		m_defaultFilename = defaultFilename;
	}

	private void SaveReport()
	{
		if (m_report == null)
		{
			return;
		}
		m_saveFile = new Popup_SaveFile();
		bool flag = true;
		while (flag)
		{
			if (m_defaultFilename != null && m_defaultFilename != "")
			{
				m_saveFile.ShowDialog("PDF", "pdf", m_defaultFilename);
			}
			else
			{
				m_saveFile.ShowDialog("PDF", "pdf", "");
			}
			if (m_saveFile.DialogResult.UserAction == Popup_SaveFile.UserActionEnum.Save)
			{
				string fullPath = m_saveFile.DialogResult.FullPath;
				SaveFileResultsEnum saveFileResultsEnum = SaveReport(m_report, fullPath);
				if (saveFileResultsEnum == SaveFileResultsEnum.Saved)
				{
					Visible = true;
					Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Save report to PDF", "The report was successfully saved to: \n " + fullPath);
					popup_MessageBox_OK.ShowDialog();
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
		}
		m_saveFile = null;
	}

	private SaveFileResultsEnum SaveReport(Report report, string fullPath)
	{
		try
		{
			report.ExportToPDF(fullPath);
			return SaveFileResultsEnum.Saved;
		}
		catch (Report.SaveToPDFException ex)
		{
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Save error", ex.Description + ".");
			popup_MessageBox_OK.ShowDialog();
			return SaveFileResultsEnum.Error;
		}
	}

	private void button_Save_Click(object sender, EventArgs e)
	{
		SaveReport();
	}

	private void button_Close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button_Print_Click(object sender, EventArgs e)
	{
		PrintReport();
	}

	private void PrintReport()
	{
		PrintDialog printDialog = new PrintDialog();
		printDialog.Document = m_report;
		if (printDialog.ShowDialog() == DialogResult.OK)
		{
			m_report.Print();
		}
	}

	private void button_Save_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Save);
	}

	private void button_Save_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Save);
	}

	private void button_Close_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Close);
	}

	private void button_Close_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Close);
	}

	private void button_Print_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Print);
	}

	private void button_Print_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Print);
	}
}
