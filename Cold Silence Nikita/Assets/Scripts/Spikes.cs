using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Enemy
{
    public int attack;
    void Start()
    {
        setAttackDamage(attack);
    }
}
