using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EzyPlan_Interface_V2;

internal class IconExtractor
{
	public struct SHFILEINFO
	{
		public IntPtr hIcon;

		public IntPtr iIcon;

		public uint dwAttributes;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szDisplayName;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string szTypeName;
	}

	private class Win32
	{
		public const uint SHGFI_ICON = 256u;

		public const uint SHGFI_LARGEICON = 0u;

		public const uint SHGFI_SMALLICON = 1u;

		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
	}

	public static Icon GetFileIcon(string fileName, IconSize _iconSize)
	{
		Icon icon = null;
		try
		{
			SHFILEINFO psfi = default;
			Win32.SHGetFileInfo(fileName, 0u, ref psfi, (uint)Marshal.SizeOf(psfi), 0x100u | ((_iconSize == IconSize.Small) ? 1u : 0u));
			return Icon.FromHandle(psfi.hIcon);
		}
		catch
		{
			return null;
		}
	}
}
