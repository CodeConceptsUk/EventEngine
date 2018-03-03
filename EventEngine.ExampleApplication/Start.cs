using System;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Interfaces.Queries;
using EventEngine.Interfaces.Commands;
using Newtonsoft.Json;

namespace EventEngine.ExampleApplication
{
    public class Start
    {
        public static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();
            var exampleViewQuery = container.GetInstance<IExampleViewQuery>();
            var commandDispatcher = container.GetInstance<ICommandDispatcher>();

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
        
    }
}