using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal class PackingEndStep : IEndStep<byte[]>
    {
        private readonly byte[] _message;

        public PackingEndStep(byte[] message)
        {
            _message = message;
        }

        public byte[] End() => _message;
    }
}