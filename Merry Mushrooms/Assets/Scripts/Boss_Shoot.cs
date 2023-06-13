using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Shoot : StateMachineBehaviour
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
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Shoot");
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
