using System.Threading.Tasks;

using Pipester.Storage.Repository;

namespace Pipester.Storage
{
    internal sealed class Notifier
    {
        private readonly IHandlerRepository _repository;

        public Notifier(IHandlerRepository repository)
        {
            _repository = repository;
        }

        public void Notify(string messageTypeName, object message)
        {
            var handlers = _repository.GetHandlersBySubscriptionTypeName(messageTypeName);

            if(handlers == null)
            {
                return;
            }

            Parallel.ForEach(handlers, handler => handler(message));
        }
    }
}