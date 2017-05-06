using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Queries
{
    public interface ISinglePolicyQuery : IQuery
    {
        PolicyView Read(Guid contextId);
    }
}