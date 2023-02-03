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
        InteractableUI _interactableUI;

        public GameObject interactableUIGameObject;
        public GameObject itemPopUpGameObject;

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
            _interactableUI = FindObjectOfType<InteractableUI>();

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
            _anim.SetBool("isInAir", isInAir);

            _inputHandler.TickInput(delta);
            _locomotion.HandleMovement(delta);
            _locomotion.HandleRollingAndSprinting(delta);
            _locomotion.HandleFalling(delta, _locomotion.moveDirection);
            // _locomotion.HandleJumping(); ���� : ������� �ִϸ��̼� ��������� ��Ʈ����� ���� ��, �����ص� ĳ���Ͱ� �������� ����. rigidBody�� �̿��غ����� ����� ���������� ���� ���� ������ ����

            CheckForInteractableObject();
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
            _inputHandler.e_Input = false;
            _inputHandler.jump_Input = false;

            if (isInAir)
            {
                _locomotion.inAirTimer = _locomotion.inAirTimer + Time.deltaTime;
            }

        }

        // ���Ӿ��� Ȯ���ؾ��ϹǷ� Update���� ȣ�����ش�
        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, _cameraHandler.ignoreLayers))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        string interactableText = interactable.interactableText;
                        // TODO : UI text�� ��ȣ�ۿ� text�� �ٲٱ�
                        _interactableUI.interactableText.text = interactableText;
                        // TODO : TEXT POP UP TRUE
                        interactableUIGameObject.SetActive(true);

                        if (_inputHandler.e_Input)
                        {
                            // ��ư�� ������ ���� hit�ϰ��ִ�(this)��ü�� interactable�� ��ȣ�ۿ�
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemPopUpGameObject != null && _inputHandler.e_Input)
                {
                    itemPopUpGameObject.SetActive(false);
                }
            }
        }
    }

}
