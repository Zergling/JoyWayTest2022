using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "CreatureConfig", menuName = "Configs/CreatureConfig")]
    public class CreatureConfig : ScriptableObject
    {
        public CreatureType creatureType;
        public float moveSpeed;
        public Vector3 jumpForce;
        public int maxHP;
    }
}

