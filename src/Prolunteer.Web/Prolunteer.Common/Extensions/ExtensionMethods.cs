using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Common.Extensions
{
    public static class ExtensionMethods
    {
        public static string GetDisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            var result = enumType.GetField(name).GetCustomAttributes(false).OfType<DisplayNameAttribute>().SingleOrDefault()?.DisplayName;

            if (String.IsNullOrWhiteSpace(result))
            {
                return name;
            }

            return result;
        }
    }
}
