using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Log;
using ZerglingPlugins.Tools.Singleton;

namespace ZerglingPlugins.Timers
{
    public class TimerManager : DontDestroyMonoBehaviourSingleton<TimerManager>
    {
        public delegate void TickListener(float timeLeft);
        
        private Dictionary<int, TimerObject> _timersDict;
        private List<TimerObject> _timersList;

        private Dictionary<int, List<TickListener>> _tickListeners;

        private float _realtimeSinceStartup;

        public const int ALL_TIMERS = -1;

        private void Awake()
        {
            _timersDict = new Dictionary<int, TimerObject>();
            _timersList = new List<TimerObject>();

            _tickListeners = new Dictionary<int, List<TickListener>>();

            _realtimeSinceStartup = Time.realtimeSinceStartup;
        }

        public void OnLateUpdate()
        {
            var realtimeSinceStartup = Time.realtimeSinceStartup;
            var delta = realtimeSinceStartup - _realtimeSinceStartup;
            _realtimeSinceStartup = realtimeSinceStartup;

            for (int i = 0; i < _timersList.Count; i++)
            {
                var timer = _timersList[i];
                if (timer.Paused)
                    continue;
                
                timer.Tick(delta);

                var timerId = timer.Id;
                if (!_tickListeners.ContainsKey(timerId))
                    continue;

                var list = _tickListeners[timerId];
                for (int j = 0; j < list.Count; j++)
                    list[j].Invoke(timer.Duration);
            }
        }

        public void SetPause(int timerId, bool pause)
        {
            if (timerId == ALL_TIMERS)
            {
                for (int i = 0; i < _timersList.Count; i++)
                    _timersList[i].Paused = pause;

                return;
            }

            _timersDict[timerId].Paused = pause;
        }

        public void CreateTimer(int id)
        {
            if (_timersDict.ContainsKey(id))
                return;
            
            var timer = new TimerObject(id);
            _timersDict[id] = timer;
            _timersList.Add(timer);
        }

        public void DeleteTimer(int id)
        {
            if (!_timersDict.ContainsKey(id))
                return;

            var timer = _timersDict[id];
            var index = _timersList.IndexOf(timer);
            _timersList.RemoveAt(index);
            _timersDict.Remove(id);
        }
        
        public bool HasTimer(int id)
        {
            return _timersDict.ContainsKey(id);
        }

        public void AddTimerDuration(int id, float time)
        {
            if (!_timersDict.ContainsKey(id))
                return;

            var timer = _timersDict[id];
            timer.AddDuration(time);
        }

        public void SetTimerDuration(int id, float time)
        {
            if (!_timersDict.ContainsKey(id))
                return;

            var timer = _timersDict[id];
            timer.SetDuration(time);
        }

        public float GetTimeLeft(int id)
        {
            if (!_timersDict.ContainsKey(id))
                return float.NaN;

            var timer = _timersDict[id];
            return timer.Duration;
        }

        public void SubscribeToTick(int id, TickListener listener)
        {
            if (!_tickListeners.ContainsKey(id))
                _tickListeners[id] = new List<TickListener>();

            var list = _tickListeners[id];
            if (list.Contains(listener))
            {
                LogUtils.Error(this, $"Listener {listener.Method.Name} already subscribed to timer tick");
                return;
            }

            list.Add(listener);
        }

        public void UnSubscribeToTick(int id, TickListener listener)
        {
            if (!_tickListeners.ContainsKey(id))
                return;

            var list = _tickListeners[id];
            if (!list.Contains(listener))
            {
                LogUtils.Error(this, $"Listener {listener.Method.Name} not subscribed to timer tick");
                return;
            }

            list.Remove(listener);
        }
    }
}

