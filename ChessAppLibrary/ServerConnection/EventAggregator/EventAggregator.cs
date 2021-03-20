using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAppLibrary.ServerConnection.EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private readonly WeakReferenceList<object> subscribers = new WeakReferenceList<object>();
        private static EventAggregator _instance;

        public static EventAggregator Get()
        {
            if(_instance == null)
            {
                _instance = new EventAggregator();
            }

            return _instance;
        }

        public virtual void Subscribe(object subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Publish<T>(T message) where T : class
        {
            Publish(ListSubscribers(message), message);
        }

        protected virtual void Publish<T>(IEnumerable<IHandle<T>> filteredSubscribers, T message) where T : class
        {
            foreach (var s in filteredSubscribers)
            {
                s.Handle(message);
            }
        }

        protected IEnumerable<IHandle<T>> ListSubscribers<T>(T message) where T : class
        {
            return subscribers
                .OfType<IHandle<T>>();
        }

        public virtual void Unsubscribe(object subscriber)
        {
            subscribers.Remove(subscriber);
        }
    }
}
