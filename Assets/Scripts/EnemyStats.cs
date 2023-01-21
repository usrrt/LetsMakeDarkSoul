using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{

    public class EnemyStats : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public void TakeDamage(int damage)
        {
            Debug.Log($"적에게 가한 데미지 : {damage}");
        }
    }

}