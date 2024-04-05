using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Enemy
{
    public int attack;
    // Start is called before the first frame update
    void Start()
    {
        setAttackDamage(attack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
