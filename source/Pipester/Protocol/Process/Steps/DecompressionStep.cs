using System.IO;
using System.IO.Compression;

using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Storage.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class DecompressionStep : IExecutableStep
    {
        private readonly byte[] _sourceMessage;

        private byte[] _processedMessage;

        private IHandlerRepository _repository;

        public DecompressionStep(byte[] message, IHandlerRepository repository)
        {
            _sourceMessage = message;
            _repository = repository;
        }

        public void Execute()
        {
            using var inputStream = new MemoryStream(_sourceMessage);
            using var defalteStream = new DeflateStream(inputStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();

            defalteStream.CopyTo(outputStream);

            _processedMessage = outputStream.ToArray();
        }

        public IStep Next()
        {
            var step = new FirstDeserializationStep(_processedMessage, _repository);

            return step;
        }
    }
}