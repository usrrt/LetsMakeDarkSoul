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
            MoveInput(delta); // �̷������� ��ȣ������ ���Ѽ� ����ϴ±���! ����� ĸ�����̾�
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
            // TODO : inputAction���� �̽� �� �ذ��
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

            // triggered�� ����Ͽ� bool������ �Ҵ�
            //b_Input = _inputActions.PlayerActions.Roll.triggered;
            // sprint����� �������� phase���
            b_Input = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            // TODO : ����Ʈ�� ������ �޸��� �ִϸ��̼��� �����. ������ ���� �ʴµ���!
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
    }

}
