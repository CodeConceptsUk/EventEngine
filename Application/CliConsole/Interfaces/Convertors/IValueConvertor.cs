using System;

namespace CliConsole.Interfaces.Convertors
{
    public interface IValueConvertor
    {
        bool TryConvert(Type type, string value, out object convertedValue);
    }
}