using System;
using EventEngine.Application.Interfaces;

namespace EventEngine.UnitTests
{
    public class StateObject : IView
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}