using System;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain
{
    public class Transaction
    {
        public string Type { get; set; }

        public decimal Value { get; set; }

        public DateTime TransactionDateTime { get; set; }
    }
}