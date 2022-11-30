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

        public Animator anim;

        private int vertical;
        private int horizontal;

        public bool isRotate;

        // Ŭ������ �ٸ������� �����Ҵ� ����ϱ��� �ʱ�ȭ�� �ʿ��Ѱ͵��� �� �޼ҵ忡 �־�ΰ� ����ϴ°� start���� �ʱ�ȭ���ִ°��� ���� �⺻���� ���·� ���� ���δ�
        public void Initialize()
        {
            anim = GetComponent<Animator>();

            // StringToHash : Animator Contorller�� �Ķ���Ϳ� �����Ҽ��ְ� �����ִ� AnimatorŬ���� ���. �Ķ���͸� �����Ҽ��ִ�(Generates an parameter id from a string)
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValue(float verticalMovement, float horizontalMovement)
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

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            isRotate = true;
        }

        public void StopRotation()
        {
            isRotate = false;
        }
    }

}