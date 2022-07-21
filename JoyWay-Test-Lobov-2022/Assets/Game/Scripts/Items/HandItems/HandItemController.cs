using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Player;
using Game.Scripts.Damage;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.InflictEffects;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Items
{
    public class HandItemController : BasicMonoBehaviour, IObjectPoolItem
    {
        public ItemId ItemId => _itemId;
        
        [SerializeField] private ItemId _itemId;

        protected ItemConfig _config;
        protected PlayerController _playerController;

        protected ObjectPoolManager _objectPoolManager;
        protected EventBus _eventBus;

        private void Awake()
        {
            var diContainer = DIContainer.Instance;
            
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
            _eventBus = diContainer.Resolve<EventBus>();
            
            var itemConfigList = diContainer.Resolve<ItemConfigList>();
            _config = itemConfigList.GetConfig(_itemId);
        }

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

        public void Use(InputActionPhase phase)
        {
            switch (phase)
            {
                case InputActionPhase.Started:
                    UseStarted();
                    break;
                
                case InputActionPhase.Performed:
                    UsePerformed();
                    break;
                
                case InputActionPhase.Canceled:
                    UseCanceled();
                    break;
            }
        }

        public void Drop(Vector3 toPosition)
        {
            var pickupItemController = _objectPoolManager.GetPickupObject(_itemId);
            pickupItemController.Transform.position = toPosition;
            pickupItemController.Transform.LookAt(_playerController.Transform);
            pickupItemController.SetActive(true);
            _objectPoolManager.ReturnHandItemObject(this);
        }

        public DamageStruct GetDamageStruct()
        {
            var result = new DamageStruct();
            result.DamageType = _config.damageType;
            result.DamageValue = _config.damageValue;
            result.WetValue = _config.wetValue;
            
            if (_config.inflictEffectConfig == null)
                result.InflictEffect = null;
            else
                result.InflictEffect = new InflictEffectController(_config.inflictEffectConfig);
            
            return result;
        }

        protected virtual void UseStarted()
        {
        }

        protected virtual void UsePerformed()
        {
        }

        protected virtual void UseCanceled()
        {
        }
    }
}

