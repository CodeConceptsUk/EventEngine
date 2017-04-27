using System.Collections.Generic;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Views
{
    public class PolicyView : IView<IPolicyContext>
    {
        public PolicyView()
        {
            Funds = new List<IFund>();
        }

        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }

        public IList<IFund> Funds { get; set; }
    }
}