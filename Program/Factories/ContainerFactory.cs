using System.Linq;
using CliConsole;
using CliConsole.Convertors;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Convertors;
using CliConsole.Interfaces.Factories;
using FrameworkExtensions.Factories;
using FrameworkExtensions.Interfaces.Factories;
using FrameworkExtensions.LinqExtensions;
using Microsoft.Practices.Unity;
using Policy.Application;
using Policy.Application.Factories;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;
using Policy.Application.Interfaces.Repositories;
using Policy.Contracts.Services;
using Policy.Plugin.Isa.Policy.DataAccess.InMemory;
using Policy.Plugin.Isa.Policy.DataAccess.Sql;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;
using Program.Services;
using ICommand = Policy.Application.Interfaces.ICommand;

namespace Program.Factories
{
    public class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();

            container.RegisterInstance(container);
            container.RegisterType<IConsoleProxy, ConsoleProxy>();
            container.RegisterType<IConsoleDispatcher, ConsoleDispatcher>();
            container.RegisterType<IConsoleParser, ConsoleParser>();
            container.RegisterType<IValueConvertor, ValueConvertor>();
            container.RegisterType<ICommandInstanceFactory, ContainerCommandInstanceFactory>();
            container.RegisterType<IServiceHosting, ServiceHosting>();
            container.RegisterType<IRemoteClientService, RemoteClientService>();
            container.RegisterType<ICommandChannelClientFactory, CommandChannelClientFactory>();

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
            container.RegisterType<IEventPlayer<IsaPolicyEvent>, EventPlayer<IsaPolicyEvent>>();
            container.RegisterType<ILogFactory, LogFactory>();
            container.RegisterType<IStopwatchFactory, StopwatchFactory>();


            container.RegisterType<ISequencingRepository, SequencingSqlStore>();
            container.RegisterType<IUnitPricingRepository, UnitPricingInMemoryStore>();
            container.RegisterType<ISnapshotStore<PolicyView>, SinglePolicySnapshotMemoryStore>();
            container.RegisterType<ISnapshotStore<PolicyTransactionView>, SinglePolicyTransactionSnapshotMemoryStore>();
            container.RegisterType<IEventStoreRepository<IsaPolicyEvent>, IsaPolicyEventsInSqlStore>();
            container.RegisterType<IIsaPolicyEventStoreRepository, IsaPolicyEventsInSqlStore>();


            //SetupQueries(container);
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
            RegisterNamedTypes<ICommandHandler>(container);
            RegisterNamedTypes<ICommand>(container);
            container.RegisterType<ICommandDispatcherFactory, CommandDispatcherFactory>();
            container.RegisterType<IEventPlayerFactory, EventPlayerFactory>();



            //SetupQueries(container);
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.TypeName,
                WithLifetime.ContainerControlled);
            RegisterNamedTypes<IEventEvaluator>(container);

            RegisterConsoleCommands(container);

            return container;
        }
        
        private void RegisterConsoleCommands(IUnityContainer container)
        {
            var consoleCommands = GetType()
                .Assembly
                .GetTypes()
                .Where(t => t.IsPublic && !t.IsAbstract)
                .Where(t => typeof(ICommand).IsAssignableFrom(t));
            consoleCommands.ForEach(consoleCommand =>
            {
                container.RegisterType(typeof(ICommand), consoleCommand, consoleCommand.Name);
            });
        }

        private void RegisterNamedTypes<TType>(IUnityContainer container)
        {
            var types = GetType().Assembly.GetTypes().Where(t => t.IsAbstract == false && typeof(TType).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                container.RegisterType(typeof(TType), t, t.FullName, new ContainerControlledLifetimeManager());
            });
        }
    }
}