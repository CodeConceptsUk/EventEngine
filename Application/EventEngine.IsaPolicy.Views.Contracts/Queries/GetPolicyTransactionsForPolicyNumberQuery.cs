using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetPolicyTransactionsForPolicyNumberQuery : IsaPolicyQuery
    {
        public GetPolicyTransactionsForPolicyNumberQuery(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        [DataMember]
        public string PolicyNumber { get; set; }
    }
}