using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOneAnimFalse : MonoBehaviour
{
    private CircleCollider2D MyCircleCollider2D;
    private void Start()
    {
        MyCircleCollider2D=GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Player")
        {
            StartCoroutine(BloodNum.Instance.PlayerDamageBlood(10));
        }
    }
}
