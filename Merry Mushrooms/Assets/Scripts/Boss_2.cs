using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_2 : MonoBehaviour
{
    Boss_Scpt boss;
    //NavMeshAgent agent;
    public int rand;
    public float timer;
    public float minTime;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        boss = GetComponent<Boss_Scpt>();
        //boss.agent = GetComponent<NavMeshAgent>();



        //boss.anim.ResetTrigger("Shoot");
        //boss.anim.ResetTrigger("Summon Minions");
        //boss.anim.ResetTrigger("Jump Attack");
        //boss.anim.ResetTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(timeee());
        if (boss.agent.isActiveAndEnabled && boss.agent.remainingDistance >= boss.agent.stoppingDistance)
        {
            boss.agent.SetDestination(gameManager.instance.player.transform.position);

        }

        if (boss.agent.isActiveAndEnabled)
        {
            if (boss.agent.remainingDistance <= boss.agent.stoppingDistance)
            {
                boss.anim.SetTrigger("Idle");
            }
        }

        if (timer <= 0)
        {
            if (boss.numMinions <= 0 && boss.playerInRange == true)
            {
                rand = Random.Range(0, 3);

                if (rand == 0)
                    boss.anim.SetTrigger("Jump Attack");
                else if (rand == 1)
                    boss.anim.SetTrigger("Shoot");
                else
                    boss.anim.SetTrigger("Summon Minions");
            }
            else
            {
                rand = Random.Range(0, 2);
                if (rand == 0)
                    boss.anim.SetTrigger("Jump Attack");
                else
                    boss.anim.SetTrigger("Shoot");
            }
        }
        else
            timer -= Time.deltaTime;
    }
    IEnumerator timeee()
    {
        yield return new WaitForSeconds(timer);
    }
}

