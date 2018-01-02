using System.Collections.Generic;

namespace AAS.Core.Contracts
{
    public interface IBankService
    {
        IEnumerable<BankTransaction> ImportTransactions();
    }
}