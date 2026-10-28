using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;

namespace EzyPlan_Interface_V2;

public class Info_BuildResults : UserControl
{
	private IContainer components;

	private GroupBox groupBox_Results;

	private TextBox textBox_DogLeg;

	private Label label_Dogleg;

	private Label label20;

	private Label label_Toolface;

	private Label label3;

	private TextBox textBox_ToolFace;

	private Label label_NaviStart;

	private TextBox textBox_NaviStart;

	private Label label18;

	private Label label19;

	private TextBox textBox_NaviStop;

	private Label label_NaviStop;

	private Label label1;

	private Label label17;

	private TextBox textBox_NaviDistance;

	private Label label2;

	private TextBox textBox_StraightDistance;

	private Label label16;

	public BuildAndHoldResults BuildResults
	{
		set
		{
			textBox_ToolFace.Text = value.ToolFaceSetting.ToString();
			textBox_DogLeg.Text = value.DogLeg.ToString();
			textBox_NaviStart.Text = value.NaviStartMVD.ToString();
			textBox_NaviStop.Text = value.NaviStopMVD.ToString();
			textBox_NaviDistance.Text = value.NaviDistance.ToString();
			textBox_StraightDistance.Text = value.StraightDistance.ToString();
			Refresh();
		}
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
		this.groupBox_Results = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label_NaviStart = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox_NaviDistance = new System.Windows.Forms.TextBox();
		this.textBox_NaviStart = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.textBox_StraightDistance = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label_Toolface = new System.Windows.Forms.Label();
		this.textBox_NaviStop = new System.Windows.Forms.TextBox();
		this.label_NaviStop = new System.Windows.Forms.Label();
		this.textBox_DogLeg = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label_Dogleg = new System.Windows.Forms.Label();
		this.textBox_ToolFace = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox_Results.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_Results.Controls.Add(this.label1);
		this.groupBox_Results.Controls.Add(this.label_NaviStart);
		this.groupBox_Results.Controls.Add(this.label17);
		this.groupBox_Results.Controls.Add(this.label2);
		this.groupBox_Results.Controls.Add(this.textBox_NaviDistance);
		this.groupBox_Results.Controls.Add(this.textBox_NaviStart);
		this.groupBox_Results.Controls.Add(this.label18);
		this.groupBox_Results.Controls.Add(this.label19);
		this.groupBox_Results.Controls.Add(this.textBox_StraightDistance);
		this.groupBox_Results.Controls.Add(this.label16);
		this.groupBox_Results.Controls.Add(this.label_Toolface);
		this.groupBox_Results.Controls.Add(this.textBox_NaviStop);
		this.groupBox_Results.Controls.Add(this.label_NaviStop);
		this.groupBox_Results.Controls.Add(this.textBox_DogLeg);
		this.groupBox_Results.Controls.Add(this.label3);
		this.groupBox_Results.Controls.Add(this.label_Dogleg);
		this.groupBox_Results.Controls.Add(this.textBox_ToolFace);
		this.groupBox_Results.Controls.Add(this.label20);
		this.groupBox_Results.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox_Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox_Results.Location = new System.Drawing.Point(0, 0);
		this.groupBox_Results.Name = "groupBox_Results";
		this.groupBox_Results.Size = new System.Drawing.Size(567, 235);
		this.groupBox_Results.TabIndex = 4;
		this.groupBox_Results.TabStop = false;
		this.groupBox_Results.Text = "Drilling instructrions:";
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(7, 164);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(115, 20);
		this.label1.TabIndex = 3;
		this.label1.Text = "Build Distance:";
		this.label_NaviStart.AutoSize = true;
		this.label_NaviStart.ForeColor = System.Drawing.Color.Black;
		this.label_NaviStart.Location = new System.Drawing.Point(7, 102);
		this.label_NaviStart.Name = "label_NaviStart";
		this.label_NaviStart.Size = new System.Drawing.Size(116, 20);
		this.label_NaviStart.TabIndex = 2;
		this.label_NaviStart.Text = "Build Start MD:";
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.ForeColor = System.Drawing.Color.Red;
		this.label17.Location = new System.Drawing.Point(126, 193);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(49, 16);
		this.label17.TabIndex = 16;
		this.label17.Text = "meters";
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(278, 164);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(136, 20);
		this.label2.TabIndex = 3;
		this.label2.Text = "Straight Distance:";
		this.textBox_NaviDistance.BackColor = System.Drawing.Color.White;
		this.textBox_NaviDistance.ForeColor = System.Drawing.Color.Black;
		this.textBox_NaviDistance.Location = new System.Drawing.Point(129, 164);
		this.textBox_NaviDistance.Multiline = true;
		this.textBox_NaviDistance.Name = "textBox_NaviDistance";
		this.textBox_NaviDistance.ReadOnly = true;
		this.textBox_NaviDistance.Size = new System.Drawing.Size(137, 26);
		this.textBox_NaviDistance.TabIndex = 15;
		this.textBox_NaviDistance.TabStop = false;
		this.textBox_NaviStart.BackColor = System.Drawing.Color.White;
		this.textBox_NaviStart.ForeColor = System.Drawing.Color.Black;
		this.textBox_NaviStart.Location = new System.Drawing.Point(129, 102);
		this.textBox_NaviStart.Multiline = true;
		this.textBox_NaviStart.Name = "textBox_NaviStart";
		this.textBox_NaviStart.ReadOnly = true;
		this.textBox_NaviStart.Size = new System.Drawing.Size(137, 26);
		this.textBox_NaviStart.TabIndex = 10;
		this.textBox_NaviStart.TabStop = false;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.ForeColor = System.Drawing.Color.Red;
		this.label18.Location = new System.Drawing.Point(126, 131);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(49, 16);
		this.label18.TabIndex = 16;
		this.label18.Text = "meters";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.ForeColor = System.Drawing.Color.Red;
		this.label19.Location = new System.Drawing.Point(414, 128);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(49, 16);
		this.label19.TabIndex = 16;
		this.label19.Text = "meters";
		this.textBox_StraightDistance.BackColor = System.Drawing.Color.White;
		this.textBox_StraightDistance.ForeColor = System.Drawing.Color.Black;
		this.textBox_StraightDistance.Location = new System.Drawing.Point(417, 164);
		this.textBox_StraightDistance.Multiline = true;
		this.textBox_StraightDistance.Name = "textBox_StraightDistance";
		this.textBox_StraightDistance.ReadOnly = true;
		this.textBox_StraightDistance.Size = new System.Drawing.Size(134, 26);
		this.textBox_StraightDistance.TabIndex = 15;
		this.textBox_StraightDistance.TabStop = false;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.Red;
		this.label16.Location = new System.Drawing.Point(414, 193);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(49, 16);
		this.label16.TabIndex = 16;
		this.label16.Text = "meters";
		this.label_Toolface.AutoSize = true;
		this.label_Toolface.ForeColor = System.Drawing.Color.Black;
		this.label_Toolface.Location = new System.Drawing.Point(289, 25);
		this.label_Toolface.Name = "label_Toolface";
		this.label_Toolface.Size = new System.Drawing.Size(74, 20);
		this.label_Toolface.TabIndex = 8;
		this.label_Toolface.Text = "Toolface:";
		this.textBox_NaviStop.BackColor = System.Drawing.Color.White;
		this.textBox_NaviStop.ForeColor = System.Drawing.Color.Black;
		this.textBox_NaviStop.Location = new System.Drawing.Point(416, 102);
		this.textBox_NaviStop.Multiline = true;
		this.textBox_NaviStop.Name = "textBox_NaviStop";
		this.textBox_NaviStop.ReadOnly = true;
		this.textBox_NaviStop.Size = new System.Drawing.Size(135, 26);
		this.textBox_NaviStop.TabIndex = 11;
		this.textBox_NaviStop.TabStop = false;
		this.label_NaviStop.AutoSize = true;
		this.label_NaviStop.ForeColor = System.Drawing.Color.Black;
		this.label_NaviStop.Location = new System.Drawing.Point(278, 102);
		this.label_NaviStop.Name = "label_NaviStop";
		this.label_NaviStop.Size = new System.Drawing.Size(115, 20);
		this.label_NaviStop.TabIndex = 5;
		this.label_NaviStop.Text = "Build Stop MD:";
		this.textBox_DogLeg.BackColor = System.Drawing.Color.White;
		this.textBox_DogLeg.ForeColor = System.Drawing.Color.Black;
		this.textBox_DogLeg.Location = new System.Drawing.Point(93, 26);
		this.textBox_DogLeg.Multiline = true;
		this.textBox_DogLeg.Name = "textBox_DogLeg";
		this.textBox_DogLeg.ReadOnly = true;
		this.textBox_DogLeg.Size = new System.Drawing.Size(179, 26);
		this.textBox_DogLeg.TabIndex = 13;
		this.textBox_DogLeg.TabStop = false;
		this.textBox_DogLeg.Text = "123456879";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Red;
		this.label3.Location = new System.Drawing.Point(371, 54);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(154, 16);
		this.label3.TabIndex = 16;
		this.label3.Text = "degrees off the high side";
		this.label_Dogleg.AutoSize = true;
		this.label_Dogleg.ForeColor = System.Drawing.Color.Black;
		this.label_Dogleg.Location = new System.Drawing.Point(19, 26);
		this.label_Dogleg.Name = "label_Dogleg";
		this.label_Dogleg.Size = new System.Drawing.Size(68, 20);
		this.label_Dogleg.TabIndex = 7;
		this.label_Dogleg.Text = "Dogleg: ";
		this.textBox_ToolFace.BackColor = System.Drawing.Color.White;
		this.textBox_ToolFace.ForeColor = System.Drawing.Color.Black;
		this.textBox_ToolFace.Location = new System.Drawing.Point(374, 25);
		this.textBox_ToolFace.Multiline = true;
		this.textBox_ToolFace.Name = "textBox_ToolFace";
		this.textBox_ToolFace.ReadOnly = true;
		this.textBox_ToolFace.Size = new System.Drawing.Size(177, 26);
		this.textBox_ToolFace.TabIndex = 14;
		this.textBox_ToolFace.TabStop = false;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.Red;
		this.label20.Location = new System.Drawing.Point(90, 55);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(94, 16);
		this.label20.TabIndex = 16;
		this.label20.Text = "degrees / 30m";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.groupBox_Results);
		base.Name = "Info_BuildResults";
		base.Size = new System.Drawing.Size(567, 235);
		this.groupBox_Results.ResumeLayout(false);
		this.groupBox_Results.PerformLayout();
		base.ResumeLayout(false);
	}

	public Info_BuildResults()
	{
		InitializeComponent();
	}
}
