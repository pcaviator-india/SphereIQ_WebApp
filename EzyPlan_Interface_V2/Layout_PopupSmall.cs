using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Layout_PopupSmall : Form
{
	private IContainer components;

	public Panel panel_Detail;

	public Label label1;

	public Panel panel_Footer;

	public Panel panel_Header;

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
		this.panel_Header = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panel_Footer = new System.Windows.Forms.Panel();
		this.panel_Detail = new System.Windows.Forms.Panel();
		this.panel_Header.SuspendLayout();
		base.SuspendLayout();
		this.panel_Header.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_Header.Controls.Add(this.label1);
		this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_Header.Location = new System.Drawing.Point(0, 0);
		this.panel_Header.Name = "panel_Header";
		this.panel_Header.Size = new System.Drawing.Size(626, 38);
		this.panel_Header.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(3, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(262, 24);
		this.label1.TabIndex = 0;
		this.label1.Text = "Instructions text goes here!";
		this.panel_Footer.BackColor = System.Drawing.Color.GhostWhite;
		this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel_Footer.Location = new System.Drawing.Point(0, 217);
		this.panel_Footer.Name = "panel_Footer";
		this.panel_Footer.Size = new System.Drawing.Size(626, 56);
		this.panel_Footer.TabIndex = 2;
		this.panel_Detail.BackColor = System.Drawing.Color.White;
		this.panel_Detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel_Detail.Location = new System.Drawing.Point(0, 38);
		this.panel_Detail.Name = "panel_Detail";
		this.panel_Detail.Size = new System.Drawing.Size(626, 179);
		this.panel_Detail.TabIndex = 1;
		base.ClientSize = new System.Drawing.Size(626, 273);
		base.ControlBox = false;
		base.Controls.Add(this.panel_Detail);
		base.Controls.Add(this.panel_Footer);
		base.Controls.Add(this.panel_Header);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Layout_PopupSmall";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Popup";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Layout_PopupSmall_FormClosing);
		this.panel_Header.ResumeLayout(false);
		this.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}

	public Layout_PopupSmall()
	{
		InitializeComponent();
	}

	private void Layout_PopupSmall_FormClosing(object sender, FormClosingEventArgs e)
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
