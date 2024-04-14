using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public void GetDamage(int damage)
    {
        Player.Instance.health -= damage;
        Debug.Log("ловушка");
    }
}
