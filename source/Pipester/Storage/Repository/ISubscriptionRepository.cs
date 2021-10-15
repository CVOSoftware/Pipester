using System;

namespace Pipester.Storage.Repository
{
    internal interface ISubscriptionRepository
    {
        Subscription Subscribe(Type subscriptionType, Action<object> handler);
    }
}