using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSW
{
    public class PlayerManager : MonoBehaviour
    {
        // ###############################################
        //             NAME : HongSW                      
        //             MAIL : gkenfktm@gmail.com         
        // ###############################################

        InputHandler _inputHandler;
        Animator _anim;

        // Start is called before the first frame update
        void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            _inputHandler.isInteracting = _anim.GetBool("isInteracting");
            _inputHandler.rollFlag = false;
        }
    }

}
