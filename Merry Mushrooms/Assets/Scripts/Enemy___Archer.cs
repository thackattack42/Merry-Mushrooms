using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy___Archer : Enemy_Scpt
{
    [Header("------ Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(0.1f, 10)][SerializeField] float ShootRate;
    [Range(30, 180)][SerializeField] float ShootAngle;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Bullet bulletScript;
    //[SerializeField] int bulletDamage;
    private bool isShooting;
    private bool canShoot = true;

    private void SetBulletDamage()
    {
        bulletScript.damage = level;
    }

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

                if (!isShooting && angleToPlayer <= ShootAngle && !gameManager.instance.playerScript.isUnderAttack && canShoot)
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
        gameManager.instance.playerScript.isUnderAttack = true;
        animr.SetTrigger("Shoot");
        yield return new WaitForSeconds(ShootRate);
        gameManager.instance.playerScript.isUnderAttack = false;
        isShooting = false;
        StartCoroutine(AttackCooldown());
    }
    public void createBullet()
    {
        SetBulletDamage();
        Instantiate(bullet, shootPos.position, transform.rotation);
    }
    IEnumerator AttackCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        canShoot = true;
    }

    #endregion
}
