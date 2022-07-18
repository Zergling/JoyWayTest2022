using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Items
{
    public class PickupItemController : MonoBehaviour
    {
        public ItemType ItemType => _itemType;
        
        [SerializeField] private ItemType _itemType;
    }
}

