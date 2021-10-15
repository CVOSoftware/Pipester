using System.Collections.Generic;

using Pipester.Storage.Model;

namespace Pipester.Storage.Repository
{
    internal interface IHandlerRepository
    {
        IReadOnlySet<TrackedItem> GetTrackedItemsBySubscriptionTypeName(string subscriptionTypeName);
    }
}