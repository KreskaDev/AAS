using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace AAS.Persistance
{
    public static class MappingTemplate
    {
        public static Configuration Generate(
            IPersistenceConfigurer databaseInfo,
            params Assembly[] assemblies)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var fluentConfig = Fluently.Configure()
                .Database(databaseInfo)
                .Mappings(m =>
                {
                    var autoMapping = AutoMap
                        .Assemblies(new ModelAutomappingConfiguration(), assemblies)
                        .Conventions
                        .AddAssembly(currentAssembly)
                        .UseOverridesFromAssembly(currentAssembly);

                    foreach (var assembly in assemblies)
                    {
                        autoMapping.UseOverridesFromAssembly(assembly);
                    }

                    m.AutoMappings.Add(autoMapping);
                });

            var nhibernateConfiguration = fluentConfig.BuildConfiguration();
            return nhibernateConfiguration;
        }
    }
}