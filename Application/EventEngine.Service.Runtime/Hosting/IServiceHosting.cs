using System;

namespace CodeConcepts.EventEngine.Services.Hosting
{
    public interface IServiceHosting : IDisposable
    {
        void Start();
    }
}