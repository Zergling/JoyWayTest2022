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

    public enum CreatureValueType
    {
        NONE = 0,
        HP = 1,
        Wet = 2,
    }

    public enum CreatureState
    {
        NONE,
        Idle,
        Hit,
        Dead,
        ReturnToPool,
    }
}
