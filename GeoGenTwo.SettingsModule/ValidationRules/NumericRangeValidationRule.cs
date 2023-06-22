using System.Globalization;
using System.Windows.Controls;

namespace GeoGenTwo.SettingsModule.ValidationRules
{
    public class NumericRangeValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string strValue)
            {
                if (int.TryParse(strValue, out int numericValue))
                {
                    if (numericValue >= Min && numericValue <= Max)
                        return ValidationResult.ValidResult;
                }
            }

            return new ValidationResult(false, $"Please enter a value between {Min} and {Max}.");
        }
    }
}
