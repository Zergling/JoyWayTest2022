using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ObjectPool
{
    public interface IObjectPoolItem
    {
        void OnSpawn();
        void OnDespawn();
    }
}

