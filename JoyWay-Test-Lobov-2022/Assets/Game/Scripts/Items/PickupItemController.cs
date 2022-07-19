using System.Collections;
using System.Collections.Generic;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using UnityEngine;

namespace Game.Scripts.Items
{
    public class PickupItemController : BasicMonoBehaviour, IObjectPoolItem
    {
        public ItemId ItemId => _itemId;
        
        [SerializeField] private ItemId _itemId;

        private ObjectPoolManager _objectPoolManager;
        
        public void OnSpawn()
        {
            var diContainer = DIContainer.Instance;
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
        }

        public void OnDespawn()
        {
        }
        
        public HandItemController OnPickup()
        {
            var handItemController = _objectPoolManager.GetHandItemObject(_itemId);
            _objectPoolManager.ReturnPickupObject(this);
            return handItemController;
        }
    }
}

