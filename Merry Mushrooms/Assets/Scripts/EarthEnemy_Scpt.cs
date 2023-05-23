using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEnemy_Scpt : Enemy_Scpt, IIceDamage
{
    public void TakeIceDamage(int dmg)
    {
        HP -= dmg * 2;
    }


    //public void TakeFireDamage(int dmg)
    //{
    //    HP -= dmg / 2;
    //}
}