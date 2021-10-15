using System;

namespace Pipester
{
    public sealed class Sender
    {
        private readonly Action<object> _handleRequestAction;

        internal Sender(Action<object> handleRequestAction)
        {
            _handleRequestAction = handleRequestAction;
        }

        public void Send(object message)
        {
            _handleRequestAction(message);
        }
    }
}