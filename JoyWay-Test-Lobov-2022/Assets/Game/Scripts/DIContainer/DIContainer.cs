using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Log;
using ZerglingPlugins.Tools.Singleton;

namespace Game.Scripts.DI
{
    public class DIContainer : Singleton<DIContainer>
    {
        private Dictionary<Type, object> _bindings;

        public DIContainer(): base()
        {
            _bindings = new Dictionary<Type, object>();
        }

        public void BindInstance(object instance)
        {
            var instanceType = instance.GetType();
            if (_bindings.ContainsKey(instanceType))
            {
                LogUtils.Error(this, $"{instanceType} is already binded!");
                return;
            }

            _bindings[instanceType] = instance;
        }

        public void UnBindInstance(object instance)
        {
            var instanceType = instance.GetType();
            if (!_bindings.ContainsKey(instanceType))
            {
                LogUtils.Error(this, $"Therer are no instances of {instanceType}");
                return;
            }

            _bindings.Remove(instanceType);
        }

        public T Resolve<T>() where T : class, new()
        {
            var instanceType = typeof(T);
            if (!_bindings.ContainsKey(instanceType))
            {
                LogUtils.Error(this, $"{instanceType} has no binded instances!");
                return null;
            }

            T result = (T) _bindings[instanceType];
            return result;
        }
    }
}


