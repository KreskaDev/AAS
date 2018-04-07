using AAS.Persistance;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace AAS.Core.Contracts.Model
{
    public class BankAccount
        : Entity<int>
        , IEntity
    {
        public virtual string Number { get; set; }
        public virtual string Name { get; set; }
        public virtual string Adress { get; set; }
    }

    public class BankAccountMap
        : IAutoMappingOverride<BankAccount>
    {
        public void Override(AutoMapping<BankAccount> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Identity();
        }
    }

}