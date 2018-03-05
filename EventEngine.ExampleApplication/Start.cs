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
            
            var setNameCommand = new SetNameCommand { Name = "Name1" };
            commandDispatcher.Dispatch(contextId, setNameCommand);

            setNameCommand = new SetNameCommand { Name = "Name2" };
            commandDispatcher.Dispatch(contextId, setNameCommand);

            var setDateOfBirthCommand = new SetDateOfBirthCommand { DateOfBirth = DateTime.Now };
            commandDispatcher.Dispatch(contextId, setDateOfBirthCommand);
            
            setNameCommand = new SetNameCommand { Name = "Name3" };
            commandDispatcher.Dispatch(contextId, setNameCommand);

            var setDateOfBirth2Command = new SetDateOfBirth2Command { DateOfBirth = DateTime.Now.AddDays(1) };
            commandDispatcher.Dispatch(contextId, setDateOfBirth2Command);

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