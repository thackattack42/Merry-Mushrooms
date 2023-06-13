using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Phase2 : StateMachineBehaviour
{
    Boss_Scpt boss;
    NavMeshAgent agent;
    int rand;
    float timer;
    public float minTime;
    public float maxTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        boss = animator.GetComponent<Boss_Scpt>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 5;

        //animator.ResetTrigger("Shoot");
        //animator.ResetTrigger("Summon Minions");
        //animator.ResetTrigger("Jump Attack");
        //animator.ResetTrigger("Idle");
        animator.ResetTrigger("Punch");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.isActiveAndEnabled && agent.remainingDistance > agent.stoppingDistance)
        {
            agent.SetDestination(gameManager.instance.player.transform.position);

        }

        //    if (agent.isActiveAndEnabled)
        //    {
        //        if (agent.remainingDistance <= agent.stoppingDistance)
        //        {
        //            animator.SetTrigger("Idle");
        //        }
        //    }

        //    if (timer <= 0)
        //    {
        //        if (boss.numMinions <= 0)
        //        {
        //            rand = Random.Range(0, 3);

        //            if (rand == 0)
        //                animator.SetTrigger("Jump Attack");
        //            else if (rand == 1)
        //                animator.SetTrigger("Shoot");
        //            else
        //                animator.SetTrigger("Summon Minions");
        //        }
        //        else
        //        {
        //            rand = Random.Range(0, 2);
        //            if (rand == 0)
        //                animator.SetTrigger("Jump Attack");
        //            else
        //                animator.SetTrigger("Shoot");
        //        }
        //    }
        //    else
        //        timer -= Time.deltaTime;
        //}

        //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{

        //}
    }
}
