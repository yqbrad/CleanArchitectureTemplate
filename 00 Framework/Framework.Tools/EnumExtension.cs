using System;
using System.ComponentModel;

namespace Framework.Tools
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum @enum)
        {
            var genericEnumType = @enum.GetType();
            var memberInfo = genericEnumType.GetMember(@enum.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            return @enum.ToString();
        }
    }
}