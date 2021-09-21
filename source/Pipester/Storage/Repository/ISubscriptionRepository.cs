using System;

namespace Pipester.Storage.Repository
{
    internal interface ISubscriptionRepository
    {
        void Subscribe(Type subscriptionType, Action<object> handler);

        void Unsubscribe(Type subscriptionType, Action<object> handler);
    }
}