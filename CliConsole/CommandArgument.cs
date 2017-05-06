using System;

namespace CliConsole
{
    public class CommandArgument<T> : CommandArgument
    {
        public CommandArgument(string name, string description, Action<T> action, bool isRequired = false)
            : base(name, description, isRequired)
        {
            Action = action;
        }

        public Action<T> Action { get; }
    }

    public abstract class CommandArgument
    {
        protected CommandArgument(string name, string description, bool isRequired = false)
        {
            IsRequired = isRequired;
            Name = name;
            Description = description;
        }

        public bool IsRequired { get; }
        public string Name { get; }
        public string Description { get; }
    }
}