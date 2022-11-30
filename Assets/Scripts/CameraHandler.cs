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

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float _defaultPosition;
        private float _lookAngle;
        private float _pivotAngle;

        public float maximumPivot = 35f;
        public float minimumPivot = -35f;

        private void Awake()
        {
            singleton = this;
            _myTransform = transform;
            _defaultPosition = cameraTransform.localPosition.z;
            
            // TODO : ��Ʈ�������ΰ� ����? ��� �ؼ��ϴ��� �𸣰ڴ� �ϴ���
            _ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);

        }

        /// <summary>
        /// ī�޶� Ÿ����ġ�� �̵��Ѵ�
        /// </summary>
        /// <param name="delta"></param>
        public void FollowTarget(float delta)
        {
            // ���⼭ target�� player�� �ɰ��̴�
            Vector3 targetPosition = Vector3.Lerp(_myTransform.position, targetTransform.position, delta / followSpeed);
            _myTransform.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            _lookAngle += (mouseXInput * lookSpeed) / delta;
            _pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            _pivotAngle = Mathf.Clamp(_pivotAngle, minimumPivot, maximumPivot);

            // player�� ȸ��? or ī�޶��� ȸ��?
            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            _myTransform.rotation = targetRotation;

            // pivot�� ȸ��
            rotation = Vector3.zero;
            rotation.x = _pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
    }

}