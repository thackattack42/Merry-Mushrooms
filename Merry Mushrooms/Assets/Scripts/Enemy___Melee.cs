using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy___Melee : Enemy_Scpt
{
    [Header("------ Weapon Stats ------")]
    [Range(0.1f, 10)][SerializeField] float AttackRate;
    [Range(30, 180)][SerializeField] float AttackAngle;
    [SerializeField] BoxCollider MeleeObj;
    private bool isAttacking;


    public override bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
            {
                agent.stoppingDistance = stoppingDistOrig;
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FacePlayer();
                }

                if (!isAttacking && angleToPlayer <= AttackAngle)
                {
                    //aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);
                    StartCoroutine(Attack());
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

#region Attacking Functions
IEnumerator Attack()
    {
        isAttacking = true;
        animr.SetTrigger("MeleeAttack");
        yield return new WaitForSeconds(AttackRate);
        isAttacking = false;
    }
    public void AttackingOn()
    {
        MeleeObj.enabled = true;
    }
    public void AttackingOff()
    {
        MeleeObj.enabled = false;
    }
    #endregion

}
