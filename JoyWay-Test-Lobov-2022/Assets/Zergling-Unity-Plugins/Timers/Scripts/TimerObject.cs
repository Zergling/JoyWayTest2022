using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Timers
{
    public class TimerObject
    {
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }

        public int Id => _id;
        public float Duration => _duration;
        public float SumDelta => _sumDelta;
        
        private int _id;
        private float _duration;
        private float _sumDelta;
        private bool _paused;

        public TimerObject(int id)
        {
            _id = id;
            _duration = 0;
            _sumDelta = 0;
            _paused = false;
        }

        public void Tick(float deltaTime)
        {
            if (float.IsNaN(_duration))
                return;
        
            _duration -= deltaTime;
            _sumDelta += deltaTime;
            if (_duration < 0)
                _duration = float.NaN;
        }

        public void AddDuration(float time)
        {
            if (float.IsNaN(_duration))
            {
                _duration = 0.0f;
                _sumDelta = 0.0f;
            }

            _duration += time;
        }

        public void SetDuration(float time)
        {
            _duration = time;
            _sumDelta = 0.0f;
        }

        public void OnEverySecondTick()
        {
            _sumDelta -= 1.0f;
        }
    }
}


