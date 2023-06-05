using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy___Mage : Enemy_Scpt
{
    [Header("------ Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(0.1f, 10)][SerializeField] float ShootRate;
    [Range(30, 180)][SerializeField] float ShootAngle;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    private bool isShooting;

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

                if (!isShooting && angleToPlayer <= ShootAngle)
                {
                    StartCoroutine(shoot());
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    #region Shooting Functions
    IEnumerator shoot()
    {
        isShooting = true;
        animr.SetTrigger("MageAttack");
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;
    }
    public void createBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }
    #endregion
}
