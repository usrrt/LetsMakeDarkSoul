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
            /*���Ⱑ �ִٸ� ������ �������� sprite�� �־��ְ�
             ���Ⱑ ���ٸ�(weapon == null) sprite�� null�� ������ְ� �������� ���ش�*/
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
