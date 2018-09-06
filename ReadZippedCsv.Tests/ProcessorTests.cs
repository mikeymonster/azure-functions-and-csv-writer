using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ReadZippedCsv.Application.Processors;
using ReadZippedCsv.Data;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Tests
{
    public class ProcessorTests
    {
        [Test]
        public void DataLockSqlScriptProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var sut = new DataLockSqlScriptProcessor(mockConfiguration);

            Assert.AreEqual("Data_Lock", sut.Schema);
            Assert.AreEqual("DAS_DataLocks", sut.TableName);
            Assert.AreEqual("DAS_DataLocks", sut.SqlFileNamePrefix);
            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }

        [Test]
        public void AimSqlScriptProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var sut = new AimSqlScriptProcessor(mockConfiguration);

            Assert.AreEqual("Data_Lock", sut.Schema);
            Assert.AreEqual("DAS_ValidAims", sut.TableName);
            Assert.AreEqual("DAS_ValidAims", sut.SqlFileNamePrefix);
            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }

        [Test]
        public void LearnerSqlScriptProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var sut = new LearnerSqlScriptProcessor(mockConfiguration);

            Assert.AreEqual("Data_Lock", sut.Schema);
            Assert.AreEqual("DAS_ValidLearners", sut.TableName);
            Assert.AreEqual("DAS_ValidLearners", sut.SqlFileNamePrefix);
            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }
        
        [Test]
        public void DataLockUpdateProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = Mock.Of<IDataLockRepository>();
            var sut = new DataLockUpdateProcessor(mockRepository ,mockConfiguration);

            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }

        [Test]
        public void AimUpdateProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = Mock.Of<IDataLockRepository>();
            var sut = new AimUpdateProcessor(mockRepository, mockConfiguration);

            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }

        [Test]
        public void LearnerUpdateProcessorConstructorSetsRequiredProperties()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = Mock.Of<IDataLockRepository>();
            var sut = new LearnerUpdateProcessor(mockRepository, mockConfiguration);

            Assert.AreEqual(null, sut.Schema);
            Assert.AreEqual(null, sut.TableName);
            Assert.AreEqual(null, sut.SqlFileNamePrefix);
            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }
        
        [Test]
        public async Task AimUpdateProcessorProcessZipFileStreamCallsRepository()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = new Mock<IDataLockRepository>();

            mockRepository.Setup(r => r.DeleteDataLocksAsync());
            mockRepository.Setup(r => r.InsertValidAimAsync(It.IsAny<Aim>()));

            var content = await TestHelper.GetSampleZipFileContent();
            var stream = new MemoryStream(content);

            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var entry = zipArchive.Entries.First(e => e.Name.StartsWith("Valid Aims"));
                using (var entryStream = entry.Open())
                {
                    var sut = new AimUpdateProcessor(mockRepository.Object, mockConfiguration);
                    sut.Process(entryStream);
                }
            }

            mockRepository.Verify(x => x.DeleteValidAimsAsync(), Times.Once);
            mockRepository.Verify(x => x.InsertValidAimAsync(It.IsAny<Aim>()), Times.AtLeastOnce);
        }
        [Test]
        public async Task LearnerUpdateProcessorProcessZipFileStreamCallsRepository()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = new Mock<IDataLockRepository>();

            mockRepository.Setup(r => r.DeleteDataLocksAsync());
            mockRepository.Setup(r => r.InsertValidLearnerAsync(It.IsAny<Learner>()));

            var content = await TestHelper.GetSampleZipFileContent();
            var stream = new MemoryStream(content);

            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var entry = zipArchive.Entries.First(e => e.Name.StartsWith("Valid Learners"));
                using (var entryStream = entry.Open())
                {
                    var sut = new LearnerUpdateProcessor(mockRepository.Object, mockConfiguration);
                    sut.Process(entryStream);
                }
            }

            mockRepository.Verify(x => x.DeleteValidLearnersAsync(), Times.Once);
            mockRepository.Verify(x => x.InsertValidLearnerAsync(It.IsAny<Learner>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task DataLockUpdateProcessorProcessZipFileStreamCallsRepository()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var mockRepository = new Mock<IDataLockRepository>();

            mockRepository.Setup(r => r.DeleteDataLocksAsync());
            mockRepository.Setup(r => r.InsertDataLockAsync(It.IsAny<DataLock>()));

            var content = await TestHelper.GetSampleZipFileContent();
            var stream = new MemoryStream(content);

            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var entry = zipArchive.Entries.First(e => e.Name.StartsWith("Data Lock"));
                using (var entryStream = entry.Open())
                {
                    var sut = new DataLockUpdateProcessor(mockRepository.Object, mockConfiguration);
                    sut.Process(entryStream);
                }
            }

            mockRepository.Verify(x => x.DeleteDataLocksAsync(), Times.Once);
            mockRepository.Verify(x => x.InsertDataLockAsync(It.IsAny<DataLock>()), Times.AtLeastOnce);
        }
    }
}
