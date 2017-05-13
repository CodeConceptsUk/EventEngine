using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Contracts.Resolvers
{
    public class CommandContractResolver : DataContractResolver
    {
        private readonly IUnityContainer _container;
        private readonly XmlDictionary _xmlDictionary = new XmlDictionary();

        public CommandContractResolver(IUnityContainer container)
        {
            _container = container;
        }

        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver,
            out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            if (!typeof(ICommand).IsAssignableFrom(type))
                return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);

            typeName = _xmlDictionary.Add(type.Name);
            typeNamespace = _xmlDictionary.Add(type.Namespace ?? string.Empty);
            return true;
        }

        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            var commandType = _container
                .Registrations
                .Where(t => t.RegisteredType == typeof(ICommand))
                .Select(t => t.MappedToType)
                .SingleOrDefault(t => t.Namespace == typeNamespace && t.Name == typeName);

            return commandType ?? knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, knownTypeResolver);
        }
    }
}