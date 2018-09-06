using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ReadZippedCsv.Tests
{
    public class TestHelper
    {
        public static async Task<byte[]> GetSampleZipFileContent()
        {
            return await ReadResourceBytes(@"ReadZippedCsv.Tests.TestData.DataLocksTestData.zip");
        }

        private static string ReadResourceString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            string readData;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    readData = reader.ReadToEnd();
                }
            }

            return readData;
        }

        private static async Task<byte[]> ReadResourceBytes(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                byte[] ba = new byte[stream.Length];
                await stream.ReadAsync(ba, 0, ba.Length);
                return ba;

                //var content = Encoding.UTF8.GetBytes("test");

                var bytes = new byte[stream.Length + 10];
                var numBytesToRead = (int)stream.Length;
                var numBytesRead = 0;
                do
                {
                    // Read may return anything from 0 to 10.
                    var n = await stream.ReadAsync(bytes, numBytesRead, 10);
                    numBytesRead += n;
                    numBytesToRead -= n;
                } while (numBytesToRead > 0);

                return bytes;
            }
        }

        private static async Task<string> ReadResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            string readData;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    readData = reader.ReadToEnd();
                }
            }

            return readData;
        }
    }
}
