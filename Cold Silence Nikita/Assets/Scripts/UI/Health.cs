using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
    }
    public void SetHealth(float health,float max_health)
    {
        gameObject.GetComponent<Image>().fillAmount = max_health/health;
    }
    public void SetHealth()
    {
        gameObject.GetComponent<Image>().fillAmount = (float)Player.Instance.health / (float)Player.Instance.max_health;
    }
}
