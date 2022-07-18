using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _moveInput;
        private Vector2 _lookInput;
        private Vector3 _currentRotation;
        
        private bool _isJumping;

        private PlayerConfig _playerConfig;

        private void Start()
        {
            _playerConfig = PlayerConfig.Instance;
            _currentRotation = new Vector3(_cameraTransform.localEulerAngles.x, _rootTransform.localEulerAngles.y, 0);
        }

        public void Update()
        {
            LookProcess();
            MoveProcess();
        }

        private void MoveProcess()
        {
            if (_moveInput.sqrMagnitude < 0.01f)
                return;

            var moveSpeed = Time.deltaTime * _playerConfig.moveSpeed;
            var moveVectorX = _rootTransform.right * _moveInput.x * moveSpeed;
            var moveVectorZ = _rootTransform.forward * _moveInput.z * moveSpeed;
            var moveVector = moveVectorX + moveVectorZ;
            _rigidbody.position += moveVector;
        }

        private void LookProcess()
        {
            if (_lookInput.sqrMagnitude < 0.01f)
                return;

            var rotationSpeed = Time.deltaTime * _playerConfig.mouseSensivity;
            
            var cameraRotationX = _lookInput.y * rotationSpeed;
            var rootRotationY = _lookInput.x * rotationSpeed;

            _currentRotation.x = Mathf.Clamp(_currentRotation.x - cameraRotationX, -89, 89);
            _currentRotation.y += rootRotationY;

            var rootRotation = new Vector3(0, _currentRotation.y, 0);
            var cameraRotation = new Vector3(_currentRotation.x, 0, 0);

            _rootTransform.localEulerAngles = rootRotation;
            _cameraTransform.localEulerAngles = cameraRotation;
        }

        public void OnMoveInputAction(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            _moveInput = new Vector3(input.x, 0, input.y);
        }

        public void OnLookInputAction(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        public void OnJumpInputAction(InputAction.CallbackContext context)
        {
            if (_isJumping)
                return;

            if (!context.performed)
                return;

            _isJumping = true;
            _rigidbody.AddForce(_playerConfig.jumpForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals(Tags.Ground))
                return;

            _isJumping = false;
        }
    }
}

