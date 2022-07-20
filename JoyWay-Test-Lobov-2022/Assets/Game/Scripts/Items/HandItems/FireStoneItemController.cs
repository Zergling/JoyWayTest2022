using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Player;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Items
{
    public class FireStoneItemController : HandItemController
    {
        [SerializeField] private ParticleSystem _particleSystem;

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
    }
}


