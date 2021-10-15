using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace Pipester.Protocol.Pipe
{
    internal sealed class Server : IDisposable
    {
        private object _sendLockObject;

        private StreamWriter _writer;

        private readonly CancellationTokenSource _tokenSource;

        private readonly NamedPipeServerStream _pipeServerStream;

        public Server(string pipeName)
        {
            _sendLockObject = new object();
            _tokenSource = new CancellationTokenSource();
            _pipeServerStream = new NamedPipeServerStream(pipeName);
        }

        public void WaitConnection()
        {
            Task.Run(() => 
            {
                _pipeServerStream.WaitForConnection();
                _writer = new StreamWriter(_pipeServerStream);
            }, _tokenSource.Token);
        }

        public void Send(string message)
        {
            lock(_sendLockObject)
            {
                try
                {
                    _writer.WriteLine(message);
                    _writer.Flush();
                }
                finally { }
            }
            
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _pipeServerStream.Dispose();
            _writer.Dispose();
        }
    }
}