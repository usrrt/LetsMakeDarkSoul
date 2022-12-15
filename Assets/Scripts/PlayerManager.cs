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

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;

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

            _inputHandler.TickInput(delta);
            _locomotion.HandleMovement(delta);
            _locomotion.HandleRollingAndSprinting(delta);
        }

        private void LateUpdate()
        {
            _inputHandler.rollFlag = false;
            _inputHandler.sprintFlag = false;
            isSprinting = _inputHandler.b_Input;
        }
    }

}
