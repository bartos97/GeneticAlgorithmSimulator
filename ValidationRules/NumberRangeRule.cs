using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GeneticAlgorithmSimulator.ValidationRules
{
    public class NumberRangeRule : ValidationRule
    {
        public int Min { get; set; } = Int32.MinValue;
        public int Max { get; set; } = Int32.MaxValue;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = 0;

            try
            {
                if (((string)value).Length > 0)
                    num = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((num < Min) || (num > Max))
            {
                return new ValidationResult(false,
                  $"Please enter an age in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
