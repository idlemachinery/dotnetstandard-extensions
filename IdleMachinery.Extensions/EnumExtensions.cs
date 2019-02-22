using System.ComponentModel;
using System.Linq;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the name of the constant in the specified enumeration value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant in terms of it's underlying type.</param>
        /// <returns>A string containing the name of the enumerated constant in enumType whose value
        //     is value; or null if no such constant is found.</returns>
        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// Gets the value stored in a DescriptionAttribute on the specified enumeration value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant in terms of it's underlying type.</param>
        /// <returns>A string containing the description stored in an attribute; or the name of the
        ///     enumerated constant if no such attribute is found.</returns>
        public static string GetDescription(this Enum value)
        {
            var name = value.GetName();
            var fieldInfo = value.GetType().GetField(name);
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute == null ? name : descriptionAttribute.Description;
        }        

        /// <summary>
        /// Retrieves the integer value of the constant in the specified enumeration value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant in terms of it's underlying type.</param>
        /// <returns>An integer of the enumerated constant in enumType whose value is value.</returns>
        public static int ToInt(this Enum value)
        {
            return (int)value.GetType().GetField(value.ToString()).GetValue(value);
        }

        /// <summary>
        /// Retrieves the integer value as a string of the constant in the specified enumeration value.
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant in terms of it's underlying type.</param>
        /// <returns>A string containing an integer of the enumerated constant in enumType whose value is value</returns>
        public static string ToIntString(this Enum value)
        {
            return value.ToInt().ToString();
        }

        // TODO - document
        public static T? ToEnum<T>(this string str, T? defaultValue = null) 
            where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("T must be an Enumeration type.");
            }
            var didParse = Enum.TryParse(str, true, out T val);
            return didParse && Enum.IsDefined(enumType, val) ? val : defaultValue;
            //return Enum.TryParse(str, true, out T val) ? val : default(T);
        }

        // TODO - document
        public static T? ToEnum<T>(this int intValue, T? defaultValue = null) 
            where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("T must be an Enumeration type.");
            }
            var val = Enum.ToObject(enumType, intValue);
            return Enum.IsDefined(enumType, val) ? (T)val : defaultValue;
            //return (T)Enum.ToObject(enumType, intValue);
        }
    }
}
