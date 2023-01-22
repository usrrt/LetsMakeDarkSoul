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
        public string OneHanded_Light_Attack_2;
        public string OneHanded_Heavy_Attack_1;
        public string OneHanded_Heavy_Attack_2;

        // TODO : 팔이 어색해서 임시 보류(현재 layer weight 0)
        [Header("Idle Animations")]
        public string Right_Hand_Idle_01;
        public string Left_Hand_Idle_01;
    }
}