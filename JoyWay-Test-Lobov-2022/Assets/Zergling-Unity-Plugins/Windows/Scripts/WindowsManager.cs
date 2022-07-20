using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZerglingPlugins.Tools.Log;
using ZerglingPlugins.Tools.Singleton;

namespace ZerglingPlugins.Windows
{
    [RequireComponent(typeof(Canvas))]
    public class WindowsManager : DontDestroyMonoBehaviourSingleton<WindowsManager>
    {
        public delegate void WindowDelegate(Type windowType);

        public delegate BaseWindow InstantiateWindowDelegate(BaseWindow window, Transform parent);

        public static InstantiateWindowDelegate CustomWindowInstantiator;

        public event WindowDelegate WindowOpenedEvent;
        public event WindowDelegate WindowClosedEvent;
        public event Action AllWindowsClosedEvent;

        [SerializeField] private Transform _transform;
        
        [Tooltip("Initial prefabs. You can use AddWindowPrefabs() method for append other prefabs at runtime or component AdditionalWindowPrefabs.")]
        [SerializeField] 
        private List<BaseWindow> windowPrefabs;

        private readonly Dictionary<Type, BaseWindow> _windowPrefabsByType = new Dictionary<Type, BaseWindow>();
        private readonly Dictionary<Type, BaseWindow> _openedWindowsByType = new Dictionary<Type, BaseWindow>();

        private Canvas _canvas;
        private int _nextWindowSortingOrder;

        public bool IsAnyWindowOpened => _openedWindowsByType.Count > 0;

        public int OpenedWindowsCount => _openedWindowsByType.Count;

        public List<Type> OpenedWindowsTypes => _openedWindowsByType.Keys.ToList();

        public Camera CanvasCamera => _canvas.worldCamera;

        private List<IWindowUpdatable> _updatableWindows;

        protected override void Init()
        {
            base.Init();

            _canvas = GetComponent<Canvas>();
            _updatableWindows = new List<IWindowUpdatable>();

            AddWindowPrefabs(windowPrefabs);
        }

        private void Update()
        {
            for (int i = 0; i < _updatableWindows.Count; i++)
                _updatableWindows[i].OnUpdate();
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _updatableWindows.Count; i++)
                _updatableWindows[i].OnLateUpdate();
        }

        public void AddWindowPrefabs(List<BaseWindow> prefabs)
        {
            foreach (var windowPrefab in prefabs)
            {
                _windowPrefabsByType[windowPrefab.GetType()] = windowPrefab;
            }
        }
        
        public void SubscribeToUpdate(IWindowUpdatable window)
        {
            _updatableWindows.Add(window);
        }

        public void UnSubscribeToUpdate(IWindowUpdatable window)
        {
            var index = _updatableWindows.IndexOf(window);
            _updatableWindows.RemoveAt(index);
        }

        public void Open<TWindow>()
            where TWindow : Window<WindowSetup.Empty>, new()
        {
            Open<TWindow, WindowSetup.Empty>(WindowSetup.Empty.Instance);
        }

        public void Open<TWindow, TSetup>(TSetup setup)
            where TWindow : Window<TSetup>, new()
            where TSetup : WindowSetup, new()
        {
            var windowType = typeof(TWindow);

            if (!_windowPrefabsByType.ContainsKey(windowType))
                throw new Exception($"No prefab for window: {windowType}");

            LogUtils.Info(this, $"Open: {windowType}");

            var window = CreateWindow<TWindow, TSetup>(windowType);
            window.Setup(setup);
            window.Controller.Show();
        }

        private Window<TSetup> CreateWindow<TWindow, TSetup>(Type windowType)
            where TWindow : Window<TSetup>, new()
            where TSetup : WindowSetup, new()
        {
            var original = _windowPrefabsByType[windowType];

            var window = InstantiateWindow((TWindow)original);
            window.Adjust();

            SetWindowSorting(window);
            ControlWindow(window, windowType);

            _openedWindowsByType.Add(windowType, window);

            return window;
        }

        private Window<TSetup> InstantiateWindow<TSetup>(Window<TSetup> original) where TSetup : WindowSetup, new()
        {
            var window = CustomWindowInstantiator != null
                ? (Window<TSetup>)CustomWindowInstantiator(original, _transform)
                : Instantiate(original, _transform);

            return window;
        }

        private void SetWindowSorting(BaseWindow window)
        {
            window.gameObject.layer = gameObject.layer;

            var canvas = window.Controller.Canvas;
            canvas.overrideSorting = true;
            canvas.sortingLayerID = _canvas.sortingLayerID;
            canvas.sortingOrder = _nextWindowSortingOrder;

            _nextWindowSortingOrder++;
        }

        private void ControlWindow(BaseWindow window, Type windowType)
        {
            var controller = window.Controller;
            controller.WindowShownEvent += OnWindowShown;
            controller.WindowHiddenEvent += OnWindowHidden;

            void OnWindowShown()
            {
                controller.WindowShownEvent -= OnWindowShown;
                WindowOpenedEvent?.Invoke(windowType);
            }

            void OnWindowHidden()
            {
                controller.WindowShownEvent -= OnWindowShown;
                controller.WindowHiddenEvent -= OnWindowHidden;
                Destroy(window.gameObject);

                _openedWindowsByType.Remove(windowType);
                WindowClosedEvent?.Invoke(windowType);

                if (_openedWindowsByType.Count == 0)
                {
                    _nextWindowSortingOrder = 0;
                    AllWindowsClosedEvent?.Invoke();
                }
            }
        }

        public bool IsOpened<TWindow>() where TWindow : BaseWindow
        {
            var windowType = typeof(TWindow);

            return _openedWindowsByType.ContainsKey(windowType);
        }

        public void CloseAll()
        {
            if (!IsAnyWindowOpened)
                return;

            LogUtils.Info(this, $"Close All count: {_openedWindowsByType.Count}");

            var windowTypes = _openedWindowsByType.Keys.ToList();

            foreach (var windowType in windowTypes)
            {
                Close(windowType);
            }
        }

        public void Close<TWindow>() where TWindow : BaseWindow
        {
            var windowType = typeof(TWindow);

            Close(windowType);
        }

        private void Close(Type windowType)
        {
            if (!_openedWindowsByType.TryGetValue(windowType, out var window))
                return;

            LogUtils.Info(this, $"Close: {windowType}");

            window.Controller.Hide();
        }
    }
}
