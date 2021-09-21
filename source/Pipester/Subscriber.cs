using System;

namespace Pipester
{
    public sealed class Subscriber
    {
        private const string HandlerArgumentExceptionMessage = "Handler action is null";

        internal Subscriber()
        {
        }

        public void Subscribe<T>(Action<T> handler) where T : class
        {

        }

        public void Unsubscribe<T>(Action<object> handler) where T : class
        {
        }
    }
}