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

            var player = _objectPoolManager.GetCreatureObject(CreatureType.Player);
            player.Transform.position = _playerSpawnPoint.position;

            var dummy = _objectPoolManager.GetCreatureObject(CreatureType.Dummy);
            dummy.Transform.position = _dummySpawnPoint.position;
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
    }
}

