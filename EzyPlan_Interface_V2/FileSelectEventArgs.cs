using System;

namespace EzyPlan_Interface_V2;

public class FileSelectEventArgs : EventArgs
{
	private string fileName;

	public string FileName
	{
		get
		{
			return fileName;
		}
		set
		{
			fileName = FileName;
		}
	}

	public FileSelectEventArgs(string fileName)
	{
		this.fileName = fileName;
	}
}
