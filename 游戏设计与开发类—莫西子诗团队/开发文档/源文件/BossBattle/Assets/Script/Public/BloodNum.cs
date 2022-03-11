using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BloodNum : MonoBehaviour
{
    public static BloodNum Instance;
    public GameObject PlayerPrefab;
    public GameObject SnakePrefab;
    private GameObject PlayerPrefabParent;
    private GameObject SnakePrefabParent;
    void Start()
    {
        Instance=this;
        #if UNITY_EDITOR
        /* PlayerPrefab=AssetDatabase.LoadAssetAtPath("Assets/Prefab/BloodB.prefab",typeof(GameObject)) as GameObject;//获取预制体PlayerBloodB；
        SnakePrefab=AssetDatabase.LoadAssetAtPath("Assets/Prefab/PBloodA.prefab",typeof(GameObject)) as GameObject;//获取预制体SnakePBloodA； */
        #endif
        PlayerPrefabParent=GameObject.FindGameObjectWithTag("PlayerBlood").gameObject;
        SnakePrefabParent=GameObject.FindGameObjectWithTag("SnakeBlood").gameObject;

    }
    public IEnumerator PlayerDamageBlood(float Num)
    {
        PublicControl.PlayerBloodHit+=Num;//受伤减血血量；
        yield return new WaitForSeconds(0.1f);//0.1秒后执行实例化血条受伤背景递减效果；
        GameObject gameObject=Instantiate(PlayerPrefab,PlayerPrefabParent.transform.parent);
        gameObject.transform.SetAsFirstSibling();//将实例化的物体放置到最上层；
        StopCoroutine(BloodNum.Instance.PlayerDamageBlood(0));
    }

    public IEnumerator MonsterDamageBlood(float Num)
    {
        PublicControl.IsAttackThr=false;
        PublicControl.SnakeBloodHit+=Num;
        yield return null;
        yield return new WaitForSeconds(0.1f);
        GameObject gameObject=Instantiate(SnakePrefab,SnakePrefabParent.transform.parent);
        gameObject.transform.SetAsFirstSibling();
        StopCoroutine(BloodNum.Instance.MonsterDamageBlood(0));
    }

}
