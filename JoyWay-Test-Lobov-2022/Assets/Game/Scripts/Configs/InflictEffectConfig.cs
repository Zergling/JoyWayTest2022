using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Damage;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "InflictEffectConfig", menuName = "Configs/InflictEffectConfig")]
    public class InflictEffectConfig : ScriptableObject
    {
        public InflictEffectType effectType;
        public Color creatureSpriteColor;
        public InflictEffectConfigTimerItem timerItem;
        public InflictEffectConfigCheckApplyItem[] checkApplyItems;
        public DamageStruct[] damageItems;
        public InflictEffectConfigApplyDamageResistItem[] resistItems;
        public InflictEffectType[] removeEffects;
    }

    [Serializable]
    public struct InflictEffectConfigTimerItem
    {
        public bool withTimer;
        public float duration;
    }

    [Serializable]
    public struct InflictEffectConfigCheckApplyItem
    {
        public CreatureValueType checkValueType;
        
        public bool checkBelow;
        public int belowValue;
        
        public bool checkEquals;
        public int equalsValue;
        
        public bool checkAbove;
        public int aboveValue;
    }

    [Serializable]
    public struct InflictEffectConfigApplyDamageResistItem
    {
        public DamageType damageType;
        public int resistValue;
    }
}

