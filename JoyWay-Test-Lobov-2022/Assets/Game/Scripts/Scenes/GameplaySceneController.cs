using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using UnityEngine;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.GameSystems;
using Game.Scripts.ObjectPool;

namespace Game.Scripts.Scenes
{
    public class GameplaySceneController : MonoBehaviour
    {
        [Header("BINDINGS")]
        [SerializeField] private ObjectPoolManager _objectPoolManager;

        [Header("SPAWN POINTS")] 
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _dummySpawnPoint;
        [SerializeField] private Transform[] _pistolPickupSpawnPoints;
        [SerializeField] private Transform[] _fireStonePickupSpawnPoints;
        [SerializeField] private Transform[] _waterStonePickupSpawnPoints;

        private CreatureSystem _creatureSystem;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            InstallBindings();

            var diContainer = DIContainer.Instance;
            _creatureSystem = diContainer.Resolve<CreatureSystem>();
            
            _objectPoolManager.OnAwake();

            SpawnGameObjects();
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            _creatureSystem.OnFixedUpdate();
        }

        private void Update()
        {
            _creatureSystem.OnUpdate();
        }

        private void LateUpdate()
        {
        }

        private void InstallBindings()
        {
            var diContainer = DIContainer.Instance;
            
            // configs
            diContainer.BindInstance(CreatureConfigList.Instance);
            diContainer.BindInstance(GameSettingsConfig.Instance);
            
            // object pools
            diContainer.BindInstance(_objectPoolManager);
            
            // game systems
            diContainer.BindInstance(CreatureSystem.Instance);
            diContainer.BindInstance(EventBus.Instance);
        }

        private void SpawnGameObjects()
        {
            var player = _objectPoolManager.GetCreatureObject(CreatureType.Player);
            player.Transform.position = _playerSpawnPoint.position;
            player.Transform.eulerAngles = _playerSpawnPoint.eulerAngles;
            player.Transform.SetParent(null);
            player.SetActive(true);

            var dummy = _objectPoolManager.GetCreatureObject(CreatureType.Dummy);
            dummy.Transform.position = _dummySpawnPoint.position;
            dummy.Transform.eulerAngles = _dummySpawnPoint.eulerAngles;
            dummy.Transform.SetParent(null);
            dummy.SetActive(true);

            for (int i = 0; i < _pistolPickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _pistolPickupSpawnPoints[i];
                
                var pistolPickup = _objectPoolManager.GetPickupObject(ItemId.Pistol);
                pistolPickup.Transform.position = spawnPoint.position;
                pistolPickup.Transform.eulerAngles = spawnPoint.eulerAngles;
                pistolPickup.SetActive(true);
            }
            
            for (int i = 0; i < _fireStonePickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _fireStonePickupSpawnPoints[i];
                
                var fireStonePickup = _objectPoolManager.GetPickupObject(ItemId.FireStone);
                fireStonePickup.Transform.position = spawnPoint.position;
                fireStonePickup.Transform.eulerAngles = spawnPoint.eulerAngles;
                fireStonePickup.SetActive(true);
            }
            
            for (int i = 0; i < _waterStonePickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _waterStonePickupSpawnPoints[i];
                
                var waterStonePickup = _objectPoolManager.GetPickupObject(ItemId.WaterStone);
                waterStonePickup.Transform.position = spawnPoint.position;
                waterStonePickup.Transform.eulerAngles = spawnPoint.eulerAngles;
                waterStonePickup.SetActive(true);
            }
        }
    }
}

