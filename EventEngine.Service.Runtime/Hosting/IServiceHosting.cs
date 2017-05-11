using System;

namespace Program.Services
{
    public interface IServiceHosting : IDisposable
    {
        void Start();
    }
}