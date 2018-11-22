using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReadZippedCsv.Functions;
using ReadZippedCsv.Functions.IoC;

[assembly: WebJobsStartup(typeof(Startup))]
namespace ReadZippedCsv.Functions
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("config.json")
            //    .Build();

            //https://github.com/Azure/azure-webjobs-sdk/issues/1865#issuecomment-417958408

            //Registering a filter
            //builder.Services.AddSingleton<IFunctionFilter, ScopeCleanupFilter>();

            //builder.Services.AddTransient<IZipFileProcessor, ZipFileProcessor>();
            builder.AddExtension<InjectConfiguration>();
            //Registering an extension
            //builder.AddExtension<InjectConfiguration>(); //AddExtension returns a builder that allows extending the configuration model
        }
    }
}