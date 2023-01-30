using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class WeaponPickUp : Interactable
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager); // ���� ���� ȣ��

            // �����Լ� ������
            // �������� �ݰ� �κ��丮�� ����
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory inventory;
            PlayerLocomotion locomotion;
            AnimatorHandler animatorHandler;

            // PlayerManager�� ���� �ʿ��� ������Ʈ ��������
            inventory = playerManager.GetComponent<PlayerInventory>();
            locomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            locomotion.rigid.velocity = Vector3.zero; // ������ �ֿ� �� �÷��̾� ���߱�
            animatorHandler.PlayTargetAnimation("Pick Up", true); // ������ �ֿ� �� �ִϸ��̼� ����
            inventory.weaponsInventory.Add(weapon);
            Destroy(gameObject);
        }
    }
}
