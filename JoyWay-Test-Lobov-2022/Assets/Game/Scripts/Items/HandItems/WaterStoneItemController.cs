using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Damage;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Items
{
    public class WaterStoneItemController : HandItemController
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _cameraForwardFactor;
        
        protected override void UsePerformed()
        {
            var cameraPosition = _playerController.CameraTransform.position;
            var cameraForward = _playerController.CameraTransform.forward;
            var hitPosition = cameraPosition + cameraForward * _cameraForwardFactor;
            var forceVector = hitPosition - _projectileSpawnPoint.position;

            var damage = GetDamageStruct();

            var projectile = _objectPoolManager.GetProjectileObject(_config.projetileType);
            projectile.Setup(damage);
            projectile.Transform.position = _projectileSpawnPoint.position;
            projectile.SetActive(true);
            projectile.AddForce(forceVector, ForceMode.Impulse);
        }
    }
}

