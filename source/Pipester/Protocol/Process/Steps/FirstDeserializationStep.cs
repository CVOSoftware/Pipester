using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class FirstDeserializationStep : IExecutableStep
    {
        private CommunicationMessage _processedMessage;

        private readonly byte[] _sourceMessage;

        public FirstDeserializationStep(byte[] sourceMessage)
        {
            _sourceMessage = sourceMessage;
        }

        public void Execute()
        {
            var sourceMessageAsText = Encoding.UTF8.GetString(_sourceMessage);

            _processedMessage = JsonSerializer.Deserialize<CommunicationMessage>(sourceMessageAsText); 
        }

        public IStep Next()
        {
            var step = new ToBusinessMessageStep(_processedMessage);

            return step;
        }
    }
}