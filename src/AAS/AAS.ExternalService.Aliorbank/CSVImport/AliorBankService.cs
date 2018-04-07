using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAS.Core.Contracts;
using AAS.Core.Contracts.Model;
using AAS.Core.Contracts.Model.Vo;
using AAS.ExternalService.Aliorbank.CSVImport.Model;
using CsvHelper;

namespace AAS.ExternalService.Aliorbank.CSVImport
{
    public class AliorBankService
        : IBankService
        , IDisposable
    {
        private readonly CsvReader _csvReader;

        private IList<AliorBankCSVTransaction> _transactions;

        public IList<AliorBankCSVTransaction> Transactions
            => _transactions ?? (_transactions = _csvReader.GetRecords<AliorBankCSVTransaction>().ToList());

        public AliorBankService(Stream stream)
        {
            _csvReader = new CsvReader(new StreamReader(stream, Encoding.GetEncoding(1250)));//, Encoding.Default));
            _csvReader.Configuration.RegisterClassMap<AliorBankCSVTransactionClassMap>();
            //_csvReader.Configuration.Encoding = Encoding.UTF8;
            //_csvReader.Configuration.Encoding = Encoding.Unicode;
            //_csvReader.Configuration.Encoding = Encoding.GetEncoding("windows-1254");
            //_csvReader.Configuration.Encoding = Encoding.GetEncoding("iso-8859-1");
            //_csvReader.Configuration.Encoding = Encoding.GetEncoding(1250);
            //_csvReader.Configuration.Encoding = Encoding.;
            _csvReader.Configuration.Delimiter = ";";
            _csvReader.Configuration.HeaderValidated = null;
            _csvReader.Configuration.BadDataFound = null;
            //(isValid, headerNames, headerNameIndex, context) =>
            //{
            //    //if (!isValid)
            //    //{
            //    //    logger.WriteLine($"Header matching ['{string.Join("', '", headerNames)}'] names at index {headerNameIndex} was not found.");
            //    //}
            //};
        }

        public IEnumerable<BankTransaction> ImportTransactions()
        {
            return Transactions.Select(x => new BankTransaction
            {
                //ReferencedNr = 
                Info = new TransactionInfo
                {
                    WhenBooked = DateTime.ParseExact(x.WhenBooked, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Description = x.Description,
                    WhenHappened = DateTime.ParseExact(x.When, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Title1 = x.TitleLine1,
                    Title2 = x.TitleLine2,
                    Title3 = x.TitleLine3,
                    Title4 = x.TitleLine4,
                },
                Recipent = new BankAccount
                {
                    Name = x.Recipent,
                },
                Sender = new BankAccount
                {
                    Name = x.Sender
                },
                Amount = new Money
                {
                    Amount = decimal.Parse(x.Amount, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
                BalanceAfterOperation = new Money
                {
                    Amount = decimal.Parse(x.BalanceAfterOperation, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
            });
        }


        public IEnumerable<BankTransaction> ImportTransactions(IList<BankAccount> bankAccounts)
        {
            return Transactions.Select(x => new BankTransaction
            {
                //ReferencedNr = 
                Info = new TransactionInfo
                {
                    WhenBooked = DateTime.ParseExact(x.WhenBooked, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Description = x.Description,
                    WhenHappened = DateTime.ParseExact(x.When, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Title1 = x.TitleLine1,
                    Title2 = x.TitleLine2,
                    Title3 = x.TitleLine3,
                    Title4 = x.TitleLine4,
                },
                Recipent = bankAccounts.Single(y => y.Name == x.Recipent),
                Sender = bankAccounts.Single(y => y.Name == x.Sender),
                Amount = new Money
                {
                    Amount = decimal.Parse(x.Amount, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
                BalanceAfterOperation = new Money
                {
                    Amount = decimal.Parse(x.BalanceAfterOperation, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
            });
        }

        public IList<BankAccount> GetRecipents()
        {
            return Transactions.Select(x => new BankTransaction
            {
                //ReferencedNr = 
                Info = new TransactionInfo
                {
                    WhenBooked = DateTime.ParseExact(x.WhenBooked, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Description = x.Description,
                    WhenHappened = DateTime.ParseExact(x.When, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Title1 = x.TitleLine1,
                    Title2 = x.TitleLine2,
                    Title3 = x.TitleLine3,
                    Title4 = x.TitleLine4,
                },
                Recipent = new BankAccount
                {
                    Name = x.Recipent,
                },
                Sender = new BankAccount
                {
                    Name = x.Sender
                },
                Amount = new Money
                {
                    Amount = decimal.Parse(x.Amount, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
                BalanceAfterOperation = new Money
                {
                    Amount = decimal.Parse(x.BalanceAfterOperation, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
            })
            .Select(x => x.Recipent)
            .Distinct(new RecipentEqualityComparer())
            .ToList();
        }

        public IList<BankAccount> GetSenders()
        {
            return Transactions.Select(x => new BankTransaction
            {
                //ReferencedNr = 
                Info = new TransactionInfo
                {
                    WhenBooked = DateTime.ParseExact(x.WhenBooked, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Description = x.Description,
                    WhenHappened = DateTime.ParseExact(x.When, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Title1 = x.TitleLine1,
                    Title2 = x.TitleLine2,
                    Title3 = x.TitleLine3,
                    Title4 = x.TitleLine4,
                },
                Recipent = new BankAccount
                {
                    Name = x.Recipent,
                },
                Sender = new BankAccount
                {
                    Name = x.Sender
                },
                Amount = new Money
                {
                    Amount = decimal.Parse(x.Amount, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
                BalanceAfterOperation = new Money
                {
                    Amount = decimal.Parse(x.BalanceAfterOperation, CultureInfo.GetCultureInfo("pl-PL")),
                    Currency = x.Currency,
                },
            })
            .Select(x => x.Sender)
            .Distinct(new RecipentEqualityComparer())
            .ToList();
        }

        public IList<BankAccount> GetBankAccounts()
        {
            return Transactions.SelectMany(x
            => new List<BankAccount>
            {
                new BankAccount{Name = x.Recipent,},
                new BankAccount{Name = x.Sender},
            })
            .Distinct(new RecipentEqualityComparer())
            .ToList();
        }

        public IList<string> GetTransactionBucketTypes() 
        => Transactions
        .Select(x => x.Description)
        .Distinct()
        .ToList();

        public void Dispose()
        {


        }
    }
}
