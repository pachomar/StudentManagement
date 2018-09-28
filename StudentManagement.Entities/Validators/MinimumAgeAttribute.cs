using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StudentManagement.Entities.Validators
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }

            return false;
        }
    }
}
