using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Layout_Popup_Message : Form
{
	private IContainer components;

	public Panel panel_Footer;

	public Panel panel_Detail;

	public RichTextBox richTextBox_text;

	private bool m_allowClose;

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
		this.panel_Footer = new System.Windows.Forms.Panel();
		this.panel_Detail = new System.Windows.Forms.Panel();
		this.richTextBox_text = new System.Windows.Forms.RichTextBox();
		this.panel_Detail.SuspendLayout();
		base.SuspendLayout();
		this.panel_Footer.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_Footer.Location = new System.Drawing.Point(0, 120);
		this.panel_Footer.Name = "panel_Footer";
		this.panel_Footer.Size = new System.Drawing.Size(499, 56);
		this.panel_Footer.TabIndex = 2;
		this.panel_Detail.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_Detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_Detail.Controls.Add(this.richTextBox_text);
		this.panel_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Detail.Location = new System.Drawing.Point(0, 0);
		this.panel_Detail.Name = "panel_Detail";
		this.panel_Detail.Padding = new System.Windows.Forms.Padding(15);
		this.panel_Detail.Size = new System.Drawing.Size(499, 120);
		this.panel_Detail.TabIndex = 3;
		this.richTextBox_text.BackColor = System.Drawing.Color.GhostWhite;
		this.richTextBox_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.richTextBox_text.Dock = System.Windows.Forms.DockStyle.Fill;
		this.richTextBox_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.richTextBox_text.Location = new System.Drawing.Point(15, 15);
		this.richTextBox_text.Name = "richTextBox_text";
		this.richTextBox_text.ReadOnly = true;
		this.richTextBox_text.Size = new System.Drawing.Size(467, 88);
		this.richTextBox_text.TabIndex = 1;
		this.richTextBox_text.TabStop = false;
		this.richTextBox_text.Text = "null";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
		base.ClientSize = new System.Drawing.Size(499, 176);
		base.ControlBox = false;
		base.Controls.Add(this.panel_Detail);
		base.Controls.Add(this.panel_Footer);
		base.Name = "Layout_Popup_Message";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "null";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Layout_Popup_Message_FormClosing);
		this.panel_Detail.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Layout_Popup_Message()
	{
		InitializeComponent();
	}

	private void Layout_Popup_Message_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			if (m_allowClose)
			{
				m_allowClose = false;
			}
			else
			{
				e.Cancel = true;
			}
		}
	}

	public new void Close()
	{
		m_allowClose = true;
		base.Close();
	}

	protected void AllowClose()
	{
		m_allowClose = true;
	}

	protected void PreventClose()
	{
		m_allowClose = false;
	}
}
