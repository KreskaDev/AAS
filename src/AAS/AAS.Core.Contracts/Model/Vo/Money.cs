namespace AAS.Core.Contracts.Model
{
    public class Money // ValueObject
    {
        public virtual string Currency { get; set; }
        public virtual decimal Amount { get; set; }
    }
}