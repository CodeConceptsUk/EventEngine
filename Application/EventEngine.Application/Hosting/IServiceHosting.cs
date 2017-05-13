using System;

namespace CodeConcepts.EventEngine.Application.Hosting
{
    public interface IServiceHosting : IDisposable
    {
        void Start();
    }
}