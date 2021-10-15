using System;
using System.Collections.Generic;

using Pipester.Storage.Model;

namespace Pipester.Storage.Interface
{
    internal interface IHandlerRepository
    {
        Type GetTypeBySubscriptionName(string subscriptionName);

        IReadOnlySet<TrackedItem> GetTrackedItemsBySubscriptionName(string subscriptionTypeName);
    }
}