using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Api.Contracts.Resolvers
{
    public class CommandContractResolver : DataContractResolver
    {
        private readonly Container _container;
        private readonly XmlDictionary _xmlDictionary = new XmlDictionary();

        public CommandContractResolver(Container container)
        {
            _container = container;
        }

        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver,
            out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            if (typeof(ICommand).IsAssignableFrom(type) || 
                typeof(IView).IsAssignableFrom(type) ||
                typeof(IQuery).IsAssignableFrom(type))
            {
                typeName = _xmlDictionary.Add(type.Name);
                typeNamespace = _xmlDictionary.Add(type.Namespace ?? string.Empty);
                return true;
            }

            return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
        }

        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            var commandType = _container
                .Registrations
                .Where(t => t.RegisteredType == typeof(ICommand) || t.RegisteredType == typeof(IView) || t.RegisteredType == typeof(IQuery))
                .Select(t => t.MappedToType)
                .SingleOrDefault(t => t.Namespace == typeNamespace && t.Name == typeName);

            return commandType ?? knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, knownTypeResolver);
        }
    }
}