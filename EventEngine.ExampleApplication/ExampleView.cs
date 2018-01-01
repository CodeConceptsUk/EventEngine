using System;
using EventEngine.Application.Interfaces;

namespace EventEngine.ExampleApplication
{
    public class ExampleView : IView
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? HourOfBirth { get; set; }
    }
}