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

        public string lastAttack; // 마지막에 사용한 공격모션

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
            // 프레임당 한번만 부르기위해 flag사용
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
