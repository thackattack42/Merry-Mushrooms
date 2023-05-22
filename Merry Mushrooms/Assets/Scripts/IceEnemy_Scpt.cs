using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy_Scrpt : Enemy_Scpt, IFireDamage
{
    public void TakeFireDamage(int dmg)
    {
        EnemyHP -= dmg * 2;
    }
}
