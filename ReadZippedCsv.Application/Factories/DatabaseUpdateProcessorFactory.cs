using System;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Application.Processors;
using ReadZippedCsv.Data;

namespace ReadZippedCsv.Application.Factories
{
    public class DatabaseUpdateProcessorFactory : IProcessorFactory
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IDataLockRepository _repository;

        public DatabaseUpdateProcessorFactory(IDataLockRepository repository, IConfigurationRoot configuration)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public IProcessor GetProcessor(string hint)
        {
            switch (hint)
            {
                case var s when s.StartsWith("Data Lock Report"):
                    return new DataLockUpdateProcessor(_repository, _configuration);
                case var s when s.StartsWith("Valid Aims Report"):
                    return new AimUpdateProcessor(_repository, _configuration);
                case var s when s.StartsWith("Valid Learners Report"):
                    return new LearnerUpdateProcessor(_repository, _configuration);
            }

            throw new InvalidOperationException("The hint passed to the factory did not match any of the known patterns.");
        }
    }
}
