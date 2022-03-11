using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBloodB : MonoBehaviour
{
    private Image BloodBImage;
    private void Awake()
    {
        BloodBImage=GetComponent<Image>();
        BloodBImage.fillAmount=(PublicControl.PlayerBlood-PublicControl.PlayerBloodHit)/PublicControl.PlayerBlood;   
    }

    void Des()
    {
        Destroy(this.gameObject);
    }
}
