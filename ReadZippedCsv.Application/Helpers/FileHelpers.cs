using System;
using System.IO;

namespace ReadZippedCsv.Application.Helpers
{
    public class FileHelpers
    {
        public static string GetSqlFilePath(string directory, string prefix)
        {
           //if (Configuration != null)
           // {
           //     directory = Configuration["scriptOutputDirectory"];
           // }
            //directory = directory ?? $@"C:\temp\";

            return Path.Combine(directory,
                $@"{prefix}_{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.sql");
        }
    }
}
