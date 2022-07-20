using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.DI;
using Game.Scripts.Events;
using UnityEngine;
using ZerglingPlugins.Windows;

namespace Game.Scripts.Utils
{
    public class WindowsManagerEventHandler : MonoBehaviour
    {
        [SerializeField] private WindowsManager _windowsManager;

        private EventBus _eventBus;
        
        private void Awake()
        {
            var diContainer = DIContainer.Instance;
            _eventBus = diContainer.Resolve<EventBus>();
        }

        private void OnEnable()
        {
            _windowsManager.WindowOpenedEvent += OnWindowOpenedEvent;
            _windowsManager.WindowClosedEvent += OnWindowClosedEvent;
            _windowsManager.AllWindowsClosedEvent += OnAllWindowsClosedEvent;
        }

        private void OnDisable()
        {
            _windowsManager.WindowOpenedEvent -= OnWindowOpenedEvent;
            _windowsManager.WindowClosedEvent -= OnWindowClosedEvent;
            _windowsManager.AllWindowsClosedEvent -= OnAllWindowsClosedEvent;
        }
        
        private void OnWindowOpenedEvent(Type windowType)
        {
            var evnt = new WindowOpenedEvent(windowType);
            _eventBus.Fire(evnt);
        }
        
        private void OnWindowClosedEvent(Type windowType)
        {
            var evnt = new WindowClosedEvent(windowType);
            _eventBus.Fire(evnt);
        }
        
        private void OnAllWindowsClosedEvent()
        {
            var evnt = new AllWindowClosedEvent();
            _eventBus.Fire(evnt);
        }
    }
}

