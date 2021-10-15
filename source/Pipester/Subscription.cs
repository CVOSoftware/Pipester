using System;

using Pipester.Storage.Model;

namespace Pipester
{
    public struct Subscription
    {
        private readonly Guid _id;

        private readonly SubscriptionTracker _tracker;

        internal Subscription(Guid id, SubscriptionTracker tracker)
        {
            _id = id;
            _tracker = tracker;
        }

        public bool IsSubscribe => _tracker.IsExist(_id);

        public void Unsubscribe()
        {
            _tracker.Remove(_id);
        }
    }
}