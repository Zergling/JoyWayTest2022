using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Animations
{
    public class MonoAnimatorController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        protected int _currentStateHash;

        public void PlayAmination(int stateHash)
        {
            _currentStateHash = stateHash;
            _animator.enabled = true;
            _animator.Play(_currentStateHash);
        }

        public void StopAnimation()
        {
            _animator.enabled = false;
        }

        public virtual void OnAnimationEvent()
        {
        }
    }
}

