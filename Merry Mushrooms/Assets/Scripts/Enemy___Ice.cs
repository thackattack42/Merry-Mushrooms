using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy___Ice : MonoBehaviour, IFireDamage, IEarthDamage, IIceDamage, IDamage, IPhysics
{
    Enemy_Scpt enemy;

    float timer ;
    int damage;
    

    [SerializeField] public int knockbackPower;
    private void Start()
    {
       
        enemy = GetComponent<Enemy_Scpt>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (gameManager.instance.playerScript.onFire)
        {
            StartCoroutine(FireDamageTime());
            


        }
    }
    public void KnockBack(Vector3 dir)
    {
        GetComponent<NavMeshAgent>().velocity += dir;
    }
    public void TakeFireDamage(int dmg)
    {
        enemy.HP -= dmg * 2;
        gameManager.instance.playerScript.onFire = true;
        
        damage = dmg;
        StartCoroutine(FireDamageTime());
        StartCoroutine(WaitForThing());
        if (enemy.HP <= 0 && !gameManager.instance.playerScript.onFire)
        {
            enemy.animr.SetBool("Death", true);
            enemy.agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(enemy.EnemyDespawn());
            return;
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
        KnockBack(new Vector3(0, 0, knockbackPower));
       
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

    public void TakeIceDamage(int dmg)
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
    IEnumerator FireDamageTime()
    {
        gameManager.instance.playerScript.onFiref = true;
        for (int i = 0; i < timer; i++)
        {
            if (timer >= 1)
            {
                timer = 0f;
                enemy.HP -= damage;
                StartCoroutine(enemy.FlashHitColor());
            }
            
        }
        if (enemy.HP <= 0)
        {
            //TakeFireDamage(damage);
            enemy.animr.SetBool("Death", true);
            enemy.agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(enemy.EnemyDespawn());
            gameManager.instance.playerScript.onFire = false;
            yield break;
        }
        else
        {
            enemy.animr.SetTrigger("Damaged");
            enemy.agent.SetDestination(gameManager.instance.player.transform.position);

        }
        yield return new WaitForSeconds(3);
        
    }
    IEnumerator WaitForThing()
    {
        yield return new WaitForSeconds(3);
        if(gameManager.instance.playerScript.onFire)
        {
            gameManager.instance.playerScript.onFire = false;
        }
    }
    }
