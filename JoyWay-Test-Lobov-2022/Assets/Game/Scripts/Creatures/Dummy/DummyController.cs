using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Animations;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Events;
using Game.Scripts.InflictEffects;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

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
            _states[CreatureState.Idle] = new DummyStateIdle(this);
            _states[CreatureState.Hit] = new DummyStateHit(this);
            _states[CreatureState.Dead] = new DummyStateDead(this);
            _states[CreatureState.ReturnToPool] = new DummyStateReturnToPool(this);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            var playerPosition = _playerController.Transform.position;
            var lookAtPoisition = new Vector3(playerPosition.x, 0, playerPosition.z);
            _transform.LookAt(lookAtPoisition);
        }

        private void OnParticleCollision(GameObject other)
        {
            var instanceId = other.GetInstanceID();
            var evnt = new CreatureParticleCollisionEvent(this, instanceId);
            _eventBus.Fire(evnt);
        }
    }
}

