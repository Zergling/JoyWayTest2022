using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Enums;
using UnityEngine;
using ZerglingPlugins.Tools.Log;
using ZerglingPlugins.Tools.Singleton;

namespace Game.Scripts.GameSystems
{
    public class CreatureSystem : Singleton<CreatureSystem>
    {
        private List<CreatureController> _creaturesList;
        private Dictionary<CreatureType, List<CreatureController>> _creaturesDict;

        public CreatureSystem()
        {
            _creaturesList = new List<CreatureController>();
            _creaturesDict = new Dictionary<CreatureType, List<CreatureController>>();
        }
        
        public void OnFixedUpdate()
        {
            for (int i = 0; i < _creaturesList.Count; i++)
                _creaturesList[i].OnFixedUpdate();
        }

        public void OnUpdate()
        {
            for (int i = 0; i < _creaturesList.Count; i++)
                _creaturesList[i].OnUpdate();
        }

        public void SubscribeCreature(CreatureController creatureController)
        {
            _creaturesList.Add(creatureController);

            var creatureType = creatureController.CreatureType;
            if (!_creaturesDict.ContainsKey(creatureType))
                _creaturesDict[creatureType] = new List<CreatureController>();
            
            _creaturesDict[creatureType].Add(creatureController);
        }

        public void UnSusbscribeCreature(CreatureController creatureController)
        {
            var index = _creaturesList.IndexOf(creatureController);
            _creaturesList.RemoveAt(index);

            var creatureType = creatureController.CreatureType;
            var list = _creaturesDict[creatureType];
            index = list.IndexOf(creatureController);
            list.RemoveAt(index);
        }

        public CreatureController GetFirst(CreatureType creatureType)
        {
            if (!_creaturesDict.ContainsKey(creatureType))
            {
                LogUtils.Error(this, $"Has no creatures of type {creatureType}");
                return null;
            }

            var list = _creaturesDict[creatureType];
            var result = list[0];
            return result;
        }
    }
}

