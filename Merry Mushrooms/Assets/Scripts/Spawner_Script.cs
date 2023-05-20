using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnCount;

    int numberSpawned;
    bool playerInRange;
    bool isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
}
