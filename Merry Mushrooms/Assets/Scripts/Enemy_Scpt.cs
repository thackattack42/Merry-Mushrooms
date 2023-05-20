using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Scpt : MonoBehaviour, IDamage, IPhysics
{
    [Header("------ Enemy Stats ------")]
    // [Range(5, 100)][SerializeField] public int maxEnemyHP;
    [Range(5, 100)][SerializeField] public int EnemyHP;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;
    [SerializeField] float animrTransSpeed;
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] Animator animr;
    [SerializeField] AudioSource aud;

    [Header("------ Enemy Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(0.1f, 10)][SerializeField] float ShootRate;
    [Range(30, 180)][SerializeField] float ShootAngle;
    [SerializeField] GameObject bullet;

    [Header("------ Audio ------")]
    [SerializeField] AudioClip[] audShoot;

    [Header("------ Audio Vol ------")]
    [SerializeField] float audShootVol;
    //Other Assets
    Color origColor;
    private bool isShooting;
    Vector3 playerDir;
    bool playerInRange;
    float angleToPlayer;
    float speed;
    bool DestChosen;
    Vector3 startingPos;
    float stoppingDistOrig;
    float viewDistOrig;

    // Start is called before the first frame update
    void Start()
    {
        //gets original color and sets it here
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        gameManager.instance.UpdateGameGoal(1);
        origColor = model.material.color;
        viewDistOrig = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {

            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animrTransSpeed);
            animr.SetFloat("Speed", speed);

            if (playerInRange && !canSeePlayer())
            {
                StartCoroutine(Roam());
            }
            else if (agent.destination != gameManager.instance.player.transform.position)
            {
                StartCoroutine(Roam());
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
        animr.SetTrigger("Shoot");
        //GameObject bulletClone = Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;
    }
    public void createBullet()
    {
        aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);
        Instantiate(bullet, shootPos.position, transform.rotation);
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
            agent.stoppingDistance = 0;
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
                agent.stoppingDistance = stoppingDistOrig;
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FacePlayer();
                }

                if (!isShooting && angleToPlayer <= ShootAngle)
                {
                    //aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);
                    StartCoroutine(shoot());
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }
    IEnumerator Roam()
    {
        if (!DestChosen && agent.remainingDistance < 0.05f)
        {
            DestChosen = true;
            agent.stoppingDistance = 0;
            yield return new WaitForSeconds(roamPauseTime);
            DestChosen = false;

            Vector3 ranPos = Random.insideUnitSphere * roamDist;
            ranPos += startingPos;

            NavMeshHit hitdest;
            NavMesh.SamplePosition(ranPos, out hitdest, roamDist, 1);

            agent.SetDestination(hitdest.position);
        }
    }

    void OnEnable()
    {
        PlayerController.Crouch += ReduceVision;
        PlayerController.Uncrouch += IncreaseVision;
    }

    void OnDisable()
    {
        PlayerController.Crouch -= ReduceVision;
        PlayerController.Uncrouch -= IncreaseVision;
    }

    void ReduceVision()
    {
        if (!canSeePlayer())
            GetComponent<SphereCollider>().radius /= 2;
       
    }

    void IncreaseVision()
    {
        GetComponent<SphereCollider>().radius = viewDistOrig;
    }
    public void KnockBack(Vector3 dir)
    {
        agent.velocity += dir;
    }
        
}

