using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace Dolinay;

internal class DriveDetector : IDisposable
{
	private class Native
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, uint Flags);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern uint UnregisterDeviceNotification(IntPtr hHandle);
	}

	public struct DEV_BROADCAST_HANDLE
	{
		public int dbch_size;

		public int dbch_devicetype;

		public int dbch_reserved;

		public IntPtr dbch_handle;

		public IntPtr dbch_hdevnotify;

		public Guid dbch_eventguid;

		public long dbch_nameoffset;

		public byte dbch_data;

		public byte dbch_data1;
	}

	public struct DEV_BROADCAST_VOLUME
	{
		public int dbcv_size;

		public int dbcv_devicetype;

		public int dbcv_reserved;

		public int dbcv_unitmask;
	}

	private const int DBT_DEVTYP_DEVICEINTERFACE = 5;

	private const int DBT_DEVTYP_HANDLE = 6;

	private const int BROADCAST_QUERY_DENY = 1112363332;

	private const int WM_DEVICECHANGE = 537;

	private const int DBT_DEVICEARRIVAL = 32768;

	private const int DBT_DEVICEQUERYREMOVE = 32769;

	private const int DBT_DEVICEREMOVECOMPLETE = 32772;

	private const int DBT_DEVTYP_VOLUME = 2;

	private FileStream mFileOnFlash;

	private string mFileToOpen;

	private IntPtr mDeviceNotifyHandle;

	private IntPtr mRecipientHandle;

	private string mCurrentDrive;

	public bool IsQueryHooked
	{
		get
		{
			if (mDeviceNotifyHandle == IntPtr.Zero)
			{
				return false;
			}
			return true;
		}
	}

	public string HookedDrive => mCurrentDrive;

	public FileStream OpenedFile => mFileOnFlash;

	public event DriveDetectorEventHandler DeviceArrived;

	public event DriveDetectorEventHandler DeviceRemoved;

	public event DriveDetectorEventHandler QueryRemove;

	public DriveDetector(Control control)
	{
		mFileToOpen = null;
		mDeviceNotifyHandle = IntPtr.Zero;
		mRecipientHandle = control.Handle;
		mCurrentDrive = "";
	}

	public DriveDetector(Control control, string FileToOpen)
	{
		mFileToOpen = FileToOpen;
		mDeviceNotifyHandle = IntPtr.Zero;
		mRecipientHandle = control.Handle;
		mCurrentDrive = "";
	}

	public bool EnableQueryRemove(string fileOnDrive)
	{
		if (fileOnDrive == null || fileOnDrive.Length == 0)
		{
			throw new ArgumentException("Drive path must be supplied to register for Query remove.");
		}
		if (fileOnDrive.Length == 2 && fileOnDrive[1] == ':')
		{
			fileOnDrive += '\\';
		}
		if (mDeviceNotifyHandle != IntPtr.Zero)
		{
			RegisterForDeviceChange(register: false, null);
		}
		if (!File.Exists(fileOnDrive))
		{
			mFileToOpen = null;
		}
		else
		{
			mFileToOpen = fileOnDrive;
		}
		RegisterQuery(Path.GetPathRoot(fileOnDrive));
		if (mDeviceNotifyHandle == IntPtr.Zero)
		{
			return false;
		}
		return true;
	}

	public void DisableQueryRemove()
	{
		if (mDeviceNotifyHandle != IntPtr.Zero)
		{
			RegisterForDeviceChange(register: false, null);
		}
	}

	public void Dispose()
	{
		RegisterForDeviceChange(register: false, null);
	}

	public void WndProc(ref Message m)
	{
		if (m.Msg != 537)
		{
			return;
		}
		switch (m.WParam.ToInt32())
		{
		case 32768:
		{
			int num = Marshal.ReadInt32(m.LParam, 4);
			if (num != 2)
			{
				break;
			}
			char c = DriveMaskToLetter(((DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME))).dbcv_unitmask);
			DriveDetectorEventHandler deviceArrived = DeviceArrived;
			if (deviceArrived == null)
			{
				break;
			}
			DriveDetectorEventArgs e2 = new DriveDetectorEventArgs();
			e2.Drive = c + ":\\";
			deviceArrived(this, e2);
			if (e2.HookQueryRemove)
			{
				if (mDeviceNotifyHandle != IntPtr.Zero)
				{
					RegisterForDeviceChange(register: false, null);
				}
				RegisterQuery(c + ":\\");
			}
			break;
		}
		case 32769:
		{
			int num = Marshal.ReadInt32(m.LParam, 4);
			if (num != 6)
			{
				break;
			}
			DriveDetectorEventHandler queryRemove = QueryRemove;
			if (queryRemove != null)
			{
				DriveDetectorEventArgs e3 = new DriveDetectorEventArgs();
				e3.Drive = mCurrentDrive;
				queryRemove(this, e3);
				if (e3.Cancel)
				{
					m.Result = (IntPtr)1112363332;
				}
				else if (mFileOnFlash != null)
				{
					mFileOnFlash.Close();
					mFileOnFlash = null;
				}
			}
			break;
		}
		case 32772:
		{
			int num = Marshal.ReadInt32(m.LParam, 4);
			if (num != 2)
			{
				break;
			}
			num = Marshal.ReadInt32(m.LParam, 4);
			if (num == 2)
			{
				char c = DriveMaskToLetter(((DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME))).dbcv_unitmask);
				DriveDetectorEventHandler deviceRemoved = DeviceRemoved;
				if (deviceRemoved != null)
				{
					DriveDetectorEventArgs e = new DriveDetectorEventArgs();
					e.Drive = c + ":\\";
					deviceRemoved(this, e);
				}
			}
			break;
		}
		case 32770:
		case 32771:
			break;
		}
	}

	private void RegisterQuery(string drive)
	{
		bool flag = true;
		if (mFileToOpen == null)
		{
			mFileToOpen = GetAnyFile(drive);
			if (mFileToOpen.Length == 0)
			{
				return;
			}
		}
		else if (mFileToOpen.Contains(":"))
		{
			string path = mFileToOpen.Substring(3);
			string pathRoot = Path.GetPathRoot(drive);
			mFileToOpen = Path.Combine(pathRoot, path);
		}
		else
		{
			mFileToOpen = Path.Combine(drive, mFileToOpen);
		}
		try
		{
			mFileOnFlash = new FileStream(mFileToOpen, FileMode.Open);
		}
		catch (Exception)
		{
			flag = false;
		}
		if (flag)
		{
			RegisterForDeviceChange(register: true, mFileOnFlash.SafeFileHandle);
			mCurrentDrive = drive;
		}
	}

	private void RegisterForDeviceChange(bool register, SafeFileHandle fileHandle)
	{
		if (register)
		{
			DEV_BROADCAST_HANDLE dEV_BROADCAST_HANDLE = new DEV_BROADCAST_HANDLE
			{
				dbch_devicetype = 6,
				dbch_reserved = 0,
				dbch_nameoffset = 0L,
				dbch_handle = fileHandle.DangerousGetHandle(),
				dbch_hdevnotify = (IntPtr)0
			};
			IntPtr intPtr = Marshal.AllocHGlobal(dEV_BROADCAST_HANDLE.dbch_size = Marshal.SizeOf(dEV_BROADCAST_HANDLE));
			Marshal.StructureToPtr(dEV_BROADCAST_HANDLE, intPtr, fDeleteOld: true);
			mDeviceNotifyHandle = Native.RegisterDeviceNotification(mRecipientHandle, intPtr, 0u);
		}
		else
		{
			if (mDeviceNotifyHandle != IntPtr.Zero)
			{
				Native.UnregisterDeviceNotification(mDeviceNotifyHandle);
			}
			mDeviceNotifyHandle = IntPtr.Zero;
			mCurrentDrive = "";
			if (mFileOnFlash != null)
			{
				mFileOnFlash.Close();
				mFileOnFlash = null;
			}
		}
	}

	private static char DriveMaskToLetter(int mask)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		int num = 0;
		int num2 = mask / 2;
		while (num2 != 0)
		{
			num2 /= 2;
			num++;
		}
		if (num < text.Length)
		{
			return text[num];
		}
		return '?';
	}

	private string GetAnyFile(string drive)
	{
		string result = "";
		string[] files = Directory.GetFiles(drive);
		if (files.Length == 0)
		{
			files = Directory.GetFiles(drive, "*.*", SearchOption.AllDirectories);
		}
		if (files.Length > 0)
		{
			result = files[0];
		}
		return result;
	}
}
