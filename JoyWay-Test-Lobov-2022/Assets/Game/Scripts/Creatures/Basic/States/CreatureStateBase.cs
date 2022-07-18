using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class CreatureStateBase
    {
        public virtual void Enter()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void OnAnimationEvent()
        {
        }
    }
}

