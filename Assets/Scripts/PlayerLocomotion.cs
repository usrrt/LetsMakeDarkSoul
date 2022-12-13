using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class PlayerLocomotion : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        private Transform _cameraObject;
        private InputHandler _inputHandler;

        private Vector3 _moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigid;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField] float movementSpeed = 5;
        [SerializeField] float sprintSpeed = 7;
        [SerializeField] float rotationSpeed = 10;

        public bool isSprinting;

        private void Start()
        {
            rigid = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            _cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            isSprinting = _inputHandler.b_Input;
            _inputHandler.TickInput(delta);
            HandleMovement(delta);
            HandleRollingAndSprinting(delta);
        }

        #region Movement

        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = _inputHandler.moveAmount;

            targetDir = _cameraObject.forward * _inputHandler.vertical;
            targetDir += _cameraObject.right * _inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0; // y축으로 이동하지 않게한다

            if (targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            float rotSpeed = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotSpeed * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleMovement(float delta)
        {
            if (_inputHandler.rollFlag)
            {
                return;
            }

            _moveDirection = _cameraObject.forward * _inputHandler.vertical;
            _moveDirection += _cameraObject.right * _inputHandler.horizontal;
            _moveDirection.Normalize();

            // 이동하다 플레이어가 공중에 뜨는 현상 fix
            _moveDirection.y = 0;

            float speed = movementSpeed;
            if (_inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                isSprinting = true;
                _moveDirection *= speed;
            }
            else
            {
                _moveDirection *= speed;
            }

            // TODO : Vector3.ProjectOnPlane 무엇인지 알아보기
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, normalVector);
            rigid.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValue(_inputHandler.moveAmount, 0, isSprinting);

            if (animatorHandler.isRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
            {
                return;
            }

            if (_inputHandler.rollFlag)
            {
                _moveDirection = _cameraObject.forward * _inputHandler.vertical;
                _moveDirection += _cameraObject.right * _inputHandler.horizontal;

                if (_inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    _moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(_moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Backstep", true);
                }
            }
        }

        #endregion

    }

}
