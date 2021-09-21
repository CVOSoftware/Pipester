using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace Pipester.Protocol.Pipe
{
    internal sealed class Client : IDisposable
    {
        private StreamReader _reader;

        private readonly CancellationToken _token;

        private readonly Action<string> _handleAction;

        private readonly NamedPipeClientStream _pipeClientStream;

        public Client(CancellationToken token)
        {
        }

        public void Connect()
        {
            _pipeClientStream.Connect();

            _reader = new StreamReader(_pipeClientStream);

            Task.Run(WaitResponse, _token);
        }

        public void Dispose()
        {
            _pipeClientStream?.Dispose();
            _reader?.Dispose();
        }

        private void WaitResponse()
        {
            while (_token.IsCancellationRequested == false)
            {
                var message = _reader.ReadLine();

                if (_token.IsCancellationRequested == false && message != null)
                {
                    _handleAction(message);
                }
            }
        }
    }
}