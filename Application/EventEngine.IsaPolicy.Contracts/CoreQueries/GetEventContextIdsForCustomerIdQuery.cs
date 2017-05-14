using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetEventContextIdsForCustomerIdQuery : IsaPolicyQuery
    {
        public GetEventContextIdsForCustomerIdQuery(string customerId)
        {
            CustomerId = customerId;
        }

        [DataMember]
        public string CustomerId { get; set; }
    }
}