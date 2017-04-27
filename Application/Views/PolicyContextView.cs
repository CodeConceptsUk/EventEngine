using System;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Views
{
    public class PolicyContextView : IView<IPolicyContext>
    {
        public Guid EventContextId { get; set; }
    }
}