using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public Transform parentOverride; // ���� ���� ��ġ(pivot)
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            // weaponItem null�ϰ�� ����ִ� ���������Ʈ�� ���ش�
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            // ���⸦ �ٽ� �ε��Ұ�� currentWeaponModel�� �ٽ� null�� ������ش�
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            // unload weapon and destroy
            UnloadWeaponAndDestroy();

            if (weaponItem == null)
            {
                // unload weapon
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                // ����� �÷��̾� Hand ������ü�� ��ġ�ϰ� �ȴ�
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
    }
}