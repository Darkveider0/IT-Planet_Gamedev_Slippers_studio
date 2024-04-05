using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text textbox;
    // Start is called before the first frame update
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
