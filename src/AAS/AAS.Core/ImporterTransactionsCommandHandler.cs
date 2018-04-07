using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAS.Core.Contracts;
using AAS.Core.Contracts.Model;
using AAS.ExternalService.Aliorbank.CSVImport;
using AAS.Persistance;
using NHibernate.Linq;

namespace AAS.Core
{
    public class ImporterTransactionsCommandHandler
    {
        private readonly NhSessionProvider _nhSessionProvider;
        private readonly IBankService _bankService;

        public ImporterTransactionsCommandHandler(Stream stream, NhSessionProvider nhSessionProvider)
        {
            _nhSessionProvider = nhSessionProvider;
            _bankService = new AliorBankService(stream);
        }

        public void Handle()
        {
            using (var session = _nhSessionProvider.OpenSession())
            {
                //var bankAccounts = new List<BankAccount>();
                //bankAccounts.AddRange(_bankService.GetRecipents());
                //bankAccounts.AddRange(_bankService.GetSenders());
                
                var transactionBucketTypes = _bankService.GetTransactionBucketTypes();
                foreach (var transactionBucketType in transactionBucketTypes)
                {
                    session.Save(new TransactionBucketType(transactionBucketType));
                }

                var bankAccounts = _bankService.GetBankAccounts();
                foreach (var bankAccount in bankAccounts.Distinct(new BankAccountEqualityComparer()))
                {
                    session.Save(bankAccount);
                }

                var transactions = _bankService.ImportTransactions(session.Query<BankAccount>().ToList()).ToList();

                foreach (var bankTransaction in transactions)
                {
                    session.Save(bankTransaction);
                }
            }
        }
    }

    public class BankAccountEqualityComparer : IEqualityComparer<BankAccount>
    {
        public bool Equals(BankAccount x, BankAccount y)
        {
            return x?.Name.Equals(y?.Name) ?? false;
            //&& x.Adress.Equals(y.Adress)
            //&& x.Number.Equals(y.Number);
        }

        public int GetHashCode(BankAccount obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
