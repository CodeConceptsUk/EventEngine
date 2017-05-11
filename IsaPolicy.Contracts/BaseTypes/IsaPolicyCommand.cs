using System.Runtime.Serialization;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Operations.BaseTypes
{
    [DataContract]
    public abstract class IsaPolicyCommand : ICommand
    {
        
    }
}