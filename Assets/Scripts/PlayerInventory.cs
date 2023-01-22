using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class PlayerInventory : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        WeaponSlotManager _waponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public WeaponItem unarmedWeapon;

        // ���� ������ ���� WeaponItem �迭
        public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        private void Awake()
        {
            _waponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            rightWeapon = unarmedWeapon;
            leftWeapon = unarmedWeapon;
        }

        public void ChangeRightWeapon()
        {
            // �ε��������鼭 ������ null�� �ƴҶ� ���⸦ load�Ѵ�
            // ���⸦ �ٲٱ����ؼ� weaponIndex�θ� �����Ҽ��ִ�

            currentRightWeaponIndex = currentRightWeaponIndex + 1;

            if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
            {
                // -1�� ���� : ������¿��� �ٽ� �޼ҵ带 ȣ���ϸ� ���� ���ڵ�� ���� ++�Ǿ� 0�̵ȴ�. �ε��� 0�϶��� ���ǹ��� �����
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                _waponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
            else if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                _waponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

            if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
            {
                // -1�� ���� : ������¿��� �ٽ� �޼ҵ带 ȣ���ϸ� ���� ���ڵ�� ���� ++�Ǿ� 0�̵ȴ�. �ε��� 0�϶��� ���ǹ��� �����
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                _waponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
            else if (weaponsInLeftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                _waponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], false);
            }
            else
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }
        }
    }
}