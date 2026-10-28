using System;
using System.Drawing;
using System.Windows.Forms;
using EzyPlanData;
using RibbonStyle;

namespace EzyPlan_Interface_V2;

public static class Utility
{
	public enum StringToDecimalResultEnum
	{
		NoError,
		ValueRequired,
		NumberOutOfDecimalRange,
		StringIsNull,
		IncorectFormat
	}

	public enum StringToIntegerEnum
	{
		NoError,
		NumberOutOfIntegerRange,
		StringIsNull,
		IncorectFormat
	}

	public static Color m_ExtremeColor = Color.Red;

	public static Color m_SevereColor = Color.Orange;

	public static StringToDecimalResultEnum StringToDecimal(string s, out decimal number)
	{
		if (s != "")
		{
			try
			{
				number = decimal.Parse(s);
				return StringToDecimalResultEnum.NoError;
			}
			catch (OverflowException)
			{
				number = 0m;
				return StringToDecimalResultEnum.NumberOutOfDecimalRange;
			}
			catch (ArgumentNullException)
			{
				number = 0m;
				return StringToDecimalResultEnum.StringIsNull;
			}
			catch (FormatException)
			{
				number = 0m;
				return StringToDecimalResultEnum.IncorectFormat;
			}
		}
		number = 0m;
		return StringToDecimalResultEnum.ValueRequired;
	}

	public static string ToHumanReadable(StringToDecimalResultEnum enumValue)
	{
		return enumValue switch
		{
			StringToDecimalResultEnum.NoError => "No error occured", 
			StringToDecimalResultEnum.ValueRequired => "A value is required", 
			StringToDecimalResultEnum.NumberOutOfDecimalRange => "The value entered is outside the minimum or maximum value posible for a decimal", 
			StringToDecimalResultEnum.StringIsNull => "A value must be entered", 
			StringToDecimalResultEnum.IncorectFormat => "The value entered is incorectly formated", 
			_ => "Unknown Error", 
		};
	}

	public static StringToIntegerEnum StringToInteger(string s, out int number)
	{
		try
		{
			number = int.Parse(s);
			return StringToIntegerEnum.NoError;
		}
		catch (OverflowException)
		{
			number = 0;
			return StringToIntegerEnum.NumberOutOfIntegerRange;
		}
		catch (ArgumentNullException)
		{
			number = 0;
			return StringToIntegerEnum.StringIsNull;
		}
		catch (FormatException)
		{
			number = 0;
			return StringToIntegerEnum.IncorectFormat;
		}
	}

	public static string ToHumanReadable(StringToIntegerEnum value)
	{
		return value switch
		{
			StringToIntegerEnum.NoError => "No error occured", 
			StringToIntegerEnum.NumberOutOfIntegerRange => "The value entered is outside the minimum or maximum value allowed", 
			StringToIntegerEnum.StringIsNull => "A value must be entered", 
			StringToIntegerEnum.IncorectFormat => "The value entered is incorectly formated", 
			_ => null, 
		};
	}

	public static bool ConvertUserEnteredValue(string stringValue, out decimal value, string fieldName)
	{
		StringToDecimalResultEnum stringToDecimalResultEnum = StringToDecimal(stringValue, out value);
		if (stringToDecimalResultEnum != StringToDecimalResultEnum.NoError)
		{
			DisplayConversionErrorPopup(fieldName, ToHumanReadable(stringToDecimalResultEnum));
			return false;
		}
		return true;
	}

	private static void DisplayConversionErrorPopup(string fieldName, string errorText)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(fieldName + " Error!", fieldName + ": " + errorText + ".");
		popup_MessageBox_OK.ShowDialog();
	}

	public static void DisplayErrorPopup(string title, string errorText)
	{
		Popup_MessageBox_OK popup_MessageBox_OK = new Popup_MessageBox_OK(title, errorText);
		popup_MessageBox_OK.ShowDialog();
	}

	public static void ButtonGotFocus(ref RibbonMenuButton button)
	{
		button.ForeColor = Color.DimGray;
	}

	public static void ButtonLostFocus(ref RibbonMenuButton button)
	{
		button.ForeColor = Color.Black;
	}

	public static void MenuButtonGotFocus(ref RibbonMenuButton button)
	{
		button.ForeColor = Color.LightGray;
	}

	public static void MenuButtonLostFocus(ref RibbonMenuButton button)
	{
		button.ForeColor = Color.White;
	}

	public static void TextBoxGotFocus(ref TextBox textBox)
	{
		textBox.BackColor = Color.LightYellow;
		textBox.SelectAll();
	}

	public static void TextBoxLostFocus(ref TextBox textBox)
	{
		textBox.BackColor = Color.White;
	}

	public static void TextBoxGotFocus(ref MaskedTextBox textBox)
	{
		textBox.BackColor = Color.LightYellow;
		textBox.SelectAll();
	}

	public static void TextBoxLostFocus(ref MaskedTextBox textBox)
	{
		textBox.BackColor = Color.White;
	}

	public static void ComboBoxGotFocus(ref ComboBox combo)
	{
		combo.BackColor = Color.LightYellow;
		combo.SelectAll();
	}

	public static void ComboBoxLostFocus(ref ComboBox combo)
	{
		combo.BackColor = Color.White;
	}

	public static void TextBoxDoubleClick(TextBox textBox)
	{
		textBox.BackColor = Color.LightYellow;
		textBox.SelectAll();
	}

	public static void TextBoxDoubleClick(MaskedTextBox textBox)
	{
		textBox.BackColor = Color.LightYellow;
		textBox.SelectAll();
	}

	public static void ClearTextBoxes(Control control)
	{
		foreach (Control control2 in control.Controls)
		{
			try
			{
				((TextBox)control2).Clear();
			}
			catch (InvalidCastException)
			{
				ClearTextBoxes(control2);
			}
		}
	}

	public static void RelativePositionDescription(RelativePosition position, out string northing, out string easting, out string vertical, out string horizontal)
	{
		northing = position.NorthingDistance.ToString();
		switch (position.NorthingDirection)
		{
		case NorthingDirectionEnum.North:
			northing += " Meters North";
			break;
		case NorthingDirectionEnum.Null:
			northing += " Meters";
			break;
		case NorthingDirectionEnum.South:
			northing += " Meters South";
			break;
		}
		easting = position.EastingDistance.ToString();
		switch (position.EastingDirection)
		{
		case EastingDirectionEnum.East:
			easting += " Meters East";
			break;
		case EastingDirectionEnum.Null:
			easting += " Meters";
			break;
		case EastingDirectionEnum.West:
			easting += " Meters West";
			break;
		}
		vertical = position.VerticalDistance.ToString();
		switch (position.VerticalDirection)
		{
		case VerticalDirectionEnum.Above:
			vertical += " Meters Above";
			break;
		case VerticalDirectionEnum.Below:
			vertical += " Meters Below";
			break;
		case VerticalDirectionEnum.Null:
			vertical += " Meters";
			break;
		}
		horizontal = position.HorizontalDeparture + " Meters HD";
	}

	public static void EnterKeyEmulateTab(KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
			SendKeys.SendWait("{TAB}");
		}
	}
}
