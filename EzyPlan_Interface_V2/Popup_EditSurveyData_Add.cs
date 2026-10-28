using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public class Popup_EditSurveyData_Add : Layout_PopupSmall
{
	public class SaveSurveyEventArgs : EventArgs
	{
		private Survey survey;

		private int m_index;

		public Survey Survey
		{
			get
			{
				return survey;
			}
			set
			{
				if (value != null)
				{
					survey = new Survey(value);
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

	private IContainer components;

	private RibbonMenuButton button_Cancel;

	private RibbonMenuButton button_AddFinish;

	private TextBox textBox_Azi;

	private TextBox textBox_MD;

	private TextBox textBox_Dip;

	private Label label2;

	private Label label3;

	private Label label4;

	private RibbonMenuButton button_Add;

	private Panel panel1;

	private Label label_SubText;

	private int m_index;

	public event EventHandler AddFinish;

	public event EventHandler Add;

	public event EventHandler Cancel;

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
		this.textBox_Azi = new System.Windows.Forms.TextBox();
		this.textBox_MD = new System.Windows.Forms.TextBox();
		this.textBox_Dip = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.button_AddFinish = new RibbonStyle.RibbonMenuButton();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.button_Add = new RibbonStyle.RibbonMenuButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label_SubText = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.textBox_Azi);
		base.panel_Detail.Controls.Add(this.textBox_MD);
		base.panel_Detail.Controls.Add(this.textBox_Dip);
		base.panel_Detail.Controls.Add(this.label4);
		base.panel_Detail.Controls.Add(this.label2);
		base.panel_Detail.Controls.Add(this.label3);
		base.panel_Detail.Location = new System.Drawing.Point(0, 77);
		base.panel_Detail.Size = new System.Drawing.Size(814, 115);
		base.label1.Location = new System.Drawing.Point(3, 0);
		base.label1.Size = new System.Drawing.Size(259, 24);
		base.label1.Text = "Descriptive text goes here.";
		base.panel_Footer.Controls.Add(this.button_AddFinish);
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.button_Add);
		base.panel_Footer.Location = new System.Drawing.Point(0, 192);
		base.panel_Footer.Size = new System.Drawing.Size(814, 66);
		base.panel_Footer.TabIndex = 3;
		base.panel_Header.Controls.Add(this.label_SubText);
		base.panel_Header.Controls.Add(this.panel1);
		base.panel_Header.Size = new System.Drawing.Size(814, 77);
		base.panel_Header.Controls.SetChildIndex(base.label1, 0);
		base.panel_Header.Controls.SetChildIndex(this.panel1, 0);
		base.panel_Header.Controls.SetChildIndex(this.label_SubText, 0);
		this.textBox_Azi.BackColor = System.Drawing.Color.White;
		this.textBox_Azi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Azi.Location = new System.Drawing.Point(527, 51);
		this.textBox_Azi.Name = "textBox_Azi";
		this.textBox_Azi.Size = new System.Drawing.Size(225, 29);
		this.textBox_Azi.TabIndex = 2;
		this.textBox_Azi.DoubleClick += new System.EventHandler(this.textBox_Azi_DoubleClick);
		this.textBox_Azi.Leave += new System.EventHandler(this.textBox_Azimuth_Leave);
		this.textBox_Azi.Enter += new System.EventHandler(this.textBox_Azimuth_Enter);
		this.textBox_MD.BackColor = System.Drawing.Color.White;
		this.textBox_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_MD.Location = new System.Drawing.Point(16, 51);
		this.textBox_MD.Name = "textBox_MD";
		this.textBox_MD.Size = new System.Drawing.Size(225, 29);
		this.textBox_MD.TabIndex = 0;
		this.textBox_MD.DoubleClick += new System.EventHandler(this.textBox_MD_DoubleClick);
		this.textBox_MD.Leave += new System.EventHandler(this.textBox_MD_Leave);
		this.textBox_MD.Enter += new System.EventHandler(this.textBox_MD_Enter);
		this.textBox_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Dip.Location = new System.Drawing.Point(271, 51);
		this.textBox_Dip.Name = "textBox_Dip";
		this.textBox_Dip.Size = new System.Drawing.Size(225, 29);
		this.textBox_Dip.TabIndex = 1;
		this.textBox_Dip.DoubleClick += new System.EventHandler(this.textBox_Dip_DoubleClick);
		this.textBox_Dip.Leave += new System.EventHandler(this.textBox_Dip_Leave);
		this.textBox_Dip.Enter += new System.EventHandler(this.textBox_Dip_Enter);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(12, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(44, 24);
		this.label2.TabIndex = 39;
		this.label2.Text = "MD:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(267, 24);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(43, 24);
		this.label3.TabIndex = 38;
		this.label3.Text = "Dip:";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(523, 24);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(83, 24);
		this.label4.TabIndex = 37;
		this.label4.Text = "Azimuth:";
		this.button_AddFinish.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_AddFinish.AutoEllipsis = true;
		this.button_AddFinish.BackColor = System.Drawing.Color.Transparent;
		this.button_AddFinish.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_AddFinish.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.ColorOn = System.Drawing.Color.Orange;
		this.button_AddFinish.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.ColorPress = System.Drawing.Color.Orange;
		this.button_AddFinish.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_AddFinish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_AddFinish.FadingSpeed = 35;
		this.button_AddFinish.FlatAppearance.BorderSize = 0;
		this.button_AddFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_AddFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_AddFinish.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_AddFinish.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Element;
		this.button_AddFinish.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_AddFinish.ImageOffset = 4;
		this.button_AddFinish.IsPressed = false;
		this.button_AddFinish.KeepPress = false;
		this.button_AddFinish.Location = new System.Drawing.Point(393, 9);
		this.button_AddFinish.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_AddFinish.MenuPos = new System.Drawing.Point(0, 0);
		this.button_AddFinish.Name = "button_AddFinish";
		this.button_AddFinish.Radius = 10;
		this.button_AddFinish.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_AddFinish.Size = new System.Drawing.Size(261, 45);
		this.button_AddFinish.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_AddFinish.SplitDistance = 0;
		this.button_AddFinish.TabIndex = 1;
		this.button_AddFinish.Text = "Add and finish";
		this.button_AddFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_AddFinish.Title = "";
		this.button_AddFinish.UseVisualStyleBackColor = true;
		this.button_AddFinish.Click += new System.EventHandler(this.button_AddFinish_Click);
		this.button_AddFinish.Leave += new System.EventHandler(this.button_Finish_Leave);
		this.button_AddFinish.Enter += new System.EventHandler(this.button_Finish_Enter);
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
		this.button_Cancel.Location = new System.Drawing.Point(660, 9);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(151, 45);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 2;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.button_Cancel.Leave += new System.EventHandler(this.button_Cancel_Leave);
		this.button_Cancel.Enter += new System.EventHandler(this.button_Cancel_Enter);
		this.button_Add.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Add.AutoEllipsis = true;
		this.button_Add.BackColor = System.Drawing.Color.Transparent;
		this.button_Add.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Add.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorOn = System.Drawing.Color.Orange;
		this.button_Add.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Add.ColorPress = System.Drawing.Color.Orange;
		this.button_Add.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Add.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Add.FadingSpeed = 35;
		this.button_Add.FlatAppearance.BorderSize = 0;
		this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Add.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Add.Image = EzyPlan_Interface_V2.Properties.Resources.Add_Document;
		this.button_Add.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Add.ImageOffset = 4;
		this.button_Add.IsPressed = false;
		this.button_Add.KeepPress = false;
		this.button_Add.Location = new System.Drawing.Point(126, 9);
		this.button_Add.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Add.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Add.Name = "button_Add";
		this.button_Add.Radius = 10;
		this.button_Add.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Add.Size = new System.Drawing.Size(261, 45);
		this.button_Add.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Add.SplitDistance = 0;
		this.button_Add.TabIndex = 0;
		this.button_Add.Text = "Add and enter another";
		this.button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Add.Title = "";
		this.button_Add.UseVisualStyleBackColor = true;
		this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
		this.button_Add.Leave += new System.EventHandler(this.button_Add_Leave);
		this.button_Add.Enter += new System.EventHandler(this.button_Add_Enter);
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.Add_Record;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(750, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(64, 77);
		this.panel1.TabIndex = 1;
		this.label_SubText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_SubText.Location = new System.Drawing.Point(3, 28);
		this.label_SubText.Name = "label_SubText";
		this.label_SubText.Size = new System.Drawing.Size(741, 40);
		this.label_SubText.TabIndex = 2;
		this.label_SubText.Text = "Enter the new survey fields below and click \"Add and enter another\" to add the new survey and continue entering another or \"Add and finish\" to add the survey and close the popup.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(814, 258);
		base.Name = "Popup_EditSurveyData_Add";
		this.Text = "Add new survey";
		base.Shown += new System.EventHandler(this.Popup_EditSurveyData_Add_Shown);
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Footer.ResumeLayout(false);
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_EditSurveyData_Add()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(Survey survey, string descriptiveText)
	{
		label1.Text = descriptiveText;
		Text = descriptiveText;
		return ShowDialog();
	}

	public DialogResult ShowDialog(int index, Survey survey, string descriptiveText)
	{
		label1.Text = descriptiveText;
		Text = descriptiveText;
		m_index = index;
		textBox_MD.Text = survey.MD.ToString();
		textBox_Dip.Text = survey.Dip.ToString();
		textBox_Azi.Text = survey.Azimuth.ToString();
		textBox_MD.Focus();
		return ShowDialog();
	}

	private void OnAdd(Survey survey)
	{
		SaveSurveyEventArgs e = new SaveSurveyEventArgs();
		e.Survey = survey;
		e.Index = m_index;
		if (Add != null)
		{
			Add(this, e);
		}
		textBox_MD.Focus();
	}

	private void OnAddFinish(Survey survey)
	{
		SaveSurveyEventArgs e = new SaveSurveyEventArgs();
		e.Survey = survey;
		e.Index = m_index;
		if (AddFinish != null)
		{
			AddFinish(this, e);
		}
	}

	private void OnCancel()
	{
		if (Cancel != null)
		{
			Cancel(this, null);
		}
	}

	private bool ValidateAndSave(out Survey survey)
	{
		survey = new Survey();
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
								return true;
							}
							textBox_Azi.Focus();
							DisplayErrorPopup(setAzimuthEnum);
							return false;
						}
						Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK("Could not save changes!", "The Azimuth value is invalid.");
						textBox_Azi.Focus();
						popup_MessageBox_OK.ShowDialog();
						return false;
					}
					textBox_Dip.Focus();
					DisplayErrorPopup(setDipEnum);
					return false;
				}
				Popup_MessageBox_OK popup_MessageBox_OK2 = new Popup_MessageBox_OK("Could not save changes!", "The Dip value is invalid.");
				textBox_Dip.Focus();
				popup_MessageBox_OK2.ShowDialog();
				return false;
			}
			textBox_MD.Focus();
			DisplayErrorPopup(setMDEnum);
			return false;
		}
		Popup_MessageBox_OK popup_MessageBox_OK3 = new Popup_MessageBox_OK("Could not save changes!", "The MD value is invalid.");
		textBox_MD.Focus();
		popup_MessageBox_OK3.ShowDialog();
		return false;
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

	private void button_Add_Click(object sender, EventArgs e)
	{
		if (ValidateAndSave(out var survey))
		{
			OnAdd(survey);
		}
	}

	private void button_AddFinish_Click(object sender, EventArgs e)
	{
		if (ValidateAndSave(out var survey))
		{
			OnAddFinish(survey);
		}
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		OnCancel();
	}

	private void textBox_MD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_MD);
	}

	private void textBox_MD_Leave(object sender, EventArgs e)
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

	private void textBox_Azimuth_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Azi);
	}

	private void textBox_Azimuth_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Azi);
	}

	private void button_Add_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Add);
	}

	private void button_Add_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Add);
	}

	private void button_Finish_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_AddFinish);
	}

	private void button_Finish_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_AddFinish);
	}

	private void button_Cancel_Enter(object sender, EventArgs e)
	{
		Utility.ButtonGotFocus(ref button_Cancel);
	}

	private void button_Cancel_Leave(object sender, EventArgs e)
	{
		Utility.ButtonLostFocus(ref button_Cancel);
	}

	private void Popup_EditSurveyData_Add_Shown(object sender, EventArgs e)
	{
		textBox_MD.Focus();
	}

	private void textBox_Azi_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Azi);
	}

	private void textBox_Dip_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_Dip);
	}

	private void textBox_MD_DoubleClick(object sender, EventArgs e)
	{
		Utility.TextBoxDoubleClick(textBox_MD);
	}
}
