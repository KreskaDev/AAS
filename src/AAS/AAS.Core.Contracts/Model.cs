using System;

namespace AAS.Core.Contracts
{
    public class BankTransaction
    {
        public TransactionInfo Info { get; set; }
        public BankAccount Sender { get; set; }
        public BankAccount Recipent { get; set; }
        public string ReferencedNr { get; set; }
        public Money Amount { get; set; }
    }

    public class BankAccount
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
    }

    public class TransactionInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public DateTime WhenBooked { get; set; }
    }

    public class Money
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
