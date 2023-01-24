using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    // ¸ðµç itemÀÇ base
    public class Item : ScriptableObject
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        [Header("Item information")]
        public Sprite itemIcon;
        public string itemName;
    }

}
