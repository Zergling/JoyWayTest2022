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
        public int HP => _hp;
        public int WetValue => _wetValue;

        [SerializeField] protected CreatureType _creatureType;

        protected int _hp;
        protected int _wetValue;

        protected GameSettingsConfig _gameSettingsConfig;
        protected CreatureConfig _config;
        protected CreatureSystem _creatureSystem;
        protected EventBus _eventBus;
        
        public void OnSpawn()
        {
            var diContainer = DIContainer.Instance;
            _creatureSystem = diContainer.Resolve<CreatureSystem>();
            _eventBus = diContainer.Resolve<EventBus>();
            
            var creatureConfigList = diContainer.Resolve<CreatureConfigList>();
            _config = creatureConfigList.GetConfig(_creatureType);

            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();

            _hp = _config.maxHP;
            _wetValue = 0;
            
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
            _hp -= damageStruct.DamageValue;
            if (_hp > _config.maxHP)
                _hp = _config.maxHP;
            
            if (_hp < 0)
                _hp = 0;
            
            _wetValue += damageStruct.WetValue;
            if (_wetValue > _gameSettingsConfig.wetMaxValue)
                _wetValue = _gameSettingsConfig.wetMaxValue;

            if (_wetValue < 0)
                _wetValue = 0;

            var evnt = new CreatureValuesChangedEvent(this, _hp, _wetValue);
            _eventBus.Fire(evnt);
        }
    }
}

