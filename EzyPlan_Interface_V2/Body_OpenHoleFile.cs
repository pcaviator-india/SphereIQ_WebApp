using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_OpenHoleFile : Layout_Body_Wizard
{
	private delegate DrillHoleFile.OpenFile_ErrorsEnum OpenFileDelegate(ref string filePath, ref string fileName, ref DrillHoleFile drillHoleFile);

	private delegate void OpenFileResponseDelegate(DrillHoleFile.OpenFile_ErrorsEnum result, string filePath, string fileName);

	public class ButtonPressedEventArgs : EventArgs
	{
		public enum ButtonPressedEnum
		{
			Open,
			Cancel
		}

		private ButtonPressedEnum m_buttonPressed;

		private string m_fileName;

		private string m_filePath;

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

		public string FileName
		{
			get
			{
				return m_fileName;
			}
			set
			{
				m_fileName = value;
			}
		}

		public string FilePath
		{
			get
			{
				return m_filePath;
			}
			set
			{
				m_filePath = value;
			}
		}
	}

	private IContainer components;

	private GroupBox groupBox_select;

	private Panel panel1;

	private GroupBox groupBox_Navigation;

	private Control_DriveBox driveBox1;

	private FilesListBox filesListBox1;

	private RibbonMenuButton button_Open;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Refresh;

	private RibbonMenuButton button_MyDocuments;

	private RibbonMenuButton button_Desktop;

	private Panel panel2;

	private Panel panel4;

	private Panel panel3;

	private Panel panel5;

	private Panel panel6;

	private DrillHoleFile m_drillHoleFile;

	private bool firstRun = true;

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
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Open = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
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
		base.label_headerText1.Size = new System.Drawing.Size(349, 24);
		base.label_headerText1.Text = "Select the SphereIQ hole file to open:";
		base.groupBox_frame.Size = new System.Drawing.Size(1009, 525);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Open);
		base.panel_buttonPanel.Location = new System.Drawing.Point(712, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(311, 56);
		base.panel_body.Size = new System.Drawing.Size(1023, 539);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(1023, 52);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 591);
		base.panel_footer.Size = new System.Drawing.Size(1023, 56);
		base.label_headerText2.Size = new System.Drawing.Size(809, 20);
		base.label_headerText2.Text = "Browse to and select the SphereIQ file you want to open and click Open to continue or Cancel to quit.";
		base.panel_scrollPanel.AutoScroll = false;
		base.panel_scrollPanel.Controls.Add(this.groupBox_select);
		base.panel_scrollPanel.Controls.Add(this.panel1);
		base.panel_scrollPanel.Controls.Add(this.panel6);
		base.panel_scrollPanel.Controls.Add(this.groupBox_Navigation);
		base.panel_scrollPanel.Size = new System.Drawing.Size(1003, 506);
		this.groupBox_select.Controls.Add(this.filesListBox1);
		this.groupBox_select.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_select.Location = new System.Drawing.Point(366, 0);
		this.groupBox_select.Name = "groupBox_select";
		this.groupBox_select.Padding = new System.Windows.Forms.Padding(3, 25, 3, 3);
		this.groupBox_select.Size = new System.Drawing.Size(627, 506);
		this.groupBox_select.TabIndex = 1;
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
		this.filesListBox1.Size = new System.Drawing.Size(621, 448);
		this.filesListBox1.TabIndex = 0;
		this.filesListBox1.FileSelected += new EzyPlan_Interface_V2.FileSelectedEventHandler(this.filesListBox1_FileSelected);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(993, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 506);
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
		this.groupBox_Navigation.Size = new System.Drawing.Size(356, 506);
		this.groupBox_Navigation.TabIndex = 0;
		this.groupBox_Navigation.TabStop = false;
		this.groupBox_Navigation.Text = "Navigation:";
		this.driveBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.driveBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.driveBox1.Location = new System.Drawing.Point(3, 151);
		this.driveBox1.Margin = new System.Windows.Forms.Padding(6);
		this.driveBox1.Name = "driveBox1";
		this.driveBox1.Size = new System.Drawing.Size(350, 287);
		this.driveBox1.TabIndex = 2;
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(3, 438);
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
		this.button_Refresh.Location = new System.Drawing.Point(3, 450);
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
		this.button_Refresh.Enter += new System.EventHandler(this.button_Refresh_Enter);
		this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
		this.button_Refresh.Leave += new System.EventHandler(this.button_Refresh_Leave);
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
		this.button_MyDocuments.Enter += new System.EventHandler(this.button_MyDocuments_Enter);
		this.button_MyDocuments.Click += new System.EventHandler(this.button_MyDocuments_Click);
		this.button_MyDocuments.Leave += new System.EventHandler(this.button_MyDocuments_Leave);
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
		this.button_Desktop.Enter += new System.EventHandler(this.button_Desktop_Enter);
		this.button_Desktop.Click += new System.EventHandler(this.button_Desktop_Click);
		this.button_Desktop.Leave += new System.EventHandler(this.button_Desktop_Leave);
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
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Open.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Open.AutoEllipsis = true;
		this.button_Open.BackColor = System.Drawing.Color.Transparent;
		this.button_Open.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Open.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Open.ColorOn = System.Drawing.Color.Orange;
		this.button_Open.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Open.ColorPress = System.Drawing.Color.Orange;
		this.button_Open.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Open.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Open.FadingSpeed = 35;
		this.button_Open.FlatAppearance.BorderSize = 0;
		this.button_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Open.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Open.Image = EzyPlan_Interface_V2.Properties.Resources.Open_File;
		this.button_Open.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Open.ImageOffset = 4;
		this.button_Open.IsPressed = false;
		this.button_Open.KeepPress = false;
		this.button_Open.Location = new System.Drawing.Point(3, 6);
		this.button_Open.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Open.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Open.Name = "button_Open";
		this.button_Open.Radius = 10;
		this.button_Open.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Open.Size = new System.Drawing.Size(148, 45);
		this.button_Open.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Open.SplitDistance = 0;
		this.button_Open.TabIndex = 0;
		this.button_Open.Text = "Open";
		this.button_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Open.Title = "";
		this.button_Open.UseVisualStyleBackColor = true;
		this.button_Open.Enter += new System.EventHandler(this.button_Open_Enter);
		this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
		this.button_Open.Leave += new System.EventHandler(this.button_Open_Leave);
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Open_File;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(959, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 52);
		this.panel5.TabIndex = 11;
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(356, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(10, 506);
		this.panel6.TabIndex = 36;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_OpenHoleFile";
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

	public Body_OpenHoleFile(ref DrillHoleFile drillHoleFile)
	{
		InitializeComponent();
		m_drillHoleFile = drillHoleFile;
		driveBox1.SelectedDriveChanged += driveBox1_SelectedDriveChanged;
		filesListBox1.Invalidated += filesListBox1_Invalidated;
		SetFileListPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
	}

	public void Display()
	{
		Focus();
		if (firstRun)
		{
			driveBox1.UpdateDrivesList();
			SetFileListPath(GetMyDocumentsPath());
			firstRun = false;
		}
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
			OpenFileDelegate openFileDelegate = OpenFile;
			string filePath = filesListBox1.SelectedFile;
			string fileName = filesListBox1.SelectedItem.ToString();
			openFileDelegate.BeginInvoke(ref filePath, ref fileName, ref m_drillHoleFile, OpenFileCallback, null);
		}
		else if ((string)filesListBox1.SelectedItem != "" && (string)filesListBox1.SelectedItem != "..")
		{
			string text = filesListBox1.SelectedPath;
			if (text[text.Length - 1] != '\\')
			{
				text += "\\";
			}
			text += (string)filesListBox1.SelectedItem;
			if (IsFolder(text))
			{
				filesListBox1.SelectedPath = text;
			}
		}
	}

	private bool IsFolder(string path)
	{
		FileInfo fileInfo = new FileInfo(path);
		_ = fileInfo.Attributes;
		string text = fileInfo.Attributes.ToString();
		if (text.Contains("Directory"))
		{
			return true;
		}
		return false;
	}

	private DrillHoleFile.OpenFile_ErrorsEnum OpenFile(ref string filePath, ref string fileName, ref DrillHoleFile drillHoleFile)
	{
		return drillHoleFile.Open(filePath);
	}

	private void OpenFileCallback(IAsyncResult ar)
	{
		AsyncResult asyncResult = (AsyncResult)ar;
		OpenFileDelegate openFileDelegate = (OpenFileDelegate)asyncResult.AsyncDelegate;
		string filePath = null;
		DrillHoleFile drillHoleFile = null;
		string fileName = null;
		DrillHoleFile.OpenFile_ErrorsEnum result = openFileDelegate.EndInvoke(ref filePath, ref fileName, ref drillHoleFile, ar);
		OpenFileResponse(result, filePath, fileName);
	}

	private void OpenFileResponse(DrillHoleFile.OpenFile_ErrorsEnum result, string filePath, string fileName)
	{
		if (InvokeRequired)
		{
			OpenFileResponseDelegate method = OpenFileResponse;
			Invoke(method, result, filePath, fileName);
		}
		else if (result == DrillHoleFile.OpenFile_ErrorsEnum.NoError)
		{
			OnFinished(ButtonPressedEventArgs.ButtonPressedEnum.Open, fileName, filePath);
		}
		else
		{
			string text = DrillHoleFile.ExplainError(result);
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("File Error!", "The selected file could not be opened. " + text + ".");
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

	private void OnFinished(ButtonPressedEventArgs.ButtonPressedEnum state, string fileName, string filePath)
	{
		ButtonPressedEventArgs e = new ButtonPressedEventArgs();
		e.ButtonPressed = state;
		e.FileName = fileName;
		e.FilePath = filePath;
		if (ButtonPressed != null)
		{
			ButtonPressed(this, e);
		}
	}

	private void button_Open_Click(object sender, EventArgs e)
	{
		Open();
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void Cancel()
	{
		OnFinished(ButtonPressedEventArgs.ButtonPressedEnum.Cancel);
	}

	private void Open()
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
		Open();
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
		Utility.ButtonGotFocus(ref button_Open);
	}

	private void button_Open_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Open);
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
