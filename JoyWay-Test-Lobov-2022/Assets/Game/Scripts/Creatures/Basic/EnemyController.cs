using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class EnemyController : CreatureController
    {
        protected Dictionary<CreatureState, EnemyStateBase> _states;
        protected CreatureState _currentState;
        protected EnemyStateBase _currentStateBase;

        public override void OnSpawnFinish()
        {
            GenerateStatesDictionary();
            EnterState(CreatureState.Idle);
        }

        protected virtual void GenerateStatesDictionary()
        {
            _states = new Dictionary<CreatureState, EnemyStateBase>();
        }

        public override void OnFixedUpdate()
        {
            _currentStateBase?.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            _currentStateBase?.OnUpdate();
        }

        public void EnterState(CreatureState state)
        {
            _currentStateBase?.Exit();
            _currentState = state;
            _currentStateBase = _states[_currentState];
            _currentStateBase.Enter();
        }

        public void OnAnimationEvent()
        {
            _currentStateBase?.OnAnimationEvent();
        }
    }
}

