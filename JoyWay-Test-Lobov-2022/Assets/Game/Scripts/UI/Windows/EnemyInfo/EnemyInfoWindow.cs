using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.UI.Windows.Basic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZerglingPlugins.Windows;

namespace Game.Scripts.UI.Windows.EnemyInfo
{
    public class EnemyInfoWindow : WindowBasic<EnemyInfoWindowSetup>
    {
        [SerializeField] private EnemyInfoWindowTab _aliveTab;
        [SerializeField] private EnemyInfoWindowTab _deadTab;

        private CreatureController _creatureController;

        protected override void Awake()
        {
            base.Awake();
            _aliveTab.OnAwake(this);
            _deadTab.OnAwake(this);
        }

        private void OnEnable()
        {
            _aliveTab.SubscribeToEvents();
            _deadTab.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _aliveTab.SubscribeToEvents();
            _deadTab.SubscribeToEvents();
        }

        public override void Setup(EnemyInfoWindowSetup setup)
        {
            _creatureController = setup.CreatureController;

            var hp = _creatureController.GetCurrentValue(CreatureValueType.HP);
            var alive = hp > 0;
            _aliveTab.SetActive(alive);
            _deadTab.SetActive(!alive);
            
            _aliveTab.Setup(setup);
            _deadTab.Setup(setup);
        }
    }
}

