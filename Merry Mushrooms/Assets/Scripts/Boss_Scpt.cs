using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Scpt : MonoBehaviour, IFireDamage, IEarthDamage, IIceDamage, IDamage//, IEffectable
{
    #region Fields
    [Header("------ Enemy Spawner ------")]
    [SerializeField] public GameObject[] enemyToSpawn;
    [SerializeField] public Transform[] spawnPos;

    [Header("----- Boss Stats -----")]
    [SerializeField] public int maxHP;
    [SerializeField] public int currHP;
    [SerializeField] public int numMinions;
    [SerializeField] public int maxMinions;
    [Range(5, 100)][SerializeField] public int playerFaceSpeed;
    [SerializeField] public int attackAngle;
    public int viewCone;

    [Header("----- Boss Components -----")]
    [SerializeField] public Animator anim;
    public float animrTransSpeed;
    [SerializeField] Renderer model;
    [SerializeField] Transform punchPos;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] public Transform headPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Bullet bulletScript;
    [SerializeField] public GameObject aoeAttack;

    [Header("----- Misc -----")]
    public bool playerInRange;
    public bool isSpawning;
    private bool isAttacking;
    private bool phase2;
    public Vector3 playerDir;
    public float stoppingDistOrig;
    public float angleToPlayer;
    Color origColor;
    float speed;
    float rand;


    #endregion
    #region Start and Stop
    //new
    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        anim = GetComponent<Animator>();
        stoppingDistOrig = agent.stoppingDistance;
        origColor = model.material.color;
        bulletScript.damage = 5;
        gameManager.instance.UpdateGameGoal(1);
        gameManager.instance.musicScript.BossState(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animrTransSpeed);
            anim.SetFloat("Speed", speed);

            if (playerInRange && !canSeePlayer())
            {

            }

            if (currHP <= (maxHP / 2))
            {
                phase2 = true;
                agent.stoppingDistance = 10;
                stoppingDistOrig = agent.stoppingDistance;
            }
        }
    }
    #endregion
    #region Functions
    #region Collider Enter/Exit
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
    #endregion
    public void StartPunch()
    {
        punchPos.GetComponent<SphereCollider>().enabled = true;
    }

    public void StopPunch()
    {
        punchPos.GetComponent<SphereCollider>().enabled = false;
    }

    public void AOEAttack()
    {
        Instantiate(aoeAttack, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), aoeAttack.transform.rotation);
    }

    public void SpawnMinions()
    {
        while (numMinions < maxMinions)
        {
            Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], spawnPos[Random.Range(0, spawnPos.Length)].position, transform.rotation);
            numMinions++;
        }
    }

    public void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    public virtual bool canSeePlayer()
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

                if (!isAttacking && angleToPlayer <= attackAngle)
                {
                    if (!phase2)
                    {
                        StartCoroutine(Punch());
                    }
                    else
                    {
                        anim.ResetTrigger("Punch");
                        if (numMinions <= 0)
                        {
                            rand = Random.Range(0, 3);
                            switch (rand)
                            {
                                case 0:
                                    StartCoroutine(JumpAttack());
                                    break;
                                case 1:
                                    StartCoroutine(Shoot());
                                    break;
                                case 2:
                                    StartCoroutine(SummonMinions());
                                    break;
                            }
                        }
                        else
                        {
                            rand = Random.Range(0, 2);
                            switch (rand)
                            {
                                case 0:
                                    StartCoroutine(JumpAttack());
                                    break;
                                case 1:
                                    StartCoroutine(Shoot());
                                    break;
                            }
                        }
                    }
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    IEnumerator Punch()
    {
        isAttacking = true;
        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    IEnumerator JumpAttack()
    {
        isAttacking = true;
        anim.SetTrigger("Jump Attack");
        yield return new WaitForSeconds(3);
        isAttacking = false;
        anim.ResetTrigger("Jump Attack");
    }

    IEnumerator Shoot()
    {
        isAttacking = true;
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(3);
        isAttacking = false;
        anim.ResetTrigger("Shoot");
    }
    IEnumerator SummonMinions()
    {
        isSpawning = true;
        anim.SetTrigger("Summon Minions");
        yield return new WaitForSeconds(3);
        isSpawning = false;
        anim.ResetTrigger("Summon Minions");
    }

    public void createBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }

    #region Damage Functions

    public void takeDamage(int amount)
    {
        currHP -= amount;

        if (currHP <= 0)
        {
            anim.SetBool("Death", true);
            gameManager.instance.UpdateGameGoal(-1);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);

        }
        else
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }
    public void TakeEarthDamage(int dmg)
    {
        currHP -= dmg * 2;

        if (currHP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            anim.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);
        }
        else
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }


    public void TakeIceDamage(int dmg)
    {
        currHP -= dmg / 2;

        if (currHP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            anim.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);

        }
        else
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(FlashHitColor());
        }
    }

    public void TakeFireDamage(int dmg)
    {
        currHP += dmg;
    }

    public IEnumerator FlashHitColor() //flash when the enemy is hit
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
    #endregion
    public int GetCurrHP()
    {
        return currHP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    #endregion
}
