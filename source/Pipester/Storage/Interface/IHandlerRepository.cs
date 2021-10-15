using System.Collections.Generic;

using Pipester.Storage.Model;

namespace Pipester.Storage.Interface
{
    internal interface IHandlerRepository
    {
        IReadOnlySet<TrackedItem> GetTrackedItemsBySubscriptionTypeName(string subscriptionTypeName);
    }
}