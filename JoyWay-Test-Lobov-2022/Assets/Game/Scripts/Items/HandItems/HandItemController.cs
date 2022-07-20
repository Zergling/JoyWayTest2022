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
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ZerglingPlugins.Tools.Log;

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
            var diContainer = DIContainer.Instance;
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
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
        
        protected virtual void UseStarted()
        {
            LogUtils.Info(this, $"Use started");
        }

        protected virtual void UsePerformed()
        {
            LogUtils.Info(this, $"Use preformed");
        }

        protected virtual void UseCanceled()
        {
            LogUtils.Info(this, $"Use canceled");
        }

        
    }
}

