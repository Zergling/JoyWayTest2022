using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using UnityEngine;

namespace Game.Scripts.Animations
{
    public class EnemyAnimatorController : MonoAnimatorController
    {
        [SerializeField] private EnemyController _enemyController;

        public override void OnAnimationEvent()
        {
            _enemyController.OnAnimationEvent();
        }
    }
}

