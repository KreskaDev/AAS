using System.Reflection;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace AAS.Persistance
{
    public class MssqlDataBasePersister
        : DataBasePersister
    {
        public MssqlDataBasePersister(string connectionString, params Assembly[] assemblies)
            : base(connectionString, assemblies)
        { }

        private Configuration _configuration;

        public override Configuration Configuration
            => _configuration ?? (
                _configuration = MappingTemplate.Generate(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(ConnectionString)
                        .ShowSql(),
                    Assemblies
                    )
                );
    }
}