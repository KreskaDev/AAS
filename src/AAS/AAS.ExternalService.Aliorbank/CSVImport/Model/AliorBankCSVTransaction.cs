using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace AAS.ExternalService.Aliorbank.CSVImport.Model
{
    public class AliorBankCSVTransaction
    {

        public string WhenBooked { get; set; }
        public string When { get; set; }
        public string Sender { get; set; }
        public string Recipent { get; set; }
        public string TitleLine1 { get; set; }
        public string TitleLine2 { get; set; }
        public string TitleLine3 { get; set; }
        public string TitleLine4 { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string BalanceAfterOperation { get; set; }
    }

    public class AliorBankCSVTransactionClassMap
        : ClassMap<AliorBankCSVTransaction>
    {
        public AliorBankCSVTransactionClassMap()
        {
            //AutoMap();
            Map(x => x.WhenBooked).Index(0);//.TypeConverter<DateTimeConverter>();//Name("Data księgowania");
            Map(x => x.When).Index(1);//.TypeConverter<DateTimeConverter>();//.Name("Data transakcji");

            Map(x => x.Sender).Index(2);//.Name("Nadawca");
            Map(x => x.Recipent).Index(3);//.Name("Odbiorca");
            Map(x => x.TitleLine1).Index(4);//.Name("Tytuł płatności (linia 1)");
            Map(x => x.TitleLine2).Index(5);//.Name("Tytuł płatności (linia 2)");
            Map(x => x.TitleLine3).Index(6);//.Name("Tytuł płatności (linia 3)");
            Map(x => x.TitleLine4).Index(7);//.Name("Tytuł płatności (linia 4)");

            Map(x => x.Description).Index(8);//.Name("Opis transakcji");

            Map(x => x.Amount).Index(9);//.Name("Kwota");
            Map(x => x.Currency).Index(10);//.Name("Waluta");
            Map(x => x.BalanceAfterOperation).Index(11);//.Name("Saldo po operacji");
        }
    }

    //public class AliorBankDateTimeConverter
    //    : ITypeConverter
    //{
    //    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


}
