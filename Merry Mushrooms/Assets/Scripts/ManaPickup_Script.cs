using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup_Script : MonoBehaviour
{
    [SerializeField] public AudioSource aud;
    [SerializeField] public AudioClip[] audPickup;
    [SerializeField] public float audPickupVol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.playerScript.MP < gameManager.instance.playerScript.maxMP)
            {
                //aud.PlayOneShot(audPickup[Random.Range(0, audPickup.Length)], audPickupVol);
                gameManager.instance.playerScript.MP = gameManager.instance.playerScript.maxMP;
                //gameManager.instance.playerHUD.updatePlayerHealth(gameManager.instance.playerScript.MP);
                Destroy(gameObject);
            }
        }
    }
}
