using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.MonoBehaviours
{
    public class BasicMonoBehaviour : MonoBehaviour
    {
        public Transform Transform => _transform;

        [SerializeField] protected Transform _transform;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnUpdate()
        {
        }
    }
}

