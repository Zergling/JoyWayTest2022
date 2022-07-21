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
            var hp = _values[CreatureValueType.HP];
            if (hp > _config.maxHP)
                hp = _config.maxHP;
            
            if (hp < 0)
                hp = 0;
            
            _values[CreatureValueType.Wet] += damageStruct.WetValue;
            var wet = _values[CreatureValueType.Wet];
            if (wet > _gameSettingsConfig.wetMaxValue)
                wet = _gameSettingsConfig.wetMaxValue;

            if (wet < 0)
                wet = 0;

            var evnt = new CreatureValuesChangedEvent(this, hp, wet);
            _eventBus.Fire(evnt);
        }
    }
}

