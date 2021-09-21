using System;
using System.Collections.Generic;

namespace Pipester.Storage.Model
{
    internal class Subscription
    {
        private readonly HashSet<Action<object>> _handlers;

        public Subscription(Type subscriptionType, Action<object> handler)
        {
            SubscriptionType = subscriptionType;
            _handlers = new HashSet<Action<object>>
            {
                handler
            };
        }

        public Type SubscriptionType { get; }

        public void Add(Action<object> handler)
        {
            _handlers.Add(handler);
        }

        public void Remove(Action<object> handler)
        {
            _handlers.Remove(handler);
        }

        public IReadOnlySet<Action<object>> GetHandlers()
        {
            return _handlers;
        }
    }
}