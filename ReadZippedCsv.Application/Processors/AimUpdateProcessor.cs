using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class AimUpdateProcessor : ProcessorBase<Aim>, IProcessor
    {
        private readonly IDataLockRepository _repository;

        public AimUpdateProcessor(IDataLockRepository repository, IConfigurationRoot configuration)
            : base(configuration)
        {
            _repository = repository;
        }

        public void Process(Stream stream)
        {
            _repository.DeleteValidAimsAsync();

            foreach (var item in GetData<Aim>(stream))
            {
                //Debug.Print($"Aim: UkPrn = {item.UkPrn}, NumberOfLearnersWithACT1 = NumberOfLearnersWith{item.NumberOfLearnersWithACT1}, NumberOfLearnersWithACT2 = {item.NumberOfLearnersWithACT2}");
                _repository.InsertValidAimAsync(item);
            }
        }
    }
}
