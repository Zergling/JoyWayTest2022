using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Damage;
using Game.Scripts.Utils;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Items
{
    public class PistolItemController : HandItemController
    {
        protected override void UsePerformed()
        {
            var cameraPosition = _playerController.CameraTransform.position;
            var cameraForward = _playerController.CameraTransform.forward;
            var ray = new Ray(cameraPosition, cameraForward);

            if (Physics.Raycast(ray, out var hitInfo, float.MaxValue))
            {
                var colliderTag = hitInfo.collider.gameObject.tag;
                if (!colliderTag.Equals(Tags.Creature))
                    return;
                
                var creatureController = hitInfo.collider.gameObject.GetComponent<CreatureController>();

                var damage = GetDamageStruct();
                
                creatureController.ApplyDamage(ref damage);
            }
        }
    }
}

