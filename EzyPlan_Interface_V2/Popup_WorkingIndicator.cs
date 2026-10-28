using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;
using VistaStyleProgressBar;

namespace EzyPlan_Interface_V2;

internal class Popup_WorkingIndicator : Layout_PopupSmall
{
	public enum CancelStates
	{
		Pending,
		Canceled
	}

	private delegate void SetInvoker(CancelStates state);

	private delegate void GetInvoker(out CancelStates state);

	private delegate void SetStringInvoker(string text);

	public class CancelStateArgs
	{
		public bool cancelled;

		public CancelStateArgs()
		{
			cancelled = false;
		}
	}

	private delegate void UpdateProgressDelegate(string stateText);

	private delegate void UpdateProgressDelegate2(string stateText, int addToProgress);

	private delegate void GetStatusDelegate(ref CancelStateArgs cancelState);

	private CancelStates m_cancelState;

	private IContainer components;

	private Label label_StatusText;

	private RibbonMenuButton button_Cancel;

	private VistaStyleProgressBar.ProgressBar progressBar1;

	public CancelStates CancelState
	{
		get
		{
			GetCancelState(out var result);
			return result;
		}
		set
		{
			SetCancelState(value);
		}
	}

	public string StatusText
	{
		set
		{
			SetStatusText(value);
		}
	}

	public Popup_WorkingIndicator(string title, string header, bool showCancelButton)
	{
		InitializeComponent();
		label1.Text = header;
		Text = title;
		m_cancelState = CancelStates.Pending;
		button_Cancel.Visible = showCancelButton;
	}

	public Popup_WorkingIndicator(string title, string header, bool showCancelButton, int totalProgress)
		: this(title, header, showCancelButton)
	{
		progressBar1.Value = 0;
		progressBar1.MaxValue = totalProgress;
	}

	private void SetCancelState(CancelStates state)
	{
		if (InvokeRequired)
		{
			SetInvoker method = SetCancelState;
			Invoke(method, state);
			return;
		}
		m_cancelState = state;
		if (state == CancelStates.Canceled)
		{
			label_StatusText.Text = "Cancelling...";
		}
	}

	private void GetCancelState(out CancelStates state)
	{
		state = CancelStates.Pending;
		if (InvokeRequired)
		{
			GetInvoker method = GetCancelState;
			Invoke(method, state);
		}
		else
		{
			state = m_cancelState;
		}
	}

	private void SetStatusText(string text)
	{
		if (InvokeRequired)
		{
			SetStringInvoker method = SetStatusText;
			Invoke(method, text);
		}
		else
		{
			label_StatusText.Text = text;
		}
	}

	public new void Close()
	{
		if (InvokeRequired)
		{
			MethodInvoker method = Close;
			Invoke(method);
		}
		else
		{
			base.Close();
		}
	}

	public void UpdateProgress(string stateText)
	{
		if (InvokeRequired)
		{
			UpdateProgressDelegate method = UpdateProgress;
			Invoke(method, stateText);
		}
		else
		{
			SetStatusText(stateText);
		}
	}

	public void UpdateProgress(string stateText, int addToProgress)
	{
		if (InvokeRequired)
		{
			UpdateProgressDelegate2 method = UpdateProgress;
			Invoke(method, stateText, addToProgress);
		}
		else
		{
			UpdateProgress(stateText);
			progressBar1.Value += addToProgress;
		}
	}

	public void GetCancelStatus(ref CancelStateArgs cancelState)
	{
		if (InvokeRequired)
		{
			GetStatusDelegate method = GetCancelStatus;
			Invoke(method, cancelState);
		}
		else if (m_cancelState == CancelStates.Canceled)
		{
			cancelState.cancelled = true;
		}
		else
		{
			cancelState.cancelled = false;
		}
	}

	private void Popup_WorkingIndicator_KeyDown(object sender, KeyEventArgs e)
	{
		e.SuppressKeyPress = true;
	}

	private void button_Cancel_Click(object sender, EventArgs e)
	{
		CancelState = CancelStates.Canceled;
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
		this.label_StatusText = new System.Windows.Forms.Label();
		this.button_Cancel = new RibbonStyle.RibbonMenuButton();
		this.progressBar1 = new VistaStyleProgressBar.ProgressBar();
		base.panel_Detail.SuspendLayout();
		base.panel_Footer.SuspendLayout();
		base.panel_Header.SuspendLayout();
		base.SuspendLayout();
		base.panel_Detail.Controls.Add(this.progressBar1);
		base.panel_Detail.Size = new System.Drawing.Size(578, 61);
		base.panel_Footer.Controls.Add(this.button_Cancel);
		base.panel_Footer.Controls.Add(this.label_StatusText);
		base.panel_Footer.Location = new System.Drawing.Point(0, 99);
		base.panel_Footer.Size = new System.Drawing.Size(578, 46);
		base.panel_Footer.TabIndex = 4;
		base.panel_Header.Size = new System.Drawing.Size(578, 38);
		base.panel_Header.TabIndex = 3;
		this.label_StatusText.AutoSize = true;
		this.label_StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_StatusText.Location = new System.Drawing.Point(3, 18);
		this.label_StatusText.Name = "label_StatusText";
		this.label_StatusText.Size = new System.Drawing.Size(123, 24);
		this.label_StatusText.TabIndex = 0;
		this.label_StatusText.Text = "Please Wait...";
		this.button_Cancel.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Cancel.AutoEllipsis = true;
		this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
		this.button_Cancel.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Cancel.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Cancel.ColorOn = System.Drawing.Color.Orange;
		this.button_Cancel.ColorOnStroke = System.Drawing.Color.FromArgb(100, 153, 99, 0);
		this.button_Cancel.ColorPress = System.Drawing.Color.Orange;
		this.button_Cancel.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Cancel.FadingSpeed = 35;
		this.button_Cancel.FlatAppearance.BorderSize = 0;
		this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Cancel.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Cancel.Image = EzyPlan_Interface_V2.Properties.Resources.OK;
		this.button_Cancel.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Cancel.ImageOffset = 0;
		this.button_Cancel.IsPressed = false;
		this.button_Cancel.KeepPress = false;
		this.button_Cancel.Location = new System.Drawing.Point(422, 3);
		this.button_Cancel.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Cancel.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Cancel.Name = "button_Cancel";
		this.button_Cancel.Radius = 10;
		this.button_Cancel.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Cancel.Size = new System.Drawing.Size(148, 39);
		this.button_Cancel.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Cancel.SplitDistance = 0;
		this.button_Cancel.TabIndex = 0;
		this.button_Cancel.Text = "Cancel";
		this.button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Cancel.Title = "";
		this.button_Cancel.UseVisualStyleBackColor = true;
		this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
		this.progressBar1.BackColor = System.Drawing.Color.Transparent;
		this.progressBar1.Location = new System.Drawing.Point(12, 15);
		this.progressBar1.MaxValue = 1000;
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(554, 32);
		this.progressBar1.TabIndex = 0;
		this.progressBar1.TabStop = false;
		this.progressBar1.Value = 999;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(578, 145);
		this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
		base.Name = "Popup_WorkingIndicator";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		this.Text = "null";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Popup_WorkingIndicator_KeyDown);
		base.panel_Detail.ResumeLayout(false);
		base.panel_Footer.ResumeLayout(false);
		base.panel_Footer.PerformLayout();
		base.panel_Header.ResumeLayout(false);
		base.panel_Header.PerformLayout();
		base.ResumeLayout(false);
	}
}
