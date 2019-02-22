using System.Globalization;

namespace System
{
    public static class Number
    {
        public static Number<T> CreateNew<T>(T val) where T : struct, IConvertible
        {
            return new Number<T>(val);
        }
    }

    public class Number<TNum> : IConvertible where TNum : struct, IConvertible
    {        
        public TNum? Value { get; set; }

        public CultureInfo Culture { get; set; } = new CultureInfo("en-US", true);

        //https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
        public string FormatString { get; set; } = "N";

        public bool UseZeroForNullValue { get; set; } = true;

        public Number(TNum? value)
        {
            if (!ValueType.IsNumericDatatype())
                throw new ArgumentException("Generic TNum is not a numeric type.", nameof(value));
            Value = value;
        }

        public bool HasValue { get { return Value.HasValue; } }
        public Type ValueType { get { return typeof(TNum); } }       

        double GetDoubleValue()
        {
            return HasValue ? Convert.ToDouble(Value) : default(float);
        }

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return Type.GetTypeCode(ValueType);
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return HasValue && GetDoubleValue() != 0.0;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(GetDoubleValue());
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(GetDoubleValue());
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(GetDoubleValue());
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(GetDoubleValue());
        }

        public double ToDouble(IFormatProvider provider)
        {
            return GetDoubleValue();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(GetDoubleValue());
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(GetDoubleValue());
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(GetDoubleValue());
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(GetDoubleValue());
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(GetDoubleValue());
        }

        public string ToString(IFormatProvider provider)
        {
            return HasValue ? Convert.ToString(GetDoubleValue()) : string.Empty;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(GetDoubleValue(), conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(GetDoubleValue());
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(GetDoubleValue());
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(GetDoubleValue());
        }

        #endregion
    }
}
