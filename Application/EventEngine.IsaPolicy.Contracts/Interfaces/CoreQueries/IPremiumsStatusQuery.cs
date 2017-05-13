﻿using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPremiumsStatusQuery : IQuery
    {
        PremiumsStatusView Read(Guid contextId);
    }
}