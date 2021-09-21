using System;
using System.Collections.Generic;

namespace Pipester.Storage.Repository
{
    internal interface IHandlerRepository
    {
        IReadOnlySet<Action<object>> GetHandlersBySubscriptionTypeName(string subscriptionTypeName);
    }
}