using System;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface ISinglePolicyQuery
    {
        PolicyView Build(Guid contextId);
    }
}