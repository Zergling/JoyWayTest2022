using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public ItemId itemId;
        public DamageType damageType;
        public int damageValue;
        public int wetValue;
        public ProjectileType projetileType;
        public InflictEffectConfig inflictEffectConfig;
    }
}

