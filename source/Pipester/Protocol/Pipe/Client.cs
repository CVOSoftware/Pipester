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

        private readonly CancellationTokenSource _tokenSource;

        private readonly Action<string> _handleResponseAction;

        private readonly NamedPipeClientStream _pipeClientStream;

        public Client(string pipeName, Action<string> handleResponseAction)
        {
            _handleResponseAction = handleResponseAction;
            _tokenSource = new CancellationTokenSource();
            _pipeClientStream = new NamedPipeClientStream(pipeName);
        }

        public void Connect()
        {
            _pipeClientStream.Connect();

            _reader = new StreamReader(_pipeClientStream);

            Task.Run(WaitResponse, _tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _pipeClientStream.Dispose();
            _reader.Dispose();
        }

        private void WaitResponse()
        {
            var token = _tokenSource.Token;

            while (token.IsCancellationRequested == false)
            {
                try
                {
                    var message = _reader.ReadLine();

                    if (token.IsCancellationRequested == false && message != null)
                    {
                        _handleResponseAction(message);
                    }
                }
                finally { }
            }
        }
    }
}