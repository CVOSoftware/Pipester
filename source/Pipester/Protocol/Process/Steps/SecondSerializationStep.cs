using System.Text;
using System.Text.Json;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class SecondSerializationStep : IExecutableStep
    {
        private readonly CommunicationMessage _communicationMessage;

        private readonly WorkflowSetting _setting;

        private byte[] _serializedMessageAsBytes;

        public SecondSerializationStep(CommunicationMessage message, WorkflowSetting setting)
        {
            _communicationMessage = message;
        }

        public void Execute()
        {
            var serializedMessage = JsonSerializer.Serialize(_communicationMessage);

            _serializedMessageAsBytes = Encoding.UTF8.GetBytes(serializedMessage);
        }

        public IStep Next()
        {
            if (string.IsNullOrEmpty(_setting.EncryptionKey) == false)
            {
                var encryptionStep = new EncryptionStep(_serializedMessageAsBytes, _setting.EncryptionKey);

                return encryptionStep; 
            }

            IStep compressOrPackingEndStep = _setting.IsCompress
                ? new CompressionStep(_serializedMessageAsBytes)
                : new PackingEndStep(_serializedMessageAsBytes);

            return compressOrPackingEndStep;
        }
    }
}