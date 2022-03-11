using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : AttackControl
{
    private Vector3 myEmptyDestrVector;
    private GameObject myEmptyDestr;
    void Start() 
    {
        myEmptyDestr=this.gameObject.transform.GetChild(0).gameObject;//获取攻击特效生成点；
    }
    void Update()
    {
        ArrowMove();
    }

    void ArrowMove()
    {
        transform.Translate(Vector3.left*5f*Time.deltaTime);//弓箭移动；
        if(this.gameObject.transform.position.x>Right.x||this.gameObject.transform.position.x<Left.x)//超出场景边界销毁弓箭；
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag=="Boss")
        {
            PublicControl.IsAttackThr=true;
            myEmptyDestrVector=myEmptyDestr.transform.position;
            gameObject.SetActive(false);
            GameObject ingameObject=Instantiate(myPrefabDestr,myEmptyDestrVector,this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
