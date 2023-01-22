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

        public bool b_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        PlayerControls _inputActions;
        PlayerAttacker _playerAttacker;
        PlayerInventory _playerInventory;
        PlayerManager _playerManager;

        Vector2 _movemetInput;
        Vector2 _cameraInput;

        private void Awake()
        {
            _playerAttacker = GetComponent<PlayerAttacker>();
            _playerInventory = GetComponent<PlayerInventory>();
            _playerManager = GetComponent<PlayerManager>();
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
            HandleMoveInput(delta); // �̷������� ��ȣ������ ���Ѽ� ����ϴ±���! ����� ĸ�����̾�
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotsInput();
        }

        private void HandleMoveInput(float delta)
        {
            horizontal = _movemetInput.x;
            vertical = _movemetInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = _cameraInput.x;
            mouseY = _cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            // TODO : inputAction���� �̽� �� �ذ��
            #region �̽�
            /*
             * _inputActions.PlayerActions.Roll.phase���� performed�� ������
             * �׷���_inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started�� �۵����� �ʾ���
             * phase�� ��ó�� Started�� ���´ٰ� �ߴµ� ������ Started�� ������ �ʾ���
             * 
             * ����
             * ���Ӱ� ������Ʈ�� InputSystem�������� started�� �ǳʶٰ� �ٷ� performed���·� �����Ѵ�
             * �׷��� started�� log�� ������ ������
             * 
             * �ذ��� 
             * ù��° �ذ�δ� playerControls���� interaction�� slow tap�� �߰��ϸ� started�� �������Ҽ��ִ�
             * �ι�°������� phase�� ��������ʰ� triggered������ ����ϸ� bool���� return�������ִ�
             * ����°�δ� performed�� ������ �޴°��̴�
             */

            #endregion

            // triggered�� ����Ͽ� bool������ �Ҵ�
            //b_Input = _inputActions.PlayerActions.Roll.triggered;
            // sprint����� �������� phase���
            b_Input = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            // TODO : ����Ʈ�� ������ �޸��� �ִϸ��̼��� �����. ������ ���� �ʴµ���!
            #region �̽�
            /*
             *** ���� ������ �ذ�� ***
            ������ ������ ������ ��, moveDirection�� 0�ϰ�쿣 �޸��� ��Ǿȳ����� �ȴ�
            PlayerLocomotion�� animatorHandler.UpdateAnimatorValue�޼��带 ȣ���Ѵµ� ���⼭ bool������ isSprinting�� ����Ѵ�. 
            �� �޼���� ĳ���� ������ blend tree ���� �ٲ��ִµ� ���� isSprinting�� true�� 2�� �����Ǹ� ĳ���Ͱ� �޸��� ����� ���ϰ� �ȴ�.
            ��, isSprinting�� true ���¸� ĳ���Ͱ� �޸��� ����� ������ �ȴ�.
            �׷��Ƿ� ���ϴ� ������ ���ؼ� ����Ʈ�� �����鼭 movedirection.x���� 0�� �ƴҰ��(����Ű�� �������) isSprinting�� true�� �ٲ��ָ� �ȴ�
            ���� playerManager�� LateUpdate�� �ִ� isSprinting = _inputHandler.b_Input�� �ּ�ó���ϰ�
            PlayerLocomotion HandleMovement�޼���� sprintFlag���ǹ��� �����Ͽ���
            ����Ʈ�� ����Ű�� ���ÿ� �������ϴ� ����(_inputHandler.sprintFlag && (moveDirection.x != 0))�� ��� isSprinting�� true�� �ǰ�
            �׷��� ������쿣 false�� �ٲ����

            *** ��Ʃ�� �ذ�� ***
            PlayerManager�� isSprinting = _inputHandler.b_Input;���� -> �׻� true�� ��ȯ�ϱ� ������
            PlayerLocomotion�� �޸��� ���ǹ� �߰� -> moveAmount���� ���� ��ġ �̻��̶�� ���� �߰���
                �߰��� else���� walingSpeed, speed ���ǹ� �߰�

            
             */

            #endregion

            if (b_Input)
            {
                // ������ ������ sprint
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                // ��� ������ ���� roll
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            _inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            _inputActions.PlayerActions.RT.performed += i => rt_Input = true;

            if (rb_Input)
            {
                // �޺������϶� �޺����ݸ��
                if (_playerManager.canDoCombo)
                {
                    comboFlag = true;
                    _playerAttacker.HandleWeaponCombo(_playerInventory.rightWeapon);
                    comboFlag = false; // ������ ȣ��Ǵ°��� �������� ������ false
                }
                else
                {
                    // ����Ű�� ������ ������ �ԷµǴ°� ����
                    if (_playerManager.isInteracting)
                    {
                        return;
                    }
                    if (_playerManager.canDoCombo)
                    {
                        return;
                    }
                    _playerAttacker.HandleLightAttack(_playerInventory.rightWeapon);
                }
            }

            if (rt_Input)
            {
                if (_playerManager.isInteracting)
                {
                    return;
                }
                if (_playerManager.canDoCombo)
                {
                    return;
                }
                _playerAttacker.HandleHeavyAttack(_playerInventory.rightWeapon);
            }
        }

        private void HandleQuickSlotsInput()
        {
            _inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
            _inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

            if (d_Pad_Right)
            {
                _playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
                _playerInventory.ChangeLeftWeapon();
            }
        }
    }

}
