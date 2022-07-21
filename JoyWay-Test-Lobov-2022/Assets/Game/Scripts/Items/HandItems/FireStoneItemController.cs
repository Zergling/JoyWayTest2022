using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Player;
using Game.Scripts.Damage;
using Game.Scripts.Events;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Items
{
    public class FireStoneItemController : HandItemController
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private int _instanceId;

        private void OnEnable()
        {
            _eventBus.Subscribe<CreatureParticleCollisionEvent>(OnCreatureParticleCollisionEvent);
        }

        private void OnDisable()
        {
            _eventBus.UnSubscribe<CreatureParticleCollisionEvent>(OnCreatureParticleCollisionEvent);
        }

        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            _instanceId = gameObject.GetInstanceID();
        }

        protected override void UsePerformed()
        {
            var emission = _particleSystem.emission;
            emission.enabled = true;
            _particleSystem.Play();
        }

        protected override void UseCanceled()
        {
            var emission = _particleSystem.emission;
            emission.enabled = false;
        }

        public void OnCreatureParticleCollisionEvent(CreatureParticleCollisionEvent evnt)
        {
            var instanceId = evnt.FromInstanceId;
            if (_instanceId != instanceId)
                return;

            var damage = new DamageStruct();
            damage.DamageType = _config.damageType;
            damage.DamageValue = _config.damageValue;
            damage.WetValue = _config.wetValue;

            var creatureController = evnt.ThisCreatureController;
            creatureController.ApplyDamage(damage);
        }
    }
}


