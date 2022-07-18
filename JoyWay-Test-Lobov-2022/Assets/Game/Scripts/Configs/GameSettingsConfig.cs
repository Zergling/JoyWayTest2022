using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettingsConfig")]
    public class GameSettingsConfig : ConfigBase<GameSettingsConfig>
    {
        public float mouseSensivity;
        public float interactDistance;
    }
}


