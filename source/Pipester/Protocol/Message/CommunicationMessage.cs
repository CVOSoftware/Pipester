namespace Pipester.Protocol.Message
{
    internal sealed class CommunicationMessage
    {
        public string Id { get; init; }

        public string Type { get; init; }

        public string Value { get; init; }
    }
}