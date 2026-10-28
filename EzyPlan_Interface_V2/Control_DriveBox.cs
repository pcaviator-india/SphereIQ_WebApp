using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

public class Control_DriveBox : UserControl
{
	private delegate void UpdateListDelegate(DriveListElement element);

	private delegate void ClearListInvoker();

	private IContainer components;

	private ListBox listBox1;

	private DriveListElement m_selectedDrive;

	private object m_thisLock = new object();

	public DriveListElement SelectedDrive => m_selectedDrive;

	public string SelectedDriveRoot => m_selectedDrive.Root;

	public event EventHandler SelectedDriveChanged;

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
		this.listBox1 = new System.Windows.Forms.ListBox();
		base.SuspendLayout();
		this.listBox1.BackColor = System.Drawing.Color.White;
		this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(0, 0);
		this.listBox1.Name = "listBox1";
		this.listBox1.ScrollAlwaysVisible = true;
		this.listBox1.Size = new System.Drawing.Size(223, 303);
		this.listBox1.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.listBox1);
		base.Name = "Control_DriveBox";
		base.Size = new System.Drawing.Size(223, 307);
		base.ResumeLayout(false);
	}

	public Control_DriveBox()
	{
		InitializeComponent();
		m_selectedDrive = null;
		listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
	}

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listBox1.SelectedItem != null)
		{
			m_selectedDrive = (DriveListElement)listBox1.SelectedItem;
			if (SelectedDriveChanged != null)
			{
				SelectedDriveChanged(this, null);
			}
		}
	}

	public void UpdateDrivesList()
	{
		MethodInvoker methodInvoker = GetDrives;
		methodInvoker.BeginInvoke(null, null);
	}

	private void ClearList()
	{
		if (listBox1.InvokeRequired)
		{
			ClearListInvoker method = ClearList;
			listBox1.Invoke(method);
		}
		else
		{
			listBox1.Items.Clear();
		}
	}

	public void DeSelectDrivesList()
	{
		listBox1.SelectedItem = null;
		m_selectedDrive = null;
	}

	private void GetDrives()
	{
		lock (m_thisLock)
		{
			ClearList();
			DriveInfo[] drives = DriveInfo.GetDrives();
			DriveInfo[] array = drives;
			foreach (DriveInfo driveInfo in array)
			{
				if (driveInfo.IsReady)
				{
					if (driveInfo.VolumeLabel != "")
					{
						DriveListElement element = new DriveListElement(driveInfo.VolumeLabel.ToString(), driveInfo.DriveType.ToString(), driveInfo.RootDirectory.ToString());
						AddDriveToList(element);
					}
					else
					{
						DriveListElement element2 = new DriveListElement(driveInfo.DriveType.ToString(), driveInfo.RootDirectory.ToString());
						AddDriveToList(element2);
					}
				}
				else
				{
					DriveListElement element3 = new DriveListElement(driveInfo.DriveType.ToString(), driveInfo.RootDirectory.ToString());
					AddDriveToList(element3);
				}
			}
		}
	}

	private void AddDriveToList(DriveListElement element)
	{
		if (listBox1.InvokeRequired)
		{
			UpdateListDelegate method = AddDriveToList;
			listBox1.Invoke(method, element);
		}
		else
		{
			listBox1.Items.Add(element);
		}
	}
}
