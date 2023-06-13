using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase_Scpts : MonoBehaviour
{
    Boss_Scpt boss;
    public int rand;
    bool phase2;
    bool breath;


    void Start()
    {
        boss = GetComponent<Boss_Scpt>();
        boss.anim.ResetTrigger("Punch");
        breath = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (boss.agent.isActiveAndEnabled)
        {
            if (boss.currHP <= boss.maxHP / 2)
            {
                phase2 = true;
            }
            if (boss.agent.remainingDistance >= boss.agent.stoppingDistance && !boss.canSeePlayer())
            {
                boss.anim.SetTrigger("Idle");
            }
            else if (boss.agent.remainingDistance >= boss.agent.stoppingDistance && boss.canSeePlayer() && !phase2)
            {
                boss.anim.SetTrigger("Run");
            }
            else if (boss.agent.remainingDistance <= boss.agent.stoppingDistance && boss.canSeePlayer() && !phase2)
            {
                boss.anim.SetTrigger("Punch");
            }
            else if (boss.agent.remainingDistance <= boss.agent.stoppingDistance && boss.canSeePlayer() && phase2 && breath)
            {
                boss.anim.SetTrigger("Phase 2");
                breath = false;
                StartCoroutine(Idlewait());

            }
            //        else if (boss.agent.remainingDistance <= boss.agent.stoppingDistance && boss.canSeePlayer() && phase2 && !breath)
            //        {

            //            if (boss.numMinions <= 0)
            //            {
            //                if (rand == 0)
            //                {
            //                    boss.anim.SetTrigger("Jump Attack");
            //                    //StartCoroutine(Idlewait());
            //                }
            //                else if (rand == 1)
            //                {
            //                    boss.anim.SetTrigger("Shoot");
            //                    //StartCoroutine(Idlewait());
            //                }
            //                else
            //                {
            //                    boss.anim.SetTrigger("Summon Minions");
            //                    //StartCoroutine(Idlewait());
            //                }

            //            }
            //            else
            //            {
            //                if (rand == 0)
            //                {
            //                    boss.anim.SetTrigger("Jump Attack");
            //                    //StartCoroutine(Idlewait());
            //                }
            //                else
            //                {
            //                    boss.anim.SetTrigger("Shoot");
            //                    //StartCoroutine(Idlewait());
            //                }
            //            }
            //            StartCoroutine(Idlewait());
            //        }
            //    }

        }
        IEnumerator Idlewait()
        {
            boss.anim.SetTrigger("Run");
            yield return new WaitForSeconds(2);
            rand = Random.Range(0, 3);
            boss.anim.ResetTrigger("Punch");
            boss.anim.ResetTrigger("Jump Attack");
            boss.anim.ResetTrigger("Shoot");
            boss.anim.ResetTrigger("Summon Minions");
        }
    }
}

