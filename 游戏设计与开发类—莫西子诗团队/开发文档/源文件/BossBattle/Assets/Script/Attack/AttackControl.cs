using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class AttackControl : MonoBehaviour
{
    protected Animator myAnimator;
    protected GameObject myPlayer;
    protected Vector2 Left;
    protected Vector2 Right;
    public GameObject myPrefabArrow;
    public GameObject myPrefabDestr;
    protected GameObject myTemporary;
    void Awake()
    {
        myAnimator=GameObject.Find("Player").GetComponent<Animator>();
        myPlayer=GameObject.Find("Player").gameObject;
        Left=GameObject.Find("SceneControl").gameObject.transform.Find("Left").gameObject.transform.position;//场景左边界；
        Right=GameObject.Find("SceneControl").gameObject.transform.Find("Right").gameObject.transform.position;//场景右边界；
        myTemporary=GameObject.Find("Temporary").gameObject;//存放实例化的箭；
        /* #if UNITY_EDITOR
        myPrefabArrow=AssetDatabase.LoadAssetAtPath("Assets/Prefab/jian.prefab",typeof(GameObject)) as GameObject;//获取预制体箭；
        myPrefabDestr=AssetDatabase.LoadAssetAtPath("Assets/Prefab/Blood.prefab",typeof(GameObject)) as GameObject;//获取预制体箭销毁特效；
        #endif */
    }
}
