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
        [SerializeField] float rotationSpeed = 10;

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

            _inputHandler.TickInput(delta);

            _moveDirection = _cameraObject.forward * _inputHandler.vertical;
            _moveDirection += _cameraObject.right * _inputHandler.horizontal;
            _moveDirection.Normalize();

            // 이동하다 플레이어가 공중에 뜨는 현상 fix
            _moveDirection.y = 0;

            float speed = movementSpeed;
            _moveDirection *= speed;

            // TODO : Vector3.ProjectOnPlane 무엇인지 알아보기
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, normalVector);
            rigid.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValue(_inputHandler.moveAmount, 0);

            if (animatorHandler.isRotate)
            {
                HandleRotation(delta);
            }

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

        #endregion

    }

}
