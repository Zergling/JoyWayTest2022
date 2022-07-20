using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Enums;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyStateHit : DummyStateBase
    {
        public DummyStateHit(DummyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _controller.AnimatorController.PlayAnimation(AnimationHash.Hit);
        }

        public override void OnAnimationEvent()
        {
            _controller.EnterState(CreatureState.Idle);
        }
    }
}

