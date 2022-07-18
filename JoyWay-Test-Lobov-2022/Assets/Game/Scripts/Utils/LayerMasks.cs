using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class LayerMasks
    {
        public static int PickupItemLayer = 1 << LayerMask.NameToLayer("PickupItem");
    }
}

