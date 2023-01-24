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

        QuickSlotsUI _quickSlotsUI;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            // ������ weaponholerslot���� �޼� weponhodlerslot���� Ȯ��
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
                _quickSlotsUI.UpdateWeaponQuickSlotUI(true, weaponItem);
                #region Handle Left Weapon Idle Animations
                // �����ϰ� nullüũ (�ٵ� null�� ������ LoadLeftWeaponDamageCollider���� ������ �߻� ���� ������?)
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
                _quickSlotsUI.UpdateWeaponQuickSlotUI(false, weaponItem);
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
        ***** open/close�� ��ó�� ���� ���� ******
         weaponSlotManager�� animator�� ���� �� �ֻ�� ��ġ�� �ִ�
        ����Ƽ���� �����ϴ� ����� �ϳ��� �ִϸ��̼� �̺�Ʈ�� ����Ҽ�����
        ���� �ִϸ��̼� Ư�� �����Ӹ��� public �޼���(open, close)�� ȣ���Ҽ�����
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