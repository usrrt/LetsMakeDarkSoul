using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    /*
    PlayerManager�� �ϴ� ����
    1. ������Ʈ �޼ҵ带 �ٷ�
    2. Flags�� �ٷ�(isSprinting, isFalling, isInteracting ��Ÿ ���)
    3. �÷��̾� ���� ���(other scripts)���� ������(������ hub����)
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

        // GetBool�� ���¸� �����µ� ���ǹ����� �����
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
            // ī�޶� ���� �������� FixedUpdate���� ó����
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
            // update���� ������ ����Ǵ� lateUpdate�� ���� flag�� �Է�Ű ���¸� false�� �ٲ��ָ� ������ �ԷµǴ°��� �������ִ�
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
