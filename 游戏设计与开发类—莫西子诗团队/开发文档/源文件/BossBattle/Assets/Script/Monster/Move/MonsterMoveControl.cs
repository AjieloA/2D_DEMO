using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterMoveControl : MonoBehaviour
{
    protected Transform[] Location= new Transform[2];
    protected Rigidbody2D myRigidbody;
    protected GameObject Player;
    protected Animator myAnimator;

    [Header("Raycast检测控制")]
    protected LayerMask PlayerLayer;
    protected RaycastHit2D TrackRaycastHit2D;//角色进入范围进行跟随角色移动；
    protected Collider2D NorTrackRaycastHit2D;//角色进入范围进行跟随角色移动；
    protected RaycastHit2D AttackRaycastHit2D;//角色进入范围开始攻击；
    protected RaycastHit2D AttackTwoRaycasthit2D;//二技能攻击检测；
    protected float TrackNumX=4;//检测跟随范围；
    protected float AttackNumx=2.5f;//检测攻击范围；
    protected float TrackSpeed=2;//跟随速度；
    protected float LoopRunSpeed=1;//LoopRun速度；

    public GameObject AttackTwoPrefab;
    protected GameObject AttackTwoParent;
    protected Vector2 Left;
    protected Vector2 Right;
    protected GameObject myTemporary;
    
    private void Awake()
    {
        /* #if UNITY_EDITOR
        AttackTwoPrefab=AssetDatabase.LoadAssetAtPath("Assets/Prefab/BoosAttackTwoShow.prefab",typeof(GameObject)) as GameObject;
        #endif */
        AttackTwoParent=GameObject.Find("Snake").gameObject.transform.Find("AttackTwo").gameObject;
    }
    void Start()
    {
        myTemporary=GameObject.Find("Temporary").gameObject;//存放实例化的箭；
        myAnimator=GetComponent<Animator>();
        Player=GameObject.Find("Player").gameObject;
        PlayerLayer=LayerMask.GetMask("Player");
        myRigidbody=GetComponent<Rigidbody2D>();
        ChildLocation();
    }

    
    void Update()
    {
        RayPlayer();
        if(PublicControl.IsMonsterTrackRun==true)
        {
            TrackRun();
        }
    }
    void FixedUpdate()
    {
        if(PublicControl.IsMonsterLoopRun==true)
        {
            LoopRun();
        }
    }
    void ChildLocation()
    {
        for(int i=0;i<=1;++i)
        {
            Location[i]=this.gameObject.transform.GetChild(i).gameObject.transform;
        }
        for(int i=0;i<=1;++i)
        {
            Location[i].transform.parent=null; 
        }
    }

    void LoopRun()//巡逻；
    {
        if(transform.position.x<=Location[0].position.x)
        {
            transform.localScale=new Vector3(1,1,1);
        }
        else if(transform.position.x>=Location[1].position.x)
        {
            transform.localScale=new Vector3(-1,1,1);
        }
        if(transform.position.x>=Location[0].position.x&&transform.position.x<=Location[1].position.x)
        {
            myAnimator.SetBool("Walk",true);
            myRigidbody.velocity=new Vector2(transform.localScale.x*1,myRigidbody.velocity.y);
        } 
    }

    void TrackRun()//跟随角色移动；
    {
        if(Player.transform.position.x>transform.position.x)
        {
            transform.localScale=new Vector3(1,1,1);
        }
        else if(Player.transform.position.x<transform.position.x)
        {
            transform.localScale=new Vector3(-1,1,1);
        }
        myAnimator.SetBool("Walk",true);
        transform.position=Vector3.MoveTowards(transform.position,new Vector3(Player.transform.position.x,transform.position.y,transform.position.z),TrackSpeed*Time.deltaTime);
    }

    void RayPlayer()
    {
        Vector2 test=new Vector2(transform.position.x,transform.position.y+0.1f);//检测Y轴偏移值；
        TrackRaycastHit2D=Physics2D.Raycast(test,Vector2.right*transform.localScale.x,TrackNumX,PlayerLayer);//跟随检测；
        Debug.DrawRay(test,Vector3.right*TrackNumX*transform.localScale.x,Color.blue);
        NorTrackRaycastHit2D=Physics2D.OverlapCircle(transform.position,4.5f,PlayerLayer);//不跟随，恢复巡逻检测；
        AttackRaycastHit2D=Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-0.1f),Vector2.right*transform.localScale.x,AttackNumx,PlayerLayer);//AttackOne攻击检测；
        Debug.DrawRay(new Vector2(transform.position.x,transform.position.y-0.1f),Vector3.right*AttackNumx*transform.localScale.x,Color.green);
        AttackTwoRaycasthit2D=Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,3.5f,PlayerLayer);//AttackTwo检测；
        Debug.DrawRay(transform.position,Vector3.right*3.5f*transform.localScale.x,Color.black);

        if(TrackRaycastHit2D&&TrackRaycastHit2D.transform.gameObject.name=="Player")//跟随角色；
        {
            PublicControl.IsMonsterLoopRun=false;
            PublicControl.IsMonsterTrackRun=true;
        }

        if(PublicControl.IsMonsterTrackRun==true)//恢复巡逻；
        {
            if(!NorTrackRaycastHit2D)
            {
                PublicControl.IsMonsterLoopRun=true;
                PublicControl.IsMonsterTrackRun=false;
            }   
        }
        if(PublicControl.IsMonsterAttackTwo==false)
        {
            if(PublicControl.IsMonsterLoopRun==false&&PublicControl.IsMonsterTrackRun==false)
            {
                //PublicControl.IsMonsterTrackRun=true;
            }
        }

        if(AttackRaycastHit2D&&AttackRaycastHit2D.transform.gameObject.name=="Player")//检测攻击1范围；
        {
            if(PublicControl.MonsterAttackOneStartTime+PublicControl.MonsterWaitTime<Time.time)
            {
                myAnimator.SetBool("AttackOne",true);
                PublicControl.MonsterAttackOneStartTime=Time.time;
            }
            myAnimator.SetBool("Walk",false);
            PublicControl.IsMonsterLoopRun=false;
            PublicControl.IsMonsterTrackRun=false;
            
        }

        if(AttackTwoRaycasthit2D&&AttackTwoRaycasthit2D.transform.gameObject.name=="Player")//检测攻击2范围；
        {
            if((PublicControl.MonsterAttackTwoStartTime+PublicControl.MonsterAttackTwoWaitTime<Time.time))
            {
                if(Mathf.Abs((AttackTwoRaycasthit2D.transform.position.x-transform.position.x))>=3.5f)
                {
                    
                    PublicControl.IsMonsterAttackTwo=true;
                    PublicControl.IsMonsterTrackRun=false;
                    PublicControl.IsMonsterLoopRun=false;
                    myAnimator.SetBool("Walk",false);
                    myAnimator.SetBool("AttackTwo",true);   
                    PublicControl.MonsterAttackTwoStartTime=Time.time;
                }
                
            }
        }
    }

    void OnDrawGizmosSelected()//显示圆形检测范围；
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,3);
    }

    void AttackOneFalse()
    {
        myAnimator.SetBool("AttackOne",false);
    }

    void AttackTwoFalse()
    {
        myAnimator.SetBool("AttackTwo",false);
        PublicControl.IsMonsterAttackTwo=false;
    }

    void AttackTwoLocax()
    {
        PublicControl.MonsterLocaX=this.transform.localScale.x;
    }
    void AttackTwoShow()
    {
        if(PublicControl.MonsterLocaX==1)
        {
            GameObject gameObject=Instantiate(AttackTwoPrefab,AttackTwoParent.transform.position,Quaternion.Euler(0,0,0));
            gameObject.transform.parent=myTemporary.transform;
        }
        if(PublicControl.MonsterLocaX==-1)
        {
            GameObject gameObject=Instantiate(AttackTwoPrefab,AttackTwoParent.transform.position,Quaternion.Euler(0,180,0));
            gameObject.transform.parent=myTemporary.transform;
        }
    }
}
