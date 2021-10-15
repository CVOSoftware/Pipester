using System;

using Pipester.Storage.Repository;

namespace Pipester
{
    public sealed class Subscriber
    {
        private readonly ISubscriptionRepository _repository;

        internal Subscriber(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public Subscription Subscribe<T>(Action<T> handler) where T : class
        {
            var type = typeof(T);
            var token = _repository.Subscribe(type, obj =>
            {
                if (obj is T message)
                { 
                    handler(message);
                }
            });

            return token;
        }
    }
}