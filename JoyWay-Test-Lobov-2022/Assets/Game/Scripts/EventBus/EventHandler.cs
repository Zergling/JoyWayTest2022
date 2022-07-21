using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Events
{
    public class EventHandler<T>: EventHandlerBase
    {
        private List<Action<T>> _callbacks;

        public EventHandler()
        {
            _callbacks = new List<Action<T>>();
        }

        public virtual void Subscribe(Action<T> callback)
        {
            if (_callbacks.Contains(callback))
                return;
            
            _callbacks.Add(callback);
        }

        public virtual void UnSubscribe(Action<T> callback)
        {
            if (!_callbacks.Contains(callback))
                return;

            var index = _callbacks.IndexOf(callback);
            _callbacks.RemoveAt(index);
        }

        public virtual void Fire(T evnt)
        {
            for (int i = 0; i < _callbacks.Count; i++)
                _callbacks[i].Invoke(evnt);
        }
    }
}

