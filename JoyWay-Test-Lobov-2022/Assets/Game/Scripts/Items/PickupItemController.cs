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
        public ItemId ItemId => itemId;
        
        [SerializeField] private ItemId itemId;
    }
}

