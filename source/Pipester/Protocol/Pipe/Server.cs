using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

using Pipester.Protocol.Connector;

namespace Pipester.Protocol.Pipe
{
    internal sealed class Server : IDisposable
    {
        private StreamWriter _writer;

        private readonly CancellationToken _token;

        private readonly NamedPipeServerStream _pipeServerStream;

        public Server(OutputConnector connector, CancellationToken token)
        {
            _pipeServerStream = new NamedPipeServerStream(connector.PipeName, PipeDirection.Out);
            _token = token;
        }

        public void WaitConnection()
        {
            Task.Run(() => 
            {
                _pipeServerStream.WaitForConnection();
                _writer = new StreamWriter(_pipeServerStream);
            }, _token);
        }

        public void Send(string message)
        {
            Task.Run(() => 
            {   
                _writer.WriteLine(message);
                _writer.Flush();
            }, _token);
            
        }

        public void Dispose()
        {
            _pipeServerStream?.Dispose();
            _writer?.Dispose();
        }
    }
}