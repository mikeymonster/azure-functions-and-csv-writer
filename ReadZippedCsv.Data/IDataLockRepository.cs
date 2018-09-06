using System.Threading.Tasks;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Data
{
    public interface IDataLockRepository
    {
        Task DeleteDataLocksAsync();

        Task DeleteValidAimsAsync();

        Task DeleteValidLearnersAsync();

        Task InsertDataLockAsync(DataLock dataLock);

        Task InsertValidAimAsync(Aim aim);

        Task InsertValidLearnerAsync(Learner learner);
    }
}
