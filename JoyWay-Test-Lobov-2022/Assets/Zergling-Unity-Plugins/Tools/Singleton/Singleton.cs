using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Tools.Singleton
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }

        protected Singleton()
        {
            _instance = (T) this;
        }
    }
}
