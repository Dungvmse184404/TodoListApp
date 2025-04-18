using Models.Message;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Message;

namespace BLL.Utilities.Validators
{
    public static class ValidateDataType
    {
        public static string ValidateStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return null;

            status = status.Trim();

            if (Enum.TryParse<LabelStatus>(status, ignoreCase: true, out var result))
            {
                return result.ToString();
            }

            return null;
        }


        public static T ValidateInput<T>(T input, string errorMessage)
        {
            if (input is string str)
            {
                input = (T)(object)str.Trim();
            }
            if (EqualityComparer<T>.Default.Equals(input, default))
            {
                throw new ArgumentException(errorMessage);
            }

            return input;
        }
    }
}
