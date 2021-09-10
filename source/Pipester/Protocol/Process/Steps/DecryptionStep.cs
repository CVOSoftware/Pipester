﻿using System.Security.Cryptography;
using System.IO;

using Pipester.Protocol.Process.Steps.Base;
using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class DecryptionStep : BaseEncryptionStep
    {
        public DecryptionStep(byte[] sourceMessage, string encryptionKey)
            : base(sourceMessage, encryptionKey)
        {

        }

        public override void Execute()
        {
            using var aes = GetAes();

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var inputStream = new MemoryStream(_sourceMessage);
            using var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read);
            using var outputStream = new MemoryStream();

            cryptoStream.CopyTo(outputStream);

            _processedMessage = outputStream.ToArray();
        }

        public override IStep Next()
        {
            var step = new FirstDeserializationStep(_processedMessage);

            return step;
        }
    }
}