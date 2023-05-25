using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Scpt : Enemy_Scpt, IFireDamage, IEarthDamage, IIceDamage
{

    [Header("------ Enemy Spawner ------")]
    [SerializeField] GameObject[] enemyToSpawn;
    [SerializeField] Transform[] spawnPoss;
    [SerializeField] float spawnDelayy;
    int spawnCountt;
    int numberSpawnedd;
    bool isSpawningg;





    new
    // Start is called before the first frame update
    void Start()
    {

        base.Start();

    }

    new
    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (HP == 1000)
        {
            spawnCountt = 2;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
        }
        else if (HP <= 500)
        {
            spawnCountt = 5;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            if (playerInRange)
            {
                //make AOE attack
            }
        }
        else if (HP <= 250)
        {
            spawnCountt = 9;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            if (playerInRange)
            {
                
                //make AOE attack
                //defence enabled
                //heal enemies in area
            }
            
        }


    }
    void phases()
    {
        if (HP <= 0.5f)
        {
            spawnCountt = 3;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            //spawn 3 E
            //if(player in range){
            //
            //make AOE attack take damage
            //
        }
        else if (HP <= 0.2f)
        {
            spawnCountt = 3;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            //spawn 4 E
            //
            //if(player in range){
            //
            //start another aoe}
            //if(!player in range){
            // start heal aoe then if hit stop heal aoe
            //
        }

    }
    IEnumerator EnemySpawn()
    {
        isSpawningg = true;
        Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], spawnPoss[Random.Range(0, spawnPoss.Length)].position, transform.rotation);
        numberSpawnedd++;
        yield return new WaitForSeconds(spawnDelayy);
        isSpawningg = false;
    }
    public void TakeEarthDamage(int dmg)
    {
        HP -= dmg * 2;

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
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


    public void TakeIceDamage(int dmg)
    {
        HP -= dmg / 2;

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
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

    public void TakeFireDamage(int dmg)
    {
        HP += dmg;
    }
}
