using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using Game.Scripts.InflictEffects;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Damage
{
    [Serializable]
    public struct DamageStruct
    {
        public DamageType DamageType;
        public int DamageValue;
        public int WetValue;
        public InflictEffectController? InflictEffect;
    }
}


