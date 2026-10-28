namespace EzyPlan_Interface_V2;

public class DriveListElement
{
	private string m_root;

	private string m_name;

	private string m_type;

	public string Root
	{
		get
		{
			return m_root;
		}
		set
		{
			m_root = value;
		}
	}

	public string Name
	{
		get
		{
			return m_name;
		}
		set
		{
			m_name = value;
		}
	}

	public string Type
	{
		get
		{
			return m_type;
		}
		set
		{
			if (value == "Fixed")
			{
				m_type = "HardDisk";
			}
			else
			{
				m_type = value;
			}
		}
	}

	public DriveListElement(string name, string type, string root)
	{
		Name = name;
		Root = root;
		Type = type;
	}

	public DriveListElement(string type, string root)
	{
		Type = type;
		Root = root;
	}

	public override string ToString()
	{
		if (Name != null)
		{
			return Name + " (" + Root + ") - " + Type;
		}
		return "(" + Root + ") - " + Type;
	}
}
