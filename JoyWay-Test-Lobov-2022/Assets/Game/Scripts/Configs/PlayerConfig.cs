using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ConfigBase<PlayerConfig>
    {
        public float moveSpeed;
        public float mouseSensivity;
        public Vector3 jumpForce;
    }
}

