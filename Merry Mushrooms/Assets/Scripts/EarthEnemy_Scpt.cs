using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEnemy_Scpt : Enemy_Scpt, IIceDamage, IFireDamage, IEarthDamage
{
    public void TakeIceDamage(int dmg)
    {
        HP -= dmg * 2;

        if (HP <= 0)
        {
            animr.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(EnemyDespawn());
            StopAllCoroutines();
        }
        else
        {
            animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }


    public void TakeFireDamage(int dmg)
    {
        HP -= dmg - 1;

        if (HP <= 0)
        {
            animr.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(EnemyDespawn());
            StopAllCoroutines();
        }
        else
        {
            animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }

    public void TakeEarthDamage(int dmg)
    {
        HP += dmg;
    }
}