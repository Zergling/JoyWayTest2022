using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using UnityEngine;
using Game.Scripts.DI;
using Game.Scripts.Enums;
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
        }

        private void SpawnGameObjects()
        {
            var player = _objectPoolManager.GetCreatureObject(CreatureType.Player);
            player.Transform.position = _playerSpawnPoint.position;
            player.Transform.eulerAngles = _playerSpawnPoint.eulerAngles;
            player.gameObject.SetActive(true);

            var dummy = _objectPoolManager.GetCreatureObject(CreatureType.Dummy);
            dummy.Transform.position = _dummySpawnPoint.position;
            dummy.Transform.eulerAngles = _dummySpawnPoint.eulerAngles;
            dummy.gameObject.SetActive(true);

            for (int i = 0; i < _pistolPickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _pistolPickupSpawnPoints[i];
                
                var pistolPickup = _objectPoolManager.GetPickupObject(ItemType.Pistol);
                pistolPickup.gameObject.SetActive(true);
                pistolPickup.transform.position = spawnPoint.position;
                pistolPickup.transform.eulerAngles = spawnPoint.eulerAngles;
            }
            
            for (int i = 0; i < _fireStonePickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _fireStonePickupSpawnPoints[i];
                
                var fireStonePickup = _objectPoolManager.GetPickupObject(ItemType.FireStone);
                fireStonePickup.gameObject.SetActive(true);
                fireStonePickup.transform.position = spawnPoint.position;
                fireStonePickup.transform.eulerAngles = spawnPoint.eulerAngles;
            }
            
            for (int i = 0; i < _waterStonePickupSpawnPoints.Length; i++)
            {
                var spawnPoint = _waterStonePickupSpawnPoints[i];
                
                var waterStonePickup = _objectPoolManager.GetPickupObject(ItemType.WaterStone);
                waterStonePickup.gameObject.SetActive(true);
                waterStonePickup.transform.position = spawnPoint.position;
                waterStonePickup.transform.eulerAngles = spawnPoint.eulerAngles;
            }
        }
    }
}

