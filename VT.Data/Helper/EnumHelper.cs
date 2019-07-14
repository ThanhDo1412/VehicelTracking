using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace VT.Data.Helper
{
    public static class EnumHelper
    {
        public static DisplayAttribute GetDisplayAttribute(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
        }
    }
}
