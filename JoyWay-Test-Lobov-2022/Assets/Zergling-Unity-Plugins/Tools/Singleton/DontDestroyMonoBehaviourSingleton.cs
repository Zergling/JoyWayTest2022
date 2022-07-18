﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Tools.Singleton
{
    public abstract class DontDestroyMonoBehaviourSingleton<T> : MonoBehaviourSingleton<T>
        where T : DontDestroyMonoBehaviourSingleton<T>
    {
        public new static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

        protected override void Init()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
