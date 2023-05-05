using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Perficient.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayText(this Enum enumValue)
        {
            return enumValue.GetDisplay()?.Description ?? enumValue.ToString();
        }
        public static DisplayAttribute GetDisplay(this Enum enumValue)
        {
            return enumValue.GetAttribute<DisplayAttribute>();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            if (enumValue == null)
            {
                return null;
            }

            var enumType = enumValue.GetType();
            var valName = Enum.GetName(enumType, enumValue);
            return enumType.GetField(valName).GetCustomAttributes(false).OfType<TAttribute>().FirstOrDefault();
        }
    }
}
