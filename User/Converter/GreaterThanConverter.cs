using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace HCI_Tim10_Putovanja.User.Converter
{
	public class GreaterThanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.TryParse(value.ToString(), out double doubleValue) &&
				double.TryParse(parameter.ToString(), out double doubleParametar) &&
				doubleValue > doubleParametar;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.TryParse(value.ToString(), out double doubleValue) &&
				double.TryParse(parameter.ToString(), out double doubleParametar) &&
				doubleValue > doubleParametar;
		}
	}
}
