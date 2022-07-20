using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyStateIdle : DummyStateBase
    {
        public DummyStateIdle(DummyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _controller.AnimatorController.PlayAnimation(AnimationHash.Idle);
        }
    }
}

