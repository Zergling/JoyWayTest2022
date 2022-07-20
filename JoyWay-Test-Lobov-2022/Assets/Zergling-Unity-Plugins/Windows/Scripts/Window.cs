using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    public abstract class Window<TSetup> : BaseWindow where TSetup : WindowSetup, new()
    {
        public abstract void Setup(TSetup setup);
    }

    [RequireComponent(typeof(WindowController))]
    public abstract class BaseWindow : MonoBehaviour
    {
        public WindowController Controller { get; private set; }

        protected virtual void Awake()
        {
            Controller = GetComponent<WindowController>();
        }

        public virtual void Adjust()
        {
            var rt = GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localScale = Vector3.one;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
    }
}
