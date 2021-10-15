using System;

namespace Pipester.Storage.Interface
{
    internal interface ISubscriptionRepository
    {
        Subscription Subscribe(Type subscriptionType, Action<object> handler);
    }
}