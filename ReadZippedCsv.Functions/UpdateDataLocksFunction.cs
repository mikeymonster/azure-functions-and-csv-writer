using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Application.Processors;
using ReadZippedCsv.Functions.IoC;

namespace ReadZippedCsv.Functions
{
    public static class UpdateDataLocksFunction
    {
        [FunctionName("UpdateDataLocksHttp")]
        public static async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            [Inject]IZipFileProcessor processor,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for UpdateDataLocksFunction.");

            try
            {
                //To test:
                //https://stackoverflow.com/questions/38164723/uploading-file-to-http-via-powershell
                ////TODO: Make this async
                processor.Process(req.Body);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError($"Processing failed with error '{ex.Message}'.");
                return new InternalServerErrorResult();
            }
        }

        [FunctionName("UpdateDataLocksFromBlob")]
        public static async Task RunBlob(
            [BlobTrigger("datalocks-uploads/{name}", Connection = "StorageConnection")]Stream blob,
            string name,
            [Inject]IZipFileProcessor processor,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function UpdateDataLocksFunction processed blob\n Name:{name} \n Size: {blob.Length} Bytes");

            try
            {
                processor.Process(blob);
            }
            catch (Exception ex)
            {
                log.LogError($"Processing failed with error '{ex.Message}'.");
            }
        }
    }
}
