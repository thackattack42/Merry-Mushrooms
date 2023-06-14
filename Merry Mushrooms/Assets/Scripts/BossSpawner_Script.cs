using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner_Script : MonoBehaviour
{
    //[SerializeField] Animator animr;
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnCount;

    int numberSpawned;
    bool playerInRange;
    bool isSpawning;
    GameObject bossBarrier;
    BoxCollider bossBarrierCollider;

    private void Start()
    {
        bossBarrier = GameObject.FindGameObjectWithTag("Boss Barrier");
        bossBarrierCollider = bossBarrier.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !isSpawning && numberSpawned < spawnCount)
        {
            StartCoroutine(Spawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossBarrierCollider.enabled = true;
            playerInRange = true;
        }
    }
    IEnumerator Spawn()
    {
        isSpawning = true;
        Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], spawnPos[Random.Range(0, spawnPos.Length)].position, transform.rotation);
        numberSpawned++;
        yield return new WaitForSeconds(spawnDelay);
        isSpawning = false;
    }
}
