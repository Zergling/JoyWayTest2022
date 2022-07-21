using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Damage;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.GameSystems;
using Game.Scripts.InflictEffects;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using TMPro;
using UnityEngine;
using ZerglingPlugins.Timers;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Creatures.Basic
{
    public class CreatureController : BasicMonoBehaviour, IObjectPoolItem
    {
        public CreatureType CreatureType => _creatureType;

        public int MaxHP => _config.maxHP;

        [SerializeField] protected CreatureType _creatureType;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        protected Dictionary<CreatureValueType, int> _values;
        protected List<InflictEffectController> _effectControllers;

        protected GameSettingsConfig _gameSettingsConfig;
        protected CreatureConfig _config;
        protected CreatureSystem _creatureSystem;
        protected EventBus _eventBus;
        protected TimerManager _timerManager;

        private void Awake()
        {
            var diContainer = DIContainer.Instance;
            _creatureSystem = diContainer.Resolve<CreatureSystem>();
            _eventBus = diContainer.Resolve<EventBus>();
            _timerManager = diContainer.Resolve<TimerManager>();
            
            var creatureConfigList = diContainer.Resolve<CreatureConfigList>();
            _config = creatureConfigList.GetConfig(_creatureType);

            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();
            
            _values = new Dictionary<CreatureValueType, int>();
            _values[CreatureValueType.HP] = _config.maxHP;
            _values[CreatureValueType.Wet] = 0;
            
            _effectControllers = new List<InflictEffectController>();
        }

        public void OnSpawn()
        {
            var instanceId = gameObject.GetInstanceID();
            _timerManager.CreateTimer(instanceId);
            _timerManager.SubscribeToEverySecondTick(instanceId, OnTimerTick);
            _creatureSystem.SubscribeCreature(this);
            OnSpawnFinish();
        }

        public void OnDespawn()
        {
            var instanceId = gameObject.GetInstanceID();
            _timerManager.SubscribeToEverySecondTick(instanceId, OnTimerTick);
            _timerManager.DeleteTimer(instanceId);
            _creatureSystem.UnSusbscribeCreature(this);
            OnDespawnFinish();
        }

        public virtual void OnSpawnFinish()
        {
        }

        public virtual void OnDespawnFinish()
        {
        }

        public virtual void ApplyDamage(ref DamageStruct damageStruct)
        {
            // applying inflict effects damage resists
            for (int i = 0; i < _effectControllers.Count; i++)
            {
                var effectController = _effectControllers[i];
                effectController.ApplyDamageResist(ref damageStruct);
            }

            //  applying HP damage
            _values[CreatureValueType.HP] -= damageStruct.DamageValue;
            if (_values[CreatureValueType.HP] > _config.maxHP)
                _values[CreatureValueType.HP] = _config.maxHP;
            
            if (_values[CreatureValueType.HP] < 0)
                _values[CreatureValueType.HP] = 0;
            
            
            // applying WET damage
            _values[CreatureValueType.Wet] += damageStruct.WetValue;
            if (_values[CreatureValueType.Wet] > _gameSettingsConfig.wetMaxValue)
                _values[CreatureValueType.Wet] = _gameSettingsConfig.wetMaxValue;

            if (_values[CreatureValueType.Wet] < 0)
                _values[CreatureValueType.Wet] = 0;
            
            // try to apply inflict effect
            if (damageStruct.InflictEffect != null)
                TryApplyInflictEffect(damageStruct.InflictEffect.Value);

            var evnt = new CreatureValuesChangedEvent(this);
            _eventBus.Fire(evnt);
        }
        
        public int GetCurrentValue(CreatureValueType valueType)
        {
            return _values[valueType];
        }

        protected void TryApplyInflictEffect(InflictEffectController effectController)
        {
            var withTimer = effectController.WithTimer;
            var instanceId = gameObject.GetInstanceID();
            
            // check if same effect is inflicted 
            // reset timer duration if found one
            // return if inflicted effect without timer
            for (int i = 0; i < _effectControllers.Count; i++)
            {
                var appliedEffect = _effectControllers[i];
                if (appliedEffect.EffectType == effectController.EffectType)
                {
                    if (!withTimer)
                        return;
                    
                    _timerManager.SetTimerDuration(instanceId, effectController.TimerDuration);
                    return;
                }
            }

            // return if cant apply inflict effect
            var checkApplyResult = effectController.CheckApply(this);
            if (!checkApplyResult)
                return;
            
            // remove effects
            var j = 0;
            while (j < _effectControllers.Count)
            {
                var appliedEffect = _effectControllers[j];
                if (effectController.RemoveEffects.Contains(appliedEffect.EffectType))
                {
                    _effectControllers.RemoveAt(j);
                    continue;
                }

                j++;
            }

            // set timer duration if inflict effect with timer duration
            if (withTimer)
                _timerManager.SetTimerDuration(instanceId, effectController.TimerDuration);
            
            _effectControllers.Add(effectController);

            // recolor _spriteRenderer on applying inflict effect
            if (_spriteRenderer != null)
                _spriteRenderer.color = effectController.Color;
        }
        
        private void OnTimerTick(float timeleft)
        {
            if (_effectControllers.Count == 0)
                return;

            var i = 0;
            while (i < _effectControllers.Count)
            {
                var effectController = _effectControllers[i];
                if (!effectController.WithTimer)
                {
                    i++;
                    continue;
                }

                var damageStructs = effectController.DamageStructs;
                for (int j = 0; j < damageStructs.Length; j++)
                {
                    var damageItem = damageStructs[j];
                    
                    var damage = new DamageStruct();
                    damage.DamageType = damageItem.damageType;
                    damage.DamageValue = damageItem.damageValue;
                    damage.WetValue = 0;
                    damage.InflictEffect = null;
                    
                    ApplyDamage(ref damage);
                }

                if (float.IsNaN(timeleft))
                {
                    _effectControllers.RemoveAt(i);
                    _spriteRenderer.color = Color.white;
                    continue;
                }

                i++;
            }
        }
    }
}

