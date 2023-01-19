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

        private void Awake()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            _animatorHandler.PlayTargetAnimation(weapon.OneHanded_Light_Attack_1, true);
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            _animatorHandler.PlayTargetAnimation(weapon.OneHanded_Heavy_Attack_1, true);
        }
    }

}
