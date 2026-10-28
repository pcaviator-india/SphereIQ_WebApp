using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_CreateNewHole_SaveFile : Layout_Body_Wizard
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Save,
			Cancel,
			Back
		}

		private FinishedStateEnum m_finishedState;

		private string m_filePath;

		private DrillHoleFile m_drillHoleFile;

		public FinishedStateEnum FinishedState
		{
			get
			{
				return m_finishedState;
			}
			set
			{
				m_finishedState = value;
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

		public DrillHoleFile DrillHoleFile
		{
			get
			{
				return m_drillHoleFile;
			}
			set
			{
				m_drillHoleFile = value;
			}
		}
	}

	private DrillHoleFile m_drillHoleFile;

	private DrillHoleFile m_newDrillHoleFile;

	private Popup_WorkingIndicator m_loading;

	private bool firstRun = true;

	private IContainer components;

	private GroupBox groupBox_select;

	private FilesListBox filesListBox1;

	private Panel panel1;

	private GroupBox groupBox2;

	private TextBox textBox_FileName;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Back;

	private RibbonMenuButton button_Save;

	private GroupBox groupBox_Navigation;

	private Panel panel4;

	private Panel panel3;

	private RibbonMenuButton button_Refresh;

	private RibbonMenuButton button_MyDocuments;

	private Panel panel2;

	private RibbonMenuButton button_Desktop;

	private Control_DriveBox driveBox1;

	private Panel panel5;

	public event EventHandler Finished;

	public Body_CreateNewHole_SaveFile(ref DrillHoleFile drillHoleFile)
	{
		InitializeComponent();
		m_drillHoleFile = drillHoleFile;
		driveBox1.SelectedDriveChanged += driveBox1_SelectedDriveChanged;
		filesListBox1.Invalidated += filesListBox1_Invalidated;
		SetFileListPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
		m_loading = new Popup_WorkingIndicator("Loading", "Loading SphereIQ File", showCancelButton: false);
		Visible = false;
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

	public void Display(DrillHoleFile newDrillHole)
	{
		if (firstRun)
		{
			driveBox1.UpdateDrivesList();
			SetFileListPath(GetMyDocumentsPath());
			firstRun = false;
		}
		SetDefaultFileName(newDrillHole.HoleID);
		m_newDrillHoleFile = newDrillHole;
		Visible = true;
		Focus();
	}

	public void SetDefaultFileName(string fileName)
	{
		textBox_FileName.Text = fileName;
	}

	public void RemoveFromDisplay()
	{
		Visible = false;
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

	private bool SaveFile(out string filePath)
	{
		if ((filesListBox1.SelectedPath != null) & (textBox_FileName.Text != ""))
		{
			filePath = filesListBox1.SelectedPath + textBox_FileName.Text + ".ezn";
			DrillHoleFile.SaveAs_ErrorsEnum saveAs_ErrorsEnum = m_newDrillHoleFile.SaveAs(filePath);
			if (saveAs_ErrorsEnum == DrillHoleFile.SaveAs_ErrorsEnum.NoError)
			{
				m_drillHoleFile.Close();
				m_drillHoleFile.Copy(m_newDrillHoleFile, copyStream: true);
				Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("SphereIQ file created!", "A new SphereIQ file has been created at: \n" + filePath);
				popup_MessageBox_OK.ShowDialog();
				return true;
			}
			DisplayError(saveAs_ErrorsEnum);
			return false;
		}
		filePath = null;
		DisplayError("Please select a path and enter a file name to save the new file.");
		return false;
	}

	private void DisplayError(DrillHoleFile.SaveAs_ErrorsEnum value)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Save Error!", DrillHoleFile.ExplainError(value) + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void DisplayError(string text)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Save Error!", text + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	private void OnFinished(FinishedEventArgs e)
	{
		if (Finished != null)
		{
			Finished(this, e);
		}
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

	private void button_Save_Click(object sender, EventArgs e)
	{
		if (SaveFile(out var filePath))
		{
			FinishedEventArgs e2 = new FinishedEventArgs();
			e2.FinishedState = FinishedEventArgs.FinishedStateEnum.Save;
			e2.FilePath = filePath;
			e2.DrillHoleFile = m_newDrillHoleFile;
			OnFinished(e2);
		}
	}

	private void button_Back_Click(object sender, EventArgs e)
	{
		FinishedEventArgs e2 = new FinishedEventArgs();
		e2.FinishedState = FinishedEventArgs.FinishedStateEnum.Back;
		e2.DrillHoleFile = m_newDrillHoleFile;
		OnFinished(e2);
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		FinishedEventArgs e2 = new FinishedEventArgs();
		e2.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
		OnFinished(e2);
	}

	private void button_Desktop_Click(object sender, EventArgs e)
	{
		Desktop();
	}

	private void button_MyDocuments_Click(object sender, EventArgs e)
	{
		MyDocuments();
	}

	private void button_Refresh_Click(object sender, EventArgs e)
	{
		RefreshDriveList();
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

	private void button_Save_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Save);
	}

	private void button_Save_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Save);
	}

	private void button_Back_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Back);
	}

	private void button_Back_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Back);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void textBox_FileName_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_FileName);
	}

	private void textBox_FileName_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_FileName);
	}

	private void textBox_FileName_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_FileName);
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
		this.groupBox_select = new System.Windows.Forms.GroupBox();
		this.filesListBox1 = new EzyPlan_Interface_V2.FilesListBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.textBox_FileName = new System.Windows.Forms.TextBox();
		this.groupBox_Navigation = new System.Windows.Forms.GroupBox();
		this.driveBox1 = new EzyPlan_Interface_V2.Control_DriveBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.button_Refresh = new RibbonStyle.RibbonMenuButton();
		this.button_MyDocuments = new RibbonStyle.RibbonMenuButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.button_Desktop = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Back = new RibbonStyle.RibbonMenuButton();
		this.button_Save = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_select.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox_Navigation.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(258, 24);
		base.label_headerText1.Text = "Save the new SphereIQ file:";
		base.groupBox_frame.Size = new System.Drawing.Size(986, 594);
		base.groupBox_frame.TabIndex = 1;
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Back);
		base.panel_buttonPanel.Controls.Add(this.button_Save);
		base.panel_buttonPanel.Location = new System.Drawing.Point(535, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(465, 56);
		base.panel_body.Size = new System.Drawing.Size(1000, 608);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(1000, 52);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 660);
		base.panel_footer.Size = new System.Drawing.Size(1000, 56);
		base.label_headerText2.Size = new System.Drawing.Size(595, 20);
		base.label_headerText2.Text = "Enter a filename and select where to save the file. Click Save when done.";
		base.panel_scrollPanel.Controls.Add(this.groupBox_select);
		base.panel_scrollPanel.Controls.Add(this.panel1);
		base.panel_scrollPanel.Controls.Add(this.groupBox_Navigation);
		base.panel_scrollPanel.Controls.Add(this.groupBox2);
		base.panel_scrollPanel.Size = new System.Drawing.Size(980, 575);
		this.groupBox_select.Controls.Add(this.filesListBox1);
		this.groupBox_select.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_select.Location = new System.Drawing.Point(366, 62);
		this.groupBox_select.Name = "groupBox_select";
		this.groupBox_select.Padding = new System.Windows.Forms.Padding(3, 25, 3, 3);
		this.groupBox_select.Size = new System.Drawing.Size(614, 513);
		this.groupBox_select.TabIndex = 2;
		this.groupBox_select.TabStop = false;
		this.groupBox_select.Text = "Save To:";
		this.filesListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.filesListBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.filesListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.filesListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.filesListBox1.FileIconSize = EzyPlan_Interface_V2.IconSize.Large;
		this.filesListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.filesListBox1.FormattingEnabled = true;
		this.filesListBox1.ItemHeight = 32;
		this.filesListBox1.Location = new System.Drawing.Point(3, 47);
		this.filesListBox1.Name = "filesListBox1";
		this.filesListBox1.SelectedPath = "c:\\";
		this.filesListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.filesListBox1.Size = new System.Drawing.Size(608, 448);
		this.filesListBox1.TabIndex = 0;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(356, 62);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 513);
		this.panel1.TabIndex = 31;
		this.groupBox2.Controls.Add(this.textBox_FileName);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(980, 62);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "File Name:";
		this.textBox_FileName.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBox_FileName.Location = new System.Drawing.Point(3, 25);
		this.textBox_FileName.MaxLength = 80;
		this.textBox_FileName.Name = "textBox_FileName";
		this.textBox_FileName.Size = new System.Drawing.Size(974, 29);
		this.textBox_FileName.TabIndex = 0;
		this.textBox_FileName.Enter += new System.EventHandler(this.textBox_FileName_Enter);
		this.textBox_FileName.DoubleClick += new System.EventHandler(this.textBox_FileName_DoubleClick);
		this.textBox_FileName.Leave += new System.EventHandler(this.textBox_FileName_Leave);
		this.groupBox_Navigation.Controls.Add(this.driveBox1);
		this.groupBox_Navigation.Controls.Add(this.panel4);
		this.groupBox_Navigation.Controls.Add(this.panel3);
		this.groupBox_Navigation.Controls.Add(this.button_Refresh);
		this.groupBox_Navigation.Controls.Add(this.button_MyDocuments);
		this.groupBox_Navigation.Controls.Add(this.panel2);
		this.groupBox_Navigation.Controls.Add(this.button_Desktop);
		this.groupBox_Navigation.Dock = System.Windows.Forms.DockStyle.Left;
		this.groupBox_Navigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Navigation.Location = new System.Drawing.Point(0, 62);
		this.groupBox_Navigation.Name = "groupBox_Navigation";
		this.groupBox_Navigation.Size = new System.Drawing.Size(356, 513);
		this.groupBox_Navigation.TabIndex = 1;
		this.groupBox_Navigation.TabStop = false;
		this.groupBox_Navigation.Text = "Navigation:";
		this.driveBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.driveBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.driveBox1.Location = new System.Drawing.Point(3, 151);
		this.driveBox1.Margin = new System.Windows.Forms.Padding(11);
		this.driveBox1.Name = "driveBox1";
		this.driveBox1.Size = new System.Drawing.Size(350, 294);
		this.driveBox1.TabIndex = 2;
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(3, 445);
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
		this.button_Refresh.Location = new System.Drawing.Point(3, 457);
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
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorPress = System.Drawing.Color.Orange;
		this.button_Cancel.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Cancel.FadingSpeed = 35;
		this.button_Cancel.FlatAppearance.BorderSize = 0;
		this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Cancel.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Cancel_Save;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(311, 6);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 2;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Back.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Back.AutoEllipsis = true;
		this.button_Back.BackColor = System.Drawing.Color.Transparent;
		this.button_Back.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Back.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorOn = System.Drawing.Color.Orange;
		this.button_Back.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Back.ColorPress = System.Drawing.Color.Orange;
		this.button_Back.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Back.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Back.FadingSpeed = 35;
		this.button_Back.FlatAppearance.BorderSize = 0;
		this.button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Back.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Back.Image = EzyPlan_Interface_V2.Properties.Resources.back;
		this.button_Back.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Back.ImageOffset = 4;
		this.button_Back.IsPressed = false;
		this.button_Back.KeepPress = false;
		this.button_Back.Location = new System.Drawing.Point(157, 6);
		this.button_Back.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Back.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Back.Name = "button_Back";
		this.button_Back.Radius = 10;
		this.button_Back.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Back.Size = new System.Drawing.Size(148, 45);
		this.button_Back.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Back.SplitDistance = 0;
		this.button_Back.TabIndex = 1;
		this.button_Back.Text = "Back";
		this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Back.Title = "";
		this.button_Back.UseVisualStyleBackColor = true;
		this.button_Back.Enter += new System.EventHandler(this.button_Back_Enter);
		this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
		this.button_Back.Leave += new System.EventHandler(this.button_Back_Leave);
		this.button_Save.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Save.AutoEllipsis = true;
		this.button_Save.BackColor = System.Drawing.Color.Transparent;
		this.button_Save.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Save.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Save.ColorOn = System.Drawing.Color.Orange;
		this.button_Save.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Save.ColorPress = System.Drawing.Color.Orange;
		this.button_Save.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Save.FadingSpeed = 35;
		this.button_Save.FlatAppearance.BorderSize = 0;
		this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Save.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Save.Image = EzyPlan_Interface_V2.Properties.Resources.Save;
		this.button_Save.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Save.ImageOffset = 4;
		this.button_Save.IsPressed = false;
		this.button_Save.KeepPress = false;
		this.button_Save.Location = new System.Drawing.Point(3, 6);
		this.button_Save.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Save.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Save.Name = "button_Save";
		this.button_Save.Radius = 10;
		this.button_Save.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Save.Size = new System.Drawing.Size(148, 45);
		this.button_Save.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Save.SplitDistance = 0;
		this.button_Save.TabIndex = 0;
		this.button_Save.Text = "Save";
		this.button_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Save.Title = "";
		this.button_Save.UseVisualStyleBackColor = true;
		this.button_Save.Enter += new System.EventHandler(this.button_Save_Enter);
		this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
		this.button_Save.Leave += new System.EventHandler(this.button_Save_Leave);
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Save_File;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(936, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 52);
		this.panel5.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_CreateNewHole_SaveFile";
		base.Size = new System.Drawing.Size(1000, 716);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_select.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox_Navigation.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
