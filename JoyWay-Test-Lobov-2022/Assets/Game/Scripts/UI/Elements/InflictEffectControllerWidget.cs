using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.DI;
using Game.Scripts.InflictEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZerglingPlugins.Timers;

namespace Game.Scripts.UI.Elements
{
    public class InflictEffectControllerWidget : MonoBehaviour
    {
        [SerializeField] private Image _effectIcon;
        [SerializeField] private Image _timerFiller;

        private int _creatureInstanceId;
        private InflictEffectController _effectController;
        private bool _unsubscribeTimerTickOnReset;
        
        private TimerManager _timerManager;
        private SpriteConfig _spriteConfig;
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void Init()
        {
            _unsubscribeTimerTickOnReset = false;
            
            var diContainer = DIContainer.Instance;
            _timerManager = diContainer.Resolve<TimerManager>();
            _spriteConfig = diContainer.Resolve<SpriteConfig>();
        }

        public void Setup(int creatureInstanceId, InflictEffectController effectController)
        {
            _creatureInstanceId = creatureInstanceId;
            _effectController = effectController;

            var icon = _spriteConfig.GetInflictEffectSprite(_effectController.EffectType);
            _effectIcon.sprite = icon;
            
            if (_effectController.WithTimer)
            {
                _unsubscribeTimerTickOnReset = true;
                _timerManager.SubscribeToLateUpdateTick(_creatureInstanceId, OnLateUpdateTick);
            }
            else
                _unsubscribeTimerTickOnReset = false;
        }

        public void Reset()
        {
            _timerFiller.fillAmount = 0f;
            
            if (_unsubscribeTimerTickOnReset)
                _timerManager.UnSubscribeToLateUpdateTick(_creatureInstanceId, OnLateUpdateTick);
        }

        private void OnLateUpdateTick(float timeleft)
        {
            var duration = _effectController.TimerDuration;
            var timeSpend = duration - timeleft;
            var fillAmount = timeSpend / duration;
            _timerFiller.fillAmount = fillAmount;
        }
    }
}

