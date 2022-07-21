using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.UI.Elements;
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

        [SerializeField] private InflictEffectControllerWidget[] _inflictEffectWidgets;
        
        private CreatureController _creatureController;
        
        private GameSettingsConfig _gameSettingsConfig;

        public override void SubscribeToEvents()
        {
            var eventBus = _window.EventBus;
            eventBus.Subscribe<CreatureValuesChangedEvent>(OnCreatureValuesChangedEvent);
            eventBus.Subscribe<CreatureSpawnedEvent>(OnCreatureSpawnedEvent);
            eventBus.Subscribe<CreatureInflictEffectsChangedEvent>(OnCreatureInflictEffectsChangedEvent);
        }

        public override void UnSubscribeToEvents()
        {
            var eventBus = _window.EventBus;
            eventBus.UnSubscribe<CreatureValuesChangedEvent>(OnCreatureValuesChangedEvent);
            eventBus.UnSubscribe<CreatureSpawnedEvent>(OnCreatureSpawnedEvent);
            eventBus.UnSubscribe<CreatureInflictEffectsChangedEvent>(OnCreatureInflictEffectsChangedEvent);
        }

        public override void Setup(EnemyInfoWindowSetup setup)
        {
            base.Setup(setup);

            _creatureController = setup.CreatureController;
            
            var diContainer = DIContainer.Instance;
            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();

            for (int i = 0; i < _inflictEffectWidgets.Length; i++)
            {
                var widget = _inflictEffectWidgets[i];
                widget.Init();
                widget.SetActive(false);
            }

            UpdateHPBar();
            UpdateWetBar();
            UpdateInflictEffectWidgets();
        }
        
        private void UpdateHPBar()
        {
            var hp = _creatureController.GetCurrentValue(CreatureValueType.HP);
            var maxHP = _creatureController.MaxHP;
            
            _hpText.text = $"{hp}/{maxHP}";

            var fillAmount = (float)hp / maxHP;
            _hpBarFiller.fillAmount = fillAmount;
        }

        private void UpdateWetBar()
        {
            var wetValue = _creatureController.GetCurrentValue(CreatureValueType.Wet);
            var wetMax = _gameSettingsConfig.wetMaxValue;

            _wetText.text = $"{wetValue}/{wetMax}";

            var fillAmount = (float)wetValue / wetMax;
            _wetBarFiller.fillAmount = fillAmount;
        }

        private void UpdateInflictEffectWidgets()
        {
            var inflictEffects = _creatureController.EffectControllers;
            var creatureInstanceId = _creatureController.gameObject.GetInstanceID();
            for (int i = 0; i < _inflictEffectWidgets.Length; i++)
            {
                var widget = _inflictEffectWidgets[i];
                widget.Reset();
                widget.SetActive(false);

                if (i >= inflictEffects.Count)
                    continue;

                var effectController = inflictEffects[i];
                
                widget.SetActive(true);
                widget.Setup(creatureInstanceId, effectController);
            }
        }

        private void OnCreatureValuesChangedEvent(CreatureValuesChangedEvent evnt)
        {
            var creature = evnt.CreatureController;
            if (_creatureController != creature)
                return;
            
            UpdateHPBar();
            UpdateWetBar();
        }

        private void OnCreatureSpawnedEvent(CreatureSpawnedEvent evnt)
        {
            _creatureController = evnt.CreatureController;
            UpdateHPBar();
            UpdateWetBar();
            UpdateInflictEffectWidgets();
        }
        
        private void OnCreatureInflictEffectsChangedEvent(CreatureInflictEffectsChangedEvent evnt)
        {
            UpdateInflictEffectWidgets();
        }
    }
}

