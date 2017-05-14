using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetPolicyTransactionsForEventContextIdQuery : IQuery
    {
        public GetPolicyTransactionsForEventContextIdQuery(Guid contextId)
        {
            ContextId = contextId;
        }

        [DataMember]
        public Guid ContextId { get; set; }
    }
}