using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy___Melee : Enemy_Scpt
{
    [Header("------ Weapon Stats ------")]
    [Range(0.1f, 10)][SerializeField] float AttackRate;
    [Range(30, 180)][SerializeField] float AttackAngle;
    [SerializeField] BoxCollider MeleeObj;
    private bool isAttacking;
    private bool canAttack = true;

    [Header("------ Components ------")]
    [SerializeField] MeleeWeapon_Scpt meleeScript;

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

                if (!isAttacking && angleToPlayer <= AttackAngle && !gameManager.instance.playerScript.isUnderAttack && canAttack)
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
        SetAttackDamage();
        isAttacking = true;
        gameManager.instance.playerScript.isUnderAttack = true;
        animr.SetTrigger("MeleeAttack");
        if (audShoot.Length != 0)
        {
            aud.PlayOneShot(audShoot[0], audShootVol);

        }
        yield return new WaitForSeconds(AttackRate);
        gameManager.instance.playerScript.isUnderAttack = false;
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }
    public void AttackingOn()
    {
        MeleeObj.enabled = true;
    }
    public void AttackingOff()
    {
        MeleeObj.enabled = false;
    }

    void SetAttackDamage()
    {
        meleeScript.dmg = level;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        canAttack = true;
    }
    #endregion

}
