using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Damage;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Creatures.Basic
{
    public class EnemyController : CreatureController
    {
        protected Dictionary<CreatureState, CreatureStateBase> _states;
        protected CreatureState _currentState;
        protected CreatureStateBase _currentStateBase;

        public override void OnSpawnFinish()
        {
            GenerateStatesDictionary();
            _spriteRenderer.color = Color.white;
            EnterState(CreatureState.Idle);
        }

        protected virtual void GenerateStatesDictionary()
        {
            _states = new Dictionary<CreatureState, CreatureStateBase>();
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

        public override void ApplyDamage(ref DamageStruct damageStruct)
        {
            if (_currentState == CreatureState.Dead)
                return;
            
            base.ApplyDamage(ref damageStruct);

            var hp = _values[CreatureValueType.HP];
            if (hp > 0)
                EnterState(CreatureState.Hit);
            else
                EnterState(CreatureState.Dead);
        }

        public void OnAnimationEvent()
        {
            _currentStateBase?.OnAnimationEvent();
        }
    }
}

