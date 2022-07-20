using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    public class WindowTab<TWindow, TWindowSetup> : MonoBehaviour where TWindow : BaseWindow where TWindowSetup : WindowSetup
    {
        public bool activeInHierarchy => gameObject.activeInHierarchy;
        
        protected TWindow _window;
        protected WindowController _controller;
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void SubscribeToEvents()
        {
        }

        public virtual void UnSubscribeToEvents()
        {
        }

        public virtual void Setup(TWindow window, TWindowSetup setup)
        {
            _window = window;
            _controller = window.Controller;
        }

        public void CloseParentWindow()
        {
            _controller.Hide();
        }
    }
}

