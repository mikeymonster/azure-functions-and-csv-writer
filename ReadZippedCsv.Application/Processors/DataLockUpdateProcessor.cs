using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class DataLockUpdateProcessor : ProcessorBase<Aim>, IProcessor
    {
        private readonly IDataLockRepository _repository;

        public DataLockUpdateProcessor(IDataLockRepository repository, IConfigurationRoot configuration)
            : base(configuration)
        {
            _repository = repository;
        }

        public void Process(Stream stream)
        {
            _repository.DeleteDataLocksAsync();

            foreach (var item in GetData<DataLock>(stream))
            {
                //Debug.Print($"DataLock: Collection = {item.Collection}, UkPrn = {item.UkPrn}, LearnRefNumber = {item.LearnRefNumber}, ULN = {item.ULN}, AimSeqNumber = {item.AimSeqNumber}, RuleId = {item.RuleId}, CollectionPeriodName = {item.CollectionPeriodName}, CollectionPeriodMonth = {item.CollectionPeriodMonth}, CollectionPeriodYear = {item.CollectionPeriodYear }, LastSubmission = {item.LastSubmission:dd/MM/yyyy HH:mm:ss}, TNP = {item.TNP} ");
                _repository.InsertDataLockAsync(item);
            }
        }
    }
}
