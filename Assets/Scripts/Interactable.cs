using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    // ��ȣ�ۿ� ������ ��ü�� base class�� �ɰ��̴�
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

        // �����Լ� => Override�Ͽ� ������
        public virtual void Interact(PlayerManager playerManager)
        {
            // ��ȣ�ۿ�� ȣ��Ǵ� �޼ҵ�
            Debug.Log("interacted with an object");
        }
    }
}
