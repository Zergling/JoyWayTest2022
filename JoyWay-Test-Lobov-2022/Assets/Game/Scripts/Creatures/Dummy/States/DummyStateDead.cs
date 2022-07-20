using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyStateDead : DummyStateBase
    {
        public DummyStateDead(DummyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _controller.AnimatorController.PlayAnimation(AnimationHash.Dead);
        }

        public override void OnAnimationEvent()
        {
            
        }
    }
}

