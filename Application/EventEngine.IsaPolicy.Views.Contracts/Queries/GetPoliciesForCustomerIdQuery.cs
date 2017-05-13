using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetPoliciesForCustomerIdQuery : IQuery
    {
        public GetPoliciesForCustomerIdQuery(int customerId)
        {
            CustomerId = customerId;
        }

        [DataMember]
        public int CustomerId { get; set; }
    }
}