using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyStateBase : CreatureStateBase
    {
        protected DummyController _controller;

        public DummyStateBase(DummyController controller)
        {
            _controller = controller;
        }
    }
}

