using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Damage;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.MonoBehaviours;
using Game.Scripts.ObjectPool;
using Game.Scripts.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Projectiles
{
    public class ProjectileController : BasicMonoBehaviour, IObjectPoolItem
    {
        public ProjectileType ProjectileType => _projectileType;
        
        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private Rigidbody _rigidbody;
        
        private DamageStruct _damageStruct;
        
        private ObjectPoolManager _objectPoolManager;

        public void Setup(DamageStruct damageStruct)
        {
            _damageStruct = damageStruct;
        }

        public void OnSpawn()
        {
            var diContainer = DIContainer.Instance;
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
        }

        public void OnDespawn()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            _rigidbody.AddForce(force, forceMode);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _objectPoolManager.ReturnProjectileObject(this);
            if (!collision.gameObject.tag.Equals(Tags.Creature))
                return;

            var controller = collision.collider.gameObject.GetComponent<CreatureController>();
            controller.ApplyDamage(ref _damageStruct);
        }
    }
}

