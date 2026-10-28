using System;
using System.Threading;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

internal static class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		string text = "";
		if (args.GetLength(0) > 0)
		{
			foreach (string text2 in args)
			{
				text = text + " " + text2;
			}
		}
		Mutex mutex = null;
		try
		{
			mutex = Mutex.OpenExisting("PCAVIATOR SphereIQ");
		}
		catch (WaitHandleCannotBeOpenedException)
		{
		}
		if (mutex == null)
		{
			mutex = new Mutex(initiallyOwned: true, "PCAVIATOR SphereIQ");
			RunApplication(text);
			GC.KeepAlive(mutex);
			mutex.ReleaseMutex();
		}
		else
		{
			mutex.Close();
			MessageBox.Show("Application is already running.", "SphereIQ not started!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Application.Exit();
		}
	}

	private static void RunApplication(string args)
	{
		AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: true);
		Splash.ShowSplash(500);
		Splash.LoadingComponents();
		Form_MainForm mainForm = ((args.Length > 0) ? new Form_MainForm(args) : new Form_MainForm());
		mainForm.Shown += MainApplicationForm_Shown;
		mainForm.FormClosed += MainApplicationForm_FormClosed;
		Splash.ApplicationMainForm = mainForm;
		Application.Run(mainForm);
	}

	private static void MainApplicationForm_Shown(object sender, EventArgs e)
	{
		if (Splash.Instance != null)
		{
			Splash.FinishProgressIndicator();
			Splash.FadedOut += Splash_FadedOut;
			Splash.Fadeout();
		}
	}

	private static void Splash_FadedOut(object sender, EventArgs e)
	{
		Splash.ApplicationMainForm.Enabled = true;
	}

	private static void MainApplicationForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Application.Exit();
	}

	private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
	{
		Splash.IncrementProgressIndicator();
	}
}
