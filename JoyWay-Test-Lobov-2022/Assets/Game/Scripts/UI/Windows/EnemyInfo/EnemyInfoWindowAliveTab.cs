using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
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
        
        public override void Setup(EnemyInfoWindow window, EnemyInfoWindowSetup setup)
        {
            base.Setup(window, setup);

            _creatureController = setup.CreatureController;
            
            var diContainer = DIContainer.Instance;
            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();
            
            UpdateHPBar();
            UpdateWetBar();
        }
        
        private void UpdateHPBar()
        {
            var hp = _creatureController.HP;
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
    }
}

