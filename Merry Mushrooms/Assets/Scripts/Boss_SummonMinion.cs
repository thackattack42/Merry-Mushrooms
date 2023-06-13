using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_SummonMinion : StateMachineBehaviour
{
    Boss_Scpt boss;
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss_Scpt>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.isSpawning && boss.numMinions < boss.maxMinions)
        {
            boss.isSpawning = true;
            Instantiate(boss.enemyToSpawn[Random.Range(0, boss.enemyToSpawn.Length)], boss.spawnPos[Random.Range(0, boss.spawnPos.Length)].position, boss.transform.rotation);
            boss.numMinions++;
            boss.isSpawning = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Summon Minions");

        //if (agent.isActiveAndEnabled)
        //{
        //    if (agent.remainingDistance < agent.stoppingDistance)
        //    {
        //        animator.SetTrigger("Idle");
        //    }
        //    else
        //        animator.SetTrigger("Run");
        //}
    }
}