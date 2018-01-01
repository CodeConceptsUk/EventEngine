using System;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Players;
using EventEngine.Application.Services;
using EventEngine.ExampleApplication.Commands;
using Newtonsoft.Json;
using Unity;
using Unity.Container.Lifetime;
using Unity.Extension;

namespace EventEngine.ExampleApplication
{
    public class Start
    {
        public static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();
            var player = container.Resolve<IEventPlayer>();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var eventStore = container.Resolve<IEventStore>();
            var eventFactory = container.Resolve<IEventFactory>();
            SetupEventEvaluators(container);
            SetupCommandHandlers(container, eventFactory);

            var contextId = Guid.NewGuid();
            var view = new ExampleView();

            var setNameCommand = new SetNameCommand { ContextId = contextId, Name = Guid.NewGuid().ToString() };
            commandDispatcher.Dispatch(setNameCommand);
            player.Play(eventStore.Get(), view);
            ViewToConsole(view);

            setNameCommand = new SetNameCommand { ContextId = contextId, Name = Guid.NewGuid().ToString() };
            commandDispatcher.Dispatch(setNameCommand);
            player.Play(eventStore.Get(), view);
            ViewToConsole(view);

            var setDateOfBirthCommand = new SetDateOfBirthCommand { ContextId = contextId, DateOfBirth = DateTime.Now };
            commandDispatcher.Dispatch(setDateOfBirthCommand);
            player.Play(eventStore.Get(), view);
            ViewToConsole(view);

            setNameCommand = new SetNameCommand { ContextId = contextId, Name = Guid.NewGuid().ToString() };
            commandDispatcher.Dispatch(setNameCommand);
            player.Play(eventStore.Get(), view);
            ViewToConsole(view);

            var setDateOfBirth2Command = new SetDateOfBirth2Command { ContextId = contextId, DateOfBirth = DateTime.Now.AddDays(1) };
            commandDispatcher.Dispatch(setDateOfBirth2Command);
            player.Play(eventStore.Get(), view);
            ViewToConsole(view);

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