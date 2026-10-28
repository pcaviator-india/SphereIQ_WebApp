using System;

namespace Dolinay;

public class DriveDetectorEventArgs : EventArgs
{
	public bool Cancel;

	public string Drive;

	public bool HookQueryRemove;

	public DriveDetectorEventArgs()
	{
		Cancel = false;
		Drive = "";
		HookQueryRemove = false;
	}
}
