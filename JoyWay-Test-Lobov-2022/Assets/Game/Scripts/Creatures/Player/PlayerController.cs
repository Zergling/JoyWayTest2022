using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Creatures.Player
{
    public class PlayerController : CreatureController
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _moveInput;
        private Vector2 _lookInput;
        private Vector3 _currentRotation;
        
        private bool _isJumping;

        private GameSettingsConfig _gameSettingsConfig;

        public override void OnAwake()
        {
            _gameSettingsConfig = GameSettingsConfig.Instance;
        }

        public override void OnStart()
        {
            base.OnStart();
            _currentRotation = new Vector3(_cameraTransform.localEulerAngles.x, _rootTransform.localEulerAngles.y, 0);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            LookProcess();
            MoveProcess();
        }

        private void MoveProcess()
        {
            if (_moveInput.sqrMagnitude < 0.01f)
                return;

            var moveSpeed = Time.deltaTime * _config.moveSpeed;
            var moveVectorX = _rootTransform.right * _moveInput.x * moveSpeed;
            var moveVectorZ = _rootTransform.forward * _moveInput.z * moveSpeed;
            var moveVector = moveVectorX + moveVectorZ;
            _rigidbody.position += moveVector;
        }

        private void LookProcess()
        {
            if (_lookInput.sqrMagnitude < 0.01f)
                return;

            var rotationSpeed = Time.deltaTime * _gameSettingsConfig.mouseSensivity;
            
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
            _rigidbody.AddForce(_config.jumpForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals(Tags.Ground))
                return;

            _isJumping = false;
        }
    }
}

