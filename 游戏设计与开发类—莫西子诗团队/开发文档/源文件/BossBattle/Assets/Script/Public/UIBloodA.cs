using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBloodA : MonoBehaviour
{
    private Image BloodBImage;
    private void Awake()
    {
        BloodBImage=GetComponent<Image>();
        BloodBImage.fillAmount=(PublicControl.SnakeBlood-PublicControl.SnakeBloodHit)/PublicControl.SnakeBlood;   
    }

    void Des()
    {
        Destroy(this.gameObject);
    }
}
