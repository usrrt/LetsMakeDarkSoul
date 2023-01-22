using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    /*
    PlayerManager가 하는 역할
    1. 업데이트 메소드를 다룸
    2. Flags를 다룸(isSprinting, isFalling, isInteracting 기타 등등)
    3. 플레이어 관련 기능(other scripts)들의 연결점(일종의 hub역할)
     */
    public class PlayerManager : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        InputHandler _inputHandler;
        Animator _anim;
        CameraHandler _cameraHandler;
        PlayerLocomotion _locomotion;

        // GetBool로 상태를 가져온뒤 조건문으로 사용함
        public bool isInteracting;
        public bool canDoCombo;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        private void Awake()
        {
            _cameraHandler = CameraHandler.singleton;
        }

        void Start()
        {
            _locomotion = GetComponent<PlayerLocomotion>();
            _inputHandler = GetComponent<InputHandler>();
            _anim = GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            // 카메라 관련 움직임은 FixedUpdate에서 처리함
            float delta = Time.deltaTime;

            if (_cameraHandler != null)
            {
                _cameraHandler.FollowTarget(delta);
                _cameraHandler.HandleCameraRotation(delta, _inputHandler.mouseX, _inputHandler.mouseY);
            }
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = _anim.GetBool("isInteracting");
            canDoCombo = _anim.GetBool("canDoCombo");

            _inputHandler.TickInput(delta);
            _locomotion.HandleMovement(delta);
            _locomotion.HandleRollingAndSprinting(delta);
            _locomotion.HandleFalling(delta, _locomotion.moveDirection);
        }

        private void LateUpdate()
        {
            // update보다 느리게 실행되는 lateUpdate를 통해 flag나 입력키 상태를 false로 바꿔주면 여러번 입력되는것을 막을수있다
            _inputHandler.rollFlag = false;
            _inputHandler.sprintFlag = false;
            _inputHandler.rb_Input = false;
            _inputHandler.rt_Input = false;
            _inputHandler.d_Pad_Up = false;
            _inputHandler.d_Pad_Down = false;
            _inputHandler.d_Pad_Right = false;
            _inputHandler.d_Pad_Left = false;

            if (isInAir)
            {
                _locomotion.inAirTimer = _locomotion.inAirTimer + Time.deltaTime;
            }
        }
    }

}
