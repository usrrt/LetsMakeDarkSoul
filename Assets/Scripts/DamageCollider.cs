using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class DamageCollider : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        BoxCollider _damageCollider;

        public int currentDamage = 25;

        private void Awake()
        {
            _damageCollider = GetComponent<BoxCollider>();
            _damageCollider.gameObject.SetActive(true);
            _damageCollider.isTrigger = true;
            _damageCollider.enabled = false;
        }

        // 상황에 따라서 콜라이더 꺼지고 켜지는 컴포넌트를 만들면 유용하게 사용할수있다
        public void EnableDamageCollider()
        {
            _damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            _damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentDamage);
                }
            }

            if (collision.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentDamage);
                }
            }
        }
    }
}