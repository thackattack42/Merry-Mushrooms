using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy Stats ------")]
    [Range(5, 100)][SerializeField] public int maxEnemyHP;
    [Range(5, 100)][SerializeField] public int curEnemyHP;
    [Range(5, 100)][SerializeField] int EnemyHP;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;

    [Header("------ Enemy Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(1, 10)][SerializeField] float ShootRate;
    [Range(90, 180)][SerializeField] float ShootAngle;
    [SerializeField] GameObject bullet;
    //
    //Other Assets
    Color origColor;
    private bool isShooting;
    Vector3 playerDir;
    bool playerInRange;
    float angleToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //gets original color and sets it here
        gameManager.instance.UpdateGameGoal(1);
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canSeePlayer())
        {

            

        }
    }

    public void takeDamage(int dmg) //this make it that enemy takes damage
    {
        EnemyHP -= dmg;
        StartCoroutine(FlashHitColor());
        agent.SetDestination(gameManager.instance.player.transform.position);
        playerInRange = true;
        if (EnemyHP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }
    IEnumerator FlashHitColor() //flash when the enemy is hit
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
    IEnumerator shoot()
    {
        isShooting = true;
        GameObject bulletClone = Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;
    }
    public void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    private bool canSeePlayer()
    {
        playerDir = gameManager.instance.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);
        Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
            {
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
        return false;
    }
   
}

