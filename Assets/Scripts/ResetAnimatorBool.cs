using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    // 애니메이션(rolling, land, attack...)이 끝날경우 empty state로 돌아가게한다
    public string targetBool;
    public bool status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }

}
