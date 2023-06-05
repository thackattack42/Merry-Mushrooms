using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase2 : StateMachineBehaviour
{
    Boss_Scpt boss;
    private int rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss_Scpt>();

        rand = Random.Range(0, 4);

        if (rand == 0)
            animator.SetTrigger("Jump Attack");
        else if (rand == 1)
            animator.SetTrigger("Shoot Projectile");
        else if (rand == 2)
            animator.SetTrigger("Idle");
        else
            animator.SetTrigger("Summon Minions");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
