using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EzyPlan_Interface_V2.Properties;
using RibbonStyle;
using VistaStyleProgressBar;

namespace EzyPlan_Interface_V2;

internal sealed class Splash : Form
{
	private delegate void UpdateProgressIndicatorDelegate();

	public class DisclaimerEventArgs : EventArgs
	{
		private DisclaimerResponses m_disclaimerResponse;

		private Form_MainForm m_mainApplicationForm;

		public Form_MainForm MainApplicationForm
		{
			get
			{
				return m_mainApplicationForm;
			}
			set
			{
				m_mainApplicationForm = value;
			}
		}

		public DisclaimerResponses DisclaimerResponse
		{
			get
			{
				return m_disclaimerResponse;
			}
			set
			{
				m_disclaimerResponse = value;
			}
		}

		public DisclaimerEventArgs()
		{
			m_disclaimerResponse = DisclaimerResponses.NotSet;
		}
	}

	public enum DisclaimerResponses
	{
		NotSet,
		AcceptLicense,
		DeclineLicense
	}

	private Form_MainForm m_applicationMainForm;

	private System.Windows.Forms.Timer fadeTimer;

	internal static Splash Instance;

	private Label label_Status;

	private VistaStyleProgressBar.ProgressBar progressBar1;

	private TextBox textBox1;

	private Label label1;

	private Panel panel1;

	private RibbonMenuButton button_Accept;

	private RibbonMenuButton button_Decline;

	internal static Thread splashThread;

	private IContainer components;

	private bool m_allowClose;

	public static Form_MainForm ApplicationMainForm
	{
		get
		{
			return Instance.m_applicationMainForm;
		}
		set
		{
			Instance.m_applicationMainForm = value;
		}
	}

	public static event EventHandler FadedOut;

	public static event EventHandler DisclaimerResponse;

	public Splash()
	{
		InitializeComponent();
		StartPosition = FormStartPosition.CenterScreen;
		FormBorderStyle = FormBorderStyle.None;
		TopMost = true;
		ShowInTaskbar = true;
		BackColor = Color.Pink;
		TransparencyKey = Color.Pink;
		if (components == null)
		{
			components = new Container();
		}
		fadeTimer = new System.Windows.Forms.Timer(components);
	}

	public void OnFadedOut()
	{
		if (FadedOut != null)
		{
			EventArgs e = new EventArgs();
			FadedOut(this, e);
		}
	}

	private void OnDisclaimerResponse(DisclaimerResponses response)
	{
		DisclaimerEventArgs e = new DisclaimerEventArgs();
		e.DisclaimerResponse = response;
		e.MainApplicationForm = m_applicationMainForm;
		if (DisclaimerResponse != null)
		{
			DisclaimerResponse(null, e);
		}
	}

	private void button_Accept_Click(object sender, EventArgs e)
	{
		OnDisclaimerResponse(DisclaimerResponses.AcceptLicense);
	}

	private void button_Decline_Click(object sender, EventArgs e)
	{
		OnDisclaimerResponse(DisclaimerResponses.DeclineLicense);
	}

	public static void ShowSplash()
	{
		ShowSplash(0);
	}

	public static void ShowSplash(int fadeinTime)
	{
		if (Instance != null)
		{
			return;
		}
		Instance = new Splash();
		Instance.Opacity = 0.0;
		Instance.Show();
		Application.DoEvents();
		if (fadeinTime > 0)
		{
			int num = (int)Math.Round((double)fadeinTime / 20.0);
			Instance.fadeTimer.Interval = num;
			for (int i = 0; i <= fadeinTime; i += num)
			{
				Thread.Sleep(num);
				Instance.Opacity += 0.05;
			}
		}
		else
		{
			Instance.fadeTimer.Interval = 1;
		}
		Instance.Opacity = 1.0;
	}

	public static void Fadeout()
	{
		if (Instance != null)
		{
			Instance.BeginInvoke(new MethodInvoker(Instance.Close));
			Application.DoEvents();
		}
	}

