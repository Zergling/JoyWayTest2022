using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using UnityEngine;

namespace Game.Scripts.Items
{
    public class PickupItemController : BasicMonoBehaviour
    {
        public ItemType ItemType => _itemType;
        
        [SerializeField] private ItemType _itemType;
    }
}

