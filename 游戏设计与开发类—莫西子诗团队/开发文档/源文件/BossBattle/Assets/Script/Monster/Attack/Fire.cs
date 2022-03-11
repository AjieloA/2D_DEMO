using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonsterMoveControl
{
    protected GameObject Monster;
    private void Start()
    {
        Monster=GameObject.Find("Snake").gameObject;
        Left=GameObject.Find("SceneControl").gameObject.transform.Find("Left").gameObject.transform.position;//场景左边界；
        Right=GameObject.Find("SceneControl").gameObject.transform.Find("Right").gameObject.transform.position;//场景右边界；
    }
    void Update()
    {
        transform.Translate(Vector3.right*5*Time.deltaTime);
        if(transform.position.x<Left.x||transform.position.x>Right.x)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Player")
        {
            StartCoroutine(BloodNum.Instance.PlayerDamageBlood(15));
            Destroy(this.gameObject);
        }
    }
}
