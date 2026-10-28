using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2.VBusers.Windows.Forms.Utils;

[DebuggerStepThrough]
public class AutoWaitCursor
{
	private class ApplicationStateMonitor : IDisposable
	{
		private const int SMTO_NORMAL = 0;

		private const int SMTO_BLOCK = 1;

		private const int SMTO_NOTIMEOUTIFNOTHUNG = 8;

		private DateTime _inactiveStart = DateTime.Now;

		private bool _isStarted;

		private TimeSpan _delay;

		private IntPtr _mainWindowHandle = IntPtr.Zero;

		private Thread _callbackThread;

		private bool _isDisposed;

		private bool _enabled = true;

		private uint _mainThreadId;

		private uint _callbackThreadId;

		private Cursor _oldCursor;

		private Cursor _waitCursor;

		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
			}
		}

		public TimeSpan Delay
		{
			get
			{
				return _delay;
			}
			set
			{
				_delay = value;
			}
		}

		public bool IsStarted => _isStarted;

		public IntPtr MainWindowHandle
		{
			get
			{
				return _mainWindowHandle;
			}
			set
			{
				_mainWindowHandle = value;
			}
		}

		public Cursor Cursor
		{
			get
			{
				return _waitCursor;
			}
			set
			{
				_waitCursor = value;
			}
		}

		public TimeSpan ApplicationIdleTime => DateTime.Now.Subtract(_inactiveStart);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, string lParam, int fuFlags, int uTimeout, out int lpdwResult);

		[DllImport("USER32.DLL")]
		private static extern uint AttachThreadInput(uint attachTo, uint attachFrom, bool attach);

		[DllImport("KERNEL32.DLL")]
		private static extern uint GetCurrentThreadId();

		public ApplicationStateMonitor(Cursor waitCursor, TimeSpan delay)
		{
			_mainThreadId = GetCurrentThreadId();
			_delay = delay;
			_waitCursor = waitCursor;
			Application.ThreadExit += _OnApplicationThreadExit;
		}

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
			}
		}

		public void Start()
		{
			if (!_isStarted)
			{
				_isStarted = true;
				_CreateMonitorThread();
			}
		}

		public void Stop()
		{
			if (_isStarted)
			{
				_isStarted = false;
			}
		}

		public void SetWaitCursor()
		{
			_callbackThreadId = GetCurrentThreadId();
			AttachThreadInput(_callbackThreadId, _mainThreadId, attach: true);
			_oldCursor = Cursor.Current;
			Cursor.Current = _waitCursor;
		}

		public void RestoreCursor()
		{
			Cursor.Current = _oldCursor;
			AttachThreadInput(_callbackThreadId, _mainThreadId, attach: false);
		}

		private void _CreateMonitorThread()
		{
			_callbackThread = new Thread(_ThreadCallbackLoop);
			_callbackThread.Name = "AutoWaitCursorCallback";
			_callbackThread.IsBackground = true;
			_callbackThread.Start();
		}

		private void _ThreadCallbackLoop()
		{
			try
			{
				do
				{
					if (!_enabled || _mainWindowHandle == IntPtr.Zero)
					{
						Thread.Sleep(_delay);
					}
					else if (_IsApplicationBusy(_delay, _mainWindowHandle))
					{
						try
						{
							SetWaitCursor();
							_WaitForIdle();
						}
						finally
						{
							RestoreCursor();
							_inactiveStart = DateTime.Now;
						}
					}
					else
					{
						Thread.Sleep(25);
					}
				}
				while (!_isDisposed && _isStarted);
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}

		private bool _IsApplicationBusy(TimeSpan delay, IntPtr windowHandle)
		{
			int lpdwResult = 0;
			if (delay == TimeSpan.MaxValue)
			{
				SendMessageTimeout(windowHandle, 0, 0, null, 1, int.MaxValue, out lpdwResult);
			}
			else
			{
				SendMessageTimeout(windowHandle, 0, 0, null, 1, Convert.ToInt32(delay.TotalMilliseconds), out lpdwResult);
			}
			if (lpdwResult != 0)
			{
				return true;
			}
			return false;
		}

		private void _WaitForIdle()
		{
			_IsApplicationBusy(TimeSpan.MaxValue, _mainWindowHandle);
		}

		private void _OnApplicationThreadExit(object sender, EventArgs e)
		{
			Dispose();
		}
	}

	private static readonly TimeSpan DEFAULT_DELAY = new TimeSpan(0, 0, 0, 0, 25);

	private static ApplicationStateMonitor _appStateMonitor = new ApplicationStateMonitor(Cursors.WaitCursor, DEFAULT_DELAY);

	public TimeSpan ApplicationIdleTime => _appStateMonitor.ApplicationIdleTime;

	public static bool IsStarted => _appStateMonitor.IsStarted;

	public static Cursor Cursor
	{
		get
		{
			return _appStateMonitor.Cursor;
		}
		set
		{
			_appStateMonitor.Cursor = value;
		}
	}

	public static bool Enabled
	{
		get
		{
			return _appStateMonitor.Enabled;
		}
		set
		{
			_appStateMonitor.Enabled = value;
		}
	}

	public static TimeSpan Delay
	{
		get
		{
			return _appStateMonitor.Delay;
		}
		set
		{
			_appStateMonitor.Delay = value;
		}
	}

	public static IntPtr MainWindowHandle
	{
		get
		{
			return _appStateMonitor.MainWindowHandle;
		}
		set
		{
			_appStateMonitor.MainWindowHandle = value;
		}
	}

	private AutoWaitCursor()
	{
	}

	public static void Start()
	{
		_appStateMonitor.Start();
	}

	public static void Stop()
	{
		_appStateMonitor.Stop();
	}
}
