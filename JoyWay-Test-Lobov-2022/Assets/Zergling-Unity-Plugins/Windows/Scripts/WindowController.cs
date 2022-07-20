using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    [RequireComponent(typeof(Canvas))]
    public abstract class WindowController : MonoBehaviour
    {
        public event Action WindowShownEvent;
        public event Action WindowHiddenEvent;

        public Canvas Canvas { get; private set; }

        protected virtual void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }

        protected virtual void OnDestroy()
        {
            Canvas = null;
        }

        public void Show()
        {
            AnimateShow(OnShown);
        }

        protected virtual void AnimateShow(Action completeCallback)
        {
            completeCallback.Invoke();
        }

        private void OnShown()
        {
            WindowShownEvent?.Invoke();
        }

        public void Hide()
        {
            AnimateHide(OnHidden);
        }

        protected virtual void AnimateHide(Action completeCallback)
        {
            completeCallback.Invoke();
        }

        private void OnHidden()
        {
            WindowHiddenEvent?.Invoke();
        }
    }
}


