using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReadZippedCsv.Application;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;
using StructureMap;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                var assemblyNames = (typeof(DefaultRegistry).Assembly.GetReferencedAssemblies()).ToList().Where(w => w.FullName.StartsWith("SFA.DAS.")).Select(a => a.FullName);

                foreach (var assemblyName in assemblyNames)
                {
                    scan.Assembly(assemblyName);
                }

                scan.RegisterConcreteTypesAgainstTheFirstInterface();
            });

            var config = GetConfiguration();

            //Not sure we need this but it lets us get the config elsewhere
            For<IConfigurationRoot>().Use(config);

            RegisterRepositories(config);
            RegisterServices();

            ConfigureLogging();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                //.SetBasePath(context.FunctionAppDirectory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        private void RegisterRepositories(IConfigurationRoot config)
        {
            var connectionString = config.GetConnectionString("SqlConnectionString");
            For<IDataLockRepository>().Use<DataLockRepository>().Ctor<string>().Is(connectionString);
        }

        private void RegisterServices()
        {
            For<IProcessorFactory>().Use<DatabaseUpdateProcessorFactory>();
            For<IZipFileProcessor>().Use<ZipFileDatabaseUpdateProcessor>();
        }

        private void ConfigureLogging()
        {
            For<ILogger>().Use(x => new DebugLogger<string>()).AlwaysUnique();
        }

    }
}
