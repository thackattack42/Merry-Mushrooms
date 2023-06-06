using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_Scpt : MonoBehaviour
{
    [SerializeField] GameObject PlayerObj;
    [SerializeField] Transform Tele;
    [SerializeField] GameObject TeleporterObj;
    bool isPlayerInRange;

    private void Update()
    {
        if (isPlayerInRange)
        {

            StartCoroutine(Spawn());
            isPlayerInRange = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; 
        }
    }
    IEnumerator Spawn()
    {
        
        yield return new WaitForSeconds(0.1f);
        PlayerObj.transform.position = Tele.position;
        yield return new WaitForSeconds(0.1f);
    }
}
