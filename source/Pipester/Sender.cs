using System;

using Pipester.Protocol;

namespace Pipester
{
    public sealed class Sender
    {
        private const string MessageException = "Message argument is null";

        private readonly ConnectionManager _sender;

        internal Sender(ConnectionManager sender)
        {
            _sender = sender;
        }

        public void Send(object message)
        {
            if (message == null)
            {
                throw new ArgumentException(MessageException);
            }

            _sender.Send(message);
        }
    }
}