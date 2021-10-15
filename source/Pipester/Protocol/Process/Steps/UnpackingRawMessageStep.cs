using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;
using Pipester.Storage.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal class UnpackingRawMessageStep : IExecutableStep
    {
        private readonly byte[] _message;

        private readonly WorkflowSetting _setting;

        private IExecutableStep _nextStep;

        private readonly IHandlerRepository _repository;

        public UnpackingRawMessageStep(byte[] message, WorkflowSetting setting, IHandlerRepository repository)
        {
            _message = message;
            _setting = setting;
            _repository = repository;
        }

        public void Execute()
        {
            if (string.IsNullOrEmpty(_setting.EncryptionKey) == false)
            {
                _nextStep = new DecryptionStep(_message, _setting.EncryptionKey, _repository);
            }

            _nextStep = _setting.IsCompress
                ? new DecompressionStep(_message, _repository)
                : new FirstDeserializationStep(_message, _repository);
        }

        public IStep Next() => _nextStep;
    }
}