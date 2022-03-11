using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
  public static ShadowPool instance;
  public GameObject Prefab;
  private float PrefabeCount=10;
  private Queue<GameObject> PrefabQueue=new Queue<GameObject>();
  private void Start()
  {
      instance=this;
      PrefabFull();
  }

  private void PrefabFull()
  {
    for(int i=0;i<PrefabeCount;i++)
    {
      var NewPrefab=Instantiate(Prefab);
      NewPrefab.transform.SetParent(transform);
      ReturnPool(NewPrefab);
    }
  }
  
  public void ReturnPool(GameObject gameObject)
  {
    gameObject.SetActive(false);
    PrefabQueue.Enqueue(gameObject);
  }

  public GameObject GetPool()
  {
    if(PrefabQueue.Count==0)
    {
      PrefabFull();
    }
    var GetPrefab=PrefabQueue.Dequeue();
    GetPrefab.SetActive(true);
    return GetPrefab;
  }
}
