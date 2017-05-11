using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    [DataContract]
    public abstract class IsaPolicyCommand : ICommand
    {
        
    }
}