using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;

namespace Pipester.Protocol.Process.Steps
{
    internal class UnpackingRawMessageStep : IExecutableStep
    {
        private readonly byte[] _message;

        private readonly WorkflowSetting _setting;

        private IExecutableStep _nextStep;

        public UnpackingRawMessageStep(byte[] message, WorkflowSetting setting)
        {
            _message = message;
            _setting = setting;
        }

        public void Execute()
        {
            if (string.IsNullOrEmpty(_setting.EncryptionKey) == false)
            {
                _nextStep = new DecryptionStep(_message, _setting.EncryptionKey);
            }

            _nextStep = _setting.IsCompress
                ? new DecompressionStep(_message)
                : new FirstDeserializationStep(_message);
        }

        public IStep Next() => _nextStep;
    }
}