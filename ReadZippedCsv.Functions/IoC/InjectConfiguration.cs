using System.IO;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadZippedCsv.Application;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Application.Processors;
using ReadZippedCsv.Data;

namespace ReadZippedCsv.Functions.IoC
{
    public class InjectConfiguration : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider(true);

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(new InjectBindingProvider(serviceProvider));

            //See https://github.com/Azure/azure-functions-core-tools/issues/684
            //Or better: https://github.com/Azure/azure-webjobs-sdk/issues/1865#issuecomment-417958408
            // 
            //var registry = context.Config.GetService<IExtensionRegistry>();
            //var filter = new ScopeCleanupFilter();
            //regiserExtensitry.Registon(typeof(IFunctionInvocationFilter), filter);
            //registry.RegisterExtension(typeof(IFunctionExceptionFilter), filter);


            //https://blog.jongallant.com/2018/01/azure-function-config/
        }

        private void RegisterServices(IServiceCollection services)
        {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory());
            //    config.AddJsonFile("local.settings.json");
            //    config.Build();

            //https://blog.jongallant.com/2018/01/azure-function-config/
            var config = new ConfigurationBuilder()
                //.SetBasePath(context.FunctionAppDirectory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton(config);

            //For EF:
            //https://stackoverflow.com/questions/50504805/inject-entityframeworkcore-dbcontext-in-azurefunction
            var connectionString = config.GetConnectionString("SqlConnectionString");
            //services.AddEntityFrameworkSqlServer()
            //    .AddDbContext<AnimalHubContext>((serviceProvider, options) >
            //                                    options.UseSqlServer(connectionString)
            //                                        .UseInternalServiceProvider(serviceProvider));


            services.AddTransient<IDataLockRepository, DataLockRepository>(
                p => new DataLockRepository(connectionString));
            services.AddTransient<IProcessorFactory, DatabaseUpdateProcessorFactory>();
            services.AddTransient<IZipFileProcessor, ZipFileDatabaseUpdateProcessor>();
        }
    }
}