using System;

namespace AAS.Core.Contracts.Model.Vo
{
    public class TransactionInfo
    {
        public virtual string Title1 { get; set; }
        public virtual string Title2 { get; set; }
        public virtual string Title3 { get; set; }
        public virtual string Title4 { get; set; }
        public virtual TransactionBucketType BucketType { get; set; }
        //public virtual string Description { get; set; }
        public virtual DateTime WhenHappened { get; set; }
        public virtual DateTime WhenBooked { get; set; }
    }
}