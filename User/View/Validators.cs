using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_Tim10_Putovanja.User.View
{
	class validators: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.WriteLine("heeej22");

            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid double value.");
            }
            catch(Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }
	public class EmailValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (regex.IsMatch(value.ToString())) return new ValidationResult(true, null);
            return new ValidationResult(false, "Email treba biti formata ime@domen");
        }
	}

	public class NotNullText : ValidationRule
    {
        public string FieldName
        {
            get;
            set;
        }


        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            Debug.WriteLine("heeej");
            if (value == null || value.ToString().Length <2)
            {
                return new ValidationResult(false, $"Polje za {FieldName} mora imati bar 2 karaktera");
                
            }
            return new ValidationResult(true, null);
        }
    }

    public class PriceRange : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || (double) value > 100000 || (double) value < 1000) 
            {
                return new ValidationResult(false, $"Cena se krece u opsegu od 1000 do 100 000");

            }
            return new ValidationResult(true, null);
        }
    }

    class DoubleVal : ValidationRule
    {
        public string FieldName
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, $"Polje {FieldName} se unosi u ciframa");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }

    class DateFmt : ValidationRule
    {
        public string FieldName
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            try
            {
                var s = value as string;
                DateTime r;
                if (DateTime.TryParseExact(s, "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, $"Polje {FieldName} je formata d/M/yyyy h:mm:ss tt");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }

}
