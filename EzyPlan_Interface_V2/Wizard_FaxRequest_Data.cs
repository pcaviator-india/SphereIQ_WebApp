namespace EzyPlan_Interface_V2;

internal class Wizard_FaxRequest_Data
{
	private string m_from;

	private string m_faxNumber;

	private string m_companyName;

	private string m_SerialNumber;

	public string From
	{
		get
		{
			return m_from;
		}
		set
		{
			m_from = value;
		}
	}

	public string FaxNumber
	{
		get
		{
			return m_faxNumber;
		}
		set
		{
			m_faxNumber = value;
		}
	}

	public string CompanyName
	{
		get
		{
			return m_companyName;
		}
		set
		{
			m_companyName = value;
		}
	}

	public string SerialNumber
	{
		get
		{
			return m_SerialNumber;
		}
		set
		{
			m_SerialNumber = value;
		}
	}
}
