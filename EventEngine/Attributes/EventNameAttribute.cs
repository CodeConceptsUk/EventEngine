using System;

namespace EventEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventNameAttribute : Attribute
    {
        public EventNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}