using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class PlayerAttacker : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        AnimatorHandler _animatorHandler;
        InputHandler _inputHandler;
        WeaponSlotManager _weaponSlotManager;

        public string lastAttack; // �������� ����� ���ݸ��

        private void Awake()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
            _weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            _inputHandler = GetComponent<InputHandler>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            _weaponSlotManager.attackingWeapon = weapon;
            _animatorHandler.PlayTargetAnimation(weapon.OneHanded_Light_Attack_1, true);
            lastAttack = weapon.OneHanded_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            _weaponSlotManager.attackingWeapon = weapon;
            _animatorHandler.PlayTargetAnimation(weapon.OneHanded_Heavy_Attack_1, true);
            lastAttack = weapon.OneHanded_Heavy_Attack_1;
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            // �����Ӵ� �ѹ��� �θ������� flag���
            if (_inputHandler.comboFlag)
            {
                _animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OneHanded_Light_Attack_1)
                {
                    _animatorHandler.PlayTargetAnimation(weapon.OneHanded_Light_Attack_2, true);
                }

            }
        }

    }

}
