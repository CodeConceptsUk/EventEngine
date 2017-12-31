using System;

namespace EventEngine.Application.Dispatchers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public VersionAttribute(string name, int major = 0, int minor = 0, int build = 0, int revision = 0)
        {
            Name = name;
            Version = new Version(major, minor, build, revision);
        }

        public Version Version { get; }

        public string Name { get; } // TODO: TEST ME <-
    }
}