using System.Runtime.Serialization;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/queries/isapolicy")]
    public class GetPolicyForPolicyNumberQuery : IQuery
    {
        public GetPolicyForPolicyNumberQuery(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        [DataMember]
        public string PolicyNumber { get; set; }
    }
}