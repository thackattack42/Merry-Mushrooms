using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SummonMinion : StateMachineBehaviour
{
    Boss_Scpt boss;
    public int rand;
    public float timer;
    public float minTime;
    public float maxTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //timer = Random.Range(minTime, maxTime);
        //boss = animator.GetComponent<Boss_Scpt>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (!boss.isSpawning && boss.numMinions < boss.maxMinions)
        //{
        //    boss.isSpawning = true;
        //    Instantiate(boss.enemyToSpawn[Random.Range(0, boss.enemyToSpawn.Length)], boss.spawnPos[Random.Range(0, boss.spawnPos.Length)].position, boss.transform.rotation);
        //    boss.numMinions++;
        //    boss.isSpawning = false;
        //}


        //if (timer <= 0)
        //{
        //    rand = Random.Range(0, 3);

        //    if (rand == 0)
        //        animator.SetTrigger("Jump Attack");
        //    else if (rand == 1)
        //        animator.SetTrigger("Shootw");
        //    else
        //        animator.SetTrigger("Idle");
        //}
        //else
        //    timer -= Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Summon Minions");
    }
}