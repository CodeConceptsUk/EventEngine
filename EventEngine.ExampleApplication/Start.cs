using System;
using System.Linq;
using System.Threading;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Services;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Interfaces.Queries;
using Newtonsoft.Json;
using Unity;

namespace EventEngine.ExampleApplication
{
    public class Start
    {
        public static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();
            var exampleViewQuery = container.Resolve<IExampleViewQuery>();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var eventFactory = container.Resolve<IEventFactory>();
            SetupEventEvaluators(container);
            SetupCommandHandlers(container, eventFactory);

            var contextId = Guid.NewGuid();
            
            var setNameCommand = new SetNameCommand { ContextId = contextId, Name = "Name1" };
            commandDispatcher.Dispatch(setNameCommand);

            setNameCommand = new SetNameCommand { ContextId = contextId, Name = "Name2" };
            commandDispatcher.Dispatch(setNameCommand);

            var setDateOfBirthCommand = new SetDateOfBirthCommand { ContextId = contextId, DateOfBirth = DateTime.Now };
            commandDispatcher.Dispatch(setDateOfBirthCommand);
            
            setNameCommand = new SetNameCommand { ContextId = contextId, Name = "Name3" };
            commandDispatcher.Dispatch(setNameCommand);

            var setDateOfBirth2Command = new SetDateOfBirth2Command { ContextId = contextId, DateOfBirth = DateTime.Now.AddDays(1) };
            commandDispatcher.Dispatch(setDateOfBirth2Command);

            var exampleView = exampleViewQuery.Get(contextId);
            ViewToConsole(exampleView);

            Console.ReadLine();
        }

        private static void ViewToConsole(ExampleView view)
        {
            Console.WriteLine(JsonConvert.SerializeObject(view, Formatting.Indented));
        }

        private static void SetupCommandHandlers(IUnityContainer container, IEventFactory eventFactory)
        {
            var commandHandlerRegistry = container.Resolve<ICommandHandlerRegistry>();
            foreach (var commandHandler in ContainerFactory.GetAllCommandHandlers(container, eventFactory))
            {
                commandHandlerRegistry.Register(commandHandler);
            }
            container.RegisterInstance(typeof(ICommandHandlerRegistry), commandHandlerRegistry);
        }

        private static void SetupEventEvaluators(IUnityContainer container)
        {
            var eventEvaluatorRegistry = container.Resolve<IEventEvaluatorRegistry>();
            foreach (var eventEvaluator in ContainerFactory.GetAllEventEvaluators(container))
            {
                eventEvaluatorRegistry.Register(eventEvaluator);
            }
            container.RegisterInstance(typeof(IEventEvaluatorRegistry), eventEvaluatorRegistry);
        }
    }
}