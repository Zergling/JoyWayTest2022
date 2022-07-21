using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Creatures.Player;
using UnityEngine;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.GameSystems;
using Game.Scripts.ObjectPool;
using Game.Scripts.UI.Windows.EnemyInfo;
using Game.Scripts.UI.Windows.HUD;
using ZerglingPlugins.Timers;
using ZerglingPlugins.Windows;

namespace Game.Scripts.Scenes
{
    public class GameplaySceneController : MonoBehaviour
    {
        [Header("BINDINGS")]
        [SerializeField] private ObjectPoolManager _objectPoolManager;
        [SerializeField] private WindowsManager _windowsManager;

        [Header("SPAWN POINTS")] 
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _dummySpawnPoint;
        [SerializeField] private Transform[] _pistolPickupSpawnPoints;
        [SerializeField] private Transform[] _fireStonePickupSpawnPoints;
        [SerializeField] private Transform[] _waterStonePickupSpawnPoints;

        private CreatureSystem _creatureSystem;
        private TimerManager _timerManager;
        private EventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.Subscribe<CreatureDiedEvent>(OnCreatureDiedEvent);
            _eventBus.Subscribe<DummyResetCallEvent>(OnDummyResetCallEvent);
        }

        private void OnDisable()
        {
            _eventBus.UnSubscribe<CreatureDiedEvent>(OnCreatureDiedEvent);
            _eventBus.UnSubscribe<DummyResetCallEvent>(OnDummyResetCallEvent);
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            InstallBindings();

            var diContainer = DIContainer.Instance;
            _creatureSystem = diContainer.Resolve<CreatureSystem>();
            _timerManager = diContainer.Resolve<TimerManager>();
            _eventBus = diContainer.Resolve<EventBus>();
        }

        private void Start()
        {
            _objectPoolManager.OnStart();

            SpawnPlayer();
            SpawnDummy();
            SpawnPickupItems();
            
            var enemyInfoWindowSetup = new EnemyInfoWindowSetup();
            enemyInfoWindowSetup.CreatureController = _creatureSystem.GetFirst(CreatureType.Dummy);
            _windowsManager.Open<EnemyInfoWindow, EnemyInfoWindowSetup>(enemyInfoWindowSetup);

            var hudWindowSetup = new HUDWindowSetup();
            hudWindowSetup.PlayerController = (PlayerController)_creatureSystem.GetFirst(CreatureType.Player);
            _windowsManager.Open<HUDWindow, HUDWindowSetup>(hudWindowSetup);
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
            _timerManager.OnLateUpdate();
        }

        private void InstallBindings()
        {
            var diContainer = DIContainer.Instance;
            
            diContainer.BindInstance(EventBus.Instance);
            
            // configs
            diContainer.BindInstance(CreatureConfigList.Instance);
            diContainer.BindInstance(GameSettingsConfig.Instance);
            diContainer.BindInstance(ItemConfigList.Instance);
            
            // object pools
            _objectPoolManager = Instantiate(_objectPoolManager);
            diContainer.BindInstance(_objectPoolManager);
            
            // game systems
            _windowsManager = Instantiate(_windowsManager);
            diContainer.BindInstance(_windowsManager);
            diContainer.BindInstance(CreatureSystem.Instance);
            diContainer.BindInstance(TimerManager.Instance);
        }

        private void SpawnPlayer()
        {
            var player = _objectPoolManager.GetCreatureObject(CreatureType.Player);
            player.Transform.position = _playerSpawnPoint.position;
            player.Transform.eulerAngles = _playerSpawnPoint.eulerAngles;
            player.Transform.SetParent(null);
            player.SetActive(true);
        }

        private void SpawnDummy()
        {
            var dummy = _objectPoolManager.GetCreatureObject(CreatureType.Dummy);
            dummy.Transform.position = _dummySpawnPoint.position;
            dummy.Transform.eulerAngles = _dummySpawnPoint.eulerAngles;
            dummy.Transform.SetParent(null);
            dummy.SetActive(true);
        }

        private void SpawnPickupItems()
        {
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

        private void OnCreatureDiedEvent(CreatureDiedEvent evnt)
        {
            var creatureController = evnt.CreatureController;
            _objectPoolManager.ReturnCreatureObject(creatureController);
        }
        
        private void OnDummyResetCallEvent(DummyResetCallEvent evnt)
        {
            var dummy = _creatureSystem.GetFirst(CreatureType.Dummy);
            if (dummy == null)
            {
                SpawnDummy();
                dummy = _creatureSystem.GetFirst(CreatureType.Dummy);

                var spawnedEvent = new CreatureSpawnedEvent(dummy);
                _eventBus.Fire(spawnedEvent);
            }
            else
            {
                dummy.CheatHeal();
            }
        }
    }
}

