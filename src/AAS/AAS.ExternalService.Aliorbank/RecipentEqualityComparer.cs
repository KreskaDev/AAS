using System.Collections.Generic;
using AAS.Core.Contracts;
using AAS.Core.Contracts.Model;

namespace AAS.ExternalService.Aliorbank
{
    public class RecipentEqualityComparer
        : IEqualityComparer<BankAccount>
    {
        public bool Equals(BankAccount x, BankAccount y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(BankAccount obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}