using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expirience : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetXp();
    }
    void SetXp()
    {
        gameObject.GetComponent<Image>().fillAmount = (float)PlayerLevelManager.xp / (float)PlayerLevelManager.xp_max;
    }

}
