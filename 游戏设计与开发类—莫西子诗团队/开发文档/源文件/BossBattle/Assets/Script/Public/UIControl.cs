using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    [Header("图片控制")]
    public Image[] ReadyTines;
    public TMP_Text[] TextShows;
    public Image[] BloodNums;
    public GameObject[] Over=new GameObject[2];
    public GameObject StopG;
    
    private void Start()
    {
        Over[1].SetActive(false);
        Over[0].SetActive(false);
        StopG.SetActive(false);
    }
    private void Update() 
    {
        ReadyTime();
        TextShow();
        BloodNum();
        DestoryTh();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopG.SetActive(true);
            Time.timeScale=0;
        }
    }

    void ReadyTime()
    {
        ReadyTines[0].fillAmount=(PublicControl.DashLastTime+PublicControl.DashCoolTime-Time.time)/PublicControl.DashCoolTime;//Dash；
        ReadyTines[1].fillAmount=0;//AttackOne；
        ReadyTines[2].fillAmount=(PublicControl.AttackTwoLastTime+PublicControl.AttackTwoCoolTime-Time.time)/PublicControl.AttackTwoCoolTime;//AttackTwo；
        
        if(PublicControl.IsIem==false)
        {
            ReadyTines[3].fillAmount=0;//AttackThr；
        }
        else
        {
            ReadyTines[3].fillAmount=(PublicControl.IemCoolTime+PublicControl.IemLastTime-Time.time)/PublicControl.IemCoolTime;//AttackThr；
        }
    }

    void BloodNum()
    {
        BloodNums[0].fillAmount=(PublicControl.PlayerBlood-PublicControl.PlayerBloodHit)/PublicControl.PlayerBlood;//Player血量；
        BloodNums[1].fillAmount=(PublicControl.SnakeBlood-PublicControl.SnakeBloodHit)/PublicControl.SnakeBlood;//Snake血量；
    }

    void TextShow()
    {
        TextShows[0].text=PublicControl.ArrowNum.ToString();
        TextShows[1].text=(PublicControl.SnakeBlood-PublicControl.SnakeBloodHit).ToString();
    }

    void DestoryTh()
    {
        if(PublicControl.PlayerBlood-PublicControl.PlayerBloodHit<=0)
        {
            Over[0].SetActive(true);
            Time.timeScale=0;
        }
        if(PublicControl.SnakeBlood-PublicControl.SnakeBloodHit<=0)
        {
            Over[1].SetActive(true);
            Time.timeScale=0;
        }
    }

    public void Again()
    {
        PublicControl.SnakeBloodHit=0;
        PublicControl.PlayerBloodHit=0;
        PublicControl.DashCoolTime=3f;
        PublicControl.DashLastTime=-3f;
        PublicControl.IemCoolTime=5;
        PublicControl.AttackTwoLastTime=-7f;
        PublicControl.AttackTwoCoolTime=7f;
        PublicControl.PlayerBlood=100;
        PublicControl.SnakeBlood=1000;
        PublicControl.MonsterAttackOneStartTime=-2f;
        PublicControl.MonsterWaitTime=3f;
        PublicControl.MonsterAttackTwoStartTime=-5;
        PublicControl.MonsterAttackTwoWaitTime=10f;
        PublicControl.IsIem=false;
        PublicControl.IsMove=false;
        PublicControl.IsVelocityX=false;
        PublicControl.IsJump=false;
        PublicControl.IsLand=false;
        PublicControl.IsAttack=false;
        PublicControl.IsDash=false;
        PublicControl.IsAttackThr=false;
        PublicControl.IsMonsterLoopRun=true;
        PublicControl.IsMonsterTrackRun=false;
        PublicControl.IsMonsterAttackTwo=false;
        
        Time.timeScale=1;
        Over[1].SetActive(false);
        Over[0].SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OverGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying=false;
    }

    public void Comuni()
    {
        StopG.SetActive(false);
        Time.timeScale=1;
    }

}
