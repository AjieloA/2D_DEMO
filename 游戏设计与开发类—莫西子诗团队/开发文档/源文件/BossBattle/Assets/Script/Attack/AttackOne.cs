using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : AttackControl
{
     private PolygonCollider2D myPolyCollider;
    void Start()
    {
        myPolyCollider=GetComponent<PolygonCollider2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad1))
        {
            myAnimator.SetBool("Run",false);
            myAnimator.SetBool("AttOne",true);
        }
        else if(Input.GetKeyUp(KeyCode.Keypad1))
        {
            myAnimator.SetBool("AttOne",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Boss")
        {
            StartCoroutine(BloodNum.Instance.MonsterDamageBlood(5));
            //StartCoroutine(BloodNum.Instance.PlayerDamageBlood(5));
        }
    } 
}
