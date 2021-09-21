using System;

using Pipester.Protocol;

namespace Pipester
{
    public sealed class Sender
    {
        private const string MessageException = "Message argument is null";

        internal Sender()
        {
        }

        public void Send(object message)
        {
            if (message == null)
            {
                throw new ArgumentException(MessageException);
            }
        }
    }
}