using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EzyPlan_Interface_V2;

[Description("Display a list of files that the user can select from")]
[DefaultProperty("DirectoryName")]
[DefaultEvent("FileSelected")]
public class FilesListBox : ListBox
{
	private string _selectedPath = "C:\\";

	private bool _showDirectories = true;

	private bool _showBackIcon = true;

	private IconSize _fileIconSize;

	[Category("Data")]
	[DefaultValue("C:\\")]
	[Description("Gets or sets the directory name that the files should relate to")]
	public string SelectedPath
	{
		get
		{
			return _selectedPath;
		}
		set
		{
			if (value != string.Empty)
			{
				_selectedPath = value;
				PopulatingItems();
			}
		}
	}

	[Category("Behavior")]
	[DefaultValue(true)]
	[Description("Gets or sets a value indicating wheater to show directories on the control")]
	public bool ShowDirectories
	{
		get
		{
			return _showDirectories;
		}
		set
		{
			_showDirectories = value;
			PopulatingItems();
		}
	}

	[Description("Gets or sets a value indicating wheater to show the back directory icon on the control (when the IsToShowDirectories property is true)")]
	[DefaultValue(true)]
	[Category("Behavior")]
	public bool ShowBackIcon
	{
		get
		{
			return _showBackIcon;
		}
		set
		{
			_showBackIcon = value;
			PopulatingItems();
		}
	}

	[Browsable(false)]
	public string[] SelectedFiles
	{
		get
		{
			ArrayList arrayList = new ArrayList();
			foreach (object selectedItem in SelectedItems)
			{
				string fullName = GetFullName(selectedItem.ToString());
				if (!fullName.EndsWith("..") && File.Exists(fullName))
				{
					arrayList.Add(fullName);
				}
			}
			return arrayList.ToArray(typeof(string)) as string[];
		}
	}

	[Browsable(false)]
	public string SelectedFile
	{
		get
		{
			string[] selectedFiles = SelectedFiles;
			if (selectedFiles.Length > 0)
			{
				return selectedFiles[0];
			}
			return null;
		}
	}

	[Category("Appearance")]
	[Description("Specifies the size of the icon of each file - small or large")]
	[DefaultValue(typeof(IconSize), "IconSize.Small")]
	public IconSize FileIconSize
	{
		get
		{
			return _fileIconSize;
		}
		set
		{
			_fileIconSize = value;
			switch (_fileIconSize)
			{
			case IconSize.Small:
				base.ItemHeight = 16;
				break;
			case IconSize.Large:
				base.ItemHeight = 32;
				break;
			}
			Invalidate();
		}
	}

	[Browsable(false)]
	public override int ItemHeight
	{
		get
		{
			return base.ItemHeight;
		}
		set
		{
			if (value == 32)
			{
				FileIconSize = IconSize.Large;
				base.ItemHeight = 32;
			}
			else
			{
				FileIconSize = IconSize.Small;
				base.ItemHeight = 16;
			}
		}
	}

	[Category("Action")]
	[Description("Occures whenever a file is selected by a double click on the control")]
	public event FileSelectedEventHandler FileSelected;

	public FilesListBox()
	{
		SelectionMode = SelectionMode.MultiExtended;
		DrawMode = DrawMode.OwnerDrawFixed;
	}

	private void watcher_Changed(object sender, FileSystemEventArgs e)
	{
		MessageBox.Show("Changed");
	}

	public FilesListBox(string directoryName)
		: this()
	{
		_selectedPath = SelectedPath;
		PopulatingItems();
	}

	private void AddDirectory(string directoryName)
	{
		Items.Add(directoryName);
	}

	private void AddFile(string fileName)
	{
		Items.Add(fileName);
	}

	private string GetFullName(string fileNameOnly)
	{
		return Path.Combine(_selectedPath, fileNameOnly);
	}

	private void PopulatingItems()
	{
		if (DesignMode)
		{
			return;
		}
		Items.Clear();
		if (_showBackIcon && _selectedPath.Length > 3)
		{
			Items.Add("..");
		}
		try
		{
			if (_showDirectories)
			{
				string[] directories = Directory.GetDirectories(_selectedPath);
				string[] array = directories;
				foreach (string path in array)
				{
					string fileName = Path.GetFileName(path);
					Items.Add(fileName);
				}
			}
			string[] files = Directory.GetFiles(_selectedPath);
			string[] array2 = files;
			foreach (string path2 in array2)
			{
				string fileName2 = Path.GetFileName(path2);
				Items.Add(fileName2);
			}
		}
		catch
		{
		}
		Invalidate();
	}

	protected override void OnMouseDoubleClick(MouseEventArgs e)
	{
		if (SelectedItem == null)
		{
			return;
		}
		string fullName = GetFullName(SelectedItem.ToString());
		if (fullName.EndsWith(".."))
		{
			if (_selectedPath.EndsWith("\\"))
			{
				_selectedPath = _selectedPath.Remove(_selectedPath.Length - 1, 1);
			}
			_selectedPath = Directory.GetParent(_selectedPath).FullName;
			PopulatingItems();
		}
		else if (Directory.Exists(fullName))
		{
			_selectedPath = fullName + "\\";
			PopulatingItems();
		}
		else
		{
			OnFileSelected(new FileSelectEventArgs(fullName));
		}
		base.OnMouseDoubleClick(e);
	}

	protected void OnFileSelected(FileSelectEventArgs fse)
	{
		if (FileSelected != null)
		{
			FileSelected(this, fse);
		}
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (SelectedItem == null)
			{
				return;
			}
			string fullName = GetFullName(SelectedItem.ToString());
			if (fullName.EndsWith(".."))
			{
				if (_selectedPath.EndsWith("\\"))
				{
					_selectedPath = _selectedPath.Remove(_selectedPath.Length - 1, 1);
				}
				_selectedPath = Directory.GetParent(_selectedPath).FullName;
				PopulatingItems();
			}
			else if (Directory.Exists(fullName))
			{
				_selectedPath = fullName + "\\";
				PopulatingItems();
			}
			else
			{
				OnFileSelected(new FileSelectEventArgs(fullName));
			}
		}
		base.OnKeyPress(e);
	}

	protected override void OnDrawItem(DrawItemEventArgs e)
	{
		e.DrawBackground();
		e.DrawFocusRectangle();
		Rectangle bounds = e.Bounds;
		if (e.Index > -1 && e.Index < Items.Count)
		{
			string text = Items[e.Index].ToString();
			string fullName = GetFullName(text);
			Icon icon = null;
			Rectangle targetRect = new Rectangle(bounds.Left + 1, bounds.Top + 1, ItemHeight - 2, ItemHeight - 2);
			if (text.Equals(".."))
			{
				icon = IconExtractor.GetFileIcon(Application.StartupPath, _fileIconSize);
				e.Graphics.DrawIcon(icon, targetRect);
			}
			else
			{
				icon = IconExtractor.GetFileIcon(fullName, _fileIconSize);
				if (icon != null)
				{
					e.Graphics.DrawIcon(icon, targetRect);
				}
				else
				{
					PopulatingItems();
				}
			}
			Size size = targetRect.Size;
			icon?.Dispose();
			Rectangle rectangle = new Rectangle(bounds.Left + size.Width + 3, bounds.Top, bounds.Width - size.Width - 3, bounds.Height);
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = StringAlignment.Center;
			e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), rectangle, stringFormat);
		}
		base.OnDrawItem(e);
	}
}
