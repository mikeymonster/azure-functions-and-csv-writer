using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ReadZippedCsv.Application;
using ReadZippedCsv.Application.Interfaces;

namespace ReadZippedCsv.Tests
{
    public class ZipFileProcessorTests
    {
        [Test]
        public void ProcessorConstructorSetsDefaultProcessorFactoryProperty()
        {
            var sut = new ZipFileToSqlScriptsProcessor(null);

            Assert.IsNotNull(sut.ProcessorFactory);
        }

        [Test]
        public void ProcessorConstructorSetsScriptOutputDirectoryProperty()
        {
            var mockConfiguration = Mock.Of<IConfigurationRoot>();
            var sut = new ZipFileToSqlScriptsProcessor(mockConfiguration);

            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }

        [Test]
        public void ProcessorConstructorWithProcessorFactorySetsProcessorFactoryProperty()
        {
            var mockProcessorFactory = Mock.Of<IProcessorFactory>();

            var sut = new ZipFileToSqlScriptsProcessor(null, mockProcessorFactory);

            Assert.AreEqual(mockProcessorFactory, sut.ProcessorFactory);
        }

        [Test]
        public void ProcessorConstructorWithProcessorFactorySetsScriptOutputDirectoryProperty()
        {
            var mockProcessorFactory = Mock.Of<IProcessorFactory>();
            var mockConfiguration = Mock.Of<IConfigurationRoot>();

            var sut = new ZipFileToSqlScriptsProcessor(mockConfiguration, mockProcessorFactory);

            Assert.AreEqual(mockConfiguration, sut.Configuration);
        }
    }
}
