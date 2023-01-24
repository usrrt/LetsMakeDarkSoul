using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HSW
{
    public class QuickSlotsUI : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public Image leftWeaponIcon;
        public Image rightWeaponIcon;

        public void UpdateWeaponQuickSlotUI(bool isLeft, WeaponItem weapon)
        {
            /*무기가 있다면 무기의 아이콘을 sprite에 넣어주고
             무기가 없다면(weapon == null) sprite는 null로 만들어주고 아이콘을 꺼준다*/
            if (isLeft)
            {
                if (weapon.itemIcon == null)
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
                else
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
            }
            else
            {
                if (weapon.itemIcon == null)
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
                else
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
            }
        }
    }
}
