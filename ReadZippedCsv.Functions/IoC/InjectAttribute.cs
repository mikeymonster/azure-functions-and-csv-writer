using System;
using Microsoft.Azure.WebJobs.Description;

namespace ReadZippedCsv.Functions.IoC
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}
