using ReadZippedCsv.Application;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Factories;

namespace ReadZippedCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            //public static IServiceProvider provider;

            //https://blog.bitscry.com/2017/05/30/appsettings-json-in-net-core-console-app/
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            if (!args.Any())
            {
                Console.WriteLine("Please pass a valid zip file path as the first argument to this program.");
                return;
            }

            var path = Path.GetFullPath(args[0]);

            var processor = new ZipFileToSqlScriptsProcessor(configuration);//, new SqlScriptProcessorFactory(configuration));
            processor.Process(path);

            if (args.All(a => a != "/silent"))
            {
                Console.WriteLine("Done. Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
