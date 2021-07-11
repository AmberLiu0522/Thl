using System;
using System.ComponentModel;

namespace Thl.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memberInfo = type.GetMember(enumeration.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return enumeration.ToString();
        }
    }
}