using System;
using AAS.Persistance;

namespace AAS.Core.Contracts.Model
{
    public class TransactionBucketType
        : Dict<int, string>
            , IEntity
    {
        public TransactionBucketType()
        { }

        public TransactionBucketType(string value)
            : base(value)
        { }
    }

    public abstract class Dict<TKey, TValue>
        : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Dict(){}

        protected Dict(TValue value)
        {
            Value = value;
        }

        public virtual TValue Value { get; }
    }
}