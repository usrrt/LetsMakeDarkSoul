using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class CameraHandler : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public static CameraHandler singleton;

        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;

        private Transform _myTransform;
        private Vector3 _cameraTransformPosition;
        private LayerMask _ignoreLayers;
        private Vector3 _cameraFollowVelocity = Vector3.zero;
        

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float _targetPosition;
        private float _defaultPosition;
        private float _lookAngle;
        private float _pivotAngle;

        public float maximumPivot = 35f;
        public float minimumPivot = -35f;

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;

        private void Awake()
        {
            singleton = this;
            _myTransform = transform;
            _defaultPosition = cameraTransform.localPosition.z;
            
            // TODO : 비트연산자인거 맞지? 어떻게 해석하는지 모르겠다 일단은
            _ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        /// <summary>
        /// 카메라가 타겟위치로 이동한다
        /// </summary>
        /// <param name="delta"></param>
        public void FollowTarget(float delta)
        {
            // 여기서 target은 player가 될것이다
            // Lerp보다 SmoothDamp를 추천. 속도를 고려하면 훨씬 자연스러워 보임
            Vector3 targetPosition = Vector3.SmoothDamp(_myTransform.position, targetTransform.position, ref _cameraFollowVelocity, delta / followSpeed);
            _myTransform.position = targetPosition;

            HandleCameraCollisions(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            _lookAngle += (mouseXInput * lookSpeed) / delta;
            _pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            _pivotAngle = Mathf.Clamp(_pivotAngle, minimumPivot, maximumPivot);

            // player의 회전? or 카메라의 회전?
            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            _myTransform.rotation = targetRotation;

            // pivot의 회전
            rotation = Vector3.zero;
            rotation.x = _pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCameraCollisions(float delta)
        {
            _targetPosition = _defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(_targetPosition), _ignoreLayers))
            {
                float dist = Vector3.Distance(cameraPivotTransform.position, hit.point);
                _targetPosition = -(dist - cameraCollisionOffset);
            }

            if (Mathf.Abs(_targetPosition) < minimumCollisionOffset)
            {
                _targetPosition = -minimumCollisionOffset;
            }

            _cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, _targetPosition, delta / 0.2f);
            cameraTransform.localPosition = _cameraTransformPosition;
        }
    }

}