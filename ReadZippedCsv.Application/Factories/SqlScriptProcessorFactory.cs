using System;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Application.Processors;

namespace ReadZippedCsv.Application.Factories
{
    public class SqlScriptProcessorFactory : IProcessorFactory
    {
        private readonly IConfigurationRoot _configuration;

        public SqlScriptProcessorFactory(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IProcessor GetProcessor(string hint)
        {
            switch (hint)
            {
                case var s when s.StartsWith("Data Lock Report"):
                    return new DataLockSqlScriptProcessor(_configuration);
                case var s when s.StartsWith("Valid Aims Report"):
                    return new AimSqlScriptProcessor(_configuration);
                case var s when s.StartsWith("Valid Learners Report"):
                    return new LearnerSqlScriptProcessor(_configuration);
            }

            throw new InvalidOperationException("The hint passed to the factory did not match any of the known patterns.");
        }
    }
}
