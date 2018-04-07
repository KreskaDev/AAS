using System;
using AAS.Core.Contracts.Model.Vo;
using AAS.Persistance;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace AAS.Core.Contracts.Model
{
    public class BankTransaction
        : Entity<int>
        , IEntity
    {
        public virtual TransactionInfo Info { get; set; }
        public virtual BankAccount Sender { get; set; }
        public virtual BankAccount Recipent { get; set; }
        public virtual string ReferencedNr { get; set; }
        public virtual Money Amount { get; set; }
        public virtual Money BalanceAfterOperation { get; set; }
    }

    public class BankTransactionMap
        : IAutoMappingOverride<BankTransaction>
    {
        public void Override(AutoMapping<BankTransaction> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Identity();
            mapping.Component(x => x.Info, src =>
            {
                src.Map(x => x.Title1);
                src.Map(x => x.Title2);
                src.Map(x => x.Title3);
                src.Map(x => x.Title4);
                src.References(x => x.BucketType);/*.Map(x => x.Description);*/
                src.Map(x => x.WhenHappened);
                src.Map(x => x.WhenBooked);
            });
            mapping.Component(x => x.Amount, src =>
            {
                src.Map(x => x.Amount).Column("TransactionAmount");
                src.Map(x => x.Currency).Column("TransactionCurrency");
            });
            mapping.Component(x => x.BalanceAfterOperation, src =>
            {
                src.Map(x => x.Amount).Column("BalanceAmount");
                src.Map(x => x.Currency).Column("BalanceCurrency");
            });
            mapping.References(x => x.Recipent).Column("recipentId");
            mapping.References(x => x.Sender).Column("senderId");
        }
    }
}
