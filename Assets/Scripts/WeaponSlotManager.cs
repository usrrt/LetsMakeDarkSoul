using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class WeaponSlotManager : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        WeaponHolderSlot _leftHandSlot;
        WeaponHolderSlot _rigthHandSlot;

        DamageCollider _leftHandDamageCollider;
        DamageCollider _rightHandDamageCollider;

        Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            // 오른손 weaponholerslot인지 왼손 weponhodlerslot인지 확인
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    _leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    _rigthHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                _leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                #region Handle Left Weapon Idle Animations
                // 안전하게 null체크 (근데 null이 들어오면 LoadLeftWeaponDamageCollider에서 오류가 발생 하지 않을까?)
                if (weaponItem != null)
                {
                    _animator.CrossFade(weaponItem.Left_Hand_Idle_01, 0.2f);
                }
                else
                {
                    _animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                _rigthHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                #region Handle Right Weapon Idle Animations
                if (weaponItem != null)
                {
                    _animator.CrossFade(weaponItem.Right_Hand_Idle_01, 0.2f);

                }
                else
                {
                    _animator.CrossFade("Right Arm Empty", 0.2f);

                }
                #endregion
            }
        }

        #region Handle Weapon Damage Collider
        /*
        ***** open/close를 이처럼 나눈 이유 ******
         weaponSlotManager는 animator와 같은 모델 최상단 위치에 있다
        유니티에서 제공하는 기능중 하나인 애니메이션 이벤트를 사용할수있음
        따라서 애니메이션 특정 프레임마다 public 메서드(open, close)를 호출할수있음
         */
        private void LoadLeftWeaponDamageCollider()
        {
            _leftHandDamageCollider = _leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            _rightHandDamageCollider = _rigthHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenLeftDamageCollider()
        {
            _leftHandDamageCollider.EnableDamageCollider();
        }

        public void OpenRightDamageCollider()
        {
            _rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseLeftDamageCollider()
        {
            _leftHandDamageCollider.DisableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            _rightHandDamageCollider.DisableDamageCollider();
        }

        #endregion
    }
}