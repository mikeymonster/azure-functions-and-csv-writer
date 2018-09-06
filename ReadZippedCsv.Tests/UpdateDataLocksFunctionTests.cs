using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Functions;

namespace ReadZippedCsv.Tests
{
    public class UpdateDataLocksFunctionTests
    {
        [Test]
        public async Task UpdateDataLocksFunctionRunBlobCallsProcessor()
        {
            var log = new Mock<ILogger<String>>();
            var mockProcessor = new Mock<IZipFileProcessor>();
            mockProcessor.Setup(m => m.Process(It.IsAny<Stream>()));

            string blobName = "test";
            long streamLength = 1234;
            var mockStream = new Mock<MemoryStream>();
            mockStream.SetupGet(m => m.Length).Returns(streamLength);

            await UpdateDataLocksFunction.RunBlob(mockStream.Object, blobName, mockProcessor.Object, log.Object);

            mockProcessor.Verify(x => x.Process(It.IsAny<Stream>()), Times.Once);
        }

        [Test]
        public async Task UpdateDataLocksFunctionRunHttpCallsProcessor()
        {
            var log = new Mock<ILogger<String>>();
            var mockProcessor = new Mock<IZipFileProcessor>();
            mockProcessor.Setup(m => m.Process(It.IsAny<Stream>()));

            var content = await TestHelper.GetSampleZipFileContent();

            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Method = HttpMethod.Post.ToString(),
                Body = new MemoryStream(content)
            };

            var result = await UpdateDataLocksFunction.RunHttp(request, mockProcessor.Object, log.Object);

            Assert.IsInstanceOf<OkResult>(result);
            mockProcessor.Verify(x => x.Process(It.IsAny<Stream>()), Times.Once);
        }
    }
}
