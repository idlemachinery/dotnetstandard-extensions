namespace System
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Determines if the Type is numeric.
        /// </summary>
        /// <param name="type">typeof</param>
        /// <returns>A boolean indicating whether or not the type is numeric</returns>
        public static bool IsNumericDatatype(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines if the object is of numeric type.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>A boolean indicating whether or not the object type is numeric</returns>
        public static bool IsNumericDatatype(this object obj)
        {
            return obj.GetType().IsNumericDatatype();
        }        
    }
}
