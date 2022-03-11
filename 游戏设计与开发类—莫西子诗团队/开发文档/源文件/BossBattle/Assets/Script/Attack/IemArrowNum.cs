using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IemArrowNum : AttackControl
{
    void Update() 
    {
        if(PublicControl.ArrowNum<5&&PublicControl.IsIem==false)
        {
            StartCoroutine(IsIemArrow());
        }
    }
    IEnumerator IsIemArrow()
    {
        PublicControl.IsIem=true;
        PublicControl.IemLastTime=Time.time;
        yield return new WaitForSeconds(PublicControl.IemCoolTime);
        PublicControl.ArrowNum++;
        if(PublicControl.ArrowNum<5)
        {
            StartCoroutine(IsIemArrow());
        }
        else
        {
            PublicControl.IsIem=false;
            StopCoroutine(IsIemArrow());
        }
    }
}
