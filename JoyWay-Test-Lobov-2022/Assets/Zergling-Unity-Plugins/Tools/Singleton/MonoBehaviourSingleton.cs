using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Components;

namespace ZerglingPlugins.Tools.Singleton
{
    public abstract class MonoBehaviourSingleton<T> : RequiredFieldsMonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        protected static T _instance;

        public static T Instance => _instance;

        private void Awake()
        {
            _instance = (T)this;

            Init();
        }

        protected abstract void Init();
    }
}
