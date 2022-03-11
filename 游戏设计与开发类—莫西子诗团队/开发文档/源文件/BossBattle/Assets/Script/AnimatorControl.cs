using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator myAnimator;
    private void Awake()
    {
        myAnimator=GameObject.Find("Player").GetComponent<Animator>();
    }
    void Update()
    {
        
        myAnimator.SetBool("Jump",PublicControl.IsJump);
        if(PublicControl.IsLand==false)
        {
            myAnimator.SetBool("Jump_Fall",true);
        }
        else
        {
            myAnimator.SetBool("Jump_Fall",false);
        }
    }
    private void FixedUpdate()
    {
        if(PublicControl.IsDash!=true)
        {
            myAnimator.SetBool("Run",PublicControl.IsVelocityX);
            myAnimator.SetBool("Dec",false);
        }

        if(PublicControl.IsDash==true&&PublicControl.IsJump!=true)
        {
            if(myAnimator.GetBool("Run")==true)
            {
                myAnimator.SetBool("Run",false);
            }
            myAnimator.SetBool("Dec",true);
        }
    }
}
