using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy_Scpt : Enemy_Scpt, IEarthDamage, IIceDamage, IFireDamage
{
    public void TakeEarthDamage(int dmg)
    {
        HP -= dmg * 2;

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            animr.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }


    public void TakeIceDamage(int dmg)
    {
        HP -= dmg / 2;

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            animr.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
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
        HP += dmg;
    }
}