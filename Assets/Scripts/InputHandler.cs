using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class InputHandler : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls _inputActions;
        CameraHandler _cameraHandler;

        Vector2 _movemetInput;
        Vector2 _cameraInput;

        private void Awake()
        {
            _cameraHandler = CameraHandler.singleton;
        }

        private void FixedUpdate() // 이번에도 카메라 관련 움직임은 FixedUpdate에서 처리함
        {
            float delta = Time.deltaTime;

            if (_cameraHandler  != null)
            {
                _cameraHandler.FollowTarget(delta);
                _cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerControls();
                _inputActions.PlayerMovement.Movement.performed += movementInputActions => _movemetInput = movementInputActions.ReadValue<Vector2>();
                _inputActions.PlayerMovement.Camera.performed += cameraInputActions => _cameraInput = cameraInputActions.ReadValue<Vector2>();
            }

            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta); // 이런식으로 보호수준을 지켜서 사용하는구만! 상당히 캡슐적이야
        }

        private void MoveInput(float delta)
        {
            horizontal = _movemetInput.x;
            vertical = _movemetInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = _cameraInput.x;
            mouseY = _cameraInput.y;
        }
    }

}
