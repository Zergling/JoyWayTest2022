using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Damage;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Items
{
    public class PistolItemController : HandItemController
    {
        protected override void UsePerformed()
        {
            var cameraPosition = _playerController.CameraTransform.position;
            var cameraForward = _playerController.CameraTransform.forward;
            var ray = new Ray(cameraPosition, cameraForward);

            if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, LayerMasks.CreaturesLayer))
            {
                var creatureController = hitInfo.collider.gameObject.GetComponent<CreatureController>();
                var damage = new DamageStruct();
                damage.Value = _config.damageValue;
                damage.DamageType = _config.damageType;
                creatureController.ApplyDamage(damage);
            }
        }
    }
}

