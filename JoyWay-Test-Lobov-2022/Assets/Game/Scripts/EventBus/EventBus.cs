using System;
using System.Collections;
using System.Collections.Generic;
using ZerglingPlugins.Tools.Singleton;

namespace Game.Scripts.Events
{
    public class EventBus : Singleton<EventBus>
    {
        private Dictionary<Type, EventHandlerBase> _handlers;

        public EventBus()
        {
            _handlers = new Dictionary<Type, EventHandlerBase>();
        }

        public void Subscribe<T>(Action<T> callback) where T : struct, IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
            {
                var handler = new EventHandler<T>();
                _handlers[eventType] = handler;
            }
            
            (_handlers[eventType] as EventHandler<T>).Subscribe(callback);
        }

        public void UnSubscribe<T>(Action<T> callback) where T :  struct, IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
                return;
            
            (_handlers[eventType] as EventHandler<T>).UnSubscribe(callback);
        }

        public void Fire<T>(T evnt) where T : struct, IEvent
        {
            var eventType = evnt.GetType();
            
            if (!_handlers.ContainsKey(eventType))
                return;
            
            (_handlers[eventType] as EventHandler<T>).Fire(evnt);
        }
    }
}

