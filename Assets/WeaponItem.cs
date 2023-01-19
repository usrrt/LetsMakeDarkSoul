using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Attack Animations")]
        public string OneHanded_Light_Attack_1;
        public string OneHanded_Heavy_Attack_1;
    }
}