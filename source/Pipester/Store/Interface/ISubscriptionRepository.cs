using System;
using System.Collections.Generic;

namespace Pipester.Store.Interface
{
    internal interface ISubscriptionRepository
    {
        IReadOnlySet<Action<object>> GetListeners(Type subscription);
    }
}