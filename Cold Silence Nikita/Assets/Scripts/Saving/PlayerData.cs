using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData:MonoBehaviour
{
    public static int health;
    public static int max_health;
    public static int damage;
    private bool manager_exists;
    public int level, xp;
    public float[] position;

    public PlayerData(Player player)
    {
        level = PlayerLevelManager.level;
        xp = PlayerLevelManager.xp;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
    void Start()
    {
        if (manager_exists)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        manager_exists = true;
    }
    public static void SavePlayerData()
    {
        health = Player.Instance.health;
        max_health = Player.Instance.max_health;
        damage = Player.Instance.damage;
    }
}
