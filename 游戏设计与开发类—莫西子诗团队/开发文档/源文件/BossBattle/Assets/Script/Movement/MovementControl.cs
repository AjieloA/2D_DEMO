using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    
    protected Rigidbody2D myRigidbody2D;
    protected CapsuleCollider2D myCapsuleCollider;
    protected Animator myAnimator;
    protected float myKeyHor;//返回-1，0，1控制角色移动方向；
    protected Vector2 StartPosion;

    [Header("速度控制")]
    public float myMoveSpeed;//角色正常移动速度；
    public float myJumpHight;//跳跃高度；

    [Header("射线检测控制")]
    protected RaycastHit2D myRaycastHite2D;
    protected LayerMask myLandLyaer;
    protected float myNumbY=0.92f;//射线检测初始Y值偏移距离；
    protected float myNumbL=0.2f;//射线检测距离；

    protected RaycastHit2D myRaycastHit2DMonster;
    protected LayerMask myMonsterLayer;

    [Header("Dash控制")]
    protected float DashTimeGet=0.1f;//Dash时长；
    protected float DashSpeed=30;//Dash速度；
    
    
    void Start()
    {
        StartPosion=transform.position;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myLandLyaer=LayerMask.GetMask("LandCollider");
        myMonsterLayer=LayerMask.GetMask("Monster");
        myCapsuleCollider=GetComponent<CapsuleCollider2D>();
    }
    
    void Update()
    {
        Flip();//调用player转向；
        KeyDown();//调用按键检测；
        RayLand();//调用射线检测；
        RayMonster();
    }
    

    void FixedUpdate()
    { 
        Move();//调用移动；
        Dash();//调用冲刺；
    }

    void KeyDown()
    {
        //PublicControl.IsVelocityX=Math.Abs(myRigidbody2D.velocity.x)>Mathf.Epsilon;//Epsilon一个趋于零但不等于零的值；/* myKeyHor!=0; */
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>Mathf.Epsilon)
        {
            PublicControl.IsVelocityX=true;
        }
        else if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))<Mathf.Epsilon)
        {
            PublicControl.IsVelocityX=false;
        }
        myKeyHor=Input.GetAxisRaw("Horizontal");
        /* 跳跃按键检测 */
        if(Input.GetKeyDown(KeyCode.Space)&&myRaycastHite2D)//移动状态跳跃；
        {
            if(PublicControl.IsAttackTwo==false)
            {
                PublicControl.IsJump=true;
                myRigidbody2D.AddForce(new Vector2(myRigidbody2D.velocity.x,myJumpHight));
            }
        }
        
        if(PublicControl.IsJump==true)
        {
            if(myRigidbody2D.velocity.y<Mathf.Epsilon)
            {
                PublicControl.IsJump=false;
            }
        }

        /* 奔跑按键检测 */
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            if(Time.time>=(PublicControl.DashLastTime+PublicControl.DashCoolTime))//判断当前时间大于上一次执行Dash加上冷却时间，即可再次执行Dash；
            {
                //可以执行Dash
                PublicControl.IsDash=true;
                PublicControl.DashShowTimeSet=DashTimeGet;
                PublicControl.DashLastTime=Time.time;
            }
        }
    }

     void Dash()
    {
        if( PublicControl.IsDash==true)
        {
            if(PublicControl.DashShowTimeSet>0)
            {
                myRigidbody2D.velocity=new Vector2((gameObject.transform.localScale.x*-1)*DashSpeed,myRigidbody2D.velocity.y);
                PublicControl.DashShowTimeSet-=Time.deltaTime;
                ShadowPool.instance.GetPool();
            }
            if( PublicControl.DashShowTimeSet<=0)
            {
                PublicControl.IsDash=false;
            }
        }
    }
    void Move()//角色移动
    { 
        if(PublicControl.IsAttackTwo==false)
        {
            if(Math.Abs(myKeyHor)>Mathf.Epsilon)
            {
                PublicControl.IsMove=true;
            } 
        }
        if(PublicControl.IsMove==true&&PublicControl.IsAttackTwo==false)
        {
            myRigidbody2D.velocity=new Vector2(myKeyHor*myMoveSpeed,myRigidbody2D.velocity.y);
        }
    }

   

    void Flip()//角色利用欧拉角旋转
    {
        if (myKeyHor!=0&&PublicControl.IsAttackTwo==false)
        {
            if (myKeyHor==1)
            {
                transform.localScale=new Vector2(-1,1);
            }
            if (myKeyHor==-1)
            {
                transform.localScale=new Vector2(1,1);
            }
        }
    }  
    void RayLand()//通过射线检测角色是否在地面；
    {
        Vector2 myThisVector2D=new Vector2(transform.position.x,transform.position.y-myNumbY);
        myRaycastHite2D=Physics2D.Raycast(myThisVector2D,Vector2.down,myNumbL,myLandLyaer);
        Debug.DrawRay(myThisVector2D,Vector3.down*myNumbL,Color.red);//显示检测射线；
        if(myRaycastHite2D&&myRaycastHite2D.transform.gameObject.name=="land")
        {
            PublicControl.IsLand=true;
        }
        else
        {
            PublicControl.IsLand=false;
        }
    }

    void RayMonster()
    {
        Vector2 myThisVector2D=new Vector2(transform.position.x,transform.position.y-myNumbY);
        myRaycastHit2DMonster=Physics2D.Raycast(myThisVector2D,Vector2.down,myNumbL);
        Debug.DrawRay(myThisVector2D,Vector3.down*myNumbL,Color.blue);//显示检测射线；
        if(myRaycastHit2DMonster&&myRaycastHit2DMonster.transform.gameObject.name=="Snake")
        {
            float x= UnityEngine.Random.Range(3,10);
            transform.position=new Vector3(StartPosion.x-x,transform.position.y,transform.position.z);
            StartCoroutine(BloodNum.Instance.PlayerDamageBlood(0.5f));
        }
    }

    void FalseAttack()//设置攻击状态为false，在animation动画序列帧结尾调用（Attack_Thr）：
    {
        PublicControl.IsAttackTwo=false;
        myAnimator.SetBool("AttTwo",false);
    }

    void JumpFallAnim()//开启角色跳跃下降动画，在animation动画序列帧结尾调用；
{
    myAnimator.SetBool("Jump",false);
    myAnimator.SetBool("Jump_Fall",true); 
}

    void CloseJumpFallAnim()//关闭角色跳跃下降动画，在animation动画序列帧结尾调用；
    {
        myAnimator.SetBool("Jump_Fall",false);   
    }

}