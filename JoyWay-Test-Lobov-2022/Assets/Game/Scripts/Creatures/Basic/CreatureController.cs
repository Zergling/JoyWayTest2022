using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Damage;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.GameSystems;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
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

        protected DIContainer _diContainer;
        protected CreatureConfig _config;
        protected CreatureSystem _creatureSystem;
        
        public void OnSpawn()
        {
            _diContainer = DIContainer.Instance;
            _creatureSystem = _diContainer.Resolve<CreatureSystem>();
            
            var creatureConfigList = _diContainer.Resolve<CreatureConfigList>();
            _config = creatureConfigList.GetConfig(_creatureType);

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
            _wetValue += damageStruct.WetValue;
        }
    }
}

