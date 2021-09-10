using System.Threading.Tasks;

using Pipester.Store.Interface;

namespace Pipester.Store
{
    internal sealed class Notifier
    {
        private readonly ISubscriptionRepository _repository;

        public Notifier(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public void Notify(object message)
        {
            var type = message.GetType();
            var listeners = _repository.GetListeners(type);

            if (listeners == null)
            {
                return;
            }

            Parallel.ForEach(listeners, listener => listener(message));
        }
    }
}