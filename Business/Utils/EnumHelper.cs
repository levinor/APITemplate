using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Levinor.Business.Utils
{
    public class EnumHelper
    {
        public static bool IsEnumValid(Type _acceptedValues, object value)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(value as string))
            {
                var valueWithoutSpaces = value.ToString().Replace(" ", string.Empty).ToLower();
                result = Enum.GetNames(_acceptedValues).Any(x => x.ToLower() == valueWithoutSpaces);
            }
            return result;
        }
    }
}
