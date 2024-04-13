using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text textbox;
    void Start()
    {
        //textbox = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //textbox.text = Player.Instance.level.ToString();
        //SetLevel();
    }
    public void SetLevel(int level)
    {
        
    }

}
