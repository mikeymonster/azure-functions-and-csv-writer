using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using ReadZippedCsv.Functions.StructureMapped;
using ReadZippedCsv.Functions.StructureMapped.IoC;

[assembly: WebJobsStartup(typeof(Startup))]
namespace ReadZippedCsv.Functions.StructureMapped
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<InjectConfiguration>();
        }
    }
}