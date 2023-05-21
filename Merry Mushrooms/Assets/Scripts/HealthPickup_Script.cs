using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup_Script : MonoBehaviour
{
    [SerializeField] public AudioSource aud;
    [SerializeField] public AudioClip[] audPickup;
    [SerializeField] public float audPickupVol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.playerScript.HP < gameManager.instance.playerScript.maxHP)
            {
                aud.PlayOneShot(audPickup[Random.Range(0, audPickup.Length)], audPickupVol);
                gameManager.instance.playerScript.HP = gameManager.instance.playerScript.maxHP;
                gameManager.instance.playerHUD.updatePlayerHealth(gameManager.instance.playerScript.HP);
                Destroy(gameObject);
            }
        }
    }
}
