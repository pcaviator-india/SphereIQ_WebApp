using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyNavMath;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Popup_Extrapolate_ToDirection : Popup_Extrapolate
{
	private IContainer components;

	private TextBox textBox_DL;

	private TextBox textBox_Direction;

	private TextBox textBox_Dip;

	private Label label_DL;

	private Label label_TFS;

	private Label label_Dip;

	public Popup_Extrapolate_ToDirection(AbsoluteSurvey lastSurvey)
		: base(lastSurvey)
	{
		InitializeComponent();
	}

	protected override void SaveExtrapolatedSurvey(out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		new NavMath();
		lastExtrapolatedSurvey = null;
		if (Utility.ConvertUserEnteredValue(textBox_Direction.Text, out var value, "Direction"))
		{
			if (Utility.ConvertUserEnteredValue(textBox_Dip.Text, out var value2, "Dip"))
			{
				if (Utility.ConvertUserEnteredValue(textBox_DL.Text, out var value3, "Dogleg"))
				{
					if (CalculateExtrapolation(value, value2, value3, LastSurvey, out lastExtrapolatedSurvey))
					{
						textBox_Direction.Focus();
					}
				}
				else
				{
					textBox_DL.Focus();
				}
			}
			else
			{
				textBox_DL.Focus();
			}
		}
		else
		{
			textBox_Direction.Focus();
		}
	}

	private bool CalculateExtrapolation(decimal direction, decimal dip, decimal dogleg, AbsoluteSurvey LastSurvey, out ExtrapolatedAbsoluteSurvey lastExtrapolatedSurvey)
	{
		NavMath navMath = new NavMath();
		lastExtrapolatedSurvey = null;
		NavMath.ExtrapolateToDirectionResultEnum extrapolateToDirectionResultEnum = navMath.ExtrapolateToDirection(dip, direction, dogleg, LastSurvey, out lastExtrapolatedSurvey);
		if (extrapolateToDirectionResultEnum == NavMath.ExtrapolateToDirectionResultEnum.NoError)
		{
			return true;
		}
		bool flag = false;
		switch (extrapolateToDirectionResultEnum)
		{
		case NavMath.ExtrapolateToDirectionResultEnum.DepthTooGreatPickHigherDogLeg:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_DL.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.DipTooGreat:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.InvalidDirection:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.InvalidDogleg:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_DL.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.InvalidInclination:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Dip.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.PathCannotBeAchieved:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.AlreadyThere:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.UnknownError:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		case NavMath.ExtrapolateToDirectionResultEnum.UnknownException:
			DisplayError(extrapolateToDirectionResultEnum);
			textBox_Direction.Focus();
			break;
		}
		if (!flag)
		{
			lastExtrapolatedSurvey = null;
		}
		return flag;
	}

	private void DisplayError(NavMath.ExtrapolateToDirectionResultEnum result)
	{
		Utility.DisplayErrorPopup("Error generating extrapolated survey", NavMath.ToHumanReadable(result) + ".");
	}

	protected override void ResetForDataEntry()
	{
		textBox_Direction.Focus();
	}

	private void textBox_Direction_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Direction);
	}

	private void textBox_Direction_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Direction);
	}

	private void textBox_Inclination_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_Dip);
	}

	private void textBox_Inclination_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_Dip);
	}

	private void textBox_DL_Enter(object sender, EventArgs e)
	{
		Utility.TextBoxGotFocus(ref textBox_DL);
	}

	private void textBox_DL_Leave(object sender, EventArgs e)
	{
		Utility.TextBoxLostFocus(ref textBox_DL);
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
		this.textBox_DL = new System.Windows.Forms.TextBox();
		this.textBox_Direction = new System.Windows.Forms.TextBox();
		this.textBox_Dip = new System.Windows.Forms.TextBox();
		this.label_DL = new System.Windows.Forms.Label();
		this.label_TFS = new System.Windows.Forms.Label();
		this.label_Dip = new System.Windows.Forms.Label();
		base.panel_Detail.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.label_subText.Size = new System.Drawing.Size(730, 49);
		base.panel_Detail.Controls.Add(this.textBox_DL);
		base.panel_Detail.Controls.Add(this.textBox_Direction);
		base.panel_Detail.Controls.Add(this.textBox_Dip);
		base.panel_Detail.Controls.Add(this.label_DL);
		base.panel_Detail.Controls.Add(this.label_TFS);
		base.panel_Detail.Controls.Add(this.label_Dip);
		base.panel_Detail.Location = new System.Drawing.Point(0, 76);
		base.panel_Detail.Size = new System.Drawing.Size(803, 127);
		base.label1.Size = new System.Drawing.Size(573, 24);
		base.label1.Text = "Enter the required values below to extrapolate to a direction:";
		base.panel_Footer.Location = new System.Drawing.Point(0, 203);
		base.panel_Footer.Size = new System.Drawing.Size(803, 56);
		base.panel_Header.Size = new System.Drawing.Size(803, 76);
		this.textBox_DL.BackColor = System.Drawing.Color.White;
		this.textBox_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_DL.Location = new System.Drawing.Point(540, 62);
		this.textBox_DL.Name = "textBox_DL";
		this.textBox_DL.Size = new System.Drawing.Size(112, 29);
		this.textBox_DL.TabIndex = 2;
		this.textBox_DL.Enter += new System.EventHandler(this.textBox_DL_Enter);
		this.textBox_DL.Leave += new System.EventHandler(this.textBox_DL_Leave);
		this.textBox_Direction.BackColor = System.Drawing.Color.White;
		this.textBox_Direction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Direction.Location = new System.Drawing.Point(28, 62);
		this.textBox_Direction.Name = "textBox_Direction";
		this.textBox_Direction.Size = new System.Drawing.Size(130, 29);
		this.textBox_Direction.TabIndex = 0;
		this.textBox_Direction.Enter += new System.EventHandler(this.textBox_Direction_Enter);
		this.textBox_Direction.Leave += new System.EventHandler(this.textBox_Direction_Leave);
		this.textBox_Dip.BackColor = System.Drawing.Color.White;
		this.textBox_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Dip.Location = new System.Drawing.Point(272, 62);
		this.textBox_Dip.Name = "textBox_Dip";
		this.textBox_Dip.Size = new System.Drawing.Size(151, 29);
		this.textBox_Dip.TabIndex = 1;
		this.textBox_Dip.Enter += new System.EventHandler(this.textBox_Inclination_Enter);
		this.textBox_Dip.Leave += new System.EventHandler(this.textBox_Inclination_Leave);
		this.label_DL.AutoSize = true;
		this.label_DL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_DL.Location = new System.Drawing.Point(536, 35);
		this.label_DL.Name = "label_DL";
		this.label_DL.Size = new System.Drawing.Size(206, 24);
		this.label_DL.TabIndex = 20;
		this.label_DL.Text = "Dogleg (Degrees/30m):";
		this.label_TFS.AutoSize = true;
		this.label_TFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_TFS.Location = new System.Drawing.Point(24, 35);
		this.label_TFS.Name = "label_TFS";
		this.label_TFS.Size = new System.Drawing.Size(178, 24);
		this.label_TFS.TabIndex = 21;
		this.label_TFS.Text = "Direction (Degrees):";
		this.label_Dip.AutoSize = true;
		this.label_Dip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Dip.Location = new System.Drawing.Point(268, 35);
		this.label_Dip.Name = "label_Dip";
		this.label_Dip.Size = new System.Drawing.Size(132, 24);
		this.label_Dip.TabIndex = 22;
		this.label_Dip.Text = "Dip (Degrees):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(803, 259);
		base.Name = "Popup_Extrapolate_ToDirection";
		this.Text = "Extrapolate to direction";
		base.panel_Detail.ResumeLayout(false);
		base.panel_Detail.PerformLayout();
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}
}
