using System.Text.Json;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Protocol.Setting;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class ToCommunicationMessageStep : IExecutableStep
    {
        private readonly BusinessMessage _businessMessage;

        private readonly WorkflowSetting _setting;

        private CommunicationMessage _communicationMessage;

        public ToCommunicationMessageStep(BusinessMessage message, WorkflowSetting setting)
        {
            _businessMessage = message;
            _setting = setting;
        }

        public void Execute()
        {
            _communicationMessage = new CommunicationMessage
            {
                Id = _businessMessage.Id.ToString(),
                Type = _businessMessage.Type.FullName,
                Value = JsonSerializer.Serialize(_businessMessage.Value)
            };
        }

        public IStep Next()
        {
            var step = new SecondSerializationStep(_communicationMessage, _setting);

            return step;
        }
    }
}