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
            HandleMoveInput(delta); // 이런식으로 보호수준을 지켜서 사용하는구만! 상당히 캡슐적이야
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
            // TODO : inputAction관련 이슈 및 해결법
            #region 이슈
            /*
             * _inputActions.PlayerActions.Roll.phase값이 performed만 들어왔음
             * 그래서_inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started는 작동하지 않았음
             * phase는 맨처음 Started가 들어온다고 했는데 왜인지 Started는 찍히지 않았음
             * 
             * 원인
             * 새롭게 업데이트된 InputSystem버전에선 started를 건너뛰고 바로 performed상태로 진입한다
             * 그래서 started가 log에 찍히지 않은것
             * 
             * 해결방안 
             * 첫번째 해결로는 playerControls에서 interaction에 slow tap을 추가하면 started가 찍히게할수있다
             * 두번째방법으론 phase를 사용하지않고 triggered변수를 사용하면 bool값을 return받을수있다
             * 세번째로는 performed를 변수로 받는것이다
             */

            #endregion

            // triggered를 사용하여 bool값으로 할당
            //b_Input = _inputActions.PlayerActions.Roll.triggered;
            // sprint기능을 쓰기위해 phase사용
            b_Input = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            // TODO : 쉬프트를 누르면 달리기 애니메이션이 실행됨. 앞으로 가지 않는데도!
            #region 이슈
            /*
             *** 내가 생각한 해결법 ***
            앞으로 나가지 않으면 즉, moveDirection이 0일경우엔 달리는 모션안나오면 된다
            PlayerLocomotion엔 animatorHandler.UpdateAnimatorValue메서드를 호출한는데 여기서 bool값으로 isSprinting을 사용한다. 
            이 메서드는 캐릭터 움직임 blend tree 값을 바꿔주는데 만약 isSprinting이 true면 2로 고정되며 캐릭터가 달리는 모션을 취하게 된다.
            즉, isSprinting이 true 상태면 캐릭터가 달리는 모션이 나오게 된다.
            그러므로 원하는 구현을 위해선 쉬프트를 누르면서 movedirection.x값이 0이 아닐경우(방향키를 누를경우) isSprinting을 true로 바꿔주면 된다
            나는 playerManager의 LateUpdate에 있는 isSprinting = _inputHandler.b_Input를 주석처리하고
            PlayerLocomotion HandleMovement메서드안 sprintFlag조건문을 수정하였다
            쉬프트와 방향키를 동시에 눌러야하는 조건(_inputHandler.sprintFlag && (moveDirection.x != 0))일 경우 isSprinting은 true가 되고
            그렇지 않은경우엔 false로 바꿔줬다

            *** 유튜버 해결법 ***
            PlayerManager의 isSprinting = _inputHandler.b_Input;삭제 -> 항상 true를 반환하기 때문에
            PlayerLocomotion의 달리기 조건문 추가 -> moveAmount값이 일정 수치 이상이라는 조건 추가함
                추가로 else문에 walingSpeed, speed 조건문 추가

            
             */

            #endregion

            if (b_Input)
            {
                // 누르고 있으면 sprint
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                // 잠시 눌렀다 떼면 roll
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
                // 콤보상태일땐 콤보공격모션
                if (_playerManager.canDoCombo)
                {
                    comboFlag = true;
                    _playerAttacker.HandleWeaponCombo(_playerInventory.rightWeapon);
                    comboFlag = false; // 여러번 호출되는것을 막기위해 사용즉시 false
                }
                else
                {
                    // 공격키를 누를때 여러번 입력되는것 방지
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
