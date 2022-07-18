using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class EnemyController : CreatureController
    {
        protected Dictionary<CreatureStateType, EnemyStateBase> _states;
        protected CreatureStateType _currentStateType;
        protected EnemyStateBase _currentStateBase;

        public override void OnAwake()
        {
            base.OnAwake();
            GenerateStatesDictionary();
        }

        public override void OnStart()
        {
            EnterState(CreatureStateType.Idle);
        }

        protected virtual void GenerateStatesDictionary()
        {
            _states = new Dictionary<CreatureStateType, EnemyStateBase>();
        }

        public override void OnFixedUpdate()
        {
            _currentStateBase?.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            _currentStateBase?.OnUpdate();
        }

        public void EnterState(CreatureStateType state)
        {
            _currentStateBase?.Exit();
            _currentStateType = state;
            _currentStateBase = _states[_currentStateType];
            _currentStateBase.Enter();
        }

        public void OnAnimationEvent()
        {
            _currentStateBase?.OnAnimationEvent();
        }
    }
}

