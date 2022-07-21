using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Configs;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SpriteConfig", menuName = "Configs/SpriteConfig")]
    public class SpriteConfig : ConfigBase<SpriteConfig>
    {
        [SerializeField] private InflictEffectSpriteItem[] _inflictEffectSpriteItems;

        private Dictionary<InflictEffectType, Sprite> _inflictEffectSpritesDict;

        public override void Init()
        {
            _inflictEffectSpritesDict = new Dictionary<InflictEffectType, Sprite>();
            foreach (var item in _inflictEffectSpriteItems)
                _inflictEffectSpritesDict[item.effectType] = item.effectSprite;
        }

        public Sprite GetInflictEffectSprite(InflictEffectType effectType)
        {
            return _inflictEffectSpritesDict[effectType];
        }
    }

    [Serializable]
    public struct InflictEffectSpriteItem
    {
        public InflictEffectType effectType;
        public Sprite effectSprite;
    }
}

