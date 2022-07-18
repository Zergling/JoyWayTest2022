using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    public class CreatureConfig : ConfigBase<CreatureConfig>
    {
        public CreatureType creatureType;
        public float moveSpeed;
        public Vector3 jumpForce;
        public int maxHP;
    }
}

