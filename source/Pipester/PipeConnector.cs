using System;

using Pipester.Protocol;
using Pipester.Subscription;

namespace Pipester
{
    public sealed class PipeConnector : IDisposable
    {
        private readonly bool _compress;

        private readonly string _encryption;

        
        public PipeConnector(Guid input, Guid output, bool compress = false)
        {
            var subscriptionManager = new Repository();

            _compress = compress;

            Subscriber = new Subscriber(subscriptionManager);
        }

        public PipeConnector(Guid input, Guid output, string encryption) : this (input, output)
        {
            _encryption = encryption;
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