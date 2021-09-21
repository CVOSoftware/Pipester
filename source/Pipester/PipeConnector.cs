using System;

using Pipester.Protocol;

namespace Pipester
{
    public sealed class PipeConnector : IDisposable
    {
        private readonly bool _compress;

        private readonly string _encryption;

        
        public PipeConnector(Guid input, Guid output, bool compress = false)
        {

        }

        public PipeConnector(Guid input, Guid output, string encryption) : this (input, output)
        {

        }

        public bool Connected { get; }

        public Sender Sender { get; }

        public Subscriber Subscriber { get; }

        public void Connect()
        {
            if (Connected)
            {
                return;
            }

            try
            {
                //_communicator.Connect();
            }
            catch
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            //_communicator?.Dispose();
        }
    }
}