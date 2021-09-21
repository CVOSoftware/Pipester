using System;
using System.Collections.Generic;

using Pipester.Storage.Model;
using Pipester.Storage.Repository;

namespace Pipester.Storage
{
    internal sealed class SubscriptionManager : IHandlerRepository, ISubscriptionRepository
    {
        private readonly Dictionary<string, Subscription> _subscribers;

        public SubscriptionManager()
        {
            _subscribers = new Dictionary<string, Subscription>();
        }

        #region Implementation ISubscriptionRepository

        public void Subscribe(Type subscriptionType, Action<object> handler)
        {
            var subscriptionTypeName = subscriptionType.FullName;

            if (_subscribers.TryGetValue(subscriptionTypeName, out var subscription))
            {
                subscription.Add(handler);

                return;
            }

            var newSubscription = new Subscription(subscriptionType, handler);

            _subscribers.TryAdd(subscriptionTypeName, newSubscription);
        }

        public void Unsubscribe(Type subscriptionType, Action<object> action)
        {
            var subscriptionTypeName = subscriptionType.FullName;

            if (_subscribers.TryGetValue(subscriptionTypeName, out var subscription))
            {
                subscription.Remove(action);
            }
        }

        #endregion

        #region Implementation IHandlerRepository

        public IReadOnlySet<Action<object>> GetHandlersBySubscriptionTypeName(string subscriptionTypeName)
        {
            if (_subscribers.TryGetValue(subscriptionTypeName, out var subscription))
            {
                return subscription.GetHandlers();
            }

            return default;
        }

        #endregion
    }
}