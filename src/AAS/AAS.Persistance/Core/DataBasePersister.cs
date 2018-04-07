using System;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace AAS.Persistance
{
    public abstract class DataBasePersister
    {
        protected readonly string ConnectionString;
        protected readonly Assembly[] Assemblies;

        protected DataBasePersister(string connectionString, params Assembly[] assemblies)
        {
            ConnectionString = connectionString;
            Assemblies = assemblies;
#if DEBUG
            Console.SetOut(new DebugWriter());
#endif
        }

        public abstract Configuration Configuration { get; }

        public void RecreateReadModel()
        {
            var schemaExport = new SchemaExport(Configuration);
            schemaExport.Execute(false, true, false);
        }

        public void DropDatabase()
        {
            var schemaExport = new SchemaExport(Configuration);
            schemaExport.Drop(false, true);
        }
    }
}