
namespace System
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Return 'Yes' or 'No' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A string of 'Yes' for true or 'No' for false.</returns>
        public static string ToYesNo(this bool val)
        {
            return val ? "Yes" : "No";
        }

        /// <summary>
        /// Return 'Yes' or 'No' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A string of 'Yes' for true or 'No' for false; An empty string is returned for null.</returns>
        public static string ToYesNo(this bool? val)
        {
            return val.HasValue ? val.Value.ToYesNo() : string.Empty;
        }

        /// <summary>
        /// Return 'Y' or 'N' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A string of 'Y' for true or 'N' for false.</returns>
        public static string ToYN(this bool val)
        {
            return val ? "Y" : "N";
        }

        /// <summary>
        /// Return 'Y' or 'N' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A string of 'Y' for true or 'N' for false; An empty string is returned for null.</returns>
        public static string ToYN(this bool? val)
        {
            return val.HasValue ? val.Value.ToYN() : string.Empty;
        }

        /// <summary>
        /// Return 'True' or 'False' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A string of 'True' for true or 'False' for false.</returns>
        public static string ToTrueFalse(this bool val)
        {
            return val ? "True" : "False";
        }

        // <summary>
        /// Return 'True' or 'False' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A string of 'True' for true or 'False' for false; An empty string is returned for null.</returns>
        public static string ToTrueFalse(this bool? val)
        {
            return val.HasValue ? val.Value.ToTrueFalse() : string.Empty;
        }

        /// <summary>
        /// Return 'T' or 'F' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A string of 'T' for true or 'F' for false.</returns>
        public static string ToTF(this bool val)
        {
            return val ? "T" : "F";
        }

        // <summary>
        /// Return 'T' or 'F' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A string of 'T' for true or 'F' for false; An empty string is returned for null.</returns>
        public static string ToTF(this bool? val)
        {
            return val.HasValue ? val.Value.ToTF() : string.Empty;
        }

        /// <summary>
        /// Return '1' or '0' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A string of '1' for true or '0' for false.</returns>
        public static string To1or0(this bool val)
        {
            return val ? "1" : "0";
        }

        // <summary>
        /// Return '1' or '0' depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A string of '1' for true or '0' for false; An empty string is returned for null.</returns>
        public static string To1or0(this bool? val)
        {
            return val.HasValue ? val.Value.To1or0() : string.Empty;
        }

        /// <summary>
        /// Return a 1 or 0 depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>A byte of 1 for true or 0 for false.</returns>
        public static byte ToByte(this bool val)
        {
            return Convert.ToByte(val);
        }

        // <summary>
        /// Return 1 or 0 depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>A byte of 1 for true or 0 for false; A null byte is returned for null.</returns>
        public static byte? ToByte(this bool? val)
        {
            return val.HasValue ? val.Value.ToByte() : (byte?)null;
        }

        /// <summary>
        /// Return a 1 or 0 depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The boolean to evaluate.</param>
        /// <returns>An int of 1 for true or 0 for false.</returns>
        public static int ToInt(this bool val)
        {
            return Convert.ToInt16(val);
        }

        // <summary>
        /// Return 1 or 0 depending on the passed boolean value.
        /// </summary>
        /// <param name="val">The nullable boolean to evaluate.</param>
        /// <returns>An int of 1 for true or 0 for false; A null int is returned for null.</returns>
        public static int? ToInt(this bool? val)
        {
            return val.HasValue ? val.Value.ToByte() : (int?)null;
        }

        /// <summary>
        /// Determines whether or not the specified string is a boolean representation and returns true/false.
        /// Valid strings are: 'Y', 'N', 'Yes', 'No', 'T', 'F', 'True', 'False', '1', '0'
        /// The process is case-insensitive.
        /// </summary>
        /// <param name="str">The string to be evaluated.</param>
        /// <param name="defaultVal">What to return if the string does not represent a boolean. Default is null.</param>
        /// <returns>A nullable boolean representation.</returns>
        public static bool? ToBoolean(this string str, bool? defaultVal = null)
        {
            switch (str.ToLower())
            {
                case "y":
                case "yes":
                case "t":
                case "true":
                case "1":
                    return true;
                case "n":
                case "no":
                case "f":
                case "false":
                case "0":
                    return false;
                default:
                    return defaultVal;
            }
        }
    }
}
