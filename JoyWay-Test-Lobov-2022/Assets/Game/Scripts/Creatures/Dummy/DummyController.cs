using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Animations;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Creatures.Player;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Creatures.Dummy
{
    public class DummyController : EnemyController
    {
        public EnemyAnimatorController AnimatorController => _animatorController;
        
        [SerializeField] private EnemyAnimatorController _animatorController;

        private CreatureController _playerController;

        public override void OnSpawnFinish()
        {
            base.OnSpawnFinish();
            _playerController = _creatureSystem.GetFirst(CreatureType.Player);
        }

        protected override void GenerateStatesDictionary()
        {
            base.GenerateStatesDictionary();
            _states[CreatureStateType.Idle] = new DummyStateIdle(this);
            _states[CreatureStateType.Hit] = new DummyStateHit(this);
            _states[CreatureStateType.Dead] = new DummyStateDead(this);
            _states[CreatureStateType.Dead] = new DummyStateDead(this);
            _states[CreatureStateType.ReturnToPool] = new DummyStateReturnToPool(this);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            var playerPosition = _playerController.Transform.position;
            var lookAtPoisition = new Vector3(playerPosition.x, 0, playerPosition.z);
            _transform.LookAt(lookAtPoisition);
        }
    }
}

