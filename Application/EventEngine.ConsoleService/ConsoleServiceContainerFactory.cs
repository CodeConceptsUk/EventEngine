using System.Collections.Generic;
using System.Reflection;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application;
using CodeConcepts.EventEngine.Application.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.Sql;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleService
{
    public class ConsoleServiceContainerFactory : ServiceContainerFactory
    {
        //TODO - this is less than ideal - get plugin system
        protected override void SetupSpecificRegistrations(Container container)
        {
            base.SetupSpecificRegistrations(container);
            container.Register<ISequencingRepository, SqlSequencingRepository>();
            container.Register<IUnitPricingRepository, InMemoryUnitPricingRepository>();
            
            //TODO snapshots need to be application lifetime

            var assemblies = new List<Assembly>
            {
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Contracts)}"),
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"),
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.Views.{nameof(IsaPolicy.Views.Contracts)}"),
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}")
            };

            RegisterByConventionFromAssemblies(assemblies);

            container.Register(typeof(IEventStoreRepository<>), assemblies);
            container.RegisterCollection<ICommand>(assemblies);
            container.RegisterCollection<ICommandHandler>(assemblies);
            container.Register(typeof(ICommandHandler<,>), assemblies);
            container.RegisterCollection<IQuery>(assemblies);
            container.RegisterCollection<IQueryHandler>(assemblies);
            container.Register(typeof(IQueryHandler<,>),assemblies);
            container.RegisterCollection<ISnapshotStore>(assemblies);
            container.Register(typeof(ISnapshotStore<>),assemblies);
            container.RegisterCollection<IEventEvaluator>(assemblies);
            container.Register(typeof(IEventEvaluator<,>), assemblies);

            //TODO register
            //register all interfaces of IQuery
            //register all interfaces of IQueryHandler
            
        }

        private static void RegisterByConventionFromAssemblies(List<Assembly> assemblies)
        {
            assemblies.ForEach(ass => { ass.GetTypes(); });
        }
    }
}