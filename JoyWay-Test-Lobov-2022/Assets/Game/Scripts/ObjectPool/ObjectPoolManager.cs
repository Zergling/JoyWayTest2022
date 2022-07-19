using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Enums;
using Game.Scripts.Items;
using UnityEngine;
using ZerglingPlugins.Tools.Singleton;

namespace Game.Scripts.ObjectPool
{
    public class ObjectPoolManager : DontDestroyMonoBehaviourSingleton<ObjectPoolManager>
    {
        [Header("CREATURES")]
        [SerializeField] private ObjectPoolController _playerObjectPool;
        [SerializeField] private ObjectPoolController _dummyObjectPool;

        [Header("PICKUPS")]
        [SerializeField] private ObjectPoolController _pistolPickupPool;
        [SerializeField] private ObjectPoolController _fireStonePickupPool;
        [SerializeField] private ObjectPoolController _waterStonePickupPool;

        [Header("HAND ITEMS")] 
        [SerializeField] private ObjectPoolController _pistolHandItemPool;
        [SerializeField] private ObjectPoolController _fireStoneHandItemPool;
        [SerializeField] private ObjectPoolController _waterStoneHandItemPool;

        public void OnAwake()
        {
            Init();
            
            _playerObjectPool.OnStart();
            _dummyObjectPool.OnStart();

            _pistolPickupPool.OnStart();
            _fireStonePickupPool.OnStart();
            _waterStonePickupPool.OnStart();
            
            _pistolHandItemPool.OnStart();
            _fireStoneHandItemPool.OnStart();
            _waterStoneHandItemPool.OnStart();
        }

        public CreatureController GetCreatureObject(CreatureType creatureType)
        {
            GameObject obj = null;

            switch (creatureType)
            {
                case CreatureType.Player:
                    obj = _playerObjectPool.GetObject();
                    break;

                case CreatureType.Dummy:
                    obj = _dummyObjectPool.GetObject();
                    break;
            }

            var controller = obj.GetComponent<CreatureController>();
            controller.OnSpawn();
            return controller;
        }

        public void ReturnCreatureObject(CreatureController creatureController)
        {
            creatureController.OnDespawn();

            var creatureType = creatureController.CreatureType;

            switch (creatureType)
            {
                case CreatureType.Player:
                    _playerObjectPool.ReturnObject(creatureController.gameObject);
                    break;

                case CreatureType.Dummy:
                    _dummyObjectPool.ReturnObject(creatureController.gameObject);
                    break;
            }
        }

        public PickupItemController GetPickupObject(ItemId itemId)
        {
            GameObject obj = null;

            switch (itemId)
            {
                case ItemId.Pistol:
                    obj = _pistolPickupPool.GetObject();
                    break;

                case ItemId.FireStone:
                    obj = _fireStonePickupPool.GetObject();
                    break;

                case ItemId.WaterStone:
                    obj = _waterStonePickupPool.GetObject();
                    break;
            }

            var controller = obj.GetComponent<PickupItemController>();
            return controller;
        }

        public void ReturnPickupObject(PickupItemController pickupItemController)
        {
            var itemType = pickupItemController.ItemId;

            switch (itemType)
            {
                case ItemId.Pistol:
                    _pistolPickupPool.ReturnObject(pickupItemController.gameObject);
                    break;

                case ItemId.FireStone:
                    _fireStonePickupPool.ReturnObject(pickupItemController.gameObject);
                    break;

                case ItemId.WaterStone:
                    _waterStonePickupPool.ReturnObject(pickupItemController.gameObject);
                    break;
            }
        }

        public HandItemController GetHandItemObject(ItemId itemId)
        {
            GameObject obj = null;
            
            switch (itemId)
            {
                case ItemId.Pistol:
                    obj = _pistolHandItemPool.GetObject();
                    break;
                
                case ItemId.FireStone:
                    obj = _fireStoneHandItemPool.GetObject();
                    break;
                
                case ItemId.WaterStone:
                    obj = _waterStoneHandItemPool.GetObject();
                    break;
            }

            var controller = obj.GetComponent<HandItemController>();
            controller.OnSpawn();
            return controller;
        }

        public void ReturnHandItemObject(HandItemController handItemController)
        {
            handItemController.OnDespawn();
            var itemId = handItemController.ItemId;

            switch (itemId)
            {
                case ItemId.Pistol:
                    _pistolHandItemPool.ReturnObject(handItemController.gameObject);
                    break;
                
                case ItemId.FireStone:
                    _fireStoneHandItemPool.ReturnObject(handItemController.gameObject);
                    break;
                
                case ItemId.WaterStone:
                    _waterStoneHandItemPool.ReturnObject(handItemController.gameObject);
                    break;
            }
        }
    }
}

