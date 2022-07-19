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
        [SerializeField] private ObjectPoolController _playerObjectPool;
        [SerializeField] private ObjectPoolController _dummyObjectPool;

        [SerializeField] private ObjectPoolController _pistolPickupPool;
        [SerializeField] private ObjectPoolController _fireStonePickupPool;
        [SerializeField] private ObjectPoolController _waterStonePickupPool;

        public void OnAwake()
        {
            Init();
            _playerObjectPool.OnStart();
            _dummyObjectPool.OnStart();
            
            _pistolPickupPool.OnStart();
            _fireStonePickupPool.OnStart();
            _waterStonePickupPool.OnStart();
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

        public PickupItemController GetPickupObject(ItemType itemType)
        {
            GameObject obj = null;

            switch (itemType)
            {
                case ItemType.Pistol:
                    obj = _pistolPickupPool.GetObject();
                    break;
                
                case ItemType.FireStone:
                    obj = _fireStonePickupPool.GetObject();
                    break;
                
                case ItemType.WaterStone:
                    obj = _waterStonePickupPool.GetObject();
                    break;
            }

            var controller = obj.GetComponent<PickupItemController>();
            return controller;
        }

        public void ReturnPickupObject(PickupItemController pickupItemController)
        {
            var itemType = pickupItemController.ItemType;

            switch (itemType)
            {
                case ItemType.Pistol:
                    _pistolPickupPool.ReturnObject(pickupItemController.gameObject);
                    break;
                
                case ItemType.FireStone:
                    _fireStonePickupPool.ReturnObject(pickupItemController.gameObject);
                    break;
                
                case ItemType.WaterStone:
                    _waterStonePickupPool.ReturnObject(pickupItemController.gameObject);
                    break;
            }
        }
    }
}

