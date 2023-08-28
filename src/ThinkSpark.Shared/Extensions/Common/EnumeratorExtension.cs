using System.ComponentModel;
using System.Reflection;

namespace ThinkSpark.Shared.Extensions.Common
{
    public static class EnumeratorExtension
    {
        public static int ToNumber<TEnum>(this TEnum soure) where TEnum : IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum deve ser um tipo de enumerador");

            var result = (int)(IConvertible)soure;
            return result;
        }

        public static string ToDescribe<T>(this T soure) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return string.Empty;

            string description = soure.ToString();
            var fieldInfo = soure.GetType().GetField(soure.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attrs != null && attrs.Length > 0)
                    description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }

        public static string ToDescribe(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
