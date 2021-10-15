using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Storage.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class ToBusinessMessageStep : IExecutableStep
    {
        private BusinessMessage _processedMessage;

        private readonly CommunicationMessage _sourceMessage;

        private readonly IHandlerRepository _repository;

        public ToBusinessMessageStep(CommunicationMessage sourceMessage, IHandlerRepository repository)
        {
            _sourceMessage = sourceMessage;
            _repository = repository;
        }

        public void Execute()
        {
            var type = _repository.GetTypeBySubscriptionName(_sourceMessage.Type);

            _processedMessage = new BusinessMessage
            {
                Id = Guid.Parse(_sourceMessage.Id),
                Type = type,
                Value = JsonSerializer.Deserialize(_sourceMessage.Value, type)
            };
        }

        public IStep Next()
        {
            var step = new UnpackingEndStep(_processedMessage);

            return step;
        }
    }
}