using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    void Awake()
    {
        levelText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        levelText.text = PlayerLevelManager.level.ToString();
    }
    public static void SetLevel()
    {
        //levelText.text = PlayerLevelManager.level.ToString();
    }
}
