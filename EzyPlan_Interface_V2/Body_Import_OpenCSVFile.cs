using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_Import_OpenCSVFile : Layout_Body_Wizard
{
	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Open,
			Cancel
		}

		private ButtonPressedEnum m_buttonPressed;

		private StreamReader m_streamReader;

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

		public StreamReader StreamReader
		{
			get
			{
				return m_streamReader;
			}
			set
			{
				m_streamReader = value;
			}
		}
	}

	private IContainer components;

	private GroupBox groupBox_select;

	private Panel panel1;

	private GroupBox groupBox_Navigation;

	private Control_DriveBox driveBox1;

	private FilesListBox filesListBox1;

	private RibbonMenuButton button_Next;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Refresh;

	private RibbonMenuButton button_MyDocuments;

	private RibbonMenuButton button_Desktop;

	private Panel panel2;

	private Panel panel4;

	private Panel panel3;

	private Panel panel5;

	private Label label3;

	private Label label4;

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
		this.groupBox_select = new System.Windows.Forms.GroupBox();
		this.filesListBox1 = new EzyPlan_Interface_V2.FilesListBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox_Navigation = new System.Windows.Forms.GroupBox();
		this.driveBox1 = new EzyPlan_Interface_V2.Control_DriveBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.button_Refresh = new RibbonStyle.RibbonMenuButton();
		this.button_MyDocuments = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.button_Desktop = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_select.SuspendLayout();
		this.groupBox_Navigation.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(266, 24);
		base.label_headerText1.Text = "Select the CSV file to open:";
		base.groupBox_frame.Size = new System.Drawing.Size(1007, 483);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Location = new System.Drawing.Point(712, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(311, 56);
		base.panel_body.Location = new System.Drawing.Point(0, 92);
		base.panel_body.Size = new System.Drawing.Size(1023, 499);
		base.panel_header.Controls.Add(this.label3);
		base.panel_header.Controls.Add(this.label4);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(1023, 92);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_header.Controls.SetChildIndex(this.label4, 0);
		base.panel_header.Controls.SetChildIndex(this.label3, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 591);
		base.panel_footer.Size = new System.Drawing.Size(1023, 56);
		base.label_headerText2.Location = new System.Drawing.Point(7, 23);
		base.label_headerText2.Size = new System.Drawing.Size(732, 20);
		base.label_headerText2.Text = "Browse to and select the CSV file that contains the survey data and click Next to continue.";
		base.panel_scrollPanel.AutoScroll = false;
		base.panel_scrollPanel.Controls.Add(this.groupBox_select);
		base.panel_scrollPanel.Controls.Add(this.panel1);
		base.panel_scrollPanel.Controls.Add(this.groupBox_Navigation);
		base.panel_scrollPanel.Size = new System.Drawing.Size(1001, 464);
		this.groupBox_select.Controls.Add(this.filesListBox1);
		this.groupBox_select.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_select.Location = new System.Drawing.Point(366, 0);
		this.groupBox_select.Name = "groupBox_select";
		this.groupBox_select.Padding = new System.Windows.Forms.Padding(3, 25, 3, 3);
		this.groupBox_select.Size = new System.Drawing.Size(635, 464);
		this.groupBox_select.TabIndex = 0;
		this.groupBox_select.TabStop = false;
		this.groupBox_select.Text = "null";
		this.filesListBox1.BackColor = System.Drawing.Color.White;
		this.filesListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.filesListBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.filesListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.filesListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.filesListBox1.FileIconSize = EzyPlan_Interface_V2.IconSize.Large;
		this.filesListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.filesListBox1.FormattingEnabled = true;
		this.filesListBox1.ItemHeight = 32;
		this.filesListBox1.Location = new System.Drawing.Point(3, 44);
		this.filesListBox1.Name = "filesListBox1";
		this.filesListBox1.ScrollAlwaysVisible = true;
		this.filesListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.filesListBox1.Size = new System.Drawing.Size(629, 416);
		this.filesListBox1.TabIndex = 0;
		this.filesListBox1.FileSelected += new EzyPlan_Interface_V2.FileSelectedEventHandler(this.filesListBox1_FileSelected);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(356, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 464);
		this.panel1.TabIndex = 35;
		this.groupBox_Navigation.Controls.Add(this.driveBox1);
		this.groupBox_Navigation.Controls.Add(this.panel4);
		this.groupBox_Navigation.Controls.Add(this.panel3);
		this.groupBox_Navigation.Controls.Add(this.button_Refresh);
		this.groupBox_Navigation.Controls.Add(this.button_MyDocuments);
		this.groupBox_Navigation.Controls.Add(this.panel2);
		this.groupBox_Navigation.Controls.Add(this.button_Desktop);
		this.groupBox_Navigation.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupBox_Navigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Navigation.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Navigation.Name = "groupBox_Navigation";
		this.groupBox_Navigation.Size = new System.Drawing.Size(356, 464);
		this.groupBox_Navigation.TabIndex = 1;
		this.groupBox_Navigation.TabStop = false;
		this.groupBox_Navigation.Text = "Navigation:";
		this.driveBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.driveBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.driveBox1.Location = new System.Drawing.Point(3, 151);
		this.driveBox1.Margin = new System.Windows.Forms.Padding(6);
		this.driveBox1.Name = "driveBox1";
		this.driveBox1.Size = new System.Drawing.Size(350, 245);
		this.driveBox1.TabIndex = 2;
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(3, 396);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(350, 12);
		this.panel4.TabIndex = 38;
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(3, 139);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(350, 12);
		this.panel3.TabIndex = 37;
		this.button_Refresh.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Refresh.AutoEllipsis = true;
		this.button_Refresh.BackColor = System.Drawing.Color.Transparent;
		this.button_Refresh.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Refresh.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Refresh.ColorOn = System.Drawing.Color.Orange;
		this.button_Refresh.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Refresh.ColorPress = System.Drawing.Color.Orange;
		this.button_Refresh.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Refresh.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.button_Refresh.FadingSpeed = 35;
		this.button_Refresh.FlatAppearance.BorderSize = 0;
		this.button_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Refresh.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Refresh.Image = EzyPlan_Interface_V2.Properties.Resources.Refresh_Document;
		this.button_Refresh.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Refresh.ImageOffset = 4;
		this.button_Refresh.IsPressed = false;
		this.button_Refresh.KeepPress = false;
		this.button_Refresh.Location = new System.Drawing.Point(3, 408);
		this.button_Refresh.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Refresh.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Refresh.Name = "button_Refresh";
		this.button_Refresh.Radius = 10;
		this.button_Refresh.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Refresh.Size = new System.Drawing.Size(350, 53);
		this.button_Refresh.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Refresh.SplitDistance = 0;
		this.button_Refresh.TabIndex = 3;
		this.button_Refresh.Text = "Refresh";
		this.button_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Refresh.Title = "";
		this.button_Refresh.UseVisualStyleBackColor = true;
		this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
		this.button_Refresh.Leave += new System.EventHandler(this.button_Refresh_Leave);
		this.button_Refresh.Enter += new System.EventHandler(this.button_Refresh_Enter);
		this.button_MyDocuments.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_MyDocuments.AutoEllipsis = true;
		this.button_MyDocuments.BackColor = System.Drawing.Color.Transparent;
		this.button_MyDocuments.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_MyDocuments.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_MyDocuments.ColorOn = System.Drawing.Color.Orange;
		this.button_MyDocuments.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_MyDocuments.ColorPress = System.Drawing.Color.Orange;
		this.button_MyDocuments.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_MyDocuments.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_MyDocuments.Dock = System.Windows.Forms.DockStyle.Top;
		this.button_MyDocuments.FadingSpeed = 35;
		this.button_MyDocuments.FlatAppearance.BorderSize = 0;
		this.button_MyDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_MyDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_MyDocuments.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_MyDocuments.Image = EzyPlan_Interface_V2.Properties.Resources.My_Documents;
		this.button_MyDocuments.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_MyDocuments.ImageOffset = 4;
		this.button_MyDocuments.IsPressed = false;
		this.button_MyDocuments.KeepPress = false;
		this.button_MyDocuments.Location = new System.Drawing.Point(3, 88);
		this.button_MyDocuments.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_MyDocuments.MenuPos = new System.Drawing.Point(0, 0);
		this.button_MyDocuments.Name = "button_MyDocuments";
		this.button_MyDocuments.Radius = 10;
		this.button_MyDocuments.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_MyDocuments.Size = new System.Drawing.Size(350, 51);
		this.button_MyDocuments.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_MyDocuments.SplitDistance = 0;
		this.button_MyDocuments.TabIndex = 1;
		this.button_MyDocuments.Text = "My Documents";
		this.button_MyDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_MyDocuments.Title = "";
		this.button_MyDocuments.UseVisualStyleBackColor = true;
		this.button_MyDocuments.Click += new System.EventHandler(this.button_MyDocuments_Click);
		this.button_MyDocuments.Leave += new System.EventHandler(this.button_MyDocuments_Leave);
		this.button_MyDocuments.Enter += new System.EventHandler(this.button_MyDocuments_Enter);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(3, 76);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(350, 12);
		this.panel2.TabIndex = 36;
		this.button_Desktop.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Desktop.AutoEllipsis = true;
		this.button_Desktop.BackColor = System.Drawing.Color.Transparent;
		this.button_Desktop.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Desktop.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Desktop.ColorOn = System.Drawing.Color.Orange;
		this.button_Desktop.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Desktop.ColorPress = System.Drawing.Color.Orange;
		this.button_Desktop.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Desktop.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Desktop.Dock = System.Windows.Forms.DockStyle.Top;
		this.button_Desktop.FadingSpeed = 35;
		this.button_Desktop.FlatAppearance.BorderSize = 0;
		this.button_Desktop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Desktop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Desktop.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Desktop.Image = EzyPlan_Interface_V2.Properties.Resources.Desktop;
		this.button_Desktop.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Desktop.ImageOffset = 4;
		this.button_Desktop.IsPressed = false;
		this.button_Desktop.KeepPress = false;
		this.button_Desktop.Location = new System.Drawing.Point(3, 25);
		this.button_Desktop.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Desktop.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Desktop.Name = "button_Desktop";
		this.button_Desktop.Radius = 10;
		this.button_Desktop.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Desktop.Size = new System.Drawing.Size(350, 51);
		this.button_Desktop.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Desktop.SplitDistance = 0;
		this.button_Desktop.TabIndex = 0;
		this.button_Desktop.Text = "Desktop";
		this.button_Desktop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Desktop.Title = "";
		this.button_Desktop.UseVisualStyleBackColor = true;
		this.button_Desktop.Click += new System.EventHandler(this.button_Desktop_Click);
		this.button_Desktop.Leave += new System.EventHandler(this.button_Desktop_Leave);
		this.button_Desktop.Enter += new System.EventHandler(this.button_Desktop_Enter);
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
		this.button_Next.ImageOffset = 4;
		this.button_Next.IsPressed = false;
		this.button_Next.KeepPress = false;
		this.button_Next.Location = new System.Drawing.Point(3, 6);
		this.button_Next.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Next.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Next.Name = "button_Next";
		this.button_Next.Radius = 10;
		this.button_Next.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Next.Size = new System.Drawing.Size(148, 45);
		this.button_Next.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Next.SplitDistance = 0;
		this.button_Next.TabIndex = 0;
		this.button_Next.Text = "Next";
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Open_Leave);
		this.button_Next.Enter += new System.EventHandler(this.button_Open_Enter);
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
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(156, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 1;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Import_Doc;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(959, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 92);
		this.panel5.TabIndex = 11;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(7, 43);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(517, 20);
		this.label3.TabIndex = 13;
		this.label3.Text = "Dip can be in mineral or oil and gas standard but is stored as oil and gas.";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 63);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(607, 20);
		this.label4.TabIndex = 12;
		this.label4.Text = "The Azimuth values must be relative to grid north. (Corrected for magnetic delination).";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_Import_OpenCSVFile";
		base.Size = new System.Drawing.Size(1023, 647);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_select.ResumeLayout(false);
		this.groupBox_Navigation.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Body_Import_OpenCSVFile()
	{
		InitializeComponent();
		driveBox1.SelectedDriveChanged += driveBox1_SelectedDriveChanged;
		filesListBox1.Invalidated += filesListBox1_Invalidated;
		SetFileListPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
	}

	public void DriveChange(string path)
	{
		DriveChange();
		SetFileListPath(path);
	}

	public void DriveChange()
	{
		driveBox1.UpdateDrivesList();
	}

	public void Display(bool refresh)
	{
		if (refresh)
		{
			driveBox1.UpdateDrivesList();
			SetFileListPath(GetMyDocumentsPath());
			Focus();
		}
		else
		{
			Focus();
		}
	}

	private void filesListBox1_Invalidated(object sender, InvalidateEventArgs e)
	{
		groupBox_select.Text = filesListBox1.SelectedPath;
	}

	private void driveBox1_SelectedDriveChanged(object sender, EventArgs e)
	{
		SetFileListPath(driveBox1.SelectedDriveRoot);
	}

	private void SetFileListPath(string path)
	{
		groupBox_select.Text = path;
		filesListBox1.SelectedPath = path;
	}

	private string GetMyDocumentsPath()
	{
		return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\";
	}

	private string GetDesktopPath()
	{
		return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
	}

	private void RefreshDriveList()
	{
		driveBox1.UpdateDrivesList();
	}

	private void OpenSelectedFile()
	{
		if (filesListBox1.SelectedFile != null)
		{
			string selectedFile = filesListBox1.SelectedFile;
			StreamReader stream = null;
			Utility_FileOperations.CreateStreamReader_ErrorsEnum createStreamReader_ErrorsEnum = Utility_FileOperations.CreateStreamReader(selectedFile, ref stream);
			if (createStreamReader_ErrorsEnum == Utility_FileOperations.CreateStreamReader_ErrorsEnum.NoError)
			{
				OnFinished(ButtonPressedEventArgs.ButtonPressedEnum.Open, ref stream);
				return;
			}
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Error opening CSV file", Utility_FileOperations.ExplainError(createStreamReader_ErrorsEnum) + ".");
			popup_MessageBox_OK.ShowDialog();
		}
	}

	private void OnFinished(ButtonPressedEventArgs.ButtonPressedEnum state)
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = state;
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void OnFinished(ButtonPressedEventArgs.ButtonPressedEnum state, ref StreamReader streamReader)
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = state;
		e.StreamReader = streamReader;
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Next_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void Cancel()
	{
		OnFinished(ButtonPressedEventArgs.ButtonPressedEnum.Cancel);
	}

	private void Next()
	{
		OpenSelectedFile();
	}

	private void MyDocuments()
	{
		driveBox1.DeSelectDrivesList();
		SetFileListPath(GetMyDocumentsPath());
	}

	private void Desktop()
	{
		driveBox1.DeSelectDrivesList();
		SetFileListPath(GetDesktopPath());
	}

	private void button_Refresh_Click(object sender, EventArgs e)
	{
		RefreshDriveList();
	}

	private void button_Desktop_Click(object sender, EventArgs e)
	{
		Desktop();
	}

	private void button_MyDocuments_Click(object sender, EventArgs e)
	{
		MyDocuments();
	}

	private void filesListBox1_FileSelected(object sender, FileSelectEventArgs fse)
	{
		Next();
	}

	private void button_Desktop_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Desktop);
	}

	private void button_Desktop_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Desktop);
	}

	private void button_MyDocuments_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_MyDocuments);
	}

	private void button_MyDocuments_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_MyDocuments);
	}

	private void button_Refresh_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Refresh);
	}

	private void button_Refresh_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Refresh);
	}

	private void button_Open_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Open_Leave(object sender, EventArgs e)
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
}
