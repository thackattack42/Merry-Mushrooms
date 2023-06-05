using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Jump : StateMachineBehaviour
{
    Boss_Scpt boss;
    public int rand;
    public float timer;
    public float minTime;
    public float maxTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        boss = animator.GetComponent<Boss_Scpt>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (boss.numMinions < boss.maxMinions)
            {
                rand = Random.Range(0, 3);

                if (rand == 0)
                    animator.SetTrigger("Idle");
                else if (rand == 1)
                    animator.SetTrigger("Shoot Projectile");
                else
                    animator.SetTrigger("Summon Minions");
            }
            else
            {
                rand = Random.Range(0, 2);
                if (rand == 0)
                    animator.SetTrigger("Idle");
                else
                    animator.SetTrigger("Shoot Projectile");
            }
        }
        else
            timer -= Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Jump Attack");
    }
}
