using System;

using Pipester.Protocol;
using Pipester.Storage;

namespace Pipester
{
    public sealed class Connector : IDisposable
    {
        private readonly bool _compress;

        private readonly string _encryption;

        private readonly Notifier _notifier;

        private readonly SubscriptionManager _subscriptionManager;
        
        public Connector(Guid input, Guid output, bool compress = false)
        {
            _subscriptionManager = new SubscriptionManager();
            _notifier = new Notifier(_subscriptionManager);
            Subscriber = new Subscriber(_subscriptionManager);
        }

        public Connector(Guid input, Guid output, string encryption) : this (input, output)
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