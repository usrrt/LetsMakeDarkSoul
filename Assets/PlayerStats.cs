using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class PlayerStats : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;

        private AnimatorHandler _animatorHandler;

        private void Awake()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
            
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);

            _animatorHandler.PlayTargetAnimation("Damage_01", true);

            // TODO : Handle Player Death
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                _animatorHandler.PlayTargetAnimation("Die_01", true);
            }
        }
    }
}