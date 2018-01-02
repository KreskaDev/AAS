using System;
using System.Collections.Generic;
using System.Linq;
using AAS.Core.Contracts;
using AAS.ExternalService.Aliorbank.CSVImport;
using Shouldly;
using Xunit;

namespace AAS.Tests
{
    public class UnitTest1
    {
        private IEnumerable<BankTransaction> Execute()
        {
            return new AliorBankService().ImportTransactions();
        }

        [Fact]
        public void TestMethod1()
        {
            var result = Execute().ToList();
            result.Count.ShouldBe(3936);
        }
    }
}
