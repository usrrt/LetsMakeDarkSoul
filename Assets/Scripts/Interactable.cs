using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    // 상호작용 가능한 개체의 base class가 될것이다
    public class Interactable : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public float radius = 0.6f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        // 가상함수 => Override하여 재정의
        public virtual void Interact(PlayerManager playerManager)
        {
            // 상호작용시 호출되는 메소드
            Debug.Log("interacted with an object");
        }
    }
}
