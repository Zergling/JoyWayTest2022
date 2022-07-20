using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    public interface IWindowUpdatable
    {
        void OnUpdate();
    }
    
    public abstract class WindowUpdatable<TSetup> : Window<TSetup>, IWindowUpdatable where TSetup : WindowSetup, new()
    {
        private void OnEnable()
        {
            WindowsManager.Instance.SubscribeToUpdate(this);
        }

        private void OnDisable()
        {
            WindowsManager.Instance.UnSubscribeToUpdate(this);
        }

        public override void Setup(TSetup setup)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
