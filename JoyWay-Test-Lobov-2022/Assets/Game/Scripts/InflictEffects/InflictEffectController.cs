using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Damage;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.InflictEffects
{
    public struct InflictEffectController
    {
        public InflictEffectType EffectType => _config.effectType;
        public InflictEffectConfigApplyDamageItem[] DamageStructs => _config.damageItems;
        public List<InflictEffectType> RemoveEffects => _config.removeEffects;

        public bool WithTimer => _config.timerItem.withTimer;
        public float TimerDuration => _config.timerItem.duration; 
        
        public Color Color => _config.creatureSpriteColor;

        private InflictEffectConfig _config;

        public InflictEffectController(InflictEffectConfig config)
        {
            _config = config;
        }

        public bool CheckApply(CreatureController creatureController)
        {
            var result = false;
            
            var checkApplyItems = _config.checkApplyItems;
            for (int i = 0; i < checkApplyItems.Length; i++)
            {
                var checkApplyItem = checkApplyItems[i];
                var creatureValue = creatureController.GetCurrentValue(checkApplyItem.checkValueType);

                if (checkApplyItem.checkAbove)
                    result = result || (creatureValue > checkApplyItem.aboveValue);

                if (checkApplyItem.checkEquals)
                    result = result || (creatureValue == checkApplyItem.equalsValue);

                if (checkApplyItem.checkBelow)
                    result = result || (creatureValue < checkApplyItem.belowValue);
            }

            return result;
        }

        public void ApplyDamageResist(ref DamageStruct damageStruct)
        {
            var resistItems = _config.resistItems;
            for (int i = 0; i < resistItems.Length; i++)
            {
                var resistItem = resistItems[i];
                if (damageStruct.DamageType == resistItem.damageType)
                    damageStruct.DamageValue -= resistItem.resistValue;

                if (damageStruct.DamageValue < 0)
                    damageStruct.DamageValue = 0;
            }
        }
    }
}


