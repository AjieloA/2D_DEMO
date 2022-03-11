using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class PublicControl
{
    /* Bool型 */
    public static bool IsIem=false;//协程状态
    public static bool IsMove=false;//是否处于移动状态；
    public static bool IsVelocityX=false;//判断角色X方向是否受到力；
    public static bool IsJump=false;//是否处于跳跃状态；
    public static bool IsLand=false;//是否在地面；
    public static bool IsAttack=false;//是否处于攻击状态；
    public static bool IsDash=false;//是否处于Dash状态；
    public static bool IsAttackOne;//AttackOne攻击状态；
    public static bool IsAttackTwo;//AttackTwo攻击状态；
    public static bool IsAttackThr=false;//AttackThr攻击状态；
    public static bool IsMonsterLoopRun=true;//判断怪物是否巡逻；
    public static bool IsMonsterTrackRun=false;//判断跟随；
    public static bool IsMonsterAttackTwo=false;

    /* Float数值型 */
    public static float ArrowGoRotation=0;//弓箭发射时角色方向
    public static float ArrowNum=5;//弓箭初始数量
    public static float ShadowRotationY;//shadow状态时方向；
    public static float DashShowTimeSet;//Dash开始时间；
    public static float DashCoolTime=3f;//Dash冷却时间；
    public static float DashLastTime=-3f;//上一次Dash结束时间；
    public static float IemLastTime;//弓箭冷却开始时间；
    public static float IemCoolTime=5;//弓箭冷却时长；
    public static float AttackTwoLastTime=-7f;//AttackTwo上一次释放时间点；
    public static float AttackTwoCoolTime=7f;//AttackTwo冷却时长；
    public static float AttackTwoCoolTimeGet;//在循环中倒计时冷却；
    public static float PlayerBlood=100;//Player初始血量；
    public static float PlayerBloodHit;//Player受伤血量；
    public static float PlayerBloodHitBG;//Player受伤血量；
    public static float SnakeBlood=1000;//Snake初始血量；
    public static float SnakeBloodHit;//Snake受伤血量；
    public static float SnakeBloodHitBG;//Snake受伤血量；
    public static float MonsterAttackOneStartTime=-2f;//monster技能上一次释放的时间；
    public static float MonsterWaitTime=3f;//monster技能冷却时间；
    public static float MonsterAttackTwoStartTime=-5;//monster 2技能初始时间；
    public static float MonsterAttackTwoWaitTime=10f;//monster 2技能冷却时间；
    public static float MonsterLocaX;//monster释放技能时的朝向；
}


