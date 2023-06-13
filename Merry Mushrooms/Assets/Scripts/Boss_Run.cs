using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Run : StateMachineBehaviour
{
    Boss_Scpt boss;
    NavMeshAgent agent;
    public int rand;
    public float timer;
    public float minTime;
    public float maxTime;

    // onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        timer = Random.Range(minTime, maxTime);
        boss = animator.GetComponent<Boss_Scpt>();
        agent = animator.GetComponent<NavMeshAgent>();

        animator.ResetTrigger("Shoot");
        animator.ResetTrigger("Summon Minions");
        animator.ResetTrigger("Jump Attack");
        animator.ResetTrigger("Idle");
    }

    // onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance && !animator.GetBool("Phase 2"))
            {
                FacePlayer();
                animator.SetTrigger("Punch");
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && animator.GetBool("Phase 2"))
            {
                FacePlayer();
                animator.SetTrigger("Idle");
            }
            else
            {
                if (timer <= 0)
                {
                    if (boss.numMinions <= 0)
                    {
                        rand = Random.Range(0, 3);

                        if (rand == 0)
                            animator.SetTrigger("Jump Attack");
                        else if (rand == 1)
                            animator.SetTrigger("Shoot");
                        else
                            animator.SetTrigger("Summon Minions");
                    }
                    else
                    {
                        rand = Random.Range(0, 2);
                        if (rand == 0)
                            animator.SetTrigger("Jump Attack");
                        else
                            animator.SetTrigger("Shoot");
                    }
                }
                else
                    timer -= Time.deltaTime;
            }
        }
    }

    // onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("Punch");
    }

    public void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(boss.playerDir.x, 0, boss.playerDir.z));
        boss.transform.rotation = Quaternion.Lerp(boss.transform.rotation, rot, Time.deltaTime * boss.playerFaceSpeed);
    }

    //public virtual bool canSeePlayer()
    //{
    //    boss.playerDir = gameManager.instance.player.transform.position - boss.headPos.position;
    //    boss.angleToPlayer = Vector3.Angle(new Vector3(boss.playerDir.x, 0, boss.playerDir.z), boss.transform.forward);
    //    agent.SetDestination(gameManager.instance.player.transform.position);

    //    RaycastHit hit;
    //    if (Physics.Raycast(boss.headPos.position, boss.playerDir, out hit))
    //    {
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            boss.agent.stoppingDistance = boss.stoppingDistOrig;

    //            if (agent.isActiveAndEnabled)
    //            {
    //                if (agent.remainingDistance < agent.stoppingDistance)
    //                {
    //                    FacePlayer();
    //                }
    //            }
    //            return true;
    //        }
    //    }
    //    agent.stoppingDistance = 0;
    //    return false;
    //}
}
