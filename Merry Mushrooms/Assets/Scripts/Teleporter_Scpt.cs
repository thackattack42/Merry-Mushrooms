using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_Scpt : MonoBehaviour
{
    [SerializeField] GameObject PlayerObj;
    [SerializeField] GameObject TeleporterObj;
    bool isPlayerInRange;

    private void Update()
    {
        if (isPlayerInRange)
        {

            StartCoroutine(Spawn());
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            StartCoroutine(gameManager.instance.YouWin());
        }
    }
    IEnumerator Spawn()
    {
        
        yield return new WaitForSeconds(1);
        PlayerObj.transform.position = TeleporterObj.transform.position;
        yield return new WaitForSeconds(1);
        isPlayerInRange = false;
    }
}
