using System.Text;
using System.Security.Cryptography;

using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps.Base
{
    internal abstract class BaseEncryptionStep : IExecutableStep
    {
        private const byte Size = 16;

        protected byte[] _processedMessage;

        protected readonly byte[] _sourceMessage;

        private readonly string _encryptionKey;

        protected BaseEncryptionStep(byte[] sourceMessage, string encryptionKey)
        {
            _sourceMessage = sourceMessage;
            _encryptionKey = encryptionKey;
        }

        protected Aes GetAes()
        {
            var aes = Aes.Create();

            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = new byte[Size];

            return aes;
        }

        public abstract void Execute();

        public abstract IStep Next();
    }
}