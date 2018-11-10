using System.Collections.Generic;
using System.Text.RegularExpressions;

// Business ID value specification:
// https://www.vero.fi/contentassets/38c6e70f10b342cdba4716e904c3edbf/tarkistusmerkin-laskenta.pdf

namespace BusinessId.Specification
{
    public class BusinessId
    {
        private const int ValueLength = 9;
        private const string ContentPattern = @"^[0-9-]+$";
        private const string FormatPattern = @"^.{7}-.{1}$";

        private string _value;
        private List<string> _errors;

        public BusinessId()
        {
            _value = string.Empty;
            _errors = new List<string>();
        }

        public string Value
        {
            set
            {
                _value = value;
            }
        }

        public IEnumerable<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        public bool VerifyValue()
        {
            _errors.Clear();

            if(!_value.Length.Equals(ValueLength))
            {
                _errors.Add("Length is invalid");
            }
            if(!Regex.IsMatch(_value, ContentPattern))
            {
                _errors.Add("Content is invalid");
            }
            if(!Regex.IsMatch(_value, FormatPattern))
            {
                _errors.Add("Format is invalid");
            }

            if (_errors.Count != 0)
            {
                return false;
            }

            char[] values = _value.ToCharArray();

            int sum =
                (values[0] & 0xf) * 7 +
                (values[1] & 0xf) * 9 +
                (values[2] & 0xf) * 10 +
                (values[3] & 0xf) * 5 +
                (values[4] & 0xf) * 8 +
                (values[5] & 0xf) * 4 +
                (values[6] & 0xf) * 2 ;
            int verificator = values[8] & 0xf;

            int mod = sum % 11;
            if(mod != 1)
            {
                if (mod > 1 && verificator != 11 - mod || mod == 0 && verificator != 0)
                {
                    _errors.Add("Wrong verification number");
                    return false;
                }
            }
            else
            {
                _errors.Add("Value is not in use");
                return false;
            }

            return true;
        } // VerifyValue
    }
}
