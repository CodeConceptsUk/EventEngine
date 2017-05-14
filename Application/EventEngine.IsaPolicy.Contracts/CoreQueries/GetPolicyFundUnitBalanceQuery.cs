using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetPolicyFundUnitBalanceQuery : IsaPolicyQuery, IEventContextIdQuery
    {
        public GetPolicyFundUnitBalanceQuery(Guid eventContextId)
        {
            EventContextId = eventContextId;
        }

        [DataMember]
        public Guid EventContextId { get; set; }
    }
}