using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetSinglePolicyFromContextQuery : IQuery
    {
        public GetSinglePolicyFromContextQuery(Guid contextId)
        {
            ContextId = contextId;
        }

        [DataMember]
        public Guid ContextId { get; set; }
    }

    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetSinglePolicyQuery : IQuery
    {
        public GetSinglePolicyQuery(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        [DataMember]
        public string PolicyNumber { get; set; }
    }

    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetCustomerSinglePolicyQuery : IQuery
    {
        public GetCustomerSinglePolicyQuery(int customerId)
        {
            CustomerId = customerId;
        }

        [DataMember]
        public int CustomerId { get; set; }
    }
}
