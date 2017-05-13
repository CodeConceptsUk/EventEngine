﻿using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Events.Events
{
    public class AddPremiumEvent : IsaPolicyEvent
    {
        public AddPremiumEvent(Guid eventContextId, string premiumId, DateTime premiumDateTime, IEnumerable<PremiumPartitionDetails> partitionDetails)
            : base(eventContextId)
        {
            PremiumId = premiumId;
            PremiumDateTime = premiumDateTime;
            PartitionDetails = partitionDetails;
        }

        public string PremiumId { get; set; }

        public DateTime PremiumDateTime { get; set; }

        public IEnumerable<PremiumPartitionDetails> PartitionDetails { get; }
    }

    public class PremiumPartitionDetails
    {
        public string FundId { get; set; }

        public decimal Amount { get; set; }

        public Guid PartitionId { get; set; }
    }
}