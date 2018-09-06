using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Data
{
    public class DataLockRepository : BaseRepository, IDataLockRepository
    {
        public DataLockRepository(string connectionString) 
            : base(connectionString)
        { }

        public async Task DeleteValidAimsAsync()
        {
            await WithConnection(async c => 
                await c.ExecuteAsync(
                "DELETE FROM [Data_Lock].[DAS_ValidAims]",
                commandType: CommandType.Text));
        }

        public async Task DeleteValidLearnersAsync()
        {
            await WithConnection(async c => 
                await c.ExecuteAsync(
                    "DELETE FROM [Data_Lock].[DAS_ValidLearners]",
                    commandType: CommandType.Text));
        }

        public async Task DeleteDataLocksAsync()
        {
            await WithConnection(async c =>
                await c.ExecuteAsync(
                    "DELETE FROM [Data_Lock].[DAS_DataLocks]",
                    commandType: CommandType.Text));
        }

        public async Task InsertValidAimAsync(Aim aim)
        {
            await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ukprn", aim.UkPrn, DbType.Int64);
                parameters.Add("@numberOfLearnersWithACT1", aim.NumberOfLearnersWithACT1, DbType.Int32);
                parameters.Add("@numberOfLearnersWithACT2", aim.NumberOfLearnersWithACT2, DbType.Int32);

                return await c.ExecuteAsync(
                        "INSERT INTO [Data_Lock].[DAS_ValidAims] " +
                        "([Ukprn], [NumberOfLearnersWithACT1], [NumberOfLearnersWithACT2]) " +
                        "VALUES(@ukprn, @numberOfLearnersWithACT1, @numberOfLearnersWithACT2)",
                        param: parameters,
                        commandType: CommandType.Text);
            });
        }

        public async Task InsertDataLockAsync(DataLock dataLock)
        {
            await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@collection", dataLock.Collection, DbType.String);
                parameters.Add("@ukprn", dataLock.UkPrn, DbType.Int64);
                parameters.Add("@learnRefNumber", dataLock.LearnRefNumber, DbType.String);
                parameters.Add("@uln", dataLock.ULN, DbType.Int64);
                parameters.Add("@aimSeqNumber", dataLock.AimSeqNumber, DbType.Int64);
                parameters.Add("@ruleId", dataLock.RuleId, DbType.String);
                parameters.Add("@collectionPeriodName", dataLock.CollectionPeriodName, DbType.String);
                parameters.Add("@collectionPeriodMonth", dataLock.CollectionPeriodMonth, DbType.Int32);
                parameters.Add("@collectionPeriodYear", dataLock.CollectionPeriodYear, DbType.Int32);
                parameters.Add("@lastSubmission", dataLock.LastSubmission, DbType.DateTime);
                parameters.Add("@tnp", dataLock.TNP, DbType.Int64);

                try
                {
                    //return await c.ExecuteAsync(
                    c.Execute(
                        "INSERT INTO [Data_Lock].[DAS_DataLocks] " +
                        "([Collection],[Ukprn], [LearnRefNumber], [ULN], [AimSeqNumber], [RuleId], " +
                        "[CollectionPeriodName], [CollectionPeriodMonth], [CollectionPeriodYear], " +
                        "[LastSubmission], [TNP]) " +
                        "VALUES(@collection, @ukprn, @learnRefNumber, @uln, @aimSeqNumber, @ruleId, " +
                        "@collectionPeriodName, @collectionPeriodMonth, @collectionPeriodYear, " +
                        "@lastSubmission, @tnp)",
                        param: parameters,
                        commandType: CommandType.Text);
                }
                catch (Exception e)
                {
                    throw;
                }

                return new Task<int>(() => -1);
            });

            /*
            INSERT INTO [Data_Lock].[DAS_DataLocks]
           ([Collection]
           ,[Ukprn]
           ,[LearnRefNumber]
           ,[ULN]
           ,[AimSeqNumber]
           ,[RuleId]
           ,[CollectionPeriodName]
           ,[CollectionPeriodMonth]
           ,[CollectionPeriodYear]
           ,[LastSubmission]
           ,[TNP])
     VALUES
           (<Collection, nvarchar(10),>
           ,<Ukprn, bigint,>
           ,<LearnRefNumber, nvarchar(12),>
           ,<ULN, bigint,>
           ,<AimSeqNumber, bigint,>
           ,<RuleId, nvarchar(10),>
           ,<CollectionPeriodName, nvarchar(10),>
           ,<CollectionPeriodMonth, int,>
           ,<CollectionPeriodYear, int,>
           ,<LastSubmission, datetime,>
           ,<TNP, bigint,>)
              */
        }

        public async Task InsertValidLearnerAsync(Learner learner)
        {
            await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ukprn", learner.UkPrn, DbType.Int64);
                parameters.Add("@numberOfLearners", learner.NumberOfLearners, DbType.Int32);

                try
                {
                    //return await c.ExecuteAsync(
                    c.Execute(
                        "INSERT INTO [Data_Lock].[DAS_ValidLearners] " +
                        "([Ukprn], [NumberOfLearners]) " +
                        "VALUES(@ukprn, @numberOfLearners)",
                        param: parameters,
                        commandType: CommandType.Text);
                }
                catch (Exception e)
                {
                    throw;
                }

                return new Task<int>(() => -1);
            });
            
            /*
            INSERT INTO [Data_Lock].[DAS_ValidLearners]
           ([Ukprn] ,[NumberOfLearners])
     VALUES 
           (<Ukprn, bigint,>
           ,<NumberOfLearners, int,>)
              */

        }
    }
}
