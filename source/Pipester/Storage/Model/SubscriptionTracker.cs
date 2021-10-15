using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipester.Storage.Model
{
    internal sealed class SubscriptionTracker
    {
        private readonly HashSet<TrackedItem> _trackedItems;

        public SubscriptionTracker(Type subscriptionType)
        {
            SubscriptionType = subscriptionType;
            _trackedItems = new HashSet<TrackedItem>();
        }

        public Type SubscriptionType { get; }

        public IReadOnlySet<TrackedItem> Handlers => _trackedItems;

        public Subscription Add(Action<object> handler)
        {
            var trackedItem = _trackedItems.FirstOrDefault(item => item.Handler.Equals(handler));

            if (trackedItem != null)
            {
                return new Subscription(trackedItem.Id, this);
            }

            trackedItem = new TrackedItem(handler);

            _trackedItems.Add(trackedItem);

            return new Subscription(trackedItem.Id, this);
        }

        public void Remove(Guid id)
        {
            var trackedItem = _trackedItems.FirstOrDefault(item => item.Id == id);

            if (trackedItem == null)
            {
                return;
            }

            _trackedItems.Remove(trackedItem);
        }

        public bool IsExist(Guid id)
        {
            var isExist = _trackedItems.Any(item => item.Id == id);

            return isExist;
        }
    }
}