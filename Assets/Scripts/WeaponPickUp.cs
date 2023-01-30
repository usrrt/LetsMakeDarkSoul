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
            base.Interact(playerManager); // 기존 내용 호출

            // 가상함수 재정의
            // 아이템을 줍고 인벤토리에 저장
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory inventory;
            PlayerLocomotion locomotion;
            AnimatorHandler animatorHandler;

            // PlayerManager를 거쳐 필요한 컴포넌트 가져오기
            inventory = playerManager.GetComponent<PlayerInventory>();
            locomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            locomotion.rigid.velocity = Vector3.zero; // 아이템 주울 때 플레이어 멈추기
            animatorHandler.PlayTargetAnimation("Pick Up", true); // 아이템 주울 때 애니메이션 실행
            inventory.weaponsInventory.Add(weapon);
            Destroy(gameObject);
        }
    }
}
