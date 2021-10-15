using System;
using System.Threading.Tasks;

using Pipester.Protocol.Message;
using Pipester.Protocol.Pipe;
using Pipester.Protocol.Process;
using Pipester.Protocol.Setting;
using Pipester.Storage;

namespace Pipester
{
    public sealed class Connector : IDisposable
    {
        private readonly Server _server;

        private readonly Client _client;

        private readonly WorkflowSetting _setting;

        private readonly Notifier _notifier;

        private readonly Repository _repository;

        private Connector(Guid input, Guid output)
        {
            _client = new Client(input.ToString(), HandleResponse);
            _server = new Server(output.ToString());
            _repository = new Repository();
            _notifier = new Notifier(_repository);
            Subscriber = new Subscriber(_repository);
            Sender = new Sender(HandleRequest);
        }
        
        public Connector(Guid input, Guid output, bool compress = false) : this(input, output)
        {
            _setting = new WorkflowSetting(compress);

        }

        public Connector(Guid input, Guid output, string encryption) : this(input, output)
        {
            _setting = new WorkflowSetting(encryption);
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
                _client.Connect();
                _server.WaitConnection();
            }
            catch
            {
                Dispose();
            }
        }

        #region Dispose

        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _client.Dispose();
            _server.Dispose();

            _isDisposed = true;
        }

        ~Connector()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Handlers

        private void HandleResponse(string message)
        {
            Task.Run(() =>
            {
                try
                {
                    var workflow = new UnpackingWorkflow(_setting, _repository);
                    var result = workflow.Run(message);

                    _notifier.Notify(result.Type.FullName, result.Value);
                }
                finally { }
            });
        }

        private void HandleRequest(object message)
        {
            Task.Run(() =>
            {
                try
                {
                    var workflow = new PackingWorkflow(_setting);
                    var businessMessage = new BusinessMessage
                    {
                        Id = Guid.NewGuid(),
                        Type = message.GetType(),
                        Value = message
                    };
                    var result = workflow.Run(businessMessage);

                    _server.Send(result);
                }
                finally { }
            });
        }

        #endregion
    }
}