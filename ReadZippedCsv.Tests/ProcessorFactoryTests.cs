using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Processors;
using ReadZippedCsv.Data;

namespace ReadZippedCsv.Tests
{
    public class ProcessorFactoryTests
    {
        #region SqlScriptProcessorFactory tests

        [Test]
        public void SqlScriptProcessorFactoryReturnsDataLockProcessor()
        {
            var sut = new SqlScriptProcessorFactory(Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Data Lock Report 20180801");

            Assert.IsInstanceOf<DataLockSqlScriptProcessor>(result);
        }

        [Test]
        public void SqlScriptProcessorFactoryReturnsLearnerProcessor()
        {
            var sut = new SqlScriptProcessorFactory(Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Valid Learners Report 20180801");

            Assert.IsInstanceOf<LearnerSqlScriptProcessor>(result);
        }

        [Test]
        public void SqlScriptProcessorFactoryReturnsAimProcessory()
        {
            var sut = new SqlScriptProcessorFactory(Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Valid Aims Report 20180801");
            
            Assert.IsInstanceOf<AimSqlScriptProcessor>(result);
        }

        #endregion

        #region DatabaseUpdateProcessorFactory tests

        [Test]
        public void DatabaseUpdateProcessorFactoryReturnsDataLockProcessor()
        {
            var sut = new DatabaseUpdateProcessorFactory(Mock.Of<IDataLockRepository>(), Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Data Lock Report 20180801");

            Assert.IsInstanceOf<DataLockUpdateProcessor>(result);
        }

        [Test]
        public void DatabaseUpdateProcessorFactoryReturnsLearnerProcessor()
        {
            var sut = new DatabaseUpdateProcessorFactory(Mock.Of<IDataLockRepository>(), Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Valid Learners Report 20180801");

            Assert.IsInstanceOf<LearnerUpdateProcessor>(result);
        }

        [Test]
        public void DatabaseUpdateProcessorFactoryReturnsAimProcessor()
        {
            var sut = new DatabaseUpdateProcessorFactory(Mock.Of<IDataLockRepository>(), Mock.Of<IConfigurationRoot>());

            var result = sut.GetProcessor("Valid Aims Report 20180801");

            Assert.IsInstanceOf<AimUpdateProcessor>(result);
        }

        #endregion

    }
}
