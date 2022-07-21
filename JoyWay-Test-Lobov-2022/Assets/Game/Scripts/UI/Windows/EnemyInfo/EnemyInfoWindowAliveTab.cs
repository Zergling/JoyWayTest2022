using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Windows.EnemyInfo
{
    public class EnemyInfoWindowAliveTab : EnemyInfoWindowTab
    {
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private Image _hpBarFiller;

        [SerializeField] private TMP_Text _wetText;
        [SerializeField] private Image _wetBarFiller;
        
        private CreatureController _creatureController;
        
        private GameSettingsConfig _gameSettingsConfig;

        public override void SubscribeToEvents()
        {
            var eventBus = _window.EventBus;
            eventBus.Subscribe<CreatureValuesChangedEvent>(OnCreatureValuesChangedEvent);
        }

        public override void UnSubscribeToEvents()
        {
            var eventBus = _window.EventBus;
            eventBus.UnSubscribe<CreatureValuesChangedEvent>(OnCreatureValuesChangedEvent);
        }

        public override void Setup(EnemyInfoWindowSetup setup)
        {
            base.Setup(setup);

            _creatureController = setup.CreatureController;
            
            var diContainer = DIContainer.Instance;
            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();
            
            UpdateHPBar();
            UpdateWetBar();
        }
        
        private void UpdateHPBar()
        {
            var hp = _creatureController.HPValue;
            var maxHP = _creatureController.MaxHP;
            
            _hpText.text = $"{hp}/{maxHP}";

            var fillAmount = (float)hp / maxHP;
            _hpBarFiller.fillAmount = fillAmount;
        }

        private void UpdateWetBar()
        {
            var wetValue = _creatureController.WetValue;
            var wetMax = _gameSettingsConfig.wetMaxValue;

            _wetText.text = $"{wetValue}/{wetMax}";

            var fillAmount = (float)wetValue / wetMax;
            _wetBarFiller.fillAmount = fillAmount;
        }

        private void OnCreatureValuesChangedEvent(CreatureValuesChangedEvent evnt)
        {
            UpdateHPBar();
            UpdateWetBar();
        }
    }
}

