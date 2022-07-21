using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Events;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyStateDead : DummyStateBase
    {
        private EventBus _eventBus;
        
        public DummyStateDead(DummyController controller) : base(controller)
        {
            var diContainer = DIContainer.Instance;
            _eventBus = diContainer.Resolve<EventBus>();
        }

        public override void Enter()
        {
            _controller.AnimatorController.PlayAnimation(AnimationHash.Dead);
        }

        public override void OnAnimationEvent()
        {
            var evnt = new CreatureDiedEvent(_controller);
            _eventBus.Fire(evnt);
        }
    }
}

