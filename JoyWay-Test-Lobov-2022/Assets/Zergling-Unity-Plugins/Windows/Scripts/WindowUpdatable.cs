using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    public interface IWindowUpdatable
    {
        void OnUpdate();
        void OnLateUpdate();
    }
}
