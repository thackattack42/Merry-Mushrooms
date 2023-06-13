using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy___Fire : MonoBehaviour, IEarthDamage, IIceDamage, IFireDamage, IDamage, IPhysics
{
    Enemy_Scpt enemy;

    float origSpeed;
    NavMeshAgent agent;
    [SerializeField] public int knockbackPower;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy_Scpt>();
        origSpeed = agent.speed;
    }
    public void TakeEarthDamage(int dmg)
    {
        KnockBack(new Vector3(0, 0, knockbackPower));
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
    public void KnockBack(Vector3 dir)
    {
        GetComponent<NavMeshAgent>().velocity += dir;
    }

    public void TakeIceDamage(int dmg)
    {
        enemy.HP -= dmg / 2;


        StartCoroutine(SlowDownCoolDown());
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

    IEnumerator SlowDownCoolDown()
    {

        agent.speed /= 2;

        yield return new WaitForSeconds(3);

        
        agent.speed = origSpeed;
    }
}
