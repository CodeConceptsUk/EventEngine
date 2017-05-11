using System;

namespace CodeConcepts.CliConsole.Interfaces.Convertors
{
    public interface IValueConvertor
    {
        bool TryConvert(Type type, string value, out object convertedValue);
    }
}