using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Components;
using ZerglingPlugins.Tools.Log;

namespace ZerglingPlugins.Windows
{
    public class AdditionalWindowPrefabs : RequiredFieldsMonoBehaviour
    {
        [SerializeField] private List<BaseWindow> windowPrefabs;

        private void Awake()
        {
            var windowsManager = WindowsManager.Instance;

            if (windowsManager != null)
                windowsManager.AddWindowPrefabs(windowPrefabs);
            else
                LogUtils.Error(this, "WindowsManager must be instantiated first");
        }
    }
}


