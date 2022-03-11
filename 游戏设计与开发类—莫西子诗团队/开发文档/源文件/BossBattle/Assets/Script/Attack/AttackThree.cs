using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AttackThree : AttackControl
{
    
    private GameObject myEmptyThr;
    private Vector3 myEmptyVector;
    private float Timer;
    void Start()
    {
        myEmptyThr=GameObject.Find("Attack_Three").gameObject;
    } 

    // Update is called once per frame
    void Update()
    {
        ThrMain(); 
    }

    public void ArrowGo()
    {
        myEmptyVector=myEmptyThr.transform.position;//获取射箭时角色的位置信息
        PublicControl.ArrowGoRotation=myPlayer.transform.localScale.x;//获取射箭时角色面向的方向
        if(PublicControl.ArrowGoRotation==1)
        {
            GameObject instance=Instantiate(myPrefabArrow,myEmptyVector,Quaternion.Euler(0,0*180,0));//实例化箭
            instance.transform.parent=myTemporary.transform;//将实例化箭存放到统一的空物体下
        }
        else
        {
            GameObject instance=Instantiate(myPrefabArrow,myEmptyVector,Quaternion.Euler(0,PublicControl.ArrowGoRotation*180,0));//实例化箭
            instance.transform.parent=myTemporary.transform;//将实例化箭存放到统一的空物体下
        }
        
        PublicControl.ArrowNum--;//箭的数量减一
    }

    void ThrMain()
    {
        if(PublicControl.IsAttackThr==true)
        {
            StartCoroutine(BloodNum.Instance.MonsterDamageBlood(15f));
        }

        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            if(PublicControl.ArrowNum>=1)
            {
                myAnimator.SetBool("Run",false);
                myAnimator.SetBool("AttThr",true);
                
            }
            
        }
        else if(Input.GetKeyUp(KeyCode.Keypad3))
        {
            myAnimator.SetBool("AttThr",false);
        }
    }
    

   
}
