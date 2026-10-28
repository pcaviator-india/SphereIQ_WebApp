using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_EditSurveyData_EditSurvey : Layout_PopupSmall
{
	public class SaveSurveyEventArgs : EventArgs
	{
		private Survey m_survey;

		private int m_index;

		public Survey Survey
		{
			get
			{
				return m_survey;
			}
			set
			{
				if (value != null)
				{
					m_survey = new Survey(value);
				}
			}
		}

		public int Index
		{
			get
			{
				return m_index;
			}
			set
			{
				m_index = value;
			}
		}
	}

	private int m_index;

	private IContainer components;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_Save;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox textBox_MD;

	private TextBox textBox_Azi;

	private TextBox textBox_Dip;

	private Panel panel1;

	private Label label_SubText;

	public event EventHandler Save;

	public event EventHandler Cancel;

	public Popup_EditSurveyData_EditSurvey()
	{
		InitializeComponent();
	}

	private void ValidateAndSave()
	{
		Survey survey = new Survey();
		if (Utility_Conversions.StringToDecimal(textBox_MD.Text, out var number) == Utility_Conversions.StringToDecimalResult.NoError)
		{
			Survey.SetMDEnum setMDEnum = survey.SetMD(number);
			if (setMDEnum == Survey.SetMDEnum.ValueSet)
			{
				if (Utility_Conversions.StringToDecimal(textBox_Dip.Text, out var number2) == Utility_Conversions.StringToDecimalResult.NoError)
				{
					Survey.SetDipEnum setDipEnum = survey.SetDip(number2);
					if (setDipEnum == Survey.SetDipEnum.ValueSet)
					{
						if (Utility_Conversions.StringToDecimal(textBox_Azi.Text, out var number3) == Utility_Conversions.StringToDecimalResult.NoError)
						{
							Survey.SetAzimuthEnum setAzimuthEnum = survey.SetAzimuth(number3);
							if (setAzimuthEnum == Survey.SetAzimuthEnum.ValueSet)
							{
								OnSave(survey);
								return;
							}
							textBox_Azi.Focus();
							DisplayErrorPopup(setAzimuthEnum);
						}
						else
						{
							Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not save changes!", "The Azimuth value is invalid.");
							textBox_Azi.Focus();
							popup_MessageBox_OK.ShowDialog();
						}
					}
					else
					{
						textBox_Dip.Focus();
						DisplayErrorPopup(setDipEnum);
					}
				}
				else
				{
					Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Could not save changes!", "The Dip value is invalid.");
					textBox_Dip.Focus();
					popup_MessageBox_OK2.ShowDialog();
				}
			}
			else
			{
				textBox_MD.Focus();
				DisplayErrorPopup(setMDEnum);
			}
		}
		else
		{
			Popup_MessageBox_OK popup_MessageBox_OK3 = new Popup_MessageBox_OK("Could not save changes!", "The MD value is invalid.");
			textBox_MD.Focus();
			popup_MessageBox_OK3.ShowDialog();
		}
	}

	private void DisplayErrorPopup(Survey.SetAzimuthEnum result)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Edit Unsuccessful", Survey.ToHumanReadable(result) + ".\nEnter a valid Azimuth value to continue.");
		popup_MessageBox_OK.ShowDialog();
	}

	private void DisplayErrorPopup(Survey.SetDipEnum result)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Edit Unsuccessful", Survey.ToHumanReadable(result) + ".\nEnter a valid Dip value to continue.");
		popup_MessageBox_OK.ShowDialog();
	}

	private void DisplayErrorPopup(Survey.SetMDEnum result)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Edit Unsuccessful", Survey.ToHumanReadable(result) + ".\nEnter a valid MD value to continue.");
		popup_MessageBox_OK.ShowDialog();
	}

	public DialogResult ShowDialog(Survey survey, int index)
	{
		textBox_MD.Text = survey.MD.ToString();
		textBox_Dip.Text = survey.Dip.ToString();
		textBox_Azi.Text = survey.Azimuth.ToString();
		m_index = index;
		return ShowDialog();
	}

	private void OnSave(Survey survey)
	{
		SaveSurveyEventArgs e = new SaveSurveyEventArgs();
		e.Survey = survey;
		e.Index = m_index;
		if (Save != null)
		{
			Save(this, e);
		}
	}

	private void OnCancel()
	{
		if (Cancel != null)
		{
			Cancel(this, null);
		}
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		OnCancel();
	}

	private void button_Save_Click(object sender, EventArgs e)
	{
		ValidateAndSave();
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void button_Save_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Save);
	}

	private void button_Save_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Save);
	}

	private void textBoxMD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_MD);
	}

	private void textBoxMD_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_MD);
	}

	private void textBox_Dip_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Dip);
	}

	private void textBox_Dip_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Dip);
	}

	private void textBox_Azi_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Azi);
	}

	private void textBox_Azi_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Azi);
	}

	private void textBox_MD_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void textBox_Dip_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void textBox_Azi_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void Popup_EditSurveyData_EditSurvey_Shown(object sender, EventArgs e)
	{
		textBox_MD.Focus();
	}

	private void textBox_MD_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_MD);
	}

	private void textBox_Dip_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Dip);
	}

	private void textBox_Azi_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Azi);
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
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Save = new RibbonStyle.RibbonMenuButton();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.textBox_Dip = new System.Windows.Forms.TextBox();
		this.textBox_MD = new System.Windows.Forms.TextBox();
		this.textBox_Azi = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label_SubText = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.textBox_MD);
		base.panel_Detail.Controls.Add(this.textBox_Azi);
		base.panel_Detail.Controls.Add(this.textBox_Dip);
		base.panel_Detail.Controls.Add(this.label4);
		base.panel_Detail.Controls.Add(this.label3);
		base.panel_Detail.Controls.Add(this.label2);
		base.panel_Detail.Location = new System.Drawing.Point(0, 75);
		base.panel_Detail.Size = new System.Drawing.Size(781, 102);
		base.panel_Detail.TabIndex = 0;
		base.label1.Location = new System.Drawing.Point(3, 3);
		base.label1.Size = new System.Drawing.Size(239, 24);
		base.label1.Text = "Edit the selected survey:";
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.button_Save);
		base.panel_Footer.Location = new System.Drawing.Point(0, 177);
		base.panel_Footer.Size = new System.Drawing.Size(781, 56);
		base.panel_Header.Controls.Add(this.label_SubText);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(781, 75);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Header.Controls.SetChildIndex(this.label_SubText, 0);
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
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.Close;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 4;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(626, 6);
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
		this.button_Save.Image = EzyPlan_Interface_V2.Properties.Resources.Edit_Document;
		this.button_Save.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Save.ImageOffset = 4;
		this.button_Save.IsPressed = false;
		this.button_Save.KeepPress = false;
		this.button_Save.Location = new System.Drawing.Point(472, 6);
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
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(14, 19);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(44, 24);
		this.label2.TabIndex = 0;
		this.label2.Text = "MD:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(196, 19);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(43, 24);
		this.label3.TabIndex = 0;
		this.label3.Text = "Dip:";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(376, 19);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(83, 24);
		this.label4.TabIndex = 0;
		this.label4.Text = "Azimuth:";
		this.textBox_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Dip.Location = new System.Drawing.Point(200, 46);
		this.textBox_Dip.Name = "textBox_Dip";
		this.textBox_Dip.Size = new System.Drawing.Size(153, 29);
		this.textBox_Dip.TabIndex = 1;
		this.textBox_Dip.Enter += new System.EventHandler(this.textBox_Dip_Enter);
		this.textBox_Dip.DoubleClick += new System.EventHandler(this.textBox_Dip_DoubleClick);
		this.textBox_Dip.Leave += new System.EventHandler(this.textBox_Dip_Leave);
		this.textBox_Dip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Dip_KeyPress);
		this.textBox_MD.BackColor = System.Drawing.Color.White;
		this.textBox_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_MD.Location = new System.Drawing.Point(18, 46);
		this.textBox_MD.Name = "textBox_MD";
		this.textBox_MD.Size = new System.Drawing.Size(153, 29);
		this.textBox_MD.TabIndex = 0;
		this.textBox_MD.Enter += new System.EventHandler(this.textBoxMD_Enter);
		this.textBox_MD.DoubleClick += new System.EventHandler(this.textBox_MD_DoubleClick);
		this.textBox_MD.Leave += new System.EventHandler(this.textBoxMD_Leave);
		this.textBox_MD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_MD_KeyPress);
		this.textBox_Azi.BackColor = System.Drawing.Color.White;
		this.textBox_Azi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Azi.Location = new System.Drawing.Point(380, 46);
		this.textBox_Azi.Name = "textBox_Azi";
		this.textBox_Azi.Size = new System.Drawing.Size(153, 29);
		this.textBox_Azi.TabIndex = 2;
		this.textBox_Azi.Enter += new System.EventHandler(this.textBox_Azi_Enter);
		this.textBox_Azi.DoubleClick += new System.EventHandler(this.textBox_Azi_DoubleClick);
		this.textBox_Azi.Leave += new System.EventHandler(this.textBox_Azi_Leave);
		this.textBox_Azi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Azi_KeyPress);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Edit_Record;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(717, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 75);
		this.panel1.TabIndex = 2;
		this.label_SubText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SubText.Location = new System.Drawing.Point(3, 27);
		this.label_SubText.Name = "label_SubText";
		this.label_SubText.Size = new System.Drawing.Size(681, 42);
		this.label_SubText.TabIndex = 3;
		this.label_SubText.Text = "Make the required changes and click the \"Save\" button to save the changes or \"Cancel\" to cancel them.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(781, 233);
		base.Name = "Popup_EditSurveyData_EditSurvey";
		this.Text = "Edit Survey";
		base.Shown += new System.EventHandler(this.Popup_EditSurveyData_EditSurvey_Shown);
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}
}
