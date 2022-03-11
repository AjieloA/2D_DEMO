using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTwo : AttackControl
{
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad2)&&PublicControl.IsLand)
        {
            if(Time.time>(PublicControl.AttackTwoLastTime+PublicControl.AttackTwoCoolTime)&&myAnimator.GetBool("Run")==false)
            {
                PublicControl.IsAttackTwo=true;
                myAnimator.SetBool("AttTwo",true);
                PublicControl.AttackTwoLastTime=Time.time;
                
            }
            else
            {
                PublicControl.IsAttackTwo=false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Boss")
        {
            PublicControl.PlayerBloodHit-=5;
            StartCoroutine(BloodNum.Instance.MonsterDamageBlood(50));
        }
    }
}
