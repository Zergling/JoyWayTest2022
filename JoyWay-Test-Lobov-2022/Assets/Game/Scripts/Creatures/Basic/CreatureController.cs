using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Damage;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.GameSystems;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class CreatureController : BasicMonoBehaviour, IObjectPoolItem
    {
        public CreatureType CreatureType => _creatureType;
        public int MaxHP => _config.maxHP;
        public int HPValue=> _values[CreatureValueType.HP];
        public int WetValue => _values[CreatureValueType.Wet];

        [SerializeField] protected CreatureType _creatureType;

        protected Dictionary<CreatureValueType, int> _values;

        protected GameSettingsConfig _gameSettingsConfig;
        protected CreatureConfig _config;
        protected CreatureSystem _creatureSystem;
        protected EventBus _eventBus;

        private void Awake()
        {
            var diContainer = DIContainer.Instance;
            _creatureSystem = diContainer.Resolve<CreatureSystem>();
            _eventBus = diContainer.Resolve<EventBus>();
            
            var creatureConfigList = diContainer.Resolve<CreatureConfigList>();
            _config = creatureConfigList.GetConfig(_creatureType);

            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();
            
            _values = new Dictionary<CreatureValueType, int>();
            _values[CreatureValueType.HP] = _config.maxHP;
            _values[CreatureValueType.Wet] = 0;
        }

        public void OnSpawn()
        {
            _creatureSystem.SubscribeCreature(this);
            OnSpawnFinish();
        }

        public void OnDespawn()
        {
            _creatureSystem.UnSusbscribeCreature(this);
            OnDespawnFinish();
        }

        public virtual void OnSpawnFinish()
        {
        }

        public virtual void OnDespawnFinish()
        {
        }

        public virtual void ApplyDamage(DamageStruct damageStruct)
        {
            _values[CreatureValueType.HP] -= damageStruct.DamageValue;
            if (_values[CreatureValueType.HP] > _config.maxHP)
                _values[CreatureValueType.HP] = _config.maxHP;
            
            if (_values[CreatureValueType.HP] < 0)
                _values[CreatureValueType.HP] = 0;
            
            _values[CreatureValueType.Wet] += damageStruct.WetValue;
            if (_values[CreatureValueType.Wet] > _gameSettingsConfig.wetMaxValue)
                _values[CreatureValueType.Wet] = _gameSettingsConfig.wetMaxValue;

            if (_values[CreatureValueType.Wet] < 0)
                _values[CreatureValueType.Wet] = 0;

            var evnt = new CreatureValuesChangedEvent(this, _values[CreatureValueType.HP], _values[CreatureValueType.Wet]);
            _eventBus.Fire(evnt);
        }
    }
}

