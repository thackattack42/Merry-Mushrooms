using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy Stats ------")]
    // [Range(5, 100)][SerializeField] public int maxEnemyHP;
    [Range(5, 100)][SerializeField] public int EnemyHP;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;
    [SerializeField] float animrTransSpeed;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] Animator animr;

    [Header("------ Enemy Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(0.1f, 10)][SerializeField] float ShootRate;
    [Range(30, 180)][SerializeField] float ShootAngle;
    [SerializeField] GameObject bullet;
    //
    //Other Assets
    Color origColor;
    private bool isShooting;
    Vector3 playerDir;
    bool playerInRange;
    float angleToPlayer;
    float speed;


    // Start is called before the first frame update
    void Start()
    {
        //gets original color and sets it here
        gameManager.instance.UpdateGameGoal(1);
        origColor = model.material.color;

        // EnemyHP = maxEnemyHP; //changed
        // gameManager.instance.enemyHPSlider.fillAmount = 1f; //changed

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {

            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animrTransSpeed);
            animr.SetFloat("Speed", speed);

            if (playerInRange && canSeePlayer())
            {



            }
        }
    }

    public void takeDamage(int dmg) //this make it that enemy takes damage
    {
        EnemyHP -= dmg;
        // playerInRange = true;
        if (EnemyHP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            //Destroy(gameObject);
            animr.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());

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
        playerDir = gameManager.instance.player.transform.position - headPos.position;
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

