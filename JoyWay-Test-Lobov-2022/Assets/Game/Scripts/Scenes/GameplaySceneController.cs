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

        private DIContainer _diContainer;
        
        private CreatureSystem _creatureSystem;
        
        private void Awake()
        {
            InstallBindings();
            
            _creatureSystem = _diContainer.Resolve<CreatureSystem>();
            
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
            _diContainer = DIContainer.Instance;
            
            // configs
            _diContainer.BindInstance(CreatureConfigList.Instance);
            _diContainer.BindInstance(GameSettingsConfig.Instance);
            
            // object pools
            _diContainer.BindInstance(_objectPoolManager);
            
            // game systems
            _diContainer.BindInstance(CreatureSystem.Instance);
            _diContainer.BindInstance(EventBus.Instance);
        }

        private void SpawnGameObjects()
        {
            var player = _objectPoolManager.GetCreatureObject(CreatureType.Player);
            player.Transform.position = _playerSpawnPoint.position;
            player.Transform.eulerAngles = _playerSpawnPoint.eulerAngles;
            player.SetActive(true);

            var dummy = _objectPoolManager.GetCreatureObject(CreatureType.Dummy);
            dummy.Transform.position = _dummySpawnPoint.position;
            dummy.Transform.eulerAngles = _dummySpawnPoint.eulerAngles;
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

