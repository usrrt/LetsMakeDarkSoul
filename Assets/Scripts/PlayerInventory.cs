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

        // 무기 슬롯을 위한 WeaponItem 배열
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
            // 인덱스범위면서 슬롯이 null이 아닐때 무기를 load한다
            // 무기를 바꾸기위해선 weaponIndex로만 접근할수있다

            //currentRightWeaponIndex = currentRightWeaponIndex + 1;

            //if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
            //{
            //    // -1인 이유 : 비무장상태에서 다시 메소드를 호출하면 제일 윗코드로 인해 ++되어 0이된다. 인덱스 0일때의 조건문이 실행됨
            //    currentRightWeaponIndex = -1;
            //    rightWeapon = unarmedWeapon;
            //    _waponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            //}
            //else if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)
            //{
            //    rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            //    _waponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
            //}
            //else
            //{
            //    currentRightWeaponIndex = currentRightWeaponIndex + 1;
            //}

            ++currentRightWeaponIndex;

            if (currentRightWeaponIndex <= weaponsInRightHandSlots.Length - 1)
            {
                if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)
                {
                    rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                }
                else
                {
                    ++currentRightWeaponIndex;
                    rightWeapon = unarmedWeapon;
                }
            }
            else
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
            }

            _waponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }

        public void ChangeLeftWeapon()
        {
            ++currentLeftWeaponIndex;

            if (currentLeftWeaponIndex <= weaponsInLeftHandSlots.Length - 1)
            {
                if (weaponsInLeftHandSlots[currentLeftWeaponIndex] != null)
                {
                    leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                }
                else
                {
                    ++currentLeftWeaponIndex;
                    leftWeapon = unarmedWeapon;
                }
            }
            else
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
            }

            _waponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
    }
}