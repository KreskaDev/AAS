using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AAS.Core.Contracts;
using AAS.Core.Contracts.Model;
using AAS.ExternalService.Aliorbank;
using AAS.ExternalService.Aliorbank.CSVImport;
using Shouldly;
using Xunit;

namespace AAS.Tests
{
    public class ReadingFromCSVImportedTransactionsSet_Tests
    {
        private static string _medicalResourceName = "AAS.Tests._CSV.export20171231152650.csv";

        private IEnumerable<BankTransaction> Execute()
        {
            var csvFile = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(_medicalResourceName);

            return new AliorBankService(csvFile)
                .ImportTransactions();
        }

        [Fact]
        public void Reading_From_CSV()
        {
            var result = Execute().ToList();
            result.Count.ShouldBe(3936);

            result[0].ReferencedNr.ShouldBe(null);//TODO:ShouldNotBe

            result[0].Info.WhenBooked.ShouldBe(new DateTime(2017, 12, 30));
            result[0].Info.WhenHappened.ShouldBe(new DateTime(2017, 12, 28));
            result[0].Info.Title1.ShouldBe("Transakcja kartą debetową;obciążenie 15.50 PLN z dnia:2017-12-28 Kod MCC 5921  POL,BIALYSTOK SKLEP MONOPOLOWY");
            result[0].Info.Description.ShouldBe("Transakcja kartą debetową");

            result[0].Amount.Amount.ShouldBe(-15.50m);
            result[0].Amount.Currency.ShouldBe("PLN");

            result[0].BalanceAfterOperation.Amount.ShouldBe(9298.50m);
            result[0].BalanceAfterOperation.Currency.ShouldBe("PLN");

            result[0].Sender.Name.ShouldBe("Rafał Krasowski ");
            result[0].Sender.Adress.ShouldBe(null);
            result[0].Sender.Number.ShouldBe(null);

            result[0].Recipent.Name.ShouldBe(" ");
            result[0].Recipent.Adress.ShouldBe(null);
            result[0].Recipent.Number.ShouldBe(null);
        }

        [Fact]
        public void Query()
        {
            var result = Execute().ToList();

            var recipient = result
                .Select(x => x.Recipent)
                .Distinct(new RecipentEqualityComparer())
                .ToList();
            recipient.Count.ShouldBe(53);

            var senders = result
                .Select(x => x.Sender)
                .Distinct(new RecipentEqualityComparer())
                .ToList();
            senders.Count.ShouldBe(25);
        }
    }
}
