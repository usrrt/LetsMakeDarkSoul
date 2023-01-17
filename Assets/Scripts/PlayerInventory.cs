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

        private void Awake()
        {
            _waponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            _waponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            _waponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
    }
}