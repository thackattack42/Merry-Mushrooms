using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Scpt : MonoBehaviour, IFireDamage, IEarthDamage, IIceDamage, IDamage//, IEffectable
{
    #region Fields
    [Header("------ Enemy Spawner ------")]
    [SerializeField] public GameObject[] enemyToSpawn;
    [SerializeField] public Transform[] spawnPos;
    //[SerializeField] float spawnDelayy;
    //int spawnCountt;
    //int numberSpawnedd;
    //bool isSpawningg;
    [Header("----- Boss Stats -----")]
    [SerializeField] public int maxHP;
    [SerializeField] public int currHP;
    [SerializeField] int projectileDamage;
    [SerializeField] int punchDamage;
    [SerializeField] int AOEDamage;
    [SerializeField] public int numMinions;
    [SerializeField] public int maxMinions;
    [Range(5, 100)][SerializeField] public int playerFaceSpeed;
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
    [SerializeField] public GameObject aoeAttack;

    [Header("----- Misc -----")]
    public bool playerInRange;
    public bool isSpawning;
    public Vector3 playerDir;
    public float stoppingDistOrig;
    public float angleToPlayer;
    [SerializeField] public GameObject teleporter;
    Color origColor;
    float speed;
    float rand;
    float timer;
    public float minTime;
    public float maxTime;


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
        //teleporter = GameObject.FindGameObjectWithTag("Teleporter");
        //base.Start();
        gameManager.instance.UpdateGameGoal(1);
        gameManager.instance.musicScript.BossState(true);
    }

        // Update is called once per frame
    private void Update()
    {
        speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animrTransSpeed);
        anim.SetFloat("Speed", speed);

        if (currHP <= (maxHP / 2))
        {
            anim.SetTrigger("Phase 2");
        }

        if (!anim.GetBool("Phase 2"))
        {
            if (agent.remainingDistance < agent.stoppingDistance)
                anim.SetTrigger("Punch");
        }
        else if (anim.GetBool("Phase 2"))
        {
            //timer = Random.Range(minTime, maxTime);

            //if (timer <= 0)
            //{
            //    if (numMinions <= 0)
            //    {
            //        rand = Random.Range(0, 3);

            //        if (rand == 0)
            //            anim.SetTrigger("Jump Attack");
            //        else if (rand == 1)
            //            anim.SetTrigger("Shoot");
            //        else
            //            anim.SetTrigger("Summon Minions");
            //    }
            //    else
            //    {
            //        rand = Random.Range(0, 2);
            //        if (rand == 0)
            //            anim.SetTrigger("Jump Attack");
            //        else
            //            anim.SetTrigger("Shoot");
            //    }
            //}
            //else
            //    timer -= Time.deltaTime;

            rand = Random.Range(0, 1 * Time.deltaTime);
            if (numMinions <= 0)
            {
                if (rand == 0)
                    anim.SetTrigger("Jump Attack");
                else if (rand == 1)
                    anim.SetTrigger("Summon Minions");
                else if (rand == 2)
                    anim.SetTrigger("Shoot");
            }
            else
            {
                if (rand == 0)
                    anim.SetTrigger("Jump Attack");
                else if (rand == 1)
                    anim.SetTrigger("Shoot");
            }
        }
    }
    #endregion
    #region Functions
    #region Collider Enter/Exit
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInRange = true;

    //    }
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //agent.stoppingDistance = 0;
    //        playerInRange = false;
    //    }
    //}
    #endregion
    public void Punch()
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
                if (agent.isActiveAndEnabled)
                {
                    agent.SetDestination(gameManager.instance.player.transform.position);
                }

                if (agent.isActiveAndEnabled)
                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        FacePlayer();
                    }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    //IEnumerator SummonMinions()
    //{
    //    isSpawning = true;
    //    Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], spawnPos[Random.Range(0, spawnPos.Length)].position, transform.rotation);
    //    numMinions++;
    //    yield return new WaitForSeconds(0.1f);
    //    isSpawning = false;
    //}

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
            Instantiate(teleporter, transform.position, transform.rotation);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);

        }
        else
        {
            //animr.SetTrigger("Damaged");
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
            Instantiate(teleporter, transform.position, transform.rotation);
            anim.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);
        }
        else
        {
            //animr.SetTrigger("Damaged");
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
            Instantiate(teleporter, transform.position, transform.rotation);
            anim.SetBool("Death", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.instance.musicScript.BossState(false);

        }
        else
        {
            //animr.SetTrigger("Damaged");
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
    #region Effects
    //public void ApplyEffect(StatusEffectData data)
    //{

    //}
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
