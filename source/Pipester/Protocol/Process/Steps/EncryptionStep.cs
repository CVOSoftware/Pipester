using System.Security.Cryptography;
using System.IO;

using Pipester.Protocol.Process.Steps.Base;
using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal class EncryptionStep : BaseEncryptionStep
    {
        public EncryptionStep(byte[] sourceMessage, string encryptionKey)
            : base(sourceMessage, encryptionKey)
        {

        }

        public override void Execute()
        {
            using var aes = GetAes();

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);

            streamWriter.Write(_sourceMessage);

            _processedMessage = memoryStream.ToArray();
        }

        public override IStep Next()
        {
            var step = new PackingEndStep(_processedMessage);

            return step;
        }
    }
}