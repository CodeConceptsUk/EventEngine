﻿using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
using Policy.Plugin.Isa.Policy.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface ISinglePolicyQuery : IQuery
    {
        PolicyView Build(Guid contextId);
    }
}