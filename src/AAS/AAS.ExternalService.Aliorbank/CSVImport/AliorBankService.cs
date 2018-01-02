using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAS.Core.Contracts;
using AAS.ExternalService.Aliorbank.CSVImport.Model;
using CsvHelper;

namespace AAS.ExternalService.Aliorbank.CSVImport
{
    public class AliorBankService 
        : IBankService
    {


        public IEnumerable<BankTransaction> ImportTransactions()
        {
            using (var stream = File.OpenText(@"e:\export20171231152650.csv"))
            using (var csv = new CsvReader(stream))
            {
                csv.Configuration.RegisterClassMap<AliorBankCSVTransactionClassMap>();
                csv.Configuration.Encoding = Encoding.UTF8;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.BadDataFound = null;
                //(isValid, headerNames, headerNameIndex, context) =>
                //{
                //    //if (!isValid)
                //    //{
                //    //    logger.WriteLine($"Header matching ['{string.Join("', '", headerNames)}'] names at index {headerNameIndex} was not found.");
                //    //}
                //};
                var records = csv.GetRecords<AliorBankCSVTransaction>().ToList();

                //List<AliorBankCSVTransaction> transactions = new List<AliorBankCSVTransaction>();

                //while (csv.Read())
                //{
                //    transactions.Add();
                //}

                return records.Select(x => new BankTransaction
                {
                    Info = new TransactionInfo
                    {
                        WhenBooked = DateTime.ParseExact(x.WhenBooked, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                        Description = x.Description,
                        When = DateTime.ParseExact(x.When, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                        Title = x.TitleLine1 + x.TitleLine2 + x.TitleLine3 + x.TitleLine4,
                       
                    },
                    //ReferencedNr = 
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
                        Amount = decimal.Parse(x.Amount),
                        Currency = x.Currency,
                    }
                });
            }
        }

        //private AliorBankCSVTransaction ReadRecord(CsvReader csvReader)
        //{
        //    var transaction = new AliorBankCSVTransaction();

        //    transaction.WhenBooked = csvReader[0];


        //}
    }
}
