using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enums
{
    public enum CreatureType
    {
        NONE = 0,
        Player = 1,
        Dummy = 2,
    }
    
    public enum CreatureStateType
    {
        NONE,
        Idle,
        Hit,
        Dead,
    }
}
