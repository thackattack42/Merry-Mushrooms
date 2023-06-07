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
    [SerializeField] int maxHP;
    [SerializeField] int currHP;
    [SerializeField] int projectileDamage;
    [SerializeField] int punchDamage;
    [SerializeField] int AOEDamage;
    [SerializeField] public int numMinions;
    [SerializeField] public int maxMinions;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;
    public int viewCone;

    [Header("----- Boss Components -----")]
    [SerializeField] Animator anim;
    [SerializeField] Transform punchPos;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] public Transform headPos;
    [SerializeField] GameObject bullet;
    [SerializeField] public GameObject aoeAttack;

    [Header("----- Misc -----")]
    public bool isSpawning;
    public Vector3 playerDir;
    public float stoppingDistOrig;
    public float angleToPlayer;

    #endregion
    #region Start and Stop
    //new
    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        anim = GetComponent<Animator>();
        stoppingDistOrig = agent.stoppingDistance;
        //base.Start();
        gameManager.instance.UpdateGameGoal(1);
    }

        // Update is called once per frame
    private void Update()
    {
        if (currHP <= (maxHP / 2))
        {
            anim.SetTrigger("Phase 2");
        }

        //if (agent.remainingDistance < agent.stoppingDistance)
        //{
        //    FacePlayer();
        //}
    }
    #endregion
    #region Functions
    public void Punch()
    {
        punchPos.GetComponent<SphereCollider>().enabled = true;
        //Collider[] collider = Physics.OverlapCapsule(new Vector3(punchPos.position.x, punchPos.position.y, punchPos.position.z), gameManager.instance.player.transform.position, agent.stoppingDistance);
    }

    public void StopPunch()
    {
        punchPos.GetComponent<SphereCollider>().enabled = false;
    }

    public void AOEAttack()
    {
        Instantiate(aoeAttack, transform.position, aoeAttack.transform.rotation);
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
        transform.rotation = rot;//Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
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
                //agent.stoppingDistance = stoppingDistOrig;
                //agent.SetDestination(gameManager.instance.player.transform.position);

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
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            //animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            //StartCoroutine(FlashHitColor());
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
        }
        else
        {
            //animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            //StartCoroutine(FlashHitColor());
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
        }
        else
        {
            //animr.SetTrigger("Damaged");
            agent.SetDestination(gameManager.instance.player.transform.position);
            //StartCoroutine(FlashHitColor());
        }
    }

    public void TakeFireDamage(int dmg)
    {
        currHP += dmg;
    }
    #endregion
    #region Effects
    //public void ApplyEffect(StatusEffectData data)
    //{

    //}
    #endregion
    #endregion
}
