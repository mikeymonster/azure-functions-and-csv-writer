using System;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Functions.StructureMapped.IoC;

namespace ReadZippedCsv.Functions.StructureMapped
{
    public static class UpdateDataLocksFunction
    {
        [FunctionName("UpdateDataLocks")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Inject]IZipFileProcessor processor,
            [Inject]ILogger log)
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
    }
}
