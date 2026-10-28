using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavMath;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate_ToMD : Popup_Extrapolate
{
	private IContainer components;

	private TextBox textBox_DL;

	private TextBox textBox_TFS;

	private TextBox textBox_MD;

	private Label label_DL;

	private Label label_TFS;

	private Label label_MD;

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
		this.textBox_DL = new System.Windows.Forms.TextBox();
		this.textBox_TFS = new System.Windows.Forms.TextBox();
		this.textBox_MD = new System.Windows.Forms.TextBox();
		this.label_DL = new System.Windows.Forms.Label();
		this.label_TFS = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.label_subText.Location = new System.Drawing.Point(3, 25);
		base.label_subText.Size = new System.Drawing.Size(733, 48);
		base.panel_Detail.Controls.Add(this.textBox_DL);
		base.panel_Detail.Controls.Add(this.textBox_TFS);
		base.panel_Detail.Controls.Add(this.textBox_MD);
		base.panel_Detail.Controls.Add(this.label_DL);
		base.panel_Detail.Controls.Add(this.label_TFS);
		base.panel_Detail.Controls.Add(this.label_MD);
		base.panel_Detail.Location = new System.Drawing.Point(0, 76);
		base.panel_Detail.Size = new System.Drawing.Size(804, 131);
		base.label1.Size = new System.Drawing.Size(644, 24);
		base.label1.Text = "Enter the required values below to extrapolate to a measured depth:";
		base.panel_Footer.Location = new System.Drawing.Point(0, 207);
		base.panel_Footer.Size = new System.Drawing.Size(804, 56);
		base.panel_Header.Size = new System.Drawing.Size(804, 76);
		this.textBox_DL.BackColor = System.Drawing.Color.White;
		this.textBox_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_DL.Location = new System.Drawing.Point(239, 56);
		this.textBox_DL.Name = "textBox_DL";
		this.textBox_DL.Size = new System.Drawing.Size(112, 29);
		this.textBox_DL.TabIndex = 1;
		this.textBox_DL.Leave += new System.EventHandler(this.textBox_DL_Leave);
		this.textBox_DL.Enter += new System.EventHandler(this.textBox_DL_Enter);
		this.textBox_TFS.BackColor = System.Drawing.Color.White;
		this.textBox_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_TFS.Location = new System.Drawing.Point(482, 56);
		this.textBox_TFS.Name = "textBox_TFS";
		this.textBox_TFS.Size = new System.Drawing.Size(115, 29);
		this.textBox_TFS.TabIndex = 2;
		this.textBox_TFS.Leave += new System.EventHandler(this.textBox_TFS_Leave);
		this.textBox_TFS.Enter += new System.EventHandler(this.textBox_TFS_Enter);
		this.textBox_MD.BackColor = System.Drawing.Color.White;
		this.textBox_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_MD.Location = new System.Drawing.Point(28, 56);
		this.textBox_MD.Name = "textBox_MD";
		this.textBox_MD.Size = new System.Drawing.Size(151, 29);
		this.textBox_MD.TabIndex = 0;
		this.textBox_MD.Leave += new System.EventHandler(this.textBox_MD_Leave);
		this.textBox_MD.Enter += new System.EventHandler(this.textBox_MD_Enter);
		this.label_DL.AutoSize = true;
		this.label_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DL.Location = new System.Drawing.Point(235, 29);
		this.label_DL.Name = "label_DL";
		this.label_DL.Size = new System.Drawing.Size(206, 24);
		this.label_DL.TabIndex = 8;
		this.label_DL.Text = "Dogleg (Degrees/30m):";
		this.label_TFS.AutoSize = true;
		this.label_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TFS.Location = new System.Drawing.Point(478, 29);
		this.label_TFS.Name = "label_TFS";
		this.label_TFS.Size = new System.Drawing.Size(239, 24);
		this.label_TFS.TabIndex = 9;
		this.label_TFS.Text = "Toolface Setting (Degrees):";
		this.label_MD.AutoSize = true;
		this.label_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_MD.Location = new System.Drawing.Point(24, 29);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(188, 24);
		this.label_MD.TabIndex = 10;
		this.label_MD.Text = "Measured Depth (m):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(804, 263);
		base.Name = "Popup_Extrapolate_ToMD";
		this.Text = "Extrapolate to MD";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_Extrapolate_ToMD(AbsoluteSurvey lastSurvey)
		: base(lastSurvey)
	{
		InitializeComponent();
	}

	protected override void SaveExtrapolatedSurvey(out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		new NavMath();
		lastExtrapolatedSurvey = null;
		if (Utility.ConvertUserEnteredValue(textBox_MD.Text, out var value, "Measured Depth"))
		{
			if (Utility.ConvertUserEnteredValue(textBox_DL.Text, out var value2, "Dogleg"))
			{
				if (Utility.ConvertUserEnteredValue(textBox_TFS.Text, out var value3, "Toolface"))
				{
					if (CalculateExtrapolation(value, value2, value3, LastSurvey, out lastExtrapolatedSurvey))
					{
						textBox_MD.Focus();
					}
				}
				else
				{
					textBox_TFS.Focus();
				}
			}
			else
			{
				textBox_DL.Focus();
			}
		}
		else
		{
			textBox_MD.Focus();
		}
	}

	private bool CalculateExtrapolation(decimal MD, decimal DL, decimal TF, AbsoluteSurvey LastSurvey, out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		NavMath navMath = new NavMath();
		lastExtrapolatedSurvey = null;
		NavMath.ExtrapolateToMDResultEnum extrapolateToMDResultEnum = navMath.ExtrapolateToMD(MD, DL, TF, LastSurvey, out lastExtrapolatedSurvey);
		if (extrapolateToMDResultEnum == NavMath.ExtrapolateToMDResultEnum.NoError)
		{
			return true;
		}
		bool flag = false;
		switch (extrapolateToMDResultEnum)
		{
		case NavMath.ExtrapolateToMDResultEnum.DepthTooGreat:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.DipTooGreat:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.InvalidDogleg:
			DisplayError(extrapolateToMDResultEnum);
			textBox_DL.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.InvalidMD:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.InvalidToolface:
			DisplayError(extrapolateToMDResultEnum);
			textBox_TFS.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.MaximumDipReachedBeforeDepth:
			DisplayError(extrapolateToMDResultEnum);
			flag = true;
			break;
		case NavMath.ExtrapolateToMDResultEnum.PathCannotBeAchieved:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.UnknownError:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		case NavMath.ExtrapolateToMDResultEnum.UnknownException:
			DisplayError(extrapolateToMDResultEnum);
			textBox_MD.Focus();
			break;
		}
		if (!flag)
		{
			lastExtrapolatedSurvey = null;
		}
		return flag;
	}

	private void DisplayError(NavMath.ExtrapolateToMDResultEnum result)
	{
		Utility.DisplayErrorPopup("Error generating extrapolated survey", NavMath.ToHumanReadable(result) + ".");
	}

	protected override void ResetForDataEntry()
	{
		textBox_MD.Focus();
	}

	private void textBox_MD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_MD);
	}

	private void textBox_MD_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_MD);
	}

	private void textBox_DL_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_DL);
	}

	private void textBox_DL_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_DL);
	}

	private void textBox_TFS_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_TFS);
	}

	private void textBox_TFS_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_TFS);
	}
}
