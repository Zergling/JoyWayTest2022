using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Damage
{
    [Serializable]
    public struct DamageStruct
    {
        public DamageType DamageType;
        public int DamageValue;
        public int WetValue;
    }
}


