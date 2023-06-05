using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy___Earth : MonoBehaviour, IIceDamage, IFireDamage, IEarthDamage, IDamage
{
    Enemy_Scpt enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy_Scpt>();
    }
    public void TakeIceDamage(int dmg)
    {
        enemy.HP -= dmg * 2;

        if (enemy.HP <= 0)
        {
            enemy.animr.SetBool("Death", true);
            enemy.agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(enemy.EnemyDespawn());
        }
        else
        {
            enemy.animr.SetTrigger("Damaged");
            enemy.agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(enemy.FlashHitColor());
        }
    }


    public void TakeFireDamage(int dmg)
    {
        enemy.HP -= dmg - 1;

        if (enemy.HP <= 0)
        {
            enemy.animr.SetBool("Death", true);
            enemy.agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(enemy.EnemyDespawn());
        }
        else
        {
            enemy.animr.SetTrigger("Damaged");
            enemy.agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(enemy.FlashHitColor());
        }
    }

    public void TakeEarthDamage(int dmg)
    {
        enemy.HP += dmg;
    }
    public void takeDamage(int dmg)
    {
        enemy.HP -= dmg;

        if (enemy.HP <= 0)
        {
            enemy.animr.SetBool("Death", true);
            enemy.agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(enemy.EnemyDespawn());
        }
        else
        {
            enemy.animr.SetTrigger("Damaged");
            enemy.agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(enemy.FlashHitColor());
        }
    }
}
