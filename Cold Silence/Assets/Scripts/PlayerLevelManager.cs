using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static  int xp;
    public static int xp_max;
    public static int level;
    public static int skill_points;
    private bool manager_exists;
    void Start()
    {
        if(manager_exists)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        manager_exists = true;
        xp_max = 20;
        level = 1;
    }
    public static void NewLevel()
    {
        UIManager.Instance.ActivateSkillMenuButton();
        xp -= xp_max;
        level++;
        skill_points++;
        xp_max += 5;
    }
}
