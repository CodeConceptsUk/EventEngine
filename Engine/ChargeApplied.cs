namespace Engine
{
    public class ChargeApplied : IEvent
    {
        public string PolicyNumber { get; }
        public string Fundid { get; }
        public decimal Charge { get; }

        public ChargeApplied(string policyNumber, string fundid, decimal charge)
        {
            PolicyNumber = policyNumber;
            Fundid = fundid;
            Charge = charge;
        }
    }
}
