using System.Text;

using Pipester.Protocol.Process.Steps.Interface;

namespace Pipester.Protocol.Process.Steps
{
    internal class PackingEndStep : IEndStep<string>
    {
        private readonly string _message;

        public PackingEndStep(byte[] message)
        {
            _message = Encoding.UTF8.GetString(message);
        }

        public string End() => _message;
    }
}