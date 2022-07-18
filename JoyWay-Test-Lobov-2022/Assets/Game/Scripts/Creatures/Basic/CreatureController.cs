using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class CreatureController : MonoBehaviour
    {
        public int MaxHP => _config.maxHP;
        public int HP => _hp;

        [SerializeField] protected CreatureType _creatureType;
        
        protected int _hp;
        
        protected CreatureConfig _config;
        
        public virtual void OnAwake()
        {
            _config = CreatureConfigList.Instance.GetConfig(_creatureType);
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnUpdate()
        {
        }
    }
}

