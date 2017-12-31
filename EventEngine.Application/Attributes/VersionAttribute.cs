using System;

namespace EventEngine.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public VersionAttribute(int major = 0, int minor = 0, int build = 0, int revision = 0)
        {
            Version = new Version(major, minor, build, revision);
        }

        public Version Version { get; }
    }
}