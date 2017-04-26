using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Views
{
    public class PolicyView : IView<IPolicyContext>
    {
        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }
    }
}