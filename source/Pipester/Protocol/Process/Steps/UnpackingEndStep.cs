using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class UnpackingEndStep : IEndStep<BusinessMessage>
    {
        private readonly BusinessMessage _unpackedMessage;

        public UnpackingEndStep(BusinessMessage unpackedMessage)
        {
            _unpackedMessage = unpackedMessage;
        }

        public BusinessMessage End() => _unpackedMessage;
    }
}