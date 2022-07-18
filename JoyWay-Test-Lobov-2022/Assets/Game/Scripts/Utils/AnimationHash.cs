using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class AnimationHash
    {
        public static int Idle = Animator.StringToHash("Idle");
        public static int Hit = Animator.StringToHash("Hit");
        public static int Dead = Animator.StringToHash("Dead");
    }
}

