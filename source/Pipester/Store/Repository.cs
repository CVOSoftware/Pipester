using System;
using System.Collections.Generic;

using Pipester.Store.Interface;

namespace Pipester.Store
{
    internal sealed class Repository : IMessageTypeRepository, ISubscriptionRepository
    {
        private readonly Dictionary<string, Type> _messageTypeCache;

        private readonly Dictionary<Type, HashSet<Action<object>>> _messageSubscribers;

        public Repository()
        {
            _messageTypeCache = new Dictionary<string, Type>();
            _messageSubscribers = new Dictionary<Type, HashSet<Action<object>>>();
        }

        public void Subscribe(Type subscription, Action<object> action)
        {
            if (_messageSubscribers.TryGetValue(subscription, out var subscribers))
            {
                subscribers.Add(action);

                return;
            }

            var newSubscribersSet = new HashSet<Action<object>>
            {
                action
            };

            _messageTypeCache.TryAdd(subscription.FullName, subscription);
            _messageSubscribers.TryAdd(subscription, newSubscribersSet);
        }

        public void Unsubscribe(Type subscription, Action<object> action)
        {
            if (_messageSubscribers.TryGetValue(subscription, out var subscribers))
            {
                subscribers.Remove(action);

                if (subscribers.Count == 0 && subscription.FullName != null)
                {
                    _messageTypeCache.Remove(subscription.FullName);
                    _messageSubscribers.Remove(subscription);
                }
            }
        }
        
        public Type GetMessageTypeByName(string name)
        {
            var result = _messageTypeCache.TryGetValue(name, out var type) 
                ? type 
                : default;

            return result;
        }

        public IReadOnlySet<Action<object>> GetListeners(Type subscription)
        {
            var result = _messageSubscribers.TryGetValue(subscription, out var listeners)
                ? listeners
                : default;

            return result;
        }
    }
}