using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EzyPlan_Interface_V2.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
[CompilerGenerated]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("support@2icaustralia.com")]
	public string supportAddress => (string)this["supportAddress"];

	[DefaultSettingValue("+61 8 9456 4177")]
	[DebuggerNonUserCode]
	[ApplicationScopedSetting]
	public string supportPhone => (string)this["supportPhone"];

	[DebuggerNonUserCode]
	[DefaultSettingValue("3")]
	[ApplicationScopedSetting]
	public decimal severeDL => (decimal)this["severeDL"];

	[DefaultSettingValue("7")]
	[DebuggerNonUserCode]
	[ApplicationScopedSetting]
	public decimal extremeDL => (decimal)this["extremeDL"];

	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string PreviouseFile
	{
		get
		{
			return (string)this["PreviouseFile"];
		}
		set
		{
			this["PreviouseFile"] = value;
		}
	}

	[DefaultSettingValue("1.02")]
	[DebuggerNonUserCode]
	[ApplicationScopedSetting]
	public string ProductVersion => (string)this["ProductVersion"];
}
