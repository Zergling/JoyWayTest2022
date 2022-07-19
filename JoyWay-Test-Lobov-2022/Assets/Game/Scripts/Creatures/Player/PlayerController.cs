using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Creatures.Basic;
using Game.Scripts.DI;
using Game.Scripts.Enums;
using Game.Scripts.Items;
using Game.Scripts.ObjectPool;
using Game.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using ZerglingPlugins.Tools.Log;

namespace Game.Scripts.Creatures.Player
{
    public class PlayerController : CreatureController
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Transform _leftHandContainer;
        [SerializeField] private Transform _rightHandContainer;

        private HandItemController _leftHandItem;
        private HandItemController _rightHandItem;

        private Vector3 _moveInput;
        private Vector2 _lookInput;
        private Vector3 _currentRotation;
        
        private bool _isJumping;

        private GameSettingsConfig _gameSettingsConfig;
        private ObjectPoolManager _objectPoolManager;

        public override void OnSpawnFinish()
        {
            var diContainer = DIContainer.Instance;
            _gameSettingsConfig = diContainer.Resolve<GameSettingsConfig>();
            _objectPoolManager = diContainer.Resolve<ObjectPoolManager>();
            
            _currentRotation = new Vector3(_cameraTransform.localEulerAngles.x, _transform.localEulerAngles.y, 0);

            _leftHandItem = null;
            _rightHandItem = null;
        }

        public override void OnUpdate()
        {
            LookProcess();
            MoveProcess();
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

        public void OnInteractLeftHandInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            
            OnInteractProcess(false);
        }

        public void OnInteractRightHandInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            
            OnInteractProcess(true);
        }

        private void MoveProcess()
        {
            if (_moveInput.sqrMagnitude < 0.01f)
                return;

            var moveSpeed = Time.deltaTime * _config.moveSpeed;
            var moveVectorX = _transform.right * _moveInput.x * moveSpeed;
            var moveVectorZ = _transform.forward * _moveInput.z * moveSpeed;
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

            _transform.localEulerAngles = rootRotation;
            _cameraTransform.localEulerAngles = cameraRotation;
        }

        private void OnInteractProcess(bool rightHand)
        {
            var handItemController = rightHand ? _rightHandItem : _leftHandItem;
            
            if (handItemController == null)
                TryPickupItem(rightHand);
            else
                TryDropItem(rightHand);
        }

        private void TryPickupItem(bool rightHand)
        {
            var ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            if (Physics.Raycast(ray, out var hitInfo, _gameSettingsConfig.interactDistance, LayerMasks.PickupItemLayer))
            {
                var pickupItemController = hitInfo.collider.gameObject.GetComponent<PickupItemController>();
                var itemId = pickupItemController.ItemId;
                _objectPoolManager.ReturnPickupObject(pickupItemController);
                var handItemController = _objectPoolManager.GetHandItemObject(itemId);

                if (rightHand)
                {
                    handItemController.Transform.SetParent(_rightHandContainer);
                    _rightHandItem = handItemController;
                }
                else
                {
                    handItemController.Transform.SetParent(_leftHandContainer);
                    _leftHandItem = handItemController;
                }
                
                handItemController.Transform.localPosition = Vector3.zero;
                handItemController.Transform.localRotation = Quaternion.identity;
                handItemController.SetActive(true);
            }
        }

        private void TryDropItem(bool rightHand)
        {
            ItemId itemId = ItemId.NONE;
            
            if (rightHand)
            {
                itemId = _rightHandItem.ItemId;
                _objectPoolManager.ReturnHandItemObject(_rightHandItem);
                _rightHandItem = null;
            }
            else
            {
                itemId = _leftHandItem.ItemId;
                _objectPoolManager.ReturnHandItemObject(_leftHandItem);
                _leftHandItem = null;
            }

            var pickupItem = _objectPoolManager.GetPickupObject(itemId);
            var position = _transform.position + (_transform.forward * _gameSettingsConfig.dropDistance);
            pickupItem.Transform.position = position;
            pickupItem.Transform.LookAt(_transform);
            pickupItem.SetActive(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals(Tags.Ground))
                return;

            _isJumping = false;
        }
    }
}

