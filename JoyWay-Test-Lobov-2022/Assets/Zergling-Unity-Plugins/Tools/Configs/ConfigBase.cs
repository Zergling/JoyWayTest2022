using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace ZerglingPlugins.Tools.Configs
{
    /// <summary>
    /// не забудь в дочернем классе добавить
    /// [CreateAssetMenu(fileName = "T.Name", menuName = "Configs/T.Name")]
    /// чтобы добавить пункт меню для создания конфига
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigBase<T> : ScriptableObject where T: Object
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<T>(typeof(T).Name);

                return _instance;
            }
        }

        public virtual void Init()
        {
            LogUtils.Info(typeof(T).Name, $"Init");
        }
    }
}
