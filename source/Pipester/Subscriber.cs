using System;

using Pipester.Subscription.Abstraction;

namespace Pipester
{
    public sealed class Subscriber
    {
        private const string HandlerArgumentExceptionMessage = "Handler action is null";

        private readonly ISubscriber _subscriber;

        internal Subscriber(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public void Subscribe<T>(Action<T> handler) where T : class
        {
            if (handler == null)
            {
                throw new ArgumentException(HandlerArgumentExceptionMessage);
            }

            var subscription = typeof(T);

            _subscriber.Subscribe(subscription, o => handler((T)o));
        }

        public void Unsubscribe<T>(Action<object> handler) where T : class
        {
            if (handler == null)
            {
                throw new ArgumentException(HandlerArgumentExceptionMessage);
            }

            var subscription = typeof(T);

            _subscriber.Unsubscribe(subscription, handler);
        }
    }
}