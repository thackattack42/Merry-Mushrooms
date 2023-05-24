using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Scpt : Enemy_Scpt 
{

    [Header ("------ Enemy Spawner ------")]
    [SerializeField] GameObject[] enemyToSpawn;
    [SerializeField] Transform[] spawnPoss;
    [SerializeField] float spawnDelayy;
    int spawnCountt;
    int numberSpawnedd;
    bool isSpawningg;


    // Start is called before the first frame update
    void Start()
    {
        
        base.Start();
       
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        spawnCountt = 2;
        if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    void phases()
    {
        if(HP <= 0.5f)
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
        else if(HP <= 0.2f)
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
}
