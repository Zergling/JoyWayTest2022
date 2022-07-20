using System.Collections;
using System.Collections.Generic;
using Game.Scripts.DI;
using UnityEngine;
using ZerglingPlugins.Windows;

namespace Game.Scripts.UI.Windows.Basic
{
    public class WindowBasic<TSetup> : Window<TSetup> where TSetup : WindowSetup, new()
    {
        protected WindowsManager _windowsManager;
        
        protected override void Awake()
        {
            base.Awake();
            
            var diContainer = DIContainer.Instance;
            _windowsManager = diContainer.Resolve<WindowsManager>();
        }

        public override void Setup(TSetup setup)
        {
            throw new System.NotImplementedException();
        }
    }
}

