using System;

namespace CodeConcepts.EventEngine.Api.Contracts
{
    public interface ITypeResolver
    {
        Type ResolveType(string @namespace, string typeName);
    }
    //TODO consider this
}