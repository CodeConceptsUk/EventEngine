using System;
using System.CodeDom;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;

namespace CodeConcepts.EventEngine.Application.Interfaces.Factories
{
    public interface IEventRepositoryResolver
    {
        IEventStoreRepository Resolve(Type eventType);
    }
}