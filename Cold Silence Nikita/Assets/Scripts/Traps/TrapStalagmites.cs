using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStalagmites : ITrap
{
    public void FallIntoTrap(TrapController trap) //��������� � �������
    {
        trap.GetDamage(Player.Instance.damage - 1);
    }
}
