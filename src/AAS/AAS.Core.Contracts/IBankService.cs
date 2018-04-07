using System.Collections.Generic;
using AAS.Core.Contracts.Model;

namespace AAS.Core.Contracts
{
    public interface IBankService
    {
        IEnumerable<BankTransaction> ImportTransactions(IList<BankAccount> bankAccounts);
        IList<BankAccount> GetRecipents();
        IList<BankAccount> GetSenders();
        IList<BankAccount> GetBankAccounts();
        IList<string> GetTransactionBucketTypes();
    }
}