	public static void IncrementProgressIndicator()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = IncrementProgressIndicator;
				Instance.Invoke(method);
			}
			else if (Instance.progressBar1.Value < Instance.progressBar1.MaxValue)
			{
				Instance.progressBar1.Value++;
				Instance.progressBar1.Refresh();
			}
		}
	}

	public static void FinishProgressIndicator()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = FinishProgressIndicator;
				Instance.Invoke(method);
			}
			else
			{
				Instance.progressBar1.Value = Instance.progressBar1.MaxValue;
				Instance.progressBar1.Refresh();
			}
		}
	}

	public static void LoadingInterface()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = LoadingInterface;
				Instance.Invoke(method);
			}
			else
			{
				Instance.label_Status.Text = "Loading Interface...";
				Instance.Refresh();
			}
		}
	}

	public static void EnableDisclaimerResponse()
	{
		Instance.button_Accept.Visible = true;
		Instance.button_Decline.Visible = true;
		WaitingForResponse();
		Application.DoEvents();
	}

	public static void LoadingComponents()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = CheckingLicenseStatus;
				Instance.Invoke(method);
			}
			else
			{
				Instance.label_Status.Text = "Loading components...";
				Instance.Refresh();
			}
		}
	}

	public static void CheckingLicenseStatus()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = CheckingLicenseStatus;
				Instance.Invoke(method);
			}
			else
			{
				Instance.label_Status.Text = "Checking activation status...";
				Instance.Refresh();
			}
		}
	}

	public static void WaitingForResponse()
	{
		if (Instance != null)
		{
			if (Instance.progressBar1.InvokeRequired)
			{
				UpdateProgressIndicatorDelegate method = CheckingLicenseStatus;
				Instance.Invoke(method);
			}
			else
			{
				Instance.label_Status.Text = "Wating for disclaimer response...";
				Instance.Refresh();
			}
		}
	}

	protected override void OnClick(EventArgs e)
	{
		Close();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		if (!m_allowClose)
		{
			return;
		}
		if (fadeTimer.Interval == 1)
		{
			e.Cancel = false;
			OnFadedOut();
		}
		else if (Application.OpenForms.Count > 1)
		{
			if (Opacity > 0.0)
			{
				e.Cancel = true;
				Opacity -= 0.05;
				fadeTimer.Tick -= FadeoutTick;
				fadeTimer.Tick += FadeoutTick;
				fadeTimer.Start();
			}
			else
			{
				e.Cancel = false;
				fadeTimer.Stop();
				OnFadedOut();
				Instance = null;
			}
		}
		else if (Opacity > 0.0)
		{
			Thread.Sleep(fadeTimer.Interval);
			Instance.Opacity -= 0.05;
			Close();
		}
		else
		{
			e.Cancel = false;
			OnFadedOut();
			Instance = null;
		}
	}

	private void FadeoutTick(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EzyPlan_Interface_V2.Splash));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label_Status = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button_Decline = new RibbonStyle.RibbonMenuButton();
		this.progressBar1 = new VistaStyleProgressBar.ProgressBar();
		this.button_Accept = new RibbonStyle.RibbonMenuButton();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.Transparent;
		this.panel1.BackgroundImage = EzyPlan_Interface_V2.Properties.Resources.ezynav2_splash;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.panel1.Controls.Add(this.button_Decline);
		this.panel1.Controls.Add(this.progressBar1);
		this.panel1.Controls.Add(this.button_Accept);
		this.panel1.Controls.Add(this.label_Status);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.textBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(701, 578);
		this.panel1.TabIndex = 9;
		this.label_Status.AutoSize = true;
		this.label_Status.BackColor = System.Drawing.Color.Transparent;
		this.label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label_Status.Location = new System.Drawing.Point(52, 508);
		this.label_Status.Name = "label_Status";
		this.label_Status.Size = new System.Drawing.Size(196, 20);
		this.label_Status.TabIndex = 1;
		this.label_Status.Text = "Loading  components...";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(52, 239);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(240, 20);
		this.label1.TabIndex = 7;
		this.label1.Text = "Terms and conditions of use:";
		this.textBox1.BackColor = System.Drawing.Color.LightYellow;
		this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox1.Location = new System.Drawing.Point(55, 266);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(592, 172);
		this.textBox1.TabIndex = 3;
		this.textBox1.TabStop = false;
		this.textBox1.Text = resources.GetString("textBox1.Text");
		this.button_Decline.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Decline.AutoEllipsis = true;
		this.button_Decline.BackColor = System.Drawing.Color.Transparent;
		this.button_Decline.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Decline.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Decline.ColorOn = System.Drawing.Color.Orange;
		this.button_Decline.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Decline.ColorPress = System.Drawing.Color.Orange;
		this.button_Decline.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Decline.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Decline.FadingSpeed = 35;
		this.button_Decline.FlatAppearance.BorderSize = 0;
		this.button_Decline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Decline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Decline.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Decline.Image = EzyPlan_Interface_V2.Properties.Resources.No;
		this.button_Decline.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Decline.ImageOffset = 4;
		this.button_Decline.IsPressed = false;
		this.button_Decline.KeepPress = false;
		this.button_Decline.Location = new System.Drawing.Point(470, 444);
		this.button_Decline.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Decline.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Decline.Name = "button_Decline";
		this.button_Decline.Radius = 10;
		this.button_Decline.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Decline.Size = new System.Drawing.Size(177, 43);
		this.button_Decline.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Decline.SplitDistance = 0;
		this.button_Decline.TabIndex = 1;
		this.button_Decline.Text = "Decline terms";
		this.button_Decline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Decline.Title = "";
		this.button_Decline.UseVisualStyleBackColor = true;
		this.button_Decline.Visible = false;
		this.button_Decline.Click += new System.EventHandler(this.button_Decline_Click);
		this.button_Decline.Leave += new System.EventHandler(this.button_Decline_Leave);
		this.button_Decline.Enter += new System.EventHandler(this.button_Decline_Enter);
		this.progressBar1.BackColor = System.Drawing.Color.Transparent;
		this.progressBar1.EndColor = System.Drawing.Color.Blue;
		this.progressBar1.Location = new System.Drawing.Point(56, 531);
		this.progressBar1.MaxValue = 15;
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(592, 33);
		this.progressBar1.StartColor = System.Drawing.Color.Red;
		this.progressBar1.TabIndex = 2;
		this.button_Accept.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
		this.button_Accept.AutoEllipsis = true;
		this.button_Accept.BackColor = System.Drawing.Color.Transparent;
		this.button_Accept.ColorBase = System.Drawing.Color.FromArgb(192, 192, 192);
		this.button_Accept.ColorBaseStroke = System.Drawing.Color.Silver;
		this.button_Accept.ColorOn = System.Drawing.Color.Orange;
		this.button_Accept.ColorOnStroke = System.Drawing.Color.Silver;
		this.button_Accept.ColorPress = System.Drawing.Color.Orange;
		this.button_Accept.ColorPressStroke = System.Drawing.Color.Silver;
		this.button_Accept.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_Accept.FadingSpeed = 35;
		this.button_Accept.FlatAppearance.BorderSize = 0;
		this.button_Accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_Accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button_Accept.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
		this.button_Accept.Image = EzyPlan_Interface_V2.Properties.Resources.OK;
		this.button_Accept.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
		this.button_Accept.ImageOffset = 4;
		this.button_Accept.IsPressed = false;
		this.button_Accept.KeepPress = false;
		this.button_Accept.Location = new System.Drawing.Point(287, 444);
		this.button_Accept.MaxImageSize = new System.Drawing.Point(40, 40);
		this.button_Accept.MenuPos = new System.Drawing.Point(0, 0);
		this.button_Accept.Name = "button_Accept";
		this.button_Accept.Radius = 10;
		this.button_Accept.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
		this.button_Accept.Size = new System.Drawing.Size(177, 43);
		this.button_Accept.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
		this.button_Accept.SplitDistance = 0;
		this.button_Accept.TabIndex = 0;
		this.button_Accept.Text = "Accept terms";
		this.button_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button_Accept.Title = "";
		this.button_Accept.UseVisualStyleBackColor = true;
		this.button_Accept.Visible = false;
		this.button_Accept.Click += new System.EventHandler(this.button_Accept_Click);
		this.button_Accept.Leave += new System.EventHandler(this.button_Accept_Leave);
		this.button_Accept.Enter += new System.EventHandler(this.button_Accept_Enter);
		this.BackColor = System.Drawing.SystemColors.ActiveBorder;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		base.ClientSize = new System.Drawing.Size(701, 582);
		base.ControlBox = false;
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Splash";
		this.Text = "SphereIQ Loading";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Layout_PopupSmall_FormClosing);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void button_Accept_Enter(object sender, EventArgs e)
	{
		ButtonGotFocus(Instance.button_Accept);
	}

	private void button_Accept_Leave(object sender, EventArgs e)
	{
		ButtonLostFocus(Instance.button_Accept);
	}

	private void button_Decline_Enter(object sender, EventArgs e)
	{
		ButtonGotFocus(Instance.button_Decline);
	}

	private void button_Decline_Leave(object sender, EventArgs e)
	{
		ButtonLostFocus(Instance.button_Decline);
	}

	public static void ButtonGotFocus(RibbonMenuButton button)
	{
		button.ForeColor = Color.DimGray;
	}

	public static void ButtonLostFocus(RibbonMenuButton button)
	{
		button.ForeColor = Color.Black;
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
}
