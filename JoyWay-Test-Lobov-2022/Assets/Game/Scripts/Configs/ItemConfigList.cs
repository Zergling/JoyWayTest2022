using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ItemConfigList", menuName = "Configs/ItemConfigList")]
    public class ItemConfigList : ConfigBase<ItemConfigList>
    {
        [SerializeField] private ItemConfig[] _configs;

        public ItemConfig GetConfig(ItemId itemId)
        {
            ItemConfig result = null;

            for (int i = 0; i < _configs.Length; i++)
            {
                var config = _configs[i];
                if (config.itemId == itemId)
                {
                    result = config;
                    break;
                }
            }

            return result;;
        }
    }
}

