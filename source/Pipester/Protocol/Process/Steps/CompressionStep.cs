using System.IO;
using System.IO.Compression;

using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class CompressionStep : IExecutableStep
    {
        private readonly byte[] _message;

        private byte[] _compressedMessage;

        public CompressionStep(byte[] message)
        {
            _message = message;
        }

        public void Execute()
        {
            using var memoryStream = new MemoryStream();
            using (var deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
            {
                deflateStream.Write(_message, 0, _message.Length);
            }

            _compressedMessage = memoryStream.ToArray();
        }

        public IStep Next()
        {
            var step = new PackingEndStep(_compressedMessage);

            return step;
        }
    }
}