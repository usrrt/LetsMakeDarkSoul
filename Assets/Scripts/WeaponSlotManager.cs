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

        private void Awake()
        {
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
            }
            else
            {
                _rigthHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
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