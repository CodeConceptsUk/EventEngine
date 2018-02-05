using System;

namespace EventEngine.PropertyBags
{
    public class EventValidityData
    {
        public string EventName { get; set; }
        public Version MinimumVersion { get; set; }
        public Version MaximumVersion { get; set; }
    }
}