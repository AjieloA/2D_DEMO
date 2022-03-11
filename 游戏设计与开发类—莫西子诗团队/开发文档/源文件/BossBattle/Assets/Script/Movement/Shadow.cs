using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private SpriteRenderer PlayerSpriteRenderer;
    private SpriteRenderer ThisSperiteRenderer;
    private Transform PlayerTransform;
    private Color ThisColor;

    [Header("时间控制")]
    public float ShowTime;//显示时长；
    private float StartTime;//残影开始时间点；
    private float OverTime;//残影接受时间；

    [Header("不透明度控制")]
    private float Alpha;//不透明度；
    public float AlphaSet;//不透明度初始值；
    public float AlphaMultiplier;//不透明度衰减；
    private void OnEnable()
    {
        PlayerTransform=GameObject.Find("Player").transform;
        PlayerSpriteRenderer=PlayerTransform.GetComponent<SpriteRenderer>();//获取palyer当前序列帧图片；
        ThisSperiteRenderer=GetComponent<SpriteRenderer>();//获取shadow当前序列帧图片；

        Alpha=AlphaSet;//设置Alpha初始值；

        ThisSperiteRenderer.sprite=PlayerSpriteRenderer.sprite;//设置shadow当前序列帧图片；
        transform.position=PlayerTransform.position;//设置shadow位置；
        transform.rotation=PlayerTransform.rotation;//设置shadow旋转；
        transform.localScale=PlayerTransform.localScale;//设置shadow缩放；
        PublicControl.ShadowRotationY=PlayerTransform.rotation.y;//获取Shadow朝向；

        StartTime=Time.time;
    }
    private void Update() 
    {
        Alpha*=AlphaMultiplier;
        ThisColor=new Color(0.5f,1,1,Alpha);
        ThisSperiteRenderer.color=ThisColor;
        if(Time.time>=StartTime+ShowTime)//当当前游戏时间大于shadow加载开始时间+shadow展示时间，则将该shadow放回对象池；
        {
            ShadowPool.instance.ReturnPool(this.gameObject);//放回对象池；
        }
    }
}
