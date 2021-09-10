using System;

namespace Pipester.Protocol.Message
{
    internal sealed class BusinessMessage
    {
        public Guid Id { get; init; }

        public Type Type { get; init; }

        public object Value { get; init; }
    }
}