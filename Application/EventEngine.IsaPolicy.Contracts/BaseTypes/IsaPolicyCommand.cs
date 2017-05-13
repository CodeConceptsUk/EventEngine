using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    [DataContract]
    public abstract class IsaPolicyCommand : ICommand
    {
        
    }
}