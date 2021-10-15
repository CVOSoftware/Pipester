using System;
using System.Collections.Generic;

using Pipester.Storage.Model;
using Pipester.Storage.Interface;

namespace Pipester.Storage
{
    internal sealed class Repository : IHandlerRepository, ISubscriptionRepository
    {
        private readonly Dictionary<string, SubscriptionTracker> _subscribers;

        public Repository()
        {
            _subscribers = new Dictionary<string, SubscriptionTracker>();
        }

        #region Implementation ISubscriptionRepository

        public Subscription Subscribe(Type subscriptionType, Action<object> handler)
        {
            var subscriptionName = subscriptionType.FullName;

            if (_subscribers.TryGetValue(subscriptionName, out var tracker))
            {
                var existedSubscription = tracker.Add(handler);

                return existedSubscription;
            }

            var newTracker = new SubscriptionTracker(subscriptionType);
            var subscription = newTracker.Add(handler);

            _subscribers.TryAdd(subscriptionName, newTracker);

            return subscription;
        }

        #endregion

        #region Implementation IHandlerRepository

        public IReadOnlySet<TrackedItem> GetTrackedItemsBySubscriptionTypeName(string subscriptionName)
        {
            if (_subscribers.TryGetValue(subscriptionName, out var subscription))
            {
                return subscription.Handlers;
            }

            return default;
        }

        #endregion
    }
}