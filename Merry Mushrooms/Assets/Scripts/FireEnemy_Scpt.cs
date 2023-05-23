using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy_Scpt : Enemy_Scpt, IEarthDamage, IIceDamage
{
    public void TakeEarthDamage(int dmg)
    {
        HP -= dmg * 2;
    }


    public void TakeIceDamage(int dmg)
    {
        HP -= dmg / 2;
    }
}