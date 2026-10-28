using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavMath;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate_ToTVD : Popup_Extrapolate
{
	private IContainer components;

	private TextBox textBox_DL;

	private TextBox textBox_TFS;

	private TextBox textBox_TVD;

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
		this.textBox_TVD = new System.Windows.Forms.TextBox();
		this.label_DL = new System.Windows.Forms.Label();
		this.label_TFS = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.label_subText.Size = new System.Drawing.Size(729, 53);
		base.panel_Detail.Controls.Add(this.textBox_DL);
		base.panel_Detail.Controls.Add(this.textBox_TFS);
		base.panel_Detail.Controls.Add(this.textBox_TVD);
		base.panel_Detail.Controls.Add(this.label_DL);
		base.panel_Detail.Controls.Add(this.label_TFS);
		base.panel_Detail.Controls.Add(this.label_MD);
		base.panel_Detail.Location = new System.Drawing.Point(0, 80);
		base.panel_Detail.Size = new System.Drawing.Size(799, 130);
		base.label1.Size = new System.Drawing.Size(659, 24);
		base.label1.Text = "Enter the required values below to extrapolate to a true vertical depth:";
		base.panel_Footer.Location = new System.Drawing.Point(0, 210);
		base.panel_Footer.Size = new System.Drawing.Size(799, 56);
		base.panel_Header.Size = new System.Drawing.Size(799, 80);
		this.textBox_DL.BackColor = System.Drawing.Color.White;
		this.textBox_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_DL.Location = new System.Drawing.Point(241, 64);
		this.textBox_DL.Name = "textBox_DL";
		this.textBox_DL.Size = new System.Drawing.Size(112, 29);
		this.textBox_DL.TabIndex = 12;
		this.textBox_DL.Leave += new System.EventHandler(this.textBox_DL_Leave);
		this.textBox_DL.Enter += new System.EventHandler(this.textBox_DL_Enter);
		this.textBox_TFS.BackColor = System.Drawing.Color.White;
		this.textBox_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_TFS.Location = new System.Drawing.Point(484, 64);
		this.textBox_TFS.Name = "textBox_TFS";
		this.textBox_TFS.Size = new System.Drawing.Size(115, 29);
		this.textBox_TFS.TabIndex = 13;
		this.textBox_TFS.Leave += new System.EventHandler(this.textBox_TFS_Leave);
		this.textBox_TFS.Enter += new System.EventHandler(this.textBox_TFS_Enter);
		this.textBox_TVD.BackColor = System.Drawing.Color.White;
		this.textBox_TVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_TVD.Location = new System.Drawing.Point(30, 64);
		this.textBox_TVD.Name = "textBox_TVD";
		this.textBox_TVD.Size = new System.Drawing.Size(151, 29);
		this.textBox_TVD.TabIndex = 11;
		this.textBox_TVD.Leave += new System.EventHandler(this.textBox_TVD_Leave);
		this.textBox_TVD.Enter += new System.EventHandler(this.textBox_TVD_Enter);
		this.label_DL.AutoSize = true;
		this.label_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DL.Location = new System.Drawing.Point(237, 37);
		this.label_DL.Name = "label_DL";
		this.label_DL.Size = new System.Drawing.Size(206, 24);
		this.label_DL.TabIndex = 14;
		this.label_DL.Text = "Dogleg (Degrees/30m):";
		this.label_TFS.AutoSize = true;
		this.label_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TFS.Location = new System.Drawing.Point(480, 37);
		this.label_TFS.Name = "label_TFS";
		this.label_TFS.Size = new System.Drawing.Size(239, 24);
		this.label_TFS.TabIndex = 15;
		this.label_TFS.Text = "Toolface Setting (Degrees):";
		this.label_MD.AutoSize = true;
		this.label_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_MD.Location = new System.Drawing.Point(26, 37);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(86, 24);
		this.label_MD.TabIndex = 16;
		this.label_MD.Text = "TVD (m):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(799, 266);
		base.Name = "Popup_Extrapolate_ToTVD";
		this.Text = "Extrapolate to TVD";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_Extrapolate_ToTVD(AbsoluteSurvey lastSurvey)
		: base(lastSurvey)
	{
		InitializeComponent();
	}

	protected override void SaveExtrapolatedSurvey(out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		lastExtrapolatedSurvey = null;
		if (Utility.ConvertUserEnteredValue(textBox_TVD.Text, out var value, "TVD"))
		{
			if (Utility.ConvertUserEnteredValue(textBox_DL.Text, out var value2, "Dogleg"))
			{
				if (Utility.ConvertUserEnteredValue(textBox_TFS.Text, out var value3, "Toolface"))
				{
					if (CalculateExtrapolation(value, value2, value3, LastSurvey, out lastExtrapolatedSurvey))
					{
						textBox_TVD.Focus();
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
			textBox_TVD.Focus();
		}
	}

	private bool CalculateExtrapolation(decimal TVD, decimal DL, decimal TF, AbsoluteSurvey LastSurvey, out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		NavMath navMath = new NavMath();
		lastExtrapolatedSurvey = null;
		NavMath.ExtrapolateToTVDResultEnum extrapolateToTVDResultEnum = navMath.ExtrapolateToTVD(TVD, DL, TF, LastSurvey, out lastExtrapolatedSurvey);
		if (extrapolateToTVDResultEnum == NavMath.ExtrapolateToTVDResultEnum.NoError)
		{
			return true;
		}
		bool flag = false;
		switch (extrapolateToTVDResultEnum)
		{
		case NavMath.ExtrapolateToTVDResultEnum.DepthTooGreat:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.DipTooGreat:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.InvalidDogleg:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_DL.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.InvalidTVD:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.InvalidToolface:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TFS.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.MaxMDReachedBeforeTVD:
			DisplayError(extrapolateToTVDResultEnum);
			flag = true;
			break;
		case NavMath.ExtrapolateToTVDResultEnum.MaximumDipReachedBeforeDepth:
			DisplayError(extrapolateToTVDResultEnum);
			flag = true;
			break;
		case NavMath.ExtrapolateToTVDResultEnum.PathCannotBeAchieved:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.UnknownError:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		case NavMath.ExtrapolateToTVDResultEnum.UnknownException:
			DisplayError(extrapolateToTVDResultEnum);
			textBox_TVD.Focus();
			break;
		}
		if (!flag)
		{
			lastExtrapolatedSurvey = null;
		}
		return flag;
	}

	private void DisplayError(NavMath.ExtrapolateToTVDResultEnum result)
	{
		Utility.DisplayErrorPopup("Error generating extrapolated survey", NavMath.ToHumanReadable(result) + ".");
	}

	protected override void ResetForDataEntry()
	{
		textBox_TVD.Focus();
	}

	private void textBox_TVD_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_TVD);
	}

	private void textBox_TVD_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_TVD);
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
