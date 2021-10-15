using System;

namespace Pipester.Storage.Model
{
    internal sealed class TrackedItem
    {
        public TrackedItem(Action<object> handler)
        {
            Id = Guid.NewGuid();
            Handler = handler;
        }

        public Guid Id { get; }

        public Action<object> Handler { get; }
    }
}