using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;
using Pipester.Storage.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class FirstDeserializationStep : IExecutableStep
    {
        private CommunicationMessage _processedMessage;

        private readonly byte[] _sourceMessage;

        private readonly IHandlerRepository _repository;

        public FirstDeserializationStep(byte[] sourceMessage, IHandlerRepository repository)
        {
            _sourceMessage = sourceMessage;
            _repository = repository;
        }

        public void Execute()
        {
            var sourceMessageAsText = Encoding.UTF8.GetString(_sourceMessage);

            _processedMessage = JsonSerializer.Deserialize<CommunicationMessage>(sourceMessageAsText); 
        }

        public IStep Next()
        {
            var step = new ToBusinessMessageStep(_processedMessage, _repository);

            return step;
        }
    }
}