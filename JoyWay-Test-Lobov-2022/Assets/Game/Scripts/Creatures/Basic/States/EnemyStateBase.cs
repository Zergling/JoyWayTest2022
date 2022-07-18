using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class EnemyStateBase : CreatureStateBase
    {
        protected EnemyController _controller;
        
        public EnemyStateBase(EnemyController controller)
        {
            _controller = controller;
        }
    }
}

