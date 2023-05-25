using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    //[SerializeField] Animator animr;
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnCount;

    int numberSpawned;
    bool playerInRange;
    bool isSpawning;

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
