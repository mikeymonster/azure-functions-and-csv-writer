using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class LearnerUpdateProcessor : ProcessorBase<Aim>, IProcessor
    {
        private readonly IDataLockRepository _repository;

        public LearnerUpdateProcessor(IDataLockRepository repository, IConfigurationRoot configuration)
            : base(configuration)
        {
            _repository = repository;
        }

        public void Process(Stream stream)
        {
            _repository.DeleteValidLearnersAsync();

            foreach (var item in GetData<Learner>(stream))
            {
                //Debug.Print($"Learner: UkPrn = {item.UkPrn}, NumberOfLearners = {item.NumberOfLearners}");
                _repository.InsertValidLearnerAsync(item);
            }
        }
    }
}
