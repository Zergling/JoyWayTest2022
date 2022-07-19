using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Singleton;

namespace Game.Scripts.Events
{
    public class EventBus : Singleton<EventBus>
    {
        private Dictionary<Type, List<Action<IEvent>>> _handlers;

        public EventBus()
        {
            _handlers = new Dictionary<Type, List<Action<IEvent>>>();
        }

        public void Subscribe<T>(Action<IEvent> callback) where T : IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
                _handlers[eventType] = new List<Action<IEvent>>();

            var list = _handlers[eventType];
            list.Add(callback);
        }

        public void UnSubscribe<T>(Action<IEvent> callback) where T : IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
                return;

            var list = _handlers[eventType];
            if (!list.Contains(callback))
                return;

            list.Remove(callback);
        }

        public void Fire(IEvent evnt)
        {
            var eventType = evnt.GetType();
            
            if (!_handlers.ContainsKey(eventType))
                return;
            
            var list = _handlers[eventType];
            for (int i = 0; i < list.Count; i++)
            {
                var callback = list[i];
                callback.Invoke(evnt);
            }
        }
    }
}

