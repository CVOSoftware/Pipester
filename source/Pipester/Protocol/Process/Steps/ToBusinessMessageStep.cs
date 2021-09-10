using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pipester.Protocol.Message;
using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal sealed class ToBusinessMessageStep : IExecutableStep
    {
        private BusinessMessage _processedMessage;

        private readonly CommunicationMessage _sourceMessage;

        public ToBusinessMessageStep(CommunicationMessage sourceMessage)
        {
            _sourceMessage = sourceMessage;
        }

        public void Execute()
        {
            _processedMessage = new BusinessMessage
            {
                Id = Guid.Parse(_sourceMessage.Id),
                Type = default,
                Value = default
            };
        }

        public IStep Next()
        {
            var step = new UnpackingEndStep(_processedMessage);

            return step;
        }
    }
}