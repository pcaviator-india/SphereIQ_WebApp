using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavMath;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate_ToInclination : Popup_Extrapolate
{
	private IContainer components;

	private TextBox textBox_Dogleg;

	private TextBox textBox_Toolface;

	private TextBox textBox_Inclination;

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
		this.textBox_Dogleg = new System.Windows.Forms.TextBox();
		this.textBox_Toolface = new System.Windows.Forms.TextBox();
		this.textBox_Inclination = new System.Windows.Forms.TextBox();
		this.label_DL = new System.Windows.Forms.Label();
		this.label_TFS = new System.Windows.Forms.Label();
		this.label_MD = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.label_subText.Size = new System.Drawing.Size(735, 48);
		base.panel_Detail.Controls.Add(this.textBox_Dogleg);
		base.panel_Detail.Controls.Add(this.textBox_Toolface);
		base.panel_Detail.Controls.Add(this.textBox_Inclination);
		base.panel_Detail.Controls.Add(this.label_DL);
		base.panel_Detail.Controls.Add(this.label_TFS);
		base.panel_Detail.Controls.Add(this.label_MD);
		base.panel_Detail.Location = new System.Drawing.Point(0, 75);
		base.panel_Detail.Size = new System.Drawing.Size(806, 125);
		base.label1.Size = new System.Drawing.Size(459, 24);
		base.label1.Text = "Enter the required values to extrapolate to a dip:";
		base.panel_Footer.Location = new System.Drawing.Point(0, 200);
		base.panel_Footer.Size = new System.Drawing.Size(806, 56);
		base.panel_Header.Size = new System.Drawing.Size(806, 75);
		this.textBox_Dogleg.BackColor = System.Drawing.Color.White;
		this.textBox_Dogleg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Dogleg.Location = new System.Drawing.Point(253, 64);
		this.textBox_Dogleg.Name = "textBox_Dogleg";
		this.textBox_Dogleg.Size = new System.Drawing.Size(112, 29);
		this.textBox_Dogleg.TabIndex = 1;
		this.textBox_Dogleg.Enter += new System.EventHandler(this.textBox_Dogleg_Enter);
		this.textBox_Dogleg.Leave += new System.EventHandler(this.textBox_Dogleg_Leave);
		this.textBox_Toolface.BackColor = System.Drawing.Color.White;
		this.textBox_Toolface.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Toolface.Location = new System.Drawing.Point(497, 64);
		this.textBox_Toolface.Name = "textBox_Toolface";
		this.textBox_Toolface.Size = new System.Drawing.Size(115, 29);
		this.textBox_Toolface.TabIndex = 2;
		this.textBox_Toolface.Enter += new System.EventHandler(this.textBox_Toolface_Enter);
		this.textBox_Toolface.Leave += new System.EventHandler(this.textBox_Toolface_Leave);
		this.textBox_Inclination.BackColor = System.Drawing.Color.White;
		this.textBox_Inclination.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Inclination.Location = new System.Drawing.Point(35, 64);
		this.textBox_Inclination.Name = "textBox_Inclination";
		this.textBox_Inclination.Size = new System.Drawing.Size(151, 29);
		this.textBox_Inclination.TabIndex = 0;
		this.textBox_Inclination.Enter += new System.EventHandler(this.textBox_Inclination_Enter);
		this.textBox_Inclination.Leave += new System.EventHandler(this.textBox_Inclination_Leave);
		this.label_DL.AutoSize = true;
		this.label_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DL.Location = new System.Drawing.Point(249, 37);
		this.label_DL.Name = "label_DL";
		this.label_DL.Size = new System.Drawing.Size(206, 24);
		this.label_DL.TabIndex = 20;
		this.label_DL.Text = "Dogleg (Degrees/30m):";
		this.label_TFS.AutoSize = true;
		this.label_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TFS.Location = new System.Drawing.Point(493, 37);
		this.label_TFS.Name = "label_TFS";
		this.label_TFS.Size = new System.Drawing.Size(239, 24);
		this.label_TFS.TabIndex = 21;
		this.label_TFS.Text = "Toolface Setting (Degrees):";
		this.label_MD.AutoSize = true;
		this.label_MD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_MD.Location = new System.Drawing.Point(31, 37);
		this.label_MD.Name = "label_MD";
		this.label_MD.Size = new System.Drawing.Size(132, 24);
		this.label_MD.TabIndex = 22;
		this.label_MD.Text = "Dip (Degrees):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(806, 256);
		base.Name = "Popup_Extrapolate_ToInclination";
		this.Text = "Extrapolate to dip";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Popup_Extrapolate_ToInclination(AbsoluteSurvey lastSurvey)
		: base(lastSurvey)
	{
		InitializeComponent();
	}

	protected override void SaveExtrapolatedSurvey(out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		new NavMath();
		lastExtrapolatedSurvey = null;
		if (Utility.ConvertUserEnteredValue(textBox_Inclination.Text, out var value, "Inclination"))
		{
			if (Utility.ConvertUserEnteredValue(textBox_Dogleg.Text, out var value2, "Dogleg"))
			{
				if (Utility.ConvertUserEnteredValue(textBox_Toolface.Text, out var value3, "Toolface"))
				{
					if (CalculateExtrapolation(value, value2, value3, LastSurvey, out lastExtrapolatedSurvey))
					{
						textBox_Inclination.Focus();
					}
				}
				else
				{
					textBox_Toolface.Focus();
				}
			}
			else
			{
				textBox_Dogleg.Focus();
			}
		}
		else
		{
			textBox_Inclination.Focus();
		}
	}

	private bool CalculateExtrapolation(decimal inclination, decimal dogleg, decimal toolface, AbsoluteSurvey LastSurvey, out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		NavMath navMath = new NavMath();
		lastExtrapolatedSurvey = null;
		NavMath.ExtrapolateToInclinationResultEnum extrapolateToInclinationResultEnum = navMath.ExtrapolateToInclination(inclination, dogleg, toolface, LastSurvey, out lastExtrapolatedSurvey);
		if (extrapolateToInclinationResultEnum == NavMath.ExtrapolateToInclinationResultEnum.NoError)
		{
			return true;
		}
		bool flag = false;
		switch (extrapolateToInclinationResultEnum)
		{
		case NavMath.ExtrapolateToInclinationResultEnum.CannotAchieveIncWithTFS:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Toolface.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.DipTooGreat:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.IncSameAsPrevious:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.InvalidDogleg:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Dogleg.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.InvalidInclination:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.InvalidToolface:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Toolface.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.MaximumMDReachedBeforeInclination:
			DisplayError(extrapolateToInclinationResultEnum);
			flag = true;
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.PathCannotBeAchieved:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.UnknownError:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		case NavMath.ExtrapolateToInclinationResultEnum.UnknownException:
			DisplayError(extrapolateToInclinationResultEnum);
			textBox_Inclination.Focus();
			break;
		}
		if (!flag)
		{
			lastExtrapolatedSurvey = null;
		}
		return flag;
	}

	private void DisplayError(NavMath.ExtrapolateToInclinationResultEnum result)
	{
		Utility.DisplayErrorPopup("Error generating extrapolated survey", NavMath.ToHumanReadable(result) + ".");
	}

	protected override void ResetForDataEntry()
	{
		textBox_Inclination.Focus();
	}

	private void textBox_Inclination_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Inclination);
	}

	private void textBox_Inclination_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Inclination);
	}

	private void textBox_Dogleg_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Dogleg);
	}

	private void textBox_Dogleg_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Dogleg);
	}

	private void textBox_Toolface_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Toolface);
	}

	private void textBox_Toolface_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Toolface);
	}
}
