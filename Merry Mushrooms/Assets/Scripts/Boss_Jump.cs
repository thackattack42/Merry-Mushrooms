using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Boss_Jump : StateMachineBehaviour
{
    [SerializeField] GameObject aoe;
    NavMeshAgent agent;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;
    Vector3 velocity;

    Boss_Scpt boss;
    Boss_AOE aoeAttack;
    public int rand;
    public float timer;
    public float minTime;
    public float maxTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        boss = animator.GetComponent<Boss_Scpt>();
        aoeAttack = animator.GetComponent<Boss_AOE>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        velocity.y += jumpHeight;
        velocity.y -= gravity * Time.deltaTime;

        //aoeAttack.AOEAttack();

        //if (timer <= 0)
        //{
        //    if (boss.numMinions < boss.maxMinions)
        //    {
        //        rand = Random.Range(0, 3);

        //        if (rand == 0)
        //            animator.SetTrigger("Idle");
        //        else if (rand == 1)
        //            animator.SetTrigger("Shoot");
        //        else
        //            animator.SetTrigger("Summon Minions");
        //    }
        //    else
        //    {
        //        rand = Random.Range(0, 2);
        //        if (rand == 0)
        //            animator.SetTrigger("Idle");
        //        else
        //            animator.SetTrigger("Shoot");
        //    }
        //}
        //else
        //    timer -= Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Jump Attack");
    }
}
