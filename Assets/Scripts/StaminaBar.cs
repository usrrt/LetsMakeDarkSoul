using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HSW
{
    public class StaminaBar : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public Slider staminaSlider;

        private void Awake()
        {
            staminaSlider = GetComponent<Slider>();
        }

        public void SetMaxStamina(int maxStamina)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = maxStamina;
        }

        public void SetCurrentStamina(int currentStamina)
        {
            staminaSlider.value = currentStamina;
        }
    }
}
