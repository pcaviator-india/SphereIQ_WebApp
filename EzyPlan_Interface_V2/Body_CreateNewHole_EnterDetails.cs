using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanStorage;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Body_CreateNewHole_EnterDetails : Layout_Body_Wizard
{
	public class FinishedEventArgs : EventArgs
	{
		public enum FinishedStateEnum
		{
			Next,
			Cancel
		}

		private FinishedStateEnum m_finishedState;

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

	private IContainer components;

	private GroupBox groupBox_details;

	private Panel panel_helpDetails;

	private Label label_helpHoleID;

	private Label label_helpRigID;

	private TextBox textBox_RigID;

	private TextBox textBox_HoleID;

	private Label label_RigID;

	private Label label_HoleID;

	private Panel panel2;

	private Panel panel1;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Next;

	private TextBox textBox_DrillingContractor;

	private TextBox textBox_Area;

	private TextBox textBox_Location;

	private Label label_DrillingContractor;

	private Label label_Area;

	private Label label_Location;

	private TextBox textBox_Client;

	private Label label_Client;

	private TextBox textBox_SurveyingContractor;

	private Label label3;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label2;

	private Label label1;

	private Panel panel5;

	public event EventHandler Finished;

	public Body_CreateNewHole_EnterDetails()
	{
		InitializeComponent();
		m_designTimeWidth = Width;
	}

	public void Display()
	{
		m_drillHoleFile = new DrillHoleFile();
		textBox_HoleID.Focus();
	}

	public void Display(DrillHoleFile drillHoleFileToEdit)
	{
		m_drillHoleFile = drillHoleFileToEdit;
		textBox_HoleID.Focus();
		DisplayFields(drillHoleFileToEdit);
	}

	private void DisplayFields(DrillHoleFile drillHoleFile)
	{
		textBox_HoleID.Text = drillHoleFile.HoleID;
		textBox_RigID.Text = drillHoleFile.RigID;
		textBox_Location.Text = drillHoleFile.Location;
		textBox_Area.Text = drillHoleFile.Area;
		textBox_DrillingContractor.Text = drillHoleFile.DrillingContractor;
		textBox_SurveyingContractor.Text = drillHoleFile.SurveyingContractor;
		textBox_Client.Text = drillHoleFile.Client;
	}

	private bool UpdateDrillHoleFile(DrillHoleFile drillHoleFile)
	{
		if (UpdateDetails(ref drillHoleFile))
		{
			return true;
		}
		return false;
	}

	private bool UpdateDetails(ref DrillHoleFile drillHoleFile)
	{
		if (textBox_HoleID.Text != "")
		{
			drillHoleFile.HoleID = textBox_HoleID.Text;
			if (textBox_RigID.Text != "")
			{
				drillHoleFile.RigID = textBox_RigID.Text;
				SetNonEssentialFields(drillHoleFile);
				return true;
			}
			Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Value required", "A value is required for the RigID field!");
			popup_MessageBox_OK.ShowDialog();
			textBox_RigID.Focus();
			return false;
		}
		Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Value required", "A value is required for the HoleID field!");
		popup_MessageBox_OK2.ShowDialog();
		textBox_HoleID.Focus();
		return false;
	}

	private void SetNonEssentialFields(DrillHoleFile drillHoleFile)
	{
		drillHoleFile.Location = textBox_Location.Text;
		drillHoleFile.Area = textBox_Area.Text;
		drillHoleFile.DrillingContractor = textBox_DrillingContractor.Text;
		drillHoleFile.SurveyingContractor = textBox_SurveyingContractor.Text;
		drillHoleFile.Client = textBox_Client.Text;
	}

	private void OnFinished(FinishedEventArgs e)
	{
		if (Finished != null)
		{
			Finished(this, e);
		}
	}

	private void Next()
	{
		if (UpdateDrillHoleFile(m_drillHoleFile))
		{
			FinishedEventArgs e = new FinishedEventArgs();
			e.DrillHoleFile = m_drillHoleFile;
			e.FinishedState = FinishedEventArgs.FinishedStateEnum.Next;
			OnFinished(e);
		}
	}

	private void Cancel()
	{
		FinishedEventArgs e = new FinishedEventArgs();
		e.FinishedState = FinishedEventArgs.FinishedStateEnum.Cancel;
		OnFinished(e);
	}

	private void button_next_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		Cancel();
	}

	private void AdjustLayout()
	{
		int num = Width;
		if (num < 840)
		{
			panel_helpDetails.Visible = false;
		}
		else
		{
			panel_helpDetails.Visible = true;
		}
	}

	private void Body_CreateNewHole_EnterDetails_Resize(object sender, EventArgs e)
	{
		AdjustLayout();
	}

	private void textBox_holeID_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_HoleID);
	}

	private void textBox_holeID_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_HoleID);
	}

	private void textBox_rigID_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_RigID);
	}

	private void textBox_rigID_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_RigID);
	}

	private void textBox_Location_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Location);
	}

	private void textBox_Location_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Location);
	}

	private void textBox_Area_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Area);
	}

	private void textBox_Area_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Area);
	}

	private void textBox_DrillingContractor_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_DrillingContractor);
	}

	private void textBox_DrillingContractor_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_DrillingContractor);
	}

	private void textBox_Client_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Client);
	}

	private void textBox_Client_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Client);
	}

	private void textBox_SurveyingContractor_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_SurveyingContractor);
	}

	private void textBox_SurveyingContractor_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_SurveyingContractor);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void button_Next_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Next);
	}

	private void button_Next_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Next);
	}

	private void textBox_HoleID_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_HoleID);
	}

	private void textBox_RigID_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_RigID);
	}

	private void textBox_Location_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Location);
	}

	private void textBox_Area_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Area);
	}

	private void textBox_DrillingContractor_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_DrillingContractor);
	}

	private void textBox_SurveyingContractor_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_SurveyingContractor);
	}

	private void textBox_Client_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Client);
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
		this.groupBox_details = new System.Windows.Forms.GroupBox();
		this.panel_helpDetails = new System.Windows.Forms.Panel();
		this.label_helpHoleID = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label_helpRigID = new System.Windows.Forms.Label();
		this.textBox_SurveyingContractor = new System.Windows.Forms.TextBox();
		this.textBox_DrillingContractor = new System.Windows.Forms.TextBox();
		this.textBox_Client = new System.Windows.Forms.TextBox();
		this.textBox_Area = new System.Windows.Forms.TextBox();
		this.textBox_Location = new System.Windows.Forms.TextBox();
		this.textBox_RigID = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label_DrillingContractor = new System.Windows.Forms.Label();
		this.label_Client = new System.Windows.Forms.Label();
		this.textBox_HoleID = new System.Windows.Forms.TextBox();
		this.label_Area = new System.Windows.Forms.Label();
		this.label_Location = new System.Windows.Forms.Label();
		this.label_RigID = new System.Windows.Forms.Label();
		this.label_HoleID = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Next = new RibbonStyle.RibbonMenuButton();
		this.panel5 = new System.Windows.Forms.Panel();
		base.groupBox_frame.SuspendLayout();
		base.panel_buttonPanel.SuspendLayout();
		base.panel_body.SuspendLayout();
		base.panel_header.SuspendLayout();
		base.panel_footer.SuspendLayout();
		base.panel_scrollPanel.SuspendLayout();
		this.groupBox_details.SuspendLayout();
		this.panel_helpDetails.SuspendLayout();
		base.SuspendLayout();
		base.label_headerText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.label_headerText1.Size = new System.Drawing.Size(634, 24);
		base.label_headerText1.Text = "Enter the required hole details below and press \"Next\" to continue:";
		base.groupBox_frame.Controls.Add(this.panel2);
		base.groupBox_frame.Controls.Add(this.panel1);
		base.groupBox_frame.Size = new System.Drawing.Size(1019, 506);
		base.groupBox_frame.Controls.SetChildIndex(this.panel1, 0);
		base.groupBox_frame.Controls.SetChildIndex(this.panel2, 0);
		base.groupBox_frame.Controls.SetChildIndex(base.panel_scrollPanel, 0);
		base.panel_buttonPanel.Controls.Add(this.button_Cancel);
		base.panel_buttonPanel.Controls.Add(this.button_Next);
		base.panel_buttonPanel.Location = new System.Drawing.Point(721, 0);
		base.panel_buttonPanel.Size = new System.Drawing.Size(312, 65);
		base.panel_buttonPanel.TabIndex = 6;
		base.panel_body.Location = new System.Drawing.Point(0, 49);
		base.panel_body.Size = new System.Drawing.Size(1033, 520);
		base.panel_header.Controls.Add(this.panel5);
		base.panel_header.Size = new System.Drawing.Size(1033, 49);
		base.panel_header.Controls.SetChildIndex(base.label_headerText1, 0);
		base.panel_header.Controls.SetChildIndex(base.label_headerText2, 0);
		base.panel_header.Controls.SetChildIndex(this.panel5, 0);
		base.panel_footer.Location = new System.Drawing.Point(0, 569);
		base.panel_footer.Size = new System.Drawing.Size(1033, 65);
		base.panel_footer.TabIndex = 8;
		base.label_headerText2.Size = new System.Drawing.Size(541, 20);
		base.label_headerText2.Text = "HoleID and RigID are the only required fields the rest are optional.";
		base.panel_scrollPanel.Controls.Add(this.groupBox_details);
		base.panel_scrollPanel.Location = new System.Drawing.Point(13, 16);
		base.panel_scrollPanel.Size = new System.Drawing.Size(993, 487);
		this.groupBox_details.Controls.Add(this.panel_helpDetails);
		this.groupBox_details.Controls.Add(this.textBox_SurveyingContractor);
		this.groupBox_details.Controls.Add(this.textBox_DrillingContractor);
		this.groupBox_details.Controls.Add(this.textBox_Client);
		this.groupBox_details.Controls.Add(this.textBox_Area);
		this.groupBox_details.Controls.Add(this.textBox_Location);
		this.groupBox_details.Controls.Add(this.textBox_RigID);
		this.groupBox_details.Controls.Add(this.label3);
		this.groupBox_details.Controls.Add(this.label_DrillingContractor);
		this.groupBox_details.Controls.Add(this.label_Client);
		this.groupBox_details.Controls.Add(this.textBox_HoleID);
		this.groupBox_details.Controls.Add(this.label_Area);
		this.groupBox_details.Controls.Add(this.label_Location);
		this.groupBox_details.Controls.Add(this.label_RigID);
		this.groupBox_details.Controls.Add(this.label_HoleID);
		this.groupBox_details.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox_details.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_details.Location = new System.Drawing.Point(0, 0);
		this.groupBox_details.Name = "groupBox_details";
		this.groupBox_details.Size = new System.Drawing.Size(993, 372);
		this.groupBox_details.TabIndex = 0;
		this.groupBox_details.TabStop = false;
		this.groupBox_details.Text = "Identification Details";
		this.panel_helpDetails.Controls.Add(this.label_helpHoleID);
		this.panel_helpDetails.Controls.Add(this.label6);
		this.panel_helpDetails.Controls.Add(this.label5);
		this.panel_helpDetails.Controls.Add(this.label4);
		this.panel_helpDetails.Controls.Add(this.label2);
		this.panel_helpDetails.Controls.Add(this.label1);
		this.panel_helpDetails.Controls.Add(this.label_helpRigID);
		this.panel_helpDetails.Location = new System.Drawing.Point(530, 18);
		this.panel_helpDetails.Name = "panel_helpDetails";
		this.panel_helpDetails.Size = new System.Drawing.Size(409, 339);
		this.panel_helpDetails.TabIndex = 2;
		this.label_helpHoleID.AutoSize = true;
		this.label_helpHoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpHoleID.Location = new System.Drawing.Point(3, 20);
		this.label_helpHoleID.Name = "label_helpHoleID";
		this.label_helpHoleID.Size = new System.Drawing.Size(258, 18);
		this.label_helpHoleID.TabIndex = 4;
		this.label_helpHoleID.Text = "The identification code of the drill hole.";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(3, 305);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(358, 18);
		this.label6.TabIndex = 4;
		this.label6.Text = "The name of the company the hole is being drilled for.";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(3, 263);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(386, 18);
		this.label5.TabIndex = 4;
		this.label5.Text = "The name of the company providding survey's of the hole.";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(3, 222);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(285, 18);
		this.label4.TabIndex = 4;
		this.label4.Text = "The name of the company drilling the hole.";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(3, 159);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(341, 18);
		this.label2.TabIndex = 4;
		this.label2.Text = "The area of the mine site the hole is being drilled in.";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(3, 118);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(301, 18);
		this.label1.TabIndex = 4;
		this.label1.Text = "The mine site that the hole is being drilled on.";
		this.label_helpRigID.AutoSize = true;
		this.label_helpRigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_helpRigID.Location = new System.Drawing.Point(3, 60);
		this.label_helpRigID.Name = "label_helpRigID";
		this.label_helpRigID.Size = new System.Drawing.Size(246, 18);
		this.label_helpRigID.TabIndex = 4;
		this.label_helpRigID.Text = "The identification code of the drill rig.";
		this.textBox_SurveyingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_SurveyingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_SurveyingContractor.Location = new System.Drawing.Point(215, 281);
		this.textBox_SurveyingContractor.MaxLength = 28;
		this.textBox_SurveyingContractor.Name = "textBox_SurveyingContractor";
		this.textBox_SurveyingContractor.Size = new System.Drawing.Size(277, 26);
		this.textBox_SurveyingContractor.TabIndex = 5;
		this.textBox_SurveyingContractor.Enter += new System.EventHandler(this.textBox_SurveyingContractor_Enter);
		this.textBox_SurveyingContractor.DoubleClick += new System.EventHandler(this.textBox_SurveyingContractor_DoubleClick);
		this.textBox_SurveyingContractor.Leave += new System.EventHandler(this.textBox_SurveyingContractor_Leave);
		this.textBox_DrillingContractor.BackColor = System.Drawing.Color.White;
		this.textBox_DrillingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_DrillingContractor.Location = new System.Drawing.Point(215, 240);
		this.textBox_DrillingContractor.MaxLength = 28;
		this.textBox_DrillingContractor.Name = "textBox_DrillingContractor";
		this.textBox_DrillingContractor.Size = new System.Drawing.Size(277, 26);
		this.textBox_DrillingContractor.TabIndex = 4;
		this.textBox_DrillingContractor.Enter += new System.EventHandler(this.textBox_DrillingContractor_Enter);
		this.textBox_DrillingContractor.DoubleClick += new System.EventHandler(this.textBox_DrillingContractor_DoubleClick);
		this.textBox_DrillingContractor.Leave += new System.EventHandler(this.textBox_DrillingContractor_Leave);
		this.textBox_Client.BackColor = System.Drawing.Color.White;
		this.textBox_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Client.Location = new System.Drawing.Point(215, 320);
		this.textBox_Client.MaxLength = 28;
		this.textBox_Client.Name = "textBox_Client";
		this.textBox_Client.Size = new System.Drawing.Size(277, 26);
		this.textBox_Client.TabIndex = 6;
		this.textBox_Client.Enter += new System.EventHandler(this.textBox_Client_Enter);
		this.textBox_Client.DoubleClick += new System.EventHandler(this.textBox_Client_DoubleClick);
		this.textBox_Client.Leave += new System.EventHandler(this.textBox_Client_Leave);
		this.textBox_Area.BackColor = System.Drawing.Color.White;
		this.textBox_Area.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Area.Location = new System.Drawing.Point(214, 177);
		this.textBox_Area.MaxLength = 28;
		this.textBox_Area.Name = "textBox_Area";
		this.textBox_Area.Size = new System.Drawing.Size(277, 26);
		this.textBox_Area.TabIndex = 3;
		this.textBox_Area.Enter += new System.EventHandler(this.textBox_Area_Enter);
		this.textBox_Area.DoubleClick += new System.EventHandler(this.textBox_Area_DoubleClick);
		this.textBox_Area.Leave += new System.EventHandler(this.textBox_Area_Leave);
		this.textBox_Location.BackColor = System.Drawing.Color.White;
		this.textBox_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Location.Location = new System.Drawing.Point(214, 138);
		this.textBox_Location.MaxLength = 28;
		this.textBox_Location.Name = "textBox_Location";
		this.textBox_Location.Size = new System.Drawing.Size(277, 26);
		this.textBox_Location.TabIndex = 2;
		this.textBox_Location.Enter += new System.EventHandler(this.textBox_Location_Enter);
		this.textBox_Location.DoubleClick += new System.EventHandler(this.textBox_Location_DoubleClick);
		this.textBox_Location.Leave += new System.EventHandler(this.textBox_Location_Leave);
		this.textBox_RigID.BackColor = System.Drawing.Color.White;
		this.textBox_RigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_RigID.Location = new System.Drawing.Point(214, 78);
		this.textBox_RigID.MaxLength = 18;
		this.textBox_RigID.Name = "textBox_RigID";
		this.textBox_RigID.Size = new System.Drawing.Size(277, 26);
		this.textBox_RigID.TabIndex = 1;
		this.textBox_RigID.Enter += new System.EventHandler(this.textBox_rigID_Enter);
		this.textBox_RigID.DoubleClick += new System.EventHandler(this.textBox_RigID_DoubleClick);
		this.textBox_RigID.Leave += new System.EventHandler(this.textBox_rigID_Leave);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(27, 281);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(182, 20);
		this.label3.TabIndex = 0;
		this.label3.Text = "Surveying Contractor:";
		this.label_DrillingContractor.AutoSize = true;
		this.label_DrillingContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DrillingContractor.Location = new System.Drawing.Point(27, 240);
		this.label_DrillingContractor.Name = "label_DrillingContractor";
		this.label_DrillingContractor.Size = new System.Drawing.Size(159, 20);
		this.label_DrillingContractor.TabIndex = 0;
		this.label_DrillingContractor.Text = "Drilling Contractor:";
		this.label_Client.AutoSize = true;
		this.label_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Client.Location = new System.Drawing.Point(27, 320);
		this.label_Client.Name = "label_Client";
		this.label_Client.Size = new System.Drawing.Size(60, 20);
		this.label_Client.TabIndex = 0;
		this.label_Client.Text = "Client:";
		this.textBox_HoleID.BackColor = System.Drawing.Color.White;
		this.textBox_HoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_HoleID.Location = new System.Drawing.Point(214, 38);
		this.textBox_HoleID.MaxLength = 18;
		this.textBox_HoleID.Name = "textBox_HoleID";
		this.textBox_HoleID.Size = new System.Drawing.Size(277, 26);
		this.textBox_HoleID.TabIndex = 0;
		this.textBox_HoleID.Enter += new System.EventHandler(this.textBox_holeID_Enter);
		this.textBox_HoleID.DoubleClick += new System.EventHandler(this.textBox_HoleID_DoubleClick);
		this.textBox_HoleID.Leave += new System.EventHandler(this.textBox_holeID_Leave);
		this.label_Area.AutoSize = true;
		this.label_Area.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Area.Location = new System.Drawing.Point(27, 175);
		this.label_Area.Name = "label_Area";
		this.label_Area.Size = new System.Drawing.Size(52, 20);
		this.label_Area.TabIndex = 0;
		this.label_Area.Text = "Area:";
		this.label_Location.AutoSize = true;
		this.label_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Location.Location = new System.Drawing.Point(27, 138);
		this.label_Location.Name = "label_Location";
		this.label_Location.Size = new System.Drawing.Size(83, 20);
		this.label_Location.TabIndex = 0;
		this.label_Location.Text = "Location:";
		this.label_RigID.AutoSize = true;
		this.label_RigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_RigID.Location = new System.Drawing.Point(27, 76);
		this.label_RigID.Name = "label_RigID";
		this.label_RigID.Size = new System.Drawing.Size(150, 20);
		this.label_RigID.TabIndex = 0;
		this.label_RigID.Text = "RigID (Required):";
		this.label_HoleID.AutoSize = true;
		this.label_HoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_HoleID.Location = new System.Drawing.Point(27, 38);
		this.label_HoleID.Name = "label_HoleID";
		this.label_HoleID.Size = new System.Drawing.Size(160, 20);
		this.label_HoleID.TabIndex = 0;
		this.label_HoleID.Text = "HoleID (Required):";
		this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel2.Location = new System.Drawing.Point(1006, 16);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(10, 487);
		this.panel2.TabIndex = 19;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(3, 16);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 487);
		this.panel1.TabIndex = 17;
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
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Stop_Document;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(157, 6);
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
		this.button_Next.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Next.AutoEllipsis = true;
		this.button_Next.BackColor = System.Drawing.Color.Transparent;
		this.button_Next.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Next.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Next.ColorOn = System.Drawing.Color.Orange;
		this.button_Next.ColorOnStroke = System.Drawing.Color.Silver;
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
		this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.button_Next.Title = "";
		this.button_Next.UseVisualStyleBackColor = true;
		this.button_Next.Enter += new System.EventHandler(this.button_Next_Enter);
		this.button_Next.Click += new System.EventHandler(this.button_next_Click);
		this.button_Next.Leave += new System.EventHandler(this.button_Next_Leave);
		this.panel5.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Edit_Document;
		this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(969, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(64, 49);
		this.panel5.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.Name = "Body_CreateNewHole_EnterDetails";
		base.Size = new System.Drawing.Size(1033, 634);
		base.Resize += new System.EventHandler(this.Body_CreateNewHole_EnterDetails_Resize);
		base.groupBox_frame.ResumeLayout(false);
		base.panel_buttonPanel.ResumeLayout(false);
		base.panel_body.ResumeLayout(false);
		base.panel_header.ResumeLayout(false);
		base.panel_header.PerformLayout();
		base.panel_footer.ResumeLayout(false);
		base.panel_scrollPanel.ResumeLayout(false);
		this.groupBox_details.ResumeLayout(false);
		this.groupBox_details.PerformLayout();
		this.panel_helpDetails.ResumeLayout(false);
		this.panel_helpDetails.PerformLayout();
		base.ResumeLayout(false);
	}
}
