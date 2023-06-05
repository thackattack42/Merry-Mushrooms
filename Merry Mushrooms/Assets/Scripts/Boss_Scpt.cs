using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Scpt : MonoBehaviour/*Enemy_Scpt, IFireDamage, IEarthDamage, IIceDamage, IEffectable*/
{
    #region Fields
    //[Header("------ Enemy Spawner ------")]
    //[SerializeField] GameObject[] enemyToSpawn;
    //[SerializeField] Transform[] spawnPoss;
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

    [Header("----- Boss Components -----")]
    [SerializeField] Animator anim;
    [SerializeField] Transform punchPos;
    NavMeshAgent agent;
    #endregion
    #region Start and Stop
    //new
    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        anim = GetComponent<Animator>();
        //base.Start();
        //gameManager.instance.UpdateGameGoal(1);
    }

    new
    // Update is called once per frame
    void Update()
    {
        if (currHP < (maxHP / 2))
        {
            anim.SetTrigger("Phase 2");
        }
        //base.Update();
        //spawnCountt = 2;
        //if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
        //{
        //    StartCoroutine(EnemySpawn());
        //}

        //else if (HP <= 50)
        //{
        //    spawnCountt = 5;
        //    if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
        //    {
        //        StartCoroutine(EnemySpawn());
        //    }
        //    if (playerInRange)
        //    {
        //        //make AOE attack
        //        //ApplyEffect()
        //    }
        //}
        //else if (HP <= 25)
        //{
        //    spawnCountt = 9;
        //    if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
        //    {
        //        StartCoroutine(EnemySpawn());
        //    }
        //    if (playerInRange)
        //    {

        //        //make AOE attack
        //        //ApplyEffect()
        //        //defence enabled//not needed
        //        //heal enemies in area//not needed
        //    }
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

    #region Spawner
    //IEnumerator EnemySpawn()
    //{
    //    isSpawningg = true;
    //    Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], spawnPoss[Random.Range(0, spawnPoss.Length)].position, transform.rotation);
    //    numberSpawnedd++;
    //    yield return new WaitForSeconds(spawnDelayy);
    //    isSpawningg = false;
    //}
    #endregion
    #region Damage Functions
    //public void TakeEarthDamage(int dmg)
    //{
    //    HP -= dmg * 2;

    //    if (HP <= 0)
    //    {
    //        gameManager.instance.UpdateGameGoal(-1);
    //        animr.SetBool("Death", true);
    //        agent.enabled = false;
    //        GetComponent<CapsuleCollider>().enabled = false;
    //    }
    //    else
    //    {
    //        animr.SetTrigger("Damaged");
    //        agent.SetDestination(gameManager.instance.player.transform.position);
    //        StartCoroutine(FlashHitColor());
    //    }
    //}


    //public void TakeIceDamage(int dmg)
    //{
    //    HP -= dmg / 2;

    //    if (HP <= 0)
    //    {
    //        gameManager.instance.UpdateGameGoal(-1);
    //        animr.SetBool("Death", true);
    //        agent.enabled = false;
    //        GetComponent<CapsuleCollider>().enabled = false;
    //    }
    //    else
    //    {
    //        animr.SetTrigger("Damaged");
    //        agent.SetDestination(gameManager.instance.player.transform.position);
    //        StartCoroutine(FlashHitColor());
    //    }
    //}

    //public void TakeFireDamage(int dmg)
    //{
    //    HP += dmg;
    //}
    #endregion
    #region Effects
    //public void ApplyEffect(StatusEffectData data)
    //{

    //}
    #endregion
    #endregion
}
