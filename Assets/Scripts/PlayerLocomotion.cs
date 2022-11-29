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

        private Vector3 moveDirection;

        [HideInInspector]
        public Transform MyTransform;
        [HideInInspector]
        public AnimatorHandler AnimatorHandler;

        public new Rigidbody Rigid;
        public GameObject NormalCamera;

        [Header("Stats")]
        [SerializeField] float movementSpeed = 5;
        [SerializeField] float rotationSpeed = 10;

        private void Start()
        {
            Rigid = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            AnimatorHandler = GetComponentInChildren<AnimatorHandler>();
            _cameraObject = Camera.main.transform;
            MyTransform = transform;
            AnimatorHandler.Initialize();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _inputHandler.TickInput(delta);

            moveDirection = _cameraObject.forward * _inputHandler.vertical;
            moveDirection += _cameraObject.right * _inputHandler.horizontal;
            moveDirection.Normalize();

            float speed = movementSpeed;
            moveDirection *= speed;

            // TODO : Vector3.ProjectOnPlane 무엇인지 알아보기
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            Rigid.velocity = projectedVelocity;

            AnimatorHandler.UpdateAnimatorValue(_inputHandler.moveAmount, 0);

            if (AnimatorHandler.IsRotate)
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
                targetDir = MyTransform.forward;
            }

            float rotSpeed = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(MyTransform.rotation, tr, rotSpeed * delta);

            MyTransform.rotation = targetRotation;
        }

        #endregion

    }

}
