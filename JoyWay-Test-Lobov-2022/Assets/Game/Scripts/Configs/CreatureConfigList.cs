using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "CreatureConfigList", menuName = "Configs/CreatureConfigList")]
    public class CreatureConfigList : ConfigBase<CreatureConfigList>
    {
        [SerializeField] private CreatureConfig[] _configs;

        public CreatureConfig GetConfig(CreatureType creatureType)
        {
            CreatureConfig result = null;

            for (int i = 0; i < _configs.Length; i++)
            {
                var config = _configs[i];
                if (config.creatureType == creatureType)
                {
                    result = config;
                    break;
                }
            }

            return result;;
        }
    }
}

