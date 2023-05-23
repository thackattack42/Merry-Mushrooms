using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy_Scpt : Enemy_Scpt, IFireDamage
{
    public void TakeFireDamage(int dmg)
    {
        HP -= dmg * 2;
    }
}
