using System;
using CliConsole.Interfaces.Convertors;

namespace CliConsole.Convertors
{
    public class ValueConvertor : IValueConvertor
    {
        public bool TryConvert(Type type, string value, out object convertedValue)
        {
            if (type == typeof(string))
                return TryParseString(value, out convertedValue);

            if (type == typeof(int))
                return TryParseInt(value, out convertedValue);

            if (type == typeof(decimal))
                return TryParseDecimal(value, out convertedValue);

            if (type == typeof(float))
                return TryParseFloat(value, out convertedValue);

            if (type == typeof(double))
                return TryParseDouble(value, out convertedValue);

            if (type == typeof(long))
                return TryParseLong(value, out convertedValue);

            if (type == typeof(byte))
                return TryParseByte(value, out convertedValue);

            if (type == typeof(DateTime))
                return TryParseDateTime(value, out convertedValue);

            if (type == typeof(TimeSpan))
                return TryParseTimeSpan(value, out convertedValue);

            throw new ArgumentException($"There is no type convertor for {type}");
        }

        private static bool TryParseDouble(string value, out object convertedValue)
        {
            var result = double.TryParse(value, out double v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseFloat(string value, out object convertedValue)
        {
            var result = float.TryParse(value, out float v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseLong(string value, out object convertedValue)
        {
            var result = long.TryParse(value, out long v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseByte(string value, out object convertedValue)
        {
            var result = byte.TryParse(value, out byte v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseDateTime(string value, out object convertedValue)
        {
            var result = DateTime.TryParse(value, out DateTime v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseTimeSpan(string value, out object convertedValue)
        {
            var result = TimeSpan.TryParse(value, out TimeSpan v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseDecimal(string value, out object convertedValue)
        {
            var result = decimal.TryParse(value, out decimal v);
            convertedValue = v;
            return result;
        }

        private static bool TryParseString(string value, out object convertedValue)
        {
            convertedValue = value;
            return true;
        }

        private static bool TryParseInt(string value, out object convertedValue)
        {
            var result = int.TryParse(value, out int v);
            convertedValue = v;
            return result;
        }
    }
}