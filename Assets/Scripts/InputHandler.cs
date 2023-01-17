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

        public bool rollFlag;
        public bool sprintFlag;
        public float rollInputTimer;

        PlayerControls _inputActions;

        Vector2 _movemetInput;
        Vector2 _cameraInput;

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
            HandleRollInput(delta);
        }

        private void MoveInput(float delta)
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

            // triggered를 사용하여 bool값으로 할당
            //b_Input = _inputActions.PlayerActions.Roll.triggered;
            // sprint기능을 쓰기위해 phase사용
            b_Input = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            // TODO : 쉬프트를 누르면 달리기 애니메이션이 실행됨. 앞으로 가지 않는데도!
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
    }

}
