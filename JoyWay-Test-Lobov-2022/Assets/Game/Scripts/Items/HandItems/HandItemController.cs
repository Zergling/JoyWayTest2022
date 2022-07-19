using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Player;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Items
{
    public class HandItemController : BasicMonoBehaviour, IObjectPoolItem
    {
        public ItemId ItemId => _itemId;
        
        [SerializeField] private ItemId _itemId;

        private PlayerController _playerController;

        private ObjectPoolManager _objectPoolManager;

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
        
        public virtual void Setup(PlayerController playerController)
        {
            _playerController = playerController;
            
            var diContainer = DIContainer.Instance;
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
        }

        public virtual void Fire()
        {
        }

        public void Drop()
        {
            
        }
    }
}

