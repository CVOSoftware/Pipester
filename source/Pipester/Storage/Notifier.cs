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
            var trackedItems = _repository.GetTrackedItemsBySubscriptionTypeName(messageTypeName);

            if(trackedItems == null)
            {
                return;
            }

            Parallel.ForEach(trackedItems, tracker => tracker.Handler(message));
        }
    }
}