using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class AnimatorHandler : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        PlayerManager _playerManager;
        InputHandler _inputHandler;
        PlayerLocomotion _playerLocomotion;

        public Animator anim;

        private int vertical;
        private int horizontal;

        public bool isRotate;

        // 클래스를 다른곳에서 참조할대 사용하기전 초기화가 필요한것들은 한 메소드에 넣어두고 사용하는곳 start에서 초기화해주는것이 가장 기본적인 형태로 많이 쓰인다
        public void Initialize()
        {
            _playerManager = GetComponentInParent<PlayerManager>();
            _inputHandler = GetComponentInParent<InputHandler>();
            _playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            anim = GetComponent<Animator>();
            // StringToHash : Animator Contorller의 파라미터에 접근할수있게 도와주는 Animator클래스 멤버. 파라미터를 생성할수있다(Generates an parameter id from a string)
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValue(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = 0;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void CanRotate()
        {
            isRotate = true;
        }

        public void StopRotation()
        {
            isRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (_playerManager.isInteracting == false)
            {
                return;
            }

            float delta = Time.deltaTime;
            _playerLocomotion.rigid.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            _playerLocomotion.rigid.velocity = velocity;
        }
    }

}