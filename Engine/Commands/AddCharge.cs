using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Commands
{
    public interface ICommand
    {
    }

    public class AddCharge : ICommand
    {
        public string PolicyId { get; }
        public string FundId { get; }
        public decimal Value { get; }

        public AddCharge(string policyId, string fundId, decimal value)
        {
            PolicyId = policyId;
            FundId = fundId;
            Value = value;
        }
    }
}
