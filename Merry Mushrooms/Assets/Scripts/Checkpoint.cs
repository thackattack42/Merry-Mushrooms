using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameManager.instance.playerSpawnPos.transform.position = transform.position;
    }
}
