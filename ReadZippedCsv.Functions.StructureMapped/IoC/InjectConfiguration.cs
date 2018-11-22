using System.IO;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadZippedCsv.Application;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
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
        }
    }
}