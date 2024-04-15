using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject skillMenuButton;
    public static UIManager Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ActivateSkillMenuButton()
    {
        skillMenuButton.SetActive(true);
    }
    public void DeactivateSkillMenuButton()
    {
        skillMenuButton.SetActive(false);
    }
}
