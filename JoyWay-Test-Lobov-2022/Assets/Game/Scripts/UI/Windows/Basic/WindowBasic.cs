using System.Collections;
using System.Collections.Generic;
using Game.Scripts.DI;
using Game.Scripts.Events;
using UnityEngine;
using ZerglingPlugins.Windows;

namespace Game.Scripts.UI.Windows.Basic
{
    public class WindowBasic<TSetup> : Window<TSetup> where TSetup : WindowSetup, new()
    {
        public EventBus EventBus;
        
        protected WindowsManager _windowsManager;
        
        
        protected override void Awake()
        {
            base.Awake();
            
            var diContainer = DIContainer.Instance;
            _windowsManager = diContainer.Resolve<WindowsManager>();
            EventBus = diContainer.Resolve<EventBus>();
        }

        public override void Setup(TSetup setup)
        {
            throw new System.NotImplementedException();
        }
    }
}

