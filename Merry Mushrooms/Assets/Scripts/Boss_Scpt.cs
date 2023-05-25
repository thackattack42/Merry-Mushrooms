using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Scpt : Enemy_Scpt, IFireDamage, IEarthDamage, IIceDamage, IEffectable
{
    #region Fields
    [Header("------ Enemy Spawner ------")]
    [SerializeField] GameObject[] enemyToSpawn;
    [SerializeField] Transform[] spawnPoss;
    [SerializeField] float spawnDelayy;
    int spawnCountt;
    int numberSpawnedd;
    bool isSpawningg;
    #endregion
    #region Start and Stop
    new
    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        gameManager.instance.UpdateGameGoal(1);
    }

    new
    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (HP == 250)
        {
            spawnCountt = 2;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
        }
        else if (HP <= 175)
        {
            spawnCountt = 5;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            if (playerInRange)
            {
                //make AOE attack
                //ApplyEffect()
            }
        }
        else if (HP <= 100)
        {
            spawnCountt = 9;
            if (playerInRange && !isSpawningg && numberSpawnedd < spawnCountt)
            {
                StartCoroutine(EnemySpawn());
            }
            if (playerInRange)
            {
                
                //make AOE attack
                //ApplyEffect()
                //defence enabled//not needed
                //heal enemies in area//not needed
            }
            
        }


    }
    #endregion
    #region Functions
    #region Spawner
    IEnumerator EnemySpawn()
    {
        isSpawningg = true;
        Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], spawnPoss[Random.Range(0, spawnPoss.Length)].position, transform.rotation);
        numberSpawnedd++;
        yield return new WaitForSeconds(spawnDelayy);
        isSpawningg = false;
    }
    #endregion
    #region Damage Functions
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
    #endregion
    #region Effects
    public void ApplyEffect(StatusEffectData data)
    {

    }
    #endregion
    #endregion
}
