using System;

namespace ReadZippedCsv.Application.Processors
{
    public class ProcessorFactory : IProcessorFactory
    {
        public IProcessor GetProcessor(string hint)
        {
            switch (hint)
            {
                case var s when s.StartsWith("Data Lock Report"):
                    return new DataLockProcessor();
                case var s when s.StartsWith("Valid Aims Report"):
                    return new AimProcessor();
                case var s when s.StartsWith("Valid Learners Report"):
                    return new LearnerProcessor();
            }

            throw new InvalidOperationException("The hint passed to the factory did not match any of the known patterns.");
        }
    }
}
