using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAS.Common;
using AAS.Core;
using AAS.Persistance;

namespace AAS.Console
{
    public static class Program
    {
        public static AssemblyLoader AssemblyLoader { get; } 
            = new AssemblyLoader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        public static void Main(string[] args)
        {
            Bootstrap();
        }

        public static void Bootstrap()
        {
            AssemblyLoader.LoadAssemblies();

            var databasePersister = new MssqlDataBasePersister(
                @"Server=PIRANHA-KRESKA\SQLEXPRESS;Database=AAS;Integrated Security = true",
                AssemblyLoader.GetAppDomainAssemblyByName("AAS")
            );

            using (Stream stream = File.OpenRead(@"E:\export20171231152650.csv"))//.Open(@"E:\export20171231152650.csv", FileMode.Open, FileAccess.Read))
            using (var sessionProvider = new NhSessionProvider(databasePersister))
            {
                var operation = new ImporterTransactionsCommandHandler(stream, sessionProvider);

                databasePersister.DropDatabase();
                databasePersister.RecreateReadModel();

                operation.Handle();
            }
        }
    }
}
