using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
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

        public override void Setup(EnemyInfoWindowSetup setup)
        {
            _creatureController = setup.CreatureController;
            
            var alive = _creatureController.HP > 0;
            _aliveTab.SetActive(alive);
            _deadTab.SetActive(!alive);
            
            _aliveTab.Setup(this, setup);
            _deadTab.Setup(this, setup);
        }
    }
}

